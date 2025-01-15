using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001641 RID: 5697
	public class ColumnUpdates
	{
		// Token: 0x06008F65 RID: 36709 RVA: 0x001DDA40 File Offset: 0x001DBC40
		public ColumnUpdates(IDictionary<int, FunctionValue> updates)
		{
			this.updates = updates;
		}

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x06008F66 RID: 36710 RVA: 0x001DDA4F File Offset: 0x001DBC4F
		public IDictionary<int, FunctionValue> Updates
		{
			get
			{
				return this.updates;
			}
		}

		// Token: 0x04004D91 RID: 19857
		private readonly IDictionary<int, FunctionValue> updates;
	}
}
