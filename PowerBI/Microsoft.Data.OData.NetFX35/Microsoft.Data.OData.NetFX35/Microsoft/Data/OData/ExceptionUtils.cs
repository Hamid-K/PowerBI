using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.OData
{
	// Token: 0x020002A4 RID: 676
	internal static class ExceptionUtils
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x0004E71C File Offset: 0x0004C91C
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.ThreadAbortType && type != ExceptionUtils.StackOverflowType && type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0004E74D File Offset: 0x0004C94D
		internal static void CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0004E75E File Offset: 0x0004C95E
		internal static void CheckArgumentStringNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0004E777 File Offset: 0x0004C977
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, Strings.ExceptionUtils_ArgumentStringNullOrEmpty);
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0004E78D File Offset: 0x0004C98D
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerNotNegative(value));
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0004E7A5 File Offset: 0x0004C9A5
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerPositive(value));
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0004E7BD File Offset: 0x0004C9BD
		internal static void CheckLongPositive(long value, string parameterName)
		{
			if (value <= 0L)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckLongPositive(value));
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0004E7D6 File Offset: 0x0004C9D6
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

		// Token: 0x0400096B RID: 2411
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400096C RID: 2412
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400096D RID: 2413
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x020002A5 RID: 677
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
