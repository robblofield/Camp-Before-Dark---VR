using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] private GameObject emptyBucket;
    [SerializeField] private GameObject fullBucket;
    [SerializeField] private string waterSourceTag = "Water";

    private bool isFull = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isFull) return;

        if (other.CompareTag(waterSourceTag))
        {
            FillBucket();
        }
    }

    private void FillBucket()
    {
        isFull = true;

        if (emptyBucket) emptyBucket.SetActive(false);
        if (fullBucket) fullBucket.SetActive(true);

        Debug.Log("Bucket filled!");

        gameObject.tag = waterSourceTag;
    }
}