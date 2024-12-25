using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    void Start()
    {
        Init();
    }
    
    void Update()
    {
        switch (id)
        {
            case 0:
                // 회전 시계방향
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
                break;
            default:
                break;
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                SetBullet();
                break;
            default:
                break;
        }
    }

    void SetBullet()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.Instance.poolManager.GetObject(prefabId).transform;
            
            // player를 부모로 지정
            bullet.parent = transform;
            
            // 방향 회전 후 일정 거리만큼 떨어트림
            Vector3 rotation = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            
            // 근접무기 관통 무한
            bullet.GetComponent<Bullet>().Init(damage, -1); 
            
            
        }
    }
}
