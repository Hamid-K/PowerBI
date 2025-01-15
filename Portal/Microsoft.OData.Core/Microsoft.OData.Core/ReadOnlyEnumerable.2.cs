using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.OData
{
	// Token: 0x020000C8 RID: 200
	internal sealed class ReadOnlyEnumerable<T> : ReadOnlyEnumerable, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x00016911 File Offset: 0x00014B11
		internal ReadOnlyEnumerable()
			: this(new List<T>())
		{
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001691E File Offset: 0x00014B1E
		internal ReadOnlyEnumerable(IList<T> sourceList)
			: base(sourceList)
		{
			this.sourceList = sourceList;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001692E File Offset: 0x00014B2E
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceList.GetEnumerator();
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001693B File Offset: 0x00014B3B
		internal static ReadOnlyEnumerable<T> Empty()
		{
			return ReadOnlyEnumerable<T>.EmptyInstance.Value;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00016947 File Offset: 0x00014B47
		internal void AddToSourceList(T itemToAdd)
		{
			this.sourceList.Add(itemToAdd);
		}

		// Token: 0x04000346 RID: 838
		private readonly IList<T> sourceList;

		// Token: 0x04000347 RID: 839
		private static readonly SimpleLazy<ReadOnlyEnumerable<T>> EmptyInstance = new SimpleLazy<ReadOnlyEnumerable<T>>(() => new ReadOnlyEnumerable<T>(new ReadOnlyCollection<T>(new List<T>(0))), true);
	}
}
