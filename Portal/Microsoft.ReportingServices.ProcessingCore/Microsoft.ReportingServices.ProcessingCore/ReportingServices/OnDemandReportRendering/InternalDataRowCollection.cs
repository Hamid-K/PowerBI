using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200027D RID: 637
	internal sealed class InternalDataRowCollection : DataRowCollection
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x000663AB File Offset: 0x000645AB
		internal InternalDataRowCollection(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, CustomDataRowList dataRowDefs)
			: base(owner)
		{
			this.m_dataRowDefs = dataRowDefs;
		}

		// Token: 0x17000E42 RID: 3650
		public override DataRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_cachedDataRows == null)
				{
					this.m_cachedDataRows = new DataRow[this.Count];
				}
				if (this.m_cachedDataRows[index] == null)
				{
					this.m_cachedDataRows[index] = new InternalDataRow(this.m_owner, index, this.m_dataRowDefs[index]);
				}
				return this.m_cachedDataRows[index];
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00066452 File Offset: 0x00064652
		public override int Count
		{
			get
			{
				return this.m_dataRowDefs.Count;
			}
		}

		// Token: 0x04000C8E RID: 3214
		private CustomDataRowList m_dataRowDefs;
	}
}
