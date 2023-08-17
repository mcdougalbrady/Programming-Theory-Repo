using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behavior : BasicEnemy
{
    void Start()
    {
        /*
         * Example of inheritance - Base class BasicEnemy has default functionality,
         * Derived classes set their own variables on initiation to have different
         * attributes
         */

        // INHERITANCE
        strength = 3;
        shotRate = 0.5f;
        hitScoreValue = 1;
        killScoreValue = 5;
        shooterProbability = 3; 
        Initiate();
    }
}
