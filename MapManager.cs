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
        if(TubeStack.Count == 0){
            
        }
        if(TubeStack.Count == 1){
            
        }
        if(TubeStack.Count == 2){
            //Tube_iのGameObject
            GameObject Start_Object; //ここのParentがかわっている 
            GameObject End_Object;
            Start_Object = TubeStack.ElementAt(1); 
            Debug.Log($"{Start_Object}が代入された");
            End_Object = TubeStack.ElementAt(0);
            Debug.Log($"{End_Object}が代入された");
            GameObject MapObject = GameObject.FindWithTag("Map");
           
            //GameObjectを新たに作る

            Transform[] End_ObjectCopy;
            End_ObjectCopy = new Transform[End_Object.transform.childCount];
            for(int i = 0; i < End_Object.transform.childCount; i++) {
                End_ObjectCopy[i] = End_Object.transform.GetChild(i);
            }

            GameObject CopyEnd = Instantiate(End_Object);
            

            //CopyにObjectを追加したい
            
            Start_Object.transform.GetChild(0).SetParent(CopyEnd.transform);
            CopyEnd.transform.GetChild(End_Object.transform.childCount).SetAsFirstSibling();

            

            Destroy(End_Object);
            CopyEnd.transform.SetParent(MapObject.transform);

            //Start_Object/End_Objectのavailableをtrueにする
            TubeManager StartScript = Start_Object.GetComponent<TubeManager>();
            StartScript.available = true;


            //最後にTubeStackをクリア
            TubeStack.Pop();
            Debug.Log($"TubeStackをPopしました");
            TubeStack.Pop();
            Debug.Log($"TubeStackをPopしました");

            
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

}
