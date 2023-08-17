using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehavior : MonoBehaviour
{
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] Vector3 moveDirection = Vector3.up;
    [SerializeField] protected float speed;
    public int strength;

    // ABSTRACTION
    void Update()
    {
        if(transform.position.y > maxY || transform.position.y < minY)
        {
            Destroy(gameObject);
        }

        transform.Translate(moveDirection * ApplySpeedModifier() * Time.deltaTime);
    }

    // ABSTRACTION
    protected abstract float ApplySpeedModifier();
}
