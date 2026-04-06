using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [SerializeField] private List<Image> slots = new List<Image>();

    private int slotIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Sprite itemIcon)
    {
        if (slotIndex >= slots.Count)
        {
            Debug.Log("Inventário cheio");
            return;
        }

        slots[slotIndex].sprite = itemIcon;
        slots[slotIndex].color = Color.white;

        slotIndex++;
    }
}