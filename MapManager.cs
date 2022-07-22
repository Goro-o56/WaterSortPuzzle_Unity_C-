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
        children = getChildren(parent); //Tube_i (i:0,1,2,3,...)が配列になっている
        //script = getScript()
        scripts = getScriptArr(children);
        bool[] boolArray = new bool[scripts.Length];
        //scriptsが格納される順番は i = 0,1,2,3,...
        for(int i = 0; i < scripts.Length; i++){
            boolArray[i] = scripts[i].click_state;
        }
        //boolArrayを調べて、trueがあればi番に関して
        for(int i = 0 ; i < boolArray.Length; i++){
            if(boolArray[i] == true){
                //もし,start_tubeが空なら
                if(start_Tube == null){
                    //startTubeに、Tube_iを代入 つまり、children[i]ということ
                    start_Tube = children[i];
                    Debug.Log($"start_Tubeに{start_Tube}が指定されました");
                    return;
                }
                //そうでないならendTubeを指定する
                if(start_Tube != null){
                    end_Tube = children[i]; //参照渡しが望ましいが、どうなってるかわからない
                    Debug.Log($"end_Tubeに{end_Tube}が指定されました"); 
                    
                    //ここから、移動 
                    //まず、tmpTubeにstart_Tubeの要素をコピーしておく。
                    Transform[] start_TubeElements;
                    start_TubeElements = new Transform[start_Tube.childCount];
                    Debug.Log($"start_Tubeの子を入れる配列として{start_TubeElements.Length}の長さの配列を作りました");

                    start_TubeElements = getChildren(start_Tube.gameObject);

                    //endTubeの中身をコピーしておく。
                    Transform[] end_TubeElements;
                    end_TubeElements = new Transform[end_Tube.childCount];
                    Debug.Log($"end_Tubeの子を入れる配列として{end_TubeElements.Length}の長さの配列を作りました");
                    
                    end_TubeElements  = getChildren(start_Tube.gameObject);

                   
                    //end_Tubeのコピーを作って、削除=>代入する
                    Transform[] end_TubeCopy = new Transform[end_Tube.childCount + 1];
                    Debug.Log($"{end_TubeCopy.Length}の長さの配列を作りました");

                    //end_TubeCopyは、start_TubeElements[0],children[0],...という形
                    for(int j = 0; j < children[i].childCount + 1; j++){
                        if(j == 0){
                            end_TubeCopy[j] = start_TubeElements[j];
                            Debug.Log($"0番目に{end_TubeCopy[0]}が代入されました");
                        }
                        if(j > 0){
                            end_TubeCopy[j] = end_TubeElements[j];
                            Debug.Log($"{end_TubeCopy}を代入した");
                        }
                    }

                    //children[i]の全要素を削除
                    int k = 0;
                    foreach(Transform child in end_Tube){
                        Destroy(child.gameObject);

                        k++;
                    }

                    //children[i]に代入する
                    int l = 0;
                    foreach(Transform element in end_TubeCopy){
                        Instantiate(end_Tube, element);
                        l++;
                    }
                    
                    //start_Tubeを削除する
                    InitTransform(start_Tube);
                    //end_Tubeを削除
                    InitTransform(end_Tube);
                    return;
                }
            }
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
