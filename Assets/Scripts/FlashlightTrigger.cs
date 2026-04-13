using UnityEngine;

public class FlashlightTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var effect = other.GetComponent<LightInteractEffect>();

        if (effect != null)
        {
            effect.OnLightEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var effect = other.GetComponent<LightInteractEffect>();

        if (effect != null)
        {
            effect.OnLightExit();
        }
    }
}