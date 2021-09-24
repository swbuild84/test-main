using LepFoundation;
using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            GroundObj gr1 = new GroundObj();
            gr1.Type = Enums.EGroundType.PesokMelkiy;
            gr1.IL = 0;
            gr1.FrictionGroundType = Enums.EFrictionGroundType.PESOK_MAOLOVLAGNIY;
            gr1.E = 50;
            gr1.Gamma1 = 20;
            gr1.Gamma2 = 20;
            gr1.Phi1 = 30;
            gr1.Phi2 = 34;
            gr1.C1 = 5;
            gr1.C2 = 8;
            GeoLayer lr1 = new GeoLayer();
            lr1.Ground = gr1;
            lr1.Depth = 1;

            GroundObj gr2 = new GroundObj();
            gr2.Type = Enums.EGroundType.Suglinok;
            gr2.IL = 0.6;
            gr2.FrictionGroundType = Enums.EFrictionGroundType.SUGL_PLAST;
            gr2.E = 40;
            gr2.Gamma1 = 17;
            gr2.Gamma2 = 17;
            gr2.Phi1 = 24;
            gr2.Phi2 = 26;
            gr2.C1 = 10;
            gr2.C2 = 12;
            GeoLayer lr2 = new GeoLayer();
            lr2.Ground = gr2;
            lr2.Depth = 3;

            GeoSection section = new GeoSection();
            
            section.GeoLayers.Add(lr1);
            section.GeoLayers.Add(lr2);

            GroundObj avGrnd = section.GetAverageGround(3);



            Calculator calc = new Calculator();
            Pole pole = new Pole();
            pole.b0= 0.545;
            pole.h = 3;
            pole.SupportType = Enums.ESupportType.AnkerUglKoncevaya;
            pole.IsDrilled = true;

            calc.Pole = pole;
            
            GroundObj gr = new GroundObj();
            gr.Type = Enums.EGroundType.PesokPilevatiy;
            gr.IL = 0.2;
            gr.E = 50;

            gr.Gamma1 = 19.5;
            gr.Phi1 = 21;
            gr.C1 = 8.75;
            gr.FrictionGroundType = Enums.EFrictionGroundType.SUPES_PLAST;
            calc.InputGround = gr;

            SupportLoads loads = new SupportLoads();

            loads.Nr = 80;
            loads.Mr = 192;
            loads.Qr = 9.6;

            loads.Nn = 80;
            loads.Mn = 192;
            loads.Qn = 9.6;

            calc.Loads = loads;     

            Console.WriteLine(calc.CheckFirstPS());
            Console.WriteLine(calc.CheckSecondPS());
            Console.ReadKey();
        }
    }
}
