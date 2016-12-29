using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZonaMatar : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //esto se hará enviando mensajes y tal
        SceneManager.LoadScene(0);
    }
}
