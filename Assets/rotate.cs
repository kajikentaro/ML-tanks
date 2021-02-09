using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public Transform bottom;
    public Vector3 _center;
    public Vector3 _axis = Vector3.up;
    public float _period = 2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _center = bottom.position;
        transform.RotateAround(
            _center,
            _axis,
            360 / _period * Time.deltaTime
        );
    }
}
