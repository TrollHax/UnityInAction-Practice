using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
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

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2((max.x - .01f), min.y - .1f);
        Vector2 corner2 = new Vector2((min.x + .01f), min.y - .2f);
        Vector2 boxCorner1 = new Vector2(max.x, max.y);
        Vector2 boxCorner2 = new Vector2(min.x, min.y);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        Collider2D phasing = Physics2D.OverlapArea(boxCorner1, boxCorner2, 0);

        Debug.Log(phasing);

        bool grounded = false;
        if (hit != null && phasing == null)
        {
            grounded= true;
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
    }
}
