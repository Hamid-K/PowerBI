using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.DataShaping
{
	// Token: 0x0200000B RID: 11
	internal static class Contract
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static void RetailAssert(bool condition, string message)
		{
			if (!condition)
			{
				Contract.Fail(message);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FAB File Offset: 0x000011AB
		public static void RetailAssert(bool condition, string message, object arg)
		{
			if (!condition)
			{
				Contract.Fail(Contract.GetMessage(message, new object[] { arg }));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FC5 File Offset: 0x000011C5
		public static void RetailAssert(bool condition, string message, object arg0, object arg1)
		{
			if (!condition)
			{
				Contract.Fail(Contract.GetMessage(message, new object[] { arg0, arg1 }));
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FE3 File Offset: 0x000011E3
		public static void RetailAssert(bool condition, string message, object arg0, object arg1, object arg2)
		{
			if (!condition)
			{
				Contract.Fail(Contract.GetMessage(message, new object[] { arg0, arg1, arg2 }));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003006 File Offset: 0x00001206
		public static void RetailAssert(bool condition, string message, object arg0, object arg1, object arg2, object arg3)
		{
			if (!condition)
			{
				Contract.Fail(Contract.GetMessage(message, new object[] { arg0, arg1, arg2, arg3 }));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000302E File Offset: 0x0000122E
		public static void RetailFail(string message)
		{
			Contract.RetailAssert(false, message);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003037 File Offset: 0x00001237
		public static void RetailFail(string message, object arg)
		{
			Contract.RetailAssert(false, message, arg);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003041 File Offset: 0x00001241
		public static void RetailFail(string message, object arg0, object arg1)
		{
			Contract.RetailAssert(false, message, arg0, arg1);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000304C File Offset: 0x0000124C
		public static void RetailFail(string message, object arg0, object arg1, object arg2)
		{
			Contract.RetailAssert(false, message, arg0, arg1, arg2);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003058 File Offset: 0x00001258
		internal static bool TrySetFailAction(Action<string> pfn)
		{
			Interlocked.CompareExchange<Action<string>>(ref Contract._pfnFail, pfn, null);
			return Contract._pfnFail == pfn;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003074 File Offset: 0x00001274
		private static void FailCore(string msg)
		{
			if (Contract._pfnFail != null)
			{
				Contract._pfnFail(msg);
			}
			throw new InvalidOperationException(msg);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003092 File Offset: 0x00001292
		private static void Fail()
		{
			Contract.FailCore("Assertion Failed");
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000309E File Offset: 0x0000129E
		private static void Fail(string msg)
		{
			Contract.FailCore(msg);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030A6 File Offset: 0x000012A6
		private static void FailValue()
		{
			Contract.FailCore("Non-null assertion failure");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030B2 File Offset: 0x000012B2
		private static void FailValue(string paramName)
		{
			Contract.FailCore(Contract.GetMessage("Non-null assertion failure: {0}", new object[] { paramName }));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000030CD File Offset: 0x000012CD
		private static void FailValue(string paramName, string msg)
		{
			Contract.FailCore(Contract.GetMessage("Non-null assertion failure: {0}: {1}", new object[] { paramName, msg }));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000030EC File Offset: 0x000012EC
		private static void FailEmpty()
		{
			Contract.FailCore("Non-empty assertion failure");
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030F8 File Offset: 0x000012F8
		private static void FailEmpty(string msg)
		{
			Contract.FailCore(Contract.GetMessage("Non-empty assertion failure: {0}", new object[] { msg }));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003113 File Offset: 0x00001313
		private static void FailType(Type type, string paramName)
		{
			Contract.FailCore(Contract.GetMessage("{0} must be of type {1}", new object[] { paramName, type }));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003132 File Offset: 0x00001332
		[Conditional("DEBUG")]
		public static void Assert(bool f)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003134 File Offset: 0x00001334
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003136 File Offset: 0x00001336
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg, object arg0)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003138 File Offset: 0x00001338
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg, object arg0, object arg1)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000313A File Offset: 0x0000133A
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000313C File Offset: 0x0000133C
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000313E File Offset: 0x0000133E
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s, string msg)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003140 File Offset: 0x00001340
		[Conditional("DEBUG")]
		public static void AssertNonWhitespace(string arg, string msg)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003142 File Offset: 0x00001342
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val) where T : class
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003144 File Offset: 0x00001344
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003146 File Offset: 0x00001346
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T? val, string paramName) where T : struct
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003148 File Offset: 0x00001348
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string name, string msg) where T : class
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000314A File Offset: 0x0000134A
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000314C File Offset: 0x0000134C
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args, string msg)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000314E File Offset: 0x0000134E
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IList<T> args)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003150 File Offset: 0x00001350
		[Conditional("DEBUG")]
		public static void AssertNonEmptyReadOnly<T>(IReadOnlyList<T> args)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003152 File Offset: 0x00001352
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IList<T> args, string msg)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003154 File Offset: 0x00001354
		[Conditional("DEBUG")]
		public static void AssertNonEmptyReadOnly<T>(IReadOnlyList<T> args, string msg)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003156 File Offset: 0x00001356
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IEnumerable<T> args, string msg)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003158 File Offset: 0x00001358
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args) where T : class
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000315A File Offset: 0x0000135A
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args, string msg) where T : class
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000315C File Offset: 0x0000135C
		[Conditional("DEBUG")]
		public static void AssertAllValues<TKey, TValue>(IDictionary<TKey, TValue> args) where TValue : class
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000315E File Offset: 0x0000135E
		[Conditional("DEBUG")]
		public static void AssertAll<T>(IList<T> args) where T : struct, ICheckable
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003160 File Offset: 0x00001360
		[Conditional("DEBUG")]
		public static void AssertAll<K, V>(IDictionary<K, V> args) where V : struct, ICheckable
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003162 File Offset: 0x00001362
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val) where T : class
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003164 File Offset: 0x00001364
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val, string msg) where T : class
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003166 File Offset: 0x00001366
		[Conditional("DEBUG")]
		public static void AssertInstanceOf<T>(object val, string paramName)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003168 File Offset: 0x00001368
		[Conditional("DEBUG")]
		public static void AssertRange(int value, string msg, int low, int high)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000316C File Offset: 0x0000136C
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

		// Token: 0x06000085 RID: 133 RVA: 0x000031A0 File Offset: 0x000013A0
		private static int Size<T>(IList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000031AD File Offset: 0x000013AD
		private static int Size<T>(IReadOnlyList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x0400003A RID: 58
		private static volatile Action<string> _pfnFail;
	}
}
