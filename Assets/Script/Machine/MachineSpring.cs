using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSpring : Machine
{

    public float force;
    public float maxSpeed;

    public override void Run()
    {
        base.Run();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log(rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < maxSpeed) { Debug.Log(1);  rb.AddForce(Vector2.up * force); }
            if (Mathf.Abs(rb.velocity.y) > maxSpeed) { collision.gameObject.GetComponent<Move>().SetRby(maxSpeed); }  
        }


    }


}
