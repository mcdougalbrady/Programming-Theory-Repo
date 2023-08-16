using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    private float minX = -8.0f;
    private float maxX = 7.0f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] ParticleSystem particle;
    private AudioSource audioSource;
    private GameManager gameManager;

    private void Start()
    {
        audioSource = GameObject.Find("SFXManager").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if ((Input.GetAxis("Horizontal") < 0 && transform.position.x >= minX)
            || (Input.GetAxis("Horizontal") > 0 && transform.position.x <= maxX))
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        Instantiate(bulletPrefab, bulletOrigin.position + new Vector3(0,1,0), Quaternion.identity);
        audioSource.PlayOneShot(laserSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerHit();
        }
    }

    public void PlayerHit()
    {
        audioSource.PlayOneShot(dieSound);
        particle.Play();
        SetVisible(false);
        gameManager.GameOver();
    }

    private void SetVisible(bool isVisible)
    {
        Renderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        if(isVisible)
        {
            foreach(Renderer m in meshRenderers)
            {
                m.enabled = true;
            }
        } else
        {
            foreach (Renderer m in meshRenderers)
            {
                m.enabled = false;
            }
        }
    }
}
