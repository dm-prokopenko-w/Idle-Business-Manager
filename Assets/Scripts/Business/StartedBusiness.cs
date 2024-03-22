using AdvisorSystem;
using DataSystem;
using ManagersSystem;
using QuestsSystem;
using UnityEngine;

namespace BusinessSystem
{
    public class StartedBusiness : MonoBehaviour
    {
        [Header("FX")]
        [SerializeField] private Transform textPositionFx;

        [Header("Config")]
        [SerializeField] private float initEarnings = 1;
        [SerializeField] private float benefitMultiplier = 15;
        [SerializeField] private float benefitMilestone = 2;
        [SerializeField] private AudioSource audioMp3;

        [SerializeField] private GameObject tutorialTap;

        private double earnings;
        private double earningsTemp;

        void Start()
        {
            if (DataManager.Profile.startedBusinessEarnings == 0d)
            {
                earnings = initEarnings;
                DataManager.Profile.startedBusinessEarnings = earnings;
            }
            else
            {
                earnings = DataManager.Profile.startedBusinessEarnings;
            }

            if (EarningsManager.Instance.totalEarnings == 0)
                tutorialTap.SetActive(true);
            else
                tutorialTap.SetActive(false);
        }

        public void GetEarnings()
        {
            earningsTemp = earnings;
            float touchUtility = AdvisorManager.Instance.GetAdvisorBenefit(AdvisorType.Click);

            if (touchUtility != 0f)
            {
                earningsTemp += (earningsTemp * touchUtility);
            }

            if (audioMp3 != null && PlayerPrefs.GetInt(ProfileOptionsManager.KEY_SOUND_PREF, 1) == 1)
                audioMp3.Play();

            if (DataManager.Profile.multiplier)
            {
                earningsTemp *= 2;
            }

            EarningsManager.Instance.AddEarnings(earningsTemp);
            FXManager.Instance.ShowText(textPositionFx, earningsTemp);
            QuestManager.Instance.AddProgress(Constants.InitialBusiness);

            if (EarningsManager.Instance.totalEarnings > 0)
            {
                tutorialTap.SetActive(false);
            }
        }

        private void BusinessBoughtResponse(BusinessController boughtBusiness)
        {
            if (GameManager.Instance.boughtBusinesses > 1)
            {
                earnings *= benefitMultiplier;

                DataManager.Profile.startedBusinessEarnings = earnings;
            }
        }

        private void NewMilestoneResponse(BusinessController boughtBusiness)
        {
            earnings += benefitMilestone * boughtBusiness.milestones;

            DataManager.Profile.startedBusinessEarnings = earnings;
        }

        private void OnEnable()
        {
            BusinessUI.BusinessBoughtEvent += BusinessBoughtResponse;
            BusinessController.newMilestoneEvent += NewMilestoneResponse;
        }

        private void OnDisable()
        {
            BusinessUI.BusinessBoughtEvent -= BusinessBoughtResponse;
            BusinessController.newMilestoneEvent -= NewMilestoneResponse;
        }
    }
}