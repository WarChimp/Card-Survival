using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{

    private GameManager gameScript;
    private bool _nextAnimation = true;

    [SerializeField] private Animator[] _anim;
    [SerializeField] private Text[] _resultsText;
    [SerializeField] private GameObject[] _results;
    [SerializeField] private Text dayReached;

    private void Awake()
    {
        gameScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        _resultsText[0].text = gameScript.foodAmount.ToString();
        _resultsText[1].text = gameScript.woodAmount.ToString();
        _resultsText[2].text = gameScript.tempAmount.ToString();
        dayReached.text = "You reached day: " + gameScript.dayNum.ToString();
        StartCoroutine(PlayNextStat());

    }

    IEnumerator PlayNextStat()
    {
        var animationOffset = 0f;
        for (int i = 0; i < _results.Length; i++)
        {
            if(_nextAnimation)
            {
                _nextAnimation = false;
                _anim[i].SetBool("showResults", true);
                animationOffset += 0.25f;
            }
            yield return new WaitForSeconds(0.25f + animationOffset);
            _nextAnimation = true;

        }
    }
}
