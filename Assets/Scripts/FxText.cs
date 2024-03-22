using UnityEngine;
using TMPro;

public class FxText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private Transform poolerContainer;

    public void ShowEarning(double earning, Transform container)
    {
        text.text = $"+ ${earning.CurrencyText()}";
        poolerContainer = container;
    }

    public void AnimationEnded()
    {
        gameObject.SetActive(false);
        transform.SetParent(poolerContainer);
    }
}
