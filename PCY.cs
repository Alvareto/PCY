// Author: Ivan Grgurina

using System;
using System.Collections.Generic;
using System.Linq;

namespace Analiza_velikih_skupova_podataka_2
{
    public class PCY
    {
        const int početni_broj_ponavljanje = 1;

        static void Main(string[] args)
        {
            //učitaj broj košara
            int brKošara = int.Parse(Console.ReadLine()); // učitaj iz ulaza;
            //učitaj podatke o pragu iz ulaznih podataka
            double s = double.Parse(Console.ReadLine()); //učitaj iz ulaza; //s ima vrijednos 0-1
            //učitaj broj pretinaca
            int brPretinaca = int.Parse(Console.ReadLine());
            var prag = Math.Floor(s * brKošara); //cjelobrojna vrijednost (floor)

            List<List<int>> košare = new List<List<int>>(brKošara);
            for (int i = 0; i < brKošara; i++)
            {
                košare.Add(Console.ReadLine().Split(' ').Select(int.Parse).ToList());
            }

            //brojač predmeta
            var brPredmeta = new Dictionary<int, int>();
            //prvi prolaz
            foreach (var košara in košare)
            {
                foreach (var predmet in košara)
                {
                    if (brPredmeta.ContainsKey(predmet))
                    {
                        brPredmeta[predmet]++;
                    }
                    else
                    {
                        brPredmeta.Add(predmet, početni_broj_ponavljanje);
                    }
                }
            }

            //pretinci za funkciju sažimanja - polje veličine brPretinaca
            var pretinci = new Dictionary<int, int>();
            //drugi prolaz - sažimanje
            //za svaku​ košaru {
            foreach (var košara in košare)
            {
                //za svaki​ par predmeta i,j unutar košare {
                for (int c = 0; c < košara.Count; c++)
                {
                    var i = košara[c];
                    for (int d = c + 1; d < košara.Count; d++)
                    {
                        var j = košara[d];
                        //sažmi par predmeta u pretinac
                        if (brPredmeta[i] >= prag && brPredmeta[j] >= prag)
                        {
                            //oba predmeta moraju biti česta
                            var k = ((i * brPredmeta.Count) + j) % brPretinaca;

                            if (pretinci.ContainsKey(k))
                            {
                                pretinci[k]++;
                            }
                            else
                            {
                                pretinci.Add(k, početni_broj_ponavljanje);
                            }
                        }
                    }
                }
            }

            //treći prolaz - brojanje parova
            //mapa - ključ par predmeta [i,j], vrijednost broj ponavljanja
            var parovi = new Dictionary<Tuple<int, int>, int>(); //var ​parovi = [([i, j], zbroj)]
            foreach (var košara in košare)
            {
                //za svaki​ par predmeta i,j unutar košare {
                for (int c = 0; c < košara.Count; c++)
                {
                    var i = košara[c];
                    for (int d = c + 1; d < košara.Count; d++)
                    {
                        var j = košara[d];
                        if (brPredmeta[i] >= prag && brPredmeta[j] >= prag)
                        {
                            //oba predmeta moraju biti česta i u čestom pretincu
                            var k = ((i * brPredmeta.Count) + j) % brPretinaca;
                            if (pretinci.ContainsKey(k) && pretinci[k] >= prag)
                            {
                                var par = new Tuple<int, int>(i, j);
                                if (parovi.ContainsKey(par))
                                {
                                    parovi[par]++;
                                }
                                else
                                {
                                    parovi.Add(par, početni_broj_ponavljanje);
                                }
                            }
                        }
                    }
                }
            }

            // ukupan broj kandidata čestih parova koje bi brojao algoritam u prvom prolazu
            int m = brPredmeta.Count(i => i.Value >= prag);
            int A = (m * (m - 1)) / 2;

            int P = parovi.Count;

            Console.WriteLine(A);
            Console.WriteLine(P);

            foreach (var par in parovi.Values.OrderByDescending(i => i))
            {
                Console.WriteLine(par);
            }
        }
    }
}
