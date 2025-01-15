using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000B RID: 11
	internal static class Contract
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002663 File Offset: 0x00000863
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void Assert(bool f, string msg)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002665 File Offset: 0x00000865
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void Assert(bool f, string msg, params object[] args)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002667 File Offset: 0x00000867
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmpty(string s, string paramName)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002669 File Offset: 0x00000869
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmpty(string s, string paramName, string msg)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000266B File Offset: 0x0000086B
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000266D File Offset: 0x0000086D
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<T>(T? val, string paramName) where T : struct
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000266F File Offset: 0x0000086F
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValid<T>(T val, string paramName) where T : ICheckable
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002671 File Offset: 0x00000871
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValueGeneric<T>(T val, string paramName)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002673 File Offset: 0x00000873
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue(IntPtr val, string paramName)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002675 File Offset: 0x00000875
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<T>(T val, string paramName, string msg) where T : class
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002677 File Offset: 0x00000877
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<T>(T val, string paramName, string msg, params object[] args) where T : class
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002679 File Offset: 0x00000879
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<TKey, TValue>(KeyValuePair<TKey, TValue> val, string paramName) where TKey : class where TValue : class
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000267B File Offset: 0x0000087B
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<TKey, TValue>(KeyValuePair<TKey, TValue> val, string paramName, string msg) where TKey : class where TValue : class
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000267D File Offset: 0x0000087D
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValue<TKey, TValue>(KeyValuePair<TKey, TValue> val, string paramName, string msg, params object[] args) where TKey : class where TValue : class
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000267F File Offset: 0x0000087F
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args, string paramName)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002681 File Offset: 0x00000881
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args, string paramName, string msg)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002683 File Offset: 0x00000883
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmptyReadOnly<K, T>(IReadOnlyDictionary<K, T> args, string paramName)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002685 File Offset: 0x00000885
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmpty<K, T>(IDictionary<K, T> args, string paramName)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002687 File Offset: 0x00000887
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmpty<T>(ICollection<T> args, string paramName)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002689 File Offset: 0x00000889
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmptyReadOnly<T>(IReadOnlyCollection<T> args, string paramName, string msg)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000268B File Offset: 0x0000088B
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmptyReadOnly<T>(IReadOnlyCollection<T> args, string paramName)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000268D File Offset: 0x0000088D
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmpty<T>(ICollection<T> args, string paramName, string msg)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000268F File Offset: 0x0000088F
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmptyAndAllValues<T>(IList<T> args, string paramName) where T : class
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002691 File Offset: 0x00000891
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertNonEmptyAndAllValues<T>(IList<T> args, string paramName, string msg) where T : class
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002693 File Offset: 0x00000893
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertContainsOrNull<T>(IList<T> items, T item, string paramName)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002695 File Offset: 0x00000895
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertContainsOrNull<T>(IList<T> items, T item, string paramName, string msg)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002697 File Offset: 0x00000897
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args, string paramName) where T : class
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002699 File Offset: 0x00000899
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args, string paramName, string msg) where T : class
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000269B File Offset: 0x0000089B
		[Conditional("DEBUG")]
		public static void AssertAllValuesReadOnly<T>(IReadOnlyList<T> args, string paramName) where T : class
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000269D File Offset: 0x0000089D
		[Conditional("DEBUG")]
		public static void AssertAllValuesReadOnly<T>(IReadOnlyList<T> args, string paramName, string msg) where T : class
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000269F File Offset: 0x0000089F
		[Conditional("DEBUG")]
		public static void AssertAllValues<TKey, TValue>(IDictionary<TKey, TValue> args, string paramName) where TValue : class
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026A1 File Offset: 0x000008A1
		[Conditional("DEBUG")]
		public static void AssertAllValues<TKey, TValue>(IDictionary<TKey, TValue> args, string paramName, string msg) where TValue : class
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026A3 File Offset: 0x000008A3
		[Conditional("DEBUG")]
		public static void AssertAllValuesNested<T>(IList<IList<T>> args, string paramName) where T : class
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000026A5 File Offset: 0x000008A5
		[Conditional("DEBUG")]
		public static void AssertAll<T>(IList<T> args, string msg) where T : struct, ICheckable
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026A7 File Offset: 0x000008A7
		[Conditional("DEBUG")]
		public static void AssertAll<T>(IList<T> args, Func<T, bool> condition, string msg)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026A9 File Offset: 0x000008A9
		[Conditional("DEBUG")]
		public static void AssertAll<T>(IList<T> args, Func<T, T, bool> condition, string msg)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000026AB File Offset: 0x000008AB
		[Conditional("DEBUG")]
		public static void AssertAllReadOnly<T>(IReadOnlyList<T> args, string msg) where T : struct, ICheckable
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000026AD File Offset: 0x000008AD
		[Conditional("DEBUG")]
		public static void AssertAllReadOnly<T>(IReadOnlyList<T> args, Func<T, bool> condition, string msg)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026AF File Offset: 0x000008AF
		[Conditional("DEBUG")]
		public static void AssertAllReadOnly<T>(IReadOnlyList<T> args, Func<T, T, bool> condition, string msg)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026B1 File Offset: 0x000008B1
		[Conditional("DEBUG")]
		public static void AssertAll<K, V>(IDictionary<K, V> args, string msg) where V : struct, ICheckable
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026B3 File Offset: 0x000008B3
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertAllAreOfType<S, T>(IList<S> values, string paramName) where S : class where T : S
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000026B5 File Offset: 0x000008B5
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsOfType<T>(object val, string paramName) where T : class
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000026B7 File Offset: 0x000008B7
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsOfType<T>(object val, string paramName, string msg) where T : class
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000026B9 File Offset: 0x000008B9
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsOfTypeOrNull<T>(object val, string paramName) where T : class
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026BB File Offset: 0x000008BB
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsExactType<T>(object val, string paramName) where T : class
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000026BD File Offset: 0x000008BD
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsExactType<T>(object val, string paramName, string msg) where T : class
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000026BF File Offset: 0x000008BF
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertIsExactTypeOrNull<T>(object val, string paramName) where T : class
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000026C1 File Offset: 0x000008C1
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValueInRange(int value, int exclusiveUpperBound, string paramName)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000026C3 File Offset: 0x000008C3
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValueInRange(int value, int inclusiveLowerBound, int exclusiveUpperBound, string paramName)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000026C5 File Offset: 0x000008C5
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValueInRange(int value, int exclusiveUpperBound, string paramName, string msg)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000026C7 File Offset: 0x000008C7
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void AssertValueInRange(int value, int inclusiveLowerBound, int exclusiveUpperBound, string paramName, string msg)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000026C9 File Offset: 0x000008C9
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		internal static void AssertWeightValue(double value, string paramName)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000026CB File Offset: 0x000008CB
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val, string paramName)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026CD File Offset: 0x000008CD
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val, string paramName, string msg) where T : class
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026CF File Offset: 0x000008CF
		[DebuggerStepThrough]
		public static void Check(bool f, string msg)
		{
			if (!f)
			{
				throw Contract.Except(msg);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000026DB File Offset: 0x000008DB
		[DebuggerStepThrough]
		public static void CheckNonEmpty(string s, string paramName)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw Contract.ExceptEmpty(paramName);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000026EC File Offset: 0x000008EC
		[DebuggerStepThrough]
		public static void CheckNonEmpty(string s, string paramName, string msg)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw Contract.ExceptEmpty(paramName, msg);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000026FE File Offset: 0x000008FE
		[DebuggerStepThrough]
		public static void CheckRange(bool f, string paramName)
		{
			if (!f)
			{
				throw Contract.ExceptRange(paramName);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000270A File Offset: 0x0000090A
		[DebuggerStepThrough]
		public static void CheckRange(bool f, string paramName, string msg)
		{
			if (!f)
			{
				throw Contract.ExceptRange(paramName, msg);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002717 File Offset: 0x00000917
		[DebuggerStepThrough]
		public static void CheckParam(bool f, string paramName)
		{
			if (!f)
			{
				throw Contract.ExceptParam(paramName);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002723 File Offset: 0x00000923
		[DebuggerStepThrough]
		public static void CheckParam(bool f, string paramName, string msg)
		{
			if (!f)
			{
				throw Contract.ExceptParam(paramName, msg);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002730 File Offset: 0x00000930
		[DebuggerStepThrough]
		public static void CheckValue<T>(T val, string paramName) where T : class
		{
			if (val == null)
			{
				throw Contract.ExceptValue(paramName);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002741 File Offset: 0x00000941
		[DebuggerStepThrough]
		public static void CheckValue<T>(T val, string paramName, string msg) where T : class
		{
			if (val == null)
			{
				throw Contract.ExceptValue(paramName, msg);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002754 File Offset: 0x00000954
		public static void CheckAllNonEmpty(IList<string> args, string paramName)
		{
			for (int i = 0; i < Contract.Size<string>(args); i++)
			{
				if (string.IsNullOrEmpty(args[i]))
				{
					throw Contract.ExceptEmpty(paramName);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002787 File Offset: 0x00000987
		[DebuggerStepThrough]
		public static void CheckNonEmpty<T>(ICollection<T> args, string paramName)
		{
			if (Contract.Size<T>(args) == 0)
			{
				throw Contract.ExceptEmpty(paramName);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002798 File Offset: 0x00000998
		[DebuggerStepThrough]
		public static void CheckNonEmptyReadOnly<T>(IReadOnlyCollection<T> args, string paramName)
		{
			if (Contract.Size<T>(args) == 0)
			{
				throw Contract.ExceptEmpty(paramName);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000027A9 File Offset: 0x000009A9
		[DebuggerStepThrough]
		public static void CheckNonEmpty<K, T>(IDictionary<K, T> args, string paramName)
		{
			if (Contract.Size<K, T>(args) == 0)
			{
				throw Contract.ExceptEmpty(paramName);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000027BC File Offset: 0x000009BC
		public static void CheckAllValues<T>(IList<T> args, string paramName) where T : class
		{
			for (int i = 0; i < Contract.Size<T>(args); i++)
			{
				if (args[i] == null)
				{
					throw Contract.ExceptParam(paramName);
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000027F0 File Offset: 0x000009F0
		public static void CheckAllValues<TKey, TValue>(IDictionary<TKey, TValue> args, string paramName) where TValue : class
		{
			if (args != null)
			{
				using (IEnumerator<TValue> enumerator = args.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw Contract.ExceptParam(paramName);
						}
					}
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002848 File Offset: 0x00000A48
		public static void CheckAll<T>(IList<T> args, string paramName) where T : struct, ICheckable
		{
			for (int i = 0; i < Contract.Size<T>(args); i++)
			{
				T t = args[i];
				if (!t.IsValid)
				{
					throw Contract.ExceptValid(paramName);
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002884 File Offset: 0x00000A84
		[Conditional("INVARIANT_CHECKS")]
		public static void CheckValueOrNull<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002886 File Offset: 0x00000A86
		[Conditional("INVARIANT_CHECKS")]
		public static void CheckValueOrNull<T>(T val, string paramName, string msg) where T : class
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002888 File Offset: 0x00000A88
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void DebugFail(string msg)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000288A File Offset: 0x00000A8A
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public static void DebugFail(string msg, params object[] args)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000288C File Offset: 0x00000A8C
		[DebuggerStepThrough]
		public static void TraceFail(ITracer tracer, string msg)
		{
			if (tracer != null)
			{
				tracer.TraceError(msg);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002898 File Offset: 0x00000A98
		[DebuggerStepThrough]
		public static void TraceFail(ITracer tracer, string msg, params object[] args)
		{
			if (tracer != null)
			{
				tracer.TraceError(Contract.GetMsg(msg, args));
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000028AA File Offset: 0x00000AAA
		[DebuggerStepThrough]
		public static void SanitizedTraceFail(ITracer tracer, string msg)
		{
			if (tracer != null)
			{
				tracer.SanitizedTraceError(msg, new string[0]);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000028BC File Offset: 0x00000ABC
		[DebuggerStepThrough]
		public static void SanitizedTraceFail(ITracer tracer, string msg, params object[] args)
		{
			if (tracer != null)
			{
				tracer.SanitizedTraceError(Contract.GetMsg(msg, args), new string[0]);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000028D4 File Offset: 0x00000AD4
		public static Exception Except(string msg)
		{
			return new InvalidOperationException(msg);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000028DC File Offset: 0x00000ADC
		public static Exception Except(string msg, params object[] args)
		{
			return new InvalidOperationException(Contract.GetMsg(msg, args));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000028EA File Offset: 0x00000AEA
		public static Exception Except(string msg, Exception innerException)
		{
			return new InvalidOperationException(msg, innerException);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000028F3 File Offset: 0x00000AF3
		public static Exception Except(string msg, Exception innerException, params object[] args)
		{
			return new InvalidOperationException(Contract.GetMsg(msg, args), innerException);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002902 File Offset: 0x00000B02
		public static Exception ExceptRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000290A File Offset: 0x00000B0A
		public static Exception ExceptRange(string paramName, string msg)
		{
			return new ArgumentOutOfRangeException(paramName, msg);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002913 File Offset: 0x00000B13
		public static Exception ExceptParam(string paramName)
		{
			return new ArgumentException(paramName);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000291B File Offset: 0x00000B1B
		public static Exception ExceptParam(string paramName, string msg)
		{
			return new ArgumentException(msg, paramName);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002924 File Offset: 0x00000B24
		public static Exception ExceptParam<T>(string paramName, string msg, params object[] args)
		{
			return new ArgumentException(Contract.GetMsg(msg, args), paramName);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002933 File Offset: 0x00000B33
		public static Exception ExceptValue(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000293B File Offset: 0x00000B3B
		public static Exception ExceptValue(string paramName, string msg)
		{
			return new ArgumentNullException(paramName, msg);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002944 File Offset: 0x00000B44
		public static Exception ExceptEmpty(string paramName)
		{
			return new ArgumentException(paramName);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000294C File Offset: 0x00000B4C
		public static Exception ExceptEmpty(string paramName, string msg)
		{
			return new ArgumentException(msg, paramName);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002955 File Offset: 0x00000B55
		public static Exception ExceptValid(string paramName)
		{
			return new ArgumentException(paramName);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000295D File Offset: 0x00000B5D
		public static Exception ExceptValid(string paramName, string msg)
		{
			return new ArgumentException(msg, paramName);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002966 File Offset: 0x00000B66
		public static Exception ExceptNotSupported()
		{
			return new NotSupportedException();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000296D File Offset: 0x00000B6D
		public static Exception ExceptNotSupported(string msg)
		{
			return new NotSupportedException(msg);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002975 File Offset: 0x00000B75
		internal static bool TrySetDebugFail(Action<string> failAction)
		{
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002978 File Offset: 0x00000B78
		private static string GetMsg(string msg, params object[] args)
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

		// Token: 0x06000083 RID: 131 RVA: 0x000029AC File Offset: 0x00000BAC
		private static int Size<T>(ICollection<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000029B9 File Offset: 0x00000BB9
		private static int Size<T>(IReadOnlyCollection<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000029C6 File Offset: 0x00000BC6
		private static int Size<K, T>(IDictionary<K, T> dictionary)
		{
			if (dictionary != null)
			{
				return dictionary.Count;
			}
			return 0;
		}
	}
}
