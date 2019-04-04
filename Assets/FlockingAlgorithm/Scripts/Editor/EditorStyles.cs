using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorStyles 
{
    static GUIStyle horizontalLine;
    static Dictionary<Color, GUIStyle> textStyles = new Dictionary<Color, GUIStyle>();
    
    public static void HorizontalLine(Color color) 
    {
        if (horizontalLine == null) {
            CreateLine();
        }

        Color c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

    private static void CreateLine()
    {
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset( 0, 0, 4, 4 );
        horizontalLine.fixedHeight = 1;
    }

    public static GUIStyle TextStyle(Color color)
    {
        if (textStyles.ContainsKey(color)) { return textStyles[color]; }

        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;

        textStyles.Add(color, style);
        return style;
    }
}