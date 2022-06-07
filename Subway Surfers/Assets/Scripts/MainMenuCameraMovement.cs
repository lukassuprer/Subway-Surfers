using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{ 
    private void FixedUpdate()
    {
        transform.DOMoveZ(transform.position.z + 10, 1f);
    }
}
