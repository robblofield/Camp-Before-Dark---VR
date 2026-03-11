using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BoilWater : MonoBehaviour
{
    [SerializeField] private float boilTime = 5f;
    [SerializeField] private GameObject steamParticles;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Rigidbody rb;

    private void OnEnable()
    {
        StartCoroutine(BoilRoutine());
    }

    private IEnumerator BoilRoutine()
    {
        if (steamParticles) steamParticles.SetActive(false);

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (grabInteractable) grabInteractable.enabled = false;

        yield return new WaitForSeconds(boilTime);

        if (steamParticles) steamParticles.SetActive(true);

        if (grabInteractable)
            grabInteractable.enabled = true;
    }

    public void ReleaseFromFire()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}