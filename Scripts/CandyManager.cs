using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    //Candyのストック
    const int DefaultCandyAmount = 30;

    //コルーチン発動から何秒まって処理させたいか
    const int RecoverySeconds = 10; 

    //リアルタイムのCandyのストック数
    public int candy = DefaultCandyAmount;//定数の30個を初期値にする

    //OnGUIで文字連結して表示する用：ストック回復までのカウント
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Candyのストックがデフォルトより少ない※満タンじゃない
        //回復カウントをしていない時にカウントをスタートさせる
        if(candy < DefaultCandyAmount && counter <= 0)
        {
            //コルーチンの発動
            StartCoroutine(RecoverCandy());
        }
    }

    //RecoverCandyという名のコルーチンの設計
    IEnumerator RecoverCandy()
    {
        //カウントを10
        counter = RecoverySeconds;

        while(counter > 0)
        {
            //1秒待つ
            yield return new WaitForSeconds(1.0f);
            //カウントを1減らす
            counter--;
        }

        //counterが0になったらたどり着く
        candy++;
    }

    //Candyを消費
    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }

    //Candyの数を調べる
    public int GetCandyAmount()
    {
        return candy;
    }

    //Candyを引数の数だけ補充する
    public void AddCandy(int amount)
    {
        candy += amount;
    }

    void OnGUI()
    {
        GUI.color = Color.black;//文字を黒色に

        //Candyのストック数を表示
        string label = "Candy :" + candy;

        //回復カウントしている時だけ秒数を表示
        if (counter > 0) label += "(" + counter + "s)";

        GUI.Label(new Rect(50, 50, 100, 30), label);
    }

}
