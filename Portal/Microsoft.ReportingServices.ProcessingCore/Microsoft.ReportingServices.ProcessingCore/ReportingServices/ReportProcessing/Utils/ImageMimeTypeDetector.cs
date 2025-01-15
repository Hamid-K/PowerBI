using System;
using System.IO;

namespace Microsoft.ReportingServices.ReportProcessing.Utils
{
	// Token: 0x020007A9 RID: 1961
	internal static class ImageMimeTypeDetector
	{
		// Token: 0x06006EF0 RID: 28400 RVA: 0x001CFAC0 File Offset: 0x001CDCC0
		public static bool TryDetectMimeType(Stream s, out string mimeType)
		{
			if (s == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!s.CanRead || !s.CanSeek)
			{
				throw new ArgumentException("Stream cannot be read");
			}
			mimeType = null;
			byte[] array = new byte[8];
			long position = s.Position;
			s.Seek(0L, SeekOrigin.Begin);
			int i = 0;
			int num = 0;
			while (i < 8)
			{
				num = s.Read(array, num, 8 - i);
				if (num <= 0)
				{
					break;
				}
				i += num;
			}
			s.Seek(position, SeekOrigin.Begin);
			if (i < 8)
			{
				return false;
			}
			if (array == null)
			{
				return false;
			}
			if (ImageMimeTypeDetector.IsPrefix(array, ImageMimeTypeDetector.BmpSignature))
			{
				mimeType = "image/bmp";
			}
			else if (ImageMimeTypeDetector.IsPrefix(array, ImageMimeTypeDetector.JpegSignature))
			{
				mimeType = "image/jpeg";
			}
			else if (ImageMimeTypeDetector.IsPrefix(array, ImageMimeTypeDetector.Gif87aSignature))
			{
				mimeType = "image/gif";
			}
			else if (ImageMimeTypeDetector.IsPrefix(array, ImageMimeTypeDetector.Gif89aSignature))
			{
				mimeType = "image/gif";
			}
			else if (ImageMimeTypeDetector.IsPrefix(array, ImageMimeTypeDetector.PngSignature))
			{
				mimeType = "image/png";
			}
			return mimeType != null;
		}

		// Token: 0x06006EF1 RID: 28401 RVA: 0x001CFBB4 File Offset: 0x001CDDB4
		private static bool IsPrefix(byte[] bytes, byte[] prefix)
		{
			if (bytes.Length < prefix.Length)
			{
				return false;
			}
			for (int i = 0; i < prefix.Length; i++)
			{
				if (bytes[i] != prefix[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400397D RID: 14717
		private static readonly byte[] Gif87aSignature = new byte[] { 71, 73, 70, 56, 55, 97 };

		// Token: 0x0400397E RID: 14718
		private static readonly byte[] Gif89aSignature = new byte[] { 71, 73, 70, 56, 57, 97 };

		// Token: 0x0400397F RID: 14719
		private static readonly byte[] PngSignature = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };

		// Token: 0x04003980 RID: 14720
		private static readonly byte[] JpegSignature = new byte[] { byte.MaxValue, 216, byte.MaxValue };

		// Token: 0x04003981 RID: 14721
		private static readonly byte[] BmpSignature = new byte[] { 66, 77 };

		// Token: 0x04003982 RID: 14722
		private const string GifMimetype = "image/gif";

		// Token: 0x04003983 RID: 14723
		private const string PngMimeType = "image/png";

		// Token: 0x04003984 RID: 14724
		private const string JpegMimeType = "image/jpeg";

		// Token: 0x04003985 RID: 14725
		private const string BmpMimeType = "image/bmp";

		// Token: 0x04003986 RID: 14726
		private const int MaxMimeTypeSize = 8;
	}
}
