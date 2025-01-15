using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000416 RID: 1046
	internal static class ReleaseAssert
	{
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0006F64B File Offset: 0x0006D84B
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0006F652 File Offset: 0x0006D852
		public static bool ThrowOnAssert
		{
			get
			{
				return ReleaseAssert.s_throwOnAssert;
			}
			set
			{
				ReleaseAssert.s_throwOnAssert = value;
			}
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0006F65C File Offset: 0x0006D85C
		public static void Fail(string format, params object[] args)
		{
			string text;
			if (args != null && args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					IDumpable dumpable = args[i] as IDumpable;
					if (dumpable != null)
					{
						args[i] = dumpable.Dump();
					}
				}
				text = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				text = format;
			}
			EventLogWriter.WriteError("Assert", "Assert Failed: {0}\nStack:{1}", new object[]
			{
				text,
				Environment.StackTrace
			});
			ConfigFile config = ConfigFile.Config;
			string stringValue = config.GetStringValue("dumpMethod", false, null);
			if (stringValue != null)
			{
				string[] array = stringValue.Split(new char[] { ':' });
				if (array.Length == 2)
				{
					Type type = Type.GetType(array[0]);
					type.InvokeMember(array[1], BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, null, CultureInfo.InvariantCulture);
				}
			}
			if (ReleaseAssert.ThrowOnAssert)
			{
				throw new ReleaseAssertException(text);
			}
			Environment.FailFast(text);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0006F73E File Offset: 0x0006D93E
		public static void IsTrue(bool condition, string format, object[] args)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, args);
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0006F74A File Offset: 0x0006D94A
		public static void IsTrue(bool condition, string format)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[0]);
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0006F75C File Offset: 0x0006D95C
		public static void IsTrue<T1>(bool condition, string format, T1 t1)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1 });
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0006F784 File Offset: 0x0006D984
		public static void IsTrue<T1, T2>(bool condition, string format, T1 t1, T2 t2)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2 });
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0006F7B4 File Offset: 0x0006D9B4
		public static void IsTrue<T1, T2, T3>(bool condition, string format, T1 t1, T2 t2, T3 t3)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2, t3 });
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0006F7F0 File Offset: 0x0006D9F0
		public static void IsTrue<T1, T2, T3, T4>(bool condition, string format, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2, t3, t4 });
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0006F834 File Offset: 0x0006DA34
		public static void IsTrue<T1, T2, T3, T4, T5>(bool condition, string format, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2, t3, t4, t5 });
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0006F884 File Offset: 0x0006DA84
		public static void IsTrue<T1, T2, T3, T4, T5, T6>(bool condition, string format, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2, t3, t4, t5, t6 });
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0006F8DC File Offset: 0x0006DADC
		public static void IsTrue<T1, T2, T3, T4, T5, T6, T7>(bool condition, string format, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2, t3, t4, t5, t6, t7 });
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0006F940 File Offset: 0x0006DB40
		public static void IsTrue<T1, T2, T3, T4, T5, T6, T7, T8>(bool condition, string format, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(format, new object[] { t1, t2, t3, t4, t5, t6, t7, t8 });
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0006F9AC File Offset: 0x0006DBAC
		public static void IsTrue(bool condition)
		{
			if (!condition)
			{
				ReleaseAssert.Fail(string.Empty, null);
			}
		}

		// Token: 0x04001662 RID: 5730
		private static bool s_throwOnAssert = ConfigFile.Config.GetValue<bool>("ThrowOnAssert", false);
	}
}
