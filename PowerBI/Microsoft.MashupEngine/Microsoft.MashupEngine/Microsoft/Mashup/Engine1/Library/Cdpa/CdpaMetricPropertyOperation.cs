using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE7 RID: 3559
	[DataContract]
	internal abstract class CdpaMetricPropertyOperation : CdpaOperation, IEquatable<CdpaMetricPropertyOperation>
	{
		// Token: 0x06006023 RID: 24611 RVA: 0x00149506 File Offset: 0x00147706
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x0014958B File Offset: 0x0014778B
		public override bool Equals(CdpaOperation other)
		{
			return this.Equals(other as CdpaMetricPropertyOperation);
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x00149521 File Offset: 0x00147721
		public virtual bool Equals(CdpaMetricPropertyOperation other)
		{
			return other != null && this.Name == other.Name;
		}
	}
}
