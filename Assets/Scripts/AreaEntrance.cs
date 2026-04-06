using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    [SerializeField] private Transform spawnPoint;

    private IEnumerator Start()
    {
        yield return null;

        SceneManagement sm = SceneManagement.Instance;

        if (sm == null)
        {
            Debug.LogError("SceneManagement não encontrado");
            yield break;
        }

        if (sm.SceneTransitionName != transitionName)
        {
            yield break;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint não definido");
            yield break;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player não encontrado");
            yield break;
        }

        player.transform.position = spawnPoint.position;

        // faz a camera seguir o player novamente
        CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();

        if (cam != null)
        {
            cam.Follow = player.transform;
        }
        else
        {
            Debug.LogError("CinemachineCamera não encontrada na cena");
        }

        UIFade.Instance.FadeToClear();
    }
    
}

