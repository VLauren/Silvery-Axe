using UnityEngine;
using System.Collections;

public class ProyectilTP : MonoBehaviour
{
    private Rigidbody rb;
    private Transform modelo;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        modelo = transform.Find("Modelo");
    }

    public void Lanzar(Vector3 direccion, float fuerza)
    {
        Mathf.Clamp(fuerza, 20, 100);
        transform.LookAt(transform.position + direccion);
        rb.AddForce(direccion * fuerza * 100);
        rb.AddForce(Vector3.up * fuerza * 100);
    }

    public void Update()
    {
        if(rb && rb.velocity != Vector3.zero)
            modelo.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (rb)
        {
            rb.velocity = Vector3.zero;
            Destroy(rb);
        }
    }
}
