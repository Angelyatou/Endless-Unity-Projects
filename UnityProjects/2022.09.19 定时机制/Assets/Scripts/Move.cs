using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0, v);
        if (input.magnitude > 1)
        {
            input = input.normalized;
        }

        //����ʱ�䣬����֡�ʱ仯��Ӱ��
        //transform.position += input * speed * Time.deltaTime;         //��������ϵ
        transform.Translate(input * speed * Time.deltaTime,Space.World);//��������ϵ
        transform.Translate(input * speed * Time.deltaTime);            //��������ϵ
        transform.Translate(input * speed * Time.deltaTime,Space.Self); //��������ϵ
    }
}
