using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace LW.Utility
{
    /// <summary>
    /// 用于处理图片相关的操作方法

    /// </summary>
    public static class ImageHelper
    {
        #region 生成缩略图（等比缩放）

        /// <summary>
        /// 生成缩略图（等比缩放）

        /// </summary>
        /// <param name="path">原始图片的物理路径</param>
        /// <param name="savedPath">缩略图要保存的物理路径</param>
        /// <param name="templateWidth">缩略图的模板宽度</param>
        /// <param name="templateHeight">缩略图的模拟高度</param>
        public static void MakeThumbnail(string path, string savedPath, double templateWidth, double templateHeight)
        {
            // 获取原始图片信息
            Image image = Image.FromFile(path);

            // 定义缩略图宽、高
            double thumbWidth = image.Width;
            double thumbHeight = image.Height;
            if (thumbWidth / thumbHeight > templateWidth / templateHeight)
            {
                // 宽按模版，高按比例缩放  
                thumbWidth = templateWidth;
                thumbHeight = image.Height * (templateWidth / image.Width);
            }
            else
            {
                // 高度按模版，宽度按比例缩放  
                thumbHeight = templateHeight;
                thumbWidth = image.Width * (templateHeight / image.Height);
            }

            // 新建一个bmp图片  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap((int)thumbWidth, (int)thumbHeight);

            // 新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            // 设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            // 设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // 清空一下画布  
            g.Clear(Color.White);

            // 在指定位置画图  
            g.DrawImage(image, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.GraphicsUnit.Pixel);

            try
            {
                // 如果缩略图已经存在，会先删除原缩略图  
                if (File.Exists(savedPath))
                {
                    File.SetAttributes(savedPath, FileAttributes.Normal);
                    File.Delete(savedPath);
                }

                // 保存缩略图

                bitmap.Save(savedPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
                bitmap.Dispose();
            }
        }
        #endregion

        #region 获取图片指定部分
        /// <summary>
        /// 获取图片指定部分
        /// </summary>
        /// <param name="path">原始图片的物理路径(包括文件名称)</param>
        /// <param name="x">在原始图片中，开始截取处的坐标X值</param>
        /// <param name="y">在原始图片中，开始截取处的坐标Y值</param>
        /// <param name="savedPath">目标图片的物理路径(包括文件名称，格式jpg)</param>
        /// <param name="partWidth">目标图片的宽度</param>
        /// <param name="partHeight">目标图片的高度</param>
        public static void GetPart(string path, int x, int y, string savedPath, int partWidth, int partHeight)
        {
            using (Image image = Image.FromFile(path))
            {
                Bitmap partImage = new Bitmap(partWidth, partHeight);
                Graphics g = Graphics.FromImage(partImage);
                Rectangle destRect = new Rectangle(new Point(0, 0), new Size(partWidth, partHeight));   // 目标位置
                Rectangle origRect = new Rectangle(new Point(x, y), new Size(partWidth, partHeight));   // 原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                g.DrawImage(image, destRect, origRect, GraphicsUnit.Pixel);
                image.Dispose();

                try
                {
                    // 如果缩略图已经存在，会先删除原缩略图
                    if (File.Exists(savedPath))
                    {
                        File.SetAttributes(savedPath, FileAttributes.Normal);
                        File.Delete(savedPath);
                    }

                    // 保存缩略图

                    partImage.Save(savedPath, ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 获取按比例缩放图片的指定部分
        /// <summary>
        /// 获取按比例缩放图片的指定部分
        /// </summary>
        /// <param name="path">原始图片的物理路径(包括文件名称)</param>
        /// <param name="x">在原始图片中，开始截取处的坐标X值</param>
        /// <param name="y">在原始图片中，开始截取处的坐标Y值</param>
        /// <param name="thumbWidth">缩放后图片的宽度</param>
        /// <param name="thumbHeight">缩放后图片的高度</param>
        /// <param name="savedPath">目标图片的物理路径(包括文件名称，格式jpg)</param>
        /// <param name="partWidth">目标图片的宽度</param>
        /// <param name="partHeight">目标图片的高度</param>
        public static void GetThumbPart(string path, int x, int y, int thumbWidth, int thumbHeight, string savedPath, int partWidth, int partHeight)
        {
            using (Image image = Image.FromFile(path))
            {
                if (image.Width == thumbWidth && image.Height == thumbHeight)
                {
                    GetPart(path, x, y, savedPath, partWidth, partHeight);
                    return;
                }

                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image thumbImage = image.GetThumbnailImage(thumbWidth, thumbHeight, callback, IntPtr.Zero);     // 缩放

                Bitmap partImage = new Bitmap(partWidth, partHeight);
                Graphics graphics = Graphics.FromImage(partImage);
                Rectangle destRect = new Rectangle(new Point(0, 0), new Size(partWidth, partHeight));       // 目标位置
                Rectangle origRect = new Rectangle(new Point(x, y), new Size(partWidth, partHeight));       // 原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                graphics.DrawImage(thumbImage, destRect, origRect, GraphicsUnit.Pixel);
                image.Dispose();

                try
                {
                    // 如果缩略图已经存在，会先删除原缩略图
                    if (File.Exists(savedPath))
                    {
                        File.SetAttributes(savedPath, FileAttributes.Normal);
                        File.Delete(savedPath);
                    }

                    // 保存缩略图

                    partImage.Save(savedPath, ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 在图片上添加文字水印
        /// <summary>
        /// 在原始图片上增加文字水印
        /// (默认水印添加在右下角)
        /// </summary>
        /// <param name="path">原始图片的物理路径</param>
        /// <param name="markText">要添加水印的文字内容</param>
        public static void AddTextWatermark(string path, string markText)
        {
            AddTextWatermark(path, path, markText, WaterMarkPosition.Default);
        }

        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="path">原始图片的物理路径</param>
        /// <param name="savedPath">添加水印后，图片要保存的物理路径</param>
        /// <param name="markText">要添加水印的文字内容</param>
        /// <param name="position">水印位置</param>
        public static void AddTextWatermark(string path, string savedPath, string markText, WaterMarkPosition position)
        {
            // 获取原图片信息

            Image image = Image.FromFile(path);
            Graphics g = Graphics.FromImage(image);
            //g.DrawImage(image, 0, 0, image.Width, image.Height);

            // 测量文本水印的高度和宽度
            Font font = null;
            int[] fontSize = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            SizeF sizeF = new SizeF();
            for (int i = 0; i < fontSize.Length; i++)
            {
                font = new Font("Verdana", fontSize[i]);
                sizeF = g.MeasureString(markText, font);

                if ((int)sizeF.Width < image.Width)
                    break;
            }

            // 计算文本水印在图片上左上角的坐标位置
            float x = 0;
            float y = 0;
            switch (position)
            {
                case WaterMarkPosition.LeftTop:
                    x = (float)image.Width * (float)0.01;
                    y = (float)image.Height * (float)0.01;
                    break;
                case WaterMarkPosition.RightTop:
                    x = (float)image.Width * (float)0.99 - sizeF.Width;
                    y = (float)image.Height * (float)0.01;
                    break;
                case WaterMarkPosition.Center:
                    x = (float)image.Width / 2 - sizeF.Width / 2;
                    y = (float)image.Height / 2 - sizeF.Height / 2;
                    break;
                case WaterMarkPosition.LeftBottom:
                    x = (float)image.Width * (float)0.01;
                    y = (float)image.Height * (float)0.09 - sizeF.Height;
                    break;
                case WaterMarkPosition.RightBottom:
                case WaterMarkPosition.Default:
                    x = (float)image.Width * (float)0.99 - sizeF.Width;
                    y = (float)image.Height * (float)0.99 - sizeF.Height;
                    break;
            }

            Brush brush = new SolidBrush(Color.Gray);
            g.DrawString(markText, font, brush, x, y);

            try
            {
                // 保存图片
                image.Save(savedPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion

        #region 在图片上添加图片水印
        /// <summary>
        /// 在原始图片上添加图片水印
        /// （默认水印添加在右下角）
        /// </summary>
        /// <param name="path">原始图片的物理路径</param>
        /// <param name="markPicPath">水印图片的物理路径</param>
        public static void AddPicWatermark(string path, string markPicPath)
        {
            AddPicWatermark(path, path, markPicPath, WaterMarkPosition.Default);
        }

        /// <summary>
        /// 在图片上添加图片水印
        /// </summary>
        /// <param name="path">原始图片的物理路径</param>
        /// <param name="savedPath">添加水印后，图片要保存的物理路径</param>
        /// <param name="markPicPath">水印图片的物理路径</param>
        public static void AddPicWatermark(string path, string savedPath, string markPicPath, WaterMarkPosition position)
        {
            // 获取原图片信息和水印图片的信息

            Image image = Image.FromFile(path);
            Image markImage = Image.FromFile(markPicPath);
            Graphics g = Graphics.FromImage(image);

            // 计算水印图片在原始图片上左上角的坐标位置
            float x = 0;
            float y = 0;
            switch (position)
            {
                case WaterMarkPosition.LeftTop:
                    x = (float)image.Width * (float)0.01;
                    y = (float)image.Height * (float)0.01;
                    break;
                case WaterMarkPosition.RightTop:
                    x = (float)image.Width * (float)0.99 - (float)markImage.Width;
                    y = (float)image.Height * (float)0.01;
                    break;
                case WaterMarkPosition.Center:
                    x = (float)image.Width / 2 - (float)markImage.Width / 2;
                    y = (float)image.Height / 2 - (float)markImage.Height / 2;
                    break;
                case WaterMarkPosition.LeftBottom:
                    x = (float)image.Width * (float)0.01;
                    y = (float)image.Height * (float)0.09 - (float)markImage.Height;
                    break;
                case WaterMarkPosition.RightBottom:
                case WaterMarkPosition.Default:
                    x = (float)image.Width * (float)0.99 - (float)markImage.Width;
                    y = (float)image.Height * (float)0.99 - (float)markImage.Height;
                    break;
            }

            g.DrawImage(markImage, x, y, (float)markImage.Width, (float)markImage.Height);

            try
            {
                // 保存图片
                image.Save(savedPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion

        #region 获得图像高宽信息
        /// <summary>
        /// 获得图像高宽信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ImageInfo GetImageInfo(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                ImageInfo imageInfo = new ImageInfo();
                imageInfo.Width = image.Width;
                imageInfo.Height = image.Height;
                return imageInfo;
            }
        }
        #endregion

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <returns></returns>
        public static bool ThumbnailCallback()
        {
            return false;
        }

        #region 生成验证码图片

        /// <summary>
        /// 生成验证码图片

        /// </summary>
        /// <param name="validateCode">验证码内容</param>
        /// <returns>返回验证码图片</returns>
        public static byte[] MakeValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 15.5), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器

                Random random = new Random();

                //清空图片背景色

                g.Clear(Color.White);

                //画图片的干扰线

                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Black, Color.Gray, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);

                //画图片的前景干扰点

                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线

                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);

                //输出图片流

                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion
    }

    #region 获取图片的高度和宽度信息
    /// <summary>
    /// 获取图片的高度和宽度信息
    /// </summary>
    public struct ImageInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
    #endregion

    #region 水印图片的位置

    /// <summary>
    /// 水印图片的位置，默认为右下角（RightBottom）

    /// 共有五个位置可选，分别为左上角（LeftTop）、右上角（RightTop）、正中间（Center）、左下角（LeftBottom）、右下角（RightBottom）

    /// </summary>
    public enum WaterMarkPosition
    {
        /// <summary>
        /// 默认(为右下角)
        /// </summary>
        Default = 0,

        /// <summary>
        /// 左上角

        /// </summary>
        LeftTop = 1,

        /// <summary>
        /// 右上角

        /// </summary>
        RightTop = 2,

        /// <summary>
        /// 正中间

        /// </summary>
        Center = 3,

        /// <summary>
        /// 左下角

        /// </summary>
        LeftBottom = 4,

        /// <summary>
        /// 右下角

        /// </summary>
        RightBottom = 5
    }
    #endregion
}
