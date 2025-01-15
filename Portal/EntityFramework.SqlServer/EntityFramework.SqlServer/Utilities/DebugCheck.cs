using System;
using System.Diagnostics;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000029 RID: 41
	internal class DebugCheck
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x000103A3 File Offset: 0x0000E5A3
		[Conditional("DEBUG")]
		public static void NotNull<T>(T value) where T : class
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000103A5 File Offset: 0x0000E5A5
		[Conditional("DEBUG")]
		public static void NotNull<T>(T? value) where T : struct
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000103A7 File Offset: 0x0000E5A7
		[Conditional("DEBUG")]
		public static void NotEmpty(string value)
		{
		}
	}
}
