// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class InventorySystem : MonoBehaviour
// {
//     public static InventorySystem Instance;

//     [SerializeField] private List<Image> slots = new List<Image>();

//     private int slotIndex = 0;

//     private void Awake()
//     {
//         Instance = this;
//     }

//     public void AddItem(Sprite itemIcon)
//     {
//         if (slotIndex >= slots.Count)
//         {
//             Debug.Log("Inventário cheio");
//             return;
//         }

//         slots[slotIndex].sprite = itemIcon;
//         slots[slotIndex].color = Color.white;

//         slotIndex++;
//     }
// }


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [SerializeField] private List<Image> slots = new List<Image>();

    private List<Sprite> itens = new List<Sprite>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // =========================
    // ADICIONAR ITEM
    // =========================
    public void AddItem(Sprite itemIcon)
    {
        if (itens.Count >= slots.Count)
        {
            Debug.Log("Inventário cheio");
            return;
        }

        itens.Add(itemIcon);
        UpdateUI();
    }

    // =========================
    // VERIFICAR ITEM
    // =========================
    public bool HasItem(Sprite item)
    {
        return itens.Contains(item);
    }

    // =========================
    // REMOVER ITEM
    // =========================
    public void RemoveItem(Sprite item)
    {
        if (itens.Contains(item))
        {
            itens.Remove(item);
            UpdateUI();
        }
    }

    // =========================
    // ATUALIZAR UI
    // =========================
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < itens.Count)
            {
                slots[i].sprite = itens[i];
                slots[i].color = Color.white;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}