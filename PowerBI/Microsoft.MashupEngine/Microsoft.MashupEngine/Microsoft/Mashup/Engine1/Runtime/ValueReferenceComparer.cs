using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016B9 RID: 5817
	internal class ValueReferenceComparer : IComparer<IValueReference>
	{
		// Token: 0x060093F1 RID: 37873 RVA: 0x001E866F File Offset: 0x001E686F
		public ValueReferenceComparer(IComparer<Value> comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x060093F2 RID: 37874 RVA: 0x001E867E File Offset: 0x001E687E
		public int Compare(IValueReference x, IValueReference y)
		{
			return this.comparer.Compare(x.Value, y.Value);
		}

		// Token: 0x04004EE4 RID: 20196
		private IComparer<Value> comparer;
	}
}
