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
        children = getChildren(parent); //Tube_i (i:1,2,3,...)が配列になっている
        scripts = getScriptArr(children);
        int cnt = 0; //Tubeのindexと同じ
        foreach(TubeManager script in scripts){
            bool tmpBool = watchScriptsBool(scripts, cnt);
            if(tmpBool == true){
                //もし空なら代入していい。 空でないなら別の呼び出し
                if(this.start_Tube == null){
                    
                    this.start_Tube = children[cnt];
                    Debug.Log("startTubeが設定されました");
                    return;
                }
                else if(this.start_Tube != null) {
                    
                    //Tubeの中身を入れた配列
                    Transform[] tmpTube;
                    Debug.Log("endTubeが設定されました");
                    tmpTube = new Transform[children[cnt].childCount]; //Tubeの中身
                    int j = 0;
                    foreach(Transform Liquid in tmpTube) {
                        tmpTube[j] = children[cnt].GetChild(j);
                        j++;
                    }
                    //start_Tubeからend_Tubeに代入する
                    //まずstart_Tubeを参照する準備tmpArr[0]で指定出来る
                    Transform[] tmpArr;
                    tmpArr = new Transform[start_Tube.childCount];
                    tmpArr = setArray(start_Tube, tmpArr);

                    //tmpArr[0]をchildren[cnt]の一番上に追加する
                    Transform[] copyArr;
                    copyArr = new Transform[children[cnt].childCount + 1];


                    //null,children[0],children[1],  ...という形の配列を作る
                    int i = 0;
                    foreach(Transform Liquid in tmpTube ){
                        if(i == 0){
                            copyArr[0] = tmpArr[0];
                            Debug.Log($"{tmpArr[0]}を代入した");
                        }
                        else{
                            copyArr[i] = Liquid;
                            Debug.Log($"{Liquid}が代入されました");
                        }
                    }

                    //削除 代入
                    //children[cnt]の中身を削除、copyArrの中身を代入
                    int k = 0;
                    while(k < children[cnt].childCount){
                        Destroy(children[cnt].GetChild(k).gameObject);
                        k++;
                    }
                    int l = 0;
                    foreach(Transform element in copyArr){
                        Instantiate(children[cnt], element);
                        l++;
                    }
                    //initする start_Tube
                    
                    InitTransform(start_Tube);
                    
                    return;
                }
            }
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
