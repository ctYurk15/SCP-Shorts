using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinSelector : MonoBehaviour
{
    public GameObject[] skins;

    public void activate()
    {
        for(int i = 0; i < skins.Length; i++) this.skins[i].SetActive(false);

        int index = UnityEngine.Random.Range(0, skins.Length);
        this.skins[index].SetActive(true);
    }

}
