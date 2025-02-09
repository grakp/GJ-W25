using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    private static int collectablesCollected = 0;
    public Text collectablesCollectedText;
    private AudioSource collectSound;

    private void Start()
    {
        collectablesCollectedText = GameObject.Find("CollectablesCollectedText").GetComponent<Text>();
        UpdateCollectablesCollectedText();
        collectSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collectablesCollected++;
            UpdateCollectablesCollectedText();
            collectSound.Play();

            Destroy(gameObject);
        }
    }

    private void UpdateCollectablesCollectedText()
    {
        if (collectablesCollectedText != null)
        {
            collectablesCollectedText.text = "x " + collectablesCollected;
        }
    }
}
