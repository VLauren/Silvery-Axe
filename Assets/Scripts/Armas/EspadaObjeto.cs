using UnityEngine;
using System.Collections;

public class EspadaObjeto : ArmaObjeto
{
    protected override void RecogerArma()
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "EspadaRecogida");
        base.RecogerArma();
        
    }
}
