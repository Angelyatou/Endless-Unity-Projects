using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    Character character;
    float inputX;//ģ���������-1~1
    bool jump = false;

    public float changrActTime = 2;//�ı��ж���ʱ����
    public float jumpTime = 0.5f;//ÿ��0.5�볢����Ծ
    public float jumpChance = 30;//ÿ����30%������Ծ
    void Start()
    {
        character = GetComponent<Character>();
        StartCoroutine(ChangeAct());
        StartCoroutine(AIJump());
    }

    // Update is called once per frame
    void Update()
    {
        character.Move(inputX, jump);
        jump = false;
    }

   IEnumerator ChangeAct()
    {
        while (true)
        {
            //�ı�inputX
            inputX = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(changrActTime);
        }
    }

    IEnumerator AIJump()
    {
        while (true)
        {
            int r = Random.Range(0, 100);
            if (r<jumpChance)
            {
                jump = true;
            }
            yield return new WaitForSeconds(jumpTime);
        }
    }

}
