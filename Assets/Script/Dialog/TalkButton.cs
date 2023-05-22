using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject TalkUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Button.SetActive(false);
    }

    private void Update()
    {
        if(Button.activeSelf && Input.GetKeyDown(KeyCode.I))
        {
            TalkUI.SetActive(true);
        }
    }
}
