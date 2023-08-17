using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBehavior
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
            return speed * 1.5f;
        }
        // INHERITANCE
        return speed;
    }

}
