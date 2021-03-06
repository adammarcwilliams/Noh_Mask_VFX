﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSwipe : MonoBehaviour
{
    [SerializeField]
    private GameEvent swipeLeft = null;
    [SerializeField]
    private GameEvent swipeRight = null;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private void Update()
    {
        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = secondPressPos - firstPressPos;
            currentSwipe.Normalize();

            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                swipeLeft.Raise();
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                swipeRight.Raise();
            }
        }
    }
}
