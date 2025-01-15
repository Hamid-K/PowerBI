using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001DE RID: 478
	internal sealed class ResumeScheduleActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0003A06D File Offset: 0x0003826D
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x0003A075 File Offset: 0x00038275
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0003A08E File Offset: 0x0003828E
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.m_scheduleID);
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003A0A5 File Offset: 0x000382A5
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ScheduleID))
			{
				throw new MissingParameterException("ScheduleID");
			}
		}

		// Token: 0x04000661 RID: 1633
		private string m_scheduleID = Guid.Empty.ToString();
	}
}
