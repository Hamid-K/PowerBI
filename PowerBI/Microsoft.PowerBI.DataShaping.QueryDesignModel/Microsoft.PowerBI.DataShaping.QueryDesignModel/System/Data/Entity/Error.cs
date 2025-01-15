using System;

namespace System.Data.Entity
{
	// Token: 0x02000016 RID: 22
	internal static class Error
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x000099A2 File Offset: 0x00007BA2
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000099AA File Offset: 0x00007BAA
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000099B2 File Offset: 0x00007BB2
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000099B9 File Offset: 0x00007BB9
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
