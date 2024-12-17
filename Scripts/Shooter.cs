using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs; //生成されるCandy

    public Transform candyParentTransform;//HierarchyでCandiesグループがどれかを指定

    public CandyManager candyManager;

    public float shotForce; //発射するパワー
    public float shotTorque; //発射時の回転

    public float baseWidth; //Candyが乗るBaseオブジェクトの幅の数値

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //"Fire1"相当のキーが押されたら、Shotメソッドを発動
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    //ランダムにプレハブから選んだCandyを指名
    GameObject SampleCandy()
    {
        //ランダムに0～4未満の数字を取得
        int index = Random.Range(0,candyPrefabs.Length);
        //index番号目のCandyオブジェクトを配列から取得してメソッドの結果として戻す
        return candyPrefabs[index];
    }

    Vector3 GetInstantiatePosition()
    {
        //画面のサイズとInputの割合からCandyの生成座標を計算して戻す
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);

        return transform.position + new Vector3(x, 0, 0);
    }


    //自作メソッド
    public void Shot()
    {
        //Candyのストックが0以下ならすぐやめる
        if (candyManager.GetCandyAmount() <= 0) return;

        //Candyオブジェクトを生成
        GameObject candy = Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity
            );

        //生成したCandyの親にCandiesを指名
        candy.transform.parent = candyParentTransform;

        //生成したCandyについているRigidbodyを利用して押し出しと回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        //発射
        candyRigidBody.AddForce(transform.forward * shotForce);
        //回転
        candyRigidBody.AddTorque(new Vector3(0,shotTorque,0));

        //Candyのストックを消費
        candyManager.ConsumeCandy();
    }
}
