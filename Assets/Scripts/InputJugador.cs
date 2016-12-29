using UnityEngine;
using System.Collections;

public class InputJugador : MonoBehaviour
{
    private Jugador jugador;

    void Start()
    {
        jugador = GetComponent<Jugador>();
    }

    void Update()
    {
        //leer input y envíar mensajes al movedor
        Vector3 direccion = Vector3.zero;

        direccion.x = Input.GetAxisRaw("Horizontal");
        direccion.z = Input.GetAxisRaw("Vertical");

        jugador.Mover(direccion);
    }
}
