using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficSystem
{
    public class TrafficManager : Singleton<TrafficManager>
    {
        [SerializeField] private GameObject objToCreate;
        [SerializeField] private int quantityPerObj;
        [SerializeField] private GameObject[] firstPath;
        [SerializeField] private GameObject[] secondPath;

        public Transform container => carsContainer.transform;

        public GameObject[] fPath => firstPath;
        public GameObject[] sPath => secondPath;

        private List<GameObject> createdInstances = new List<GameObject>();
        private GameObject carsContainer;

        protected override void Awake()
        {
            base.Awake();

            carsContainer = new GameObject($"Car - {objToCreate.name}");
            StartCoroutine(IECreatePooler());
        }

        private void CreatePooler()
        {
            for (int i = 0; i < quantityPerObj; i++)
            {
                GameObject obj = Instantiate(objToCreate, carsContainer.transform);
                createdInstances.Add(obj);
            }
        }

        private IEnumerator IECreatePooler()
        {
            float spinTimer = 1f;
            float seconds = quantityPerObj;

            while (spinTimer != seconds)
            {
                if (objToCreate != null && carsContainer != null)
                {
                    GameObject obj = Instantiate(objToCreate, carsContainer.transform);
                    createdInstances.Add(obj);
                }

                yield return new WaitForSeconds(0.2f);
                spinTimer++;
            }
        }

        public GameObject GetInstanceFromPooler()
        {
            for (int i = 0; i < createdInstances.Count; i++)
            {
                if (!createdInstances[i].activeSelf)
                {
                    return createdInstances[i];
                }
            }

            return null;
        }
    }
}