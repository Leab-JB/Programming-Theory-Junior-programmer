
using UnityEngine;

public class CamerController : MonoBehaviour
{
    /*
     * To zoom in or out, hold the right click button and use the scroll wheel
     */
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoom, zoomMin, zoomMax;

    // boundaries
    [SerializeField]
    private float camMinX, camMaxX, camMinY, camMaxY;

    private Vector3 dragOrigin;
    

    private void Update()
    {
        PanCamera();
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomIn();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomOut();
        }
    }

    private void PanCamera()
    {
        // save position of the mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown(1))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        // calculate distance between drag origin and new position if is still held down
        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            // move the camera by that distance

            transform.position = ClampCamera(difference);
        }

    }

    private void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoom;
        if (cam.orthographicSize < zoomMax)
        {
            camMinX += zoom;
            camMaxX -= zoom;
            camMinY += zoom;
            camMaxY -= zoom;
        }

        cam.orthographicSize = Mathf.Clamp(newSize, zoomMin, zoomMax);

        transform.position = ClampCamera(Vector3.zero);
    }

    private void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoom;
        if (cam.orthographicSize > zoomMin)
        {
            camMinX -= zoom;
            camMaxX += zoom;
            camMinY -= zoom;
            camMaxY += zoom;
        }

        cam.orthographicSize = Mathf.Clamp(newSize, zoomMin, zoomMax);

        transform.position = ClampCamera(Vector3.zero);
    }

    private Vector3 ClampCamera(Vector3 difference)
    {
        Vector3 newPosition = transform.position + difference;

        newPosition.x = Mathf.Clamp(newPosition.x, camMinX, camMaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, camMinY, camMaxY);
        newPosition.z = -129.7f;

        return newPosition;
    }


}
