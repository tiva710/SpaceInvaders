using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bulletPrefab;

  public Transform shootOffset;
  public GameObject enemyBulletPrefab;
  private AudioSource audioSource;
  public AudioClip shoot;
  public AudioClip dead;

  public ParticleSystem moveParticles; 

  public float moveSpeed = 5f; 
    // Update is called once per frame
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
      Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
      
    }

    void OnDestroy()
    {
      Enemy.OnEnemyDied -= EnemyOnOnEnemyDied; //unsubscribing
    }
    void EnemyOnOnEnemyDied(int pointsWorth)
    {
      Debug.Log($"Player received 'EnemyDied' worth {pointsWorth}");
      //add to score 
    }
    
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        audioSource.PlayOneShot(shoot);
        GetComponent<Animator>().SetTrigger("Shoot Trigger");
        GameObject shot = Instantiate(bulletPrefab, shootOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }

      float movement = moveSpeed * Time.deltaTime;
      var emission = moveParticles.emission;
      emission.rateOverTime = 0; //none unless moving 
      if (Input.GetKey(KeyCode.LeftArrow))
      {
         transform.position += Vector3.left * movement;
         ParticleDirection(false);

      }
      
      if (Input.GetKey(KeyCode.RightArrow))
      {
        transform.position += Vector3.right * movement;
        ParticleDirection(true);
      }
    }


    void ParticleDirection(bool movingRight)
    {
      var emission = moveParticles.emission;
      emission.rateOverTime = 10;

      if (movingRight)
      {
        moveParticles.transform.localRotation = Quaternion.Euler(0, 180, 0);
      }
      else
      {
        moveParticles.transform.localRotation = Quaternion.Euler(0, 0, 0);
      }

      if (!moveParticles.isPlaying)
      {
        moveParticles.Play();
      }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {

      if (collision.gameObject.CompareTag("EnemyBullet"))
      {
        audioSource.PlayOneShot(dead);
        Debug.Log("You Suck!");
        Destroy(collision.gameObject); //Destroy the bullet 
      
        GetComponent<Animator>().SetTrigger("Death Trigger");
      }
      
    }

    void playerDied()
    {
      Destroy(gameObject);
      MainMenu.Instance.CreditsTime(); //go to credits scene 
    }
  
}
