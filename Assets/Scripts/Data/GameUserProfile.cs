using System;
using AdvisorSystem;
using BusinessSystem;
using QuestsSystem;

namespace DataSystem
{
    [Serializable()]
    public class GameUserProfile
    {
        public string username;
        public string uid;
        public BusinessData[] business = { };
        public AdvisorData[] advisors = { };
        public AdvisorSlotData[] advisorsSlot = { };
        public QuestData[] quests = { };
        public double startedBusinessEarnings;
        public double totalEarnings;
        public double maxEarningsReached;
        public double sessionMaxEarningsReached;
        public double costNewBusiness;
        public double randomAdvisorCost;
        public double myBenefits;
        public double myBenefitsPerUpgrade;
        public float myUpgradePercentageCost;
        public double myUpgradeCost;
        public float myBenefitTimer;
        public string offlineTime;
        public int boughtBusinesses;
        public bool firstPurchaseCompleted;
        public bool removeAds;
        public bool multiplier;
        public string deviceId;
        public bool hasData;
        public bool hasDataAdvisors;
        public bool update;
        public string profileAdvisor;
        public string createdDate;
        public string lastTimePlayed;
    }
}