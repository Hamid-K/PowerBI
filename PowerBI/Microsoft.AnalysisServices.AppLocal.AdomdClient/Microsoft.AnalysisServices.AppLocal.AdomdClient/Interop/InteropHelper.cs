using System;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000136 RID: 310
	internal static class InteropHelper
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x000366A2 File Offset: 0x000348A2
		public static IntPtr Add(IntPtr pointer, int offset)
		{
			return IntPtr.Add(pointer, offset);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000366AC File Offset: 0x000348AC
		public static bool IsIncludedInBuffer(IntPtr buffer, int size, IntPtr pointer, out int offset)
		{
			long num = pointer.ToInt64() - buffer.ToInt64();
			if (num >= 0L && num < (long)size)
			{
				offset = (int)num;
				return true;
			}
			offset = -1;
			return false;
		}
	}
}
