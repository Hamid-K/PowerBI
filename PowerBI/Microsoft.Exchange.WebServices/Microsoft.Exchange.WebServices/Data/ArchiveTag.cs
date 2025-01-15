using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000035 RID: 53
	public sealed class ArchiveTag : RetentionTagBase
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000A180 File Offset: 0x00009180
		public ArchiveTag()
			: base("ArchiveTag")
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A18D File Offset: 0x0000918D
		public ArchiveTag(bool isExplicit, Guid retentionId)
			: this()
		{
			base.IsExplicit = isExplicit;
			base.RetentionId = retentionId;
		}
	}
}
