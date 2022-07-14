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
    

    public void onClickAct(){
        //Debug.Log("Tubeがタッチされた");
    }

    // Start is called before the first frame update
    void Start()
    {
        //getChildren
        children = getChildren(parent);

        //ソート
        Sort(children);


    }

    // Update is called once per frame
    void Update()
    {
        children = getChildren(parent);
        //チェック
        int counter;
        Transform[] tmpArr; 
        tmpArr = new Transform[parent.transform.childCount];
        counter = Check(tmpArr);

        //tmpArrをテストする
        Test_tmpArr(tmpArr,counter);


        //圧縮配列を作る
        //Debug.Log($"{counter}の長さの配列を作ります");
        Transform[] endArr; //要素を圧縮するための配列
        endArr = new Transform[counter];
        endArr = compressArr(tmpArr,endArr,counter); 


        //合成してズレた分を直す
        float  Top_Y = SetTopY(children);
        Debug.Log($"{Top_Y}に合わせます");
        reform_endArrElement(endArr, Top_Y);
      
        //
    
    }
        //getChildren
        Transform[] getChildren(GameObject parent){
            children = new Transform[this.parent.transform.childCount];

            int count = 0;
            foreach(Transform child in this.parent.transform) {
                children[count] = child;
                //Debug.Log($"{count}番目の子供は{children[count].name}です");
                count++;
            }

            return children;
        }

        
        void Sort(Transform[] children){
            float scale_y = 1.0f;
            float Top_Y = parent.transform.childCount;
            int cnt = 0;

            while(cnt < parent.transform.childCount){
                //Vector3 pos = transform.localPosition; //何かここがおかしいっぽい
                Vector3 pos = new Vector3(0,0,0);
                pos.y =  Top_Y - scale_y * cnt;
                children[cnt].transform.localPosition = pos;
                cnt++;
            }
        }

        //childrenをチェックしていく
        int Check(Transform[] tmpArr){
            
             //parent引き継ぐ必要あるのか…？
            //1つ1つchildrenをチェックしていく
            int itr = 0;
            int counter = 0;

            while(itr < parent.transform.childCount){
                //一致したら配列を入れる
                if(children[itr].tag == children[itr+1].tag) {
                    //Debug.Log($"{children[itr].tag}が一致しました");

                    if(itr == 0) {
                        //まず0番目をチェック
                        //Debug.Log($"{tmpArr[itr]}に代入しています");
                        tmpArr[itr] = children[itr];
                        counter++;
                        //Debug.Log($"tmpArr[itr]に{children[itr]}代入しました");
                    }
                tmpArr[itr+1] = children[itr+1];
                //Debug.Log($"{children[itr+1]}を代入しました");
                counter++;
                itr++;
                continue;
                }else{
                    break;
                };
            }
            return counter;
        }

        void Test_tmpArr(Transform[] tmpArr,int counter) {
            int cnt = 0;

            //Debug.Log($"{counter}の長さの配列を作ります");

            if(tmpArr.Length>0){
                foreach(Transform child in tmpArr){
                    //Debug.Log($"{cnt}番目");
                    //Debug.Log($"{child}");
                    cnt++;
                }
            }
        }

        Transform[] compressArr(Transform[] tmpArr,Transform[] endArr,int counter){
            
            int cnt = 0;
            if(tmpArr.Length > 0 ){
                //endArrに頭から入れていく
                while(cnt < counter){
                    endArr[cnt] = tmpArr[cnt];
                    cnt++;
                }
            }

            return endArr;
        }

        void reform_endArrElement(Transform[] endArr, float Top_Y){
            float difference = 0.5f; 
            int cnt_2 = 0; //削除した回数

            //Debug.Log($"{endArr}を整形します");
            if(endArr.Length > 1) {
                //endArrの0番目の大きさを変える
                Vector3 tmp;
                tmp = endArr[0].transform.localScale;
                tmp.y = tmp.y * endArr.Length;
                endArr[0].transform.localScale = tmp;
                
                //1から最後までの要素を削除する
                int cnt_1 = 1; //endArr要素を指定するインデックス


                while(cnt_1 < endArr.Length){
                    Destroy(endArr[cnt_1].gameObject);
                    cnt_2++;
                    cnt_1++;
                }

                //合成した分*0.5 下げて離れた分くっつける
                   
                Vector3 tmp_1 ;
                tmp_1 = endArr[0].transform.localPosition;
                tmp_1.y = Top_Y - difference * cnt_2;
                endArr[0].transform.localPosition = tmp_1;
            }
 
 
       }

        float SetTopY(Transform[] children){
            //TopYを返す

            float sumTopY = 0f;
            int cnt=0;
            float[] tmpArr;
            tmpArr = new float[parent.transform.childCount]; 
            foreach(Transform child in children){
                float tmpTopY;
                tmpTopY = child.transform.localScale.y;
                tmpArr[cnt] = tmpTopY;
                cnt++;
            }

            int i = 0;
            while(i < cnt){
                sumTopY += tmpArr[i];
            }
            Debug.Log($"{sumTopY}が基準になります");

            return sumTopY;
        }
        void SetByTopY(float Top_Y,Transform[] children){

        }

        //Touch
        void TouchManager() {

        }

        void Reposition(Transform[] children){ //childrenを、親オブジェクトの相対座標通りに配置し直す

        }




}

public class Tube{ //クラス作ってみたが、アタッチされていないので呼び出せない
    public GameObject parent;

    Transform[] children;
    int count;

    public Tube (GameObject Parent, Transform[] Children){
        parent = Parent;
        children = Children;
        count = 0;
    }

    public void getChildren(){
        foreach(Transform child in parent.transform){
            children[count] = child;
            //Debug.Log($"{count}番目の子供は{children[count].name}です");
            count++;
        }
    }
    
}

//子オブジェクトの存在を確認するクラス
public static partial class TransformExtensions {
    public static bool HasChild(this Transform transform){
        return 0 < transform.childCount;
    }

}