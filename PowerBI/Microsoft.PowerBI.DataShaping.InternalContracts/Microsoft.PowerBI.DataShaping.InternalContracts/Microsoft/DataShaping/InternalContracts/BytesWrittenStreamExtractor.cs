using System;
using System.IO;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000006 RID: 6
	internal static class BytesWrittenStreamExtractor
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		internal static long GetBytesWrittenLength(Stream stream)
		{
			WriteTrackerStream writeTrackerStream = stream as WriteTrackerStream;
			if (writeTrackerStream != null)
			{
				return writeTrackerStream.BytesWritten;
			}
			long num;
			try
			{
				num = stream.Length;
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				num = -1L;
			}
			return num;
		}
	}
}
