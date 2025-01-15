using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001B RID: 27
	[NullableContext(2)]
	internal interface IBinaryTree
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106
		int Height { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107
		bool IsEmpty { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108
		int Count { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109
		IBinaryTree Left { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110
		IBinaryTree Right { get; }
	}
}
