using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DDD RID: 3549
	[DataContract]
	internal class TimeDifferenceCdpaPropertyFilter : CdpaPropertyFilter
	{
		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06005FE3 RID: 24547 RVA: 0x00148D88 File Offset: 0x00146F88
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isTimeDifferenceBetween";
			}
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06005FE4 RID: 24548 RVA: 0x00148D8F File Offset: 0x00146F8F
		// (set) Token: 0x06005FE5 RID: 24549 RVA: 0x00148D97 File Offset: 0x00146F97
		[DataMember(Name = "secondPropertyName", IsRequired = true)]
		public string SecondPropertyName { get; set; }

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x06005FE6 RID: 24550 RVA: 0x00148DA0 File Offset: 0x00146FA0
		// (set) Token: 0x06005FE7 RID: 24551 RVA: 0x00148DA8 File Offset: 0x00146FA8
		[DataMember(Name = "min", IsRequired = true)]
		public int Min { get; set; }

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x06005FE8 RID: 24552 RVA: 0x00148DB1 File Offset: 0x00146FB1
		// (set) Token: 0x06005FE9 RID: 24553 RVA: 0x00148DB9 File Offset: 0x00146FB9
		[DataMember(Name = "max", IsRequired = true)]
		public int Max { get; set; }

		// Token: 0x06005FEA RID: 24554 RVA: 0x00148DC2 File Offset: 0x00146FC2
		public override bool Equals(CdpaPropertyFilter other)
		{
			return this.Equals(other as TimeDifferenceCdpaPropertyFilter);
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x00148DD0 File Offset: 0x00146FD0
		public bool Equals(TimeDifferenceCdpaPropertyFilter other)
		{
			return other != null && base.PropertyName == other.PropertyName && this.SecondPropertyName == other.SecondPropertyName && this.Min == other.Min && this.Max == other.Max;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x00148E24 File Offset: 0x00147024
		public override int GetHashCode()
		{
			return this.Operator.GetHashCode() * 37 + base.PropertyName.GetHashCode() * 5011 + this.SecondPropertyName.GetHashCode() * 37309 + this.Min + this.Max;
		}
	}
}
