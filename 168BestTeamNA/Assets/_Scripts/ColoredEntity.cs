using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColoredEntity : MonoBehaviour
{
    public enum EColor { Gray = 0, Red, Blue, Green, Yellow  };
    public static Material[] materialList;
    public EColor curColor;

    private void Awake()
    {
        materialList = new Material[5];
        //materialList[0] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Red.mat");
        //materialList[1] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Blue.mat");
        //materialList[2] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Green.mat");
        //materialList[3] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Yellow.mat");
        //materialList[4] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Gray.mat");

        materialList[0] = Resources.Load<Material>("Gray");
        materialList[1] = Resources.Load<Material>("Red");
        materialList[2] = Resources.Load<Material>("Blue");
        materialList[3] = Resources.Load<Material>("Green");
        materialList[4] = Resources.Load<Material>("Yellow");

        //materialList[1] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Blue.mat");
        //materialList[2] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Green.mat");
        //materialList[3] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Yellow.mat");
        //materialList[4] = AssetDatabase.LoadAssetAtPath<Material>("Assets/Art/Materials/Gray.mat");


        if (this.gameObject.GetComponentInChildren<Renderer>() != null)
        {
            this.gameObject.GetComponentInChildren<Renderer>().material = materialList[(int)curColor];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReColor(EColor newColor)
    {
        curColor = newColor;
        if (this.gameObject.GetComponentInChildren<Renderer>() != null)
        {
            this.gameObject.GetComponentInChildren<Renderer>().material = materialList[(int)curColor];
        }
    }
}
