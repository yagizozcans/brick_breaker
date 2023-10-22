using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrades : MonoBehaviour
{
    public float fallSpeed;
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);
    }
}
