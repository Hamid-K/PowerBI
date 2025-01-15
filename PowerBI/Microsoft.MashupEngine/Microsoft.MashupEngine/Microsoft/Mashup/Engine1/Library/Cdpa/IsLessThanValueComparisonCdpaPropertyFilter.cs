using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD6 RID: 3542
	[DataContract]
	internal class IsLessThanValueComparisonCdpaPropertyFilter : ValueComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x00148C41 File Offset: 0x00146E41
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isLessThan";
			}
		}
	}
}
