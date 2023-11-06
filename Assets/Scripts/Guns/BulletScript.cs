using UnityEngine;
using System.Collections;
using System.Linq;

public class BulletScript : MonoBehaviour
{

    [Tooltip("Furthest distance bullet will look for target")]
    public float maxDistance = 1000000;
    RaycastHit hit;
    //[Tooltip("Prefab of wall damange hit")]
    //public GameObject decalHitWall;
    [Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
    public float floatInfrontOfWall;
   /* [Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
    public GameObject bloodEffect;*/
    [Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
    public LayerMask ignoreLayer;
    [Tooltip("Impact force on rigid-bodies")]
    public float impactForce;
    [Tooltip("Damage")]
    public float damage;

    [Header("Bullet holes")]
    public GameObject wooden_bullet_hole;
    public GameObject metal_bullet_hole;
    public GameObject concrete_bullet_hole;
    public GameObject sand_bullet_hole;
    public GameObject default_bullet_hole;
    public string[] tags_to_avoid = { };

    /*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
        {
            //if (decalHitWall)
            {
                GameObject decalHitWall = null;
                string object_tag = hit.transform.tag;

                if(!tags_to_avoid.Contains(object_tag))
                {
                    switch (object_tag)
                    {
                        case "Wood":
                            decalHitWall = wooden_bullet_hole;
                            break;
                        case "Sand":
                            decalHitWall = sand_bullet_hole;
                            break;
                        case "Metal":
                            decalHitWall = metal_bullet_hole;
                            break;
                        case "Concrete":
                            decalHitWall = concrete_bullet_hole;
                            break;
                        default:
                            decalHitWall = default_bullet_hole;
                            break;
                    }

                    GameObject bullet_damage = Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
                    bullet_damage.transform.SetParent(hit.transform);
                    bullet_damage.GetComponent<RandomSkinSelector>().activate();
                }

                /*if (hit.transform.tag == "Dummie")
                {
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }*/
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