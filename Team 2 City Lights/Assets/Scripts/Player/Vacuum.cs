using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private GameObject vacuum;
    [SerializeField] private BoxCollider vacuLampCollider;
    private ParticleSystem particles;
    public static bool isOn;
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData = PlayerState.localPlayerData;
        particles = this.transform.GetChild(transform.childCount - 2).GetComponent<ParticleSystem>();
        isOn = false;
        transform.GetChild(transform.childCount - 2).transform.Rotate(new Vector3(0, 1, 0), 90);

        // Apply upgrade multiplier to vacuLamp range
        vacuLampCollider.size = new Vector3(vacuLampCollider.size.x * playerData.vacuLampRange, vacuLampCollider.size.y, vacuLampCollider.size.z);
        // Re-center the collider if there will be an increase in range 
        if (playerData.vacuLampRange > 1){
            vacuLampCollider.center = new Vector3(vacuLampCollider.center.x + (10 * playerData.vacuLampRange), vacuLampCollider.center.y, vacuLampCollider.center.z);
            ParticleSystem.MainModule mainParticles = particles.main;
            mainParticles.startLifetime = 0.5f + playerData.vacuLampRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerData.isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                particles.Play();
                isOn = true;
                PlayerState.localPlayerData.movementSpeed /= 2;
            }
            if (Input.GetMouseButtonUp(0))
            {
                particles.Pause();
                particles.Clear();
                isOn = false;
                PlayerState.localPlayerData.movementSpeed *= 2;
            }
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                targetPoint.y = transform.GetChild(transform.childCount - 2).transform.position.y;
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.GetChild(transform.childCount - 2).transform.position);
                Vector3 rot = transform.GetChild(transform.childCount - 2).transform.rotation.eulerAngles - new Vector3(0, 90, 0);
                transform.GetChild(transform.childCount - 2).transform.rotation = Quaternion.Lerp(transform.GetChild(transform.childCount - 2).transform.rotation, targetRotation, 72.0f * Time.deltaTime);
                transform.GetChild(transform.childCount - 2).transform.Rotate(new Vector3(0, -90, 0));
            }
        }

    }

    IEnumerator moveFirefly(GameObject firefly)
    {
        firefly.GetComponent<FireflyMovement>().inVac = true;
        firefly.transform.SetPositionAndRotation(firefly.transform.position, firefly.GetComponent<FireflyMovement>().init);
        float xComp = 0;
        float zComp = 0;
        if (firefly.transform.position.x > this.transform.position.x)
            xComp = -0.1f;
       else
            xComp = 0.1f;
       if (firefly.transform.position.z > this.transform.position.z)
            zComp = -0.1f;
       else
            zComp = 0.1f;

        firefly.transform.Translate(xComp, 0, zComp);
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Firefly") && isOn)
            StartCoroutine(moveFirefly(other.gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Firefly") && isOn)
            StartCoroutine(moveFirefly(other.gameObject));
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("Firefly"))
            other.gameObject.GetComponent<FireflyMovement>().inVac = false;
    }
}

