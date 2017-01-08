using UnityEngine;
using System.Collections;

public class Gestor : MonoBehaviour
{
    public void Update()
    {
        // esconder el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
