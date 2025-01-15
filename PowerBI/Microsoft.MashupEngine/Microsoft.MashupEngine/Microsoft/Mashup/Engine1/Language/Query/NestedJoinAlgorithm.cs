using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017F3 RID: 6131
	public abstract class NestedJoinAlgorithm
	{
		// Token: 0x06009AD5 RID: 39637
		public abstract IEnumerable<IValueReference> NestedJoin(NestedJoinParameters parameters);

		// Token: 0x040051EC RID: 20972
		public static readonly NestedJoinAlgorithm RightIndex = new IndexNestedJoinAlgorithm();

		// Token: 0x040051ED RID: 20973
		public static readonly NestedJoinAlgorithm GroupJoin = new GroupJoinNestedJoinAlgorithm();
	}
}
