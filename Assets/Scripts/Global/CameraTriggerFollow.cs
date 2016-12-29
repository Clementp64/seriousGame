using UnityEngine;
using System.Collections;


public class CameraTriggerFollow : MonoBehaviour {

    public float minX, maxX, minY, maxY;
    public bool followAxisX, followAxisY;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Camera.main.GetComponent<CameraBehavior>().SetFollowAxisX(followAxisX);
            Camera.main.GetComponent<CameraBehavior>().SetFollowAxisY(followAxisY);
            Camera.main.GetComponent<CameraBehavior>().SetMinX(minX);
            Camera.main.GetComponent<CameraBehavior>().SetMaxX(maxX);
            Camera.main.GetComponent<CameraBehavior>().SetMinY(minY);
            Camera.main.GetComponent<CameraBehavior>().SetMaxY(maxY);

        }
    }
}