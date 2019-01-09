using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour {

    public float updateInterval = 3f;
    private float accum = 0;
    private int frames = 0;
    private float timeleft;
    private string stringFPS;
    void Start()
    {
        Application.targetFrameRate = 100;
        timeleft = updateInterval;
    }
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        //记录Update调用的次数
        ++frames;
        //到达倒计时，更新帧率
        if (timeleft<=0.0)
        {
            Debug.Log("更新");
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS",fps);
            stringFPS = format;
            //完成一次帧数记录，重新赋值
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
    void OnGUI()
    {
        GUIStyle guiStyle = GUIStyle.none;
        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.red;
    //    guiStyle.alignment = TestAnchor.Upperleft;
        Rect rt = new Rect(40,0,100,100);
        Rect RT_2 = new Rect(40,40,100,100);
        Rect RT_3 = new Rect(40, 80, 100, 100);
        Rect rt_4 = new Rect(40, 110, 100, 100);
        GUI.Label(rt,stringFPS,guiStyle);
        GUI.Label(RT_2, Time.deltaTime.ToString(),guiStyle);
        GUI.Label(RT_3, frames.ToString(), guiStyle);
        GUI.Label(rt_4, accum.ToString(), guiStyle);
    }
}
