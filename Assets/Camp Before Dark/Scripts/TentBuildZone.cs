using UnityEngine;

public class TentBuildZone : MonoBehaviour
{
    [Header("Poles")]
    [SerializeField] private int requiredPoleCount = 5;

    [Header("Canvas Visual")]
    [SerializeField] private Renderer canvasRenderer;
    [SerializeField] private Material ghostCanvasMaterial;
    [SerializeField] private Material builtCanvasMaterial;

    private int placedPoleCount = 0;
    private bool polesComplete = false;
    private bool canvasPlaced = false;
    private bool tentComplete = false;

    public bool PolesComplete => polesComplete;
    public bool CanvasPlaced => canvasPlaced;
    public bool TentComplete => tentComplete;

    private void Start()
    {
        if (canvasRenderer != null && ghostCanvasMaterial != null)
            canvasRenderer.material = ghostCanvasMaterial;
    }

    public void RegisterPlacedPole()
    {
        if (tentComplete)
            return;

        placedPoleCount++;

        if (placedPoleCount >= requiredPoleCount)
        {
            polesComplete = true;
            Debug.Log("All tent poles placed.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!polesComplete)
            return;

        if (canvasPlaced)
            return;

        TentCanvas canvas = other.GetComponentInParent<TentCanvas>();

        if (canvas == null)
            return;

        canvasPlaced = true;

        if (canvasRenderer != null && builtCanvasMaterial != null)
            canvasRenderer.material = builtCanvasMaterial;

        Destroy(canvas.gameObject);

        CheckForTentComplete();
    }

    private void CheckForTentComplete()
    {
        if (tentComplete)
            return;

        if (polesComplete && canvasPlaced)
        {
            tentComplete = true;
            Debug.Log("Tent complete.");
        }
    }
}