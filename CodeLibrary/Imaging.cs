/**
* Copyright (c) 2013, Broadway
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace Jewar.CodeLibrary
{
    public class Imaging
    {
        /// <summary>
        /// Return Bitmap of ImageThumnail
        /// How to use:
        /// Bitmap image = ImageThumnail(Server.MapPath("~/Test.jpg"), 150);
        /// </summary>
        /// <param name="ImagePath">Valid <Image Path></param>
        /// <param name="thumbnailSize">Thumbnail Size</param>
        /// <returns></returns>
        public Bitmap ImageThumnail(string ImagePath, int thumbnailSize)
        {
            string photoPath = ImagePath;
            Bitmap target = null;
            Bitmap photo = null;
            try
            {
                photo = new Bitmap(photoPath);

                int width, height;
                if (photo.Width > photo.Height)
                {
                    width = thumbnailSize;
                    height = photo.Height * thumbnailSize / photo.Width;
                }
                else
                {
                    width = photo.Width * thumbnailSize / photo.Height;
                    height = thumbnailSize;
                }

                target = ImageResize(ImagePath, width, height);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.Imaging.ImageThumnail :-" + ex.Message);                
            }
            return target;
        }

        /// <summary>
        /// Return Bitmap of the Resized Image
        /// How to use:
        /// Bitmap image = ImageResize(Server.MapPath("~/Test.jpg"), 150,150);
        /// </summary>
        /// <param name="ImagePath">Valid <Image Path></param>
        /// <param name="width">With of the image</param>
        /// <param name="height">Height of the image</param>
        /// <returns></returns>
        public Bitmap ImageResize(string ImagePath, int width, int height)
        {
            string photoPath = ImagePath;
            Bitmap target = null;
            Bitmap photo = null;
            try
            {
                target = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(target))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    graphics.DrawImage(photo, 0, 0, width, height);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.Imaging.ImageResize :-" + ex.Message);
            }
            return target;
        }

        ///// <summary>
        ///// Return Byte of the Cropped image.
        ///// How To Use:
        ///// byte[] CropImage = OvrLod.Imaging.CropImage(path + ImageName, w, h, x, y);
        ///// </summary>
        ///// <param name="Img"></param>
        ///// <param name="Width"></param>
        ///// <param name="Height"></param>
        ///// <param name="X"></param>
        ///// <param name="Y"></param>
        ///// <returns></returns>
        //static byte[] CropImage(string Img, int Width, int Height, int X, int Y)
        //{
        //    try
        //    {
        //        using (SD.Image OriginalImage = SD.Image.FromFile(Img))
        //        {
        //            using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
        //            {
        //                bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
        //                using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
        //                {
        //                    Graphic.SmoothingMode = SmoothingMode.AntiAlias;
        //                    Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                    Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                    Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
        //                    MemoryStream ms = new MemoryStream();
        //                    bmp.Save(ms, OriginalImage.RawFormat);
        //                    return ms.GetBuffer();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ExceptionHandling.AddSystemerrorlog(ex.Message);    
        //    }
        //}

        public void ImageResizenSave(string ImagePath,string DestinationPath, string DestinationFileName, int Width, int Height)
        {
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(ImagePath);

                int thumbWidth = Width;
                int thumbHeight = Height;

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(thumbWidth, thumbHeight);
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);

                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);

                if (!System.IO.Directory.Exists(DestinationPath))
                {
                    System.IO.Directory.CreateDirectory(DestinationPath);
                }

                bmp.Save(DestinationPath + "\\" + DestinationFileName, ImageFormat.Png);
            }
            catch (Exception ex)
            { }
        }


        /// <summary>
        /// Created by Junaid hassan 2013-06-26 to  GET Resized Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns>System.IO.Stream</returns>
        public static System.IO.Stream GetResizedStream(Stream stream, int newWidth, int newHeight)
        {

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(stream);

            Bitmap target = new Bitmap(newWidth, newHeight);
            Graphics graphic = Graphics.FromImage(target);

            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphic.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            graphic.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);

            Stream Mystream = new MemoryStream(ImageToByte(target));

            return Mystream;
        }

        /// <summary>
        /// Get Byte from Image
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

    }
}
