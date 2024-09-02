using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
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
                break;
            default:
                break;
        }
        
    }
}
