using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200011E RID: 286
	internal static class ExceptionUtils
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.ThreadAbortType && type != ExceptionUtils.StackOverflowType && type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002CB13 File Offset: 0x0002AD13
		internal static T CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			T t = value;
			return value;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002CB1D File Offset: 0x0002AD1D
		internal static void CheckArgumentStringNotEmpty(string value, string parameterName)
		{
			if (value != null)
			{
				int length = value.Length;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002CB29 File Offset: 0x0002AD29
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			string.IsNullOrEmpty(value);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002CB32 File Offset: 0x0002AD32
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002CB32 File Offset: 0x0002AD32
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002CB38 File Offset: 0x0002AD38
		internal static void CheckLongPositive(long value, string parameterName)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002CB3F File Offset: 0x0002AD3F
		internal static void CheckArgumentCollectionNotNullOrEmpty<T>(ICollection<T> value, string parameterName)
		{
			if (value != null)
			{
				int count = value.Count;
			}
		}

		// Token: 0x0400065E RID: 1630
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400065F RID: 1631
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x04000660 RID: 1632
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x020001E5 RID: 485
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
