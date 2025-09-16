using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PrjectHejer.Utilities
{
    public static class ImageOptimizer
    {
        /// <summary>
        /// Reduces the size of a base64 image by resizing and/or compressing it
        /// </summary>
        /// <param name="base64Image">The original base64 image string</param>
        /// <param name="maxWidth">Maximum width of the resized image</param>
        /// <param name="maxHeight">Maximum height of the resized image</param>
        /// <param name="quality">JPEG quality (0-100)</param>
        /// <returns>Optimized base64 image string</returns>
        public static string ReduceBase64ImageSize(string base64Image, int maxWidth = 1080, int maxHeight = 1080, int quality = 75)
        {
            // Remove data:image/png;base64, part if exists
            string base64Data = base64Image;
            if (base64Image.Contains(","))
            {
                base64Data = base64Image.Split(',')[1];
            }

            // Convert base64 string to byte array
            byte[] imageBytes = Convert.FromBase64String(base64Data);

            using (MemoryStream ms = new(imageBytes))
            {
                // Create Image from stream
                using (Image image = Image.FromStream(ms))
                {
                    // Calculate new dimensions while maintaining aspect ratio
                    int newWidth, newHeight;
                    if (image.Width > maxWidth || image.Height > maxHeight)
                    {
                        double ratioX = (double)maxWidth / image.Width;
                        double ratioY = (double)maxHeight / image.Height;
                        double ratio = Math.Min(ratioX, ratioY);

                        newWidth = (int)(image.Width * ratio);
                        newHeight = (int)(image.Height * ratio);
                    }
                    else
                    {
                        // Image is already smaller than max dimensions
                        newWidth = image.Width;
                        newHeight = image.Height;
                    }

                    // Create new bitmap with the calculated dimensions
                    using (Bitmap newImage = new Bitmap(newWidth, newHeight))
                    {
                        using (Graphics g = Graphics.FromImage(newImage))
                        {
                            // Configure for high quality resizing
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            // Draw the resized image
                            g.DrawImage(image, 0, 0, newWidth, newHeight);

                            // Configure JPEG encoder with quality setting
                            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                            EncoderParameters encoderParams = new EncoderParameters(1);
                            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                            // Save the resized image to a memory stream
                            using (MemoryStream resultStream = new MemoryStream())
                            {
                                newImage.Save(resultStream, jpegCodec, encoderParams);
                                byte[] resultBytes = resultStream.ToArray();

                                // Convert back to base64
                                return Convert.ToBase64String(resultBytes);
                            }
                        }
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < encoders.Length; i++)
            {
                if (encoders[i].MimeType == mimeType)
                    return encoders[i];
            }
            return null;
        }
    }
}