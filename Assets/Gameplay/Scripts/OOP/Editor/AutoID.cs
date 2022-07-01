using UnityEditor;
using UnityEngine;

class AutoID : EditorWindow
{
    int i = 1;

    [MenuItem("Window/Auto ID")]
    public static void ShowWindow()
    {
        GetWindow<AutoID>("Auto ID");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Auto ID"))
        {
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                gameObject.GetComponent<ID>().id = i;
                i++;
            }
        }
    }
}
