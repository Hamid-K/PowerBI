using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A8 RID: 680
	internal sealed class Field
	{
		// Token: 0x06001A23 RID: 6691 RVA: 0x00069C3A File Offset: 0x00067E3A
		internal Field(Field fieldDef)
		{
			this.m_fieldDef = fieldDef;
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00069C49 File Offset: 0x00067E49
		public string Name
		{
			get
			{
				return this.m_fieldDef.Name;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00069C56 File Offset: 0x00067E56
		public string DataField
		{
			get
			{
				return this.m_fieldDef.DataField;
			}
		}

		// Token: 0x04000D06 RID: 3334
		private Field m_fieldDef;
	}
}
