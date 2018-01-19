using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSeam : MonoBehaviour {

    public float gold;
    public GameObject Platform;
    public float Y = (float)-0.5;

    public void Update()
    {
        if (gold == 0)
        {
            Destroy(gameObject);
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        Instantiate(Platform, new Vector3(gameObject.transform.position.x, Y, gameObject.transform.position.z), transform.rotation);
    }
}
