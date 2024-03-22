using UnityEngine;

namespace ManagersSystem
{
    public class ContactManager : MonoBehaviour
    {
        [SerializeField] private GameObject contactPanel;

        public void OpenContactPanel()
        {
            contactPanel.SetActive(true);
        }

        public void CloseContactPanel()
        {
            contactPanel.SetActive(false);
        }

        public void SendEmail()
        {
            string toEmail = Constants.ToEmail;
            string emailSubject = Constants.UserSupport;
            string emailBody = Constants.EmailBody;
            
            Application.OpenURL(Constants.URLReport);
        }

        public void OpenNews()
        {
            Application.OpenURL(Constants.URL);
        }
    }
}
