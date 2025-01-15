using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200033E RID: 830
	public class DaysOfWeekSelector
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x00070B90 File Offset: 0x0006ED90
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x00070B98 File Offset: 0x0006ED98
		public bool Sunday { get; set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00070BA1 File Offset: 0x0006EDA1
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x00070BA9 File Offset: 0x0006EDA9
		public bool Monday { get; set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00070BB2 File Offset: 0x0006EDB2
		// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x00070BBA File Offset: 0x0006EDBA
		public bool Tuesday { get; set; }

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00070BC3 File Offset: 0x0006EDC3
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x00070BCB File Offset: 0x0006EDCB
		public bool Wednesday { get; set; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x00070BDC File Offset: 0x0006EDDC
		public bool Thursday { get; set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00070BE5 File Offset: 0x0006EDE5
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x00070BED File Offset: 0x0006EDED
		public bool Friday { get; set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00070BF6 File Offset: 0x0006EDF6
		// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x00070BFE File Offset: 0x0006EDFE
		public bool Saturday { get; set; }

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00070C08 File Offset: 0x0006EE08
		internal uint ToUint()
		{
			DaysOfWeek daysOfWeek = (DaysOfWeek)0;
			if (this.Sunday)
			{
				daysOfWeek |= DaysOfWeek.Sunday;
			}
			if (this.Monday)
			{
				daysOfWeek |= DaysOfWeek.Monday;
			}
			if (this.Tuesday)
			{
				daysOfWeek |= DaysOfWeek.Tuesday;
			}
			if (this.Wednesday)
			{
				daysOfWeek |= DaysOfWeek.Wednesday;
			}
			if (this.Thursday)
			{
				daysOfWeek |= DaysOfWeek.Thursday;
			}
			if (this.Friday)
			{
				daysOfWeek |= DaysOfWeek.Friday;
			}
			if (this.Saturday)
			{
				daysOfWeek |= DaysOfWeek.Saturday;
			}
			return (uint)daysOfWeek;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00070C70 File Offset: 0x0006EE70
		internal static DaysOfWeekSelector UintToThis(uint days)
		{
			return new DaysOfWeekSelector
			{
				Sunday = ((days & 1U) > 0U),
				Monday = ((days & 2U) > 0U),
				Tuesday = ((days & 4U) > 0U),
				Wednesday = ((days & 8U) > 0U),
				Thursday = ((days & 16U) > 0U),
				Friday = ((days & 32U) > 0U),
				Saturday = ((days & 64U) > 0U)
			};
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00070CDC File Offset: 0x0006EEDC
		internal static uint ThisToUint(DaysOfWeekSelector selector)
		{
			uint num = 0U;
			if (selector == null)
			{
				return num;
			}
			if (selector.Sunday)
			{
				num |= 1U;
			}
			if (selector.Monday)
			{
				num |= 2U;
			}
			if (selector.Tuesday)
			{
				num |= 4U;
			}
			if (selector.Wednesday)
			{
				num |= 8U;
			}
			if (selector.Thursday)
			{
				num |= 16U;
			}
			if (selector.Friday)
			{
				num |= 32U;
			}
			if (selector.Saturday)
			{
				num |= 64U;
			}
			return num;
		}
	}
}
