using DataSystem;
using ManagersSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestsSystem
{
    public class QuestCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI descriptionTMP;
        [SerializeField] private TextMeshProUGUI rewardTMP;
        [SerializeField] private TextMeshProUGUI progressTMP;
        [SerializeField] private Image progressBar;
        [SerializeField] private GameObject claimBtn;

        public Quest myQuest { get; set; }

        public void SetCard(Quest quest)
        {
            myQuest = quest;
            titleTMP.text = quest.Name;
            descriptionTMP.text = quest.Description;
            rewardTMP.text = "$" + quest.Reward.CurrencyText();

            if (DataManager.Profile.quests[quest.index].questCompleted)
                QuestCompleted();
        }

        private void Update()
        {
            if (myQuest.CurrentProgress >= myQuest.Goals)
            {
                progressTMP.text = $"{myQuest.Goals} / {myQuest.Goals}";
            }
            else
                progressTMP.text = $"{myQuest.CurrentProgress} / {myQuest.Goals}";
            progressBar.fillAmount = (float)myQuest.CurrentProgress / myQuest.Goals;
        }

        public void ClaimReward()
        {
            EarningsManager.Instance.AddEarnings(myQuest.Reward);
            myQuest.QuestClaimed = true;

            DataManager.Profile.quests[myQuest.index].QuestClaimed = true;

            gameObject.SetActive(false);
        }

        private void QuestCompleted()
        {
            descriptionTMP.gameObject.SetActive(false);
            claimBtn.SetActive(true);
        }

        private void QuestResponse(Quest questCompleted)
        {
            if (myQuest == questCompleted)
            {
                QuestCompleted();
            }
        }

        private void OnEnable()
        {
            Quest.QuestCompletedEvent += QuestResponse;
        }

        private void OnDisable()
        {
            Quest.QuestCompletedEvent -= QuestResponse;
        }
    }
}