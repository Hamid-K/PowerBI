using System;
using System.Threading;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200001D RID: 29
	internal static class ExceptionUtils
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003B3C File Offset: 0x00001D3C
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.ThreadAbortType && type != ExceptionUtils.StackOverflowType && type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003B6D File Offset: 0x00001D6D
		internal static void CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B7E File Offset: 0x00001D7E
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, Strings.ExceptionUtils_ArgumentStringNullOrEmpty);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B94 File Offset: 0x00001D94
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerNotNegative(value));
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003BAC File Offset: 0x00001DAC
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerPositive(value));
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003BC4 File Offset: 0x00001DC4
		internal static void CheckLongPositive(long value, string parameterName)
		{
			if (value <= 0L)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckLongPositive(value));
			}
		}

		// Token: 0x040000F6 RID: 246
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x040000F7 RID: 247
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x040000F8 RID: 248
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x0200001E RID: 30
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
