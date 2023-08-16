using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] int strength = 1;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip bulletSound;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shotRate = 0.5f;
    [SerializeField] ParticleSystem explosionParticle;
    private AudioSource audioSource;
    private EnemyContainerBehavior container;
    private GameManager gameManager;
    public int hitScoreValue = 1;
    public int killScoreValue = 5;

    private void Start()
    {
        container = GetComponentInParent<EnemyContainerBehavior>();
        audioSource = GameObject.Find("SFXManager").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Randomly selects whether this enemy will be a shooter (1 in 3 chance)
        if(Random.Range(0,3) == 1)
        {
            StartCoroutine(RepeatShots());
        }
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
            other.gameObject.GetComponent<PlayerBehavior>().PlayerHit();
        }
    }

    private void Hit()
    {
        strength--;
        if(strength == 0)
        {
            explosionParticle.Play();
            audioSource.PlayOneShot(dieSound);
            gameManager.updateScore(killScoreValue);
            Destroy(gameObject);
        } else
        {
            gameManager.updateScore(hitScoreValue);
            audioSource.PlayOneShot(hitSound);
        }
    }

    IEnumerator RepeatShots()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        while(true)
        {
            Fire();
            yield return new WaitForSeconds(Random.Range(1, 6) * shotRate);
        }
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, bulletOrigin.position - new Vector3(0, -0.5f, 0), Quaternion.identity);
        //audioSource.PlayOneShot(bulletSound);
    }

}
