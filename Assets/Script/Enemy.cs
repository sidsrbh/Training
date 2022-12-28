using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InfimaGames.LowPolyShooterPack;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public int score = 20;
    

    NavMeshAgent _navMeshAgent;
    private Animator _animator;

    [SerializeField] float _attackRange = 1f;
    GameObject player;
    public float within_range;
    public float speed;

    private Rigidbody rb;
    bool alive => health > 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            GetDamage(10);
        }
    }
    public void GetDamage(float damage)
    {
        health -= damage;


        if (health <= 0)
        {
            
            Die();
           

        }
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
      
    }
    void Update()
    {
       
        if (!alive)
            return;
       

        player =  GameObject.FindGameObjectWithTag("Player");
        float dist = Vector3.Distance(player.transform.position, transform.position);
        transform.LookAt(transform.position - player.transform.position);
        //check if it is within the range you set
        if (dist <= within_range)
        {
            //move to target(player) 
            // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            float speeda = 2f;



            //Vector3 dir = Vector3.Normalize( transform.position - player.transform.position);
            //  transform.rotation = Quaternion.LookRotation(dir);
            transform.LookAt(player.transform);
            //  transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, Time.deltaTime);
            transform.position += transform.forward * speeda * Time.deltaTime;

        }
        if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
        {
            Attack();
        }

    }
    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        if (health == 0)
        {
            GameMangerr.instance.updateScore(score);
           
        }
           
        _animator.SetTrigger("Died");
        Destroy(gameObject, 5f);
       
    }
}
