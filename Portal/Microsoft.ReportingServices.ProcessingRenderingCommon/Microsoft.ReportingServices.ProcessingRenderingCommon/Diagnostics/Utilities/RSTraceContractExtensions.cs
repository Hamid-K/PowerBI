using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B3 RID: 179
	internal static class RSTraceContractExtensions
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x000111D8 File Offset: 0x0000F3D8
		[Conditional("DEBUG")]
		public static void AssertValue<T>(this RSTrace tracer, T val, string paramName) where T : class
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000111DA File Offset: 0x0000F3DA
		[Conditional("DEBUG")]
		public static void AssertValue<T>(this RSTrace tracer, T? val, string paramName) where T : struct
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000111DC File Offset: 0x0000F3DC
		[Conditional("DEBUG")]
		public static void AssertValue<T>(this RSTrace tracer, T val, string name, string msg) where T : class
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000111DE File Offset: 0x0000F3DE
		[Conditional("DEBUG")]
		public static void AssertValue<T>(this RSTrace tracer, T? val, string name, string msg) where T : struct
		{
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000111E0 File Offset: 0x0000F3E0
		[Conditional("DEBUG")]
		public static void AssertValueOfTypeOrNull<T>(this RSTrace tracer, object value, string name) where T : class
		{
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000111E2 File Offset: 0x0000F3E2
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(this RSTrace tracer, string s, string msg)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000111E4 File Offset: 0x0000F3E4
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(this RSTrace tracer, T val, string msg) where T : class
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000111E6 File Offset: 0x0000F3E6
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(this RSTrace tracer, IList<T> args, string msg) where T : class
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000111E8 File Offset: 0x0000F3E8
		public static void Fail(this RSTrace tracer, string message, params object[] args)
		{
			tracer.Assert(false, message, args);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000111F3 File Offset: 0x0000F3F3
		private static void DbgFailEmpty(RSTrace tracer, string msg)
		{
			tracer.Assert(false, "Non-empty assertion failure: {0}", new object[] { msg });
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001120B File Offset: 0x0000F40B
		private static void DbgFailValue(RSTrace tracer, string paramName)
		{
			tracer.Assert(false, "Non-null assertion failure: {0}", new object[] { paramName });
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00011223 File Offset: 0x0000F423
		private static void DbgFailValue(RSTrace tracer, string paramName, string msg)
		{
			tracer.Assert(false, "Non-null assertion failure: {0}: {1}", new object[] { paramName, msg });
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001123F File Offset: 0x0000F43F
		private static void DbgFailType(RSTrace tracer, string paramName, string typeName)
		{
			tracer.Assert(false, "Type assertion failure: {0} is expected to be {1}", new object[] { paramName, typeName });
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001125B File Offset: 0x0000F45B
		private static int Size<T>(IList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}
	}
}
