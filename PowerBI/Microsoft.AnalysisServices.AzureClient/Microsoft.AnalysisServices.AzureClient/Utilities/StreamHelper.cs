using System;
using System.IO;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000038 RID: 56
	internal static class StreamHelper
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00008D77 File Offset: 0x00006F77
		public static bool TryReadBuffer(Stream stream, byte[] buffer, int offset, int count, int minCount, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, minCount, out bytesRead);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008D86 File Offset: 0x00006F86
		public static bool TryFillBuffer(Stream stream, byte[] buffer, int offset, int count, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, count, out bytesRead);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008D94 File Offset: 0x00006F94
		private static bool TryReadBufferImpl(Stream stream, byte[] buffer, int offset, int count, int minCount, out int bytesRead)
		{
			bytesRead = 0;
			for (;;)
			{
				int num = stream.Read(buffer, offset + bytesRead, count - bytesRead);
				if (num == 0)
				{
					break;
				}
				bytesRead += num;
				if (bytesRead >= count)
				{
					return true;
				}
			}
			return bytesRead >= minCount;
		}
	}
}
