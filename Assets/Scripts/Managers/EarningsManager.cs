using DataSystem;

namespace ManagersSystem
{
    public class EarningsManager : Singleton<EarningsManager>
    {
        public double totalEarnings { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            totalEarnings = DataManager.Profile.totalEarnings;
        }

        public void AddEarnings(double amount)
        {
            totalEarnings += amount;

            if (DataManager.Profile.sessionMaxEarningsReached <= totalEarnings || DataManager.Profile.sessionMaxEarningsReached == 0)
                DataManager.Profile.sessionMaxEarningsReached = totalEarnings;

            if (DataManager.Profile.maxEarningsReached <= totalEarnings || DataManager.Profile.maxEarningsReached == 0)
                DataManager.Profile.maxEarningsReached = totalEarnings;

            DataManager.Profile.totalEarnings = totalEarnings;
        }

        public void RemoveEarnings(double amount)
        {
            if (totalEarnings >= amount)
            {
                totalEarnings -= amount;

                DataManager.Profile.totalEarnings = totalEarnings;
            }
        }
    }
}
