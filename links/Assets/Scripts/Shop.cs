using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public BuildingBlueprint normalBuilding;
    public BuildingBlueprint secondBuilding;

    private BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.instance;
    }

    public void PurchaseExampleItem() {
        Debug.Log("PurchaseExampleItem");
        buildManager.SetBuildingToBuild(buildManager.standarBuildingPrefab);
    }

    public void PurchaseExampleItemTwo() {
        Debug.Log("PurchaseExampleItem");
        buildManager.SetBuildingToBuild(buildManager.anotherBuildingPrefab);

    }
}
