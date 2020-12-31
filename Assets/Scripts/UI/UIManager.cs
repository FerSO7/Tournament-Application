﻿using System.Collections.Generic;
using UnityEngine;
using TournamentsDataToLoad;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject tournamentInfoGOToInstantiate;
    private List<GameObject> GOInScrollView;

    public static event Action onRefreshButtonPressed;

    private void OnEnable()
    {
        DataHandler.onSendingDataToUI += SetDataOnScrollView;
    }
    private void OnDisable()
    {
        DataHandler.onSendingDataToUI -= SetDataOnScrollView;
    }
    private void Start()
    {
        GOInScrollView = new List<GameObject>();
    }
    private void SetDataOnScrollView(List<Tournament> tournamentsList)
    {
        foreach(Tournament tournament in tournamentsList)
        {
           GameObject tournamentGO = Instantiate(tournamentInfoGOToInstantiate, parent);
           tournamentGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tournament.Id;
           tournamentGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tournament.Date;

            GOInScrollView.Add(tournamentGO);
        }
    }

    /* Se que destruir objectos en tiempo de ejecucion no es la mejor practica, pero me parecio un poco "overkill" crear un sistema q 
      que reusara las objetos , ya q a mi parecer estas consultas a la api no se harian repetidamente*/
    public void OnClickRefresh()
    {
        foreach(GameObject go in GOInScrollView)
        {
            Destroy(gameObject);
        }

        onRefreshButtonPressed?.Invoke();

    }

}
