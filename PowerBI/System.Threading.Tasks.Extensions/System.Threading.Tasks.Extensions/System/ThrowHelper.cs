using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal static class ThrowHelper
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.GetArgumentNullException(argument);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(argument);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002068 File Offset: 0x00000268
		private static ArgumentNullException GetArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002075 File Offset: 0x00000275
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002082 File Offset: 0x00000282
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetArgumentName(ExceptionArgument argument)
		{
			return argument.ToString();
		}
	}
}
