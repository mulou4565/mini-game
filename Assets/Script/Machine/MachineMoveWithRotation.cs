using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MachineMoveWithRotation : Machine
{
    // Start is called before the first frame update
    [Tooltip("起点位置一般为当前位置")]
    public Transform start;
    public bool changeStart;
    [Tooltip("终点位置")]
    public Transform end;
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
    public bool usewait;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        nowdirection = 1;


        nowPlace = 1;
        playerMove = null;
        canRun = false;
        if (!Auto)
        {
            switch (direction)
            {
                case Direction.VERTICAL:
                    start.position = new Vector3(transform.position.x, start.position.y, transform.position.z);
                    end.position = new Vector3(transform.position.x, end.position.y, transform.position.z);
                    if (end.position.y > start.position.y) speed = new Vector2(0, moveSpeed);
                    else speed = new Vector2(0, -moveSpeed);
                    break;
                case Direction.HORIZONTAL:
                    start.position = new Vector3(start.position.x, transform.position.y, transform.position.z);
                    end.position = new Vector3(end.position.x, transform.position.y, transform.position.z);
                    if (end.position.x > end.position.y) speed = new Vector2(moveSpeed, 0);
                    else speed = new Vector2(-moveSpeed, 0);
                    break;
                case Direction.DIRECTLY:
                    speed = new Vector2(moveSpeed * (end.position.x - start.position.x) / Vector3.Distance(end.position, start.position), moveSpeed * (end.position.y - start.position.y) / Vector3.Distance(end.position, start.position));
                    break;

            }
            transform.position = start.position;
            nowPosition = transform.position;
        }
        else { wait = StartCoroutine(Wait()); }


    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q)) { StartMove(); }

        if (Auto && canRun) { AutoMove(); return; }
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
        if (nowPlace == 1)
        {
            if (Vector3.Distance(end.position, transform.position) < moveSpeed * Time.deltaTime * 4)
            {
                run = false;
                if (playerMove != null) { playerMove.NoFollow(); }
                rb.velocity = new Vector2(0, 0);
                nowPlace = -1;
                transform.position = end.position;
                return true;
            }
        }
        if (nowPlace == -1)
        {
            if (Vector3.Distance(start.position, transform.position) < moveSpeed * Time.deltaTime * 4)
            {
                run = false;
                if (playerMove != null) { playerMove.NoFollow(); }
                rb.velocity = new Vector2(0, 0);
                nowPlace = 1;
                transform.position = start.position;
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
        if (playerMove != null)
        {
            if (speed.y * nowPlace > 0)
                playerMove.followMove = new Vector2(speed.x, 0) * nowPlace;
            else playerMove.followMove = speed * nowPlace;
        }
        rb.velocity = speed * nowPlace;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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

        if (collision.gameObject.tag == "Player") { playerMove.NoFollow(); playerMove = null; }
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(1f);
        switch (direction)
        {
            case Direction.VERTICAL:
                start.position = new Vector3(transform.position.x, start.position.y, transform.position.z);
                end.position = new Vector3(transform.position.x, end.position.y, transform.position.z);
                if (end.position.y > start.position.y) speed = new Vector2(0, moveSpeed);
                else speed = new Vector2(0, -moveSpeed);
                break;
            case Direction.HORIZONTAL:
                start.position = new Vector3(start.position.x, transform.position.y, transform.position.z);
                end.position = new Vector3(end.position.x, transform.position.y, transform.position.z);
                if (end.position.x > end.position.y) speed = new Vector2(moveSpeed, 0);
                else speed = new Vector2(-moveSpeed, 0);
                break;
            case Direction.DIRECTLY:
                speed = new Vector2(moveSpeed * (end.position.x - start.position.x) / Vector3.Distance(end.position, start.position), moveSpeed * (end.position.y - start.position.y) / Vector3.Distance(end.position, start.position));
                break;

        }
        transform.position = start.position;
        nowPosition = transform.position;
        canRun = true;
    }
}
