using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DBC RID: 3516
	[DataContract]
	internal abstract class StringCdpaPropertyFilter : CdpaPropertyFilter
	{
		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06005F92 RID: 24466 RVA: 0x0014894B File Offset: 0x00146B4B
		// (set) Token: 0x06005F93 RID: 24467 RVA: 0x00148953 File Offset: 0x00146B53
		[DataMember(Name = "value", IsRequired = true)]
		public CdpaValue Value { get; set; }

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06005F94 RID: 24468 RVA: 0x0014895C File Offset: 0x00146B5C
		// (set) Token: 0x06005F95 RID: 24469 RVA: 0x00148964 File Offset: 0x00146B64
		[DataMember(Name = "comparisonOptions", IsRequired = false)]
		public CdpaStringComparisonOptions ComparisonOptions { get; set; }

		// Token: 0x06005F96 RID: 24470 RVA: 0x0014896D File Offset: 0x00146B6D
		public override bool Equals(CdpaPropertyFilter other)
		{
			return this.Equals(other as StringCdpaPropertyFilter);
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x0014897C File Offset: 0x00146B7C
		public bool Equals(StringCdpaPropertyFilter other)
		{
			return other != null && this.Operator == other.Operator && base.PropertyName == other.PropertyName && this.Value.Equals(other.Value) && this.ComparisonOptions.NullableEquals(other.ComparisonOptions);
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x001489D8 File Offset: 0x00146BD8
		public override int GetHashCode()
		{
			return this.Operator.GetHashCode() * 37 + base.PropertyName.GetHashCode() * 5011 + this.Value.GetHashCode();
		}
	}
}
