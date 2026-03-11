using UnityEngine;

public class FireBuildZone : MonoBehaviour
{
    public enum FireState
    {
        Empty,
        TinderPlaced,
        KindlingPlaced,
        FuelPlaced,
        Lit,
        Boiling
    }

    [SerializeField] private FireState state = FireState.Empty;

    [Header("Tags")]
    [SerializeField] private string tinderTag = "Tinder";
    [SerializeField] private string kindlingTag = "Kindling";
    [SerializeField] private string fuelTag = "Fuel";
    [SerializeField] private string flintTag = "Flint";
    [SerializeField] private string waterTag = "Water";

    [Header("Visual States")]
    [SerializeField] private GameObject emptyVisual;
    [SerializeField] private GameObject tinderVisual;
    [SerializeField] private GameObject kindlingVisual;
    [SerializeField] private GameObject fuelVisual;
    [SerializeField] private GameObject litVisual;
    [SerializeField] private GameObject boilingVisual;

    [Header("UI")]
    [SerializeField] private GameObject coachingCardRoot;

    private void Start()
    {
        UpdateVisuals();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == FireState.Empty && other.CompareTag(tinderTag))
        {
            state = FireState.TinderPlaced;
            UpdateVisuals();
            NextCard();
            Destroy(other.gameObject);
            return;
        }

        if (state == FireState.TinderPlaced && other.CompareTag(kindlingTag))
        {
            state = FireState.KindlingPlaced;
            UpdateVisuals();
            NextCard();
            Destroy(other.gameObject);
            return;
        }

        if (state == FireState.KindlingPlaced && other.CompareTag(fuelTag))
        {
            state = FireState.FuelPlaced;
            UpdateVisuals();
            NextCard();
            Destroy(other.gameObject);
            return;
        }

        if (state == FireState.FuelPlaced && other.CompareTag(flintTag))
        {
            state = FireState.Lit;
            UpdateVisuals();
            NextCard();
            Destroy(other.gameObject);
            return;
        }

        if (state == FireState.Lit && other.CompareTag(waterTag))
        {
            state = FireState.Boiling;
            UpdateVisuals();
            NextCard();
            Destroy(other.gameObject);
            

            // Turn on the boil water timer on the boiling visuals
            BoilWater boilWater = boilingVisual.GetComponentInChildren<BoilWater>(true);
            if (boilWater != null)
            {
                boilWater.enabled = true;
            }

            return;
        }
    }

    private void UpdateVisuals()
    {
        if (emptyVisual) emptyVisual.SetActive(false);
        if (tinderVisual) tinderVisual.SetActive(false);
        if (kindlingVisual) kindlingVisual.SetActive(false);
        if (fuelVisual) fuelVisual.SetActive(false);
        if (litVisual) litVisual.SetActive(false);
        if (boilingVisual) boilingVisual.SetActive(false);

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

            case FireState.Boiling:
                if (boilingVisual) boilingVisual.SetActive(true);
                break;
        }
    }

    private void NextCard()
    {
        coachingCardRoot?.SendMessage("Next", SendMessageOptions.DontRequireReceiver);
    }
}