using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	internal interface IBinaryTree<[Nullable(2)] out T> : IBinaryTree
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111
		T Value { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112
		[Nullable(new byte[] { 2, 1 })]
		IBinaryTree<T> Left
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113
		[Nullable(new byte[] { 2, 1 })]
		IBinaryTree<T> Right
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}
	}
}
