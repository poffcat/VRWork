using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaFollowHand : MonoBehaviour
{
  
    public Transform rightHand; // ���ֿ����������ֵ� Transform
    public Vector3 offset = new Vector3(0.1f, 0f, 0.1f); // ��������ֵ�ƫ��
    public Vector3 rotationOffset = new Vector3(0f, 0f, 0f); // ��תƫ��

    void Update()
    {
        if (rightHand != null)
        {
            // ���� Canvas ��λ��
            transform.position = rightHand.position + rightHand.TransformDirection(offset);

            // ���� Canvas ����ת
            transform.rotation = rightHand.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
