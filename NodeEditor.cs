using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeEditor : EditorWindow
{
    private List<Rect> NodeRects = new List<Rect>();
    private Vector2 mousePos;

    [MenuItem("Window/Node Editor")]
    private static void Initialize()
    {
        GetWindow<NodeEditor>("Node Editor");
    }

    private void OnGUI()
    {
        Event e = new Event();
        mousePos = e.mousePosition;

        BeginWindows();

        for (int i = 0; i < NodeRects.Count; i++)
        {
            NodeRects[i] = GUI.Window(i, NodeRects[i], Callback, "New Node");
        }

        if(GUI.Button(new Rect(10, 10, 100, 100), "Create Node"))
        {
            Rect rect = new Rect(mousePos, new Vector2(100, 100));
            NodeRects.Add(rect);
        }

        EndWindows();
    }

    private void Callback(int id)
    {
        GUI.DragWindow();
    }
}