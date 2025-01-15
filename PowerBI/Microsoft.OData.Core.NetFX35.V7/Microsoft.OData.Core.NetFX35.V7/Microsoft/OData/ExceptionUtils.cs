using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData
{
	// Token: 0x0200000B RID: 11
	internal static class ExceptionUtils
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002BF4 File Offset: 0x00000DF4
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.ThreadAbortType && type != ExceptionUtils.StackOverflowType && type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C25 File Offset: 0x00000E25
		internal static T CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			return value;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C37 File Offset: 0x00000E37
		internal static void CheckArgumentStringNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C50 File Offset: 0x00000E50
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, Strings.ExceptionUtils_ArgumentStringNullOrEmpty);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C66 File Offset: 0x00000E66
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerNotNegative(value));
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C7E File Offset: 0x00000E7E
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerPositive(value));
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C96 File Offset: 0x00000E96
		internal static void CheckLongPositive(long value, string parameterName)
		{
			if (value <= 0L)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckLongPositive(value));
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CAF File Offset: 0x00000EAF
		internal static void CheckArgumentCollectionNotNullOrEmpty<T>(ICollection<T> value, string parameterName)
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			if (value.Count == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x04000021 RID: 33
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x04000022 RID: 34
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x04000023 RID: 35
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x02000237 RID: 567
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
