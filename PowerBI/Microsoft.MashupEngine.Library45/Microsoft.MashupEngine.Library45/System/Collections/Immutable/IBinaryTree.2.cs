using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002069 RID: 8297
	[NullableContext(1)]
	internal interface IBinaryTree<[Nullable(2)] out T> : IBinaryTree
	{
		// Token: 0x17002E1D RID: 11805
		// (get) Token: 0x06011408 RID: 70664
		T Value { get; }

		// Token: 0x17002E1E RID: 11806
		// (get) Token: 0x06011409 RID: 70665
		[Nullable(new byte[] { 2, 1 })]
		IBinaryTree<T> Left
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}

		// Token: 0x17002E1F RID: 11807
		// (get) Token: 0x0601140A RID: 70666
		[Nullable(new byte[] { 2, 1 })]
		IBinaryTree<T> Right
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}
	}
}
