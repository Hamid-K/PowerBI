using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002068 RID: 8296
	[NullableContext(2)]
	internal interface IBinaryTree
	{
		// Token: 0x17002E18 RID: 11800
		// (get) Token: 0x06011403 RID: 70659
		int Height { get; }

		// Token: 0x17002E19 RID: 11801
		// (get) Token: 0x06011404 RID: 70660
		bool IsEmpty { get; }

		// Token: 0x17002E1A RID: 11802
		// (get) Token: 0x06011405 RID: 70661
		int Count { get; }

		// Token: 0x17002E1B RID: 11803
		// (get) Token: 0x06011406 RID: 70662
		IBinaryTree Left { get; }

		// Token: 0x17002E1C RID: 11804
		// (get) Token: 0x06011407 RID: 70663
		IBinaryTree Right { get; }
	}
}
