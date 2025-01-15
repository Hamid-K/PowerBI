using System;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DA2 RID: 3490
	internal class CdpaDataflowNode : IEquatable<CdpaDataflowNode>
	{
		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x0014800C File Offset: 0x0014620C
		// (set) Token: 0x06005F0E RID: 24334 RVA: 0x00148014 File Offset: 0x00146214
		public CdpaMetricConfiguration Metric { get; set; }

		// Token: 0x06005F0F RID: 24335 RVA: 0x0014801D File Offset: 0x0014621D
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaDataflowNode);
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x0014802B File Offset: 0x0014622B
		public bool Equals(CdpaDataflowNode other)
		{
			return other != null && this.Metric.NullableEquals(other.Metric);
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x00148043 File Offset: 0x00146243
		public override int GetHashCode()
		{
			return this.Metric.NullableGetHashCode<CdpaMetricConfiguration>();
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x00148050 File Offset: 0x00146250
		public CdpaDataflowNode ShallowCopy()
		{
			return new CdpaDataflowNode
			{
				Metric = this.Metric
			};
		}
	}
}
