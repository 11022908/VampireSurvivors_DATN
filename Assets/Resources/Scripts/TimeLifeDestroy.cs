using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLifeDestroy : MonoBehaviour
{
    public float timeLifeDestroy;
    void Start()
    {
        Destroy(this.gameObject, timeLifeDestroy);
    }

}
