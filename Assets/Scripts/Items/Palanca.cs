using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Palanca : Accionador
{
    Animator animator;
    private bool activado;

    public void Start()
    {
        animator = GetComponent<Animator>();
        activado = false;
    }

    public void Update()
    {
        if (jugadorCerca && Input.GetButtonDown("Usar") && activado == false)
        {
            animator.SetBool("Activado", true);
            AbrirPuertas();
            activado = true;
        }
        else if (jugadorCerca && Input.GetButtonDown("Usar") && activado == true)
        {
            animator.SetBool("Activado", false);
            CerrarPuertas();
            activado = false;
        }
    }

    private bool jugadorCerca = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            jugadorCerca = true;
    }

    public void OnTiggerExit(Collider other)
    {
        if (other.tag == "Player")
            jugadorCerca = false;
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

}


