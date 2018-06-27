using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Grid : MonoBehaviour // TODO: Do I need it too be a mono later?
{
    [Header("Settings")]
    public float gridSize = 250; // TODO: Maybe remove
    public LayerMask gridLayer;

    public bool isCenterAtZeroZero = false;

    private Camera cam;
    private Vector3 tileCenter;
    private Vector3 minPos, maxPos;
    private Vector3 _worldMousePosition;

    private Dictionary<Vector3, GameObject> gridItems;

    // Prototype/test vars
    public Transform onOverTile;

    private Renderer gridMat;
    private float squareSize = 1f;

    private BuildManager buildManager;

    void Start() {
        cam = Camera.main;
        gridMat = GetComponent<Renderer>();
        buildManager = BuildManager.instance;

        gridItems = new Dictionary<Vector3, GameObject>();

        minPos = new Vector3((-gridSize / 2) + (squareSize / 2), 0, (-gridSize / 2) + (squareSize / 2));
        maxPos = new Vector3((-gridSize / 2) - (squareSize / 2), 0, (-gridSize / 2) - (squareSize / 2));

        // SetUpGrid(); // Shader and mesh dont like it
    }

    void Update() {
        _worldMousePosition = GetMouseWorldPosition();
        tileCenter = CalculateTilePosition(_worldMousePosition);

        // Tile overing effect handeling
        if (buildManager.GetBuildingToBuild() != null) {
            onOverTile.gameObject.SetActive(true);
            onOverTile.position = tileCenter;
        } else {
            onOverTile.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)) {
            OnGridClick();
        }
    }

    private void OnGridClick() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (buildManager.GetBuildingToBuild() == null) return; // TODO: state manager?

        var tileDicPosition = CalculateTilePosition(_worldMousePosition, true);

        if (!IsTileEmpty(tileDicPosition)) {
            Debug.LogError("Can't build there! The tile is not empty! - TODO: Display on screen.");
        }
        else {
            GameObject buildingToBuild = BuildManager.instance.GetBuildingToBuild();
            gridItems.Add(tileDicPosition, Instantiate(buildingToBuild, tileCenter, transform.rotation));
        }
    }

    private Vector3 GetMouseWorldPosition() {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 200, gridLayer)) {
            return hit.point;
        }
        else {
            Debug.LogError("Grid : No grid foud, mouse out of bound or no collider.");
            return Vector3.zero;
        }
    }

    private bool IsTileEmpty(Vector3 position) {
        return !gridItems.ContainsKey(position);
    }

    Vector3 CalculateTilePosition(Vector3 mousePosition, bool isCenterAtZeroZero = false) {
        mousePosition.y = (float)System.Math.Truncate(mousePosition.y * 100) / 100;
        var tilePosition = new Vector3(Mathf.Floor(mousePosition.x), Mathf.Floor(mousePosition.y), Mathf.Floor(mousePosition.z));

        if (!isCenterAtZeroZero)  {
            tilePosition = new Vector3(
                tilePosition.x + squareSize / 2, tilePosition.y, tilePosition.z + squareSize / 2
                );
        }

        return tilePosition;
    }


    #region Prototype/Test functions
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        DrawSquare(tileCenter);
    }

    private void OnGUI() {
        var worldMousePos = GetMouseWorldPosition();

        GUI.Label(new Rect(10, 10, 500, 20), "Mouse Positon: " + worldMousePos + " y = " + tileCenter.y);
        GUI.Label(new Rect(10, 20, 200, 20), "Tile Center: " + tileCenter);
        GUI.Label(new Rect(10, 30, 200, 20), "Floor MousePos y: " + Mathf.Floor(worldMousePos.y));
    }

    void DrawSquare(Vector3 center) {
        float xP = center.x - (squareSize / 2);
        float zP = center.z + (squareSize / 2);

        for (int x = 0; x < 2; x++) {
            Vector3 horizontal1 = new Vector3(xP, 0, zP - squareSize * x);
            Vector3 horizontal2 = new Vector3(xP + squareSize, 0, zP - squareSize * x);

            Vector3 vertical1 = new Vector3(xP + squareSize * x, 0, zP);
            Vector3 vetical2 = new Vector3(xP + squareSize * x, 0, zP - squareSize);

            Gizmos.DrawLine(horizontal1, horizontal2);
            Gizmos.DrawLine(vertical1, vetical2);
        }
    }

    void SetUpGrid() {
        transform.localScale = new Vector3(gridSize, gridSize);

        gridMat.material.mainTextureScale = new Vector2(gridSize, gridSize);
        if (isCenterAtZeroZero) {
            gridMat.material.mainTextureOffset = new Vector2(squareSize / 2, squareSize / 2);
        }
    }
    #endregion

}
