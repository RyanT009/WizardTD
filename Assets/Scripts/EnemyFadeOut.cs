using UnityEngine;
using System.Collections;

public class EnemyFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f; // seconds to fade out
    private Renderer[] renderers;
    private Material[][] runtimeMats;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        runtimeMats = new Material[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] mats = renderers[i].materials; // instantiates a copy automatically
            runtimeMats[i] = new Material[mats.Length];

            for (int j = 0; j < mats.Length; j++)
            {
                Material mat = new Material(mats[j]); // ensure we don’t edit shared material
                runtimeMats[i][j] = mat;

                // Make material transparent for fade
                SetupMaterialForTransparency(mat);

                // Make sure alpha starts at 1
                Color c = mat.color;
                c.a = 1f;
                mat.color = c;
            }

            renderers[i].materials = runtimeMats[i];
        }
    }

    /// <summary>
    /// Call this to start fading out and destroying the enemy
    /// </summary>
    public void FadeOutAndDestroy()
    {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        float elapsed = 0f;

        // Store original colors
        Color[][] startColors = new Color[runtimeMats.Length][];
        for (int i = 0; i < runtimeMats.Length; i++)
        {
            startColors[i] = new Color[runtimeMats[i].Length];
            for (int j = 0; j < runtimeMats[i].Length; j++)
            {
                startColors[i][j] = runtimeMats[i][j].color;
            }
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            for (int i = 0; i < runtimeMats.Length; i++)
            {
                for (int j = 0; j < runtimeMats[i].Length; j++)
                {
                    Color c = startColors[i][j];
                    c.a = alpha;
                    runtimeMats[i][j].color = c;
                }
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Converts any material to a transparent-compatible setup for fading
    /// </summary>
    private void SetupMaterialForTransparency(Material mat)
    {
        // If using Standard Shader
        if (mat.HasProperty("_Mode"))
        {
            mat.SetFloat("_Mode", 3); // Transparent
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
        else
        {
            // For URP/HDRP or other custom shaders, try switching to Unlit/Transparent if needed
            Shader transparentShader = Shader.Find("Universal Render Pipeline/Unlit");
            if (transparentShader != null)
                mat.shader = transparentShader;
        }
    }
}
