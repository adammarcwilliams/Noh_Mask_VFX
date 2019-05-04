using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFXController : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private float kitsuneDisperse = 0;

    [SerializeField]
    private bool playKitsune;

    [SerializeField]
    private bool stopKitsune;

    private VisualEffect visualEffect;


    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        visualEffect.SetFloat("kitsune_disperse", kitsuneDisperse);

        if (playKitsune)
        {
            visualEffect.SendEvent("OnPlayKitsune");
            playKitsune = false;
        }

        if (stopKitsune)
        {
            visualEffect.SendEvent("OnStopKitsune");
            stopKitsune = false;
        }
    }
}
