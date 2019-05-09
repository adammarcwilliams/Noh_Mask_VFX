using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class MasksController : MonoBehaviour
{
    [SerializeField]
    private bool transitionNextMask = false;

    private VisualEffect visualEffect;
    private string[] masks = new string[5];
    private int maskIndex = 0;

    public void NextMask()
    {
        StartCoroutine(TransitionNextMask());
    }

    public void PreviousMask()
    {
        StartCoroutine(TransitionPreviousMask());
    }

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        masks[0] = "Kitsune";
        masks[1] = "Hannya";
        masks[2] = "Karura";
        masks[3] = "Tengu";
        masks[4] = "Usagi";

        StartCoroutine(PlayFirstMask());
    }


    private void Update()
    {
        if (transitionNextMask)
        {
            StartCoroutine(TransitionNextMask());
            transitionNextMask = false;
        }
    }

    private IEnumerator PlayFirstMask()
    {
        yield return new WaitForSeconds(2f);
        string firstMask = masks[maskIndex];
        visualEffect.SetFloat("disperse", 0f);
        visualEffect.SendEvent($"OnPlay{firstMask}");
        yield return null;
    }

    private IEnumerator TransitionNextMask()
    {
        // disperse current mask
        string currentMask = masks[maskIndex];
        visualEffect.SetFloat("disperse", -10f);
        yield return new WaitForSeconds(1f);
        // reint visual effect
        visualEffect.Reinit();
        // update currentmask to next mask
        maskIndex = (maskIndex + 1) % masks.Length;
        string nextMask = masks[maskIndex];
        // make sure disperse is 0 on next mask
        visualEffect.SetFloat("disperse", 0f);
        // play next mask
        visualEffect.SendEvent($"OnPlay{nextMask}");
        yield return null;
    }

    private IEnumerator TransitionPreviousMask()
    {
        // disperse current mask
        string currentMask = masks[maskIndex];
        visualEffect.SetFloat("disperse", 10f);
        yield return new WaitForSeconds(1f);
        // reint visual effect
        visualEffect.Reinit();
        // update currentmask to previous mask
        if (maskIndex == 0)
        {
            maskIndex = masks.Length - 1;
        }
        else
        {
            maskIndex = (maskIndex - 1);
        }

        string previousMask = masks[maskIndex];
        // make sure disperse is 0 on next mask
        visualEffect.SetFloat("disperse", 0f);
        // play previous mask
        visualEffect.SendEvent($"OnPlay{previousMask}");
        yield return null;
    }

}
