using System.Collections;
using System.Collections.Generic;
using BusinessSystem;
using DataSystem;
using ManagersSystem;

namespace SavesSystem
{
    public static class SavesManager
    {
        public static string KEY_SAVES = "MY_BUSINESSES";

        public static void SaveBusinesses()
        {
            int quantity = GameManager.Instance.Businesses.Length;
            BusinessData[] businesses = new BusinessData[quantity];
            for (int i = 0; i < quantity; i++)
            {
                businesses[i] = new BusinessData();
                businesses[i].milestones = GameManager.Instance.Businesses[i].milestones;
                businesses[i].bought = GameManager.Instance.Businesses[i].bought;
                businesses[i].canBuyout = GameManager.Instance.Businesses[i].canBuyout;
                businesses[i].benefitTimer = GameManager.Instance.Businesses[i].benefitTimer;
                businesses[i].benefits = GameManager.Instance.Businesses[i].benefits;
                businesses[i].benefitsPerUpgrade = GameManager.Instance.Businesses[i].benefitsPerUpgrade;


                businesses[i].level = GameManager.Instance.Businesses[i].level;
                businesses[i].upgradeCost = GameManager.Instance.Businesses[i].upgradeCost;
                businesses[i].upgradePercentageCost = GameManager.Instance.Businesses[i].upgradePercentageCost;
            }

            DataManager.Profile.business = businesses;

        }
    }
}