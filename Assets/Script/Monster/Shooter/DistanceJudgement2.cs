using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceJudgement2 : Monster
{ 
    [Tooltip("怪物朝向，大于0为右，小于0为左")]
    public float Face;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Face = collision.transform.position.x - transform.GetComponentInParent<Transform>().position.x;
            transform.GetComponentInParent<ShooterMovement>().Attack();
   
        }
       
    }
    
}
