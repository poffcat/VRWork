using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithDamping : MonoBehaviour
{
    public Transform target; // VR������ӽǵ����ĵ�
    public float positionDamping = 5f; // ����ϵ����λ�ã�
    public float rotationDamping = 5f; // ����ϵ������ת��

    void Update()
    {
        if (target == null) return;

        // ƽ���ظ���λ��
        Vector3 targetPosition = target.position + target.forward * 2f + Vector3.up * 0.5f; // �������λ��
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionDamping);

        // ƽ���ظ�����ת
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
    }
}
