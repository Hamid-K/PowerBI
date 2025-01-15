using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CF RID: 207
	internal static class Error
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x0000E44C File Offset: 0x0000C64C
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000E454 File Offset: 0x0000C654
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000E45C File Offset: 0x0000C65C
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000E463 File Offset: 0x0000C663
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
