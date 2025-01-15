using System;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F1 RID: 1009
	public sealed class StaticColumnRow
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0007F180 File Offset: 0x0007D380
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0007F188 File Offset: 0x0007D388
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

		// Token: 0x04000DFD RID: 3581
		private ReportItem m_reportItem;
	}
}
