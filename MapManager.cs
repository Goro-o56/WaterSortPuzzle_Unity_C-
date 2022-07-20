using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //子オブジェクトを取得する
    public GameObject parent;

    Transform[] children;

    TubeManager[] scripts;


    // Start is called before the first frame update
    void Start()
    {

    }
   
    // Update is called once per frame
    void Update()
    {
    }



    public void callonEvent(){
        children = getChildren(parent);
        scripts = getScriptArr(children);
        int cnt = 0;
        foreach(TubeManager script in scripts){
            bool tmpBool = watchScriptsBool(scripts, cnt);

            Debug.Log($"{cnt}番目のTubeは{tmpBool}");
            cnt++;
        }
    }
    bool watchScriptsBool(TubeManager[] scripts, int cnt){ //scripts.boolを見て、そのbool値を返す
        bool  tmpBool;
        tmpBool = scripts[cnt].click_state;
        return tmpBool;
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
        children = new Transform[this.parent.transform.childCount];

        int count = 0;
        foreach(Transform child in this.parent.transform) {
            children[count] = child;
            count++;
        }
        return children; 
    }

        //bool値に対して処理をするような関数
    public Transform start_obj;
    public List<Transform> end_obj = new List<Transform>();

    void SetTubeByBool(bool Bool,Transform tmpObj, Transform start_obj,Transform end_obj){
        //start_objに何も入ってない

        if(start_obj == null) {
            start_obj = tmpObj;
        }
        else if(start_obj != null){
            if (end_obj == null){
                end_obj = tmpObj;
                //move
                moveTube(start_obj,end_obj);
            }
        }
    }

    void moveTube(Transform start_obj, Transform end_obj){
        //一番上の者をend_objに持ってくる
        Transform tmpObj = start_obj.root;
        //それをend_objの一番上に代入する。
        Instantiate(tmpObj, start_obj);
        Destroy(start_obj.root);
    }

    void Init(Transform start_obj, Transform end_obj){
        start_obj = null;
        end_obj = null;
    }

}
