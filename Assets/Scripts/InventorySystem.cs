// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class InventorySystem : MonoBehaviour
// {
//     public static InventorySystem Instance;

//     [SerializeField] private List<Image> slots = new List<Image>();

//     private List<Sprite> itens = new List<Sprite>();

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     // =========================
//     // ADICIONAR ITEM
//     // =========================
//     public void AddItem(Sprite itemIcon)
//     {
//         if (itens.Count >= slots.Count)
//         {
//             Debug.Log("Inventário cheio");
//             return;
//         }

//         itens.Add(itemIcon);
//         UpdateUI();
//     }

//     // =========================
//     // VERIFICAR ITEM
//     // =========================
//     public bool HasItem(Sprite item)
//     {
//         return itens.Contains(item);
//     }

//     // =========================
//     // REMOVER ITEM
//     // =========================
//     public void RemoveItem(Sprite item)
//     {
//         if (itens.Contains(item))
//         {
//             itens.Remove(item);
//             UpdateUI();
//         }
//     }

//     // =========================
//     // ATUALIZAR UI
//     // =========================
//     private void UpdateUI()
//     {
//         for (int i = 0; i < slots.Count; i++)
//         {
//             if (i < itens.Count)
//             {
//                 slots[i].sprite = itens[i];
//                 slots[i].color = Color.white;
//             }
//             else
//             {
//                 slots[i].sprite = null;
//                 slots[i].color = new Color(1, 1, 1, 0);
//             }
//         }
//     }
// }




using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [SerializeField] private List<Image> slots = new List<Image>();

    private List<ItemData> itens = new List<ItemData>();

    private int selectedIndex = -1;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData item)
    {
        if (itens.Count >= slots.Count)
        {
            Debug.Log("Inventário cheio");
            return;
        }

        itens.Add(item);

        int index = itens.Count - 1;

        slots[index].sprite = item.icon;
        slots[index].color = Color.white;

        Button btn = slots[index].GetComponent<Button>();

        if (btn != null)
        {
            int slotIndex = index;

            btn.onClick.RemoveAllListeners();
           //btn.onClick.AddListener(() => UseItem(slotIndex));
            btn.onClick.AddListener(() => SelectItem(slotIndex));
        }
    }

    void UseItem(int index)
    {
        ItemData item = itens[index];

        if (item.isDiaryPage)
        {
            DiaryUI.Instance.ShowPage(item.description);
        }
    }

    // 🔑 NOVO HAS ITEM
    public bool HasItem(ItemData item)
    {
        return itens.Contains(item);
    }

    // 🔑 NOVO REMOVE ITEM
    public void RemoveItem(ItemData item)
    {
        if (!itens.Contains(item)) return;

        itens.Remove(item);

        // atualiza UI
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < itens.Count)
            {
                slots[i].sprite = itens[i].icon;
                slots[i].color = Color.white;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].color = new Color(1,1,1,0);
            }
        }
    }


    public void SelectItem(int index)
    {
        selectedIndex = index;

        Debug.Log("Selecionado: " + itens[index].description);

        InventoryUI.Instance.ShowItemOptions(itens[index]);
    }


}