using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000350 RID: 848
	internal sealed class InternalTablixRowCollection : TablixRowCollection
	{
		// Token: 0x0600209A RID: 8346 RVA: 0x0007EBB4 File Offset: 0x0007CDB4
		internal InternalTablixRowCollection(Microsoft.ReportingServices.OnDemandReportRendering.Tablix owner, TablixRowList rowDefs)
			: base(owner)
		{
			this.m_rowDefs = rowDefs;
			this.m_rowROMDefs = new TablixRow[rowDefs.Count];
		}

		// Token: 0x17001265 RID: 4709
		public override TablixRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_rowROMDefs[index] == null)
				{
					this.m_rowROMDefs[index] = new InternalTablixRow(this.m_owner, index, this.m_rowDefs[index]);
				}
				return this.m_rowROMDefs[index];
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0007EC55 File Offset: 0x0007CE55
		internal IDataRegionRow GetIfExists(int index)
		{
			if (this.m_rowROMDefs != null && index >= 0 && index < this.Count)
			{
				return this.m_rowROMDefs[index];
			}
			return null;
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0007EC76 File Offset: 0x0007CE76
		public override int Count
		{
			get
			{
				return this.m_rowDefs.Count;
			}
		}

		// Token: 0x04001065 RID: 4197
		private TablixRowList m_rowDefs;

		// Token: 0x04001066 RID: 4198
		private TablixRow[] m_rowROMDefs;
	}
}
