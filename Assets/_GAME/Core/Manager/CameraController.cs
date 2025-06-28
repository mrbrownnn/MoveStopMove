using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Vector3 positionOffset=new Vector3(20f,15f,0f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + positionOffset;
        transform.LookAt(player.position);
    }
}
