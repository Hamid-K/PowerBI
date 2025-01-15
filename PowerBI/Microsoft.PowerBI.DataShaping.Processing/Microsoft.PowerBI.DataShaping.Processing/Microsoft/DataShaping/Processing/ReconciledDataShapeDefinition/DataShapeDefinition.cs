using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000035 RID: 53
	internal sealed class DataShapeDefinition
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00005791 File Offset: 0x00003991
		internal DataShapeDefinition(DataSource dataSource, IList<DataSet> dataSets, IList<DataTransform> dataTransforms, DataShape dataShape, IList<ResultTableLookupInfo> resultTableInfos, ResultTableMetadata resultTableMetadata, ResultEncodingHints encodingHints)
		{
			this._dataSource = dataSource;
			this._dataSets = dataSets;
			this._dataTransforms = dataTransforms;
			this._dataShape = dataShape;
			this._resultTableInfos = resultTableInfos;
			this._resultTableMetadata = resultTableMetadata;
			this._encodingHints = encodingHints;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000057CE File Offset: 0x000039CE
		internal DataSource DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000057D6 File Offset: 0x000039D6
		internal IList<DataSet> DataSets
		{
			get
			{
				return this._dataSets;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000057DE File Offset: 0x000039DE
		internal IList<DataTransform> DataTransforms
		{
			get
			{
				return this._dataTransforms;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000057E6 File Offset: 0x000039E6
		internal DataShape DataShape
		{
			get
			{
				return this._dataShape;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000057EE File Offset: 0x000039EE
		internal IList<ResultTableLookupInfo> ResultTableInfos
		{
			get
			{
				return this._resultTableInfos;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000057F6 File Offset: 0x000039F6
		internal ResultTableMetadata ResultTableMetadata
		{
			get
			{
				return this._resultTableMetadata;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000057FE File Offset: 0x000039FE
		internal ResultEncodingHints EncodingHints
		{
			get
			{
				return this._encodingHints;
			}
		}

		// Token: 0x040000F7 RID: 247
		private readonly DataSource _dataSource;

		// Token: 0x040000F8 RID: 248
		private readonly IList<DataSet> _dataSets;

		// Token: 0x040000F9 RID: 249
		private readonly IList<DataTransform> _dataTransforms;

		// Token: 0x040000FA RID: 250
		private readonly DataShape _dataShape;

		// Token: 0x040000FB RID: 251
		private readonly IList<ResultTableLookupInfo> _resultTableInfos;

		// Token: 0x040000FC RID: 252
		private readonly ResultTableMetadata _resultTableMetadata;

		// Token: 0x040000FD RID: 253
		private readonly ResultEncodingHints _encodingHints;
	}
}
