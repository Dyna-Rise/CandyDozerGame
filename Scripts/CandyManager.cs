using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;

    //リアルタイムのCandyのストック数
    public int candy = DefaultCandyAmount;//定数の30個を初期値にする

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GUI.Label(new Rect(50,50,100,30),label);
    }

}
