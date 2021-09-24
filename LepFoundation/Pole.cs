namespace LepFoundation
{
    public class Pole
    {
        /// <summary>
        /// глубина заделки стойки в грунт, м 
        /// </summary>
        public double h { get; set; }

        /// <summary>
        /// средняя ширина стоики в грунте, м
        /// </summary>
        public double b0 { get; set; }

        /// <summary>
        /// высота банкетки, м
        /// </summary>
        public double hb { get; set; }

        /// <summary>
        /// Тип опоры
        /// </summary>
        public Enums.ESupportType SupportType { get; set; }

        /// <summary>
        /// установка стойки в сверленый или копаный котлован
        /// </summary>
        public bool IsDrilled { get; set; } = true;


    }
}
