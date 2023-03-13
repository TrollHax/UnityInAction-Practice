using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] Vector3 dPos;

    private bool open, inProgress;
    private float cooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        inProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cooldown);
        if (inProgress)
        {
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                cooldown = 1f;
                inProgress = false;
            }
        }
    }

    public void Operate()
    {
        if (!inProgress)
        {
            inProgress = true;
            if (open)
            {
                Vector3 pos = transform.position - dPos;
                transform.DOMove(pos, 1);
            }
            else
            {
                Vector3 pos = transform.position + dPos;
                transform.DOMove(pos, 1);
            }
            open = !open;
        }
    }
}
