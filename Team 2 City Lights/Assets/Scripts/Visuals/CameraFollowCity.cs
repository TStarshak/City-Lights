using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraFollowCity : MonoBehaviour
{

    public GameObject player;

    public static float speed = 2.0f;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.layerCullSpherical = true;
    }

    void Update()
    {
        float interpolation = speed * Time.deltaTime * 2;

        Vector3 position = this.transform.position;
        if (SceneManager.GetActiveScene().name.Equals("City"))
        {
            position.z = Mathf.Lerp(this.transform.position.z, player.transform.position.z - 12, interpolation);
        } else
        {
            position.z = Mathf.Lerp(this.transform.position.z, player.transform.position.z - 10, interpolation);
        }
        position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, interpolation);

        this.transform.position = position;

        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

       // player.transform.Rotate(new Vector3(0,1,0), Vector3.Angle(mouse, player.transform.position));
    }
}