using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCheck : MonoBehaviour
{
    [Header("UI Elements")]
    public UnityEngine.UI.InputField inputField; // �����
    public UnityEngine.UI.Button button0;        // ��ť��0��
    public UnityEngine.UI.Button button1;        // ��ť��1��
    public UnityEngine.UI.Button confirmButton;  // ȷ����ť
    public Animator door;

    [Header("Validation Settings")]
    public string correctValue = "10101"; // ��ȷ�Ķ�����ֵ

    void Start()
    {
        // ��������
        inputField.text = "";

        // �󶨰�ť����¼�
        button0.onClick.AddListener(() => AppendValue("0"));
        button1.onClick.AddListener(() => AppendValue("1"));
        confirmButton.onClick.AddListener(ValidateInput);
    }

    /// <summary>
    /// ���������׷��ֵ
    /// </summary>
    /// <param name="value">׷�ӵ�ֵ��"0" �� "1"��</param>
    void AppendValue(string value)
    {
        inputField.text += value;
    }

    /// <summary>
    /// ��֤������е�ֵ�Ƿ���ȷ
    /// </summary>
    void ValidateInput()
    {
        if (inputField.text == correctValue)
        {
            door.SetTrigger("DoorOpen");
            SoundManager.Instance.PlayOneShot(1, 0.3f);
        }
        else
        {
            
        }
    }
}
