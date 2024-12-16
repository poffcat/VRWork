using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public Vector3[] patrolPoints; // Ѳ�ߵ�����
    private int currentPatrolIndex = 0; // ��ǰѲ�ߵ�����
    private bool patrollingForward = true; // �Ƿ�����Ѳ��

    public float patrolWaitTime = 2f; // ����Ѳ�ߵ��ȴ�ʱ��
    private float patrolWaitTimer; // �ȴ���ʱ��
    private void Update()
    {
        if (playerInSight)
        {
            // �������Ұ�ڣ�׷�����
            ai.speed = 3.5f;
            animator.SetFloat("Speed", 4f);
            ai.SetDestination(PlayerPositionManager.Instance.GetPlayerPositon());
            animator.SetFloat("Speed", ai.velocity.magnitude);
        }
        else
        {
            ai.speed = 1.5f;
            animator.SetFloat("Speed", 1.5f);
            Patrol(); // ִ��Ѳ���߼�
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
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag!="Player") return;
         playerInSight = false;
        Vector3 dir=other.transform.position-transform.position;
        float angle = Vector3.Angle(dir, transform.forward);
        if (angle < viewAngle * 0.5f) { 
        RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up , dir.normalized, out hit, col.radius)) {
                if (hit.collider.gameObject.tag == "Player") { 
                playerInSight=true;
                    GuardManager.Instance.FoundPlayer();
                lastSighting=PlayerPositionManager.Instance.GetPlayerPositon();
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
        if(other.gameObject.tag=="Player") playerInSight=false;
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
            // Ѳ�ߵ㵽���ȴ�
            patrolWaitTimer += Time.deltaTime;
            animator.SetFloat("Speed", 0f);
            if (patrolWaitTimer >= patrolWaitTime)
            {
                // �ȴ��������ƶ�����һ��Ѳ�ߵ�
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



}
