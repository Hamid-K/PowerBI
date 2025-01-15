using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004C8 RID: 1224
	public class CollectionEvent<T> : EventArgs
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x00052254 File Offset: 0x00050454
		public CollectionEvent(CollectionAction action, IReadOnlyList<T> changedItems = null)
		{
			this.Action = action;
			this.ChangedItems = changedItems;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0005226A File Offset: 0x0005046A
		public CollectionAction Action { get; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x00052272 File Offset: 0x00050472
		public IReadOnlyList<T> ChangedItems { get; }
	}
}
