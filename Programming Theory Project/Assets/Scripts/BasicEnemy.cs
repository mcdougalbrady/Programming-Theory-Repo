using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // ENCAPSULATION
    protected int strength { get; set; }
    protected float shotRate { get; set; }
    protected int hitScoreValue { get; set; }
    protected int killScoreValue { get; set; }
    protected int shooterProbability { get; set; }
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioClip dieSound;
    [SerializeField] protected AudioClip bulletSound;
    [SerializeField] protected Transform bulletOrigin;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected ParticleSystem explosionParticle;
    protected AudioSource audioSource;
    protected EnemyContainerBehavior container;
    protected GameManager gameManager;
    private PlayerBehavior player;

    protected void Initiate()
    {
        container = GameObject.Find("EnemyContainer").GetComponent<EnemyContainerBehavior>();
        audioSource = GameObject.Find("SFXManager").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        // Randomly selects whether this enemy will be a shooter (1 in 3 chance)
        if (Random.Range(0, shooterProbability) == 1)
        {
            StartCoroutine(RepeatShots());
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Hit(other.gameObject.GetComponent<PlayerBullet>().strength);
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            if ((other.gameObject.name == "LeftWall" && container.MoveDirection == EnemyContainerBehavior.Direction.Left)
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

    protected void Hit(int damage)
    {
        if(player.hasPowerUp)
        {
            strength -= (damage * 2);
        } else
        {
            strength -= damage;
        }
        if (strength <= 0)
        {
            explosionParticle.Play();
            audioSource.PlayOneShot(dieSound);
            gameManager.updateScore(killScoreValue);
            gameManager.DeductEnemyCount();
            Destroy(gameObject);
        }
        else
        {
            gameManager.updateScore(hitScoreValue);
            audioSource.PlayOneShot(hitSound);
        }
    }


    protected IEnumerator RepeatShots()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        while (true)
        {
            if(!player.hasPowerUp && !gameManager.gameOver)
            {
                Fire();
            }
            yield return new WaitForSeconds(Random.Range(1, 6) * shotRate);
        }
    }

    protected void Fire()
    {
        Instantiate(bulletPrefab, bulletOrigin.position - new Vector3(0, -0.5f, 0), Quaternion.identity);
        audioSource.PlayOneShot(bulletSound);
    }

}
