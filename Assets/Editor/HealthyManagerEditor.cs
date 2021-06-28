

using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(HealthyManager))]
public class HealthyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HealthyManager healthyManager = (HealthyManager)target;
        if (GUILayout.Button("更新UI测试"))
        {
            healthyManager.UpdateMaxHealthy();
        }

        if (GUILayout.Button("升级测试"))
        {
            healthyManager.HealthyUp(1);
        }


    }

}
