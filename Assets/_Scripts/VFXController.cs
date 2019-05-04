using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFXController : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private float kitsuneDisperse = 0;

    private VisualEffect visualEffect;


    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        visualEffect.SetFloat("kitsune_disperse", kitsuneDisperse);
    }
}
