// // // using UnityEngine;
// // // using UnityEngine.InputSystem;
// // // using Unity.Cinemachine;

// // // public class FlashlightController : MonoBehaviour
// // // {
// // //     [SerializeField] private GameObject flashlight;

// // //     private bool ligada = false;

// // //     void Start()
// // //     {
// // //         flashlight.SetActive(false);
// // //     }

// // //     void Update()
// // //     {
// // //         if (Keyboard.current.eKey.wasPressedThisFrame)
// // //         {
// // //             ligada = !ligada;
// // //             flashlight.SetActive(ligada);
// // //         }

// // //         RotacionarLanterna();
// // //     }

// // //     void RotacionarLanterna()
// // //     {
// // //         Vector3 mousePos = Mouse.current.position.ReadValue();
// // //         Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);

// // //         Vector2 direction = (worldMouse - transform.position).normalized;

// // //         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

// // //         flashlight.transform.rotation = Quaternion.Euler(0, 0, angle);
// // //     }
// // // }




// // using UnityEngine;
// // using UnityEngine.InputSystem;

// // public class FlashlightController : MonoBehaviour
// // {
// //     [SerializeField] private Transform flashlight;

// //     private Camera cam;
// //     private bool ligada = false;

// //     void Awake()
// //     {
// //         cam = Camera.main;

// //         if (cam == null)
// //         {
// //             Debug.LogError("Nenhuma camera com tag MainCamera encontrada");
// //         }

// //         if (flashlight != null)
// //             flashlight.gameObject.SetActive(false);
// //     }

// //     void Update()
// //     {
// //         if (Keyboard.current.eKey.wasPressedThisFrame)
// //         {
// //             ligada = !ligada;

// //             if (flashlight != null)
// //                 flashlight.gameObject.SetActive(ligada);
// //         }
// //     }

// //     void LateUpdate()
// //     {
// //         if (!ligada || cam == null || flashlight == null)
// //             return;

// //         Vector3 mousePos = Mouse.current.position.ReadValue();

// //         mousePos.z = Mathf.Abs(cam.transform.position.z);

// //         Vector3 worldMouse = cam.ScreenToWorldPoint(mousePos);

// //         Vector2 direction = worldMouse - flashlight.position;

// //         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

// //         flashlight.rotation = Quaternion.Euler(0, 0, angle);
// //     }
// // }


// using UnityEngine;
// using UnityEngine.InputSystem;

// public class FlashlightController : MonoBehaviour
// {
//     [SerializeField] private Transform flashlight;

//     private Camera cam;
//     private bool ligada;

//     void Awake()
//     {
//         cam = Camera.main;
//         flashlight.gameObject.SetActive(false);
//     }

//     void Update()
//     {
//         if (Keyboard.current.eKey.wasPressedThisFrame)
//         {
//             ligada = !ligada;
//             flashlight.gameObject.SetActive(ligada);
//         }
//     }

//     void LateUpdate()
//     {
//         if (!ligada) return;

//         Vector3 mouseScreenPos = Mouse.current.position.ReadValue();

//         Vector3 mouseWorldPos = cam.ScreenToWorldPoint(
//             new Vector3(
//                 mouseScreenPos.x,
//                 mouseScreenPos.y,
//                 cam.nearClipPlane
//             )
//         );

//         Vector2 direction = mouseWorldPos - transform.position;

//         //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

//         flashlight.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
//     }
// }



using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Transform flashlight;

    private Camera cam;
    private bool ligada;

    void Awake()
    {
        AtualizarCamera();

        flashlight.gameObject.SetActive(false);

        SceneManager.sceneLoaded += QuandoTrocarCena;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= QuandoTrocarCena;
    }

    void QuandoTrocarCena(Scene scene, LoadSceneMode mode)
    {
        AtualizarCamera();
    }

    void AtualizarCamera()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogWarning("Camera principal não encontrada ainda...");
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            ligada = !ligada;
            flashlight.gameObject.SetActive(ligada);
        }
    }

    void LateUpdate()
    {
        if (!ligada)
            return;

        if (cam == null)
        {
            AtualizarCamera();
            return;
        }

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(
            new Vector3(
                mouseScreenPos.x,
                mouseScreenPos.y,
                Mathf.Abs(cam.transform.position.z)
            )
        );

        Vector2 direction = mouseWorldPos - transform.position;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        flashlight.rotation = Quaternion.Euler(0, 0, angle);
    }
}