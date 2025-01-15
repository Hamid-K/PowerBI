using System;
using System.Diagnostics;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200008B RID: 139
	internal class DebugCheck
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0001082F File Offset: 0x0000EA2F
		[Conditional("DEBUG")]
		public static void NotNull<T>(T value) where T : class
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00010831 File Offset: 0x0000EA31
		[Conditional("DEBUG")]
		public static void NotNull<T>(T? value) where T : struct
		{
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00010833 File Offset: 0x0000EA33
		[Conditional("DEBUG")]
		public static void NotEmpty(string value)
		{
		}
	}
}
