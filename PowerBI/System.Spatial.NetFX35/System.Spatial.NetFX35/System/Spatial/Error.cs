using System;

namespace System.Spatial
{
	// Token: 0x0200008F RID: 143
	internal static class Error
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000A003 File Offset: 0x00008203
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A00B File Offset: 0x0000820B
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A013 File Offset: 0x00008213
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A01A File Offset: 0x0000821A
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
