using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C6 RID: 198
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class Schema
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036E RID: 878
		public abstract Guid Guid { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600036F RID: 879
		public abstract IEnumerable<int> ColumnRestrictions { get; }

		// Token: 0x06000370 RID: 880
		public abstract IRowset CreateRowset(IDictionary<int, object> restrictions, DictionaryDBProperties properties);
	}
}
