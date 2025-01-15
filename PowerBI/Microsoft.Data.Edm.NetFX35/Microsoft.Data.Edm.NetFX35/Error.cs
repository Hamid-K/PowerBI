using System;

namespace Microsoft.Data.Edm
{
	// Token: 0x02000244 RID: 580
	internal static class Error
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x0002C2FF File Offset: 0x0002A4FF
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0002C307 File Offset: 0x0002A507
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0002C30F File Offset: 0x0002A50F
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0002C316 File Offset: 0x0002A516
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
