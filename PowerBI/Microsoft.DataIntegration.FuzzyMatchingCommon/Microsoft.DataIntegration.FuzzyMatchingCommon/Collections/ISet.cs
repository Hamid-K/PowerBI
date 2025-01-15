using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200009F RID: 159
	internal interface ISet<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060006C0 RID: 1728
		ISet<T> Union(ISet<T> s);

		// Token: 0x060006C1 RID: 1729
		ISet<T> Intersection(ISet<T> s);

		// Token: 0x060006C2 RID: 1730
		ISet<T> Difference(ISet<T> s);

		// Token: 0x060006C3 RID: 1731
		bool Contains(T t);

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006C4 RID: 1732
		int Count { get; }
	}
}
