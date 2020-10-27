using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyButton : MonoBehaviour {

    public Color sniperColor;
    public Color originColor;
    public Button modifyBtn;

    private ColorBlock colorBlock;
    private KSO kso;

    void Start() {
        colorBlock = modifyBtn.colors;
        kso = FindObjectOfType<KSO>();
        Debug.Log(kso);
    }

    void Update() {
        ChangeColor();
    }

    void ChangeColor() {
        if (kso.GetCurrentMode())
            colorBlock.normalColor = sniperColor;
        else
            colorBlock.normalColor = originColor;

        modifyBtn.colors = colorBlock;
    }
}
