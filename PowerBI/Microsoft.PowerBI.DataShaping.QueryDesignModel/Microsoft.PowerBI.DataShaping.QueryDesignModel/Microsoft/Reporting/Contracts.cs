using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Reporting
{
	// Token: 0x020000C9 RID: 201
	internal static class Contracts
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x000215B0 File Offset: 0x0001F7B0
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

		// Token: 0x06000CBC RID: 3260 RVA: 0x000215E4 File Offset: 0x0001F7E4
		private static int Size<T>(IList<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000215F1 File Offset: 0x0001F7F1
		public static Exception Except()
		{
			return new InvalidOperationException();
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000215F8 File Offset: 0x0001F7F8
		public static Exception Except(string msg)
		{
			return new InvalidOperationException(msg);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00021600 File Offset: 0x0001F800
		public static Exception Except<T>(string msg, T arg)
		{
			return new InvalidOperationException(Contracts.GetMsg(msg, new object[] { arg }));
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002161C File Offset: 0x0001F81C
		public static Exception Except(string msg, params object[] args)
		{
			return new InvalidOperationException(Contracts.GetMsg(msg, args));
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002162A File Offset: 0x0001F82A
		public static Exception ExceptRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00021632 File Offset: 0x0001F832
		public static Exception ExceptRange(string paramName, string msg)
		{
			return new ArgumentOutOfRangeException(paramName, msg);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002163B File Offset: 0x0001F83B
		public static Exception ExceptParam(string paramName)
		{
			return new ArgumentException(paramName);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00021643 File Offset: 0x0001F843
		public static Exception ExceptParam(string paramName, string msg)
		{
			return new ArgumentException(msg, paramName);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002164C File Offset: 0x0001F84C
		public static Exception ExceptParam<T>(string paramName, string msg, T arg)
		{
			return new ArgumentException(Contracts.GetMsg(msg, new object[] { arg }), paramName);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00021669 File Offset: 0x0001F869
		public static Exception ExceptValue(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00021671 File Offset: 0x0001F871
		public static Exception ExceptValue(string paramName, string msg)
		{
			return new ArgumentNullException(paramName, msg);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002167A File Offset: 0x0001F87A
		public static Exception ExceptEmpty(string paramName)
		{
			return new ArgumentException(paramName);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00021682 File Offset: 0x0001F882
		public static Exception ExceptEmpty(string paramName, string msg)
		{
			return new ArgumentException(msg, paramName);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002168B File Offset: 0x0001F88B
		public static Exception ExceptValid(string paramName)
		{
			return new ArgumentException(paramName);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00021693 File Offset: 0x0001F893
		public static Exception ExceptValid(string paramName, string msg)
		{
			return new ArgumentException(msg, paramName);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002169C File Offset: 0x0001F89C
		public static void Check(bool f)
		{
			if (!f)
			{
				throw Contracts.Except();
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000216A7 File Offset: 0x0001F8A7
		public static void Check(bool f, string msg)
		{
			if (!f)
			{
				throw Contracts.Except(msg);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000216B3 File Offset: 0x0001F8B3
		public static void CheckNonEmpty(string s, string paramName)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw Contracts.ExceptEmpty(paramName);
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000216C4 File Offset: 0x0001F8C4
		public static void CheckNonEmpty(string s, string paramName, string msg)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw Contracts.ExceptEmpty(paramName, msg);
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000216D6 File Offset: 0x0001F8D6
		public static void CheckRange(bool f, string paramName)
		{
			if (!f)
			{
				throw Contracts.ExceptRange(paramName);
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000216E2 File Offset: 0x0001F8E2
		public static void CheckRange(bool f, string paramName, string msg)
		{
			if (!f)
			{
				throw Contracts.ExceptRange(paramName, msg);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000216EF File Offset: 0x0001F8EF
		public static void CheckParam(bool f, string paramName)
		{
			if (!f)
			{
				throw Contracts.ExceptParam(paramName);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x000216FB File Offset: 0x0001F8FB
		public static void CheckParam(bool f, string paramName, string msg)
		{
			if (!f)
			{
				throw Contracts.ExceptParam(paramName, msg);
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00021708 File Offset: 0x0001F908
		public static void CheckValue<T>(T val, string paramName) where T : class
		{
			if (val == null)
			{
				throw Contracts.ExceptValue(paramName);
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00021719 File Offset: 0x0001F919
		public static void CheckValue<T>(T val, string name, string msg) where T : class
		{
			if (val == null)
			{
				throw Contracts.ExceptValue(name, msg);
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002172C File Offset: 0x0001F92C
		public static void CheckAllNonEmpty(IList<string> args, string paramName)
		{
			for (int i = 0; i < Contracts.Size<string>(args); i++)
			{
				if (string.IsNullOrEmpty(args[i]))
				{
					throw Contracts.ExceptEmpty(paramName);
				}
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002175F File Offset: 0x0001F95F
		public static void CheckNonEmpty<T>(IList<T> args, string paramName)
		{
			if (Contracts.Size<T>(args) == 0)
			{
				throw Contracts.ExceptEmpty(paramName);
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00021770 File Offset: 0x0001F970
		public static void CheckAllValues<T>(IList<T> args, string paramName) where T : class
		{
			for (int i = 0; i < Contracts.Size<T>(args); i++)
			{
				if (args[i] == null)
				{
					throw Contracts.ExceptParam(paramName);
				}
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000217A4 File Offset: 0x0001F9A4
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
							throw Contracts.ExceptParam(paramName);
						}
					}
				}
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000217FC File Offset: 0x0001F9FC
		public static void CheckAll<T>(IList<T> args, string paramName) where T : struct, ICheckable
		{
			for (int i = 0; i < Contracts.Size<T>(args); i++)
			{
				T t = args[i];
				if (!t.IsValid)
				{
					throw Contracts.ExceptValid(paramName);
				}
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00021838 File Offset: 0x0001FA38
		[Conditional("INVARIANT_CHECKS")]
		public static void CheckValueOrNull<T>(T val) where T : class
		{
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002183A File Offset: 0x0001FA3A
		[Conditional("INVARIANT_CHECKS")]
		public static void CheckValueOrNull<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002183C File Offset: 0x0001FA3C
		[Conditional("INVARIANT_CHECKS")]
		public static void CheckValueOrNull<T>(T val, string name, string msg) where T : class
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002183E File Offset: 0x0001FA3E
		[Conditional("DEBUG")]
		public static void Assert(bool f)
		{
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00021840 File Offset: 0x0001FA40
		[Conditional("DEBUG")]
		public static void Assert(bool f, string msg)
		{
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00021842 File Offset: 0x0001FA42
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s)
		{
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00021844 File Offset: 0x0001FA44
		[Conditional("DEBUG")]
		public static void AssertNonEmpty(string s, string msg)
		{
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00021846 File Offset: 0x0001FA46
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val) where T : class
		{
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00021848 File Offset: 0x0001FA48
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string paramName) where T : class
		{
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002184A File Offset: 0x0001FA4A
		[Conditional("DEBUG")]
		public static void AssertValue<T>(T val, string name, string msg) where T : class
		{
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002184C File Offset: 0x0001FA4C
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args)
		{
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002184E File Offset: 0x0001FA4E
		[Conditional("DEBUG")]
		public static void AssertAllNonEmpty(IList<string> args, string msg)
		{
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00021850 File Offset: 0x0001FA50
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IList<T> args)
		{
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00021852 File Offset: 0x0001FA52
		[Conditional("DEBUG")]
		public static void AssertNonEmpty<T>(IList<T> args, string msg)
		{
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00021854 File Offset: 0x0001FA54
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args) where T : class
		{
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00021856 File Offset: 0x0001FA56
		[Conditional("DEBUG")]
		public static void AssertAllValues<T>(IList<T> args, string msg) where T : class
		{
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00021858 File Offset: 0x0001FA58
		[Conditional("DEBUG")]
		public static void AssertAllValues<TKey, TValue>(IDictionary<TKey, TValue> args) where TValue : class
		{
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002185A File Offset: 0x0001FA5A
		[Conditional("DEBUG")]
		public static void AssertAll<T>(IList<T> args) where T : struct, ICheckable
		{
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002185C File Offset: 0x0001FA5C
		[Conditional("DEBUG")]
		public static void AssertAll<K, V>(IDictionary<K, V> args) where V : struct, ICheckable
		{
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002185E File Offset: 0x0001FA5E
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val) where T : class
		{
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00021860 File Offset: 0x0001FA60
		[Conditional("INVARIANT_CHECKS")]
		public static void AssertValueOrNull<T>(T val, string msg) where T : class
		{
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00021862 File Offset: 0x0001FA62
		[Conditional("DEBUG")]
		public static void AssertInstanceOf<T>(object val, string msg)
		{
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00021864 File Offset: 0x0001FA64
		public static bool Verify(this bool f)
		{
			return f;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00021867 File Offset: 0x0001FA67
		public static T VerifyValue<T>(this T val) where T : class
		{
			return val;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002186A File Offset: 0x0001FA6A
		public static T VerifyValue<T>(this T val, string paramName) where T : class
		{
			return val;
		}
	}
}
