using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000084 RID: 132
	internal static class ImageUtility
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x0001BD60 File Offset: 0x00019F60
		private static ImageFormat GetTargetImageFormat(ImageFormat imageFormat)
		{
			ImageFormat imageFormat2 = ImageFormat.Png;
			if (imageFormat.Guid == ImageFormat.Jpeg.Guid)
			{
				imageFormat2 = ImageFormat.Jpeg;
			}
			return imageFormat2;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001BD91 File Offset: 0x00019F91
		private static bool IsSupportedBySilverlight(ImageFormat imageFormat)
		{
			return imageFormat.Guid == ImageFormat.Jpeg.Guid || imageFormat.Guid == ImageFormat.Png.Guid;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BDC1 File Offset: 0x00019FC1
		private static double ConvertToPixels(RVUnit unit)
		{
			return unit.ToMillimeters() * 96.0 / 25.4;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		internal static byte[] ScaleImage(byte[] sourceImageBytes, RVUnit frameWidth, RVUnit frameHeight)
		{
			ImageFormat imageFormat;
			return ImageUtility.ScaleImage(sourceImageBytes, (int)ImageUtility.ConvertToPixels(frameWidth), (int)ImageUtility.ConvertToPixels(frameHeight), out imageFormat);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001BE04 File Offset: 0x0001A004
		internal static byte[] ScaleImage(byte[] sourceImageBytes, int frameWidth, int frameHeight, out ImageFormat imageFormat)
		{
			Image image;
			try
			{
				image = Image.FromStream(new MemoryStream(sourceImageBytes), true, false);
				imageFormat = ImageUtility.GetTargetImageFormat(image.RawFormat);
			}
			catch
			{
				imageFormat = null;
				return null;
			}
			int num = Math.Max(image.Width / frameWidth, image.Height / frameHeight);
			Image image2;
			if (num > 1)
			{
				int num2 = image.Width / num;
				int num3 = image.Height / num;
				image2 = new Bitmap(num2, num3);
				using (Graphics graphics = Graphics.FromImage(image2))
				{
					graphics.DrawImage(image, 0, 0, num2, num3);
				}
				image.Dispose();
			}
			else
			{
				if (ImageUtility.IsSupportedBySilverlight(image.RawFormat))
				{
					return sourceImageBytes;
				}
				image2 = image;
			}
			MemoryStream memoryStream = new MemoryStream();
			image2.Save(memoryStream, imageFormat);
			image2.Dispose();
			return memoryStream.GetBuffer();
		}
	}
}
