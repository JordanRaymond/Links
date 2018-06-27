using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 30f;
    public float verticalSpeed = 5f;
    public float minY = 5;
    public float maxY = 20;
    public float panBorderThickness = 10f;

    private float deltaPanSpeed;
    private float maxHeight;
    private float minHeight;
    private float maxHorizontal;
    private float minHorizontal;

    private float lastHeight;
    private float lastWidth;

    void Start () {
        CalculatePanBorders();
    }
	
	void Update () {
        if (Screen.height != lastHeight || Screen.width != lastWidth) {
            CalculatePanBorders();
        }

        HandleHorizontalMovement();
        HandleVerticalMovement(); // TODO: add camera pan? https://forum.unity.com/threads/rts-camera-script.72045/
    }

    private void HandleVerticalMovement() {
        var scrool = Input.GetAxis("Mouse ScrollWheel");
        var pos = transform.position;

        pos.y -= scrool * 1000 * verticalSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    private void HandleHorizontalMovement() {
        // TODO: Input Handler?
        if (Input.GetKey("w") || (Input.mousePosition.y >= maxHeight && Input.mousePosition.y < Screen.height)) {
            transform.Translate(Vector3.forward * deltaPanSpeed, Space.World);
        }
        if (Input.GetKey("a") || (Input.mousePosition.x <= minHorizontal && Input.mousePosition.x > 0)) {
            transform.Translate(Vector3.left * deltaPanSpeed, Space.World);
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= maxHorizontal && Input.mousePosition.x < Screen.width)) {
            transform.Translate(Vector3.right * deltaPanSpeed, Space.World);
        }
        if (Input.GetKey("s") || (Input.mousePosition.y <= minHeight && Input.mousePosition.y > 0 )) {
            transform.Translate(Vector3.back * deltaPanSpeed, Space.World);
        }
    }

    private void CalculatePanBorders() {
        lastHeight = Screen.height;
        lastWidth = Screen.width;

        deltaPanSpeed = panSpeed * Time.deltaTime;
        maxHeight = lastHeight - panBorderThickness;
        minHeight = panBorderThickness;
        maxHorizontal = lastWidth - panBorderThickness;
        minHorizontal = minHeight;
    }
}
