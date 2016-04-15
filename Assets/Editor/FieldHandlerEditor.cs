using UnityEngine;
using UnityEditor;
using System.Collections;
using Quinton;

[CustomEditor(typeof(FieldHandler), true)]
public class FieldHandlerEditor : Editor
{

    public override void OnInspectorGUI()
    {

        FieldHandler script = (FieldHandler)target;

        if (GUILayout.Button("Resolve Field"))
        {
            ResolutionInfo ri =  script.Resolve();

            MonoBehaviour.print(ri.Message);

        }
        if(GUILayout.Button("View Info"))
        {
            FieldHandlerWindow.InitializeWindow(script.GatherFieldInfo());
        }
        DrawDefaultInspector();
    }
}


public class FieldHandlerWindow : EditorWindow
{
    static FieldInfo fieldInfo;

    public static void InitializeWindow(FieldInfo fi)
    {
        

        var window = GetWindow(typeof(FieldHandlerWindow));


        fieldInfo = fi;
        
    }

    void OnGUI()
    {
        if (fieldInfo == null)
            Close();

        GUILayout.Label("Field Info", EditorStyles.largeLabel);
        GUILayout.Label(" ");
        GUILayout.Label("Last Message: " + fieldInfo.LastMessage);
        GUILayout.Label(" ");
        GUILayout.Label("Good Dudes:", EditorStyles.boldLabel);

        foreach (GameObject go in fieldInfo.GoodDudes)
        {
            if (go.GetComponent<MysteryCardMono>())
            {
                MysteryCardMono m = go.GetComponent<MysteryCardMono>();
                GUILayout.Label(m.Name + " : " + m.Description + " | " + "Power: " + m.Power + " | " + "Reward: " + m.Reward, EditorStyles.label);

            }
        }
        GUILayout.Label(" ");
        GUILayout.Label("Bad Dudes:", EditorStyles.boldLabel);

        foreach (GameObject go in fieldInfo.BadDudes)
        {
            if (go.GetComponent<MysteryCardMono>())
            {
                MysteryCardMono m = go.GetComponent<MysteryCardMono>();
                GUILayout.Label(m.Name + " : " + m.Description + " | " + "Power: " + m.Power + " | " + "Reward: " + m.Reward, EditorStyles.label);

            }
        }
    }

}