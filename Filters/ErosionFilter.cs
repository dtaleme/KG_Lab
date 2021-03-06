using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Filters
{
    class ErosionFilter : Filters
    {
        private int _kwidth;
        private int _kheight;
        private int[,] _kmatrix;

        public ErosionFilter(int w, int h, int[,] k)
        {
            _kwidth = w;
            _kheight = h;
            _kmatrix = k;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color min = Color.Black;
            double minIntensity = 100000;
            for (int j = -_kheight / 2; j <= _kheight / 2; j++)
            {
                for (int i = -_kwidth / 2; i <= _kwidth / 2; i++)
                {
                    int nx = Clamp(x + i, 0, sourceImage.Width - 1);
                    int ny = Clamp(y + j, 0, sourceImage.Height - 1);
                    if ((_kmatrix[_kwidth / 2 + i, _kheight / 2 + j] != 0) &&
                        (Intensity(sourceImage.GetPixel(nx, ny)) < minIntensity))
                    {
                        min = sourceImage.GetPixel(nx, ny);
                        minIntensity = Intensity(min);
                    }
                }
            }

            return min;
           
        }
    }
}
