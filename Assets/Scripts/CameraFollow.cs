using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private Transform target;

    private void Start()
    {
        this.GetComponent<Camera>().orthographicSize = 6;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
