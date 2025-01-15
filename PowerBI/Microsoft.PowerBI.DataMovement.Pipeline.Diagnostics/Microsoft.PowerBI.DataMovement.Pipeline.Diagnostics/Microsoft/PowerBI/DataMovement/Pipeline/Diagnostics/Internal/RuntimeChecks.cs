using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000DE RID: 222
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RuntimeChecks
	{
		// Token: 0x060010E3 RID: 4323 RVA: 0x00046578 File Offset: 0x00044778
		internal static void Fail(string message)
		{
			throw new RuntimeCheckFailedException(TraceUtils.FormatWithInvariantCulture("Internal error: {0}", message), Array.Empty<PowerBIErrorDetail>());
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0004658F File Offset: 0x0004478F
		internal static void CheckValue(object o, string parameter)
		{
			if (o == null)
			{
				RuntimeChecks.Fail(parameter);
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004659A File Offset: 0x0004479A
		internal static void CheckNonEmptyValue(string s, string parameter)
		{
			if (string.IsNullOrEmpty(s))
			{
				RuntimeChecks.Fail(parameter);
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000465AA File Offset: 0x000447AA
		internal static void CheckNonEmpty(Array array, string parameter)
		{
			if (array == null || array.Length == 0)
			{
				RuntimeChecks.Fail(parameter);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000465BD File Offset: 0x000447BD
		internal static void CheckNonEmpty<[Nullable(2)] T>(T[] array, string parameter)
		{
			if (array == null || array.Length == 0)
			{
				RuntimeChecks.Fail(parameter);
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000465CC File Offset: 0x000447CC
		internal static void CheckNonNegative(int value, string parameter)
		{
			if (value < 0)
			{
				RuntimeChecks.Fail(parameter);
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000465D8 File Offset: 0x000447D8
		internal static void Check(bool condition, string message)
		{
			if (!condition)
			{
				RuntimeChecks.Fail(message);
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000465E3 File Offset: 0x000447E3
		internal static void CheckNotDisposed(bool disposed, object o)
		{
			if (disposed)
			{
				throw new ObjectDisposedException((o == null) ? null : o.GetType().Name);
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000465FF File Offset: 0x000447FF
		internal static Exception UnreachableCodepath([CallerMemberName] string memberName = null, [CallerFilePath] string sourceFilePath = null, [CallerLineNumber] int sourceLineNumber = 0, string message = "Unreachable code path reached")
		{
			return new NotImplementedException(TraceUtils.FormatWithInvariantCulture("[{0};{1};{2}] {3}", memberName, sourceFilePath, sourceLineNumber, message));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00046619 File Offset: 0x00044819
		internal static Exception UnsupportedCodepath([CallerMemberName] string memberName = null, [CallerFilePath] string sourceFilePath = null, [CallerLineNumber] int sourceLineNumber = 0, string message = "Unsupported code path reached")
		{
			return new NotSupportedException(TraceUtils.FormatWithInvariantCulture("[{0};{1};{2}] {3}", memberName, sourceFilePath, sourceLineNumber, message));
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00046633 File Offset: 0x00044833
		internal static Exception ArgumentOutOfRange(string paramName, object actualValue, string message)
		{
			return new ArgumentOutOfRangeException(paramName, actualValue, message);
		}
	}
}
