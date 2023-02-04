using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{

    public int MaxHP = 1;

    public int CurrentHP = 1;
    
    public float speed;
    public float attackRange;
    Transform targetDestination;
    public Animator animator;
    Rigidbody2D body;
    public GameObject bulletPrefab;
    public Vector2 bulletSpawnOffset;
    public float bulletVelocity = 2.0f;
    public float weaponCooldownDuration = 0.5f;
    public int damage = 1;
    
    private bool isWeaponOnCooldown;
    // Start is called before the first frame update
    void Start()
    {
        targetDestination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsInRange())
        {
            if (!isWeaponOnCooldown)
            {
                Shoot();
            }
        }
        else
        {
            Move();
        }
        
        
    }

    private IEnumerator Shoot()
    {
        var directionVector = (targetDestination.position - transform.position).normalized;
        var bullet = Instantiate(bulletPrefab, 
            transform.position.ToVector2() + 
            (bulletSpawnOffset * directionVector), Quaternion.identity);
        var bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.velocity = directionVector * bulletVelocity;
        isWeaponOnCooldown = true;
        yield return new WaitForSeconds(weaponCooldownDuration);
        isWeaponOnCooldown = false;

    }

    private void Move()
    {
        var directionVector = (targetDestination.position - transform.position).normalized;
        var facingDirection = Utils.GetHorizontalDirection(directionVector);
        var scale = transform.localScale;
        if (facingDirection == Utils.Direction.Left)
            transform.localScale = new Vector3(-1 * MathF.Abs(scale.x), 
                scale.y, scale.z);
        else
            transform.localScale = new Vector3(MathF.Abs(scale.x), 
                scale.y, scale.z);
        
        body.velocity = directionVector * speed;
    }
    
    private bool IsInRange()
    {
        var distance = Vector3.Distance(transform.position, targetDestination.position);
        return distance <= attackRange;
    }
}