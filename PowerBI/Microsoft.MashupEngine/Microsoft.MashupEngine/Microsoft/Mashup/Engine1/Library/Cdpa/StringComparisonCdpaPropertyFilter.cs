using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DCD RID: 3533
	[DataContract]
	internal abstract class StringComparisonCdpaPropertyFilter : ComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x06005FB7 RID: 24503 RVA: 0x00148AC7 File Offset: 0x00146CC7
		// (set) Token: 0x06005FB8 RID: 24504 RVA: 0x00148ACF File Offset: 0x00146CCF
		[DataMember(Name = "value", IsRequired = true)]
		public CdpaValue Value { get; set; }

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06005FB9 RID: 24505 RVA: 0x00148AD8 File Offset: 0x00146CD8
		// (set) Token: 0x06005FBA RID: 24506 RVA: 0x00148AE0 File Offset: 0x00146CE0
		[DataMember(Name = "comparisonOptions", IsRequired = false)]
		public CdpaStringComparisonOptions ComparisonOptions { get; set; }

		// Token: 0x06005FBB RID: 24507 RVA: 0x00148AE9 File Offset: 0x00146CE9
		public override bool Equals(CdpaPropertyFilter other)
		{
			return this.Equals(other as StringComparisonCdpaPropertyFilter);
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x00148AF8 File Offset: 0x00146CF8
		public bool Equals(StringComparisonCdpaPropertyFilter other)
		{
			return other != null && this.Operator == other.Operator && base.PropertyName == other.PropertyName && this.Value.Equals(other.Value) && this.ComparisonOptions.NullableEquals(other.ComparisonOptions);
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x00148B54 File Offset: 0x00146D54
		public override int GetHashCode()
		{
			return this.Operator.GetHashCode() * 37 + base.PropertyName.GetHashCode() * 5011 + this.Value.GetHashCode();
		}
	}
}
