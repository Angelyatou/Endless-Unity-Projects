using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    Transform grabTrans;    //����ץȡ�����壬û������Ϊnull
    public Transform grabPos;      //zץȡ����ĵ�

    public void Grab()
    {
        if (grabTrans==null)
        {
            //����ץ����
            Vector3 start = transform.position + new Vector3(0, 0.7f, 0);
            //Debug.DrawLine(start, start + transform.right, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(start, transform.right, out hit, 1, LayerMask.GetMask("GrabBox")))
            {
                Transform box = hit.transform;
                box.parent = grabPos;
                box.localPosition = Vector3.zero;
                box.localRotation = Quaternion.identity;
                box.GetComponent<Rigidbody>().isKinematic = true;
                grabTrans = box;
            }
        }
        else
        {
            //������
            grabTrans.parent = null;//������Żس����У����ø�����Ϊ�գ��ص�һ�����壩
            grabTrans.GetComponent<Rigidbody>().isKinematic = false;
            grabTrans.position = new Vector3(grabTrans.position.x, grabTrans.position.y, 0);
            grabTrans.rotation = new Quaternion(grabTrans.rotation.x, 0, grabTrans.rotation.z, 0);
            grabTrans = null;
        }
    }

    private void Update()
    {
        if (grabTrans==null)
        {
            anim.SetBool("Grab", false);
        }
        else
        {
            anim.SetBool("Grab", true);
        }
    }
}
