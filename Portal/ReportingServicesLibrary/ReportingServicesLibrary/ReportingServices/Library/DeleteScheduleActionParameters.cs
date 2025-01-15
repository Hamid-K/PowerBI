using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D4 RID: 468
	internal sealed class DeleteScheduleActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000399EA File Offset: 0x00037BEA
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x000399F2 File Offset: 0x00037BF2
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

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00039A0B File Offset: 0x00037C0B
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "ScheduleID={0}", this.ScheduleID);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00039A22 File Offset: 0x00037C22
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ScheduleID))
			{
				throw new MissingParameterException("ScheduleID");
			}
		}

		// Token: 0x04000658 RID: 1624
		private string m_scheduleID = Guid.Empty.ToString();
	}
}
