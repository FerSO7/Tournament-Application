using System.Collections.Generic;
using System;

namespace TournamentsDataToLoad
{
    public class JsonDataHandler
    {
        [Serializable]
        public class TournamentJsonData
        {
            public List<TournamentData> data;            
        }

        [Serializable]
        public class TournamentData
        {
            public string id;
            public TournamentAttributes attributes;
        }

        [Serializable]
        public class TournamentAttributes
        {
            public string createdAt;           
        }
    }
}
  
