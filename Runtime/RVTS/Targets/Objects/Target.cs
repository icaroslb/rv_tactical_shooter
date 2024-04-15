using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Target : MonoBehaviour
{
    [SerializeField] protected AudioClip enterSound;
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioClip outSound;
    //[SerializeField] protected AudioClip moveSound;

    [SerializeField] protected AudioSource audioSource;

    public abstract void Hitted(Vector3 hitPosition);

    public void PlayEnterSound()
    {
        if (enterSound != null)
        {
            audioSource.PlayOneShot(enterSound);
        }
    }
    public void PlayHitsound()
    {
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
    public void PlayOutSound()
    {
        if (outSound != null)
        {
            audioSource.PlayOneShot(outSound);
        }
    }

    public void PlayMoveSound()
    {
        // if (moveSound != null)
        // {
        //     audioSource.PlayOneShot(moveSound);
        // }
    }
}
