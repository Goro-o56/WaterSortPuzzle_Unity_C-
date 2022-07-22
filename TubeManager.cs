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
        
    public bool click_state;
    
    public GameObject refParent; //MapManagerから参照されるParentObject
    public void onClickAct(){
        //Debug.Log($"{click_state}が今の状態です");
        if(click_state==true){
            click_state = false;
            refParent=null;
            //Debug.Log("falseにしました");
            return;
        }
        if(click_state==false){
            click_state = true;
            //Debug.Log("trueにしました");
            refParent = onClickReturnObject(parent);//tmpParentに代入
            return;
        }
    }
    public GameObject onClickReturnObject(GameObject parent){
        GameObject tmpObj;
        Debug.Log($"{this.parent}が引数{this.refParent}に代入されます。");
        tmpObj = this.parent;
        return tmpObj;
    }

    // Start is called before the first frame update
    void Start()
    {
        //getChildren
        children = getChildren(parent);

        //ソート
        Sort(children);
        //重複要素を合成する
        int counter;
        Transform[] tmpArr;
        tmpArr = new Transform[parent.transform.childCount];
        counter = Check(tmpArr);

        Transform[] endArr;
        endArr = new Transform[counter];
        endArr = compressArr(tmpArr, endArr,counter);
        
        float Top_Y;
        //children = getChildren(parent);
        Top_Y = SetTopY(children);
        reform_endArrElement(endArr, Top_Y);


    }

    // Update is called once per frame
    void Update(){
 
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
                Vector3 pos = children[cnt].transform.localPosition; //何かここがおかしいっぽい
                //Vector3 pos = new Vector3(0,0,0);
                pos.y =  Top_Y/2 - scale_y * cnt -0.5f;
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


                for (int i = 0 ; i < endArr.Length; i++) {
                    if (i==0){
                        
                    }else{
                        Destroy(endArr[i].gameObject);
                        cnt_2++;
                    }
                }

                //合成した分*0.5 下げて離れた分くっつける
                   
                Vector3 tmp_1 ;
                tmp_1 = endArr[0].transform.localPosition;
                tmp_1.y = tmp_1.y - difference * cnt_2;
                endArr[0].transform.localPosition = tmp_1;
                Debug.Log($"{difference * cnt_2}分下げました");
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
                i++;
            }
           
            //Debug.Log($"{sumTopY}が基準になります");

            return sumTopY - 0.5f;
        
        }



}
