using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200020D RID: 525
	public class AdvancedCondition
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x000281BC File Offset: 0x000263BC
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x000281C4 File Offset: 0x000263C4
		public ConditionType ConditionType { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x000281CD File Offset: 0x000263CD
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x000281D5 File Offset: 0x000263D5
		public FilterOperator FilterOperator { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x000281DE File Offset: 0x000263DE
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x000281E6 File Offset: 0x000263E6
		public string AdvancedConditionValue { get; set; }
	}
}
