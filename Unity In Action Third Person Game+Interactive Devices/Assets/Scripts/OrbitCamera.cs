using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 1.5f;

    private float rotY;
    private Vector3 offset;
    private Vector3 ogOffset;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rotY = transform.eulerAngles.y;
        ogOffset = target.position - transform.position;
        offset = ogOffset;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        rotY += Input.GetAxis("Mouse X") * rotSpeed;

        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);

        if (Physics.SphereCast(transform.position, 0.5f, target.position, out hit, 0.5f))
        {
            offset = new Vector3(Vector3.distance)
        }
    }
}
