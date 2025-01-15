using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.OData
{
	// Token: 0x020000AA RID: 170
	internal sealed class ReadOnlyEnumerable<T> : ReadOnlyEnumerable, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x00012051 File Offset: 0x00010251
		internal ReadOnlyEnumerable()
			: this(new List<T>())
		{
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001205E File Offset: 0x0001025E
		internal ReadOnlyEnumerable(IList<T> sourceList)
			: base(sourceList)
		{
			this.sourceList = sourceList;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001206E File Offset: 0x0001026E
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceList.GetEnumerator();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001207B File Offset: 0x0001027B
		internal static ReadOnlyEnumerable<T> Empty()
		{
			return ReadOnlyEnumerable<T>.EmptyInstance.Value;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00012087 File Offset: 0x00010287
		internal void AddToSourceList(T itemToAdd)
		{
			this.sourceList.Add(itemToAdd);
		}

		// Token: 0x040002E6 RID: 742
		private readonly IList<T> sourceList;

		// Token: 0x040002E7 RID: 743
		private static readonly SimpleLazy<ReadOnlyEnumerable<T>> EmptyInstance = new SimpleLazy<ReadOnlyEnumerable<T>>(() => new ReadOnlyEnumerable<T>(new ReadOnlyCollection<T>(new List<T>(0))), true);
	}
}
