using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public float enemyHealth;
    public bool isPlayerSeen = false;

  
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject, 5);
            GetComponent<Animator>().SetBool("Dead", true);
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (isPlayerSeen)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            float attackDistanceThreshold = 2f;

            if (distanceToPlayer <= attackDistanceThreshold)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<Animator>().SetBool("Attack", true);
            }
            else
            {
                GetComponent<NavMeshAgent>().isStopped = false;
                GetComponent<Animator>().SetBool("Attack", false);
            }
        }
        else
        {
            
        }
    }


    public void Hit(float damage)
    {
        enemyHealth -= damage;
    }
}
