using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

[RequireComponent(typeof(VisualEffect))]
[RequireComponent(typeof(AudioSource))]
public class MasksController : MonoBehaviour
{
    [SerializeField]
    private Mask[] masks = null;

    private VisualEffect visualEffect;
    private AudioSource audioSource;

    private int maskIndex = 0;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayFirstMask());
    }

    public void NextMask()
    {
        StartCoroutine(TransitionNextMask());
    }

    public void PreviousMask()
    {
        StartCoroutine(TransitionPreviousMask());
    }

    public void PlayMaskDescription ()
    {
        AudioClip description = masks[maskIndex].AudioDescription;
        if (description)
        {
            audioSource.clip = description;
            audioSource.Play();
        }
    }


    private IEnumerator PlayFirstMask()
    {
        // VFX seems to require delay on start
        // before calling an event??
        yield return new WaitForSeconds(2f);
        string firstMask = masks[maskIndex].MaskName;
        visualEffect.SendEvent($"OnPlay{firstMask}");
        yield return null;
    }

    private IEnumerator TransitionNextMask()
    {
        // disperse current mask using turbulence
        string currentMask = masks[maskIndex].MaskName;
        visualEffect.SetFloat("intensity", 50f);
        visualEffect.SendEvent($"OnStop{currentMask}");
        yield return new WaitForSeconds(1.5f);

        // update currentmask to next mask
        maskIndex = (maskIndex + 1) % masks.Length;
        string nextMask = masks[maskIndex].MaskName;
        // reset insensity to default
        visualEffect.SetFloat("intensity", 0.2f);
        // play next mask
        visualEffect.SendEvent($"OnPlay{nextMask}");
        yield return null;
    }

    private IEnumerator TransitionPreviousMask()
    {
        // disperse current mask using turbulence
        string currentMask = masks[maskIndex].MaskName;
        visualEffect.SetFloat("intensity", 50f);
        visualEffect.SendEvent($"OnStop{currentMask}");
        yield return new WaitForSeconds(1.5f);

        // update currentmask to previous mask
        if (maskIndex == 0)
        {
            maskIndex = masks.Length - 1;
        }
        else
        {
            maskIndex = maskIndex - 1;
        }

        string previousMask = masks[maskIndex].MaskName;
        // reset insensity to default
        visualEffect.SetFloat("disperse", 0.2f);
        // play previous mask
        visualEffect.SendEvent($"OnPlay{previousMask}");
        yield return null;
    }

}
