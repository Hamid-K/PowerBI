using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DEB RID: 3563
	[DataContract]
	internal class PercentileCdpaMetricPropertyOperation : CdpaMetricPropertyOperation
	{
		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x001495AF File Offset: 0x001477AF
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "percentile";
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x001495B6 File Offset: 0x001477B6
		// (set) Token: 0x0600602D RID: 24621 RVA: 0x001495BE File Offset: 0x001477BE
		[DataMember(Name = "value", IsRequired = true)]
		public double Value { get; set; }

		// Token: 0x0600602E RID: 24622 RVA: 0x00149506 File Offset: 0x00147706
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x001495C7 File Offset: 0x001477C7
		public override bool Equals(CdpaMetricPropertyOperation other)
		{
			return this.Equals(other as PercentileCdpaMetricPropertyOperation);
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x001495D5 File Offset: 0x001477D5
		public bool Equals(PercentileCdpaMetricPropertyOperation other)
		{
			return other != null && this.Name == other.Name && this.Value == other.Value;
		}
	}
}
