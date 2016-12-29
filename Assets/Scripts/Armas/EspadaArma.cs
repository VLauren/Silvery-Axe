using UnityEngine;
using System.Collections;
using System;

public class EspadaArma : Arma
{
    public static GameObject elCubo;

    public override void Usar()
    {
        if (!elCubo && Jugador.instancia.cc.isGrounded)
        {
            Vector3 posicionCubo = Jugador.instancia.transform.position;
            posicionCubo.y -= 1.3f;

            //Jugador.instancia.transform.position += new Vector3(0, 0.5f, 0);

            elCubo = GameObject.Instantiate(Resources.Load("Prefabs/CuboHielo")) as GameObject;
            elCubo.transform.position = posicionCubo;

            Jugador.AnimEspada();
        }

        base.Usar();
    }

    public override void Usar2()
    {
        if (elCubo != null)
        {
            //esto es para que se active el OnTriggerExit
            elCubo.transform.position = new Vector3(0, -100, 0);
            //esto pasará por el gestor en lugar de estar así
            Jugador.instancia.StartCoroutine(DestruirCubo());

            Efectos.EfectoCuboHielo(elCubo.transform.position);
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