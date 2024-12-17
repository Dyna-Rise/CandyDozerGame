using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager; //CandyManagerスクリプト
    public int reward; //補充数の設定

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //何かのColliderと触れた時
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            //引数に指定した数だけストックを回復する
            candyManager.AddCandy(reward);

            //ぶつかった相手otherを削除
            Destroy(other.gameObject);
        }
    }
}
