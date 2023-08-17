using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBehavior
{
    private PlayerBehavior player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    // POLYMORPHISM
    protected override float ApplySpeedModifier()
    {
        if(player.hasPowerUp)
        {
            return 0;
        }
        // INHERITANCE
        return speed;
    }
}
