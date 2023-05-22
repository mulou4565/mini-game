using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UseMachine))]
public class RotationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public UseMachine use;
    public MachineRotationMove rotationMove;
    public GameObject[] gmShow;
    public GameObject[] gmHide;
    public bool canRun;
    private void Awake()
    {
        canRun = true;
        use = GetComponent<UseMachine>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player")) { rotationMove.SetPos(this.transform.position); use.use(); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player") && canRun==true) 
        {
            canRun = false;
            rotationMove.SetPos(this.transform.position);use.use();
            foreach (var item in gmShow)
            {
                item.SetActive(true);
            }
            foreach (var item in gmHide)
            {
                item.SetActive(false);
            }
            Destroy(this.gameObject);
        
        }
    }
  
}
