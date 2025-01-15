using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000090 RID: 144
	internal static class Error
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00009F5B File Offset: 0x0000815B
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00009F63 File Offset: 0x00008163
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00009F6B File Offset: 0x0000816B
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009F72 File Offset: 0x00008172
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
