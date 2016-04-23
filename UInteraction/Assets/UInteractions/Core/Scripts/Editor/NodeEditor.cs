using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeEditor : EditorWindow
{
    private List<Node> Nodes = new List<Node>();
    private Vector2 mousePos;

    [MenuItem("Window/Node Editor")]
    private static void Initialize()
    {
        GetWindow<NodeEditor>("Node Editor");
    }

    private void OnGUI()
    {
        Event e = Event.current;
        mousePos = e.mousePosition;

        if(e.button == 1 && e.type == EventType.MouseDown)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Create Input Node"), false, CreateNode);
            menu.AddItem(new GUIContent("Create Calculation Node"), false, CreateNode);
            menu.AddItem(new GUIContent("Create Display Node"), false, CreateNode);
            menu.ShowAsContext();
        }

        BeginWindows();
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].rect = GUI.Window(i, Nodes[i].rect, Callback, "New Node");
        }
        EndWindows();
    }

    private void CreateNode()
    {
        Node node = new Node();

        Rect rect = new Rect(mousePos, new Vector2(100, 100));
        node.rect = rect;

        Nodes.Add(node);
    }

    private void Callback(int id)
    {
        GUI.DragWindow();
    }
}

[System.Serializable]
public class Node
{
    public Rect rect;
    public enum NodeTypes
    {
        Input, Display, Calculation
    }
    public NodeTypes NodeType;
}