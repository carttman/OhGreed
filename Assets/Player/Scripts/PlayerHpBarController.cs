using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarController : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    public void SetMaxHealth(int max)
    {
        healthSlider.maxValue = max;
        UpdateText(max, max);
       
    }

    public void SetCurrentHealth(int current)
    {
        healthSlider.value = current;
        UpdateText(current, (int)healthSlider.maxValue);
    }

    private void UpdateText(int current, int max)
    {
        healthText.text = $"{current} / {max}";
    }
}
