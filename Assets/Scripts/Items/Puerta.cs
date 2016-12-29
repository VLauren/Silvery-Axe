using UnityEngine;
using System.Collections;

public class Puerta : Receptor
{
    Animator animator;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void Accionar()
    {
        animator.SetBool("Accionado", true);
        base.Accionar();
    }

    public override void Desaccionar()
    {
        animator.SetBool("Accionado", false);
        base.Desaccionar();
    }
}
