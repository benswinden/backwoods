﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchIn : MonoBehaviour {

    bool animating;


    void Start() {

        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInParent<TreeList>().treeList.Add(this);
    }

    public void glitchIn() {

        StartCoroutine(anim(true));
    }

    public void glitchOut() {

        StartCoroutine(anim(false));
    }

    IEnumerator anim(bool active) {

        GetComponent<MeshRenderer>().enabled = true;


        if (Random.value > 0.5f) {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = true;
        }

        if (Random.value > 0.5f) {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = true;
        }

        if (Random.value > 0.5f) {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = true;
        }

        if (Random.value > 0.5f) {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = true;
        }

        if (Random.value > 0.5f) {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

            GetComponent<MeshRenderer>().enabled = true;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
        }

        GetComponent<MeshRenderer>().enabled = active;
    }
}
