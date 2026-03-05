using UnityEngine;

public class FireBuildZone : MonoBehaviour
{
    public enum FireState { Empty, TinderPlaced, KindlingPlaced, FuelPlaced, Lit }

    [SerializeField] private FireState state = FireState.Empty;

    [Header("Tags")]
    [SerializeField] private string tinderTag = "Tinder";
    [SerializeField] private string kindlingTag = "Kindling";
    [SerializeField] private string fuelTag = "Fuel";
    [SerializeField] private string flintTag = "Flint";

    [Header("Fire Visual States (Children of Prefab)")]
    [SerializeField] private GameObject emptyVisual;
    [SerializeField] private GameObject tinderVisual;
    [SerializeField] private GameObject kindlingVisual;
    [SerializeField] private GameObject fuelVisual;
    [SerializeField] private GameObject litVisual;

    [Header("Consume Items")]
    [SerializeField] private bool destroyItemOnUse = true;

    [Header("Coaching Cards (Optional)")]
    [SerializeField] private GameObject coachingCardRoot;

    private void Start()
    {
        // Ensure we start in Empty state visually
        SetStartingState(FireState.Empty);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == FireState.Empty && other.CompareTag(tinderTag))
        {
            Use(other.gameObject);
            SetState(FireState.TinderPlaced);
            return;
        }

        if (state == FireState.TinderPlaced && other.CompareTag(kindlingTag))
        {
            Use(other.gameObject);
            SetState(FireState.KindlingPlaced);
            return;
        }

        if (state == FireState.KindlingPlaced && other.CompareTag(fuelTag))
        {
            Use(other.gameObject);
            SetState(FireState.FuelPlaced);
            return;
        }

        if (state == FireState.FuelPlaced && other.CompareTag(flintTag))
        {
            Use(other.gameObject);
            SetState(FireState.Lit);
            return;
        }
    }

    
    private void SetState(FireState newState)
    {
        state = newState;
        Debug.Log("FireState is set to: " + state);
        UpdateVisuals();
        NextCard();
    }

    // Same as above but this one doesn't update the visuals or advance the ui card for our starting state
    private void SetStartingState(FireState newState)
    {
        state = newState;
        Debug.Log("FireState is set to: " + state);
    }

    private void UpdateVisuals()
    {
        // Turn everything off first
        if (emptyVisual) emptyVisual.SetActive(false);
        if (tinderVisual) tinderVisual.SetActive(false);
        if (kindlingVisual) kindlingVisual.SetActive(false);
        if (fuelVisual) fuelVisual.SetActive(false);
        if (litVisual) litVisual.SetActive(false);

        // Turn on correct one
        switch (state)
        {
            case FireState.Empty:
                if (emptyVisual) emptyVisual.SetActive(true);
                break;

            case FireState.TinderPlaced:
                if (tinderVisual) tinderVisual.SetActive(true);
                break;

            case FireState.KindlingPlaced:
                if (kindlingVisual) kindlingVisual.SetActive(true);
                break;

            case FireState.FuelPlaced:
                if (fuelVisual) fuelVisual.SetActive(true);
                break;

            case FireState.Lit:
                if (litVisual) litVisual.SetActive(true);
                break;
        }
    }

    private void NextCard()
    {
        coachingCardRoot?.SendMessage("Next", SendMessageOptions.DontRequireReceiver);
    }

    private void Use(GameObject item)
    {
        if (destroyItemOnUse && item != null)
            Destroy(item);
    }
}