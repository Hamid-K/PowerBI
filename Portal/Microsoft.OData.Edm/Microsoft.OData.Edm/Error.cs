using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D6 RID: 214
	internal static class Error
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x0000F7B9 File Offset: 0x0000D9B9
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000F7C1 File Offset: 0x0000D9C1
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000F7C9 File Offset: 0x0000D9C9
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
