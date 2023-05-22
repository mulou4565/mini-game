using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{   [Tooltip("一般行走速度")]
    public float moveSpeed;
    [Tooltip("初始的跳跃速度")]
    public float jumpSpeed;
    public Rigidbody2D rb;
    [Tooltip("检测地面的图层")]
    public LayerMask layer;
    [Tooltip("脚下位置建议创建两个子物体确定位置")]
    public Transform pos1;
    public Transform pos2;
    [Tooltip("判断是否为地面的射线长度")]
    public float lenth;
    [Tooltip("射击子弹")]
    public GameObject bullet;
    [Tooltip("射击子弹时的力")]
    public float force;
    [Tooltip("加速时的最大速度")]
    public float maxSpeed;
    [Tooltip("当前速度")]
    public float nowSpeed;
    [Tooltip("是否有刹车，TRUE是有")]
    public bool moveNormal;
    [Tooltip("刹车速度")]
    public float downspeed;
    float lastface;
    [Tooltip("加速度")]
    public float a;
    [Tooltip("攻击间隔")]
    public float waitTime;
    Coroutine wait;
    Vector3 lastpos;
    Transform ts;
    public Vector2 followMove;
    public bool isground;
    public bool isjump;
    public bool canMove;
    public Vector3 nowspeed;
    public float maxSpeedy;
    public int Hp=3;
    float scale;
    bool invincible;
    Coroutine coroutine2;
    public float invincibleTime;
    public Transform shootpos1;
    public Transform shootpos2;
    public Animator animators;
    public bool isMove;
    public SpriteRenderer[] sprites;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        lastface = 1;
        if (moveNormal) nowSpeed = moveSpeed;
        else nowSpeed = 0;
        ts = transform.parent;
        isjump = false;
        if (maxSpeedy < jumpSpeed) { maxSpeedy = 2 * jumpSpeed; }
        Hp = 3;
        scale = transform.localScale.x;
    }
    private void FixedUpdate()
    {
        
    }

    public void SetRby(float y )
    {
        rb.velocity = new Vector2(rb.velocity.x, y);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) { Hurt(); }
        if (!canMove) { return; }
        if (moveNormal  ) { normalMove(); }
        else { specialMove(); }
        if (check()) { isground = true; }
        else isground = false; 
        if (Input.GetKeyDown(KeyCode.Space) && canMove) { jump(); }
        if (Input.GetKeyDown(KeyCode.F)&& canMove) { shoot(); }
        animators.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        animators.SetBool("ismove", isMove);
        nowspeed = rb.velocity;
    }
    public void Hurt()
    {
        if (invincible) { return; }
        Hp--;
        if (Hp <= 0) { GameManager.gamemanager.failUI.SetActive(true);Time.timeScale = 0; return; }
        invincible = true;
        coroutine2 = StartCoroutine(Invincible());
    
    }
    private void normalMove()
    {
        if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift) && isground)
        {
            nowSpeed = nowSpeed + a * Time.deltaTime;
            
            nowSpeed = Mathf.Min(nowSpeed, maxSpeed);
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            nowSpeed = moveSpeed;
        }
        if (Input.GetAxis("Horizontal") > 0) { transform.localScale = new Vector3(scale, transform.localScale.y, 0);isMove =true; }
        if (Input.GetAxis("Horizontal") < 0) { transform.localScale = new Vector3(-scale, transform.localScale.y, 0); isMove = true; }
        if (Input.GetAxis("Horizontal") == 0) { isMove = false; }
        if (lastface * transform.localScale.x == -1) { nowSpeed = moveSpeed; }
        lastface = transform.localScale.x;
        if (nowSpeed > moveSpeed) { rb.velocity = new Vector3(nowSpeed*transform.localScale.x/Mathf.Abs(transform.localScale.x), rb.velocity.y,0);}
        else
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * nowSpeed, rb.velocity.y);
        }
        //if (!isjump) { rb.velocity = new Vector2(rb.velocity.x, 0) + followMove; }
        rb.velocity = new Vector2(rb.velocity.x + followMove.x, rb.velocity.y);
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(maxSpeed)) { rb.velocity = new Vector2(rb.velocity.x / Mathf.Abs(rb.velocity.x) * maxSpeed, rb.velocity.y); }
        if (Mathf.Abs(rb.velocity.y) > maxSpeedy) { rb.velocity = new Vector2(rb.velocity.x,maxSpeedy* Mathf.Abs(rb.velocity.y) / rb.velocity.y); }     
    }
    private void jump()
    {
        if (!isground) { return; }
        
        isjump = true;
        animators.SetBool("jump", isjump);
        rb.velocity = new Vector3(rb.velocity.x, Input.GetAxis("Jump") * jumpSpeed);
        
    }

    private bool check()
    {
        RaycastHit2D hit2D1 = Physics2D.Raycast(pos1.position , Vector2.down, lenth,layer);
        RaycastHit2D hit2D2 = Physics2D.Raycast(pos2.position, Vector2.down, lenth, layer);

        //if (hit2D1.collider != null&& transform.parent != hit2D1.collider.transform && hit2D1.collider.gameObject.tag == "Machine") { transform.SetParent(hit2D1.collider.transform.parent,true); }
        //else if (hit2D2.collider != null && transform.parent != hit2D2.collider.transform && hit2D2.collider.gameObject.tag == "Machine") { transform.SetParent(hit2D2.collider.transform.parent,true); }
        if (hit2D1 || hit2D2) { isjump = false; animators.SetBool("jump", isjump); return true; }
        else { isjump = true; animators.SetBool("jump", isjump); }
        //transform.SetParent(ts,true);
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1.position, pos1.position + Vector3.down * lenth);
        Gizmos.DrawLine(pos2.position , pos2.position +Vector3.down * lenth);
    }
    private void shoot()
    {
        if (wait != null) { return; } else { wait = StartCoroutine(shootWait()); }
        if (Input.GetKey(KeyCode.W))
        {
            animators.SetTrigger("shootup");
            GameObject newbullet = GameObject.Instantiate(bullet, shootpos2.position, transform.rotation);
            newbullet.transform.rotation = Quaternion.Euler(0, 0, 180);
            newbullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }
        else
        {
            animators.SetTrigger("shoot");
            GameObject newbullet = GameObject.Instantiate(bullet, shootpos1.position, transform.rotation);
            if (transform.localScale.x > 0) { newbullet.transform.rotation = Quaternion.Euler(0, 0, 90); }
            if (transform.localScale.x < 0) { newbullet.transform.rotation = Quaternion.Euler(0, 0, -90); }
            newbullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x/Mathf.Abs(transform.localScale.x), 0) * force);
        }
    }
    private void specialMove()
    {

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (isground)
            {
                if (Input.GetAxis("Horizontal") > 0  && nowSpeed>=0) { transform.localScale = new Vector3(scale, transform.localScale.y, 0); nowSpeed += a * Time.deltaTime * transform.localScale.x; }
                if (Input.GetAxis("Horizontal") < 0  && nowSpeed<=0) { transform.localScale = new Vector3(-scale, transform.localScale.y, 0); nowSpeed += a * Time.deltaTime * transform.localScale.x; }
                if (Input.GetAxis("Horizontal") > 0 && nowSpeed <=0) { transform.localScale = new Vector3(scale, transform.localScale.y, 0); nowSpeed += downspeed * Time.deltaTime * transform.localScale.x; }
                if (Input.GetAxis("Horizontal") < 0 && nowSpeed >= 0) { transform.localScale = new Vector3(-scale, transform.localScale.y, 0); nowSpeed += downspeed * Time.deltaTime * transform.localScale.x; }
                nowSpeed = Mathf.Min(nowSpeed, moveSpeed);
            }
        }
        else 
        {
            if (isground)
            {
                if (Input.GetAxis("Horizontal") > 0 && nowSpeed >= 0) { transform.localScale = new Vector3(scale, transform.localScale.y, 0); nowSpeed += a * Time.deltaTime * transform.localScale.x; }
                if (Input.GetAxis("Horizontal") < 0 && nowSpeed <= 0) { transform.localScale = new Vector3(-scale, transform.localScale.y, 0); nowSpeed += a * Time.deltaTime * transform.localScale.x; }
                if (Input.GetAxis("Horizontal") > 0 && nowSpeed <= 0) { transform.localScale = new Vector3(scale, transform.localScale.y, 0); nowSpeed += downspeed * Time.deltaTime * transform.localScale.x; }
                if (Input.GetAxis("Horizontal") < 0 && nowSpeed >= 0) { transform.localScale = new Vector3(-scale, transform.localScale.y, 0); nowSpeed += downspeed * Time.deltaTime * transform.localScale.x; }
                nowSpeed = Mathf.Min(nowSpeed, maxSpeed);
            }
        }
        if (Input.GetAxis("Horizontal") == 0 && nowSpeed!=0) 
        {
            nowSpeed = nowSpeed - downspeed * Time.deltaTime * nowSpeed / Mathf.Abs(nowSpeed);
            if (Mathf.Abs(nowSpeed) < 0.2f) { nowSpeed = 0; }
         }
        rb.velocity = new Vector3(nowSpeed, rb.velocity.y);
        rb.velocity = rb.velocity + followMove;

    }
    IEnumerator shootWait()
    {
        
        yield return new WaitForSeconds(waitTime);
        wait = null;
    
    }

    IEnumerator Invincible()
    {
      
        for (int i = 0; i < 5; i++)
        {

            foreach (var item in sprites)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 0);
            }
            yield return new WaitForSeconds(invincibleTime / 10);
            foreach (var item in sprites)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 1);
            }
            
            yield return new WaitForSeconds(invincibleTime / 10);

        }
        invincible = false;
    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") || collision.CompareTag("TriggerNoAttach"))
        {
            Debug.Log(10);
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.GetComponent<UseMachine>().use();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("TriggerRightNow"))
        { collision.GetComponent<UseMachine>().use(); }
    }
    public void NoFollow() 
    {
        followMove = Vector2.zero;
    
    }
}
