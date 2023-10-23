using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{

    public GameObject x2Upgrader;
    public float x2Chance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "block")
        {
            if(collision.gameObject.GetComponentInChildren<MapBoxIdentifier>().blockType == 0)
            {
                GameObject.Destroy(collision.gameObject);
                if(x2Chance > Random.Range(0, 100))
                {
                    GameObject upg = Instantiate(x2Upgrader, transform.position, Quaternion.identity);
                }
            }
        }
        if(collision.gameObject.tag == "deathBar")
        {
            Destroy(transform.gameObject);
        }
    }
}
