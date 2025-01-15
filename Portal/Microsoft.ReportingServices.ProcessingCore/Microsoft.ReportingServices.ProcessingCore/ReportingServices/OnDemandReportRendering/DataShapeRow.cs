using System;
using System.Linq;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200009F RID: 159
	internal sealed class DataShapeRow : ReportElementCollectionBase<DataShapeIntersection>, IDataRegionRow
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0002740D File Offset: 0x0002560D
		internal DataShapeRow(DataShape ownerDataShape, int rowIndex, DataShapeRow rifDataShapeRow)
		{
			this.m_ownerDataShape = ownerDataShape;
			this.m_rowIndex = rowIndex;
			this.m_rifDataShapeRow = rifDataShapeRow;
			this.m_intersections = new DataShapeIntersection[rifDataShapeRow.Cells.Count];
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00027440 File Offset: 0x00025640
		public override int Count
		{
			get
			{
				return this.m_intersections.Length;
			}
		}

		// Token: 0x170005E7 RID: 1511
		public override DataShapeIntersection this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				DataShapeIntersection rifIntersection = this.m_rifDataShapeRow.DataShapeIntersections[index];
				DataShapeLimit dataShapeLimit = ((this.m_ownerDataShape.Limits == null) ? null : this.m_ownerDataShape.Limits.Where((DataShapeLimit l) => l.Target == rifIntersection.Name).SingleOrDefault<DataShapeLimit>());
				if (this.m_intersections[index] == null)
				{
					this.m_intersections[index] = new DataShapeIntersection(this.m_ownerDataShape, rifIntersection, this.m_rowIndex, index, dataShapeLimit);
				}
				return this.m_intersections[index];
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00027519 File Offset: 0x00025719
		IDataRegionCell IDataRegionRow.GetIfExists(int index)
		{
			return this.GetIfExists(index);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00027522 File Offset: 0x00025722
		internal IDataRegionCell GetIfExists(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				return null;
			}
			return this.m_intersections[index];
		}

		// Token: 0x04000277 RID: 631
		private readonly DataShape m_ownerDataShape;

		// Token: 0x04000278 RID: 632
		private readonly int m_rowIndex;

		// Token: 0x04000279 RID: 633
		private readonly DataShapeRow m_rifDataShapeRow;

		// Token: 0x0400027A RID: 634
		private readonly DataShapeIntersection[] m_intersections;
	}
}
