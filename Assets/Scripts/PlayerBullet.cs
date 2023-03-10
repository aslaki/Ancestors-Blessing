using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float lifeTimeSec = 5f;
    public int damage = 1;
    private Timer _lifeTimer = new Timer();
    void Start(){
        _lifeTimer.Start(lifeTimeSec);
    }

    void Update()
    {
        _lifeTimer.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(_lifeTimer.HasElapsed)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.GetComponent<MeleeEnemy>()){
                other.gameObject.GetComponent<MeleeEnemy>().TakeDamage(damage);
            } else if (other.gameObject.GetComponent<EnemyDrone>()){
                other.gameObject.GetComponent<EnemyDrone>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
