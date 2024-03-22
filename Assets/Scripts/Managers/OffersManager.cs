using System.Collections;
using System.Collections.Generic;
using DataSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ManagersSystem
{
    public class OffersManager : Singleton<OffersManager>
    {
        [SerializeField] private GameObject offersPanel;
        [SerializeField] private GameObject speedClaimBtn;
        [SerializeField] private GameObject claimBtn;
        [SerializeField] private GameObject freeClaimBtn;
        [SerializeField] private GameObject speedFreeClaimBtn;
        [SerializeField] private TextMeshProUGUI adTitleTMP;
        [SerializeField] private TextMeshProUGUI adSubtitleTMP;
        [SerializeField] private Image adIconImg;
        [SerializeField] private Sprite speedSprite;
        [SerializeField] private Sprite moneySprite;
        [SerializeField] private TextMeshProUGUI speedTMP;

        [SerializeField] private GameObject storeOffersPanel;
        [SerializeField] private GameObject firstPurchaseIAP;
        [SerializeField] private GameObject removeAdsIAP;
        [SerializeField] private GameObject increaseEarningsIAP;
        [SerializeField] private Image icon;
        [SerializeField] private Image storeIcon;
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI subtitleTMP;
        [SerializeField] private TextMeshProUGUI storeAmountTMP;
        [SerializeField] private TextMeshProUGUI amountTMP;
        [SerializeField] private Sprite[] sprites;

        private float[] multipliers = { 0.39f, 0.42f, 0.65f, 0.67f, 0.48f, 0.72f, 0.54f };

        public double rewardAmount;

        public bool isSpeedAd = false;

        public static bool offersOpened;

        private void Start()
        {
            offersOpened = false;
        }

        public void OpenFirstPurchaseOfferPanel()
        {
            int randomAdvisor = Random.Range(0, 4);
            storeIcon.sprite = sprites[randomAdvisor];

            firstPurchaseIAP.SetActive(true);
            removeAdsIAP.SetActive(false);
            increaseEarningsIAP.SetActive(false);

            titleTMP.text = Constants.FirstPurchase;
            subtitleTMP.text = Constants.LegendaryAdvisor;
            storeAmountTMP.text = "$" + Constants.Price3;

            if (storeOffersPanel.activeSelf)
            {
                storeOffersPanel.transform.localPosition = Vector3.zero;
            }
            else
            {
                storeOffersPanel.SetActive(true);
            }
        }

        public void OpenRemoveAdsOfferPanel()
        {
            int randomAdvisor = Random.Range(0, 4);
            storeIcon.sprite = sprites[randomAdvisor];

            firstPurchaseIAP.SetActive(false);
            removeAdsIAP.SetActive(true);
            increaseEarningsIAP.SetActive(false);

            titleTMP.text = Constants.NoAdsOffer;
            subtitleTMP.text = Constants.WithoutAdsOnly;
            storeAmountTMP.text = "$" + Constants.Price1;

            if (storeOffersPanel.activeSelf)
            {
                storeOffersPanel.transform.localPosition = Vector3.zero;
            }
            else
            {
                storeOffersPanel.SetActive(true);
            }
        }

        public void OpenIncreasedEarningsOfferPanel()
        {
            int randomAdvisor = Random.Range(0, 4);
            storeIcon.sprite = sprites[randomAdvisor];

            firstPurchaseIAP.SetActive(false);
            removeAdsIAP.SetActive(false);
            increaseEarningsIAP.SetActive(true);

            titleTMP.text = Constants.CashForever;
            subtitleTMP.text = Constants.IncreasedCashOnly;
            storeAmountTMP.text = "$" + Constants.Price2;

            if (storeOffersPanel.activeSelf)
            {
                storeOffersPanel.transform.localPosition = Vector3.zero;
            }
            else
            {
                storeOffersPanel.SetActive(true);
            }
        }

        public void DeclineOffer()
        {
            offersOpened = false;
            speedClaimBtn.SetActive(false);
            offersPanel.SetActive(false);
        }

        public void SetOfferRewardAmount()
        {
            int randomMultiplier = Random.Range(0, multipliers.Length - 1);

            if (DataManager.Profile.sessionMaxEarningsReached < 950)
            {
                rewardAmount = 650 * multipliers[randomMultiplier];
            }
            else
            {
                rewardAmount = (DataManager.Profile.sessionMaxEarningsReached * 0.38f) * multipliers[randomMultiplier];
            }
        }
    }
}