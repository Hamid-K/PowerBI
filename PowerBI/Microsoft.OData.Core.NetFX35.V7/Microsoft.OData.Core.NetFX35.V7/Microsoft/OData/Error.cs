using System;

namespace Microsoft.OData
{
	// Token: 0x020000DE RID: 222
	internal static class Error
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0001BBBC File Offset: 0x00019DBC
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001BBCC File Offset: 0x00019DCC
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001BBD3 File Offset: 0x00019DD3
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
