using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBrick : BrickBase
{
    protected override void Dead()
    {
        gameManager.BrickHasDied();
        Destroy(gameObject);
    }
}
