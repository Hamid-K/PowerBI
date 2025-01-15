using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D8 RID: 472
	internal sealed class GetSchedulePropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00039BC1 File Offset: 0x00037DC1
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x00039BC9 File Offset: 0x00037DC9
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00039BE2 File Offset: 0x00037DE2
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00039BEA File Offset: 0x00037DEA
		public Schedule Schedule
		{
			get
			{
				return this.m_schedule;
			}
			set
			{
				this.m_schedule = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00039BF3 File Offset: 0x00037DF3
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.ScheduleID);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00039C0A File Offset: 0x00037E0A
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "ID ({0})", this.ScheduleID);
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00039C21 File Offset: 0x00037E21
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ScheduleID))
			{
				throw new MissingParameterException("ScheduleID");
			}
		}

		// Token: 0x0400065B RID: 1627
		private string m_scheduleID = Guid.Empty.ToString();

		// Token: 0x0400065C RID: 1628
		private Schedule m_schedule;
	}
}
