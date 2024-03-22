using System;
using AdvisorSystem;
using DataSystem;
using TMPro;
using UnityEngine;

namespace ManagersSystem
{
    public class OfflineEarningsManager : Singleton<OfflineEarningsManager>
    {
        [SerializeField] private GameObject offlineEarningsPanel;
        [SerializeField] private TextMeshProUGUI btnMessage;
        [SerializeField] private TextMeshProUGUI earningsTMP;
        [SerializeField] private TextMeshProUGUI timeTMP;

        public long seconds { get; set; }
        public string absentTime { get; set; }

        private double earnings;

        public double earningsAmount;

        public void ClaimEarnings()
        {
            offlineEarningsPanel.SetActive(false);
            EarningsManager.Instance.AddEarnings(earnings);
            seconds = 0;
            earnings = 0;
            earningsAmount = 0;
        }

        private void ShowEarnings()
        {
            if (seconds > 0 && earnings > 0d)
            {
                if (offlineEarningsPanel != null)
                    offlineEarningsPanel.SetActive(true);
                earningsTMP.text = $"+ ${earnings.CurrencyText()}";
                timeTMP.text = absentTime;
            }
        }

        private void CalculateEarnings()
        {
            if (seconds > 0)
            {
                if (seconds >= 43200)
                {
                    seconds = 43200; // 12 hours
                }

                float multiplier = GetMultiplier();

                if (EarningsManager.Instance.totalEarnings == 0)
                {
                    earnings = 10;
                }
                else
                {
                    earnings = multiplier * EarningsManager.Instance.totalEarnings;
                }

                if (earnings <= 10)
                    earnings = 10;

                float offlineUtility = AdvisorManager.Instance.GetAdvisorBenefit(AdvisorType.Offline);
                if (offlineUtility != 0f)
                {
                    earnings += earnings * offlineUtility;
                }

                earningsAmount = earnings;
            }
        }

        private float GetMultiplier() => seconds / 43200f;

        private void CalculateOfflineTime()
        {
            string time = DataManager.Profile.offlineTime;

            if (!string.IsNullOrEmpty(time))
            {
                DateTime savedTime = DateTime.FromBinary(Convert.ToInt64(time));
                TimeSpan difference = DateTime.Now.Subtract(savedTime);
                seconds = Mathf.FloorToInt((float)difference.TotalSeconds);
                absentTime = $"{difference.Hours:00}:{difference.Minutes:00}:{difference.Seconds:00}";
            }
        }

        public void SaveGameTime()
        {
            string offlineTime = DateTime.Now.ToBinary().ToString();

            DataManager.Profile.lastTimePlayed = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            DataManager.Profile.offlineTime = offlineTime;
            DataManager.Instance.SaveUserProfile();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus) return;
            SaveGameTime();
        }

        private void AdvisorsLoadedResponse(AdvisorManager manager)
        {
            CalculateOfflineTime();
            CalculateEarnings();
            ShowEarnings();
        }

        private void OnEnable()
        {
            AdvisorManager.AdvisorsLoadedEvent += AdvisorsLoadedResponse;
        }

        private void OnDisable()
        {
            AdvisorManager.AdvisorsLoadedEvent -= AdvisorsLoadedResponse;
        }
    }
}