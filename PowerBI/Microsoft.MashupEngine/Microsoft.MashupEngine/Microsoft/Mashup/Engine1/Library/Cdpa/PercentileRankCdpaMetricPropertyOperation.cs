using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DEC RID: 3564
	[DataContract]
	internal class PercentileRankCdpaMetricPropertyOperation : CdpaMetricPropertyOperation
	{
		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x06006032 RID: 24626 RVA: 0x001495FD File Offset: 0x001477FD
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "percentileRank";
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x00149604 File Offset: 0x00147804
		// (set) Token: 0x06006034 RID: 24628 RVA: 0x0014960C File Offset: 0x0014780C
		[DataMember(Name = "value", IsRequired = true)]
		public double Value { get; set; }

		// Token: 0x06006035 RID: 24629 RVA: 0x00149506 File Offset: 0x00147706
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x00149615 File Offset: 0x00147815
		public override bool Equals(CdpaMetricPropertyOperation other)
		{
			return this.Equals(other as PercentileRankCdpaMetricPropertyOperation);
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x00149623 File Offset: 0x00147823
		public bool Equals(PercentileRankCdpaMetricPropertyOperation other)
		{
			return other != null && this.Name == other.Name && this.Value == other.Value;
		}
	}
}
