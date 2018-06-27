using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour {
    public List<Transform> items;
    public GameObject uiPrefab;

    void Start () {
        CreatUIList();

    }
	
	void Update () {
		
	}

    public void CreatUIList() {
        for (int i = 0; i < items.Count; i++) {
            Instantiate(uiPrefab, transform, false);
        }
    }
}
