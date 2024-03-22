using UnityEngine;

namespace BusinessSystem
{
    [CreateAssetMenu(fileName = "Business")]
    public class Business : ScriptableObject
    {
        [Header("Sprites")]
        public Sprite firstFloor;
        public Sprite secondFloor;
        public Sprite roofFloor;

        [Header("Info")]
        public string id;
        public string name;
        public Sprite icon;
    }
}