using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetected : MonoBehaviour
{
    [SerializeField] int damage;

    void OnTriggerEnter(Collider other)
    {
        if (Enemy.Instance.isInvulnerable)
            return;

        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            ManagerSound.Instance.PlayNewSound(Enemy.Instance.fx_Sound[1].name);
            Enemy.Instance.GetDamage(damage);
        }
    }
}
