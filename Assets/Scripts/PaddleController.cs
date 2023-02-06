using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 7f;
    float increaseBallFactor = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PaddleMovement(gameObject.tag);
    }

    private void PaddleMovement(string tagName)
    {
        if (Input.GetButton(tagName))
        {
            Vector3 movementInput = new Vector3(0, Input.GetAxis(tagName), 0);
            rb.MovePosition(transform.position + movementInput * Time.deltaTime * speed);
        }        
    }

}
