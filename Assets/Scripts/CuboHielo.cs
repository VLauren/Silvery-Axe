using UnityEngine;
using System.Collections;

public class CuboHielo : MonoBehaviour
{
    public GameObject cubo;
    public GameObject humo;

    void Start()
    {
        StartCoroutine(Aparecer());
	}

    const float delay = 1f; // retraso para crear el cubo
    const float tiempoSubir = 1f; // duracion de la animacion de subir
    const float distanciaSubir = 1.25f; // distancia recorrida

    float contador = 0;

	private IEnumerator Aparecer()
    {
        yield return new WaitForSeconds(delay);
        cubo.SetActive(true);
        humo.SetActive(true);
        Efectos.EfectoCuboHielo(transform.position);

        while (contador < tiempoSubir)
        {
            contador += Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime * distanciaSubir / tiempoSubir;
            Jugador.instancia.transform.position += Vector3.up * Time.deltaTime * distanciaSubir / tiempoSubir;

            yield return new WaitForEndOfFrame();
        }
    }
}
