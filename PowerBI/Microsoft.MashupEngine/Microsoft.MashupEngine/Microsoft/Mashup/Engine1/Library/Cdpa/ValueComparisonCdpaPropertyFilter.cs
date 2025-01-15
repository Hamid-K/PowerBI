using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD1 RID: 3537
	[DataContract]
	internal abstract class ValueComparisonCdpaPropertyFilter : ComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x00148BA0 File Offset: 0x00146DA0
		// (set) Token: 0x06005FC4 RID: 24516 RVA: 0x00148BA8 File Offset: 0x00146DA8
		[DataMember(Name = "value", IsRequired = true)]
		public CdpaValue Value { get; set; }

		// Token: 0x06005FC5 RID: 24517 RVA: 0x00148BB1 File Offset: 0x00146DB1
		public override bool Equals(CdpaPropertyFilter other)
		{
			return this.Equals(other as ValueComparisonCdpaPropertyFilter);
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x00148BBF File Offset: 0x00146DBF
		public bool Equals(ValueComparisonCdpaPropertyFilter other)
		{
			return other != null && this.Operator == other.Operator && base.PropertyName == other.PropertyName && this.Value.Equals(other.Value);
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x00148BFD File Offset: 0x00146DFD
		public override int GetHashCode()
		{
			return this.Operator.GetHashCode() * 37 + base.PropertyName.GetHashCode() * 5011 + this.Value.GetHashCode();
		}
	}
}
