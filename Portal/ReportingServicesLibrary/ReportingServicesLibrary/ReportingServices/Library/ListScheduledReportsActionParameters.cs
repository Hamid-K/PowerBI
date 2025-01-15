using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E0 RID: 480
	internal sealed class ListScheduledReportsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0003A1AD File Offset: 0x000383AD
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x0003A1B5 File Offset: 0x000383B5
		public string ScheduleID
		{
			get
			{
				return this.m_scheduleID;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_scheduleID = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0003A1CE File Offset: 0x000383CE
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.m_scheduleID);
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003A1E5 File Offset: 0x000383E5
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ScheduleID))
			{
				throw new MissingParameterException("ScheduleID");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0003A1FF File Offset: 0x000383FF
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x0003A207 File Offset: 0x00038407
		public CatalogItemList Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		// Token: 0x04000662 RID: 1634
		private string m_scheduleID = Guid.Empty.ToString();

		// Token: 0x04000663 RID: 1635
		private CatalogItemList m_children = new CatalogItemList();
	}
}
