using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C3 RID: 451
	public static class CompileCheck
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x00009B3B File Offset: 0x00007D3B
		public static void IsValidReferenceField<T>() where T : class
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00009B3B File Offset: 0x00007D3B
		public static void IsValidValueField<T>() where T : struct
		{
		}
	}
}
