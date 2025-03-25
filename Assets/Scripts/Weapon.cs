using System;
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

    private float timer;
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }
    
    void Update()
    {
        switch (id)
        {
            case 0:
                // 회전 시계방향
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
                break;
            case 1:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelUp(2, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;

        if (id == 0)
        {
            SetBullet();
        }
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // 기본설정
        name = "Weapon " + data.itemName;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        
        // 값 설정
        id = data.itemId;
        damage = data.baseDmg;
        count = data.baseCount;

        for (int i = 0; i < GameManager.Instance.poolManager.prefabs.Length; i++)
        {
            if (data.projectiles == GameManager.Instance.poolManager.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }
        
        switch (id)
        {
            case 0:
                // 회전속도
                speed = 150;
                SetBullet();
                break;
            case 1:
                // 연사속도
                speed = 0.3f;
                break;
            default:
                break;
        }
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void SetBullet()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            // 기존 무기들 재배치
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.poolManager.GetObject(prefabId).transform;
                bullet.parent = transform;
            }
            
            // player를 부모로 지정
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            
            // 방향 회전 후 일정 거리만큼 떨어트림
            Vector3 rotation = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            
            // 근접무기 관통수 무한
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); 
            
            
        }
    }

    void Fire()
    {
        if(!player.scanner.nearestTarget) return;

        Vector3 targetPosition = player.scanner.nearestTarget.position;
        Vector3 dir = targetPosition - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.poolManager.GetObject(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
