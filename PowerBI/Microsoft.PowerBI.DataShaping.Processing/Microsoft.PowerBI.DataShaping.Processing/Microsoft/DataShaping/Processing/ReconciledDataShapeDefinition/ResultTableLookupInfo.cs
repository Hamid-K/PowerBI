using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200004D RID: 77
	internal sealed class ResultTableLookupInfo
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00006291 File Offset: 0x00004491
		private ResultTableLookupInfo(int dataSetIndex, int localTableIndex, int dataTransformIndex)
		{
			this._dataSetIndex = dataSetIndex;
			this._localTableIndex = localTableIndex;
			this._dataTransformIndex = dataTransformIndex;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000062AE File Offset: 0x000044AE
		internal static ResultTableLookupInfo ForDataSet(int dataSetIndex, int localTableIndex)
		{
			return new ResultTableLookupInfo(dataSetIndex, localTableIndex, -1);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000062B8 File Offset: 0x000044B8
		internal static ResultTableLookupInfo ForDataTransform(int dataTransformIndex)
		{
			return new ResultTableLookupInfo(-1, 0, dataTransformIndex);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000062C2 File Offset: 0x000044C2
		public int DataSetIndex
		{
			get
			{
				return this._dataSetIndex;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000062CA File Offset: 0x000044CA
		public int LocalTableIndex
		{
			get
			{
				return this._localTableIndex;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000062D2 File Offset: 0x000044D2
		public int DataTransformIndex
		{
			get
			{
				return this._dataTransformIndex;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000062DA File Offset: 0x000044DA
		public bool IsDataSetTable
		{
			get
			{
				return this._dataSetIndex != -1;
			}
		}

		// Token: 0x04000138 RID: 312
		private const int MissingIndex = -1;

		// Token: 0x04000139 RID: 313
		private readonly int _dataSetIndex;

		// Token: 0x0400013A RID: 314
		private readonly int _localTableIndex;

		// Token: 0x0400013B RID: 315
		private readonly int _dataTransformIndex;
	}
}
