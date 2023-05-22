using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMonsterMovement : Monster
{

    private Rigidbody2D rb;
    [Tooltip("���������ƶ���Χ�Ķ˵�")]
    public Transform leftpoint, rightpoint;
    private float leftx, rightx;
    [Tooltip("�����ƶ��ٶ�")]
    public float Speed;
    [Tooltip("������ʱʩ���ڹ������ϵ���")]
    public float AttackForce = 1.6f;
    [Tooltip("�ж�����͹������")]
    public GameObject Distancejudge;
    [Tooltip("���﹥���˺�")]//��������Ķ������ɾ�����������Ǳ߸���
    public float AttackHp = 1;
    float midSpeed;
    bool IsAttacking = false;//�Ƿ����ڹ���
    [Tooltip("��������ʱ��")]
    public float AttackTime = 2f;
    float JudgeTime = 0;
    public Animator animators;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        midSpeed = Speed;
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        Check1();
        if (IsAttacking == true)
        {
            Move1();
        }
        else {
            Check();
            Movement();
        }
    }
    
    void Check()
    {
        if (Mathf.Abs(transform.position.x - leftx) < 4 * Speed * Time.deltaTime)
        {
            Debug.Log(1);
            transform.localScale = new Vector3(-1*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            return;
        }
        if (Mathf.Abs(transform.position.x - rightx) < 4 * Speed * Time.deltaTime)
        {
            Debug.Log(-1);
            transform.localScale = new Vector3(1*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            return;
        }
        
    }
    void Check1()
    {
        if(JudgeTime+AttackTime < Time.time && JudgeTime!=0)
        {
            IsAttacking = false;
            animators.SetBool("IsAttacking", IsAttacking);
            midSpeed /= 2;
            JudgeTime = 0;
            Check();
        }

    }
    void Move1()
    {
        if((transform.localScale.x > 0 && Mathf.Abs(transform.position.x - leftx) < 12 * Speed * Time.deltaTime)|| 
            (transform.localScale.x < 0 && Mathf.Abs(transform.position.x - rightx) < 12 * Speed * Time.deltaTime))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        rb.velocity = new Vector2(midSpeed, rb.velocity.y);
        Debug.Log(11111111111111);

    }
    void Movement()
    {
        if (transform.localScale.x > 0 && Mathf.Abs(transform.position.x - rightx) < 4 * Speed * Time.deltaTime)
        {
            Debug.Log(-111);
            midSpeed = -Speed;
        }
        else if (transform.localScale.x < 0 && Mathf.Abs(transform.position.x - leftx) < 4 * Speed * Time.deltaTime)
        {
            Debug.Log(111);
            midSpeed = Speed;
        }
        rb.velocity = new Vector2(midSpeed, rb.velocity.y);
    }


    public override void Attack()
    {
        if (IsAttacking == true) return ;
        IsAttacking = true;
        animators.SetBool("IsAttacking", IsAttacking);
        JudgeTime = Time.time;
        if(transform.localScale.x > 0)
        {
            midSpeed = -Speed * 2;
        }
        else
        {
            midSpeed = Speed * 2;
        }
        base.Attack();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            //��Ϸ����
            collision.gameObject.GetComponent<Move>().Hurt();
        }
    }
    public override void Hurt(float value)
    {
        hp--;
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
        base.Hurt(value);
    }
}
