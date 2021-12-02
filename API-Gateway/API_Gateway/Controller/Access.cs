using Nancy;
using RestSharp;
using System;
using APIs;
using Models;
using NetMQ;
using NetMQ.Sockets;
using System.Linq;

namespace API_Gateway.Controller
{
    public class Access : NancyModule
    {
        public Access()
        {
            string urlPress = "http://localhost:27018";
            string urlArm = "http://localhost:27015";
            string urlConveyor = "http://localhost:27022";

            #region Arm
            Get("/v1/Arm/TurnON", x =>
            {
                ;
                return GetRequest(urlArm + "/v1/Arm/TurnON");
            });

            Get("/v1/Arm/TurnOFF", x =>
            {
                ;
                return GetRequest(urlArm + "/v1/Arm/TurnOFF");
            });

            Get("/v1/Arm/CheckState", x =>
            {
                ;
                return GetRequest(urlArm + "/v1/Arm/CheckState");
            });

            Get("/v1/Arm/Pass", x =>
            {
                ;
                return GetRequest(urlArm + "/v1/Arm/Pass");
            });
            #endregion

            #region Conveyor
            Get("/v1/Conveyour/TurnON", x =>
            {
                ;//Socket with message TurnON
                using (var client = new RequestSocket("tcp://localhost:27022"))
                {
                    String request = "TurnON";
                    client.SendFrame(request);
                    var serverReturn = client.ReceiveFrameString();
                    Console.WriteLine(serverReturn);
                    GetRequest(urlConveyor + "/v1/Conveyor/TurnON").ToString();
                    client.Close();
                    client.Dispose();
                    return serverReturn;
                }
            });

            Get("/v1/Conveyour/TurnOFF", x =>
            {
                ;//Socket with message TurnOFF
                using (var client = new RequestSocket("tcp://localhost:27022"))
                {
                    String request = "TurnOFF";
                    client.SendFrame(request);
                    var serverReturn = client.ReceiveFrameString();
                    Console.WriteLine(serverReturn);
                    GetRequest(urlConveyor + "/v1/Conveyor/TurnOFF").ToString();
                    client.Close();
                    client.Dispose();
                    return serverReturn;
                }

            });

            Get("/v1/Conveyour/CheckState", x =>
            {
                ;//Socket with checkState
                using (var client = new RequestSocket("tcp://localhost:27022"))
                {
                    String request = "CheckStatus";
                    client.SendFrame(request);
                    var serverReturn = client.ReceiveFrameString();
                    Console.WriteLine(serverReturn);
                    GetRequest(urlConveyor + "/v1/Conveyor/CheckState");
                    client.Close();
                    client.Dispose();
                    return serverReturn;
                }

            });

            Get("/v1/Conveyour/GetBultosQuantityOnConveyor", x =>
            {
                ;
                using (var client = new RequestSocket("tcp://localhost:27022"))
                {
                    String request = "GetBultosQuantityOnConveyor";
                    client.SendFrame(request);
                    var serverReturn = client.ReceiveFrameString();
                    Console.WriteLine(serverReturn);
                    GetRequest(urlConveyor + "/v1/Conveyor/GetBultosQuantityOnConveyor");
                    client.Close();
                    client.Dispose();
                    return serverReturn;
                }
                ;

            });

            Get("/v1/Conveyour/GetBultosQuantityOnPile", x =>
            {
                ;//Socket with message TurnON
                using (var client = new RequestSocket("tcp://localhost:27022"))
                {
                    String request = "GetBultosQuantityOnPile";
                    client.SendFrame(request);
                    var serverReturn = client.ReceiveFrameString();
                    Console.WriteLine(serverReturn);
                    GetRequest(urlConveyor + "/v1/Conveyor/GetBultosQuantityOnPile");
                    client.Close();
                    client.Dispose();
                    return serverReturn;
                }
                ;
            });
            Get("/v1/Conveyour/PutBulto", x =>
            {
                ;//Socket with message TurnON
                ConeyourManagerAPI api = new ConeyourManagerAPI();

                using (var client = new RequestSocket("tcp://localhost:27022"))
                {
                    //I send bultos to the Cinta using this ZeroMQ Client.
                    //First i took one Bulto from the Pila of Bultos.
                    var list = api.getBultos();
                    Bultos bulto = list.First();
                    api.bultosDelete(bulto.IDBultoMongo);

                    //Then i send this bulto to the Server that is the Cinta
                    String request = "PutBulto";//bulto.IDBulto + "#" + bulto.IDBultoMongo; 
                    client.SendFrame(request);
                    var serverReturn = client.ReceiveFrameString();
                    Console.WriteLine(serverReturn);
                    GetRequest(urlConveyor + "/v1/Conveyor/PutBulto");
                    client.Close();
                    client.Dispose();
                    return serverReturn;
                }
                ;
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
                return GetRequest(urlPress + "/v1/Press/TurnOFF");
            });

            Get("/v1/Press/CheckState", x =>
            {
                ;
                return GetRequest(urlPress + "/v1/Press/CheckState");
            });

            Get("/v1/Press/Compress", x =>
            {
                ;
                return GetRequest(urlPress + "/v1/Press/CheckState");
            });

            Get("/v1/Press/Release", x =>
            {
                ;
                return GetRequest(urlPress + "/v1/Press/CheckState");
            });



            #endregion

            #region Bultos
            Get("/v1/Bultos/Add", x =>
            {
                ;
                return GetRequest(urlArm + "/v1/Bultos/Add"); ;
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