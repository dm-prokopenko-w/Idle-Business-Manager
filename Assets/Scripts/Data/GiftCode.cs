using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSystem
{
    public enum CodeType
    {
        firstPurchaseFree,
        removeAdsFree,
        doubleProfitsFree,
        hireAdvisorFree,
        freeCash
    }

    [Serializable()]
    public class GiftCode
    {
        public string id;
        public string code;
        public string codeType;
        public bool isEnabled;
        public int availableClaims;
        public double amount;
        public List<string> userIds;
        public int claimedTimes;
    }
}