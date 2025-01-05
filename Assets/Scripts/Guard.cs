using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public class Guard : MonoBehaviour
{
    public bool playerInSight;
    public float viewAngle = 120f;
    public Vector3 lastSighting;
    public Vector3 previousSighting;
    private SphereCollider col;
    public float deadZone;
    public Animator animator;
    public NavMeshAgent ai;
    public Vector3[] points;
    public Light light;
    public Vector3[] patrolPoints; // 巡逻点数组
    private int currentPatrolIndex = 0; // 当前巡逻点索引
    private bool patrollingForward = true; // 是否正向巡逻

    public float patrolWaitTime = 2f; // 到达巡逻点后等待时间
    private float patrolWaitTimer; // 等待计时器

    public float shootWaitTime = 3f;
    private float shootWaitTimer;
    public Transform shootPoint;
    public LineRenderer lineRenderer;
    private Coroutine coroutine;
    private void Update()
    {
        if (playerInSight)
        {
            // 玩家在视野内，追踪玩家
            if (coroutine == null)
            {
                ai.speed = 3.5f;

                animator.SetFloat("Speed", ai.velocity.magnitude);
            }
            ai.SetDestination(PlayerPositionManager.Instance.GetPlayerPositon());
        

        }
        else
        {
            ai.speed = 1.5f;
            animator.SetFloat("Speed", 1.5f);
            Patrol(); // 执行巡逻逻辑
        }

    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
       //ai.updateRotation = false;
        animator.SetLayerWeight(1, 1f);
        animator.SetLayerWeight(2, 1f);
        col = GetComponent<SphereCollider>();
        print(PlayerPositionManager.Instance.GetPlayerPositon());
        lineRenderer.gameObject.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag!="Player") return;
         playerInSight = false;
       
        Vector3 dir= PlayerPositionManager.Instance.GetPlayerPositon() - transform.position;
        dir.y = 0;
        float angle = Vector3.Angle(dir, transform.forward);
     
        //print(angle);
        if (angle < viewAngle * 0.5f) { 
        RaycastHit hit;
            Debug.DrawLine(transform.position + transform.up, transform.position + transform.up + (dir.normalized)*2, Color.red);
            if (Physics.Raycast(transform.position + transform.up , dir.normalized, out hit, col.radius)) {
                print("Check");
                print(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player") {
                    print("See");
                playerInSight=true;
                    GuardManager.Instance.FoundPlayer();
                lastSighting=PlayerPositionManager.Instance.GetPlayerPositon();
                    if(coroutine==null)
                    coroutine = StartCoroutine(StartShoot(lineRenderer));
                    
                }
            }
        }
        if (!PlayerPositionManager.Instance.isQuiet) {
            if (GetPathLength(PlayerPositionManager.Instance.GetPlayerPositon()) <= col.radius) {
                lastSighting = PlayerPositionManager.Instance.GetPlayerPositon();
                playerInSight = true;
               
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSight = false;
            animator.SetBool("PlayerInSight", false);
        }

    }
    float GetPathLength(Vector3 target) { 
    NavMeshPath path=new NavMeshPath();
        if (ai.enabled) {
            ai.CalculatePath(target, path);
        }
        Vector3[] allPoints = new Vector3[path.corners.Length + 2];
        allPoints[0] = transform.position;
        allPoints[allPoints.Length-1]=target;
        for (int i = 0; i < path.corners.Length; i++) {
            allPoints[i + 1] = path.corners[i];
        }
        float pathLength = 0;
        for (int i = 0; i < allPoints.Length-1; i++) {
            pathLength += Vector3.Distance(allPoints[i],allPoints[i+1]);
        }
        return pathLength;
    }
    private void Patrol()
    {
        if (ai.remainingDistance <= ai.stoppingDistance && !ai.pathPending)
        {
            // 巡逻点到达后等待
            patrolWaitTimer += Time.deltaTime;
            animator.SetFloat("Speed", 0f);
            if (patrolWaitTimer >= patrolWaitTime)
            {
                // 等待结束后移动到下一个巡逻点
                patrolWaitTimer = 0f;

                if (patrollingForward)
                {
                    currentPatrolIndex++;
                    if (currentPatrolIndex >= patrolPoints.Length)
                    {
                        currentPatrolIndex = currentPatrolIndex%patrolPoints.Length;
                        patrollingForward = false;
                    }
                }
                else
                {
                    currentPatrolIndex--;
                    if (currentPatrolIndex < 0)
                    {
                        currentPatrolIndex = 0;
                        patrollingForward = true;
                    }
                }
                Debug.Log("Index:" + currentPatrolIndex + "  Points:" + patrolPoints[currentPatrolIndex]);
                ai.SetDestination(patrolPoints[currentPatrolIndex]);
            }
        }
    }

  
    IEnumerator StartShoot(LineRenderer lineRenderer) {
    
        while (true)
        {
            animator.SetBool("PlayerInSight", true);
            ai.speed = 0.8f;
            animator.SetFloat("Speed", 1f);
            shootWaitTimer += Time.deltaTime;
            if (shootWaitTimer >= shootWaitTime)
            {
                lineRenderer.gameObject.SetActive(true);
                shootWaitTimer = 0;
                yield return new WaitForSeconds(0.9f);
                lineRenderer.positionCount = 2;
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.025f;
                lineRenderer.SetPosition(0, shootPoint.position);
                lineRenderer.SetPosition(1, new Vector3(PlayerPositionManager.Instance.GetPlayerPositon().x, shootPoint.position.y, PlayerPositionManager.Instance.GetPlayerPositon().z));
                light.intensity = 2f;
                light.DOIntensity(0, 0.3f);
                StartCoroutine(LineFade());

            }
        }
       
    }
    IEnumerator LineFade() {
        yield return new WaitForSeconds(0.3f);

        lineRenderer.gameObject.SetActive(false);
    }
}
