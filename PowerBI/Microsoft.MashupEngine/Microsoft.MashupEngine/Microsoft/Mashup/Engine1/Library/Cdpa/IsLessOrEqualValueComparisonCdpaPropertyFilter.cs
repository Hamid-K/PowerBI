using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD7 RID: 3543
	[DataContract]
	internal class IsLessOrEqualValueComparisonCdpaPropertyFilter : ValueComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x00148C48 File Offset: 0x00146E48
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isLessOrEqual";
			}
		}
	}
}
