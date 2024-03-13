using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public float speed = 1.0f;
    public float descentRate = 0.5f;
    public bool movingRight = true;
    public float boundaryRight = 5f;
    public float boundaryLeft = -5f; 

    public int totalEnemies;
    public float speedIncrement = 0.5f;
    public int badGuysDestroyed = 0; 

    void Start()
    {
        totalEnemies = FindObjectsOfType<Enemy>().Length;  //get da enemies 
    }
    
    void Update()
    {
        
        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies defeated!");
            MainMenu.Instance.CreditsTime();//Go to credits scene 
        }
        else
        {
            MoveEnemies(); 
        }
    }

    private void MoveEnemies()
    {
        
        float currentSpeed = speed + speed * (1 - (float)totalEnemies / FindObjectsOfType<Enemy>().Length);
        float tempSpeed = currentSpeed * Time.deltaTime; //syntax reasons
        if (movingRight)
        {
            if (transform.position.x > boundaryRight)
            {
                MoveDown();
                movingRight = false;
            }
            else
            {
                transform.Translate(Vector2.right * tempSpeed);
            }
        }
        else
        {
            if (transform.position.x < boundaryLeft)
            {
                MoveDown();
                movingRight = true;
            }
            else
            {
                transform.Translate(Vector2.left * tempSpeed);
            }
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * descentRate);
    }

    public void EnemyDestroyed()
    {
        totalEnemies--;
        badGuysDestroyed++;

        if (badGuysDestroyed >= 2)
        {
            speed += speedIncrement;
            badGuysDestroyed = 0; 
        }

    }
}
