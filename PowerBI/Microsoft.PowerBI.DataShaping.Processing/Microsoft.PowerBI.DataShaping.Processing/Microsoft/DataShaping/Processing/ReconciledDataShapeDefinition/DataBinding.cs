using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000024 RID: 36
	internal sealed class DataBinding
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00004F46 File Offset: 0x00003146
		internal DataBinding(string tableId, int tableIndex, IList<Relationship> relationships, bool shouldRestoreContext)
		{
			this._tableId = tableId;
			this._tableIndex = tableIndex;
			this._relationships = relationships;
			this._shouldRestoreContext = shouldRestoreContext;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004F6B File Offset: 0x0000316B
		internal string TableId
		{
			get
			{
				return this._tableId;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004F73 File Offset: 0x00003173
		internal int TableIndex
		{
			get
			{
				return this._tableIndex;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004F7B File Offset: 0x0000317B
		internal IList<Relationship> Relationships
		{
			get
			{
				return this._relationships;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004F83 File Offset: 0x00003183
		public bool ShouldRestoreContext
		{
			get
			{
				return this._shouldRestoreContext;
			}
		}

		// Token: 0x040000AB RID: 171
		private readonly string _tableId;

		// Token: 0x040000AC RID: 172
		private readonly int _tableIndex;

		// Token: 0x040000AD RID: 173
		private readonly IList<Relationship> _relationships;

		// Token: 0x040000AE RID: 174
		private readonly bool _shouldRestoreContext;
	}
}
