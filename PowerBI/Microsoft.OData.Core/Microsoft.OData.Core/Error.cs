using System;

namespace Microsoft.OData
{
	// Token: 0x020000F2 RID: 242
	internal static class Error
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x00021D9D File Offset: 0x0001FF9D
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00021DA5 File Offset: 0x0001FFA5
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00021DAD File Offset: 0x0001FFAD
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
