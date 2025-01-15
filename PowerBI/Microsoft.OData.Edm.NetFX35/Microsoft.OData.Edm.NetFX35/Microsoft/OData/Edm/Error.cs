using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200027E RID: 638
	internal static class Error
	{
		// Token: 0x06000F73 RID: 3955 RVA: 0x0002DDD3 File Offset: 0x0002BFD3
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0002DDDB File Offset: 0x0002BFDB
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0002DDE3 File Offset: 0x0002BFE3
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002DDEA File Offset: 0x0002BFEA
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
