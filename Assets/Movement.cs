using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementHorizontal;
    [SerializeField] float movementVertical;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 15;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] GameObject pinPrefab; 

    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (pinPrefab == null)
            pinPrefab = GameObject.FindGameObjectWithTag("Pin");
    }

    void Update()
    {
        movementHorizontal = Input.GetAxis("Horizontal");
        movementVertical = Input.GetAxis("Vertical");

        // Check for shooting input
        if (Input.GetButtonDown("Fire1"))
        {
            ShootPin();
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(SPEED * movementHorizontal, SPEED * movementVertical);
        if (movementHorizontal < 0 && isFacingRight || movementHorizontal > 0 && !isFacingRight)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    void ShootPin()
    {
        GameObject pinInstance = Instantiate(pinPrefab, transform.position, Quaternion.identity);
        pinInstance.AddComponent<PinMovement>();
    }
}
