using System.Collections;
using AdvisorSystem;
using DataSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManagersSystem
{
    public class IAPManager : Singleton<IAPManager>
    {
        [SerializeField] private GameObject firstPurchaseOption;
        [SerializeField] private GameObject removeAdsOption;
        [SerializeField] private GameObject increasedEarningsOption;
        [SerializeField] private GameObject firstPurchaseBtn;
        [SerializeField] private GameObject removeAdsBtn;
        [SerializeField] private GameObject doubleProfitsBtn;
        [SerializeField] private GameObject newHireAdvisorBtn;
        [SerializeField] private GameObject storeOfferPanel;
        [SerializeField] private GameObject newHiredAvisorPanel;
        [SerializeField] private Image loading;
        [SerializeField] private Button awesomeBtn;
        [SerializeField] private Image loadingNewHiredAdvisor;
        [SerializeField] private Button newAwesomeBtn;
        [SerializeField] private GameObject GiftCodePanel;
        [SerializeField] private TMP_InputField giftCodeField;

        [SerializeField] private GameObject GiftRedeemedPanel;
        [SerializeField] private TextMeshProUGUI descriptionTMP;

        public static string KEY_FIRST_PURCHASE = "KEY_FIRST_PURCHASE";
        public static string KEY_REMOVE_ADS = "KEY_REMOVE_ADS";
        public static string KEY_DOUBLE_PROFITS = "KEY_DOUBLE_PROFITS";

        public static bool recentlyBought;

        private void Start()
        {
            if (DataManager.Profile.firstPurchaseCompleted || AdvisorManager.Instance.GetAvailablePromoAdvisors(AdvisorQuality.Legendary) == null
                || AdvisorManager.Instance.GetAvailablePromoAdvisors(AdvisorQuality.Legendary).Count == 0)
            {
                firstPurchaseOption.SetActive(false);
            }

            if (DataManager.Profile.removeAds)
            {
                removeAdsOption.SetActive(false);
            }

            if (DataManager.Profile.multiplier)
            {
                increasedEarningsOption.SetActive(false);
            }
        }

        public void OpenGiftCodePanel()
        {
            giftCodeField.text = "";
            GiftCodePanel.SetActive(true);
        }

        public void CloseGiftCodePanel()
        {
            GiftCodePanel.SetActive(false);
        }

        public void OnFirstPurchaseCompleted()
        {
            DataManager.Profile.firstPurchaseCompleted = true;

            DataManager.Instance.SaveUserProfile();

            StartCoroutine(AdvisorManager.Instance.IEPickRandom(true));
            firstPurchaseOption.SetActive(false);
            firstPurchaseBtn.transform.localPosition = Vector3.right * 2750f;

            StartCoroutine(IEClosePanel());
        }

        public void OnRemoveAdsPurchaseCompleted()
        {
            DataManager.Profile.removeAds = true;

            DataManager.Instance.SaveUserProfile();

            removeAdsOption.SetActive(false);
            storeOfferPanel.transform.localPosition = Vector3.right * 2750f;
            removeAdsBtn.transform.localPosition = Vector3.right * 2750f;
            SettingsManager.Instance.OpenConfirmationAlert("Thank you for your incredible support!");
        }

        public void OnDoubleProfitsPurchaseCompleted()
        {
            DataManager.Profile.multiplier = true;

            DataManager.Instance.SaveUserProfile();

            increasedEarningsOption.SetActive(false);
            storeOfferPanel.transform.localPosition = Vector3.right * 2750f;
            doubleProfitsBtn.transform.localPosition = Vector3.right * 2750f;
            SettingsManager.Instance.OpenConfirmationAlert("Thank you for your incredible support!");
        }

        public void OnHireNewAdvisorPurchaseCompleted()
        {
            StartCoroutine(AdvisorManager.Instance.IEPickRandom(false));

            newHireAdvisorBtn.transform.localPosition = Vector3.right * 2750f;
            recentlyBought = true;
            StartCoroutine(IECloseNewHiredAdvisorPanel());
        }

        public void OnDonationCompleted()
        {
            SettingsManager.Instance.CloseCongratulationsPanel();
            SettingsManager.Instance.OpenConfirmationAlert("Thank you for your incredible support! This will help us a lot to keep making games!");
        }

        public void OpenStoreOfferPanel()
        {
            storeOfferPanel.SetActive(true);
        }

        public void CloseStoreOfferPanel()
        {
            storeOfferPanel.SetActive(false);
        }

        private IEnumerator IEClosePanel()
        {
            float spinTimer = 1f;

            loading.gameObject.SetActive(true);
            loading.fillAmount = 0;
            awesomeBtn.interactable = false;

            while (spinTimer != 5f)
            {
                loading.fillAmount = spinTimer / 5f;
                yield return new WaitForSeconds(1);
                spinTimer++;
            }

            storeOfferPanel.transform.localPosition = Vector3.right * 2750f;
            awesomeBtn.interactable = true;
            loading.gameObject.SetActive(false);
        }

        private IEnumerator IECloseNewHiredAdvisorPanel()
        {
            float spinTimer = 1f;
            float seconds = 5f;

            loadingNewHiredAdvisor.gameObject.SetActive(true);
            loadingNewHiredAdvisor.fillAmount = 0;
            newAwesomeBtn.interactable = false;

            while (spinTimer != seconds)
            {
                loadingNewHiredAdvisor.fillAmount = spinTimer / seconds;
                yield return new WaitForSeconds(1);
                spinTimer++;
            }

            newAwesomeBtn.interactable = true;
        }

        public void RedeemGiftCode()
        {
            GiftCodePanel.SetActive(false);
            descriptionTMP.text = "Congratulations gift code claimed!";
            GiftRedeemedPanel.SetActive(true);
        }

        public void HideFirstPurchaseOption()
        {
            firstPurchaseOption.SetActive(false);
        }

        public void CloseGiftRedeemedPanel()
        {
            GiftRedeemedPanel.SetActive(false);
        }
    }
}