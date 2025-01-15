using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Microsoft.ReportingServices.Portal
{
	// Token: 0x02000013 RID: 19
	public class ThumbnailUtil
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002B70 File Offset: 0x00000D70
		public static byte[] ShrinkThumbnail(byte[] bytes)
		{
			try
			{
				using (Image image = Image.FromStream(new MemoryStream(bytes)))
				{
					if (image.Width > 460 || image.Height > 460)
					{
						int num = ((image.Width > image.Height) ? 460 : 222);
						int num2 = image.Height * num / image.Width;
						using (Image thumbnailImage = image.GetThumbnailImage(num, num2, () => false, IntPtr.Zero))
						{
							using (MemoryStream memoryStream = new MemoryStream())
							{
								thumbnailImage.Save(memoryStream, ImageFormat.Png);
								return memoryStream.ToArray();
							}
						}
					}
				}
			}
			catch (ArgumentException)
			{
			}
			return bytes;
		}

		// Token: 0x04000054 RID: 84
		private const int ThumbnailWidthLandscape = 460;

		// Token: 0x04000055 RID: 85
		private const int ThumbnailWidthPortrait = 222;
	}
}
