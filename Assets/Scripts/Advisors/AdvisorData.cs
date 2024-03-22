using System;
using UnityEngine;

namespace AdvisorSystem
{
    [Serializable()]
    public class AdvisorData
    {
        public string Name;
        public Sprite Icon;
        public string Type;
        public string Quality;
        public float Multiplier;
        public string Description;
        public bool Bought;
        public bool Selected;
        public int Index;
    }
}