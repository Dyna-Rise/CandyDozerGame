using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject candyPrefab; //生成されるCandy

    public float shotForce; //発射するパワー
    public float shotTorque; //発射時の回転

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

    //自作メソッド
    public void Shot()
    {
        //Candyオブジェクトを生成
        GameObject candy = Instantiate(
            candyPrefab,
            transform.position,
            Quaternion.identity
            );

        //生成したCandyについているRigidbodyを利用して押し出しと回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        //発射
        candyRigidBody.AddForce(transform.forward * shotForce);
        //回転
        candyRigidBody.AddTorque(new Vector3(0,shotTorque,0));
    }
}
