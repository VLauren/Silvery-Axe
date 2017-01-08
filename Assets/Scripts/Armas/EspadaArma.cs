using UnityEngine;
using System.Collections;
using System;

public class EspadaArma : Arma
{
    public static GameObject elCubo; // referencia al cubo de hielo

    public override void Usar()
    {
        // si no hay cubo colocado y estoy en el suelo, creo el cubo
        if (!elCubo && Jugador.instancia.cc.isGrounded)
        {
            // calculo la posicion del cubo
            Vector3 posicionCubo = Jugador.instancia.transform.position;
            posicionCubo.y -= 1.3f;

            // creo el objeto
            elCubo = GameObject.Instantiate(Resources.Load("Prefabs/CuboHielo")) as GameObject;
            elCubo.transform.position = posicionCubo;

            // animacion del jugador
            Jugador.AnimEspada();
        }

        base.Usar();
    }

    // con click derecho
    public override void Usar2()
    {
        // si hay cubo, lo destruyo
        if (elCubo != null)
        {
            //esto es para que se active el OnTriggerExit
            elCubo.transform.position = new Vector3(0, -100, 0);
            // TODO esto pasará por el gestor en lugar de estar así
            Jugador.instancia.StartCoroutine(DestruirCubo());

            // creo el efecto
            Efectos.EfectoCuboHielo(elCubo.transform.position);

            // compruebo si por borrar el cubo se despulsa un boton
            NotificationCenter.DefaultCenter().PostNotification(Jugador.instancia, "ComprobarBotones");
        }

        base.Usar2();
    }

    private IEnumerator DestruirCubo()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (elCubo != null)
            GameObject.Destroy(elCubo);
    }
}