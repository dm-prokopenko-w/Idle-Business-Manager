using System;

namespace BusinessSystem
{
    [Serializable()]
    public class BusinessData
    {
        public double milestones;
        public bool bought;
        public bool canBuyout;
        public float benefitTimer;
        public double benefits;
        public double benefitsPerUpgrade;

        public float level;
        public double upgradeCost;
        public double upgradePercentageCost;
    }
}