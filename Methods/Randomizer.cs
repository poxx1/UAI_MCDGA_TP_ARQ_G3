using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Properties;

namespace APIs
{
    public class Randomizer
    {
        //Generacion de los datos aleatorios
        #region Cuerpos celetes aleatorizados
        public Estrella randomStar()
        {
            var estrella = new Estrella();

            Random r = new Random();

            estrella.Tipo = "Estrella";
            estrella.Name = "Estrella" + r.Next(1, 10000); ;
            estrella.Distancia = r.Next(1, 10);
            estrella.Tamaño = r.Next(10000, 99999999);
            estrella.Temperatura = r.Next(1000, 10000);

            return estrella;
        }

        public Planeta randomPlanet()
        {
            var planeta = new Planeta();

            Random r = new Random();

            planeta.Tipo = "Planeta";
            planeta.Name = "Planeta" + r.Next(1, 10000); ;
            planeta.Tamaño = r.Next(10000, 99999999);
            planeta.Distancia = r.Next(-200, 3000);

            return planeta;
        }

        public Satelite randomSat()
        {
            var satelite = new Satelite();

            Random r = new Random();

            satelite.Tipo = "Satelite";
            satelite.Name = "Satelite" + r.Next(1, 10000); ;
            satelite.Tamaño = r.Next(10000, 99999999);
            satelite.Distancia = r.Next(-100, 2000);

            return satelite;
        }
        #endregion

        #region  Metodos para obtener las listas
        public List<Estrella> ListaEstrellas()
        {
            var listaEstrellas = new List<Estrella>();

            var r = new Random();
            int cant = r.Next(1, 10);

            for (int i = 0; i <= cant; i++)
            {
                listaEstrellas.Add(randomStar());
            }

            return listaEstrellas;
        }

        public List<Planeta> ListaPlanetas()
        {
            var listaPlanetas = new List<Planeta>();

            var r = new Random();
            int cant = r.Next(1, 10);

            for (int i = 0; i <= cant; i++)
            {
                listaPlanetas.Add(randomPlanet());
            }

            return listaPlanetas;
        }

        public List<Satelite> ListaSatelites()
        {
            var listaSatelites = new List<Satelite>();

            var r = new Random();
            int cant = r.Next(1, 10);

            for (int i = 0; i <= cant; i++)
            {
                listaSatelites.Add(randomSat());
            }

            return listaSatelites;
        }
        #endregion

        #region Outputs //Metodos para usar al entregar datos

        public OutputModel calcularOutput()
        {
            OutputModel output = new OutputModel();

            //Hago los llamados a los metodos que randomizan.
            var listStars = ListaEstrellas();
            var listPlanets = ListaPlanetas();
            var listSats = ListaSatelites();

            //Total de cada cuerpo celeste >> LINQ
            float CantidadPlanetas = listPlanets.Count;

            float CantidadEstrellas = listStars.Count;

            float CantidadSatelites = listSats.Count;

            //Promedio de distancia de cada cuerpo celeste
            float avgDistancePlanetas = 0;
            float auxDistance = 0;

            foreach (var item in listPlanets)
            {
                auxDistance += item.Distancia;
            }

            avgDistancePlanetas = auxDistance / CantidadPlanetas;


            float avgDistanceEstrellas = 0;
            auxDistance = 0;

            foreach (var item in listStars)
            {
                auxDistance += item.Distancia;

            }

            avgDistanceEstrellas = auxDistance / CantidadEstrellas;


            float avgDistanceSatelites = 0;

            auxDistance = 0;

            foreach (var item in listSats)
            {
                auxDistance += item.Distancia;

            }

            avgDistanceSatelites = auxDistance / CantidadSatelites;

            //Promedio de tamaño de cada cuerpo celeste

            float avgTamPlanetas = 0;
            float auxTam = 0;

            foreach (var item in listPlanets)
            {
                auxTam += item.Tamaño;
            }

            avgTamPlanetas = auxTam / CantidadPlanetas;

            float avgTamEstrellas = 0;
            auxTam = 0;

            foreach (var item in listStars)
            {
                auxTam += item.Tamaño;

            }

            avgTamEstrellas = auxTam / CantidadEstrellas;


            float avgTamSatelites = 0;

            auxTam = 0;

            foreach (var item in listSats)
            {
                auxTam += item.Tamaño;

            }

            avgTamSatelites = auxTam / CantidadSatelites;

            //Promedio de temperatura de las estrellas

            float avgTemperatureEstrellas = 0;
            float auxTemp = 0;

            foreach (var item in listStars)
            {
                auxTemp += item.Temperatura;

            }

            avgTemperatureEstrellas = auxTemp / CantidadEstrellas;

            output.avgDistanceEstrellas = avgDistanceEstrellas;
            output.avgDistancePlanetas = avgDistancePlanetas;
            output.avgDistanceSatelites = avgDistanceSatelites;
            output.avgTamEstrellas = avgTamEstrellas;
            output.avgTamPlanetas = avgTamPlanetas;
            output.avgTamSatelites = avgTamSatelites;
            output.avgTemperatureEstrellas = avgTemperatureEstrellas;
            output.CantidadEstrellas = CantidadEstrellas;
            output.CantidadPlanetas = CantidadPlanetas;
            output.CantidadSatelites = CantidadSatelites;
            
            return output;
        }

        //Armo el JSON para entregar para luego consumir en el client
        public string InfoCalculator(OutputModel model)
        {
            string jsonOutput = "";
            
            jsonOutput = JsonConvert.SerializeObject(model);

            return jsonOutput;
        }

        #endregion

        //public RandomizerModel randomLister()
        //{
        //    var cant = new RandomizerModel();

        //    var r = new Random();

        //    cant.planets = r.Next(1, 10);
        //    cant.sats = r.Next(1, 10); 
        //    cant.stars = r.Next(1, 10);

        //    return cant;
        //}

    }
}
