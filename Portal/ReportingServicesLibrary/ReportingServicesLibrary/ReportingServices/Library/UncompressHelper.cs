using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200029F RID: 671
	internal static class UncompressHelper
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x000639A0 File Offset: 0x00061BA0
		internal static void Uncompress(long version, byte[] compressedBuffer, int uncompressedBufferLength, out byte[] uncompressedBuffer, ref long compressionTime)
		{
			Timer timer = new Timer();
			try
			{
				timer.StartTimer();
				if (version != 2L)
				{
					throw new InternalCatalogException("unexpected version for compressed read stream" + version.ToString(CultureInfo.InvariantCulture));
				}
				UncompressHelper.UncompressManagedGZip(compressedBuffer, uncompressedBufferLength, out uncompressedBuffer);
			}
			finally
			{
				compressionTime += timer.ElapsedTimeMs();
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00063A04 File Offset: 0x00061C04
		private static void UncompressManagedGZip(byte[] compressedBuffer, int uncompressedBufferLength, out byte[] uncompressedBuffer)
		{
			using (MemoryStream memoryStream = new MemoryStream(compressedBuffer))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					uncompressedBuffer = new byte[uncompressedBufferLength];
					int num = StreamSupport.ReadToCountOrEnd(uncompressedBuffer, 0, uncompressedBufferLength, new StreamSupport.StreamRead(gzipStream.Read));
					if (num != uncompressedBufferLength)
					{
						throw new InternalCatalogException(string.Concat(new object[] { "expected ", uncompressedBufferLength, " uncompressed bytes, found ", num }));
					}
				}
			}
		}
	}
}
