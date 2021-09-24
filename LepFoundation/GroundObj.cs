using System;

namespace LepFoundation
{
    /// <summary>
    /// Грунт
    /// </summary>
    public class GroundObj : ICloneable
    {
        /// <summary>
        /// Тип грунта по прил 4 3041тм т6 для опеределения коэффициента трения
        /// </summary>
        public Enums.EFrictionGroundType FrictionGroundType { get; set; }

        double _koefPoristostiE;
        /// <summary>
        /// Коэффициент пористости грунта
        /// </summary>
        public double KoefPoristostiE
        {
            get { return _koefPoristostiE; }
            set
            {
                if (value >= 0) _koefPoristostiE = value;
                else _koefPoristostiE = 0;
            }
        }

        Enums.EGroundType _type = Enums.EGroundType.Suglinok;
        /// <summary>
        /// Тип грунта
        /// </summary>
        public Enums.EGroundType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        double _IL;
        /// <summary>
        /// Показатель текучести
        /// </summary>
        public double IL
        {
            get { return _IL; }
            set
            {
                if (_type == Enums.EGroundType.Supes ||
                    _type == Enums.EGroundType.Suglinok ||
                    _type == Enums.EGroundType.Glina)
                    _IL = value;
                else _IL = 0;
            }
        }

        double _phi2;
        /// <summary>
        /// Нормативный угол внутреннего трения грунта, градусы
        /// </summary>
        public double Phi2
        {
            get { return _phi2; }
            set { _phi2 = value; }
        }


        double _c2;
        /// <summary>
        /// Нормативное удельное сцепление грунта, кПа
        /// </summary>
        public double C2
        {
            get { return _c2; }
            set { _c2 = value; }
        }


        double _E;
        /// <summary>
        /// Модуль деформации грунта, МПа
        /// </summary>
        public double E
        {
            get { return _E; }
            set { _E = value; }
        }

        double _gamma2;
        /// <summary>
        /// Нормативный объемный вес, КН/м3
        /// </summary>
        public double Gamma2
        {
            get { return _gamma2; }
            set { _gamma2 = value; }
        }

        double _phi1;
        /// <summary>
        /// Расчетный угол внутреннего трения грунта, градусы
        /// </summary>
        public double Phi1
        {
            get { return _phi1; }
            set { _phi1 = value; }
        }


        double _c1;
        /// <summary>
        /// Расчетное удельное сцепление грунта, кПа
        /// </summary>
        public double C1
        {
            get { return _c1; }
            set { _c1 = value; }
        }


        double _gamma1;
        /// <summary>
        /// Расчетный объемный вес, КН/м3
        /// </summary>
        public double Gamma1
        {
            get { return _gamma1; }
            set { _gamma1 = value; }
        }

        /// <summary>
        /// Расчет расчетной характеристики угла внутренного трения по СНиП
        /// </summary>
        public void CalcС1()
        {
            if (Phi2 < 0) return;
            if (Type == Enums.EGroundType.PesokMelkiy ||
                Type == Enums.EGroundType.PesokSredKrupnosti ||
                Type == Enums.EGroundType.PesokGravelistiy ||
                Type == Enums.EGroundType.PesokPilevatiy)
                C1 = C2 / 4.0;
            if (Type == Enums.EGroundType.Supes && _IL <= 0.25) C1 = C2 / 2.4;
            if ((Type == Enums.EGroundType.Suglinok || Type == Enums.EGroundType.Glina) && _IL <= 0.5) C1 = C2 / 2.4;
            if (Type == Enums.EGroundType.Supes && _IL > 0.25) C1 = C2 / 3.3;
            if ((Type == Enums.EGroundType.Suglinok || Type == Enums.EGroundType.Glina) && _IL > 0.5) C1 = C2 / 3.3;
            return;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
