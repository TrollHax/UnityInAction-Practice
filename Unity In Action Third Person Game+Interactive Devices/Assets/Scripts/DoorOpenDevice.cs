using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] Vector3 dPos;

    private bool open;
    private Vector3 ogPos;

    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Operate()
    {
        if (open)
        {
            Vector3 pos = transform.position;
            pos.y = ogPos.y;
            transform.DOMove(pos, 1);
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y = ogPos.y + dPos.y;
            transform.DOMove(pos, 1);
        }
        open = !open;
    }

    public void Activate()
    {
        if (!open)
        {
            Vector3 pos = transform.position;
            pos.y = ogPos.y + dPos.y;
            transform.DOMove(pos, 1);
            open = true;
        }
    }

    public void Deactivate()
    {
        if (open)
        {
            Vector3 pos = transform.position;
            pos.y = ogPos.y;
            transform.DOMove(pos, 1);
            open = false;
        }
    }
}
