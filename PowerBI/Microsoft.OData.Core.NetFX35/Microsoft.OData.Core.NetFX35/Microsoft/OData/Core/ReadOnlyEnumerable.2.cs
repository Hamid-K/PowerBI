using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B2 RID: 434
	internal sealed class ReadOnlyEnumerable<T> : ReadOnlyEnumerable, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x000378E5 File Offset: 0x00035AE5
		internal ReadOnlyEnumerable()
			: this(new List<T>())
		{
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000378F2 File Offset: 0x00035AF2
		internal ReadOnlyEnumerable(IList<T> sourceList)
			: base(sourceList)
		{
			this.sourceList = sourceList;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00037902 File Offset: 0x00035B02
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceList.GetEnumerator();
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0003790F File Offset: 0x00035B0F
		internal static ReadOnlyEnumerable<T> Empty()
		{
			return ReadOnlyEnumerable<T>.EmptyInstance.Value;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003791B File Offset: 0x00035B1B
		internal void AddToSourceList(T itemToAdd)
		{
			this.sourceList.Add(itemToAdd);
		}

		// Token: 0x0400074E RID: 1870
		private readonly IList<T> sourceList;

		// Token: 0x0400074F RID: 1871
		private static readonly SimpleLazy<ReadOnlyEnumerable<T>> EmptyInstance = new SimpleLazy<ReadOnlyEnumerable<T>>(() => new ReadOnlyEnumerable<T>(new ReadOnlyCollection<T>(new List<T>(0))), true);
	}
}
