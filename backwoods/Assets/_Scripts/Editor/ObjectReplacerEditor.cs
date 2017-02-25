using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectReplacer))]
public class ObjectReplacerEditor : Editor {
    
    public override void OnInspectorGUI() {

        // Show default inspector property editor
        DrawDefaultInspector();


        EditorGUILayout.Space();

        if (GUILayout.Button("Build Object")) {
            replace();
        }
    }

    void replace() {

        
        ObjectReplacer _target = (ObjectReplacer)target;

        foreach (GameObject obj in _target.objectList) {
            
            GameObject newObj = Instantiate(_target.objectToReplace) as GameObject;

            newObj.transform.position = obj.transform.position;
            newObj.transform.rotation = obj.transform.rotation;
            newObj.transform.localScale = obj.transform.localScale;

            newObj.transform.parent = obj.transform.parent;

            _target.objectList.Remove(obj);
            DestroyImmediate(obj);
        }
    }
}