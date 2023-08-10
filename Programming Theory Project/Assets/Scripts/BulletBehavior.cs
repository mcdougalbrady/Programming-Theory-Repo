using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] Vector3 moveDirection = Vector3.up;
    [SerializeField] float speed;

    void Update()
    {
        if(transform.position.y > maxY || transform.position.y < minY)
        {
            Destroy(gameObject);
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
