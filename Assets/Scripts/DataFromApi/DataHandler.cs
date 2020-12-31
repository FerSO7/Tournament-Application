using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static TournamentsDataToLoad.JsonDataHandler;

namespace TournamentsDataToLoad
{
    public class DataHandler : MonoBehaviour
    {
        private const string API_URL = "https://api.pubg.com/tournaments";
        private const string API_KEY = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI2YTM3Y2JiMC0yY2Q2LTAxMzktZmFhYy00M2QwOWI1MWQyMTciLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjA5MzM3MzA4LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii0zOGE1NzY5Yi04NmRiLTRiMjItOGNmYS0wZGJhNzg2OGE1YWMifQ.ANZrll7AvRH9WLwu0PRd6N-F74ESAez302QlWqJ4pDk";
        private const string JSON_API_MEDIA_TYPE= "application/vnd.api+json";
        private string json;

        private void Start()
        {
          //  StartCoroutine(GetJSONEnumerator());
        }
        private void ProcessJsonData(string json)
        {
            TournamentJsonData tournamentJsonData = JsonUtility.FromJson<TournamentJsonData>(json);

            List<TournamentData> tournamentsList = new List<TournamentData>();
            tournamentsList = tournamentJsonData.data;
            
            foreach(TournamentData tournament in tournamentsList)
            {
                Debug.Log(tournament.id);
            }
           
        }

        private IEnumerator GetJSONEnumerator()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
            webRequest.SetRequestHeader("Authorization", API_KEY);
            webRequest.SetRequestHeader("Accept", JSON_API_MEDIA_TYPE); 
            
                          
                 yield return webRequest.SendWebRequest();

                 if (webRequest.isNetworkError)
                 {
                     print(webRequest.error);
                 }
                 else
                 {
                     json = webRequest.downloadHandler.text;
                     ProcessJsonData(json);
                    // Debug.Log(json);
                 }            
            
        }
    }

   
}

