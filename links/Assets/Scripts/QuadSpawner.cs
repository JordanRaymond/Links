using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSpawner : MonoBehaviour {

    public GameObject quadPrefab;
    public int numberOfQuadsByRow = 1000;
    List<GameObject> quads = new List<GameObject>();
    public float count;

    public bool build;

	void Start () {
		
	}
	
	void Update () {
        if (build) {
            build = false;

            if (quads.Count == 0) {
                CreatQuads();
            } else {
                ClearQuads();
            }
        }

        count = quads.Count;
	}

    void CreatQuads() {
        for (int i = 0; i < numberOfQuadsByRow; i++) {
            for (int j = 0; j < numberOfQuadsByRow; j++) {
                GameObject quad = Instantiate(quadPrefab, transform);
                quad.transform.position = new Vector3(j, i, -0.5f);

                quads.Add(quad);
            }
        }

        
    }

    void ClearQuads() {
        while (quads.Count != 0) {
            GameObject quad = quads[0];

            quads.Remove(quad);
            Destroy(quad);
        }
    }
}
