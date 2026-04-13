using UnityEngine;

public class LightInteractEffect : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    [SerializeField] private float scaleMultiplier = 1.2f;

    [SerializeField] private ParticleSystem glowParticles;

    private Vector3 originalScale;
    private bool isLit = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        // 🔥 garante que a partícula começa desligada
        if (glowParticles != null)
        {
            glowParticles.Stop();
            glowParticles.Clear();
        }
    }

    public void OnLightEnter()
    {
        if (isLit) return; // evita repetir várias vezes

        isLit = true;

        sr.color = highlightColor;
        transform.localScale = originalScale * scaleMultiplier;

        if (glowParticles != null)
        {
            glowParticles.Clear(); // limpa antes de tocar
            glowParticles.Play();
        }
    }

    public void OnLightExit()
    {
        if (!isLit) return;

        isLit = false;

        sr.color = normalColor;
        transform.localScale = originalScale;

        if (glowParticles != null)
        {
            glowParticles.Stop();
            glowParticles.Clear();
        }
    }
}