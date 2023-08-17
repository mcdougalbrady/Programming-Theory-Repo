using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behavior : BasicEnemy
{
    void Start()
    {
        /*
         * Example of inheritance - Base class BasicEnemy has default functionality,
         * Derived classes set their own variables on initiation to have different
         * attributes
         */

        // INHERITANCE
        strength = 2;
        shotRate = 0.7f;
        hitScoreValue = 2;
        killScoreValue = 4;
        shooterProbability = 4;
        Initiate();
    }

}
