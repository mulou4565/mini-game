using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShow : MonoBehaviour
{
    public GameObject show;
    public bool isstay;
    public GameObject someUI;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { if(show!=null) show.SetActive(true);isstay = true; }
    }

    private void Update()
    {
        if (isstay && Input.GetKeyDown(KeyCode.E)) { someUI.SetActive(true); Time.timeScale = 0; }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { if(show!=null) show.SetActive(false);isstay = false; }
    }
}
