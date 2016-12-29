using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mirilla : MonoBehaviour
{
    private static Text texto;

    public void Awake()
    {
        texto = GetComponent<Text>();
        texto.enabled = false;
    }

    public static void SetMirilla(bool activo)
    {
        if (texto)
        {
            texto.enabled = activo;
        }
    }
}
