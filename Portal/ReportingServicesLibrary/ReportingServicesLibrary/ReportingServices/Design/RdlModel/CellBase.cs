using System;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000419 RID: 1049
	public abstract class CellBase
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x00080F53 File Offset: 0x0007F153
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x00080F5B File Offset: 0x0007F15B
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

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x00080F64 File Offset: 0x0007F164
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x00080F6C File Offset: 0x0007F16C
		internal int ColSpan
		{
			get
			{
				return this.m_colSpan;
			}
			set
			{
				Utils.ValidateValueRange("ColSpan", value, 1, null);
				this.m_colSpan = value;
			}
		}

		// Token: 0x04000EA4 RID: 3748
		private ReportItem m_reportItem;

		// Token: 0x04000EA5 RID: 3749
		private int m_colSpan = 1;
	}
}
