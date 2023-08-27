using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBirds : MonoBehaviour
{
    bool dead = false;
    Rigidbody2D rigid;
    float startFlyTime = 0;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dead)
        {
            Debug.Log("С��������" + collision.collider.name);
            if (startFlyTime + 0.1f < Time.time)
            {
                Invoke("Die", 2.5f);
                dead = true;
            }
        }
    }
    private void Update()
    {
        //���С��Խ�磬������
        if(transform.position.x > 25 || transform.position.x < -25 || transform.position.y > 30 || transform.position.y < -30)
        {
            Invoke("Die", 2.5f);
            dead = true;
        }
    }
    void Die()
    {
        GameMode.Instance.OnPlayerBirdDie();
    }
    public void StartFly()
    {
        startFlyTime = Time.time;
    }
    public void ResetBird()
    {
        rigid.isKinematic = true;
        //С����ת��0
        transform.rotation = Quaternion.identity;
        //rigid.rotation = 0;  //����������һ��ȼ�

        //С���ٶȹ�0
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        dead = false;
    }
}
