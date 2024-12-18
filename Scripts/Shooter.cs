using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5; //ショットパワーの上限
    const int RecoverySeconds = 3; //ショットパワーの回復までの時間

    //リアルタイムに変動する残りのショットパワー
    int shotPower = MaxShotPower;

    //AudioSourceコンポーネントを扱える変数
    AudioSource shotSound;

    public GameObject[] candyPrefabs; //生成されるCandy

    public Transform candyParentTransform;//HierarchyでCandiesグループがどれかを指定

    public CandyManager candyManager;

    public float shotForce; //発射するパワー
    public float shotTorque; //発射時の回転

    public float baseWidth; //Candyが乗るBaseオブジェクトの幅の数値

    // Start is called before the first frame update
    void Start()
    {
        //自分のオブジェクトについているAudioSouceコンポーネントの情報を参照できるようにする
        shotSound = GetComponent<AudioSource>();
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
        //ショットパワーがなくなってもすぐやめる
        if (shotPower <= 0) return;

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

        //ShotPowerを消費
        ConsumePower();

        //サウンドを再生
        //AudioSourceコンポーネントにセッティングされているAudioClipを再生
        shotSound.Play();
    }

    void OnGUI()
    {
        //文字色は黒
        GUI.color = Color.black;

        //ShotPowerの残数を「+」で表現
        string label = "";
        for (int i = 0; i < shotPower; i++) label += "+";

        //変数labelの内容を画面上に表示
        GUI.Label(new Rect(50,65,100,30),label);
    }

    //ShotPowerの消費
    void ConsumePower()
    {
        //ShotPowerを1マイナス
        shotPower--;
        //消費と同時に回復スタート
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower()
    {
        //定数RecoverySeconds秒まった後にShotPower回復
        yield return new WaitForSeconds(RecoverySeconds);
        //ShotPower回復
        shotPower++;
    }
}
