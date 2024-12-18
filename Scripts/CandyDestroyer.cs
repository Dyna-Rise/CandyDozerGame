using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager; //CandyManagerスクリプト
    public int reward; //補充数の設定

    public GameObject effectPrefab; //プレハブ化したエフェクトを指定

    public Vector3 effectRotation; //生成されるエフェクトオブジェクトの角度を決めておく

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

            if (effectPrefab != null)
            {
                //Candyエフェクトを生成
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRotation)
                 );
            }
        }
    }
}
