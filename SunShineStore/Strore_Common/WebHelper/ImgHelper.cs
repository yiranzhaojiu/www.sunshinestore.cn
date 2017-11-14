using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.WebHelper
{
    public  class ImgHelper
    {
        public static byte[] BulidVerifyCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;
            Bitmap image = new Bitmap((int)Math.Ceiling((code.Length-4) * 12.5), 23);
            Graphics g = Graphics.FromImage(image);

            try
            {
                System.Random random = new Random();
                g.Clear(Color.White);//清除绘图面并加白色背景色
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.DarkSlateBlue, Color.DarkRed, 1.2f, true);
                g.DrawString(code, font, brush, 2, 2);

                //画图片的前景嗓音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

    }
}
