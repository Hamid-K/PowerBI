using System;
using System.IO;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000149 RID: 329
	internal static class StreamHelper
	{
		// Token: 0x0600115A RID: 4442 RVA: 0x0003CC2F File Offset: 0x0003AE2F
		public static bool TryReadBuffer(Stream stream, byte[] buffer, int offset, int count, int minCount, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, minCount, out bytesRead);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003CC3E File Offset: 0x0003AE3E
		public static bool TryFillBuffer(Stream stream, byte[] buffer, int offset, int count, out int bytesRead)
		{
			return StreamHelper.TryReadBufferImpl(stream, buffer, offset, count, count, out bytesRead);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003CC4C File Offset: 0x0003AE4C
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
