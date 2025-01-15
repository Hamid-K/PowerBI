using System;
using System.IO;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005CC RID: 1484
	internal static class ImageMimeTypeDetector
	{
		// Token: 0x06005398 RID: 21400 RVA: 0x00160684 File Offset: 0x0015E884
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

		// Token: 0x06005399 RID: 21401 RVA: 0x00160778 File Offset: 0x0015E978
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

		// Token: 0x04002A1A RID: 10778
		private static readonly byte[] Gif87aSignature = new byte[] { 71, 73, 70, 56, 55, 97 };

		// Token: 0x04002A1B RID: 10779
		private static readonly byte[] Gif89aSignature = new byte[] { 71, 73, 70, 56, 57, 97 };

		// Token: 0x04002A1C RID: 10780
		private static readonly byte[] PngSignature = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };

		// Token: 0x04002A1D RID: 10781
		private static readonly byte[] JpegSignature = new byte[] { byte.MaxValue, 216, byte.MaxValue };

		// Token: 0x04002A1E RID: 10782
		private static readonly byte[] BmpSignature = new byte[] { 66, 77 };

		// Token: 0x04002A1F RID: 10783
		private const string GifMimetype = "image/gif";

		// Token: 0x04002A20 RID: 10784
		private const string PngMimeType = "image/png";

		// Token: 0x04002A21 RID: 10785
		private const string JpegMimeType = "image/jpeg";

		// Token: 0x04002A22 RID: 10786
		private const string BmpMimeType = "image/bmp";

		// Token: 0x04002A23 RID: 10787
		private const int MaxMimeTypeSize = 8;
	}
}
