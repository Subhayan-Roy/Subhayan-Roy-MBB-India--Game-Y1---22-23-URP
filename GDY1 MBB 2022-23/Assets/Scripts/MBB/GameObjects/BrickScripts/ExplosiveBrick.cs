using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBrick : BrickBase
{
    public float explosionRadius = 1;
    public LayerMask brickLayer;
    public int explosiveBrickDamage = 10;

    Collider2D[] bricksInRadius;

    bool hasExploded;

    protected override void Dead()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            Explode();
            gameManager.BrickHasDied();
            Destroy(this.gameObject);
        }
        
    }

    void Explode()
    {
        GetBricksToExplode();
        
        if (bricksInRadius != null)
        {
            foreach (Collider2D brick in bricksInRadius)
            {
                BrickBase bb = brick.gameObject.GetComponent<BrickBase>();
                bb.Damage(explosiveBrickDamage);
            }
        }
    }

    void GetBricksToExplode()
    {
        bricksInRadius = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius, brickLayer);
    }


}
