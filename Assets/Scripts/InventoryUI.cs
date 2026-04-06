using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    private bool aberto = false;

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            aberto = !aberto;

            inventoryPanel.SetActive(aberto);

            // pausa o jogo
            Time.timeScale = aberto ? 0f : 1f;
        }
    }
}