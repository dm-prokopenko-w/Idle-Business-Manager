using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSystem
{
    [Serializable()]
    public class Purchase
    {
        public string id;
        public string deviceId;
        public string deviceModel;
        public string userId;
        public string date;
        public string dateTime;
        public string productId;
        public float amount;
    }
}