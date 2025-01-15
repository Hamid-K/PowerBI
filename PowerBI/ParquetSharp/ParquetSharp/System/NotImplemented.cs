using System;

namespace System
{
	// Token: 0x020000D4 RID: 212
	internal static class NotImplemented
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00020FEC File Offset: 0x0001F1EC
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00020FFC File Offset: 0x0001F1FC
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
