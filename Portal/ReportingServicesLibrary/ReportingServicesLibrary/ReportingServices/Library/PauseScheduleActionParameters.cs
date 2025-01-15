using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001DC RID: 476
	internal sealed class PauseScheduleActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00039F2E File Offset: 0x0003812E
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x00039F36 File Offset: 0x00038136
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

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00039F4F File Offset: 0x0003814F
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.m_scheduleID);
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00039F66 File Offset: 0x00038166
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ScheduleID))
			{
				throw new MissingParameterException("ScheduleID");
			}
		}

		// Token: 0x04000660 RID: 1632
		private string m_scheduleID = Guid.Empty.ToString();
	}
}
