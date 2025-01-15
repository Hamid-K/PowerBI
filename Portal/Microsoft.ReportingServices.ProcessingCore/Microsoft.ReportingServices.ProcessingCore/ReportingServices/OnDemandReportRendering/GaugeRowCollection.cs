using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200010D RID: 269
	public sealed class GaugeRowCollection : IDataRegionRowCollection
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x000346C2 File Offset: 0x000328C2
		internal GaugeRowCollection(GaugePanel owner, GaugeRowList gaugeRowCollectionDefs)
		{
			this.m_owner = owner;
			this.m_gaugeRowCollectionDefs = gaugeRowCollectionDefs;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000346D8 File Offset: 0x000328D8
		internal GaugeRowCollection(GaugePanel owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000346E7 File Offset: 0x000328E7
		public GaugeRow GaugeRow
		{
			get
			{
				if (this.m_gaugeRow == null && this.m_gaugeRowCollectionDefs.Count == 1)
				{
					this.m_gaugeRow = new GaugeRow(this.m_owner, this.m_gaugeRowCollectionDefs[0]);
				}
				return this.m_gaugeRow;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00034722 File Offset: 0x00032922
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00034725 File Offset: 0x00032925
		internal void SetNewContext()
		{
			if (this.m_gaugeRow != null)
			{
				this.m_gaugeRow.SetNewContext();
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003473A File Offset: 0x0003293A
		IDataRegionRow IDataRegionRowCollection.GetIfExists(int rowIndex)
		{
			if (rowIndex == 0)
			{
				return this.GaugeRow;
			}
			return null;
		}

		// Token: 0x04000523 RID: 1315
		private GaugePanel m_owner;

		// Token: 0x04000524 RID: 1316
		private GaugeRow m_gaugeRow;

		// Token: 0x04000525 RID: 1317
		private GaugeRowList m_gaugeRowCollectionDefs;
	}
}
