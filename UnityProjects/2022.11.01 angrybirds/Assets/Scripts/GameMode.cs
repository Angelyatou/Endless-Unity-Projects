using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

    public static GameMode Instance { get; private set; }
    public Transform bird;
    PlayerBirds playerBird; //bird���ϵĽű����
    public Transform center;

    public float maxDist = 3;
    public float maxForce = 700;

    //���Դ�����С������
    public int playerLives = 4;

    //ʣ���������
    public int enemyNum = 0;
    LineRenderer[] lines;

    FollowCam cam;

    public Transform prefabPoint;
    Transform[] points;

    bool isBirdFlying = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        bird.position = center.position;
        playerBird = bird.GetComponent<PlayerBirds>();

        Rigidbody2D rigid = bird.GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;

        lines = center.parent.GetComponentsInChildren<LineRenderer>();
        lines[0].SetPosition(0, bird.position);
        lines[1].SetPosition(0, bird.position);

        cam = Camera.main.GetComponent<FollowCam>();

        //ͳ�Ƶ�������
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyNum = enemies.Length;
        //���������ߵĵ�
        points = new Transform[20];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = Instantiate(prefabPoint);
            points[i].gameObject.SetActive(false);
        }

    }
    public void BeginDrag()
    {
        if (isBirdFlying)
        {
            return;
        }
        //��ʾ���еĸ�����
        ShowPoints(true);
    }
    public void Drag(Vector3 mousePos)
    {
        if (isBirdFlying)
        {
            return;
        }
        //��Ļ����ת��������
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = 0;

        //С�������ĵ�ľ���
        Vector3 v = pos - center.position;

        if (v.magnitude > maxDist)
        {
            v = v.normalized * maxDist;
        }
        bird.position = center.position + v;

        //����Ƥ��
        lines[0].SetPosition(0, bird.position);
        lines[1].SetPosition(0, bird.position);

        //���Ƹ�����
        Rigidbody2D rigid = bird.GetComponent<Rigidbody2D>();
        float f = v.magnitude / maxDist * maxForce;
        float v0m = f * Time.fixedDeltaTime / rigid.mass;
        Vector2 v0 = -v.normalized * v0m;

        //����ʱ��t��С��λ��
        //p = bird.position+(v0 * t +0.5f * Physics2D.gravity * t * t);
        float t = 0;
        for (int i = 0; i < points.Length; i++)
        {
            t += 0.2f;
            Vector2 p = (Vector2)bird.position + (v0 * t + 0.5f * Physics2D.gravity * t * t);
            points[i].position = p;
        }
    }
    public void EndDrag()
    {
        if (isBirdFlying)
        {
            return;
        }
        //�ж�С������ĵ���룬׼������
        Vector3 v = bird.position - center.position;
        if(v.magnitude < 0.01f)
        {
            return;
        }
        //ʩ����,����С��
        Rigidbody2D rigid = bird.GetComponent<Rigidbody2D>();
        rigid.isKinematic = false;
        float f = v.magnitude / maxDist * maxForce;
        rigid.AddForce(f * -v.normalized);
        playerBird.StartFly();

        cam.isFollow = true;
        isBirdFlying = true;

        //�������еĸ�����
        ShowPoints(false);

        //Ƥ���λ
        lines[0].SetPosition(0, center.position);
        lines[1].SetPosition(0, center.position);
    }
    
    public void OnPlayerBirdDie()
    {
        playerLives--;
        
        if (playerLives > 0)
        {
            //С��λ
            bird.position = center.position;
            isBirdFlying = false;
            cam.isFollow = false;
            cam.Follow();
            playerBird.ResetBird(); 
        }
        else
        {
            //�ȴ����룬Ȼ���ж�Game Over
            Invoke("DelayGameOver", 2);
        }
    }

    void DelayGameOver()
    {
        if (enemyNum <= 0)
        {
            //��Ϸ�Ѿ��ɹ�
            return;
        }
        Debug.Log("you loss");
    }
    public void ShowPoints(bool visible)
    {
        foreach(var p in points)
        {
            p.gameObject.SetActive(visible);
        }
    }
    public void OnPigDie()
    {
        enemyNum--;
        if (enemyNum <= 0)
        {
            Debug.Log("You Win!");
        }
    }
}
