using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshLogic : MonoBehaviour
{
    enum State { Idle, Chase, Attack }

    public GameObject player;
    public float chaseDistance = 100f;
    public float attackDistance = 2f;
    public int damage = 10;
    public float attackCooldown = 0.5f;


    private NavMeshAgent agent;
    private float nextAttackTime = 0f;
    private State state = State.Idle;

    private bool targetHit = false;

    private float fallDistance = 90f;

    private float fallSpeed = 150f;

    private float fallingDown = 0f;

    private Material material;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<Renderer>().material;
        InvokeRepeating(nameof(Wander), 0, 0.5f);
    }

    void Update()
    {
        if (targetHit)
        {
            ZombieDeath();
            return;
        }

        if (player == null || agent == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackDistance)
            state = State.Attack;
        else if (distance <= chaseDistance)
            state = State.Chase;
        else
            state = State.Idle;

        if (state == State.Chase)
            agent.SetDestination(player.transform.position);

        if (state == State.Attack)
            Attack();
    }

    void Wander()
    {
        if (state != State.Idle || targetHit || agent == null) return;

        Vector3 randomDir = Random.insideUnitSphere * 15f;
        randomDir += transform.position;

        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void Attack()
    {
        if (agent == null || player == null){
            return; 
        }

        Debug.Log("zombien følger etter deg");

        agent.ResetPath();

        if (Time.time >= nextAttackTime)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= attackDistance)
            {
                Debug.Log("zombien gir damage");
                Health hp = player.GetComponent<Health>();
                if (hp != null)
                {
                    hp.TakeDamage(damage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet") && !targetHit)
        {
            Destroy(col.gameObject);
            targetHit = true;

            if (agent != null){
                agent.enabled = false;
            }

            ScoreManager.Instance?.AddScore(1);
            
            CancelInvoke(nameof(Wander));
            Destroy(gameObject, 0.5f); //dør når skuddet treffer
        }
    }

    void ZombieDeath()
    {
        if (fallingDown < fallDistance)
        {
            float rotateAmount = fallSpeed * Time.deltaTime;
            transform.Rotate(transform.forward * rotateAmount);
            fallingDown += rotateAmount; 
        }
    }
}
