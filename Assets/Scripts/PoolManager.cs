using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // prefab 보관용
    public GameObject[] prefabs;

    // pool 관리용
    List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < _pools.Length; i++)
        {
            _pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetObject(int index)
    {
        GameObject selected = null;
        
        // 선택된 pool에 있는 object 중 비활성화된(사용되지 않는) gameobject에 접근
        // 있으면 그거 쓰고 없으면 새로운 object 생성 + pool에 등록
        foreach (var item in _pools[index])
        {
            if (!item.activeSelf)
            {
                selected = item;
                selected.SetActive(true);
                break;
            }
        }

        if (!selected)
        {
            selected = Instantiate(prefabs[index], transform);
            _pools[index].Add(selected);
        }
        
        return selected;
    }
}
