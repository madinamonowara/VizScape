using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMovement : MonoBehaviour
{

    public Vector3 walkPoint;
    bool setWalkPoint;
    public float walkRange;

    public NavMeshAgent agent;
    public LayerMask findGround;
    //public Transform enemyObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("enemyMove", 1f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void findWalkRange(){
        float walkRangeX = Random.Range(-walkRange, walkRange);
        float walkRangeZ = Random.Range(-walkRange, walkRange);
        walkPoint = new Vector3(transform.position.x + walkRangeX, transform.position.y, transform.position.z + walkRangeZ);
        if (Physics.Raycast(walkPoint, transform.up, 2f, findGround)){
            setWalkPoint = true;
        }
        
    }

    private void enemyMove(){
        if(setWalkPoint == false){
            findWalkRange();
        }
            agent.SetDestination(walkPoint);


        Vector3 distanceToPoint = transform.position - walkPoint;
        if(distanceToPoint.magnitude < 1f){
            setWalkPoint = true;
        }
    }
}
