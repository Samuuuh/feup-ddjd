using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalWheelController: MonoBehaviour 
{
    private bool active = false;
    private static int _idCrystal = 1;

    private void Awake() {
        Events.OnChangeSelectedCrystal.AddListener(OnChangeSelectedCrystal);
    }

    private void Update() {
        if(active) {
            // Get cursor displacement using Look

            // Calculations to draw a line with cursor displacement. The start will be the center

            // Make hover effect
            switch (_idCrystal) {
                case 1:
                    //Debug.Log("Crystal 1");
                    break;
                case 2:
                    ///Debug.Log("Crystal 2");
                    break;
                 case 3:
                    //Debug.Log("Crystal 3");
                    break;
                case 4:
                    //Debug.Log("Crystal 4");
                    break;
            }
        }
    }

    private void OnChangeSelectedCrystal(int idCrystal) 
    {
        _idCrystal = idCrystal;
    }

    public void OnToggleCrystalWheel() 
    {
        active = !active;
        Debug.Log(active);
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        gameObject.SetActive(active);
    }
}