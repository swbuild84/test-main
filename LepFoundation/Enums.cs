namespace LepFoundation
{
    public static class Enums
    {
        /// <summary>
        /// Тип опоры по виду работы
        /// </summary>
        public enum ESupportType : byte
        {
            /// <summary>
            /// Промежуточная
            /// </summary>
            Promegutochnaya = 1,
            /// <summary>
            /// Анкерная без углов и разности тяжений
            /// </summary>
            AnkernayaPryamayaBezRaznTyageniy,
            /// <summary>
            /// Анкерно-угловая или концевая
            /// </summary>
            AnkerUglKoncevaya,
            /// <summary>
            /// Специальная или переходная
            /// </summary>
            Special
        }

        /// <summary>
        /// Тип грунта по прил 4 3041тм т6 для опеределения коэффициента трения
        /// </summary>
        public enum EFrictionGroundType : byte
        {
            /// <summary>
            /// Глинистые и скальные грунты с омыливающейся поверхностью (глинистые сланцы), глинистые известняки
            /// </summary>
            GLINA_SKALA_MILO = 1,
            /// <summary>
            /// Глины в твердом состоянии
            /// </summary>
            GLINA_TVERD,
            /// <summary>
            /// Глины в пластичном состоянии
            /// </summary>
            GLINA_PLAST,
            /// <summary>
            /// Суглинки в твердом состоянии
            /// </summary>
            SUGL_TVERD,
            /// <summary>
            /// Суглинки в пластичном состоянии
            /// </summary>
            SUGL_PLAST,
            /// <summary>
            /// Супеси в твердом состоянии
            /// </summary>
            SUPES_TVERD,
            /// <summary>
            /// Супеси в пластичном состоянии
            /// </summary>
            SUPES_PLAST,
            /// <summary>
            /// Пески маловлажные
            /// </summary>
            PESOK_MAOLOVLAGNIY,
            /// <summary>
            /// Пески влажные
            /// </summary>
            PESOK_VLAGNIY,
            /// <summary>
            /// Скальные грунты (с неомыливающейся поверхностью)
            /// </summary>
            SKALA
        }

        /// <summary>
        /// Тип грунта
        /// </summary>
        public enum EGroundType : byte
        {
            /// <summary>
            /// песок пылеватый
            /// </summary>
            PesokPilevatiy = 1,
            /// <summary>
            /// песок мелкий
            /// </summary>
            PesokMelkiy,
            /// <summary>
            /// песок средней крупности
            /// </summary>
            PesokSredKrupnosti,
            /// <summary>
            /// песок гравелистый
            /// </summary>
            PesokGravelistiy,
            /// <summary>
            /// супесь
            /// </summary>
            Supes,
            /// <summary>
            /// суглинок
            /// </summary>
            Suglinok,
            /// <summary>
            /// глина
            /// </summary>
            Glina
        }
    }
}
