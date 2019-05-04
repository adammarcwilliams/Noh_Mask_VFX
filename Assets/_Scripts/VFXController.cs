using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField]
    private bool transitionNextMask = false;

    private VisualEffect visualEffect;

    private string[] masks = new string[2];

    private int maskIndex = 0;


    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        masks[0] = "Kitsune";
        masks[1] = "Hannya";
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
        visualEffect.SetFloat($"{firstMask}_disperse", 0f);
        visualEffect.SendEvent($"OnPlay{firstMask}");
        yield return null;
    }

    private IEnumerator TransitionNextMask()
    {
        // disperse current mask
        string currentMask = masks[maskIndex];
        visualEffect.SetFloat($"{currentMask}_disperse", 100f);
        yield return new WaitForSeconds(2);
        // reint visual effect
        visualEffect.Reinit();
        // make sure disperse is 0 on next mask
        maskIndex = (maskIndex + 1) % masks.Length;
        string nextMask = masks[maskIndex];
        visualEffect.SetFloat($"{nextMask}_disperse", 0f);
        // play next mask
        visualEffect.SendEvent($"OnPlay{nextMask}");
        // update currentmask to next mask
        yield return null;
    }
}
