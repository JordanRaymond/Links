using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private GameObject buildingToBuild;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one Buildmanager in the scene!");

            Destroy(this);
        }

        instance = this;
    }

    public GameObject standarBuildingPrefab;
    public GameObject anotherBuildingPrefab;
	
	void Update () {
		
	}

    public GameObject GetBuildingToBuild() {
        return buildingToBuild;
    }

    public void SetBuildingToBuild(GameObject building) {
        buildingToBuild = building;
    }
}
