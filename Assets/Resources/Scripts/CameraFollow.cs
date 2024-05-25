using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    public float smoothTime;
    Vector3 velocity = Vector3.zero;
    Vector3 offset = new Vector3(0, 0, -10f);

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Not found player");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, smoothTime);

    }

}
