using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Data.OData
{
	// Token: 0x02000246 RID: 582
	internal sealed class ReadOnlyEnumerable<T> : ReadOnlyEnumerable, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00043569 File Offset: 0x00041769
		internal ReadOnlyEnumerable()
			: this(new List<T>())
		{
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00043576 File Offset: 0x00041776
		internal ReadOnlyEnumerable(IList<T> sourceList)
			: base(sourceList)
		{
			this.sourceList = sourceList;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00043586 File Offset: 0x00041786
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceList.GetEnumerator();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00043593 File Offset: 0x00041793
		internal static ReadOnlyEnumerable<T> Empty()
		{
			return ReadOnlyEnumerable<T>.EmptyInstance.Value;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004359F File Offset: 0x0004179F
		internal void AddToSourceList(T itemToAdd)
		{
			this.sourceList.Add(itemToAdd);
		}

		// Token: 0x040006B2 RID: 1714
		private readonly IList<T> sourceList;

		// Token: 0x040006B3 RID: 1715
		private static readonly SimpleLazy<ReadOnlyEnumerable<T>> EmptyInstance = new SimpleLazy<ReadOnlyEnumerable<T>>(() => new ReadOnlyEnumerable<T>(new ReadOnlyCollection<T>(new List<T>(0))), true);
	}
}
