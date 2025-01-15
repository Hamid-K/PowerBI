using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.DI.RoaringBitmap
{
	// Token: 0x02000004 RID: 4
	public interface IRoaringBitmap<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		ulong Cardinality { get; }

		// Token: 0x06000004 RID: 4
		void Add(IEnumerable<T> indexList);

		// Token: 0x06000005 RID: 5
		void Remove(IEnumerable<T> index);

		// Token: 0x06000006 RID: 6
		IEnumerable<T> Values();

		// Token: 0x06000007 RID: 7
		bool Contains(T index);

		// Token: 0x06000008 RID: 8
		void Serialize(Stream stream);

		// Token: 0x06000009 RID: 9
		void Deserialize(Stream stream);
	}
}
