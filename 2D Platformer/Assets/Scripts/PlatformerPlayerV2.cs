using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerV2 : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        Vector2 pPos = new Vector2(body.position.x, body.position.y - .3f);
        RaycastHit2D hit = Physics2D.Raycast(pPos, -Vector2.up, .1f, 1, 0, 0);

        bool grounded = false;
        if (hit)
        {
            grounded = true;
        }

        //body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;
        if (hit)
        {
            platform = GetComponent<MovingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        anim.SetFloat("speed", Mathf.Abs(deltaX));

        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(
            Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
    }
}
