using System.Collections;
using DataSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AdvisorSystem
{
    public class AdvisorCard : MonoBehaviour
    {
        public static event UnityAction<AdvisorCard> CardSelectedEvent;
        public static event UnityAction<AdvisorCard> CardSelectedProfileEvent;

        [Header("UI")]
        [SerializeField] private Image icon;
        [SerializeField] private Image picker;
        [SerializeField] private Button button;

        private bool isProfileCard = false;

        public Advisor Advisor { get; private set; }
        
        public void SetCard(Advisor advisor)
        {
            Advisor = advisor;
            icon.sprite = advisor.Icon;
            isProfileCard = false;

            button.interactable = DataManager.Profile.advisors[advisor.Index].Bought;
        }

        public void SetCardProfile(Advisor advisor)
        {
            Advisor = advisor;
            icon.sprite = advisor.Icon;
            isProfileCard = true;
        }

        public void Click()
        {
            if (isProfileCard)
                CardSelectedProfileEvent?.Invoke(this);
            else
                CardSelectedEvent?.Invoke(this);
        }

        public void UnlockCard()
        {
            button.interactable = true;
            Advisor.Bought = true;

            DataManager.Profile.advisors[Advisor.Index].Bought = true;
            DataManager.Instance.SaveUserProfile();
        }

        public void ShowPicker()
        {
            StartCoroutine(IEPicker());
        }

        private IEnumerator IEPicker()
        {
            picker.enabled = true;
            yield return new WaitForSeconds(0.2f);
            picker.enabled = false;
        }
    }
}