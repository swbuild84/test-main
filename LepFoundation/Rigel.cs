namespace LepFoundation
{
    public class Rigel
    {
        /// <summary>
        /// Длина ригеля, м
        /// </summary>
        public double L { get; set; }

        /// <summary>
        /// Ширина ригеля, м
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Толщина ригеля, м
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Привязка ригеля от уровня планировки, м
        /// </summary>
        public double Yr { get; set; }

        /// <summary>
        /// Объем, м3
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Масса, т
        /// </summary>
        public double Weight { get; set; }
    }
}
