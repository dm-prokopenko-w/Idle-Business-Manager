using DataSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AdvisorSystem
{
    public class AdvisorSlot : MonoBehaviour
    {
        public static event UnityAction<AdvisorSlot> SlotSelectedEvent;

        [SerializeField] private Image icon;
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private int index;

        public Advisor Advisor { get; set; }

        public string KeyAdvisor;
        public int indexValue => index;

        public Image iconRef => icon;

        public string KeySavedAdvisor => Constants.KeyAdvisor + index;

        public string SavedAdvisor
        {
            get => GetAdvisorSlot();
            set => SaveAdvisorSlot(value);
        }

        public string GetAdvisorSlot() => DataManager.Profile.advisorsSlot[index].KeyAdvisor;

        public void SaveAdvisorSlot(string value)
        {
            KeyAdvisor = value;

            DataManager.Profile.advisorsSlot[index].KeyAdvisor = value;
        }

        public void AdvisorReference(AdvisorCard card)
        {
            Advisor = card.Advisor;
            SavedAdvisor = $"{Advisor.Quality} {Advisor.Name}";
            KeyAdvisor = SavedAdvisor;
        }

        public void ShowAdvisor() => icon.sprite = Advisor.Icon;

        public void ClickSlot()
        {
            SlotSelectedEvent?.Invoke(this);
            if (Advisor == null) return;
            ShowAdvisor();
        }

        public void ClearAdvisor()
        {
            Advisor = null;
            icon.sprite = defaultIcon;

            DataManager.Profile.advisorsSlot[index].KeyAdvisor = "";
        }
    }
}