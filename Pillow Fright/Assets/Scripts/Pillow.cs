using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    public int n1Damage = 10;
    public int n2Damage = 10;
    public int n3Damage = 20;
    public int aerialDamage = 10;

    PlayerControls player;
        
    void Start()
    {
        player = GetComponentInParent<PlayerControls>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (player.getAttackNum() == 1)     //Neutral 1
            {
                enemy.damage(n1Damage);
                Debug.Log("Enemy hit with Neutral 1 Attack");
            }
            if (player.getAttackNum() == 2)     //Neutral 2
            {
                enemy.damage(n2Damage);
                Debug.Log("Enemy hit with Neutral 2 Attack");
            }
            if (player.getAttackNum() == 3)     //Neutral 3
            {
                enemy.damage(n3Damage);
                Debug.Log("Enemy hit with Neutral 3 Attack");
            }
            if (player.getAttackNum() == -1)    //Aerial
            {
                enemy.damage(aerialDamage);
                Debug.Log("Enemy hit with Aerial Attack");
            }
        }
    }

}
