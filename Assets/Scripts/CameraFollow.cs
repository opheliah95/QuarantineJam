using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // called last
    void LateUpdate()
    {
        // temp camera position
        Vector3 cameraPos = transform.position;
        cameraPos.x = playerTransform.position.x + offset;
        cameraPos.y = playerTransform.position.y;

        transform.position = cameraPos;

    }
}
