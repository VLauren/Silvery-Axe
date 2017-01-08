using UnityEngine;
using System.Collections;

public class Efectos : MonoBehaviour
{
    public enum TipoEfecto
    {
        CuboHielo,
        CogerArma
    }

    public static readonly string[] nombrePrefabs = 
    {
        "EfectoCuboHielo",
        "EfectoCogerArma"
    };

    public static void EfectoCuboHielo(Vector3 posicion)
    {
        CrearEfecto(posicion, TipoEfecto.CuboHielo);
    }

    public static void CrearEfecto(Vector3 posicion, TipoEfecto tipo)
    {
        if (Global.gfxParticulas)
        {
            GameObject efecto = Resources.Load("Prefabs/"+ nombrePrefabs[(int)tipo]) as GameObject;
            GameObject.Instantiate(efecto, posicion, Quaternion.identity);
        }
    }
}
