using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette vig;

    void Start()
    {
        vig = ScriptableObject.CreateInstance<Vignette>();
        vig.enabled.Override(true);
        vig.intensity.Override(1f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vig);
    }

    void Update()
    {

    }

    void ChangeVignette(int dir)
    {
        if (dir <= 1 && vig.intensity.value > 0.45)
            vig.intensity.value = vig.intensity.value - 0.05f;
        else
            if (vig.intensity.value < 1)
                vig.intensity.value = vig.intensity.value + 0.05f;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}