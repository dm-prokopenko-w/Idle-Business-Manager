using System;

namespace QuestsSystem
{
    [Serializable()]
    public class QuestData
    {
        public string Name;
        public string ID;
        public int Goals;
        public string Type;
        public string Description;
        public double Reward;
        public bool QuestClaimed;
        public int CurrentProgress;
        public bool questCompleted;
        public int index;
    }
}