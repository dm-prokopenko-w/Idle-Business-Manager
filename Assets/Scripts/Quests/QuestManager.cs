using System.Collections.Generic;
using DataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace QuestsSystem
{
    public class QuestManager : Singleton<QuestManager>
    {
        [Header("Quests")]
        [SerializeField] private Quest[] quests;

        [Header("Config")]
        [SerializeField] private GameObject questPanel;
        [SerializeField] private Transform content;
        [SerializeField] private QuestCard questCardPrefab;
        [SerializeField] private ScrollRect scrollRect;

        private List<QuestCard> myQuests = new List<QuestCard>();

        public static bool questsOpened;
        public Vector3 scrollRectStart;

        private void Start()
        {
            questsOpened = false;

            if (DataManager.Profile.quests.Length < quests.Length)
            {
                SyncQuestManager();
            }

            if (DataManager.Profile.quests.Length == 0)
            {
                SyncQuestManager();
            }
            else
            {
                for (int i = 0; i < DataManager.Profile.quests.Length; i++)
                {
                    QuestType questType = GetQuestType(DataManager.Profile.quests[i].Type);

                    quests[i].Name = DataManager.Profile.quests[i].Name;
                    quests[i].ID = DataManager.Profile.quests[i].ID;
                    quests[i].Goals = DataManager.Profile.quests[i].Goals;
                    quests[i].Type = questType;
                    quests[i].Description = DataManager.Profile.quests[i].Description;
                    quests[i].Reward = DataManager.Profile.quests[i].Reward;
                    quests[i].QuestClaimed = DataManager.Profile.quests[i].QuestClaimed;
                    quests[i].questCompleted = DataManager.Profile.quests[i].questCompleted;
                    quests[i].index = DataManager.Profile.quests[i].index;
                    quests[i].CurrentProgress = DataManager.Profile.quests[i].CurrentProgress;
                }
            }

            LoadQuests();
            scrollRectStart = scrollRect.content.position;
        }

        public void SyncQuestManager()
        {
            DataManager.Profile.quests = new QuestData[quests.Length];
            for (int i = 0; i < quests.Length; i++)
            {
                if (!DataManager.Profile.hasData)
                {
                    quests[i].QuestClaimed = false;
                    quests[i].questCompleted = false;
                    quests[i].CurrentProgress = 0;
                }

                DataManager.Profile.quests[i] = new QuestData();
                DataManager.Profile.quests[i].Name = quests[i].Name;
                DataManager.Profile.quests[i].ID = quests[i].ID;
                DataManager.Profile.quests[i].Goals = quests[i].Goals;
                DataManager.Profile.quests[i].Type = quests[i].Type.ToString();
                DataManager.Profile.quests[i].Description = quests[i].Description;
                DataManager.Profile.quests[i].Reward = quests[i].Reward;
                DataManager.Profile.quests[i].QuestClaimed = quests[i].QuestClaimed;
                DataManager.Profile.quests[i].questCompleted = quests[i].questCompleted;
                DataManager.Profile.quests[i].index = quests[i].index;
                DataManager.Profile.quests[i].CurrentProgress = quests[i].CurrentProgress;
            }
            if (!DataManager.Profile.hasData)
                DataManager.Profile.hasData = true;
        }

        private void LoadQuests()
        {
            for (int i = 0; i < quests.Length; i++)
            {

                if (quests[i].QuestClaimed)
                {
                    continue;
                }

                QuestCard newQuest = Instantiate(questCardPrefab, content);
                newQuest.SetCard(quests[i]);
                myQuests.Add(newQuest);
            }
        }

        public void AddProgress(string questId)
        {
            Quest quest = GetQuest(questId);
            if (quest != null)
            {

                if (quest.QuestClaimed)
                    return;

                quest.UpdateQuest();
            }
        }

        private Quest GetQuest(string questId)
        {
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i].ID == questId)
                    return quests[i];
            }

            return null;
        }

        public void OpenQuests()
        {
            questPanel.transform.localPosition = Vector3.zero;
            questsOpened = true;
        }

        public void CloseQuests()
        {
            questPanel.transform.localPosition = Vector3.left * 1500;
            questsOpened = false;
            scrollRect.content.position = scrollRectStart;
        }

        public static QuestType GetQuestType(string type)
        {
            if (QuestType.Click.ToString() == type)
            {
                return QuestType.Click;
            }
            else
            {
                return QuestType.Upgrade;
            }
        }

        public void ResetQuests()
        {
            foreach (Quest quest in quests)
            {
                quest.QuestClaimed = false;
                quest.CurrentProgress = 0;
                quest.questCompleted = false;
            }
        }
    }
}