using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tubeの操作に関するクラス
public class TubeManager : MonoBehaviour
{
    //オブジェクトを取得    
    public GameObject parent;
    //配列作成
    Transform[] children;



    // Start is called before the first frame update
    void Start()
    {
        //getChildren
        children = new Transform[parent.transform.childCount];

        int count = 0;
        foreach(Transform child in parent.transform) {
            children[count] = child;
            Debug.Log($"{count}番目の子供は{children[count].name}です");
            count++;
        }

        //ソート
        float scale_y = 1.0f;
        float Top_Y = count*scale_y/2.0f;
        int cnt_0 = 0;
        
        while(cnt_0 < parent.transform.childCount){
            Vector3 pos = transform.localPosition;
            pos.y = Top_Y - scale_y*cnt_0;
            children[cnt_0].transform.localPosition = pos;

            cnt_0++;
        }


        Debug.Log($"これはカウント{parent.transform.childCount}");
        //childrenをチェックしていく。 配列を頭から見ていく。
        Transform[] tmpArr;
        tmpArr = new Transform[parent.transform.childCount];
        //1つ1つchildrenをチェックしていく 
        
        int itr = 0;
        int counter = 0;
        while(itr < parent.transform.childCount){
            //一致したら配列をいれる
            if(children[itr].tag == children[itr+1].tag) {
                Debug.Log($"{children[itr].tag}が一致しました");
   
                if(itr == 0){
                    //まず0番目をチェックする
                    Debug.Log($"{tmpArr[itr]}に代入しています");
                    tmpArr[itr] = children[itr];
                    counter++;
                    Debug.Log($"tmpArr[itr]に{children[itr]}代入しました");
                    }
                tmpArr[itr+1] = children[itr+1];
                Debug.Log($"{children[itr+1]}を代入しました");
                counter++;
                itr++;
                continue;
            }else{
                break;
            }
        }

        //テスト 各tmpArrを出力する
        int cnt_1 = 0;
        int cnt_2 = 0;

        Debug.Log($"{counter}の長さの配列を作ります");
        Transform[] endArr;
        endArr = new Transform[counter];
        //endArrに頭から入れていく
        if(tmpArr.Length > 0){
        Debug.Log("-----");
        while(cnt_1 < counter){
            endArr[cnt_1] = tmpArr[cnt_1];
            cnt_1++;
        }

        
        //endArrのテスト
        foreach(Transform child in endArr){
            
            Debug.Log($"{cnt_2}番目");
            Debug.Log($"{child}");
            cnt_2++;
        }
        Debug.Log("-----");
        }

        if(endArr.Length > 1) {
            //endArrの0番目の大きさを変える
            Vector3 tmp;
            tmp = endArr[0].transform.localScale;
            tmp.y = tmp.y * endArr.Length; 
            endArr[0].transform.localScale = tmp;
            //1から最後までの要素を削除する
            int cnt_3 = 1; 
            int cnt_4 = 0; //削除した回数
            while(cnt_3 < endArr.Length){
                Destroy(endArr[cnt_3].gameObject);//Transformは削除できない
                cnt_4++;//一つ削除しました
                cnt_3++;
            }
            //消した分*0.5 posYを下げる
            Vector3 tmp_1;
            tmp_1 = endArr[0].transform.localPosition;
            tmp_1.y = Top_Y - 0.5f * cnt_4 ; 
            endArr[0].transform.localPosition = tmp_1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}



//子オブジェクトの存在を確認するクラス
public static partial class TransformExtensions {
    public static bool HasChild(this Transform transform){
        return 0 < transform.childCount;
    }

}