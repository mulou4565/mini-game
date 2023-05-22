using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraMachine : Machine
{
    public CinemachineVirtualCamera cameras;
    public float smallSize;
    public float bigSize;
    // Start is called before the first frame update
    public Transform follow;
    public Transform target;
    Coroutine cameraChange;
    public float waitTime;
    public float allTime;
    public bool beCenter;
    public bool onlySomeTime;
    public bool noChange;
    public float lookTime;
    public override void Run()
    {
        if (onlySomeTime && cameraChange == null)
        { 
        
        
        }
        else if (cameraChange == null)
        {
            if (beCenter)
            {
                cameraChange = StartCoroutine(BeCenter());
            }
            else { cameraChange = StartCoroutine(NoCenter()); }

        }
    }
    private void Awake()
    {
       if(target.position!=null) target.position = new Vector3(target.position.x, target.position.y, -10);
        follow = cameras.Follow;
        if (!beCenter) { cameras.Follow = null; }
    }
    //private void Update()
    //{   
    //    if (Input.GetKeyDown(KeyCode.X)) { Run(); }
    //}
    void ChangeSmallSize()
    {
        cameras.m_Lens.OrthographicSize = smallSize;
    
    }
    void ChangeBigSize()
    {
        cameras.m_Lens.OrthographicSize = bigSize;

    }
    void NoFollow()
    {   
        cameras.Follow = null;
        
    }

    void HaveFollow() 
    {
        cameras.Follow = follow;
    }

    IEnumerator BeCenter() 
    {

        NoFollow();
        float nowtimes=0;
        Vector3 nowpos = transform.position;
        while (nowtimes < allTime) 
        {

            nowtimes += waitTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -10), Vector3.Distance(nowpos, new Vector3(0, 0, -10)) * waitTime / allTime);
            cameras.m_Lens.OrthographicSize = smallSize + nowtimes / allTime * (bigSize - smallSize);
            yield return new WaitForSeconds(waitTime);
        }
        transform.position = new Vector3(0, 0, -10);
        cameras.m_Lens.OrthographicSize = bigSize;
        cameraChange = null;
        beCenter = false;
    }

    IEnumerator NoCenter()
    {
        
        float nowtimes = 0;
         HaveFollow();
        while (nowtimes < allTime/2)
        {
            nowtimes += waitTime;
           
            cameras.m_Lens.OrthographicSize = bigSize - nowtimes / allTime * (bigSize - smallSize);


            yield return new WaitForSeconds(waitTime/2);
        }
        
        cameras.m_Lens.OrthographicSize = smallSize;
        cameraChange = null;
    }

    IEnumerator LookTarget()
    {
        float nowtimes = 0;
        NoFollow();
        Vector3 nowpos = transform.position;
        while (nowtimes < allTime)
        {
            nowtimes += waitTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, Vector3.Distance(target.position, nowpos) * waitTime / allTime);
            cameras.m_Lens.OrthographicSize = bigSize - nowtimes / allTime * (bigSize - smallSize);


            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitForSeconds(lookTime/3*2);
    }
}
