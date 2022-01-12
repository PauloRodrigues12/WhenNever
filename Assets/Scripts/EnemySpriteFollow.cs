using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteFollow : MonoBehaviour
{
    public Transform transformToFollow;

    void Update()
    {
        //Seguir a posição do sprite
        this.transform.position = transformToFollow.position;
    }
}
