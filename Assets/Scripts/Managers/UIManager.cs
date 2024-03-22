using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ManagersSystem
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalEarningsTMP;

        void Update()
        {
            totalEarningsTMP.text = $"${EarningsManager.Instance.totalEarnings.CurrencyText()}";
        }
    }
}