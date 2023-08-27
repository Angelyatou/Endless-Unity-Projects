using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxImpulse = 10;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ж�ײ����
        float m = 0;
        for(int i = 0; i < collision.contactCount; i++)
        {
            var c = collision.contacts[i];
            if (c.normalImpulse > m)
            {
                m = c.normalImpulse;
            }
        }
        if(m > maxImpulse)
        {
            Debug.Log($"{gameObject.name}�����{m}");
            //���ű�ը����
            anim.SetTrigger("Die");
            Invoke("Die", 0.5f);
        }
        
    }
    void Die()
    {
        GameMode.Instance.OnPigDie();
        Destroy(gameObject);
    }
}
