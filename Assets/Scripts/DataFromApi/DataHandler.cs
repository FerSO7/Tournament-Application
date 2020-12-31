using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using static TournamentsDataToLoad.JsonDataHandler;

namespace TournamentsDataToLoad
{
    public class DataHandler : MonoBehaviour
    {
        #region Properties
        public List<Tournament> tournamentList { get; private set; }
        #endregion

        #region Fields
        private const string FIRST_HEADER = "Authorization";
        private const string SECOND_HEADER = "Accept";
        private const string API_URL = "https://api.pubg.com/tournaments";
        private const string API_KEY = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI2YTM3Y2JiMC0yY2Q2LTAxMzktZmFhYy00M2QwOWI1MWQyMTciLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjA5MzM3MzA4LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii0zOGE1NzY5Yi04NmRiLTRiMjItOGNmYS0wZGJhNzg2OGE1YWMifQ.ANZrll7AvRH9WLwu0PRd6N-F74ESAez302QlWqJ4pDk";
        private const string JSON_API_MEDIA_TYPE= "application/vnd.api+json";
        private string json;
        private Tournament tournament;
        private List<TournamentData> tournamentData;
        #endregion

        #region Events
        public static event Action<List<Tournament>> onSendingDataToUI;
        #endregion

        private void Start()
        {
            tournamentData = new List<TournamentData>();
            tournamentList = new List<Tournament>();
            GetDataFromJson();
        }

        private void GetDataFromJson()
        {
            StartCoroutine(GetJSONEnumerator());
        }
        private void ProcessJsonData(string json)
        {
            TournamentJsonData tournamentJsonData = JsonUtility.FromJson<TournamentJsonData>(json);            
            tournamentData = tournamentJsonData.data;
            
            foreach(TournamentData td in tournamentData)
            {              
                tournament = new Tournament(td.id,td.attributes.createdAt);
                tournamentList.Add(tournament);                
            }

            onSendingDataToUI?.Invoke(tournamentList);

            tournamentData.Clear();
            tournamentList.Clear();        
        }

        private IEnumerator GetJSONEnumerator()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);

            webRequest.SetRequestHeader(FIRST_HEADER, API_KEY);
            webRequest.SetRequestHeader(SECOND_HEADER, JSON_API_MEDIA_TYPE);

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                print(webRequest.error);
            }
            else
            {
                json = webRequest.downloadHandler.text;
                ProcessJsonData(json);
            }
        }
    }   
}

