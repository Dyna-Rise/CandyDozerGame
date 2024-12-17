using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Vector3 startPosition; //基準値記録用

    public float amplitude; //移動量
    public float speed; //移動スピード


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //変位を計算
        float z = amplitude * Mathf.Sin(Time.time * speed);

        //変位zをPusherのPositionに反映
        transform.localPosition = startPosition + new Vector3(0,0,z);
    }
}
