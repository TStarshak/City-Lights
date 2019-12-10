using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;

public class PostProcessing : MonoBehaviour
{
    static PostProcessVolume m_Volume;
    static PostProcessProfile profile;
    Color grey = new Color(0.24f, 0.24f, 0.24f);
    Color red = new Color(1, 0, 0);
    Color intermediate = new Color(0.6f, 0.1f, 0.1f);
    Color red2 = new Color(1, 0.125f, 0.125f);


    float time = 0.33f;

    private bool pulsing;

    void Start()
    {
        profile = Resources.Load("Scenes/PrototypeScene_Profiles/Main Camera Profile") as PostProcessProfile;
        profile.GetSetting<Vignette>().intensity.Override(0.6f);
        profile.GetSetting<Vignette>().color.Override(grey);
        profile.GetSetting<ColorGrading>().colorFilter.Override(new Color(1f, 1f, 1f));
        pulsing = false;
    }

    void Update()
    {
        /*
        if (!pulsing)
        {
            pulsing = true;
            StartCoroutine(pulse());
        } */
        if (PlayerState.localPlayerData.inDangerState)
        {
            if (!pulsing)
            {
                StartCoroutine(pulse());
                pulsing = true;
            }
        }
        else
        {
            if (pulsing)
            {
                StopCoroutine(pulse());
                profile.GetSetting<Vignette>().color.Override(grey);
                profile.GetSetting<ColorGrading>().colorFilter.Override(new Color(0.8f, 0.8f, 0.8f));
                pulsing = false;
            }
        }
    }

    public static void ChangeVignette(int dir)
    {
        Vignette vig = profile.GetSetting<Vignette>();
        if (dir < 0 && vig.intensity.value < 0.6)
            vig.intensity.Override(vig.intensity + 0.05f);
        else
            if (vig.intensity.value < 1)
            vig.intensity.Override(vig.intensity - 0.05f);
        

        profile.RemoveSettings<Vignette>();
        profile.AddSettings(vig);
    }

    public IEnumerator pulse()
    {
        ColorGrading grade = profile.GetSetting<ColorGrading>();
        Vignette vig = profile.GetSetting<Vignette>();
        while (PlayerState.localPlayerData.inDangerState) {
            profile.GetSetting<Vignette>().color.Override(intermediate);
            profile.GetSetting<ColorGrading>().colorFilter.Override(new Color(1, 0.33f, 0.33f));
            
            yield return new WaitForSeconds(time);

            profile.GetSetting<Vignette>().color.Override(red);
            profile.GetSetting<ColorGrading>().colorFilter.Override(red2);

            yield return new WaitForSeconds(time);

            profile.GetSetting<Vignette>().color.Override(intermediate);
            profile.GetSetting<ColorGrading>().colorFilter.Override(new Color(1, 0.33f, 0.33f));

            yield return new WaitForSeconds(time);

            profile.GetSetting<Vignette>().color.Override(grey);
            profile.GetSetting<ColorGrading>().colorFilter.Override(new Color(1, 1f, 1f));

            yield return new WaitForSeconds(time);
        }
        profile.GetSetting<ColorGrading>().colorFilter.Override(new Color(1f, 1f, 1f));
        yield return null;

    }    
    
}