using System.Collections;
using UnityEngine;
using System;
using BayatGames.SaveGameFree;

namespace DataSystem
{
    public class DataManager : Singleton<DataManager>
    {
        [Header("Game Objects Quantity")]
        [SerializeField] private int businessQuantity;
        [SerializeField] private int questQuantity;
        [SerializeField] private int advisorsQuantity;
        [SerializeField] private int advisorsSlotQuantity;

        public static GameUserProfile Profile = new ();
        public static GameUserProfile UserProfile = new ();
        
        private string PROFILE_KEY = "PROFILE";

        private void Start()
        {
            InvokeRepeating("SaveGameRoutine", 10f, 15f);
        }

        private void SaveGameRoutine()
        {
            Debug.Log("Saving Game");
            SaveUserProfile();
        }

        public void SaveUserProfile()
        {
            if (Profile.createdDate == null || Profile.createdDate == "")
                Profile.createdDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            SaveGame.Save(PROFILE_KEY, Profile);
        }

        public IEnumerator GetProfileData(string id)
        {
            if (!SaveGame.Exists(PROFILE_KEY))
            {
                UserProfile = null;
            }
            else
            {
                if (SaveGame.Load<GameUserProfile>(PROFILE_KEY) == null)
                    UserProfile = new GameUserProfile();
                else
                    UserProfile = SaveGame.Load<GameUserProfile>(PROFILE_KEY);
            }

            yield return null;
        }
    }
}