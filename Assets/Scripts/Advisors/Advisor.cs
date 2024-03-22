using UnityEngine;

namespace AdvisorSystem
{
    public enum AdvisorType
    {
        Earnings,
        Click,
        Offline,
        UpgradeDiscount,
        UpgradeMilestone
    }

    public enum AdvisorQuality
    {
        Common,
        Normal,
        Rare,
        Epic,
        Legendary
    }

    [CreateAssetMenu(fileName = "Advisor")]
    public class Advisor : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public AdvisorType Type;
        public AdvisorQuality Quality;
        public float Multiplier;
        [TextArea] public string Description;
        public int Index;

        public bool Bought;

        public bool Selected;

        public string KeyBought => BOUGHT + Quality + Name;
        public string KeySelected => SELECTED + Quality + Name;

        private string BOUGHT = "ADVISOR_BOUGHT";
        private string SELECTED = "ADVISOR_SELECTED";
        public bool addedToProfile;
    }
}