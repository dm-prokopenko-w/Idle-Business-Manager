using DataSystem;
using UnityEngine;
using UnityEngine.Events;

namespace QuestsSystem
{
    public enum QuestType
    {
        Click,
        Upgrade
    }

    [CreateAssetMenu(fileName = "Quest")]
    public class Quest : ScriptableObject
    {
        public static event UnityAction<Quest> QuestCompletedEvent;

        [Header("Info")]
        public string Name;
        public string ID;
        public int Goals;
        public QuestType Type;
        public int index;

        [Header("Description")]
        [TextArea] public string Description;

        [Header("Reward")]
        public double Reward;

        public bool QuestClaimed = false;

        public int CurrentProgress;

        public bool questCompleted = false;

        public string KeyCompleted => QUEST_COMPLETED + ID;
        public string KeyClaimed => QUEST + ID;
        public string KeyProgress => PROGRESS + ID;

        private string QUEST_COMPLETED = "QUEST_COMPLETED";
        private string QUEST = "QUEST";
        private string PROGRESS = "PROGRESS";

        public void UpdateQuest()
        {
            CurrentProgress++;

            DataManager.Profile.quests[index].CurrentProgress = CurrentProgress;

            IsQuestCompleted();
        }

        private void IsQuestCompleted()
        {
            if (DataManager.Profile.quests[index].CurrentProgress >= DataManager.Profile.quests[index].Goals)
            {
                DataManager.Profile.quests[index].questCompleted = true;
                DataManager.Profile.quests[index].CurrentProgress = Goals;
                QuestCompletedEvent?.Invoke(this);
            }
        }
    }
}