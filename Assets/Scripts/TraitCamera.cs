using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitCamera : MonoBehaviour
{
    
    public Transform target;
    private Vector3 offset;


    private void Start()
    {
        offset = transform.position - target.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = newPosition;
    }
}