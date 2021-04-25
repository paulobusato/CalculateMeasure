using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateMeasure.UI
{
    class Ordem
    {
        public List<Barra> BarrasInseridas { get; set; } = new();
        public List<Barra> BarrasGeradas { get; set; } = new();

        public void AddBarra(Barra barra)
        {
            BarrasInseridas.Add(barra);
        }

        public void Processar()
        {
            foreach (Barra barraInserida in BarrasInseridas)
            {
                foreach (double parte in barraInserida.Partes)
                {
                    Barra barra = BarrasGeradas
                        .FirstOrDefault(b => b.Medida == barraInserida.Medida 
                                     && b.Partes.Sum() + parte <= Barra.TAMANHO_BARRA);
                    
                    if (barra == null)
                    {
                        BarrasGeradas.Add(new Barra { Medida = barraInserida.Medida, Partes = new List<double>() { parte } });
                    }
                    else
                    {
                        barra.Partes.Add(parte);
                    }
                }
            }
        }
    }

    class Barra
    {
        public const double TAMANHO_BARRA = 12;

        public double Medida { get; set; }
        public List<double> Partes { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Ordem ordem = new();
            List<Barra> barras = new()
            {
                new Barra
                {
                    Medida = 4.2,
                    Partes = GerarMedidasAleatorias(20)
                },
                new Barra
                {
                    Medida = 10.0,
                    Partes = GerarMedidasAleatorias(20)
                },
                new Barra
                {
                    Medida = 1.4,
                    Partes = GerarMedidasAleatorias(20)
                },
            };

            foreach (Barra barra in barras)
            {
                ordem.AddBarra(barra);
            }

            ordem.Processar();
        }

        static List<double> GerarMedidasAleatorias(int quantidadeMedidas)
        {
            List<double> medidasAleatorias = new();
            Random random = new();

            for (int i = 0; i < quantidadeMedidas; i++)
            {
                medidasAleatorias.Add(Math.Round(random.NextDouble() * 12, 2));
            }

            return medidasAleatorias;
        }
    }
}
