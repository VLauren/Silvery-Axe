using UnityEngine;
using System.Collections;

public class Rotar : MonoBehaviour
{
	void Update ()
    {
        transform.Rotate(0, Time.deltaTime * 45, 0, Space.World);
	}
}
