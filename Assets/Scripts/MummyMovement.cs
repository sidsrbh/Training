using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MummyMovement : MonoBehaviour
{
    public static event Action<MummyMovement> Died;
    [SerializeField] float _attackRange = 1f;
    [SerializeField] int _health = 2;

    int _currentHealth;
    NavMeshAgent _navMeshAgent;
    private Animator _animator;

    bool alive => _currentHealth > 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        _currentHealth = _health;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.enabled = false;
    }

    public  void StartWalking()
    {
        _navMeshAgent.enabled = true; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        var blasterShot = collision.collider.GetComponent<BlasterShot>();
        if (blasterShot != null)
        {
            _currentHealth--;
            if (_currentHealth <= 0)
                Die();
            else
                TakeHit();
        }
    }

    private void TakeHit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Hit");
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Died");
        Died?.Invoke(this);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
            return;
        var player = FindObjectOfType<PlayerMovement>();
        if(_navMeshAgent.enabled)
          _navMeshAgent.SetDestination(player.transform.position);

        if(Vector3.Distance(transform.position, player.transform.position) < _attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        _navMeshAgent.enabled = false;
    }

    // Animation Calback
    void AttackComplete()
    {
        if(alive)
           _navMeshAgent.enabled = true;

    }

    // Animation Calback
    void AttackHit()
    {
        Debug.Log("Killed Player");
        SceneManager.LoadScene(0);
    }

    void HitComplete()
    {
        if (alive)
            _navMeshAgent.enabled = true;
    }
}
