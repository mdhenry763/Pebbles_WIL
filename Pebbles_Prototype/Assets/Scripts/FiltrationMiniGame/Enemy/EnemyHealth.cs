using System.Collections;
using System.Collections.Generic;


using System.Data;


using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    GameObject ScoreKeeper;
    public int maxHealth;
    public int currentHealth;
    public int damage;
    public int moveSpeed;

    #region Accessors
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public int getMoveSpeed()
    {
        return moveSpeed;
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getDamage()
    {
        return damage;
    }

    
    #endregion

    public void setMaxHealth(int health)
    {
        this.maxHealth = health;
    }
    public void setCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }
    public void setMoveSpeed(int speed)
    {
        this.moveSpeed = speed;
    }
    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    /*public EnemyHealth( int MaxHP, int Damage, int moveSpeed) 
    {
        this.maxHealth = MaxHP;
        this.currentHealth = MaxHP;
        this.damage = Damage;
        this.moveSpeed = moveSpeed;
    }*/
    // Start is called before the first frame update
    void Start()
    {
       // ScoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper");
    }
    public void takeDamage(int damage)
    {
        this.currentHealth = currentHealth - damage;

        if (this.currentHealth <= 0)
        {
            Destroy(gameObject);
            ScoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper");
            ScoreKeeper.GetComponent<Score>().IncreaseScore();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
