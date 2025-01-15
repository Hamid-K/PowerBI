using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000004 RID: 4
	public static class ImageUtils
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000307C File Offset: 0x0000127C
		public static void ApplyImageProperties(Image image, ref byte[] imageData)
		{
			if (image.RawFormat.Equals(ImageFormat.Jpeg))
			{
				RotateFlipType rotateFlipType = ImageUtils.GetRotateFlipType(image);
				if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
				{
					image.RotateFlip(rotateFlipType);
					image.RemovePropertyItem(274);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						image.Save(memoryStream, ImageFormat.Jpeg);
						imageData = memoryStream.GetBuffer();
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000030F0 File Offset: 0x000012F0
		private static RotateFlipType GetRotateFlipType(Image image)
		{
			if (!image.PropertyIdList.Contains(274))
			{
				return RotateFlipType.RotateNoneFlipNone;
			}
			switch (BitConverter.ToUInt16(image.GetPropertyItem(274).Value, 0))
			{
			default:
				return RotateFlipType.RotateNoneFlipNone;
			case 2:
				return RotateFlipType.RotateNoneFlipX;
			case 3:
				return RotateFlipType.Rotate180FlipNone;
			case 4:
				return RotateFlipType.Rotate180FlipX;
			case 5:
				return RotateFlipType.Rotate90FlipX;
			case 6:
				return RotateFlipType.Rotate90FlipNone;
			case 7:
				return RotateFlipType.Rotate270FlipX;
			case 8:
				return RotateFlipType.Rotate270FlipNone;
			}
		}

		// Token: 0x04000007 RID: 7
		private const int ExifOrientationID = 274;
	}
}
