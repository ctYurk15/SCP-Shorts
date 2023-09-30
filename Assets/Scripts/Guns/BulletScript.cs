using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    [Tooltip("Furthest distance bullet will look for target")]
    public float maxDistance = 1000000;
    RaycastHit hit;
    [Tooltip("Prefab of wall damange hit")]
    public GameObject decalHitWall;
    [Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
    public float floatInfrontOfWall;
    [Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
    public GameObject bloodEffect;
    [Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
    public LayerMask ignoreLayer;
    [Tooltip("Impact force on rigid-bodies")]
    public float impactForce;
    [Tooltip("Damage")]
    public float damage;

    /*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
        {
            if (decalHitWall)
            {
                GameObject bullet_damage = Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
                bullet_damage.transform.SetParent(hit.transform);

                if (hit.transform.tag == "Dummie")
                {
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (hit.transform.GetComponent<Rigidbody>() != null)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * this.impactForce);
                }
                if (hit.transform.GetComponent<DestructibleProp>() != null)
                {
                    hit.transform.GetComponent<DestructibleProp>().Damage(damage);
                }
            }
            Destroy(gameObject);
        }
        Destroy(gameObject, 0.1f);
    }

}