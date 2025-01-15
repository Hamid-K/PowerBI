using System;

namespace System
{
	// Token: 0x02000016 RID: 22
	internal static class NotImplemented
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00009B5D File Offset: 0x00007D5D
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009B64 File Offset: 0x00007D64
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009B5D File Offset: 0x00007D5D
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
