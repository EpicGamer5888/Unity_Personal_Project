using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public PlayerController player;
    public NavMeshAgent agent;
    public Transform target;

    public GameObject door;

    [Header("Enemy Stats")]
    public int health = 100;
    public int maxHealth = 100;
    public int damageGiven = 5;
    public float pushBackForce = 20;
    public float distanceDetection = 50;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player").transform;


        agent.destination = target.position;


        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            health -= player.damage;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player" && !player.takenDamage)
        {
            player.takenDamage = true;
            player.health -= damageGiven;
            player.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * pushBackForce);
            player.StartCoroutine("cooldownDamage");
        }
    }

    private void OnDestroy()
    {
        Destroy(door);
    }
}
