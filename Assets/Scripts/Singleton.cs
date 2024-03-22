using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance != null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;

        if (!gameObject.name.Equals("LoadingManager") || !gameObject.name.Equals("TrafficManager") || !gameObject.name.Equals("SettingsManager") || !gameObject.name.Equals("GoogleMobileAdsInit"))
            DontDestroyOnLoad(gameObject);
    }
}
