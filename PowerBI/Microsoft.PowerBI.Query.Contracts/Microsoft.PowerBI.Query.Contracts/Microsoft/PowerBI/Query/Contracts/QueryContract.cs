using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.PowerBI.Query.Contracts
{
	// Token: 0x02000008 RID: 8
	public static class QueryContract
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002091 File Offset: 0x00000291
		public static void RetailAssert(bool condition, string message)
		{
			if (!condition)
			{
				QueryContract.Fail(message);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		public static void RetailAssert(bool condition, string message, object arg)
		{
			if (!condition)
			{
				QueryContract.Fail(QueryContract.GetMessage(message, new object[] { arg }));
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020B6 File Offset: 0x000002B6
		public static void RetailAssert(bool condition, string message, object arg0, object arg1)
		{
			if (!condition)
			{
				QueryContract.Fail(QueryContract.GetMessage(message, new object[] { arg0, arg1 }));
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020D4 File Offset: 0x000002D4
		public static void RetailAssert(bool condition, string message, object arg0, object arg1, object arg2)
		{
			if (!condition)
			{
				QueryContract.Fail(QueryContract.GetMessage(message, new object[] { arg0, arg1, arg2 }));
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F7 File Offset: 0x000002F7
		public static void RetailFail(string message)
		{
			QueryContract.RetailAssert(false, message);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002100 File Offset: 0x00000300
		public static void RetailFail(string message, object arg)
		{
			QueryContract.RetailAssert(false, message, arg);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000210A File Offset: 0x0000030A
		public static void RetailFail(string message, object arg0, object arg1)
		{
			QueryContract.RetailAssert(false, message, arg0, arg1);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002115 File Offset: 0x00000315
		public static void RetailFail(string message, object arg0, object arg1, object arg2)
		{
			QueryContract.RetailAssert(false, message, arg0, arg1, arg2);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002121 File Offset: 0x00000321
		internal static bool TrySetFailAction(Action<string> pfn)
		{
			Interlocked.CompareExchange<Action<string>>(ref QueryContract._pfnFail, pfn, null);
			return QueryContract._pfnFail == pfn;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000213D File Offset: 0x0000033D
		private static void FailCore(string msg)
		{
			if (QueryContract._pfnFail != null)
			{
				QueryContract._pfnFail(msg);
			}
			throw new InvalidOperationException(msg);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000215B File Offset: 0x0000035B
		private static void Fail()
		{
			QueryContract.FailCore("Assertion Failed");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002167 File Offset: 0x00000367
		private static void Fail(string msg)
		{
			QueryContract.FailCore(msg);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000216F File Offset: 0x0000036F
		private static void FailValue()
		{
			QueryContract.FailCore("Non-null assertion failure");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000217B File Offset: 0x0000037B
		private static void FailValue(string paramName)
		{
			QueryContract.FailCore(QueryContract.GetMessage("Non-null assertion failure: {0}", new object[] { paramName }));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002196 File Offset: 0x00000396
		private static void FailValue(string paramName, string msg)
		{
			QueryContract.FailCore(QueryContract.GetMessage("Non-null assertion failure: {0}: {1}", new object[] { paramName, msg }));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021B5 File Offset: 0x000003B5
		private static void FailEmpty()
		{
			QueryContract.FailCore("Non-empty assertion failure");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021C1 File Offset: 0x000003C1
		private static void FailEmpty(string msg)
		{
			QueryContract.FailCore(QueryContract.GetMessage("Non-empty assertion failure: {0}", new object[] { msg }));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021DC File Offset: 0x000003DC
		private static void FailType(string paramName)
		{
			QueryContract.FailCore(QueryContract.GetMessage("Of-type assertion failure: {0}", new object[] { paramName }));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021F7 File Offset: 0x000003F7
		[Conditional("DEBUG")]
		public static void Assert(bool f)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021F9 File Offset: 0x000003F9
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021FB File Offset: 0x000003FB
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg, object arg0)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021FD File Offset: 0x000003FD
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg, object arg0, object arg1)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021FF File Offset: 0x000003FF
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002201 File Offset: 0x00000401
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002203 File Offset: 0x00000403
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s, string msg)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002205 File Offset: 0x00000405
		[Conditional("DEBUG")]
		public static void AssertNonWhitespace(string arg, string msg)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002207 File Offset: 0x00000407
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val) where T : class
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002209 File Offset: 0x00000409
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000220B File Offset: 0x0000040B
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T? val, string paramName) where T : struct
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000220D File Offset: 0x0000040D
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string name, string msg) where T : class
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000220F File Offset: 0x0000040F
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002211 File Offset: 0x00000411
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args, string msg)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002213 File Offset: 0x00000413
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IList<T> args)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002215 File Offset: 0x00000415
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IReadOnlyList<T> args)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002217 File Offset: 0x00000417
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IList<T> args, string msg)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002219 File Offset: 0x00000419
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IReadOnlyList<T> args, string msg)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000221B File Offset: 0x0000041B
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args) where T : class
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000221D File Offset: 0x0000041D
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args, string msg) where T : class
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000221F File Offset: 0x0000041F
		[Conditional("DEBUG")]
		public static void AssertAllValues<TKey, TValue>(IDictionary<TKey, TValue> args) where TValue : class
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002221 File Offset: 0x00000421
		[Conditional("DEBUG")]
		public static void AssertAll<T>(IList<T> args) where T : struct, ICheckable
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002223 File Offset: 0x00000423
		[Conditional("DEBUG")]
		public static void AssertAll<K, V>(IDictionary<K, V> args) where V : struct, ICheckable
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002225 File Offset: 0x00000425
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val) where T : class
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002227 File Offset: 0x00000427
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val, string msg) where T : class
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002229 File Offset: 0x00000429
		[Conditional("DEBUG")]
		public static void AssertInstanceOf<T>(object val, string msg)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000222B File Offset: 0x0000042B
		[Conditional("DEBUG")]
		public static void AssertRange(int value, string msg, int low, int high)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000222D File Offset: 0x0000042D
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsOfType<T>(object val, string paramName) where T : class
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002230 File Offset: 0x00000430
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

		// Token: 0x06000038 RID: 56 RVA: 0x00002264 File Offset: 0x00000464
		private static int Size<T>(IList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002271 File Offset: 0x00000471
		private static int Size<T>(IReadOnlyList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x04000030 RID: 48
		private static volatile Action<string> _pfnFail;
	}
}
