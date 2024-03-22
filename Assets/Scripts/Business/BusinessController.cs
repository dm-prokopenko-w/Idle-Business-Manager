using AdvisorSystem;
using DataSystem;
using ManagersSystem;
using QuestsSystem;
using UnityEngine;
using UnityEngine.Events;

namespace BusinessSystem
{
    public class BusinessController : MonoBehaviour
    {
        public static event UnityAction<BusinessController> newMilestoneEvent;

        [Header("FX")]
        [SerializeField] private Transform textPositionFx;

        [Header("Config")]
        [SerializeField] private int index;
        [SerializeField] private AudioSource audioMp3;

        public double milestones { get; set; }
        public float level { get; set; } = 1;
        public int Index => index;

        public bool bought { get; set; }
        public bool canBuyout { get; set; }
        public double benefits { get; private set; }
        public double benefitsPerUpgrade { get; private set; }
        public double upgradeCost { get; private set; }
        public double upgradePercentageCost { get; private set; }
        public float benefitTimer { get; private set; }
        public bool isNextLevelMilestone => (level + 1) % 25 == 0;
        public bool hideTimer => benefitTimer <= 1;

        private float timer;
        private float minutes;
        private float seconds;

        private void Start()
        {
            if (benefitTimer == 0) return;
            timer = benefitTimer;
        }

        private void Update()
        {
            if (!bought) return;
            GenerateBenefits();
        }

        private void GenerateBenefits()
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60);
            seconds = timer % 60;

            if (minutes < 0f)
            {
                minutes = 0f;
                seconds = 0f;
                timer = benefitTimer;

                EarningsManager.Instance.AddEarnings(GetBenefits());

                if (audioMp3 != null && !AdvisorManager.advisorsOpened && !SettingsManager.settingsOpened
                    && !QuestManager.questsOpened && PlayerPrefs.GetInt(ProfileOptionsManager.KEY_SOUND_PREF, 1) == 1)
                    audioMp3.Play();

                FXManager.Instance.ShowText(textPositionFx, GetBenefits());
            }
        }

        public void UpgradeBusiness()
        {
            level++;
            benefits += benefitsPerUpgrade;

            if (level % 25 == 0)
            {
                milestones++;
                upgradeCost *= 2;
                if (benefitTimer > 1)
                {
                    benefitTimer *= 0.75f;
                }

                newMilestoneEvent?.Invoke(this);
            }
            else
            {
                double extraCost = upgradeCost * (upgradePercentageCost / 100f);
                upgradeCost += extraCost;
            }

            if (milestones > 0 && level % 25 == 0)
            {
                benefits *= 2;
                upgradeCost *= 1.2;
                benefitsPerUpgrade *= 2;
            }
        }

        public double GetBenefits()
        {
            double benefitsTemp = benefits;
            float utility = AdvisorManager.Instance.GetAdvisorBenefit(AdvisorType.Earnings);
            if (utility != 0f)
            {
                benefitsTemp += (benefitsTemp * utility);
            }

            if (DataManager.Profile != null && DataManager.Profile.multiplier)
            {
                benefitsTemp *= 2;
            }

            return benefitsTemp;
        }

        public double GetUpdatedCost()
        {
            double upgradeCostTemp = upgradeCost;
            double milestoneCostTemp = upgradeCost * GameManager.Instance.milestoneMulti;

            float upgradeUtility = AdvisorManager.Instance.GetAdvisorBenefit(AdvisorType.UpgradeDiscount);
            float milestoneUtility = AdvisorManager.Instance.GetAdvisorBenefit(AdvisorType.UpgradeMilestone);

            if (upgradeUtility != 0f)
            {
                upgradeCostTemp -= (upgradeCost * upgradeUtility);
            }

            if (milestoneUtility != 0f)
            {
                milestoneCostTemp -= (milestoneCostTemp * milestoneUtility);
            }

            return isNextLevelMilestone ? milestoneCostTemp : upgradeCostTemp;
        }

        public string GetTimer() => $"{minutes:#00}:{seconds:#00}";

        public float GetValueLevel()
        {
            if (level % 25 == 0)
                return 0;

            float multiplier = Mathf.Ceil(level / 25f);
            return 1 - (((25f * multiplier) - level) / 25f);
        }

        public float GetLoaderValueBenefit() =>  milestones < 4 ? (benefitTimer - timer) / benefitTimer : 1f;

        public void SetBenefits(double benefits, double benefitsPerUpgrade)
        {
            float multiplier = 1f;

            if (index > 0)
            {
                multiplier = (DataManager.Profile.boughtBusinesses * 10);
            }

            this.benefits = benefits * multiplier;
            this.benefitsPerUpgrade = benefitsPerUpgrade * multiplier;
        }

        public void SetUpgradeCosts(double upgradeCost, float upgradePercentageCost, float upgradeMilestoneCost)
        {
            float multiplier = 1f;
            float percentage = 1f;

            if (index > 0)
            {
                multiplier = (DataManager.Profile.boughtBusinesses * 10);
                percentage = 1.3f;
            }

            this.upgradeCost = upgradeCost * multiplier;
            this.upgradePercentageCost = upgradePercentageCost * percentage;
        }

        public void SetBenefitTimer(float benefitTimer)
        {
            this.benefitTimer = benefitTimer;
            this.timer = benefitTimer;
        }

        public void Load(BusinessData data)
        {
            level = data.level;
            milestones = data.milestones;
            bought = data.bought;
            canBuyout = data.canBuyout;
            benefitTimer = data.benefitTimer;
            benefits = data.benefits;
            benefitsPerUpgrade = data.benefitsPerUpgrade;
            upgradeCost = data.upgradeCost;
            upgradePercentageCost = data.upgradePercentageCost;
        }
    }
}