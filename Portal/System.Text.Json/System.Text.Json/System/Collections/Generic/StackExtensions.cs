using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
	// Token: 0x02000027 RID: 39
	internal static class StackExtensions
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000335A File Offset: 0x0000155A
		public static bool TryPeek<T>(this Stack<T> stack, [MaybeNullWhen(false)] out T result)
		{
			if (stack.Count > 0)
			{
				result = stack.Peek();
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000337B File Offset: 0x0000157B
		public static bool TryPop<T>(this Stack<T> stack, [MaybeNullWhen(false)] out T result)
		{
			if (stack.Count > 0)
			{
				result = stack.Pop();
				return true;
			}
			result = default(T);
			return false;
		}
	}
}
