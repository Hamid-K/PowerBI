using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003AB RID: 939
	public class StaticMember
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x0007D7FC File Offset: 0x0007B9FC
		public StaticMember()
		{
			this.Label = "";
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0007D80F File Offset: 0x0007BA0F
		public StaticMember(string label)
		{
			this.Label = label;
		}

		// Token: 0x04000D21 RID: 3361
		public string Label;
	}
}
