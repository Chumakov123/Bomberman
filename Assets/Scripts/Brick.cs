using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Destructible
{
    public override void Death()
    {
        gameObject.SetActive(false);
    }
}
