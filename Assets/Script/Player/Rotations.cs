using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotations : MonoBehaviour
{

    public float rospeed;
    Coroutine rotation;
    Rigidbody2D rb;

    private void Awake()
    {
        rotation = null;
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q) && rotation==null) { rotation = StartCoroutine(startRotation()); }
    }
    public void ro()
    {
        Debug.Log(3);
        if (rotation == null) { Debug.Log(1); rotation = StartCoroutine(startRotation()); }

    }
    IEnumerator startRotation()
    {
        float x = 90; 
        while (x>0)
        {
            x -= Mathf.Abs(rospeed);
            //rb.rotation += rospeed;
            Vector3 nowro = transform.rotation.eulerAngles;
            nowro += new Vector3(0, 0, rospeed);
            transform.rotation = Quaternion.Euler(nowro);
            yield return new WaitForSeconds(0.1f);
        }
        rotation = null;
    }
    
}
