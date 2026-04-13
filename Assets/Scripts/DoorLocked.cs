// using UnityEngine;
// using UnityEngine.InputSystem;

// public class DoorLocked : MonoBehaviour
// {
//     [SerializeField] private Sprite keyIcon;

//     private bool playerNear;

//     void Update()
//     {
//         if (!playerNear) return;

//         if (Keyboard.current.fKey.wasPressedThisFrame)
//         {
//             if (InventorySystem.Instance.HasItem(keyIcon))
//             {
//                 InventorySystem.Instance.RemoveItem(keyIcon);

//                 UIMessage.Instance.Show("Porta aberta!", 2f);

//                 gameObject.SetActive(false);
//             }
//             else
//             {
//                 UIMessage.Instance.Show("Porta trancada, precisa da chave", 2f);
//             }
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerNear = true;

//             if (InventorySystem.Instance.HasItem(keyIcon))
//                 UIMessage.Instance.Show("Pressione F para abrir", 999f);
//             else
//                 UIMessage.Instance.Show("Porta trancada", 999f);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerNear = false;
//             UIMessage.Instance.Hide();
//         }
//     }
// }

// using UnityEngine;
// using UnityEngine.InputSystem;

// public class DoorLocked : MonoBehaviour
// {
//     [SerializeField] private Sprite keyIcon;
//     [SerializeField] private Animator animator;

//     private bool playerNear;
//     private bool opened = false;

//     void Update()
//     {
//         if (!playerNear || opened) return;

//         if (Keyboard.current.fKey.wasPressedThisFrame)
//         {
//             if (InventorySystem.Instance.HasItem(keyIcon))
//             {
//                 InventorySystem.Instance.RemoveItem(keyIcon);

//                 UIMessage.Instance.Show("Abrindo porta...", 1.5f);

//                 animator.SetTrigger("Open"); // 🔥 aqui

//                 opened = true;
//             }
//             else
//             {
//                 UIMessage.Instance.Show("Porta trancada", 2f);
//             }
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerNear = true;

//             if (InventorySystem.Instance.HasItem(keyIcon))
//                 UIMessage.Instance.Show("Pressione F para abrir", 999f);
//             else
//                 UIMessage.Instance.Show("Porta trancada", 999f);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerNear = false;
//             UIMessage.Instance.Hide();
//         }
//     }
// }




// using UnityEngine;
// using UnityEngine.InputSystem;

// public class DoorLocked : MonoBehaviour
// {
//     [SerializeField] private Sprite keyIcon;
//     [SerializeField] private Animator animator;
//     [SerializeField] private Collider2D doorCollider;

//     private bool playerNear;
//     private bool opened = false;

//     private Collider2D solidCollider; // 🔥 bloqueio físico

//     void Awake()
//     {
//         // pega o collider que bloqueia o player (não trigger)
//         solidCollider = GetComponent<Collider2D>();

//         // segurança caso esqueça no inspector
//         if (animator == null)
//             animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         if (!playerNear || opened) return;

//         if (Keyboard.current.fKey.wasPressedThisFrame)
//         {
//             if (InventorySystem.Instance.HasItem(keyIcon))
//             {
//                 InventorySystem.Instance.RemoveItem(keyIcon);

//                 UIMessage.Instance.Show("Abrindo porta...", 1.5f);

//                 animator.SetTrigger("Open");

//                 opened = true;
//                 playerNear = false;

//                 UIMessage.Instance.Hide();

//                 // 🔥 desativa colisão depois de abrir
//                 if (solidCollider != null)
//                     solidCollider.enabled = false;
//             }
//             else
//             {
//                 UIMessage.Instance.Show("Porta trancada", 2f);
//             }
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (opened) return; // 🔥 não faz nada se já abriu

//         if (other.CompareTag("Player"))
//         {
//             playerNear = true;

//             if (InventorySystem.Instance.HasItem(keyIcon))
//                 UIMessage.Instance.Show("Pressione F para abrir", 999f);
//             else
//                 UIMessage.Instance.Show("Porta trancada", 999f);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (opened) return; // 🔥 evita bugs

//         if (other.CompareTag("Player"))
//         {
//             playerNear = false;
//             UIMessage.Instance.Hide();
//         }
//     }
// }





using UnityEngine;
using UnityEngine.InputSystem;

public class DoorLocked : MonoBehaviour
{
    [SerializeField] private Sprite keyIcon;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D doorCollider; // 👈 USAR ESSE

    private bool playerNear;
    private bool opened = false;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerNear || opened) return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (InventorySystem.Instance.HasItem(keyIcon))
            {
                InventorySystem.Instance.RemoveItem(keyIcon);

                UIMessage.Instance.Show("Abrindo porta...", 1.5f);

                animator.SetTrigger("Open");

                opened = true;
                playerNear = false;

                UIMessage.Instance.Hide();

                // 🔥 DESATIVA COLISÃO CORRETA
                if (doorCollider != null)
                    doorCollider.enabled = false;
            }
            else
            {
                UIMessage.Instance.Show("Porta trancada", 2f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (InventorySystem.Instance.HasItem(keyIcon))
                UIMessage.Instance.Show("Pressione F para abrir", 999f);
            else
                UIMessage.Instance.Show("Porta trancada", 999f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            playerNear = false;
            UIMessage.Instance.Hide();
        }
    }
}