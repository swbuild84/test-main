using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LepFoundation
{
    /// <summary>
    /// Геологическая скважина или разрез
    /// </summary>
    public class GeoSection
    {
        /// <summary>
        /// слои грунта начиная от поверхности 
        /// </summary>
        public List<GeoLayer> GeoLayers { get; set; }

        /// <summary>
        /// Уровень грунтовых вод, метров, считая от поверхности
        /// </summary>
        public double WaterLevel { get; set; }            

        public GeoSection()
        {
            GeoLayers = new List<GeoLayer>();
        }



        /// <summary>
        /// Расчет средневзвешенных характеристик грунта
        /// </summary>
        /// <param name="depth">глубина, до которой нужно учесть слои грунта</param>
        /// <returns>средневзвешенные характеристики от поверхности до заданной глубины с учетом грунтовых вод</returns>
        public GroundObj GetAverageGround(double depth)
        {
            Check();
            if (depth > GetTotalDepth()) throw new InvalidOperationException();
            //отделяем слои только до нужной глубины
            double Hcur = 0;
            int count = 0;
            for (int i = 0; i < GeoLayers.Count; i++)
            {
                Hcur += GeoLayers[i].Depth;
                if (Hcur > depth)
                {
                    count = i + 1;
                    break;
                }
            }
            List<GeoLayer> layers = GeoLayers.GetRange(0, count);
            //наиболее мощный слой
            GeoLayer majorLayer = layers.OrderByDescending(l => l.Depth).FirstOrDefault();

            //средневзвешенные           
            double gamma1Sum = 0;
            double gamma2Sum = 0;
            double Phi1Sum = 0;
            double Phi2Sum = 0;
            double C1Sum = 0;
            double C2Sum = 0;
            double ESum = 0;

            double hi = 0;
            Hcur = 0;
            for (int i = 0; i < layers.Count; i++)
            {
                Hcur += layers[i].Depth;
                if (i == layers.Count - 1) hi = layers[i].Depth - (Hcur - depth);
                else hi = layers[i].Depth;
                gamma1Sum += layers[i].Ground.Gamma1 * hi;
                gamma2Sum += layers[i].Ground.Gamma2 * hi;
                Phi1Sum += layers[i].Ground.Phi1 * hi;
                Phi2Sum += layers[i].Ground.Phi2 * hi;
                C1Sum += layers[i].Ground.C1 * hi;
                C2Sum += layers[i].Ground.C2 * hi;
                ESum += layers[i].Ground.E * hi;
            }
            GroundObj resGrnd = majorLayer.Ground;
            resGrnd.Gamma1 = gamma1Sum / depth;
            resGrnd.Gamma2 = gamma2Sum / depth;
            resGrnd.Phi1 = Phi1Sum / depth;
            resGrnd.Phi2 = Phi2Sum / depth;
            resGrnd.C1 = C1Sum / depth;
            resGrnd.C2 = C2Sum / depth;
            resGrnd.E = ESum / depth;
            return resGrnd;
        }

        private double GetTotalDepth()
        {
            Check();
            return GeoLayers.Sum(n => n.Depth);
        }

        private void Check()
        {
            if (GeoLayers == null) throw new NullReferenceException();
            if (GeoLayers.Count < 1) throw new Exception("Слои грунта отсутствуют");
        }
    }
}
