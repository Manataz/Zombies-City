using UnityEngine;
using UnityEngine.AI;


public class TriggerCollidersScript : MonoBehaviour
{

    public GameObject EnemyManager;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyManager.GetComponent<EnemyManager>().isPlayerSeen = true;
            EnemyManager.GetComponent<Animator>().SetBool("Run", true);
            EnemyManager.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyManager.GetComponent<EnemyManager>().isPlayerSeen = false;
            EnemyManager.GetComponent<Animator>().SetBool("Run", false);
            EnemyManager.GetComponent<NavMeshAgent>().enabled = false;

        }
    }
   
}
