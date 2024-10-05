using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerPos;
    private Vector3 posGap;

    private void Start()
    {
        posGap = transform.position - playerPos.position;
    }
    private void LateUpdate()
    {
        transform.position = playerPos.position + posGap;
    }
}
