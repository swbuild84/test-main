using System;

namespace LepFoundation
{
    public static class EngMath
    {
        //линейная интерполяция
        /// <summary>
        /// Линейная интерполяция
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="x"></param>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        public static double LineInterpol(double x0, double x1, double x, double y0, double y1)
        {
            return (y0 + ((y1 - y0) / (x1 - x0)) * (x - x0));
        }

        /// <summary>
        /// Интерполурует массив по значению
        /// </summary>
        /// <param name="ar">одномерный массив</param>
        /// <param name="x">значение для поиска в первой строке</param>
        /// <param name="len">длина строки в одномерном массиве</param>
        /// <param name="rez">переменная для возврата значения</param>
        static public void InterpOne(double[] ar, double x, int len, ref double rez)
        {
            //Если значение меньше первого, устанавливаем первое
            if (x <= ar[0])
            {
                rez = ar[len];
                return;
            }
            //Если значение больше последнего, устанавливаем последнее
            if (x > ar[len - 1])
            {
                rez = ar[(len * 2 - 1)];
                return;
            }
            int i = 1;

            while (ar[i] < x && i < len - 1)
            {
                i++;
            }
            double x1 = ar[i - 1];
            double x2 = ar[i];
            double y1 = ar[i - 1 + len];
            double y2 = ar[i + len];
            rez = (y2 - y1) / (x2 - x1) * (x - x1) + y1;

        }

        /// <summary>
        /// Градусы в радианы
        /// </summary>
        /// <param name="angle">Угол в десятичных градусах</param>
        /// <returns>угол в радианах</returns>
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// Радианы в градусы
        /// </summary>
        /// <param name="angle">Угол в радианах</param>
        /// <returns>Угол в десятичных градусах</returns>
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}
