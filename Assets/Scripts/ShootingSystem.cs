using UnityEngine;

public class ShootingSystem : MonoBehaviour
{

    public GameObject playerCam;
    public GameObject bulletTrail;
    public float range = 100f;
    public float damage = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            bulletTrail.SetActive(true);
            Invoke("DeActiveBulletTrail",0.05f);
            Shoot() ;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        int layerMask = ~LayerMask.GetMask("TriggerColliders"); // Exclude trigger colliders from raycast
        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range, layerMask))
        {
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.Hit(damage);
            }
        }
    }

    private void DeActiveBulletTrail()
    {
        bulletTrail.SetActive(false);
    }
}
