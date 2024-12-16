using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float highlightSpeed = 0.5f; // �����ٶ�

    private Material cardMaterial;
    private Color originalColor;
    private float originalSmoothness;

    void OnEnable()
    {
        // ��ȡ����
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            cardMaterial = renderer.material;

            // ȷ������֧��͸��
            SetMaterialToTransparent();

            originalColor = cardMaterial.color;
            originalSmoothness = cardMaterial.HasProperty("_Glossiness") ? cardMaterial.GetFloat("_Glossiness") : 0f;
        }
        else
        {
            Debug.LogError("�Ҳ��� Renderer����ȷ����Ƭ�в��ʣ�");
        }
    }

    void Update()
    {
        if (cardMaterial != null)
        {
            // ʹ�� PingPong ��̬��������ͷ���Ч��
            float gloss = Mathf.PingPong(Time.time * highlightSpeed, 1);
            cardMaterial.SetFloat("_Glossiness", Mathf.Lerp(originalSmoothness, 1f, gloss)); // �߹�仯
            cardMaterial.SetColor("_EmissionColor", Color.white * gloss); // ����Ч��

            // ��̬����͸����
            Color currentColor = cardMaterial.color;
            currentColor.a = Mathf.Lerp(0.5f, 1f, gloss); // ͸������ 0.5 �� 1 ֮�䲨��
            cardMaterial.color = currentColor;
        }
    }
    
    /// <summary>
    /// ����������Ϊ͸��ģʽ
    /// </summary>
    private void SetMaterialToTransparent()
    {
        cardMaterial.SetFloat("_Mode", 3); // ����Ϊ Transparent ģʽ
        cardMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        cardMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        cardMaterial.SetInt("_ZWrite", 0);
        cardMaterial.DisableKeyword("_ALPHATEST_ON");
        cardMaterial.EnableKeyword("_ALPHABLEND_ON");
        cardMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        cardMaterial.renderQueue = 3000; // ������Ⱦ����Ϊ͸������
    }
    public void ResetToDefault()
    {
        if (cardMaterial != null)
        {
            // �ָ���ɫ��͸����
            Color resetColor = originalColor;
            resetColor.a = 1f; // ����Ϊ��ȫ��͸��
            cardMaterial.color = resetColor;

            // �ָ������
            if (cardMaterial.HasProperty("_Glossiness"))
            {
                cardMaterial.SetFloat("_Glossiness", originalSmoothness);
            }

            // �رշ���Ч��
            cardMaterial.SetColor("_EmissionColor", Color.black);
        }
    }
}
