using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeList : MonoBehaviour {
    
    public List<GlitchIn> treeList { get; set; }

    public List<GlitchIn> treeListIn { get; set; }

    void Awake() {

        treeList = new List<GlitchIn>();
        treeListIn = new List<GlitchIn>();
    }

    void Update() {

        if (Input.GetKeyUp(KeyCode.RightShift)) {

            var num = Random.Range(0, treeList.Count);

            treeList[num].glitchIn();
            treeListIn.Add(treeList[num]);
            treeList.RemoveAt(num);
        }

        if (Input.GetKeyUp(KeyCode.End)) {

            var num = Random.Range(0, treeListIn.Count);

            treeListIn[num].glitchOut();
            treeList.Add(treeListIn[num]);
            treeListIn.RemoveAt(num);
        }
    }
}
