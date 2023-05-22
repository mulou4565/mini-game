using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : Monster
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [Tooltip("射手左右移动范围的端点")]
    public Transform leftpoint, rightpoint;
    public float leftx, rightx;
    [Tooltip("射手移动速度")]
    public float Speed = 6f;
    [Tooltip("判断人物和怪物距离")]
    public GameObject Distancejudge;
    [Tooltip("人物攻击伤害")]//后续人物改动后可以删除，由人物那边给出
    public float AttackHp = 1;
    [Tooltip("射手发出子弹")]
    public GameObject Cartridge;
    [Tooltip("射手攻击间隔")]
    public float WaitTime;
    Coroutine wait;
    [Tooltip("子弹发出时的力")]
    public float CartridgeForce;
    public bool IsAttacking=false;
    public float judgetime = 0f;
    private float midSpeed;
    public Animator animators;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Speed, rb.velocity.y);
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        //transform.DetachChildren();
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        midSpeed = Speed;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        ExitAttack();
        if (IsAttacking == false) {
            Movement(); 
        }
        
    }

    void Check()
    {

        if (Mathf.Abs(transform.position.x - leftx) < 4 * Speed * Time.deltaTime)
        {
            Debug.Log(1111111);
            transform.localScale = new Vector3(-1*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (Mathf.Abs(transform.position.x - rightx) < 4 * Speed * Time.deltaTime)
        {
            Debug.Log(444444);
            transform.localScale = new Vector3(1*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    void Movement()
    {
        
        if (transform.localScale.x > 0 && Mathf.Abs(transform.position.x - rightx)< 4 * Speed * Time.deltaTime)
        {
            midSpeed = -Speed;
        }
        else if(transform.localScale.x <0 && Mathf.Abs(transform.position.x - leftx) < 4 * Speed * Time.deltaTime)
        {
            midSpeed = Speed;
        }
        rb.velocity = new Vector2(midSpeed, rb.velocity.y);
        /*else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            Debug.Log(222222);
        }*/
        
    }


    public override void Attack()
    {
        Debug.Log(7777777);
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (IsAttacking == true) { return; }
        IsAttacking = true;
        animators.SetBool("IsAttacking1", IsAttacking);
        judgetime = Time.time;
        //if (wait != null) { return; } else { wait = StartCoroutine(shootWait()); }
        GameObject newCartridge = GameObject.Instantiate(Cartridge, transform.position, transform.rotation);
        newCartridge.GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.localScale.x, 0) * CartridgeForce);

        base.Attack();
    }
    public void ExitAttack()
    {
        if (judgetime + WaitTime < Time.time && judgetime != 0f)
        {
            Debug.Log(3333333);
            judgetime = 0;
            IsAttacking = false;
            animators.SetBool("IsAttacking1", IsAttacking);
        }
    }
    /*IEnumerator shootWait()
    {

        yield return new WaitForSeconds(WaitTime);
        wait = null;

    }*/
    
    public override void Hurt(float value)
    {
        hp--;
        if (hp < 0)
        {
            Debug.Log("AAAAAAAAAAAA");
            Destroy(this.gameObject);
        }
        base.Hurt(value);
    }

}
