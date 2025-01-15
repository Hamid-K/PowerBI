using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200182D RID: 6189
	public struct SortOrder
	{
		// Token: 0x06009CE5 RID: 40165 RVA: 0x00206D9E File Offset: 0x00204F9E
		public SortOrder(FunctionValue selector, FunctionValue comparer, bool ascending)
		{
			this.selector = selector;
			this.comparer = comparer;
			this.ascending = ascending;
		}

		// Token: 0x17002873 RID: 10355
		// (get) Token: 0x06009CE6 RID: 40166 RVA: 0x00206DB5 File Offset: 0x00204FB5
		public FunctionValue Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x17002874 RID: 10356
		// (get) Token: 0x06009CE7 RID: 40167 RVA: 0x00206DBD File Offset: 0x00204FBD
		public FunctionValue Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x17002875 RID: 10357
		// (get) Token: 0x06009CE8 RID: 40168 RVA: 0x00206DC5 File Offset: 0x00204FC5
		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x04005282 RID: 21122
		private FunctionValue selector;

		// Token: 0x04005283 RID: 21123
		private FunctionValue comparer;

		// Token: 0x04005284 RID: 21124
		private bool ascending;
	}
}
