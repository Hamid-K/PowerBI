using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200040F RID: 1039
	internal interface IObjectViewData<T>
	{
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600311F RID: 12575
		IList<T> List { get; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003120 RID: 12576
		bool AllowNew { get; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003121 RID: 12577
		bool AllowEdit { get; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003122 RID: 12578
		bool AllowRemove { get; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003123 RID: 12579
		bool FiresEventOnAdd { get; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003124 RID: 12580
		bool FiresEventOnRemove { get; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003125 RID: 12581
		bool FiresEventOnClear { get; }

		// Token: 0x06003126 RID: 12582
		void EnsureCanAddNew();

		// Token: 0x06003127 RID: 12583
		int Add(T item, bool isAddNew);

		// Token: 0x06003128 RID: 12584
		void CommitItemAt(int index);

		// Token: 0x06003129 RID: 12585
		void Clear();

		// Token: 0x0600312A RID: 12586
		bool Remove(T item, bool isCancelNew);

		// Token: 0x0600312B RID: 12587
		ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener);
	}
}
