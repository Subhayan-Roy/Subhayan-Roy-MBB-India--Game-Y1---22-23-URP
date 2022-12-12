using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBrick : BrickBase
{
    public float explosionRadius = 1;
    public LayerMask brickLayer;
    public int explosiveBrickDamage = 10;
	public Transform vfxTransform;

    Collider2D[] bricksInRadius;

    bool hasExploded;

	protected override void Awake()
	{
		base.Awake();

		vfxTransform.parent = null; //We make the vfx independent so that it won't get destroyed when the brick gets destroyed
	}

	protected override void Dead()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            Explode();
            gameManager.BrickHasDied();
			Destroy(gameObject);
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

		vfxTransform.gameObject.SetActive(true); //We activate the vfx object so that it plays its stuff
    }

    void GetBricksToExplode()
    {
        bricksInRadius = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius, brickLayer);
    }


}
