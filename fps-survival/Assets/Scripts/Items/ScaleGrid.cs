// Scale your grid layougt in Unity3D
// -- Add this to a gameobject with a Grid Layout Group and you can make the cell size change with resolution, by percentage of screen width.
// -- Updates as you change resolution or width percentage in editor.
// -- Runs once on play, Fix will need to be called if resolution is changed in game.
// -- Script will need expansion if you want to use non-square cells.



// I modified this script that is from https://gist.github.com/bhison/7aa45c7ff56ba692cebf828c374b8192 to scale padding and spacing too
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScaleGrid : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    public float widthPercentage;
    [SerializeField]
    [Range(0, 1)]
    public float heightPercentage;
    [Range(0, 0.005f)]
    public float spacingXPercentage;
    [Range(0, 0.005f)]
    public float spacingYPercentage;
    [Range(0, 0.01f)]
    public float paddingTopPercentage;
    [Range(0, 0.01f)]
    public float paddingBottomPercentage;
    [Range(0, 0.01f)]
    public float paddingLeftPercentage;
    [Range(0, 0.01f)]
    public float paddingRightPercentage;

    float lWidthPercentage = 0;
    float lHeightPercentage = 0;
    float lWidthScalePercentage = 0;
    float lHeightScalePercentage = 0;
    float lPaddingTopPercentage = 0;
    float lPaddingBottomPercentage = 0;
    float lPaddingLeftPercentage = 0;
    float lPaddingRightPercentage = 0;

    Vector2 viewSize = Vector2.zero;

    void Start()
    {
        Fix();
    }

    void Update()
    {
#if UNITY_EDITOR
        //This is used to detect whether in editor view resolution has changed
        if (Application.isPlaying) return;
        if (GetMainGameViewSize() != viewSize || widthPercentage != lWidthPercentage || heightPercentage != lHeightPercentage 
            || spacingXPercentage != lWidthScalePercentage || spacingYPercentage != lHeightScalePercentage || lPaddingTopPercentage != paddingTopPercentage 
            || lPaddingBottomPercentage != paddingBottomPercentage || lPaddingLeftPercentage != paddingLeftPercentage || lPaddingRightPercentage != paddingRightPercentage)
        {
            Fix();
            viewSize = GetMainGameViewSize();
            lWidthPercentage = widthPercentage;
            lHeightPercentage = heightPercentage;
            lWidthScalePercentage = spacingXPercentage;
            lHeightScalePercentage = spacingYPercentage;
            lPaddingTopPercentage = paddingTopPercentage;
            lPaddingBottomPercentage = paddingBottomPercentage;
            lPaddingLeftPercentage = paddingLeftPercentage;
            lPaddingRightPercentage = paddingRightPercentage;
        }
#endif
    }

    public void Fix()
    {
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
        var width = (float)GetMainGameViewSize().x;
        var valWidth = (int)Mathf.Round(width * widthPercentage);
        var valHeight = (int)Mathf.Round(width * heightPercentage);
        var valScaleX= (int)Mathf.Round(width * spacingXPercentage);
        var valScaleY = (int)Mathf.Round(width * spacingYPercentage);
        var valPadT = (int)Mathf.Round(width * paddingTopPercentage);
        var valPadB = (int)Mathf.Round(width * paddingBottomPercentage);
        var valPadL = (int)Mathf.Round(width * paddingLeftPercentage);
        var valPadR = (int)Mathf.Round(width * paddingRightPercentage);
        grid.cellSize = new Vector2(valWidth, valHeight);
        grid.spacing = new Vector2(valScaleX, valScaleY);
        grid.padding.top = valPadT;
        grid.padding.bottom = valPadB;
        grid.padding.left = valPadL;
        grid.padding.right = valPadR;
        //Toggle enabled to update screen (is there a better way to do this?)
        grid.enabled = false;
        grid.enabled = true;
    }

    public static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }
}