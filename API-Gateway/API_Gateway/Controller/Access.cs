using Nancy;
using RestSharp;
using System;

namespace API_Gateway.Controller
{
    public class Access : NancyModule
    {
        public Access()
        {
            string urlPress = "http://localhost:27015";
            string urlArm = "http://localhost:27015";
            string urlConveyor = "http://localhost:27015";

            #region Arm
            Get("/v1/Arm/TurnON", x =>
            {;
                return GetRequest(urlArm + "/v1/Arm/TurnON"); ;
            });

            Get("/v1/Arm/TurnOFF", x =>
            {;
                return GetRequest(urlArm + "/v1/Arm/TurnON");              
            });

            Get("/v1/Arm/CheckState", x =>
            {;
                return GetRequest(urlArm + "/v1/Arm/TurnON");
            });
            #endregion

            #region Conveyor
            Get("/v1/Conveyour/TurnON", x =>
            {
                ;
                return GetRequest(urlConveyor + "/v1/Conveyor/TurnON"); ;
            });

            Get("/v1/Conveyour/TurnOFF", x =>
            {
                ;
                return GetRequest(urlConveyor + "/v1/Conveyor/TurnON");
            });

            Get("/v1/Conveyour/CheckState", x =>
            {
                ;
                return GetRequest(urlConveyor + "/v1/Conveyor/TurnON");
            });
            #endregion

            #region Press
            Get("/v1/Press/TurnON", x =>
            {
                ;
                return GetRequest(urlPress + "/v1/Press/TurnON"); ;
            });

            Get("/v1/Press/TurnOFF", x =>
            {
                ;
                return GetRequest(urlPress + "/v1/Press/TurnON");
            });

            Get("/v1/Press/CheckState", x =>
            {
                ;
                return GetRequest(urlPress + "/v1/Press/TurnON");
            });
            #endregion
        }
        public string GetRequest(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content.ToString();
        }
    }
}