using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject objToCreate;
    [SerializeField] private int quantityPerObj;

    public Transform container => poolerContainer.transform;

    private List<GameObject> createdInstances = new List<GameObject>();
    private GameObject poolerContainer;

    private void Awake()
    {
        poolerContainer = new GameObject($"Pooler - {objToCreate.name}");
        CreatePooler();
    }

    private void CreatePooler()
    {
        for (int i = 0; i < quantityPerObj; i++)
        {
            GameObject obj = Instantiate(objToCreate, poolerContainer.transform);
            obj.SetActive(false);
            createdInstances.Add(obj);
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
