using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoLuzAntorcha : MonoBehaviour
{
    public float cantidad = 0.1f;
    public float frecuencia = 2;

    private float m_startX;
    private float m_startY;
    private float m_startZ;

    void Start()
    {
        m_startX = Random.value * Mathf.PI * 2;
        m_startY = Random.value * Mathf.PI * 2;
        m_startZ = Random.value * Mathf.PI * 2;
    }

	void Update ()
    {
        float t = Time.time * Mathf.PI * 2 * frecuencia;
        float x = transform.position.x + Mathf.Sin(Mathf.PI * 2 * (Mathf.Sin(t + m_startX))) * cantidad;
        float y = transform.position.y + Mathf.Sin(Mathf.PI * 2 * (Mathf.Sin(t + m_startY))) * cantidad;
        float z = transform.position.z + Mathf.Sin(Mathf.PI * 2 * (Mathf.Sin(t + m_startZ))) * cantidad;

        transform.position = new Vector3(x, y, z);
    }
}
