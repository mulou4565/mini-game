using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public bool end;
    public bool withTrigger;
    // Update is called once per frame
    void Update()
    {
        if (withTrigger && end && Input.GetKeyDown(KeyCode.E)) { gameObject.SetActive(false);Time.timeScale = 1; }
        if (!withTrigger && end) { gameObject.SetActive(false);Time.timeScale = 1; }
    }
}
