using System;
using System.ComponentModel;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200041A RID: 1050
	public class TableCell
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x00080F96 File Offset: 0x0007F196
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x00080F9E File Offset: 0x0007F19E
		[XmlParentElement("ReportItems")]
		public ReportItem ReportItem
		{
			get
			{
				return this.m_reportItem;
			}
			set
			{
				this.m_reportItem = value;
			}
		}

		// Token: 0x04000EA6 RID: 3750
		private ReportItem m_reportItem;

		// Token: 0x04000EA7 RID: 3751
		[DefaultValue(1)]
		public int ColSpan = 1;
	}
}
