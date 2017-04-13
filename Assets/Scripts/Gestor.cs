using UnityEngine;
using System.Collections;

public class Gestor : MonoBehaviour
{
    public bool BloquearRaton = true;

    public void Update()
    {
        if (BloquearRaton)
        {
            // esconder el cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
