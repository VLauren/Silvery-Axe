using UnityEngine;
using System.Collections;

public class LanzaObjeto : ArmaObjeto
{
    protected override void RecogerArma()
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "LanzaRecogida");
        base.RecogerArma();
    }

}
