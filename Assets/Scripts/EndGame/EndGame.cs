using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Xml.Xsl;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;

    private bool _endGame = false;

    public int currentLevel = 1;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (!_endGame && p != null)
        {
            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        endGameObjects.ForEach(i => i.SetActive(true));

        foreach(var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            SaveManager.Instance.SaveLastLevel(currentLevel);
        }
    }
}
