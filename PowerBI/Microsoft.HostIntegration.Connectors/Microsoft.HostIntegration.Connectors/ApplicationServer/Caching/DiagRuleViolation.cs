using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001AC RID: 428
	internal class DiagRuleViolation
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0002F5E4 File Offset: 0x0002D7E4
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0002F5EC File Offset: 0x0002D7EC
		public DiagnosticRule RuleViolated { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0002F5F5 File Offset: 0x0002D7F5
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x0002F5FD File Offset: 0x0002D7FD
		public bool IsRuleViolated { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0002F606 File Offset: 0x0002D806
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0002F60E File Offset: 0x0002D80E
		public bool IsFinalStateReached { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0002F617 File Offset: 0x0002D817
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0002F61F File Offset: 0x0002D81F
		public string ViolationClasses { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0002F628 File Offset: 0x0002D828
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0002F630 File Offset: 0x0002D830
		public int ViolationIndex { get; set; }

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002F639 File Offset: 0x0002D839
		public override string ToString()
		{
			if (this.RuleViolated != null)
			{
				return this.RuleViolated.ToString() + " At: " + this.ViolationIndex;
			}
			return null;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0002F665 File Offset: 0x0002D865
		public int ViolationId
		{
			get
			{
				if (this.RuleViolated != null)
				{
					return this.RuleViolated.RuleId;
				}
				return -1;
			}
		}
	}
}
