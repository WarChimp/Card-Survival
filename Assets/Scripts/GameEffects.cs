using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffects : MonoBehaviour
{

    private static GameEffects instance;
    private int maximumIconToDisplay = 7;
    private Queue<GameObject> positions;

    private float offsetY = 0.5f;
    // Start is called before the first frame update
    [SerializeField] private GameObject[] gameEffectIcons;
    [SerializeField] private Transform iconSpawn;
    private GameObject icon;

    private void Awake()
    {
        instance = this;
        foreach (var icon in gameEffectIcons)
        {
            icon.SetActive(false);
        }

        positions = new Queue<GameObject>();
    }
    void Start()
    {
        
    }

    private void DisplayIcon(int iconToDisplay)
    {
        gameEffectIcons[iconToDisplay].SetActive(true);

    }

    private void ClearAllIcons()
    {
        foreach (var icon in gameEffectIcons)
        {
            icon.SetActive(false);
        }
    }

    private void ClearSpecificIcon(int iconToRemove)
    {
        gameEffectIcons[iconToRemove].SetActive(false);
    }

    public static void DisplayIcon_Static(int iconToDisplay)
    {
        instance.DisplayIcon(iconToDisplay);
    }

    public static void ClearIcons_Static()
    {
        instance.ClearAllIcons();
    }
    public static void ClearSpecificIcon_Static(int iconToRemove)
    {
        instance.ClearSpecificIcon(iconToRemove);
    }


}
