using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200004A RID: 74
	[NullableContext(2)]
	[Nullable(0)]
	internal static class Requires
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0000999B File Offset: 0x00007B9B
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static void NotNull<T>([ValidatedNotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000099AB File Offset: 0x00007BAB
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static T NotNullPassthrough<T>([ValidatedNotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			Requires.NotNull<T>(value, parameterName);
			return value;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000099B5 File Offset: 0x00007BB5
		[DebuggerStepThrough]
		public static void NotNullAllowStructs<T>([Nullable(1)] [ValidatedNotNull] T value, string parameterName)
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000099C5 File Offset: 0x00007BC5
		[DebuggerStepThrough]
		private static void FailArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000099CD File Offset: 0x00007BCD
		[DebuggerStepThrough]
		public static void Range(bool condition, string parameterName, string message = null)
		{
			if (!condition)
			{
				Requires.FailRange(parameterName, message);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000099D9 File Offset: 0x00007BD9
		[DebuggerStepThrough]
		public static void FailRange(string parameterName, string message = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000099F1 File Offset: 0x00007BF1
		[DebuggerStepThrough]
		public static void Argument(bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000099FE File Offset: 0x00007BFE
		[DebuggerStepThrough]
		public static void Argument(bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00009A09 File Offset: 0x00007C09
		[NullableContext(1)]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void FailObjectDisposed<[Nullable(2)] TDisposed>(TDisposed disposed)
		{
			throw new ObjectDisposedException(disposed.GetType().FullName);
		}
	}
}
