using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PulseUI : MonoBehaviour
{
    [SerializeField]
    private bool playOnStart = false;

    private void Start()
    {
        if (playOnStart)
        {
            TogglePulse(true);
        }
    }

    public void TogglePulse (bool pulse)
    {
        if (pulse)
        {
            transform.DOScale(1.1f, 0.8f).SetLoops(-1);
        }
        else
        {
            DOTween.Kill(transform);
        }

    }
}
