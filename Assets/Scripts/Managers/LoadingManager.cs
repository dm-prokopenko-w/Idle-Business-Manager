using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using BayatGames.SaveGameFree;
using System.Text;
using System.Linq;
using DataSystem;

namespace ManagersSystem
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        internal static LoadingManager wkr;
        Queue<Action> jobs = new Queue<Action>();

        [SerializeField] private bool resetGame;
        [SerializeField] private TextMeshProUGUI versionTMP;
        [SerializeField] private GameObject noInternetPanel;
        [SerializeField] private float loadingDelay;

        [Header("Businesses")]
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private TextMeshProUGUI idTMP;
        [SerializeField] private GameObject playGameSection;
        [SerializeField] private Slider slider;
        [SerializeField] private float countDown;

        [Header("Objects Quantity")]
        [SerializeField] private int businessQuantity;
        [SerializeField] private int questQuantity;
        [SerializeField] private int advisorsQuantity;
        [SerializeField] private int advisorsSlotQuantity;

        private float startTime = 0f;
        private bool done;

        private string GUEST_KEY = "isGuest";

        protected override void Awake()
        {
            base.Awake();

            wkr = this;

            versionTMP.text = $"v {Application.version}";

            slider.value = 0f;

            if (resetGame)
                SaveGame.DeleteAll();
        }

        private void Start()
        {
            startTime = Time.time;
            UpdateLoading(0.12f, "Loading...", false);
        }

        private void Update()
        {
            while (jobs.Count > 0)
                jobs.Dequeue().Invoke();

            if (Time.time >= startTime + loadingDelay && !done)
            {
                done = true;

                LoadingManager.Instance.UpdateLoading(0.53f, Constants.RetrievingGuest, false);

                StartCoroutine(HandleGuestSession());
            }
        }

        private IEnumerator HandleGuestSession()
        {
            yield return StartCoroutine(DataManager.Instance.GetProfileData(SystemInfo.deviceUniqueIdentifier));

            string guestId = GenerateID();

            if (DataManager.UserProfile == null)
            {
                PlayerPrefs.SetInt(GUEST_KEY, 1);
                DataManager.Profile = new GameUserProfile();
                DataManager.Profile.uid = SystemInfo.deviceUniqueIdentifier;
                DataManager.Profile.deviceId = SystemInfo.deviceUniqueIdentifier;
                DataManager.Profile.createdDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                DataManager.Profile.username = $"Guest-{guestId}";
                DataManager.Profile.maxEarningsReached = 0;

                DataManager.Instance.SaveUserProfile();
            }
            else
            {
                DataManager.Profile = DataManager.UserProfile;
                guestId = DataManager.Profile.username.Split("-")[1];
            }

            yield return new WaitForSeconds(loadingDelay);

            UpdateLoading(1f, Constants.GuestSession + guestId, true);
        }

        public string GenerateID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString(); ;
        }

        public void QuitGame()
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }

        internal void AddJob(Action newJob)
        {
            jobs.Enqueue(newJob);
        }

        public void UpdateLoading(float progress, string message, bool complete)
        {
            slider.value = progress;
            idTMP.text = message;

            if (complete)
            {
                CompleteLoading();
            }
        }

        public void CompleteLoading()
        {
            loadingPanel.SetActive(false);
            playGameSection.SetActive(true);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}