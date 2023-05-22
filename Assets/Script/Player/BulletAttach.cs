using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttach : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(1);
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Trigger")) { collision.gameObject.GetComponent<UseMachine>().use(); }
        if (collision.gameObject.CompareTag("Monster")) { collision.gameObject.GetComponent<Monster>().Hurt(1); }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("other") && 
            !collision.gameObject.CompareTag("TriggerRightNow")
            ) Destroy(this.gameObject);


    }
}
