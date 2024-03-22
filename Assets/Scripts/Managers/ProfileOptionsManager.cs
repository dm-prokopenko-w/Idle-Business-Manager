using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ManagersSystem
{
    public class ProfileOptionsManager : MonoBehaviour
    {
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;

        public static string KEY_SOUND_PREF = "SOUND_PREF";

        private void Start()
        {
            SetSoundOption();
        }

        public void ToggleSound()
        {

            if (PlayerPrefs.GetInt(KEY_SOUND_PREF, 1) == 1)
            {
                PlayerPrefs.SetInt(KEY_SOUND_PREF, 0);
            }
            else
            {
                PlayerPrefs.SetInt(KEY_SOUND_PREF, 1);
            }

            PlayerPrefs.Save();

            SetSoundOption();
        }

        private void SetSoundOption()
        {
            if (PlayerPrefs.GetInt(KEY_SOUND_PREF, 1) == 1)
            {
                soundOff.SetActive(true);
                soundOn.SetActive(false);
                PlayAudio();
            }
            else
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
                StopAudio();
            }
        }

        public void PlayAudio()
        {
            Debug.Log("Play Audio");
        }

        public void StopAudio()
        {
            Debug.Log("Stop Audio");

        }
    }
}