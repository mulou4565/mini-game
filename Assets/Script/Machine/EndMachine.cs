using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMachine : MonoBehaviour
{
    public bool putletter;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && putletter) 
        {
           GameManager.gamemanager.canEnd = true; 
        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.gamemanager.canEnd = false;

        }
    }
}
