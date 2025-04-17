using UnityEngine;
using UnityEngine.UI;

public class ItemUIBase : MonoBehaviour
{
    public ItemData itemData;

    void awake()
    {
    }
    
    void Start()
    {
        Image image = GetComponent<Image>();
        image.sprite = itemData.ItemIcon;    
    }

    void Update()
    {
        
    }
}
