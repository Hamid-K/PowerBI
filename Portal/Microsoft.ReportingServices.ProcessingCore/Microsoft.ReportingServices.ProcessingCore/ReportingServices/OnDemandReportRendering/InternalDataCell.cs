using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000284 RID: 644
	internal sealed class InternalDataCell : Microsoft.ReportingServices.OnDemandReportRendering.DataCell
	{
		// Token: 0x06001906 RID: 6406 RVA: 0x00066992 File Offset: 0x00064B92
		internal InternalDataCell(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, int rowIndex, int colIndex, Microsoft.ReportingServices.ReportIntermediateFormat.DataCell dataCellDef)
			: base(owner, rowIndex, colIndex)
		{
			this.m_dataCellDef = dataCellDef;
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x000669A8 File Offset: 0x00064BA8
		public override DataValueCollection DataValues
		{
			get
			{
				if (this.m_dataValues == null)
				{
					this.m_dataValues = new DataValueCollection(this.m_dataCellDef, this, this.m_owner.RenderingContext, this.m_dataCellDef.DataValues, this.m_owner.Name, false);
				}
				return this.m_dataValues;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x000669F7 File Offset: 0x00064BF7
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.DataCell DataCellDef
		{
			get
			{
				return this.m_dataCellDef;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x000669FF File Offset: 0x00064BFF
		internal override Microsoft.ReportingServices.ReportRendering.DataCell RenderItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x00066A02 File Offset: 0x00064C02
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_dataCellDef;
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00066A0A File Offset: 0x00064C0A
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_dataCellDef != null)
			{
				this.m_dataCellDef.ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x04000C9D RID: 3229
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataCell m_dataCellDef;
	}
}
