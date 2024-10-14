using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpriteActivationEvent : MonoBehaviour
{
    [SerializeField] private Image buttonImage;             // ��������� Image ��� ������������ ��������� ������� �� ������
    [SerializeField] private Sprite targetSprite;           // ������, ��� ������� ������ ����� �����������
    [SerializeField] private GameObject targetObject;       // ������, ������� ����� �����������
    [SerializeField] private UnityEvent onSpriteMatched;    // �������, ������� ����� ������� ��� ���������� �������

    private void Start()
    {
        // ��������, ��� ������ ����������� ���������
        if (buttonImage == null || targetSprite == null || targetObject == null)
        {
            Debug.LogError("�� ��� ������ �����������.");
            return;
        }

        // ������������ ������ �� ���������
        targetObject.SetActive(false);
    }

    private void Update()
    {
        // ���������, ��������� �� ������� ������ � ������� ��������
        if (buttonImage.sprite == targetSprite)
        {
            // ���������� ������ � �������� �������
            targetObject.SetActive(true);
            onSpriteMatched?.Invoke();
        }
    }
}
