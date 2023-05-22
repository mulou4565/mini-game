using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class UseMachine : MonoBehaviour
{
    public Machine[] machine;
    public bool isTrigger;
    Collider2D colliders;
    public bool onlyone;
    public bool isStay;
    public Animator animators;
    private void Awake()
    {
        colliders = GetComponent<BoxCollider2D>();
        if (isTrigger == false) { colliders.enabled = false; }
    }
    public void use()
    {
        if (animators != null) { animators.SetTrigger("change"); }
        foreach (var item in machine)
        {
            item.Run();
            
        }
        if (onlyone){Destroy(this.gameObject);}

    }
    private void Update()
    {
        if (isStay && Input.GetKeyDown(KeyCode.E)) { use();  }
      // if (Input.GetKeyDown(KeyCode.L)) { use(); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isStay = false;
        }
    }
}
