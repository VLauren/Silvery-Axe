using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            JugadorEnElPortal();
        }
    }

    void CambiarNivel()
    {
        SceneManager.LoadScene(0);
    }

    public void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "LlaveRecogida");
    }

    void JugadorEnElPortal()
    {
        if (Input.GetButtonDown("Fire1") && llaveRecogida)
        {
            Invoke("CambiarNivel", 0.5f);
        }
    }

    private bool llaveRecogida = false;

    void LlaveRecogida()
    {
        llaveRecogida = true;
    }
}
