using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//Tubeの操作に関するクラス
public class TubeManager : MonoBehaviour
{
    //オブジェクトを取得    
    public GameObject parent;
    //配列作成
    Transform[] children;
        
    public bool click_state = false;
    
    public GameObject refParent; //MapManagerから参照されるParentObject

    public Stack tmpTubeStack; 
    public bool available;

    //MapObjectの
    public GameObject MapObject;
 
    
    //TubeStackを参照 を見る
    MapManager getScript(GameObject MapObject){
        MapManager script;
        script = MapObject.GetComponent<MapManager>();
        return script; //こっからTubeStackを見る
    }
    //このメソッド意味あるか？

    Stack<GameObject> getStack(MapManager script){
        Stack<GameObject> scriptStack = script.TubeStack;
        return scriptStack;
    }
    public void onClickAct(){
        //Debug.Log($"{click_state}が今の状態です");
        //if(click_state==true){
        //    click_state = false;
        //    refParent=null;
        //    //Debug.Log("falseにしました");
        //    return;
        //}
        // else if(click_state==false){
        //    click_state = true;
        //    //Debug.Log("trueにしました");
        //    refParent = onClickReturnObject(parent);//refParentに代入
        //    return;
        //}

        //Stackを用意
        MapObject = GameObject.Find("Map");
        MapManager script;
        script = getScript(MapObject);
        Stack<GameObject> TubeStack = script.TubeStack;
        
        
        //Stackへのpush
        
        if(available == true){
            //push
            TubeStack.Push(parent);
            Debug.Log($"{parent}がpushされました");
            available = false;

        }else if (available == false) {
            if((TubeStack?.Count==1)){
                //これはpopできる
                Debug.Log($"{parent}をpopする");
                TubeStack.Pop();
                available = true;

            }
            else if((TubeStack?.Count == 2)) {
                //制限として、上に物がある時はpop出来ないようにしたい parentが0番目の場合
                if(!(TubeStack.ElementAt(1) == parent)){
                    Debug.Log($"{parent}をpopする");
                    TubeStack.Pop();
                    available = true;
                } 

    

            }
            
            //out => available:trueに  もし上に物があったら出せない。これはStack.Countかつparentのindexとかで拾えそう

        }
    }
    public GameObject onClickReturnObject(GameObject parent){
        GameObject tmpObj;
        tmpObj = this.parent;
        //Debug.Log($"{this.parent}が引数{tmpObj}に代入されます。");
        return tmpObj;
    }

    // Start is called before the first frame update
    void Start()
    {
        //getChildren
        children = getChildren(parent);

        available = true;

        int counter = mergeTransforms(parent);

        SortObject(parent, counter);
       
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

        
         void SortObject(GameObject GameObject,int counter){
          
            //getChildrenして、ソート
            children = getChildren(GameObject);
            
            float Top_Y = 2.5f;


            int tubecnt = 0;
            foreach(Transform child in children){
                Debug.Log($"{parent}:{child}");
                //tubecnt == 0
                if(tubecnt == 0){
                    Vector3 tmpVec = new Vector3();
                    tmpVec.y = Top_Y - (children[tubecnt].transform.localScale.y - 1 ) * 0.5f;
                    tmpVec.x = 0;

                    children[tubecnt].transform.localPosition = tmpVec;
                    Debug.Log($"Topは{tmpVec.y}");
                }
                else if(tubecnt > 0){
                    Vector3 tmpVec = new Vector3();
                    float Top = children[tubecnt - 1].transform.localPosition.y;
                    float len = children[tubecnt - 1].transform.localScale.y * 0.5f + children[tubecnt].transform.localScale.y * 0.5f;
                    tmpVec.y = Top - len; 
                    tmpVec.x = 0;

             

                    Debug.Log($"{Top}---------{len}");
                    Debug.Log($"{tmpVec.y}にyを入れます");
                    children[tubecnt].transform.localPosition = tmpVec;
                }
                //tubecnt != 0
                tubecnt++;
            }
        }

         //mergeする関数。 childrenをみて、Transform[]を編集していく
         int mergeTransforms(GameObject GameObject){
            int counter = 0;
            children = getChildren(GameObject);
            //隣接する要素をみて、色のタグが一致するかを見る
            int i = 0;

            while (i < GameObject.transform.childCount) {
               
                 //一次的に要素を格納する

                if(i != 0){
                    //一致する可能性がある
                    if(children[i].transform.tag == children[i - 1].transform.tag){
                        //一致した
                        //ここでもう合体させてみる
                        float len = children[i].transform.localScale.y + children[i - 1].transform.localScale.y;

                        //children[i-1]の長さを編集
                        Vector3 tmpVec = children[i-1].transform.localScale;
                        tmpVec.y = len;

                        children[i-1].transform.localScale = tmpVec;

                        Destroy(children[i].transform.gameObject);
                        Debug.Log($"{i}番目と{i-1}番目が一致したので、{i}番目を削除しました");

                        counter++;
                        i++;
                    }

                }
               
                i++;

            }
            return counter;
         }



}
