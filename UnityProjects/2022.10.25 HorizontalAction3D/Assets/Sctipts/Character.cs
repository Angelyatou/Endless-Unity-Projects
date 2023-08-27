using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterController cc;
    protected Animator anim;
    Vector3 pendingVelocity;//��һ֡�ƶ�������
    bool isGround=false;

    public float speed = 6;
    public float jumpSpped = 10;
    public GameObject deathParticle;//����������Ч

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    public void Move(float h,bool jump)
    {
        pendingVelocity.x = h * speed;
        pendingVelocity.z = 0;
        if (Mathf.Abs(h) > 0.1f)
        {
            //������ת
            Quaternion right = Quaternion.LookRotation(Vector3.forward);
            Quaternion left = Quaternion.LookRotation(Vector3.back);
            if (h>0)
            {
                //����ת
                transform.rotation= Quaternion.Slerp(transform.rotation, right, 0.05f);
            }
            else
            {
                //����ת
                transform.rotation = Quaternion.Slerp(transform.rotation, left, 0.05f);
            }
        }
        //������Ծ
        if (jump && isGround)
        {
            //������Ծ
            pendingVelocity.y = jumpSpped;
            isGround = false;
        }

        //��������
        if (!isGround)
        {
            pendingVelocity.y += Physics.gravity.y * 2 * Time.deltaTime;
        }
        else
        {
            pendingVelocity.y = 0;
        }
        

        cc.Move(pendingVelocity * Time.deltaTime);
        //���¶���
        UpdateAnim();

        //��̤�������
        AttackCheck();
    }

    private void AttackCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f, LayerMask.GetMask("Cha")))
        {
            //����������
            if (hit.transform==transform)
            {
                return;
            }

            //�����ߴ��е������� hit.transform
            Character cha = hit.transform.GetComponent<Character>();
            cha.TakeDamage();
            //����
            Bounce();

        }

    }

    public void TakeDamage()
    {
        Destroy(gameObject);
        GameObject go = Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(go,2);//�����ɾ������
    }

    private void Bounce()
    {
        pendingVelocity.y = jumpSpped / 2;
    }

    private void FixedUpdate()
    {
        isGround = false;
        Ray ray = new Ray(transform.position + new Vector3(0, 0.2f, 0), Vector3.down);
        Vector3 start = transform.position + new Vector3(0, 0.2f, 0);
        //Debug.DrawLine(start, start + new Vector3(0, -0.25f, 0),Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.25f, LayerMask.GetMask("Default")))
        {
            isGround = true;
        }
    }

    private void UpdateAnim()
    {
        anim.SetFloat("Forward", cc.velocity.magnitude / speed);
        anim.SetBool("IsGround", isGround);
    }
}
