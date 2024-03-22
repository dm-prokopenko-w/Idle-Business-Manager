using BusinessSystem;
using DataSystem;
using TMPro;
using UnityEngine;

namespace ManagersSystem
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Businesses")]
        [SerializeField] private bool deleteData;

        [Header("Businesses")]
        [SerializeField] private BusinessController[] businesses;
        [SerializeField] private TextMeshProUGUI businessCounterTMP;

        [Header("Costs")]
        [SerializeField] private float costFirstBusiness = 10;
        [SerializeField] private float costBusinessMulti = 50;

        [Header("Initial Benefits")]
        [SerializeField] private float startedBenefit = 2;
        [SerializeField] private float benefitPerUpgrade = 2;
        [SerializeField] private float incrementBenefit = 400;
        [SerializeField] private float newMultiplier = 0f;

        [Header("Initial Costs")]
        [SerializeField] private float initialUpgradeCost = 2;
        [SerializeField] [Range(0, 100)] private int upgradePercentageCost = 8;
        [SerializeField] private float costMilestoneMulti = 15;

        [Header("Initial Timers")]
        [SerializeField] private float benefitTimer = 2f;
        [SerializeField] private float benefitTimerMulti = 2f;

        public int boughtBusinesses { get; private set; }
        public double costNewBusiness { get; private set; }
        public BusinessController[] Businesses => businesses;

        public float milestoneMulti => costMilestoneMulti;

        private double myBenefits;
        private double myBenefitsPerUpgrade;
        private double myUpgradeCost;
        private float myUpgradePercentageCost;
        private float myUpgradeMilestoneMulti;
        private float myBenefitTimer;

        protected override void Awake()
        {
            base.Awake();

            costNewBusiness = costFirstBusiness;
            myBenefits = startedBenefit;
            myBenefitsPerUpgrade = benefitPerUpgrade;
            myUpgradeCost = initialUpgradeCost;
            myUpgradePercentageCost = upgradePercentageCost;
            myUpgradeMilestoneMulti = costMilestoneMulti;
            myBenefitTimer = benefitTimer;

            if (DataManager.Profile.costNewBusiness == 0)
            {
                SyncGameManager();
            }

            businessCounterTMP.text = $"{DataManager.Profile.boughtBusinesses}";

            LoadBusinesses();
            LoadGameData();
        }

        public void setNewBusiness(BusinessController controller)
        {
            NewBoughtBusiness();

            controller.bought = true;
            controller.SetBenefits(myBenefits, myBenefitsPerUpgrade);
            controller.SetUpgradeCosts(myUpgradeCost, myUpgradePercentageCost, myUpgradeMilestoneMulti);
            controller.SetBenefitTimer(myBenefitTimer);
            NextBusinessCanBuyout(controller);
            UpdateValuesNewBusiness(controller);
        }

        private void NextBusinessCanBuyout(BusinessController controller)
        {
            if (controller.Index + 1 < businesses.Length)
            {
                businesses[controller.Index + 1].canBuyout = true;
            }
        }

        private void NewBoughtBusiness()
        {
            boughtBusinesses++;
            costNewBusiness *= costBusinessMulti;

            DataManager.Profile.costNewBusiness = costNewBusiness;
            DataManager.Profile.boughtBusinesses = boughtBusinesses;
            businessCounterTMP.text = $"{DataManager.Profile.boughtBusinesses}";
        }

        private void UpdateValuesNewBusiness(BusinessController controller)
        {
            myBenefits *= (newMultiplier * 5f); // increase business earnings and cost
            myBenefitsPerUpgrade = myBenefits;
            myUpgradeCost = myBenefits * 3;
            myBenefitTimer += benefitTimerMulti;

            DataManager.Profile.myBenefits = myBenefits;
            DataManager.Profile.myBenefitsPerUpgrade = myBenefitsPerUpgrade;
            DataManager.Profile.myUpgradeCost = myUpgradeCost;
            DataManager.Profile.myUpgradePercentageCost = myUpgradePercentageCost;
            DataManager.Profile.myBenefitTimer = myBenefitTimer;
        }

        private void LoadGameData()
        {
            costNewBusiness = DataManager.Profile.costNewBusiness;
            myBenefits = DataManager.Profile.myBenefits;
            myBenefitsPerUpgrade = DataManager.Profile.myBenefitsPerUpgrade;
            myUpgradeCost = DataManager.Profile.myUpgradeCost;
            myUpgradePercentageCost = DataManager.Profile.myUpgradePercentageCost;
            myBenefitTimer = DataManager.Profile.myBenefitTimer;
            boughtBusinesses = DataManager.Profile.boughtBusinesses;
        }

        public void SyncGameManager()
        {
            DataManager.Profile.costNewBusiness = costNewBusiness;
            DataManager.Profile.myBenefits = myBenefits;
            DataManager.Profile.myBenefitsPerUpgrade = myBenefitsPerUpgrade;
            DataManager.Profile.myUpgradeCost = myUpgradeCost;
            DataManager.Profile.myUpgradePercentageCost = myUpgradePercentageCost;
            DataManager.Profile.myBenefitTimer = myBenefitTimer;
        }

        private void LoadBusinesses()
        {

            BusinessData[] myBusinesses = DataManager.Profile.business;
            for (int i = 0; i < myBusinesses.Length; i++)
            {
                if (myBusinesses[i] != null)
                {
                    businesses[i].Load(myBusinesses[i]);
                }
            }

        }
    }
}