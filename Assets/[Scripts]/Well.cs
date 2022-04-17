using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    public GameObject effects;
    public bool canUse = true;

    bool playerInRange;
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.Find("InteractSoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<MovementComponent>().interactObject = this.gameObject;
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MovementComponent>().interactObject = null;
            playerInRange = false;
        }
    }

    public void Use()
    {
        effects.SetActive(false);
        AudioSource.PlayClipAtPoint(soundManager.GetSound("WellUse"), transform.position);
        StartCoroutine(Refresh());
    }

    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(120f);
        effects.SetActive(true);
        canUse = true;
    }
}
