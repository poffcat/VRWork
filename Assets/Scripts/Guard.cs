using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public bool playerInSight;
    public float AngularSpeed;
    public float angularDampTime = 0.7f;
    public float speedDampTime = 0.1f;
    public float viewAngle = 120f;
    public Vector3 lastSighting;
    public Vector3 previousSighting;
    private SphereCollider col;
    public float deadZone;
    public Animator animator;
    public NavMeshAgent ai;
    public Vector3[] points;

    private void Update()
    {
     NavAnimSetup();
      
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
       ai.updateRotation = false;
        animator.SetLayerWeight(1, 1f);
        animator.SetLayerWeight(2, 1f);
        col = GetComponent<SphereCollider>();
        print(PlayerPositionManager.Instance.GetPlayerPositon());
        ai.SetDestination(PlayerPositionManager.Instance.GetPlayerPositon());
        animator.SetFloat("Speed", 1f);
    }

    private void OnAnimatorMove()
    {
        ai.velocity = animator.deltaPosition / Time.deltaTime;
        transform.rotation = animator.rootRotation;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag!="Player") return;
         playerInSight = false;
        Vector3 dir=other.transform.position-transform.position;
        float angle = Vector3.Angle(dir, transform.forward);
        if (angle < viewAngle * 0.5f) { 
        RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * 2, dir.normalized, out hit, col.radius)) {
                if (hit.collider.gameObject.tag == "Player") { 
                playerInSight=true;
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
    void SetSpeed(float speed,float angle) {
       // float angularSpeed = angle / (angularDampTime - 0.1f);
        float angularSpeed = Mathf.LerpAngle(0f, angle, Time.deltaTime / angularDampTime); // 平滑角度变化
        animator.SetFloat("Speed", speed,speedDampTime,Time.deltaTime);
        animator.SetFloat("AngularSpeed", angularSpeed,angularDampTime,Time.deltaTime);
    }
    float FindAngle(Vector3 fromVector,Vector3 toVector,Vector3 upVector) {
        if (toVector == Vector3.zero) {
            return 0f;
        }
        float angle=Vector3.Angle(fromVector, toVector);
        Vector3 normal=Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle = Mathf.Clamp(angle, -180f, 180f);
        angle += Mathf.Rad2Deg;
        return angle;
    }
    void NavAnimSetup() {
        float speed;
        float angle;
        if (playerInSight) { 
        speed = 0f;
        angle = FindAngle(transform.forward, PlayerPositionManager.Instance.GetPlayerPositon() - transform.position, Vector3.up);
        }
        else
        {
            speed = Vector3.Project(ai.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, ai.desiredVelocity, transform.up);
            if (Mathf.Abs(angle) < deadZone) {
                transform.LookAt(transform.position + ai.desiredVelocity);
                angle = 0f;
            }
        }
        SetSpeed(speed, angle);
    }
}
