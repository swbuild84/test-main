namespace LepFoundation
{
    /// <summary>
    /// Геологический слой
    /// </summary>
    public class GeoLayer
    {
        /// <summary>
        /// тип грунта
        /// </summary>
        public GroundObj Ground { get; set; }
        /// <summary>
        /// мощность слоя, м
        /// </summary>
        public double Depth { get; set; }
    }
}
