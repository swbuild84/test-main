using System;

namespace LepFoundation
{
    /// <summary>
    /// Стойка в грунте
    /// </summary>
    public class Calculator
    {
        //PRIVATE MEMBERS
        /// <summary>
        /// длина верхнего ригеля, м
        /// </summary>
        private double lr = 1;
        /// <summary>
        /// длина нижнего ригеля, м
        /// </summary>
        private double lr1 = 1;
        /// <summary>
        /// высота верхнего ригеля, м
        /// </summary>
        private double hr;
        /// <summary>
        /// высота нижнего ригеля, м
        /// </summary>
        private double hr1;
        /// <summary>
        /// расст. от поверхн. грунта (банкетки) до середины высоты верхн. ригеля, м
        /// </summary>
        private double yr;
        /// <summary>
        /// расст. от нижнего основания стойки до середины высоты нижнего ригеля, м
        /// </summary>
        private double yr1;
        /// <summary>
        /// толщина верхнего ригеля, м
        /// </summary>
        private double ar;
        /// <summary>
        /// толщина нижнего ригеля, м
        /// </summary>
        private double ar1;

        /// <summary>
        /// глубина заделки стойки в грунт, м 
        /// </summary>
        private double h { get; set; }

        /// <summary>
        /// средняя ширина стоики в грунте, м
        /// </summary>
        private double b0 { get; set; }

        /// <summary>
        /// высота банкетки, м
        /// </summary>
        private double hb { get; set; }

        /// <summary>
        /// Тип опоры
        /// </summary>
        private Enums.ESupportType SupportType { get; set; }

        /// <summary>
        /// установка стойки в сверленый или копаный котлован
        /// </summary>
        private bool IsDrilled { get; set; } = true;
        private Rigel m_LoweRigel = null;

        private GroundObj m_grnd;

        //PUBLIC MEMBERS

        /// <summary>
        /// Нагрузки
        /// </summary>
        public SupportLoads Loads { get; set; }

        public Pole Pole { get; set; }
        /// <summary>
        /// Грунт средневзвешенные значения
        /// </summary>
        public GroundObj InputGround { get; set; }   

        /// <summary>
        /// Верхний ригель
        /// </summary>
        public Rigel UpperRigel { get; set; }
        
        /// <summary>
        /// Нижний ригель
        /// </summary>
        public Rigel LowerRigel
        {
            get
            {
                return m_LoweRigel;
            }

            set
            {
                if (value != null)
                {
                    m_LoweRigel = value;
                    IsDrilled = false;  //!!!только копаный котлован при установке нижнего ригеля!!!
                }
            }
        }
        

        private void Init()
        {
            if(Pole!=null)
            {                
                h = Pole.h;
                b0 = Pole.b0;
                hb = Pole.hb;
                SupportType = Pole.SupportType;
                IsDrilled = Pole.IsDrilled;
            }

            //Характеристики грунта засыпки в случае копаного котлована принимаются по 
            //указаниям п.6.15 3041тм-т2
            if (!IsDrilled)
            {
                m_grnd = (GroundObj)InputGround.Clone();
                m_grnd.Phi1 *= 0.8;
                m_grnd.C1 *= 0.5;
            }
            else { m_grnd = InputGround; };

            //верхний ригель
            if (UpperRigel != null)
            {
                lr = UpperRigel.L;
                hr = UpperRigel.B;
                ar = UpperRigel.A;
                yr = UpperRigel.Yr;
            }

            //нижний ригель
            if (m_LoweRigel != null)
            {
                lr1 = m_LoweRigel.L;
                hr1 = m_LoweRigel.B;
                ar1 = m_LoweRigel.A;
                yr1 = h - m_LoweRigel.Yr;
            }
        }

        /// <summary>
        /// Проверка по первому ПС (устойчивости)
        /// </summary>
        /// <returns>Проверка по первому ПС (устойчивости)</returns>
        public bool CheckFirstPS()
        {
            Init();
            double H = Loads.Mr / Loads.Qr;
            double omega = 1.0 - 0.003 * m_grnd.C1;
            double alpha = H / h;
            double f_tr = GetFriction(m_grnd.FrictionGroundType);
            double fd = f_tr * b0 / (2.0 * h);

            double m = m_grnd.Gamma1 * Math.Pow(Math.Tan(EngMath.DegreeToRadian(45 + m_grnd.Phi1 / 2.0)), 2);
            double mc = 2.0 * m_grnd.C1 * Math.Tan(EngMath.DegreeToRadian(45 + m_grnd.Phi1 / 2));
            double etta = mc / (m * h);
            double sigma = 100;
            double psi = Math.Atan(Math.Tan(EngMath.DegreeToRadian(m_grnd.Phi1)) + m_grnd.C1 / sigma);
            double Cod = (2.0 / 3.0) * Math.Tan(psi / 5.0) / Math.Tan(EngMath.DegreeToRadian(45) - psi / 2.0);
            double Kod = 1.0 + Cod * h / b0;
            double b = b0 * Kod;

            double lambdaD = (b0 / 2.0 + ar) * f_tr / h;
            double lambdaD1 = (b0 / 2.0 + ar1) * f_tr / h;

            double u = m * b * h * h / 2.0;
            double A = (lr - b0) * hr * (mc + m * yr) * (1.0 + 0.3 / lr);
            double A1 = (lr1 - b0) * hr1 * (mc + m * (h - yr1)) * (1.0 + 0.3 / lr1);

            double epsilon = A / u;
            double epsilon1 = A1 / u;
            double fn = f_tr * Loads.Nr / u;

            //Относительная глубина центра поворота определяется из уравнения 6.78
            double x0 = 0.5;
            double fx0 = x0 * x0 * x0 + 3.0 / 2.0 * (alpha + etta) * x0 * x0 + 3.0 * alpha * etta * x0 - 1.0 / 4.0 * ((2.0 * etta + 1.0) * (3.0 * alpha + 3.0 * fd + 2.0) - etta) - 3.0 / 4.0 * fn * (1 + alpha) - 3.0 / 4.0 * (epsilon * (alpha + yr / h - lambdaD) - epsilon1 * (alpha - yr1 / h + lambdaD1 + 1.0));
            double dfx0 = 2.0 * x0 * x0 + 3.0 * (alpha + etta) * x0 + 3.0 * alpha * etta;
            double x1 = x0 - fx0 / dfx0; // первое приближение

            int i = 0;
            while (Math.Abs(x1 - x0) > 0.000001)
            { // пока не достигнута точность 0.000001
                x0 = x1;
                fx0 = x0 * x0 * x0 + 3.0 / 2.0 * (alpha + etta) * x0 * x0 + 3.0 * alpha * etta * x0 - 1.0 / 4.0 * ((2.0 * etta + 1.0) * (3.0 * alpha + 3.0 * fd + 2.0) - etta) - 3.0 / 4.0 * fn * (1 + alpha) - 3.0 / 4.0 * (epsilon * (alpha + yr / h - lambdaD) - epsilon1 * (alpha - yr1 / h + lambdaD1 + 1.0));
                dfx0 = 2.0 * x0 * x0 + 3.0 * (alpha + etta) * x0 + 3.0 * alpha * etta;
                x1 = x0 - fx0 / dfx0; // последующие приближения
                i++;
            }


            double Tetta = x1;
            double QnMax = omega / (alpha + Tetta) * (u * (2.0 / 3.0 * (Math.Pow(Tetta, 3)
                + 3.0 * etta * (Math.Pow(Tetta, 2) - Tetta + 0.5) - 3.0 / 2.0 * Tetta + 1.0)
                + (2.0 * etta + 1.0) * fd) + A * (Tetta - yr / h + lambdaD) + A1 * (Tetta - yr1 / h + lambdaD1) + f_tr * Loads.Nr * (1.0 - Tetta));
            double kn = GetKoefNadegnosti(this.SupportType);
            double mz = GetKoefUslRaboti(this.InputGround, this.IsDrilled);
            double Qn = 1 / kn * mz * QnMax;
            return Qn >= this.Loads.Qr;
        }

        /// <summary>
        /// Проверка по второму ПС (устойчивости)
        /// </summary>
        /// <returns>Проверка по  второму ПС (деформациям)</returns>
        public bool CheckSecondPS()
        {
            double beta0 = 0;
            double Q = this.Loads.Qn / 9.80665; //перевод кН в т
            double H = Loads.Mn / Loads.Qn;
            double alpha = H / h;
            double E = this.InputGround.E * 102;    //перевод МПа в т/м2

            if (this.UpperRigel == null && this.m_LoweRigel == null)  //безригельное
            {
                double nyu = GetNyu(b0 / h);
                beta0 = 3 * Q / (4 * E * h * h) * (6 * alpha + 3) * nyu;
            }
            if (this.UpperRigel != null && this.m_LoweRigel == null)  //только верхний ригель           
            {
                double Fb = UpperRigel.L * UpperRigel.B;
                double nyuV = GetNyu(b0 / h);
                beta0 = 3 * Q / (8 * E * h * h) * (6 * alpha + 5) * nyuV;
            }
            if (this.UpperRigel != null && this.m_LoweRigel != null)  //верхний и нижний ригель           
            {
                double Fb = UpperRigel.L * UpperRigel.B;
                double Fh = m_LoweRigel.L * m_LoweRigel.B;
                double nyuV = GetNyu(3 * Fb / (h * h));
                double nyuN = GetNyu(3 * Fh / (h * h));
                beta0 = 3 * Q / (8 * E * h * h) * ((6 * alpha + 5) * nyuV + (6 * alpha + 1) * nyuN);
            }
            if (this.UpperRigel == null && this.m_LoweRigel != null)  //нижний ригель           
            {
                double Fh = m_LoweRigel.L * m_LoweRigel.B;
                double nyuN = GetNyu(b0 / h);
                beta0 = 3 * Q / (8 * E * h * h) * (6 * alpha + 1) * nyuN;
            }
            return beta0 < 0.01;
        }

        /// <summary>
        /// Трение 
        /// </summary>
        /// <param name="Ground"></param>
        /// <returns></returns>
        static double GetFriction(Enums.EFrictionGroundType Gtype)
        {
            switch (Gtype)
            {
                case Enums.EFrictionGroundType.GLINA_SKALA_MILO: return 0.25;
                case Enums.EFrictionGroundType.GLINA_TVERD: return 0.3;
                case Enums.EFrictionGroundType.GLINA_PLAST: return 0.2;
                case Enums.EFrictionGroundType.SUGL_TVERD: return 0.45;
                case Enums.EFrictionGroundType.SUGL_PLAST: return 0.25;
                case Enums.EFrictionGroundType.SUPES_TVERD: return 0.5;
                case Enums.EFrictionGroundType.SUPES_PLAST: return 0.35;
                case Enums.EFrictionGroundType.PESOK_MAOLOVLAGNIY: return 0.55;
                case Enums.EFrictionGroundType.PESOK_VLAGNIY: return 0.45;
                case Enums.EFrictionGroundType.SKALA: return 0.75;
                default: return -1;
            }
        }
        /// <summary>
        /// Коэф. надежности по табл. 6.10 3041тмт2
        /// </summary>
        /// <param name="type">тип опоры</param>
        /// <returns></returns>
        static double GetKoefNadegnosti(Enums.ESupportType type)
        {
            switch (type)
            {
                case Enums.ESupportType.Promegutochnaya: return 1;
                case Enums.ESupportType.AnkernayaPryamayaBezRaznTyageniy: return 1.2;
                case Enums.ESupportType.AnkerUglKoncevaya: return 1.3;
                case Enums.ESupportType.Special: return 1.7;
                default: return 0;
            }
        }

        static double GetKoefUslRaboti(GroundObj Ground, bool IsDrilled)
        {
            if (IsDrilled)
            //сверленый котлован - грунт ненарушенной стр-ры
            {
                switch (Ground.Type)
                {
                    case Enums.EGroundType.PesokGravelistiy: return 1.1;
                    case Enums.EGroundType.PesokSredKrupnosti: return 1.05;
                    case Enums.EGroundType.PesokMelkiy: return 1.1;
                    case Enums.EGroundType.PesokPilevatiy: return 1.15;
                    case Enums.EGroundType.Supes:
                        if (Ground.IL <= 0.25) return 1.3;
                        if (Ground.IL > 0.25) return 1.4;
                        break;
                    case Enums.EGroundType.Suglinok:
                        if (Ground.IL <= 0.25) return 1.25;
                        if (Ground.IL > 0.25) return 1.4;
                        break;
                    case Enums.EGroundType.Glina: return 1.5;
                    default: return 0;
                }
            }
            else
            //копаный котлован - грунт нарушенной стр-ры
            {
                switch (Ground.Type)
                {
                    case Enums.EGroundType.PesokGravelistiy: return 1.0;
                    case Enums.EGroundType.PesokSredKrupnosti: return 1.0;
                    case Enums.EGroundType.PesokMelkiy: return 1.0;
                    case Enums.EGroundType.PesokPilevatiy: return 1.05;
                    case Enums.EGroundType.Supes:
                        if (Ground.IL <= 0.25) return 1.2;
                        if (Ground.IL > 0.25) return 1.3;
                        break;
                    case Enums.EGroundType.Suglinok:
                        if (Ground.IL <= 0.25) return 1.15;
                        if (Ground.IL > 0.25) return 1.25;
                        break;
                    case Enums.EGroundType.Glina:
                        if (Ground.IL <= 0.5) return 1.3;
                        if (Ground.IL > 0.5) return 1.4;
                        break;
                    default: return 0;
                }
            }
            return 0;
        }

        static double GetNyu(double x)
        {
            double[] ar = new double[42] { 0.0634548878833812, 0.0872722295455018, 0.12534091697853, 0.143863487997347, 0.179358497726005, 0.219116240036183, 0.262378142406143, 0.308587059663296, 0.356452358589084, 0.406184583189984, 0.457262665284285, 0.53563349986689, 0.562182302776537, 0.615495538669777, 0.669195207890176, 0.723174239750992, 0.76806635948742, 0.831592793445729, 0.892624418795094, 0.940723526556998, 1, 7, 6.46689013789652, 5.79224552048345, 5.52951158284358, 5.10872734085848, 4.723576427565, 4.37534394914281, 4.06407386318733, 3.79394771260713, 3.55834942638038, 3.35477903829024, 3.10149322982397, 3.0287523067067, 2.89868472022009, 2.78530951631479, 2.68463842947831, 2.60794842579344, 2.50918520194511, 2.43635160984468, 2.40047908984234, 2.3732265529452 };
            double res = 0;
            EngMath.InterpOne(ar, x, 21, ref res);
            return res;
        }
    }
}
