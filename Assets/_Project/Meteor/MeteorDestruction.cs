using System.Collections;
using UnityEngine;

public class MeteorDestruction : MonoBehaviour
{
    private static WaitForSeconds destroyWait = new WaitForSeconds(1.5f);

    [SerializeField]
    private GameObject hitZonePrefab;
    private GameObject hitZoneObject;

    public bool WillDestroyPlanet { get; private set; }


    private void Awake() => PlaceHitZone();

    private void PlaceHitZone()
    {
        Ray ray = new Ray(transform.position, -transform.position);
        RaycastHit hit;
        int layerMask = ~(1 << gameObject.layer);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            hitZoneObject = Instantiate(hitZonePrefab, hit.point, Quaternion.identity);
            hitZoneObject.transform.forward = hit.point;
            WillDestroyPlanet = hit.transform.gameObject.layer == LayerMask.NameToLayer("Planet");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroyMeteor(triggerInteraction: true, other));
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyMeteor(triggerInteraction: false));
    }

    private IEnumerator DestroyMeteor(bool triggerInteraction, Collider other = null)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        hitZoneObject.GetComponent<ParticleSystem>().Stop();

        bool hitAnotherMeteor = false;
        if (other != null)
        {
            hitAnotherMeteor = other.gameObject.layer == gameObject.layer;
            other.GetComponent<Tree>().DestroyTree();
        }

        if (!triggerInteraction && WillDestroyPlanet && !hitAnotherMeteor)
            GameManager.Instance.Lives--;
        yield return destroyWait;
        Destroy(gameObject);
        Destroy(hitZoneObject);
    }
}
