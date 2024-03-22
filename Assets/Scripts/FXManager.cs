using UnityEngine;

public class FXManager : Singleton<FXManager>
{
    private Pooler pooler;

    private void Start()
    {
        pooler = GetComponent<Pooler>();
    }

    public void ShowText(Transform position, double earning)
    {
        GameObject instance = pooler.GetInstanceFromPooler();
        FxText text = instance.GetComponent<FxText>();
        text.ShowEarning(earning, pooler.container);
        instance.transform.SetParent(position);
        instance.transform.position = position.position;
        instance.SetActive(true);
    }
}
