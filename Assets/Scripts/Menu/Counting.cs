using UnityEngine;
using UnityEngine.UI;

public class Counting : MonoBehaviour
{

    public Text deathCount;
    public Text collectableCount;

    private Collectable collectable;

    private void Start()
    {
        deathCount.text = "Deaths: " + PlayerRespawn.GetDeaths();
        collectableCount.text = "Collectables: " + Collectable.GetCollectables();
    }
}
