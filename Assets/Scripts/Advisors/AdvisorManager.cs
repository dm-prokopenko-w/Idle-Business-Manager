using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataSystem;
using ManagersSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace AdvisorSystem
{
    public class AdvisorManager : Singleton<AdvisorManager>
    {
        public static event UnityAction<AdvisorManager> AdvisorsLoadedEvent;

        [SerializeField] private GameObject confirmationPanel;

        [Header("Config")] 
        [SerializeField] private GameObject advisorsPanel;
        [SerializeField] private GameObject advisorsTutorialPanel;
        [SerializeField] private AdvisorCard cardPrefab;
        [SerializeField] private Transform cardContent;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Advisor[] advisors;
        [SerializeField] private AdvisorSlot[] slots;
        [SerializeField] private Button buyBtn;

        [Header("Buy Advisor")] 
        [SerializeField] private float spinTimer = 5f;
        [SerializeField] private TextMeshProUGUI availableAdvisorsTMP;
        [SerializeField] private TextMeshProUGUI buyAdvisorTMP;
        [SerializeField] private double randomAdvisorCost;
        [SerializeField] private float randomAdvisorCostMultiplier;

        [Header("New Hired Advisor")] 
        [SerializeField] private GameObject newHiredAvisorPanel;
        [SerializeField] private Image newAdvisorSprite;
        [SerializeField] private TextMeshProUGUI newAdvisorSubtitleTMP;
        [SerializeField] private GameObject newAdvisorIAP;
        [SerializeField] private GameObject LoadingAdvisor;
        [SerializeField] private GameObject LoadingNewAdvisor;

        [Header("Advisor Info")] 
        [SerializeField] private TextMeshProUGUI nameInfo;
        [SerializeField] private TextMeshProUGUI descriptionInfo;
        [SerializeField] private Image iconInfo;

        [Header("Advisor Buttons")] 
        [SerializeField] private Image iconBtn1;
        [SerializeField] private Image iconSubBtn1;
        [SerializeField] private Image iconBtn2;
        [SerializeField] private Image iconSubBtn2;
        [SerializeField] private Image iconBtn3;
        [SerializeField] private Image iconSubBtn3;

        [Header("Advisor Profile Config")] 
        [SerializeField] private Transform cardContentProfile;
        [SerializeField] private GameObject ProfilePickerPanel;
        
        private AdvisorSlot selectedSlot;
        private bool buyRandom;
        private float checkTimer;
        private List<AdvisorCard> cards = new();
        private List<AdvisorCard> cardsProfile = new();

        public static bool advisorsOpened;
        public Vector3 scrollRectStart;

        private void Start()
        {
            advisorsOpened = false;

            if (DataManager.Profile.advisors.Length < advisors.Length)  SyncAdvisorsManager();

            if (DataManager.Profile.advisors.Length == 0) SyncAdvisorsManager();
            else
            {
                for (int i = 0; i < DataManager.Profile.advisors.Length; i++)
                {
                    AdvisorType advisorType = GetAdvisorType(DataManager.Profile.advisors[i].Type);
                    AdvisorQuality advisorQuality = GetAdvisorQuality(DataManager.Profile.advisors[i].Quality);

                    advisors[i].Name = DataManager.Profile.advisors[i].Name;
                    advisors[i].Type = advisorType;
                    advisors[i].Quality = advisorQuality;
                    advisors[i].Multiplier = DataManager.Profile.advisors[i].Multiplier;
                    advisors[i].Description = DataManager.Profile.advisors[i].Description;
                    advisors[i].Bought = DataManager.Profile.advisors[i].Bought;
                    advisors[i].Selected = DataManager.Profile.advisors[i].Selected;
                    advisors[i].Index = DataManager.Profile.advisors[i].Index;
                }
            }

            if (DataManager.Profile.advisorsSlot.Length < slots.Length) SyncAdvisorsSlotManager();

            if (DataManager.Profile.advisorsSlot.Length == 0) SyncAdvisorsSlotManager();
            else
            {
                for (int i = 0; i < DataManager.Profile.advisorsSlot.Length; i++)
                {
                    slots[i].KeyAdvisor = DataManager.Profile.advisorsSlot[i].KeyAdvisor;
                    slots[i].SavedAdvisor = DataManager.Profile.advisorsSlot[i].KeyAdvisor;
                }
            }

            LoadAdvisors();
            LoadSlots();
            scrollRectStart = scrollRect.content.position;
            UpdateAvailableAdvisorsText();

            randomAdvisorCost = DataManager.Profile.randomAdvisorCost;

            buyAdvisorTMP.text = $"$ {randomAdvisorCost.CurrencyText()}";
        }

        public void SyncAdvisorsManager()
        {
            DataManager.Profile.advisors = new AdvisorData[advisors.Length];
            for (int i = 0; i < advisors.Length; i++)
            {
                if (!DataManager.Profile.hasDataAdvisors)
                {
                    advisors[i].Bought = false;
                    advisors[i].Selected = false;
                }

                DataManager.Profile.advisors[i] = new AdvisorData();
                DataManager.Profile.advisors[i].Name = advisors[i].Name;
                DataManager.Profile.advisors[i].Type = advisors[i].Type.ToString();
                DataManager.Profile.advisors[i].Quality = advisors[i].Quality.ToString();
                DataManager.Profile.advisors[i].Multiplier = advisors[i].Multiplier;
                DataManager.Profile.advisors[i].Description = advisors[i].Description;
                DataManager.Profile.advisors[i].Bought = advisors[i].Bought;
                DataManager.Profile.advisors[i].Selected = advisors[i].Selected;
                DataManager.Profile.advisors[i].Index = advisors[i].Index;
            }

            if (!DataManager.Profile.hasDataAdvisors)
                DataManager.Profile.hasDataAdvisors = true;

            DataManager.Profile.randomAdvisorCost = randomAdvisorCost;
        }

        public void SyncAdvisorsSlotManager()
        {
            DataManager.Profile.advisorsSlot = new AdvisorSlotData[slots.Length];
            for (int i = 0; i < slots.Length; i++)
            {
                DataManager.Profile.advisorsSlot[i] = new AdvisorSlotData();
                DataManager.Profile.advisorsSlot[i].index = slots[i].indexValue;
                DataManager.Profile.advisorsSlot[i].KeyAdvisor = slots[i].KeyAdvisor;
            }
        }

        private void LoadAdvisors()
        {
            for (int i = 0; i < advisors.Length; i++)
            {
                advisors[i].addedToProfile = false;
                AdvisorCard newCard = Instantiate(cardPrefab, cardContent);
                newCard.SetCard(advisors[i]);
                cards.Add(newCard);
            }
        }

        private void LoadSlots()
        {
            foreach (AdvisorSlot slot in slots)
            {
                foreach (Advisor advisor in advisors)
                {
                    if (slot.SavedAdvisor == $"{advisor.Quality} {advisor.Name}")
                    {
                        slot.Advisor = advisor;
                        slot.ShowAdvisor();
                        ShowAdvisorBtnPreview();
                    }
                }
            }

            AdvisorsLoadedEvent?.Invoke(this);
        }

        public void UpdateAvailableAdvisorsText()
        {
            availableAdvisorsTMP.text =
                $"Advisors \n {advisors.Length - GetAvailableAdvisors().Count} / {advisors.Length}";
        }

        private void ShowAdvisorBtnPreview()
        {
            if (slots[0].Advisor != null)
            {
                iconBtn1.sprite = slots[0].Advisor.Icon;
                iconSubBtn1.gameObject.SetActive(false);
            }
            else
            {
                iconBtn1.sprite = slots[0].iconRef.sprite;
                iconSubBtn1.gameObject.SetActive(true);
            }

            if (slots[1].Advisor != null)
            {
                iconBtn2.sprite = slots[1].Advisor.Icon;
                iconSubBtn2.gameObject.SetActive(false);
            }
            else
            {
                iconBtn2.sprite = slots[1].iconRef.sprite;
                iconSubBtn2.gameObject.SetActive(true);
            }

            if (slots[2].Advisor != null)
            {
                iconBtn3.sprite = slots[2].Advisor.Icon;
                iconSubBtn3.gameObject.SetActive(false);
            }
            else
            {
                iconBtn3.sprite = slots[2].iconRef.sprite;
                iconSubBtn3.gameObject.SetActive(true);
            }
        }

        private void ShowAdvisorDescription()
        {
            iconInfo.sprite = selectedSlot.Advisor.Icon;
            nameInfo.text = $"{selectedSlot.Advisor.Quality} {selectedSlot.Advisor.Name}";

            Color32 color = new Color32(122, 122, 122, 255);

            if (selectedSlot.Advisor.Quality == AdvisorQuality.Normal)
                color = new Color32(36, 144, 50, 172);
            if (selectedSlot.Advisor.Quality == AdvisorQuality.Rare)
                color = new Color32(35, 86, 144, 255);
            if (selectedSlot.Advisor.Quality == AdvisorQuality.Epic)
                color = new Color32(86, 35, 144, 255);
            if (selectedSlot.Advisor.Quality == AdvisorQuality.Legendary)
                color = new Color32(255, 117, 20, 255);

            nameInfo.color = color;
            descriptionInfo.text = selectedSlot.Advisor.Description;
        }

        public float GetAdvisorBenefit(AdvisorType type)
        {
            float value = 0;
            switch (type)
            {
                case AdvisorType.Earnings:
                case AdvisorType.Click:
                case AdvisorType.Offline:
                    value = ActiveAdvisorsBenefits(type);
                    break;

                case AdvisorType.UpgradeDiscount:
                case AdvisorType.UpgradeMilestone:
                    value = ActiveAdvisorsBenefits(type);
                    value /= 100;
                    break;
            }

            return value;
        }

        private float ActiveAdvisorsBenefits(AdvisorType type) => 
            (from slot in slots where slot.Advisor != null where slot.Advisor.Type == type select slot.Advisor.Multiplier).Sum();

        public void BuyNewAdvisor()
        {
            List<AdvisorCard> availables = GetAvailableAdvisors();
            if (availables == null || availables.Count <= 0)
            {
                return;
            }

            if (EarningsManager.Instance.totalEarnings < randomAdvisorCost)
            {
                return;
            }

            newAdvisorIAP.SetActive(true);
            EarningsManager.Instance.RemoveEarnings(randomAdvisorCost);

            StartCoroutine(IEPickRandom(false));
        }

        public IEnumerator IEPickRandom(bool isPromo)
        {
            buyBtn.interactable = false;
            buyRandom = true;
            List<AdvisorCard> availablesCards = new List<AdvisorCard>();

            if (isPromo)
                availablesCards = GetAvailablePromoAdvisors(AdvisorQuality.Legendary);
            else
                availablesCards = GetAvailableAdvisors();

            if (availablesCards.Count > 1)
            {
                while (buyRandom)
                {
                    int random = Random.Range(0, Random.Range(0, availablesCards.Count));

                    checkTimer += Time.deltaTime * 40f;
                    availablesCards[random].ShowPicker();

                    if (checkTimer >= spinTimer)
                    {
                        checkTimer = 0;
                        buyRandom = false;
                        if (EarningsManager.Instance.totalEarnings >= randomAdvisorCost &&
                            GetAvailableAdvisors().Count > 0)
                            buyBtn.interactable = true;
                        else
                            buyBtn.interactable = false;
                        randomAdvisorCost *= randomAdvisorCostMultiplier;
                        buyAdvisorTMP.text = $"$ {randomAdvisorCost.CurrencyText()}";

                        DataManager.Profile.randomAdvisorCost = randomAdvisorCost;

                        availablesCards[random].UnlockCard();
                        OpenNewHiredAdvisorPanel(availablesCards[random]);
                        confirmationPanel.SetActive(false);
                        if (LoadingAdvisor != null && LoadingAdvisor.activeSelf)
                            LoadingAdvisor.SetActive(false);
                        if (LoadingNewAdvisor != null && LoadingNewAdvisor.activeSelf)
                            LoadingNewAdvisor.SetActive(false);

                        UpdateAvailableAdvisorsText();
                        if (GetAvailablePromoAdvisors(AdvisorQuality.Legendary) == null ||
                            GetAvailablePromoAdvisors(AdvisorQuality.Legendary).Count == 0)
                            IAPManager.Instance.HideFirstPurchaseOption();
                    }

                    yield return new WaitForSeconds(0.2f);
                }
            }
            else
            {
                try
                {
                    availablesCards[0].UnlockCard();
                    OpenNewHiredAdvisorPanel(availablesCards[0]);
                    UpdateAvailableAdvisorsText();
                }
                catch (System.Exception e)
                {
                    newHiredAvisorPanel.SetActive(false);

                    Debug.Log($"ADVISORS RANDOM ERROR {JsonUtility.ToJson(availablesCards)} >>> {e.Message}");
                    SettingsManager.Instance.OpenConfirmationAlert("You have already bought all the advisors");
                }

                buyRandom = false;
                buyBtn.interactable = true;

                if (GetAvailablePromoAdvisors(AdvisorQuality.Legendary) == null ||
                    GetAvailablePromoAdvisors(AdvisorQuality.Legendary).Count == 0)
                    IAPManager.Instance.HideFirstPurchaseOption();
            }
        }

        private List<AdvisorCard> GetAvailableAdvisors() =>
            cards.Where(card => !card.Advisor.Bought).ToList();

        public List<AdvisorCard> GetAvailablePromoAdvisors(AdvisorQuality quality) =>
            cards.Where(card => !card.Advisor.Bought && card.Advisor.Quality.Equals(quality)).ToList();


        public void OpenAdvisorsPanel()
        {
            advisorsPanel.transform.localPosition = Vector3.zero;
            advisorsOpened = true;

            if (!PlayerPrefs.HasKey(Constants.AdvisorsTutorialKey))
                OpenAdvisorsTutorial();

            buyBtn.interactable = EarningsManager.Instance.totalEarnings >= randomAdvisorCost &&
                                  GetAvailableAdvisors().Count >= 0;
        }

        public void OpenAdvisorsTutorial()
        {
            advisorsTutorialPanel.SetActive(true);
        }

        public void CloseAdvisorsTutorial()
        {
            if (!PlayerPrefs.HasKey(Constants.AdvisorsTutorialKey))
                PlayerPrefs.SetInt(Constants.AdvisorsTutorialKey, 1);

            advisorsTutorialPanel.SetActive(false);
        }

        public void CloseAdvisorsPanel()
        {
            advisorsPanel.transform.localPosition = Vector3.right * 1500f;
            advisorsOpened = false;
            scrollRect.content.position = scrollRectStart;
        }

        private void SlotSelectedResponse(AdvisorSlot selectedSlot)
        {
            this.selectedSlot = selectedSlot;
            if (this.selectedSlot.Advisor != null)
            {
                ShowAdvisorDescription();
            }
        }

        private void CardSelectedResponse(AdvisorCard selectedCard)
        {
            if (selectedSlot == null) return;
            foreach (var t in slots)
            {
                if (t.Advisor == selectedCard.Advisor)
                {
                    t.ClearAdvisor();
                }
            }

            selectedSlot.AdvisorReference(selectedCard);
            selectedSlot.ShowAdvisor();
            ShowAdvisorBtnPreview();
            ShowAdvisorDescription();
        }

        private void OnEnable()
        {
            AdvisorSlot.SlotSelectedEvent += SlotSelectedResponse;
            AdvisorCard.CardSelectedEvent += CardSelectedResponse;
            AdvisorCard.CardSelectedProfileEvent += CardSelectedProfileResponse;
        }

        private void OnDisable()
        {
            AdvisorSlot.SlotSelectedEvent -= SlotSelectedResponse;
            AdvisorCard.CardSelectedEvent -= CardSelectedResponse;
            AdvisorCard.CardSelectedProfileEvent -= CardSelectedProfileResponse;
        }

        public static AdvisorType GetAdvisorType(string type)
        {
            if (AdvisorType.Earnings.ToString() == type) return AdvisorType.Earnings;

            if (AdvisorType.Click.ToString() == type) return AdvisorType.Click;

            if (AdvisorType.Offline.ToString() == type) return AdvisorType.Offline;

            if (AdvisorType.UpgradeDiscount.ToString() == type) return AdvisorType.UpgradeDiscount;

            return AdvisorType.UpgradeMilestone;
        }

        public static AdvisorQuality GetAdvisorQuality(string type)
        {
            if (AdvisorQuality.Common.ToString() == type) return AdvisorQuality.Common;

            if (AdvisorQuality.Normal.ToString() == type) return AdvisorQuality.Normal;
           
            if (AdvisorQuality.Rare.ToString() == type) return AdvisorQuality.Rare;
            
            if (AdvisorQuality.Epic.ToString() == type) return AdvisorQuality.Epic;

            return AdvisorQuality.Legendary;
        }

        private void OpenNewHiredAdvisorPanel(AdvisorCard card)
        {
            newAdvisorSprite.sprite = card.Advisor.Icon;
            newAdvisorSubtitleTMP.text =
                $"Congratulations a {card.Advisor.Quality} {card.Advisor.Name} has joined your empire!";

            if (GetAvailableAdvisors().Count == 0 || IAPManager.recentlyBought)
            {
                newAdvisorIAP.SetActive(false);
                IAPManager.recentlyBought = false;
            }

            newHiredAvisorPanel.transform.localPosition = Vector3.zero;
            newHiredAvisorPanel.SetActive(true);
        }

        public void CloseNewHiredAdvisorPanel() => newHiredAvisorPanel.SetActive(false);

        public void OpenProfilePickerPanel()
        {
            LoadAdvisorsProfile();

            ProfilePickerPanel.SetActive(true);
        }

        public void CloseProfilePickerPanel() =>  ProfilePickerPanel.SetActive(false);

        public AdvisorCard GetProfileAdvisorCard(string name) => 
            cardsProfile.FirstOrDefault(card => name.Equals($"{card.Advisor.Quality} {card.Advisor.Name}"));

        private void LoadAdvisorsProfile()
        {
            for (int i = 0; i < advisors.Length; i++)
            {
                if ((advisors[i].Bought || advisors[i].Quality.Equals(AdvisorQuality.Common)) &&
                    !advisors[i].addedToProfile)
                {
                    advisors[i].addedToProfile = true;
                    AdvisorCard newCard = Instantiate(cardPrefab, cardContentProfile);
                    newCard.SetCardProfile(advisors[i]);
                    cardsProfile.Add(newCard);
                }
            }
        }

        private void CardSelectedProfileResponse(AdvisorCard selectedCard)
        {
            Debug.Log("SETTING PROFILE IMG");
            DataManager.Profile.profileAdvisor = $"{selectedCard.Advisor.Quality} {selectedCard.Advisor.Name}";

            SettingsManager.Instance.SetProfileImage(selectedCard.Advisor.Icon);

            CloseProfilePickerPanel();
        }
    }
}