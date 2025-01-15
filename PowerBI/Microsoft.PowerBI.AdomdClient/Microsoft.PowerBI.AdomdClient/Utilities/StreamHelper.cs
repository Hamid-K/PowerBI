using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000154 RID: 340
	internal static class StreamHelper
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x00039FFB File Offset: 0x000381FB
		public static bool TryReadBuffer(Stream stream, byte[] buffer, int offset, int count, int minCount, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, minCount, out bytesRead);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003A00A File Offset: 0x0003820A
		public static bool TryFillBuffer(Stream stream, byte[] buffer, int offset, int count, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, count, out bytesRead);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003A018 File Offset: 0x00038218
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
