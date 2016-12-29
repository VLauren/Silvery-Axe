using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotonSuelo : Accionador
{
    public void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "ComprobarBotones");
    }

    private List<GameObject> pulsadores = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Pulsador")
        {
            pulsadores.Add(other.gameObject);
            AbrirPuertas();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Pulsador")
        {
            pulsadores.Remove(other.gameObject);
            LimpiarLista();
            if (pulsadores.Count == 0)
                CerrarPuertas();

        }
    }

    void AbrirPuertas()
    {
        foreach (Receptor r in receptores)
        {
            r.Accionar();
        }
    }

    void CerrarPuertas()
    {
        foreach (Receptor r in receptores)
        {
            r.Desaccionar();
        }
    }

    void LimpiarLista()
    {
        for (int i = 0; i < pulsadores.Count; i++)
        {
            if (pulsadores[i] == null)
                pulsadores.RemoveAt(i);
        }
    }

}
