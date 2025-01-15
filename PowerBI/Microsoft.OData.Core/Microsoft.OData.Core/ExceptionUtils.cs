using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x02000032 RID: 50
	internal static class ExceptionUtils
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00005108 File Offset: 0x00003308
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005127 File Offset: 0x00003327
		internal static T CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			return value;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005139 File Offset: 0x00003339
		internal static void CheckArgumentStringNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005152 File Offset: 0x00003352
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, Strings.ExceptionUtils_ArgumentStringNullOrEmpty);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005168 File Offset: 0x00003368
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerNotNegative(value));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005180 File Offset: 0x00003380
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerPositive(value));
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005198 File Offset: 0x00003398
		internal static void CheckLongPositive(long value, string parameterName)
		{
			if (value <= 0L)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckLongPositive(value));
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000051B1 File Offset: 0x000033B1
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

		// Token: 0x04000093 RID: 147
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x02000285 RID: 645
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
