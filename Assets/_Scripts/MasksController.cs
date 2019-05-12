using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

[RequireComponent(typeof(VisualEffect))]
[RequireComponent(typeof(AudioSource))]
public class MasksController : MonoBehaviour
{
    [SerializeField]
    private Mask[] masks;

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
        // disperse current mask offscreen left
        string currentMask = masks[maskIndex].MaskName;
        visualEffect.SetFloat("disperse", -10f);
        yield return new WaitForSeconds(1f);
        // reinit visual effect
        visualEffect.Reinit();
        // update currentmask to next mask
        maskIndex = (maskIndex + 1) % masks.Length;
        string nextMask = masks[maskIndex].MaskName;
        // make sure disperse is 0 on next mask
        visualEffect.SetFloat("disperse", 0f);
        // play next mask
        visualEffect.SendEvent($"OnPlay{nextMask}");
        yield return null;
    }

    private IEnumerator TransitionPreviousMask()
    {
        // disperse current mask offscreen right
        string currentMask = masks[maskIndex].MaskName;
        visualEffect.SetFloat("disperse", 10f);
        yield return new WaitForSeconds(1f);
        // reinit visual effect
        visualEffect.Reinit();
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
        // make sure disperse is 0 on next mask
        visualEffect.SetFloat("disperse", 0f);
        // play previous mask
        visualEffect.SendEvent($"OnPlay{previousMask}");
        yield return null;
    }

}
