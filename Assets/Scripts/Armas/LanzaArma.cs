using UnityEngine;
using System.Collections;

public class LanzaArma : Arma
{
    private static ProyectilTP prefab;
    private static ProyectilTP lanzado;

    private bool cargando;
    private float tiempoCarga;

    public LanzaArma()
    {
        // referencia al prefab del proyectil
        if (prefab == null)
            prefab = Resources.Load<ProyectilTP>("Prefabs/ProyectilTP");
    }

    public override void Usar()
    {
        // si estoy en el suelo inicio la carga
        if (Jugador.instancia.cc.isGrounded)
        {
            cargando = true;
            tiempoCarga = Time.time;
            Jugador.AnimCargando();

            Mirilla.SetMirilla(true);
        }
    }

    const int capaJug = 8;
    int mascara = ~(1 << capaJug);

    // al soltar el boton
    public override void Soltar()
    {
        // si estaba cargando
        if (cargando)
        {
            cargando = false;
            Mirilla.SetMirilla(false);

            if (lanzado)
                GameObject.Destroy(lanzado.gameObject);

            RaycastHit hit;
            Transform cam = Camera.main.transform;
            if(Physics.Raycast(cam.position, cam.forward, out hit, 200, mascara))
            {
                Vector3 posicion = Jugador.instancia.transform.position;
                posicion += Jugador.instancia.modelo.transform.right;

                Vector3 direccion = hit.point - posicion;

                // creo la lanza y la envío en la direccion calculada
                lanzado = GameObject.Instantiate(prefab, posicion, Quaternion.identity) as ProyectilTP;
                lanzado.Lanzar(direccion, Time.time - tiempoCarga);
            }
            
            // vuelvo a la animacion de reposo
            Jugador.AnimIdle();

            // TODO animacion de lanzar
        }
    }

    // con el boton segundario
    public override void Usar2()
    {
        // teletransporte a la lanza
        Teleportar();
    }

    public static void Teleportar()
    {
        if (lanzado)
        {
            Efectos.CrearEfecto(Jugador.instancia.transform.position, Efectos.TipoEfecto.CogerArma);

            Jugador.instancia.transform.position = lanzado.transform.position;
            GameObject.Destroy(lanzado.gameObject);

            Efectos.CrearEfecto(Jugador.instancia.transform.position, Efectos.TipoEfecto.CogerArma);
        }
    }
}
