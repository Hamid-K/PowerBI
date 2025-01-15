using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD9 RID: 3545
	[DataContract]
	internal abstract class MembershipCdpaPropertyFilter : CdpaPropertyFilter
	{
		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x00148C4F File Offset: 0x00146E4F
		// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x00148C57 File Offset: 0x00146E57
		[DataMember(Name = "values", IsRequired = true)]
		public CdpaListValue Values { get; set; }

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x00148C60 File Offset: 0x00146E60
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x00148C68 File Offset: 0x00146E68
		[DataMember(Name = "comparisonOptions", IsRequired = false)]
		public CdpaStringComparisonOptions ComparisonOptions { get; set; }

		// Token: 0x06005FD9 RID: 24537 RVA: 0x00148C71 File Offset: 0x00146E71
		public override bool Equals(CdpaPropertyFilter other)
		{
			return this.Equals(other as MembershipCdpaPropertyFilter);
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x00148C80 File Offset: 0x00146E80
		public bool Equals(MembershipCdpaPropertyFilter other)
		{
			return other != null && base.PropertyName == other.PropertyName && this.Operator == other.Operator && this.Values.Equals(other.Values) && this.ComparisonOptions.NullableEquals(other.ComparisonOptions);
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x00148CDC File Offset: 0x00146EDC
		public override int GetHashCode()
		{
			return this.Operator.GetHashCode() * 37 + base.PropertyName.GetHashCode() * 5011 + this.Values.GetHashCode() * 37309 + this.ComparisonOptions.NullableGetHashCode<CdpaStringComparisonOptions>();
		}
	}
}
