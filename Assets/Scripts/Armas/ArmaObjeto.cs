using UnityEngine;
using System.Collections;

public class ArmaObjeto : MonoBehaviour
{
    GameObject modelo;
    private bool EsLlave;

    public void Start()
    {
        // me suscribo al mensaje de arma recogida
        if (!EsLlave)
            NotificationCenter.DefaultCenter().AddObserver(this, "ArmaRecogida");

        // referencia
        modelo = transform.FindChild("Modelo").gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        // si el jugador colisiona conmigo: recojo el arma
        if (other.tag == "Player")
            RecogerArma();
    }

    protected virtual void RecogerArma()
    {
        // notifico
        if (EsLlave)
            NotificationCenter.DefaultCenter().PostNotification(this, "LlaveRecogida");
        else
            NotificationCenter.DefaultCenter().PostNotification(this, "ArmaRecogida");

        // desactivo el objeto
        modelo.SetActive(false);
        modelo.GetComponentInParent<BoxCollider>().enabled = false;

        // creo el efecto
        Efectos.CrearEfecto(transform.position, Efectos.TipoEfecto.CogerArma);
    }

    // se llama cuando un arma es recogida
    void ArmaRecogida()
    {
        // creo el efecto de aparicion
        if(!modelo.active)
            Efectos.CrearEfecto(transform.position, Efectos.TipoEfecto.CogerArma);

        // reactivo el objeto
        modelo.SetActive(true);
        modelo.GetComponentInParent<BoxCollider>().enabled = true;
    }

}
