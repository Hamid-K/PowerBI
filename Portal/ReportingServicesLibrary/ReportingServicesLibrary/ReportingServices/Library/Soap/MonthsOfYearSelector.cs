using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200033F RID: 831
	public class MonthsOfYearSelector
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00070D48 File Offset: 0x0006EF48
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x00070D50 File Offset: 0x0006EF50
		public bool January { get; set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00070D59 File Offset: 0x0006EF59
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x00070D61 File Offset: 0x0006EF61
		public bool February { get; set; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x00070D6A File Offset: 0x0006EF6A
		// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x00070D72 File Offset: 0x0006EF72
		public bool March { get; set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00070D7B File Offset: 0x0006EF7B
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x00070D83 File Offset: 0x0006EF83
		public bool April { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00070D8C File Offset: 0x0006EF8C
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x00070D94 File Offset: 0x0006EF94
		public bool May { get; set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00070D9D File Offset: 0x0006EF9D
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x00070DA5 File Offset: 0x0006EFA5
		public bool June { get; set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00070DAE File Offset: 0x0006EFAE
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x00070DB6 File Offset: 0x0006EFB6
		public bool July { get; set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00070DBF File Offset: 0x0006EFBF
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x00070DC7 File Offset: 0x0006EFC7
		public bool August { get; set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00070DD8 File Offset: 0x0006EFD8
		public bool September { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00070DE1 File Offset: 0x0006EFE1
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x00070DE9 File Offset: 0x0006EFE9
		public bool October { get; set; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00070DF2 File Offset: 0x0006EFF2
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x00070DFA File Offset: 0x0006EFFA
		public bool November { get; set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00070E03 File Offset: 0x0006F003
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x00070E0B File Offset: 0x0006F00B
		public bool December { get; set; }

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00070E14 File Offset: 0x0006F014
		internal uint ToUint()
		{
			Months months = (Months)0;
			if (this.January)
			{
				months |= Months.January;
			}
			if (this.February)
			{
				months |= Months.February;
			}
			if (this.March)
			{
				months |= Months.March;
			}
			if (this.April)
			{
				months |= Months.April;
			}
			if (this.May)
			{
				months |= Months.May;
			}
			if (this.June)
			{
				months |= Months.June;
			}
			if (this.July)
			{
				months |= Months.July;
			}
			if (this.August)
			{
				months |= Months.August;
			}
			if (this.September)
			{
				months |= Months.September;
			}
			if (this.October)
			{
				months |= Months.October;
			}
			if (this.November)
			{
				months |= Months.November;
			}
			if (this.December)
			{
				months |= Months.December;
			}
			return (uint)months;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00070ECC File Offset: 0x0006F0CC
		internal static MonthsOfYearSelector UintToThis(uint months)
		{
			return new MonthsOfYearSelector
			{
				January = ((months & 1U) > 0U),
				February = ((months & 2U) > 0U),
				March = ((months & 4U) > 0U),
				April = ((months & 8U) > 0U),
				May = ((months & 16U) > 0U),
				June = ((months & 32U) > 0U),
				July = ((months & 64U) > 0U),
				August = ((months & 128U) > 0U),
				September = ((months & 256U) > 0U),
				October = ((months & 512U) > 0U),
				November = ((months & 1024U) > 0U),
				December = ((months & 2048U) > 0U)
			};
		}
	}
}
