using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014D6 RID: 5334
	public class LearnConfidenceResult
	{
		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x0600A356 RID: 41814 RVA: 0x0022975D File Offset: 0x0022795D
		// (set) Token: 0x0600A357 RID: 41815 RVA: 0x00229765 File Offset: 0x00227965
		public LearnConfidenceReason Reason { get; set; }

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x0600A358 RID: 41816 RVA: 0x0022976E File Offset: 0x0022796E
		// (set) Token: 0x0600A359 RID: 41817 RVA: 0x00229776 File Offset: 0x00227976
		public double? Score { get; set; }

		// Token: 0x0600A35A RID: 41818 RVA: 0x00229780 File Offset: 0x00227980
		public override string ToString()
		{
			string text = ((this.Reason != LearnConfidenceReason.None) ? string.Format(" ({0})", this.Reason) : string.Empty);
			return ((this.Score == null) ? "--" : this.Score.Value.ToString("N3")) + text;
		}
	}
}
