using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MapManager : MonoBehaviour
{
    //子オブジェクトを取得する
    public GameObject parent;

    Transform[] children;

    TubeManager[] scripts;

    public Stack<GameObject> TubeStack;
    // Start is called before the first frame update
    void Start()
    {
        TubeStack = new Stack<GameObject>(2);
        TubeId = defactoNull;
        
    }
   
    // Update is called once per frame
    void Update()
    {
        //Stackが満タンなら、移動
        if(TubeStack != null){
        if(TubeStack.Count == 2){
            //Tube_iのGameObject
            GameObject Start_Object;
            GameObject End_Object;
            Start_Object = TubeStack.ElementAt(0); 
            Debug.Log($"{Start_Object}が代入された");
            End_Object = TubeStack.ElementAt(1);
            Debug.Log($"{End_Object}が代入された");

            if(End_Object != null){
                string name = End_Object.name;
            }
            
            //End_Objectを削除する
            GameObject DestroyObject = GameObject.Find($"{name}");
            Destroy(DestroyObject);

            //End_ObjectにStart_Objectの一番上を入れる
            Transform[] StartObjectCopy = getChildren(Start_Object);
            Transform[] EndObjectCopy = getChildren(End_Object);

            Transform[] EndObjectPlusOne = new Transform[End_Object.transform.childCount + 1];
            GameObject parentTube = new GameObject();

            
            for(int i = 0; i < End_Object.transform.childCount + 1; i++ ){
                if (i == 0){
                   EndObjectPlusOne[i] = StartObjectCopy[i];
                }
                else if (i < End_Object.transform.childCount ){
                    EndObjectPlusOne[i] = EndObjectCopy[i];   
                }
            }

            parentTube.name = name;

            Instantiate(parentTube);
            parentTube.transform.SetParent(this.parent.transform);
            //parentTubeを親にする
            int cnt = 0;
            if((EndObjectPlusOne != null) && (parentTube != null)){
                foreach (Transform child in EndObjectPlusOne){
                child.SetParent(parentTube.transform);
                cnt++;
                }
            }
           

            

            //最後にTubeStackをクリア
            TubeStack.Pop();
            TubeStack.Pop();
            return;


            
        }
        }
    }


    
    public void callonEvent(){
        children = getChildren(parent); //Tube_i (i:0,1,2,3,...)が配列になっている
        //script = getScript()
        scripts = getScriptArr(children);
        bool[] boolArray = new bool[scripts.Length];
        //scriptsが格納される順番は i = 0,1,2,3,...
        for(int i = 0; i < scripts.Length; i++){
            boolArray[i] = scripts[i].click_state;
        }
    }

    
    private int TubeId;
   
    private int defactoNull = 1000000;
    public void TubeDetectoronTouch(int tmpTubeId) {
        //これをタッチされた時にUnity側で呼び出す 
           this.TubeId = tmpTubeId;
    }

    void NullSetTubeId(int TubeId){
        this.TubeId = defactoNull;
    }

    GameObject watchScriptsTube(int i ){ //i番目のScriptのTubeを覗く
        GameObject tmpObject = null;
        tmpObject = scripts[i].refParent;
        return tmpObject;
    }

    //getScriptArr
    TubeManager[] getScriptArr(Transform[] children){
        TubeManager[] scripts;
        scripts = new TubeManager[children.Length];


        int cnt = 0; //これはchildren.countまで動く
        foreach(Transform child in children){
            scripts[cnt] = child.GetComponent<TubeManager>();
            cnt++;
        }
        return scripts;
    }

    public Transform[] getChildren(GameObject parent){
        children = new Transform[parent.transform.childCount];

        int count = 0;
        foreach(Transform child in parent.transform) {
            children[count] = child;
            count++;
        }
        return children; 
    }

    //bool値に対して処理をするような関数
    public Transform start_Tube;
    
    public Transform end_Tube;
    public Transform[] start_TubeChildren;
    public Transform[] end_TubeChildren;

    Transform[] setArray(Transform start_Tube, Transform[] tmpArr){
       
        tmpArr = getChildren(start_Tube.gameObject);        
        return tmpArr;
    }


    void InitTransform(Transform transform){
        transform = null;
    }

}
