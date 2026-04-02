// using UnityEngine;

// public class AreaEntrance : MonoBehaviour
// {
   
//    [SerializeField] private string transitionName;

//     private void Start()
//     {
//         if (transitionName == SceneManagement.Instance.SceneTransitionName)
//         {
//             PlayerController.Instance.transform.position = this.transform.position;
//         }
//     }

// }



using UnityEngine;
using Unity.Cinemachine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        StartCoroutine(SetPlayerPosition());
    }

    private System.Collections.IEnumerator SetPlayerPosition()
    {
        yield return null; // espera 1 frame

        if (SceneManagement.Instance == null)
        {
            Debug.LogError("SceneManagement não encontrado");
            yield break;
        }

        if (PlayerController.Instance == null)
        {
            Debug.LogError("PlayerController não encontrado");
            yield break;
        }

        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            // posiciona player
            PlayerController.Instance.transform.position = transform.position;

            // conecta camera
            CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();

            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();

            if (cam != null)
            {
                cam.Follow = PlayerController.Instance.transform;
            }
            else
            {
                Debug.LogWarning("CinemachineCamera não encontrada na cena");
            }
        }
    }
}