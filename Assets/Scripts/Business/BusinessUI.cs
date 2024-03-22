using DataSystem;
using ManagersSystem;
using QuestsSystem;
using SavesSystem;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BusinessSystem
{
    public class BusinessUI : MonoBehaviour
    {
        public static event UnityAction<BusinessController> BusinessBoughtEvent;

        [Header("Config")]
        [SerializeField] private Business businessSO;

        [Header("Sprites")]
        [SerializeField] private SpriteRenderer firstFloor;
        [SerializeField] private SpriteRenderer secondFloor;
        [SerializeField] private SpriteRenderer roofFloor;

        [Header("Buyout Panel")]
        [SerializeField] private GameObject businessBuyoutPanel;
        [SerializeField] private Image buyoutIcon;
        [SerializeField] private Button buyBtn;
        [SerializeField] private TextMeshProUGUI buyoutName;
        [SerializeField] private TextMeshProUGUI buyoutCost;

        [Header("Benefits Panel")]
        [SerializeField] private GameObject businessPanel;
        [SerializeField] private Image businessImage;
        [SerializeField] private TextMeshProUGUI timerTMP;
        [SerializeField] private TextMeshProUGUI nameTMP;
        [SerializeField] private TextMeshProUGUI upgradeCostTMP;
        [SerializeField] private Button upgradeBtn;
        [SerializeField] private Sprite upgradeNormal;
        [SerializeField] private Sprite upgradeMilestone;
        [SerializeField] private GameObject timerContainer;

        [Header("Benefit Loader")]
        [SerializeField] private Image benefitLoader;
        [SerializeField] private TextMeshProUGUI benefitTMP;

        [Header("Level Loader")]
        [SerializeField] private Image levelLoader;
        [SerializeField] private TextMeshProUGUI levelTMP;

        [Header("Business Controller")]
        [SerializeField] private BusinessController myController;
        
        private void Start()
        {
            LoadBusinessInfo();
            ShowInfo();
        }

        private void Update()
        {
            if (businessSO == null) return;

            UpdateValues();
            UpdateButtons();

            if (EarningsManager.Instance.totalEarnings >= GameManager.Instance.costNewBusiness)
                buyBtn.interactable = true;
            else
                buyBtn.interactable = false;

            if (EarningsManager.Instance.totalEarnings >= myController.GetUpdatedCost())
                upgradeBtn.interactable = true;
            else
                upgradeBtn.interactable = false;
        }

        private void LoadBusinessInfo()
        {
            firstFloor.sprite = businessSO.firstFloor;
            secondFloor.sprite = businessSO.secondFloor;
            roofFloor.sprite = businessSO.roofFloor;

            buyoutIcon.sprite = businessSO.icon;
            buyoutName.text = businessSO.name;
            businessImage.sprite = businessSO.icon;
            nameTMP.text = businessSO.name;
        }

        private void ShowInfo()
        {
            if (myController.bought)
            {
                ActivateBuyoutPanel(false);
                ActivateBusinessPanel(true);
                if (myController.hideTimer)
                {
                    timerContainer.SetActive(false);
                }
            }
            else
            {
                if (myController.Index == 0)
                {
                    ActivateBuyoutPanel(true);
                }

                if (myController.canBuyout)
                {
                    ActivateBuyoutPanel(true);
                    ActivateBusinessPanel(false);
                }
            }
        }

        private void UpdateValues()
        {
            if (!myController.bought)
            {
                return;
            }

            timerTMP.text = myController.GetTimer();
            benefitLoader.fillAmount = myController.GetLoaderValueBenefit();
            upgradeCostTMP.text = $"Upgrade \n ${myController.GetUpdatedCost().CurrencyText()}";

            levelLoader.fillAmount = myController.GetValueLevel();
            levelTMP.text = myController.level.ToString();

            if (myController.benefitTimer > 1)
            {
                benefitTMP.text = $"${myController.GetBenefits().CurrencyText()}";
            }
            else
            {
                benefitTMP.text = $"${myController.GetBenefits().CurrencyText()}/s";
            }
        }

        private void UpdateButtons()
        {
            if (myController.isNextLevelMilestone)
            {
                upgradeBtn.GetComponent<Image>().sprite = upgradeMilestone;
            }
            else
            {
                upgradeBtn.GetComponent<Image>().sprite = upgradeNormal;
            }
        }

        public void UpgradeBusiness()
        {
            if (EarningsManager.Instance.totalEarnings >= myController.GetUpdatedCost())
            {
                EarningsManager.Instance.RemoveEarnings(myController.GetUpdatedCost());
                myController.UpgradeBusiness();
                QuestManager.Instance.AddProgress(businessSO.id);
                QuestManager.Instance.AddProgress($"{businessSO.id}II");
                SavesManager.SaveBusinesses();
            }
        }

        private void ActivateBuyoutPanel(bool value)
        {
            businessBuyoutPanel.SetActive(value);
            if (value)
            {
                buyoutCost.text = $"Buy \n ${GameManager.Instance.costNewBusiness.CurrencyText()}";
            }
        }

        private void ActivateBusinessPanel(bool value)
        {
            businessPanel.SetActive(value);
        }

        public void BuyoutBusiness()
        {
            if (EarningsManager.Instance.totalEarnings >= GameManager.Instance.costNewBusiness)
            {
                QuestManager.Instance.AddProgress(businessSO.id);
                QuestManager.Instance.AddProgress($"{businessSO.id}II");

                ActivateBuyoutPanel(false);
                ActivateBusinessPanel(true);
                EarningsManager.Instance.RemoveEarnings(GameManager.Instance.costNewBusiness);
                GameManager.Instance.setNewBusiness(myController);
                SavesManager.SaveBusinesses();
                BusinessBoughtEvent?.Invoke(myController);
                DataManager.Instance.SaveUserProfile();

                if (businessSO.name.Equals("Casino"))
                {
                    SettingsManager.Instance.OpenCongratulationsPanel();
                }
            }
        }

        private void BusinessBoughtResponse(BusinessController boughtBusiness)
        {
            if (boughtBusiness.Index + 1 == myController.Index)
            {
                ActivateBuyoutPanel(true);
            }
        }

        private void NewMilestoneResponse(BusinessController boughtBusiness)
        {
            if (boughtBusiness == myController)
            {
                if (myController.hideTimer)
                {
                    timerContainer.SetActive(false);
                }
            }
        }

        private void OnEnable()
        {
            BusinessBoughtEvent += BusinessBoughtResponse;
            BusinessController.newMilestoneEvent += NewMilestoneResponse;
        }

        private void OnDisable()
        {
            BusinessBoughtEvent -= BusinessBoughtResponse;
            BusinessController.newMilestoneEvent -= NewMilestoneResponse;
        }
    }
}