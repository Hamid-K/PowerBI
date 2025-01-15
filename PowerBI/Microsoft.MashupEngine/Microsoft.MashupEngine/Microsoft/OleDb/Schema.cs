using System;
using System.Collections.Generic;

namespace Microsoft.OleDb
{
	// Token: 0x02001E9A RID: 7834
	public abstract class Schema
	{
		// Token: 0x17002F63 RID: 12131
		// (get) Token: 0x0600C1B2 RID: 49586
		public abstract Guid Guid { get; }

		// Token: 0x17002F64 RID: 12132
		// (get) Token: 0x0600C1B3 RID: 49587
		public abstract IEnumerable<int> ColumnRestrictions { get; }

		// Token: 0x0600C1B4 RID: 49588
		public abstract IRowset CreateRowset(IDictionary<int, object> restrictions, IDictionary<DBPROPID, object> properties);
	}
}
