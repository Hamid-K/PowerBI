using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Core
{
	// Token: 0x0200008F RID: 143
	internal static class ExceptionUtils
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00014B28 File Offset: 0x00012D28
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.ThreadAbortType && type != ExceptionUtils.StackOverflowType && type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00014B59 File Offset: 0x00012D59
		internal static void CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00014B6A File Offset: 0x00012D6A
		internal static void CheckArgumentStringNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00014B83 File Offset: 0x00012D83
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, Strings.ExceptionUtils_ArgumentStringNullOrEmpty);
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00014B99 File Offset: 0x00012D99
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerNotNegative(value));
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00014BB1 File Offset: 0x00012DB1
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerPositive(value));
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00014BC9 File Offset: 0x00012DC9
		internal static void CheckLongPositive(long value, string parameterName)
		{
			if (value <= 0L)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckLongPositive(value));
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014BE2 File Offset: 0x00012DE2
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

		// Token: 0x04000269 RID: 617
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400026A RID: 618
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400026B RID: 619
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x02000090 RID: 144
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
