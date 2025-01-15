using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Encodings.Web
{
	// Token: 0x0200002E RID: 46
	internal static class ThrowHelper
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00006348 File Offset: 0x00004548
		[DoesNotReturn]
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006355 File Offset: 0x00004555
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006362 File Offset: 0x00004562
		private static string GetArgumentName(ExceptionArgument argument)
		{
			return argument.ToString();
		}
	}
}
