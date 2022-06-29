using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealt : MonoBehaviour
{
    public int health = 100;
    public int lifes = 100;

    public void RestoreLife()
    {
        health = 100;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Lifes();
        }
    }

    void Lifes()
    {
        lifes -= 1;

        for (int i = 0; i < Enemy.Instance.spawns.Count; i++)
        {
            Destroy(Enemy.Instance.spawns[i]);
        }

        Enemy.Instance.spawns.Clear();

        if (lifes > 0)
        {
            Enemy.Instance.LevelUp();
        }
        else
        {
            Enemy.Instance.Die();
        }
    }
}
