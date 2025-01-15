using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions.Internal
{
	// Token: 0x02000056 RID: 86
	internal static class ReadOnlyCollectionExtensions
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000908C File Offset: 0x0000728C
		internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null)
			{
				return ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>.Empty;
			}
			ReadOnlyCollection<T> readOnlyCollection = sequence as ReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			return new ReadOnlyCollection<T>(sequence.ToArray<T>());
		}

		// Token: 0x020006FF RID: 1791
		private static class DefaultReadOnlyCollection<T>
		{
			// Token: 0x17001009 RID: 4105
			// (get) Token: 0x06005475 RID: 21621 RVA: 0x0012FC14 File Offset: 0x0012DE14
			internal static ReadOnlyCollection<T> Empty
			{
				get
				{
					if (ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>._defaultCollection == null)
					{
						ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>._defaultCollection = new ReadOnlyCollection<T>(new T[0]);
					}
					return ReadOnlyCollectionExtensions.DefaultReadOnlyCollection<T>._defaultCollection;
				}
			}

			// Token: 0x04001E34 RID: 7732
			private static ReadOnlyCollection<T> _defaultCollection;
		}
	}
}
