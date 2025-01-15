using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000005 RID: 5
	internal static class Contract
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public static bool TrySetFailAction(Action<string> pfn)
		{
			Interlocked.CompareExchange<Action<string>>(ref Contract._pfnFail, pfn, null);
			return Contract._pfnFail == pfn;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002083 File Offset: 0x00000283
		private static void FailCore(string msg)
		{
			if (Contract._pfnFail != null)
			{
				Contract._pfnFail(msg);
			}
			throw new InvalidOperationException(msg);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A1 File Offset: 0x000002A1
		private static void Fail(string msg)
		{
			Contract.FailCore(msg);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A9 File Offset: 0x000002A9
		private static void FailValue(string paramName)
		{
			Contract.FailCore(Contract.GetMessage("Non-null assertion failure: {0}", new object[] { paramName }));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		private static void FailEmpty(string msg)
		{
			Contract.FailCore(Contract.GetMessage("Non-empty assertion failure: {0}", new object[] { msg }));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020DF File Offset: 0x000002DF
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E1 File Offset: 0x000002E1
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020E3 File Offset: 0x000002E3
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val, string msg) where T : class
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E8 File Offset: 0x000002E8
		private static string GetMessage(string msg, params object[] args)
		{
			try
			{
				msg = string.Format(CultureInfo.CurrentCulture, msg, args);
			}
			catch (FormatException)
			{
			}
			return msg;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000211C File Offset: 0x0000031C
		private static int Size<T>(IList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x04000029 RID: 41
		private static volatile Action<string> _pfnFail;
	}
}
