namespace LepFoundation
{
    /// <summary>
    /// Класс нагрузок на стойку в грунте на уровне заделки в грунт
    /// </summary>
    public class SupportLoads
    {
        /// <summary>
        /// Расчетный Момент, кН*м
        /// </summary>
        public double Mr { get; set; }
        /// <summary>
        /// Расчетная Вертикальная сила кН
        /// </summary>
        public double Nr { get; set; }
        /// <summary>
        /// Расчетная Поперечная сила, кН
        /// </summary>
        public double Qr { get; set; }
        /// <summary>
        /// Нормативный Момент, кН*м
        /// </summary>
        public double Mn { get; set; }
        /// <summary>
        /// Нормативная Вертикальная сила кН
        /// </summary>
        public double Nn { get; set; }
        /// <summary>
        /// Нормативная Поперечная сила, кН
        /// </summary>
        public double Qn { get; set; }
    }
}
