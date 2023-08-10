using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] int strength = 1;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip bulletSound;
    private AudioSource audioSource;
    private EnemyContainerBehavior container;

    private void Start()
    {
        container = GetComponentInParent<EnemyContainerBehavior>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Hit();
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            if((other.gameObject.name == "LeftWall" && container.MoveDirection == EnemyContainerBehavior.Direction.Left)
                || (other.gameObject.name == "RightWall" && container.MoveDirection == EnemyContainerBehavior.Direction.Right))
            {
                container.SwitchDirection();
                container.MoveDown();
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            Destroy(other.gameObject);
            container.StopMoving();
        }
    }

    private void Hit()
    {
        strength--;
        if(strength == 0)
        {
            audioSource.PlayOneShot(dieSound);
            Destroy(gameObject);
        } else
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

}
