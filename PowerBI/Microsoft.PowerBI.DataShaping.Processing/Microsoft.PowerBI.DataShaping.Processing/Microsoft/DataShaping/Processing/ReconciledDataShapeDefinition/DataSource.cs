using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000037 RID: 55
	internal sealed class DataSource
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00005A1C File Offset: 0x00003C1C
		internal DataSource(string id, string dataSourceName, Collation collation, IDataShapingDataSourceInfo dataSourceInfo)
		{
			this._id = id;
			this._dataSourceName = dataSourceName;
			this._collation = collation;
			this._dataSourceInfo = dataSourceInfo;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005A41 File Offset: 0x00003C41
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005A49 File Offset: 0x00003C49
		internal string DataSourceName
		{
			get
			{
				return this._dataSourceName;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005A51 File Offset: 0x00003C51
		internal Collation Collation
		{
			get
			{
				return this._collation;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005A59 File Offset: 0x00003C59
		internal IDataShapingDataSourceInfo DataSourceInfo
		{
			get
			{
				return this._dataSourceInfo;
			}
		}

		// Token: 0x040000FE RID: 254
		private readonly string _id;

		// Token: 0x040000FF RID: 255
		private readonly string _dataSourceName;

		// Token: 0x04000100 RID: 256
		private readonly Collation _collation;

		// Token: 0x04000101 RID: 257
		private readonly IDataShapingDataSourceInfo _dataSourceInfo;
	}
}
