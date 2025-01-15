using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001836 RID: 6198
	public struct Distinct
	{
		// Token: 0x06009D0F RID: 40207 RVA: 0x002074AD File Offset: 0x002056AD
		public Distinct(FunctionValue selector, IEqualityComparer<Value> comparer)
		{
			this.selector = selector;
			this.comparer = comparer;
		}

		// Token: 0x17002880 RID: 10368
		// (get) Token: 0x06009D10 RID: 40208 RVA: 0x002074BD File Offset: 0x002056BD
		public FunctionValue Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x17002881 RID: 10369
		// (get) Token: 0x06009D11 RID: 40209 RVA: 0x002074C5 File Offset: 0x002056C5
		public IEqualityComparer<Value> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x04005298 RID: 21144
		private FunctionValue selector;

		// Token: 0x04005299 RID: 21145
		private IEqualityComparer<Value> comparer;
	}
}
