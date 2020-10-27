using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCamera : Singleton<BoundCamera>
{

    public float tw = 2960;
    public float th = 1440;

    public GameObject target;
    [HideInInspector]public Transform targetTransform;

    public Vector2 margins; // 여백
    public Vector2 maxBounds; // 최대 카메라 거리
    public Vector2 minBounds; // 최소 카메라 거리

    private void Update()
    {
        Vector2 target = new Vector2(transform.position.x, transform.position.y);
        if (CheckXMargin())
        {
            target.x = Mathf.Lerp(transform.position.x, targetTransform.position.x, 8.0f * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            target.y = Mathf.Lerp(transform.position.y, targetTransform.position.y, 8.0f * Time.deltaTime);
        }

        target.x = Mathf.Clamp(target.x, minBounds.x, maxBounds.x);
        target.y = Mathf.Clamp(target.y, minBounds.y, maxBounds.y);

        transform.position = new Vector3(target.x, target.y, transform.position.z);
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - targetTransform.position.x) > margins.x;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - targetTransform.position.y) > margins.y;
    }

    void Awake()
    {
        targetTransform = target.transform;

        //타겟해상도(가로 1280, 세로 720, 비율).
        float tsr = tw / th;

        //실제 기기의 해상도(가로, 세로, 비율).
        float sw = (float)Screen.width;
        float sh = (float)Screen.height;
        float sr = sw / sh;   //화면 비율.

        //실제사이즈와 타겟사이즈의 비율을 계산함(옆으로 큰지 위로 큰지 계산함)
        float size = sr - tsr;

        //실제 기기의 화면 비율이 타겟비율과 비슷함(거의 같음).c
        if (Mathf.Abs(size) <= 0.01f)
        {
            //최대 해상도 제한함.
            if (sh >= th)
            {
                sh = th;
                sw = tw;
            }
        }
        else
        {
            //화면이 옆으로 길다.
            if (size > 0.0f)
            {
                //최대 해상도 제한함.
                if (sh >= th)
                {
                    sh = th;
                    sw = sh * sr;
                }
            }
            else
            {
                //최대 해상도 제한함.
                if (sw >= tw)
                {
                    sw = tw;
                    sh = sw / sr;
                }
            }
        }

        //게임 카메라의 ViewportRect 값을 조절한다. 어떤 해상도가 된다고해도 비율이 일정하게 출력됨.
        float vh = th * sw / tw / sh;
        float vw = tw * sh / th / sw;

        //이단계에서 카메라 ViewportRect 에 적용한다.
        this.GetComponent<Camera>().rect = new Rect(((1.0f - vw) * 0.5f), ((1.0f - vh) * 0.5f), vw, vh);

        Screen.SetResolution((int)sw, (int)sh, true);
    }
}
