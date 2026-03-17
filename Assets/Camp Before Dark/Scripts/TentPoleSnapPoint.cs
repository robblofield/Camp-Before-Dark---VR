using UnityEngine;

public class TentPoleSnapPoint : MonoBehaviour
{
    [Header("Pole Visual To Update")]
    [SerializeField] private Renderer targetRenderer;
    

    [Header("Materials")]
    [SerializeField] private Material ghostMaterial;
    [SerializeField] private Material builtMaterial;

    private TentBuildZone buildZone;
    private bool isFilled = false;

    private void Awake()
    {
        buildZone = GetComponentInParent<TentBuildZone>();

        if (targetRenderer != null && ghostMaterial != null)
        {
            targetRenderer.material = ghostMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFilled)
            return;

        TentPole pole = other.GetComponentInParent<TentPole>();

        if (pole == null)
            return;

        isFilled = true;

        // Swap material on target renderer
        if (targetRenderer != null && builtMaterial != null)
        {
            targetRenderer.material = builtMaterial;
        }

        

        // Prevent double trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (buildZone != null)
            buildZone.RegisterPlacedPole();
        //  Destroy held pole and turn off own renderer
        Destroy(pole.gameObject);
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}