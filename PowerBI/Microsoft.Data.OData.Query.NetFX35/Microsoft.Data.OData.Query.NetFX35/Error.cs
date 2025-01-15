using System;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x02000061 RID: 97
	internal static class Error
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000DAD7 File Offset: 0x0000BCD7
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000DADF File Offset: 0x0000BCDF
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000DAE7 File Offset: 0x0000BCE7
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
