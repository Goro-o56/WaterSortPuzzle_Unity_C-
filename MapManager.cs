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

        //この関数はどのTubeから呼び出された？

        TubeDetectoronTouch(TubeId);


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
