using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] float minMaxX = 9.0f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] AudioClip laserSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetAxis("Horizontal") < 0 && transform.position.x > -minMaxX)
            || (Input.GetAxis("Horizontal") > 0 && transform.position.x < minMaxX))
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
}
