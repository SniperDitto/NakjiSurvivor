using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reposition : MonoBehaviour
{
    Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 myPosition = transform.position;
        float diffX = MathF.Abs(playerPosition.x - myPosition.x);
        float diffY = MathF.Abs(playerPosition.y - myPosition.y);

        Vector3 playerDirection = GameManager.Instance.player.inputVector;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (Mathf.Abs(diffX - diffY) <= 0.01f) {
                    transform.Translate(Vector3.up * directionY * 40);
                    transform.Translate(Vector3.right * directionX * 40);
                }
                else if (diffX > diffY) {
                    transform.Translate(Vector3.right * directionX * 40);
                }
                else if (diffX < diffY) {
                    transform.Translate(Vector3.up * directionY * 40);
                }
                break;
            case "Enemy":
                if (_collider2D.enabled)
                {
                    transform.Translate(playerDirection * 25 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f)) * 0f);
                }
                break;
            default:
                break;
        }
        
    }
}
