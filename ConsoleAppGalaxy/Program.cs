using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppGalaxy
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Вывод приветствия и списка галактик.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Galaxy News!");
            IterateThroughList();
            Console.ReadKey();
        }

        /// <summary>
        /// Создание коллекции галактик и вывод их параметров в консоль.
        /// </summary>
        private static void IterateThroughList()
        {
            var theGalaxies = new List<Galaxy>
        {
            new Galaxy() { Name="Tadpole", MegaLightYears=400, GalaxyType=new GType('S')},
            new Galaxy() { Name="Pinwheel", MegaLightYears=25, GalaxyType=new GType('S')},
            new Galaxy() { Name="Cartwheel", MegaLightYears=500, GalaxyType=new GType('L')},
            new Galaxy() { Name="Small Magellanic Cloud", MegaLightYears=.2, GalaxyType=new GType('I')},
            new Galaxy() { Name="Andromeda", MegaLightYears=3, GalaxyType=new GType('S')},
            new Galaxy() { Name="Maffei 1", MegaLightYears=11, GalaxyType=new GType('E')}
        };

            foreach (Galaxy theGalaxy in theGalaxies)
            {
                Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.MegaLightYears + ",  " + theGalaxy.GalaxyType.MyGType);
            }

            // Expected Output:
            //  Tadpole  400,  Spiral
            //  Pinwheel  25,  Spiral
            //  Cartwheel, 500,  Lenticular
            //  Small Magellanic Cloud .2,  Irregular
            //  Andromeda  3,  Spiral
            //  Maffei 1,  11,  Elliptical
        }
    }

    /// <summary>
    /// Информация о галактике: название, расстояние и тип.
    /// </summary>
    public class Galaxy
    {
        public string Name { get; set; }
        public double MegaLightYears { get; set; }
        public GType GalaxyType { get; set; }

    }

    /// <summary>
    /// Преобразование буквенного кода типа галактики в читаемое значение.
    /// </summary>
    public class GType
    {
        /// <summary>
        /// Преобразование символа в тип галактики.
        /// </summary>
        /// <param name="type">Обозначение типов галактик через буквы</param>
        public GType(char type)
        {
            switch (type)
            {
                case 'S':
                    MyGType = Type.Spiral;
                    break;
                case 'E':
                    MyGType = Type.Elliptical;
                    break;
                case 'I':
                    MyGType = Type.Irregular;
                    break;
                case 'L':
                    MyGType = Type.Lenticular;
                    break;
                default:
                    break;
            }
        }
       
        /// <summary>
        /// Результат преобразования.
        /// </summary>
        public object MyGType { get; set; }

        /// <summary>
        /// Список типов галактик.
        /// </summary>
        private enum Type { Spiral, Elliptical, Irregular, Lenticular }
    }
}


