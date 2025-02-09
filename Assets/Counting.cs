using UnityEngine;
using UnityEngine.UI;

public class Counting : MonoBehaviour
{

    public Text deathCount;
    public Text collectableCount;

    private Collectable collectable;
    private PlayerRespawn player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathCount.text = "Deaths: " + PlayerRespawn.GetDeaths();
        collectableCount.text = "Collectables: " + Collectable.GetCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
