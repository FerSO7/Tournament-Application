using System.Collections.Generic;
using UnityEngine;
using TournamentsDataToLoad;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject tournamentInfoGOToInstantiate;

    private void OnEnable()
    {
        DataHandler.onSendingDataToUI += SetDataOnScrollView;
    }
    private void OnDisable()
    {
        DataHandler.onSendingDataToUI += SetDataOnScrollView;
    }
    private void SetDataOnScrollView(List<Tournament> tournamentsList)
    {
        foreach(Tournament tournament in tournamentsList)
        {
           GameObject tournamentGO = Instantiate(tournamentInfoGOToInstantiate, parent);
           tournamentGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tournament.Id;
           tournamentGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tournament.Date;
        }
    }

}
