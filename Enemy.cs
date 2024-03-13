using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int pointsWorth);
    public static event EnemyDied OnEnemyDied;
    public int points = 10; 
    private EnemyGroupController enemyGroupController;

    public GameObject enemyBulletPrefab;
    public float fireRate = 3.0f;
    public float nextFireTime = 0f;
    public AudioClip scream;
    public AudioClip weow; 
    private AudioSource audioSource; 
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyGroupController = FindObjectOfType<EnemyGroupController>();
        nextFireTime = Time.time + fireRate; 
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate; 
        }
    }

    private void Fire()
    {
        GameObject bullet =  Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.isEnemyBullet = true;
        audioSource.PlayOneShot(weow);
    }
    void OnCollisionEnter2D(Collision2D collision)  
    {

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("Ouch!");
            Destroy(collision.gameObject); //Destroy the bullet 
            audioSource.PlayOneShot(scream);
      
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.AddScore(points);
      
            enemyGroupController.EnemyDestroyed();
            OnEnemyDied.Invoke(points);
            
            GetComponent<Animator>().SetTrigger("Dead Trigger");
        }
      
    }

    void enemyDied()
    {
        Destroy(gameObject); //Destroy enemy 
    }

    void moveMysteryEnemy()
    {
        GetComponent<Animator>().SetTrigger("Dash Trigger");
    }
}
