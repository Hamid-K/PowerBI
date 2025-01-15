using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000087 RID: 135
	public sealed class PolicyTag : RetentionTagBase
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00014C28 File Offset: 0x00013C28
		public PolicyTag()
			: base("PolicyTag")
		{
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00014C35 File Offset: 0x00013C35
		public PolicyTag(bool isExplicit, Guid retentionId)
			: this()
		{
			base.IsExplicit = isExplicit;
			base.RetentionId = retentionId;
		}
	}
}
