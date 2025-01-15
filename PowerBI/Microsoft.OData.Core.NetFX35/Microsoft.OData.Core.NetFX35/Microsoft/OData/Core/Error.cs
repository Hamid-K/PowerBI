using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020002C8 RID: 712
	internal static class Error
	{
		// Token: 0x06001B82 RID: 7042 RVA: 0x0005900F File Offset: 0x0005720F
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00059017 File Offset: 0x00057217
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0005901F File Offset: 0x0005721F
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00059026 File Offset: 0x00057226
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
