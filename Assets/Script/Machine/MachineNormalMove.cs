using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MachineNormalMove : Machine
{

    [Tooltip("起点位置一般为当前位置")]
    public Vector3 start;
    public bool changeStart;
    [Tooltip("终点位置")]
    public Vector3 end;
    [Tooltip("平台移动方向")]
    public Direction direction;
    [Range(-1, 1)]
    int nowPlace;
    public Vector3 nowPosition;
    public float moveSpeed;
    [Tooltip("自动来回")]
    public bool Auto;
    public float waitTime;
    public Move playerMove;
    public Vector2 speed;
    bool run;
    float nowdirection;
    private Rigidbody2D rb;
    public Transform rotations;
    Coroutine wait;
    bool canRun;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        nowdirection = 1;
        nowPosition = transform.position;
        nowPlace = 1;
        if (!changeStart) { start = transform.position; }
        playerMove = null;
        canRun = false;
        switch (direction)
        {
            case Direction.VERTICAL:
                start = new Vector3(transform.position.x, start.y, transform.position.z);
                end = new Vector3(transform.position.x, end.y, transform.position.z);
                if (end.y > start.y) speed = new Vector2(0, moveSpeed);
                else speed = new Vector2(0, -moveSpeed);
                break;
            case Direction.HORIZONTAL:
                start = new Vector3(start.x, transform.position.y, transform.position.z);
                end = new Vector3(end.x, transform.position.y, transform.position.z);
                if (end.x > end.y) speed = new Vector2(moveSpeed, 0);
                else speed = new Vector2(-moveSpeed, 0);
                break;
            case Direction.DIRECTLY:
                speed = new Vector2(moveSpeed * (end.x - start.x) / Vector3.Distance(end, start), moveSpeed * (end.y - start.y) / Vector3.Distance(end, start));
                break;

        }
        if (Auto) { StartCoroutine(Wait()); }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q)) { StartMove(); }
        
        if (Auto && canRun) { AutoMove();return; }
        if (run) { CheckArrive(); }
    }
    public override void Run()
    {

        StartMove();
    }
    void AutoMove()
    {
        if (run == false) { StartMove(); }
        else { CheckArrive(); }
    }
    bool CheckArrive()
    {
        if (nowPlace == 1) {
            if (Vector3.Distance(end, transform.position) < moveSpeed * Time.deltaTime * 4)
            {
                run = false;
                if (playerMove != null) { playerMove.NoFollow(); }
                rb.velocity = new Vector2(0, 0);
                nowPlace = -1;
                transform.position = end;
                return true;
            }
        }
        if (nowPlace == -1)
        {
            if (Vector3.Distance(start, transform.position) < moveSpeed * Time.deltaTime*4)
            {
                run = false;
                if (playerMove != null) { playerMove.NoFollow(); }
                rb.velocity = new Vector2(0, 0);
                nowPlace = 1;
                transform.position = start;
                return true;
            }
        }

        return false;
    }

    private void rbMove()
    {
        rb.velocity = speed;
    }
    public void StartMove()
    {
        run = true;
        if (playerMove != null) { 
            if(speed.y*nowPlace>0)
            playerMove.followMove = new Vector2(speed.x, 0) * nowPlace;
            else playerMove.followMove =speed * nowPlace;
        }
        rb.velocity = speed*nowPlace;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" ) 
        {
            playerMove = collision.gameObject.GetComponent<Move>();


            if (run)
            {
                if (speed.y * nowPlace > 0)
                    playerMove.followMove = new Vector2(speed.x, 0) * nowPlace;
                else playerMove.followMove = speed * nowPlace;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && playerMove!=null) { playerMove.NoFollow(); playerMove = null; }
    }

    IEnumerator Wait() {

        yield return new WaitForSeconds(1f);
        canRun = true;
    }
}
