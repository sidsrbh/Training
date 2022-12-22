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
    [SerializeField] GameObject player;
    public float within_range;
    public float speed;

    private Rigidbody rb;
    bool alive => health > 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {

        }
    }
    public void GetDamage(float damage)
    {
        health -= damage;


        if (health <= 0)
        {
            //this.gameObject.GetComponent<Renderer>().material.color = Color.green;
           
            //  Destroy(this.gameObject);
            Die();
            // Destroy(this.gameObject);
            // Create a new cube primitive to set the color on
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //// Get the Renderer component from the new cube
            //var cubeRenderer = cube.GetComponent<Renderer>();

            //// Call SetColor using the shader property name "_Color" and setting the color to red
            //cubeRenderer.material.SetColor("_Color", Color.red);

        }
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
      //  _navMeshAgent.enabled = false;
    }
    void Update()
    {
        //if (!alive)
        //    return;
        ////  var player = FindObjectOfType<Movement>();
        ////if (_navMeshAgent.enabled)
        ////    _navMeshAgent.SetDestination(player.transform.position);

        ////if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
        ////{
        ////    Attack();
        ////}
        //float dist = Vector3.Distance(player.transform.position, transform.position);

        ////check if it is within the range you set
        //if (dist <= within_range)
        //{
        //    //move to target(player) 
        //    Vector3 position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //    rb.MovePosition(position);
        //    transform.LookAt(player.transform);
        //    float speeda = 2f;

        //    //Vector3 dir = Vector3.Normalize( transform.position - player.transform.position);
        //    //  transform.rotation = Quaternion.LookRotation(dir);

        //  //  transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, Time.deltaTime);
        //    //transform.position += Vector3.forward * speeda * Time.deltaTime;

        //}
        //if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
        //{
        //    Attack();
        //}

        if (!alive)
            return;
        //  var player = FindObjectOfType<Movement>();
        //if (_navMeshAgent.enabled)
        //    _navMeshAgent.SetDestination(player.transform.position);



        //if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
        //{
        //    Attack();
        //}
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
      //  _navMeshAgent.enabled = false;
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        if (health == 0)
        {
            GameMangerr.instance.updateScore(score);
           
        }
            // _navMeshAgent.enabled = false;
            _animator.SetTrigger("Died");
      //  Died?.Invoke(this);
        Destroy(gameObject, 5f);
       
    }
}
