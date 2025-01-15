using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D0 RID: 464
	internal static class Ensure
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x00029324 File Offset: 0x00027524
		[ContractAnnotation("=> halt")]
		public static void Fail(string message)
		{
			string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
			string text = string.Format(CultureInfo.InvariantCulture, "{0} [{1}]", new object[] { message, firstForeignMethodOnStack });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
			{
				new StackTrace(0, true)
			});
			throw new AssertionFailedException(text);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00029398 File Offset: 0x00027598
		[ContractAnnotation("condition:false=> halt")]
		public static void IsTrue(bool condition, string message)
		{
			if (!condition)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Condition '{0}' should not be false in {1}", new object[] { message, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new InvalidOperationException(text);
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00029410 File Offset: 0x00027610
		[ContractAnnotation("value:null=> halt")]
		public static void IsNotNull<T>([NoEnumeration] T value, string who)
		{
			if (value == null)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "'{0}' may not be null in {1}", new object[] { who, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentNullException(who, text);
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002948C File Offset: 0x0002768C
		[ContractAnnotation("value:notnull=> halt")]
		public static void IsNull<T>([NoEnumeration] T value, string who)
		{
			if (value != null)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "'{0}' should be null, but it is currently '{1} in {2}", new object[] { who, value, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(who, value, text);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00029518 File Offset: 0x00027718
		[ContractAnnotation("value:null=> halt")]
		public static void ArgNotNull<T>([NoEnumeration] T value, [InvokerParameterName] string name)
		{
			if (value == null)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Argument '{0}' may not be null in {1}", new object[] { name, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentNullException(name, text);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00029594 File Offset: 0x00027794
		[ContractAnnotation("value:null=> halt")]
		public static void ArgNotNullOrEmpty(string value, [InvokerParameterName] string name)
		{
			if (string.IsNullOrEmpty(value))
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "'{0}' may not be null or empty in {1}", new object[] { name, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentNullException(name, text);
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00029610 File Offset: 0x00027810
		[ContractAnnotation("condition:false=> halt")]
		public static void ArgSatisfiesCondition(string value, [InvokerParameterName] string name, bool condition)
		{
			if (!condition)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Argument '{0}' (value: {1}) does not meet condition in {2}", new object[] { name, value, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(name, value, text);
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002968C File Offset: 0x0002788C
		[ContractAnnotation("condition:false=> halt")]
		public static void ArgSatisfiesCondition([InvokerParameterName] string name, bool condition, string msg)
		{
			if (!condition)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Ensure.ArgSatisfiesCondition failed on {0}: {1}", new object[] { name, msg });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentException(name, msg);
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000296E4 File Offset: 0x000278E4
		public static void ArgIsNotNegative(long value, [InvokerParameterName] string name, int stackFramesToSkip)
		{
			if (value < 0L)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Constraint: '{0}' >= 0 has been violated in {1}", new object[] { name, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(name, value, text);
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00029764 File Offset: 0x00027964
		public static void ArgIsNotNegative(TimeSpan value, [InvokerParameterName] string name)
		{
			if (value < TimeSpan.Zero && value != TimeConstants.InfiniteTimeSpan)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Constraint: '{0}' >= TimeSpan.Zero || '{0}' = TimeSpan.FromMilliseconds(-1) has been violated in {1}", new object[] { name, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(name, value, text);
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000297F8 File Offset: 0x000279F8
		public static void ArgIsPositive(long value, [InvokerParameterName] string name, int stackFramesToSkip)
		{
			if (value <= 0L)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Constraint: '{0}' > 0 has been violated in {1}", new object[] { name, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(name, value, text);
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00029878 File Offset: 0x00027A78
		public static void ArgIsPositive(TimeSpan value, [InvokerParameterName] string name)
		{
			if (value <= TimeSpan.Zero)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "Constraint: '{0}' <= TimeSpan.Zero || '{0}' = TimeSpan.FromMilliseconds(-1) has been violated in {1}", new object[] { name, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(name, value, text);
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00029900 File Offset: 0x00027B00
		public static void ArgIsInRange<T>(T value, T begin, T end, [InvokerParameterName] string name, int stackFramesToSkip) where T : IComparable<T>
		{
			if (value.CompareTo(begin) < 0 || value.CompareTo(end) > 0)
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack();
				string text = string.Format(CultureInfo.InvariantCulture, "'{0}' == '{1}' which is not in the range '{2}'..'{3}' in {4}", new object[] { name, value, begin, end, firstForeignMethodOnStack });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new ArgumentOutOfRangeException(name, value, text);
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000299BC File Offset: 0x00027BBC
		public static void SlowEnumIsDefined<T>(T value, string name, int stackFramesToSkip) where T : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enum.IsDefined(typeof(T), value))
			{
				string firstForeignMethodOnStack = Ensure.GetFirstForeignMethodOnStack(stackFramesToSkip + 1);
				string text = string.Format(CultureInfo.InvariantCulture, "Argument '{0}' (value: {1}) is not an enum of type '{2}' in method {3}", new object[]
				{
					name,
					value,
					typeof(T).FullName,
					firstForeignMethodOnStack
				});
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[]
				{
					new StackTrace(0, true)
				});
				throw new InvalidEnumArgumentException(name, Convert.ToInt32(value, CultureInfo.InvariantCulture), typeof(T));
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00029A80 File Offset: 0x00027C80
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetFirstForeignMethodOnStack(int stackFramesToSkip)
		{
			string text = null;
			for (int i = stackFramesToSkip + 1; i < stackFramesToSkip + 5; i++)
			{
				StackFrame stackFrame = new StackFrame(i, true);
				MethodBase method = stackFrame.GetMethod();
				string fullyQualifiedMemberName = ExtendedReflection.GetFullyQualifiedMemberName(method);
				text = "method '{0}' ({1})".FormatWithInvariantCulture(new object[] { fullyQualifiedMemberName, stackFrame });
				if (method.DeclaringType != null && method.DeclaringType != typeof(Ensure) && method.DeclaringType != typeof(ExtendedDiagnostics) && method.DeclaringType != typeof(ExtendedReflection))
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00029B2A File Offset: 0x00027D2A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetFirstForeignMethodOnStack()
		{
			return Ensure.GetFirstForeignMethodOnStack(2);
		}
	}
}
