using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000A0 RID: 160
	internal sealed class DataShapeRowCollection : ReportElementCollectionBase<DataShapeRow>, IDataRegionRowCollection
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x0002753B File Offset: 0x0002573B
		internal DataShapeRowCollection(DataShape ownerDataShape, DataShapeRowList rows)
		{
			this.m_ownerDataShape = ownerDataShape;
			this.m_rifRowList = rows;
			if (rows != null)
			{
				this.m_rows = new DataShapeRow[rows.Count];
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00027565 File Offset: 0x00025765
		public override int Count
		{
			get
			{
				if (this.m_rows != null)
				{
					return this.m_rows.Length;
				}
				return 0;
			}
		}

		// Token: 0x170005E9 RID: 1513
		public override DataShapeRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_rows[index] == null)
				{
					this.m_rows[index] = new DataShapeRow(this.m_ownerDataShape, index, this.m_rifRowList[index]);
				}
				return this.m_rows[index];
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000275F9 File Offset: 0x000257F9
		public IDataRegionRow GetIfExists(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				return null;
			}
			return this[index];
		}

		// Token: 0x0400027B RID: 635
		private readonly DataShape m_ownerDataShape;

		// Token: 0x0400027C RID: 636
		private readonly DataShapeRowList m_rifRowList;

		// Token: 0x0400027D RID: 637
		private readonly DataShapeRow[] m_rows;
	}
}
