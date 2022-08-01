using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapateria.Clases
{
    public class Colores
    {

        //Clase exclusiva para guardar los colores del proyecto y puedan ser reutilizados
        public static Color Primary { get { return Color.FromArgb(112, 57, 201); } }
        public static Color Secondary { get { return Color.FromArgb(107, 122, 153); } }
        public static Color White { get { return Color.FromArgb(249, 249, 249); } }
        public static Color BG { get { return Color.FromArgb(22, 0, 66); } }

    }
}
