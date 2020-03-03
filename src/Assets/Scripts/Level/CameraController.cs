using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Level
{
    public class CameraController : MonoBehaviour
    {
        public float panSpeed = 30f;
        public float panBoarderThickness = 10f;

        public float scrollSpeed = 5f;
        public float minY = 10f;
        public float maxY = 80f;

        public float rotationSpeed = 3f;
        
        // Update is called once per frame
        private void Update()
        {
            if (Input.mousePosition.y > Screen.height || Input.mousePosition.y < 0 ||
                Input.mousePosition.x > Screen.width || Input.mousePosition.x < 0)
                return;
        
            // Moving
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
                Input.mousePosition.y >= Screen.height - panBoarderThickness)
                transform.Translate(Vector3.forward * (panSpeed * Time.deltaTime), Space.World);

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 0 + panBoarderThickness)
                transform.Translate(Vector3.back * (panSpeed * Time.deltaTime), Space.World);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 0 + panBoarderThickness)
                transform.Translate(Vector3.left * (panSpeed * Time.deltaTime), Space.World);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBoarderThickness)
                transform.Translate(Vector3.right * (panSpeed * Time.deltaTime), Space.World);

            // Zooming
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Vector3 pos = transform.position;
            pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;
            
            // Rotation
            bool rightClicked = Input.GetMouseButton(1);
            if (rightClicked) 
            {
                transform.RotateAround (transform.position, Vector3.up, Input.GetAxis ("Mouse X") * rotationSpeed * -1);
                // transform.RotateAround (transform.position, Vector3.left, Input.GetAxis ("Mouse Y") * rotationSpeed * -1);
            }
        }
    }
}
