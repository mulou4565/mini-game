using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public EndMachine end;
    public GameObject letter;
    public bool canPut;
    public bool havePut;
    public bool needStop;
    public bool isend;
    public Move move;
    GameObject ends;
    private void Awake()
    {
         if(end!=null)ends = end.gameObject;
    }
    private void Update()
    {   if (move!=null &&  letter.activeInHierarchy == false) { move.canMove = true; }
        if (havePut && end == null && letter.activeInHierarchy == false && isend==false) { GameManager.gamemanager.End(); isend = true; }
        if (havePut) { return; }
        
        if (canPut && letter.activeInHierarchy==false && Input.GetKeyDown(KeyCode.E)) 
        {
            letter.SetActive(true);havePut = true;
            if (end != null)
            {
                ends.SetActive(true); end.putletter = true;
            }
            if (needStop && move!=null) { move.canMove = false;move.rb.velocity = new Vector2(0, 0);move.isMove = false; }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { canPut = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { canPut = false; }
    }

    // Update is called once per frame

}
