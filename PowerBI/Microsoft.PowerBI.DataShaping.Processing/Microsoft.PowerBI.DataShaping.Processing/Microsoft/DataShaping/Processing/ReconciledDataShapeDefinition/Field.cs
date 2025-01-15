using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000033 RID: 51
	internal sealed class Field
	{
		// Token: 0x0600016F RID: 367 RVA: 0x000055A9 File Offset: 0x000037A9
		internal Field(string id, string dataField, string targetRole, bool isRowIndex, SortInformation sortInformation)
		{
			this._id = id;
			this._dataField = dataField;
			this._targetRole = targetRole;
			this._isRowIndex = isRowIndex;
			this._sortInformation = sortInformation;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000055D6 File Offset: 0x000037D6
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000055DE File Offset: 0x000037DE
		internal string DataField
		{
			get
			{
				return this._dataField;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000055E6 File Offset: 0x000037E6
		public string TargetRole
		{
			get
			{
				return this._targetRole;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000055EE File Offset: 0x000037EE
		public bool IsRowIndex
		{
			get
			{
				return this._isRowIndex;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000055F6 File Offset: 0x000037F6
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000055FE File Offset: 0x000037FE
		internal SortInformation SortInformation
		{
			get
			{
				return this._sortInformation;
			}
			set
			{
				this._sortInformation = value;
			}
		}

		// Token: 0x040000E1 RID: 225
		private readonly string _id;

		// Token: 0x040000E2 RID: 226
		private readonly string _dataField;

		// Token: 0x040000E3 RID: 227
		private readonly string _targetRole;

		// Token: 0x040000E4 RID: 228
		private readonly bool _isRowIndex;

		// Token: 0x040000E5 RID: 229
		private SortInformation _sortInformation;
	}
}
