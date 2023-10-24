using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public float effect_time = 1f;

    public void PlayEffect()
    {
        StartCoroutine("PlaySoundEffect");
    }

    IEnumerator PlaySoundEffect()
    {
        this.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(effect_time);
        Destroy(this.gameObject);
    }
}
