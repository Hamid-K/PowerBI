using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000154 RID: 340
	internal static class StreamHelper
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x0003A32B File Offset: 0x0003852B
		public static bool TryReadBuffer(Stream stream, byte[] buffer, int offset, int count, int minCount, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, minCount, out bytesRead);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003A33A File Offset: 0x0003853A
		public static bool TryFillBuffer(Stream stream, byte[] buffer, int offset, int count, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, count, out bytesRead);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003A348 File Offset: 0x00038548
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
