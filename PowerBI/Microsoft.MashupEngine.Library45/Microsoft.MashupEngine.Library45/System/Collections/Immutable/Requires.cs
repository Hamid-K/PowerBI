using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020CE RID: 8398
	[NullableContext(2)]
	[Nullable(0)]
	internal static class Requires
	{
		// Token: 0x060119FF RID: 72191 RVA: 0x003C40C3 File Offset: 0x003C22C3
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static void NotNull<T>([ValidatedNotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06011A00 RID: 72192 RVA: 0x003C40D3 File Offset: 0x003C22D3
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static T NotNullPassthrough<T>([ValidatedNotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			Requires.NotNull<T>(value, parameterName);
			return value;
		}

		// Token: 0x06011A01 RID: 72193 RVA: 0x003C40C3 File Offset: 0x003C22C3
		[DebuggerStepThrough]
		public static void NotNullAllowStructs<T>([Nullable(1)] [ValidatedNotNull] T value, string parameterName)
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06011A02 RID: 72194 RVA: 0x003C40DD File Offset: 0x003C22DD
		[DebuggerStepThrough]
		private static void FailArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x06011A03 RID: 72195 RVA: 0x003C40E5 File Offset: 0x003C22E5
		[DebuggerStepThrough]
		public static void Range(bool condition, string parameterName, string message = null)
		{
			if (!condition)
			{
				Requires.FailRange(parameterName, message);
			}
		}

		// Token: 0x06011A04 RID: 72196 RVA: 0x003C40F1 File Offset: 0x003C22F1
		[DebuggerStepThrough]
		public static void FailRange(string parameterName, string message = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x06011A05 RID: 72197 RVA: 0x003C4109 File Offset: 0x003C2309
		[DebuggerStepThrough]
		public static void Argument(bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x06011A06 RID: 72198 RVA: 0x003C4116 File Offset: 0x003C2316
		[DebuggerStepThrough]
		public static void Argument(bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x06011A07 RID: 72199 RVA: 0x003C4121 File Offset: 0x003C2321
		[NullableContext(1)]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void FailObjectDisposed<[Nullable(2)] TDisposed>(TDisposed disposed)
		{
			throw new ObjectDisposedException(disposed.GetType().FullName);
		}
	}
}
