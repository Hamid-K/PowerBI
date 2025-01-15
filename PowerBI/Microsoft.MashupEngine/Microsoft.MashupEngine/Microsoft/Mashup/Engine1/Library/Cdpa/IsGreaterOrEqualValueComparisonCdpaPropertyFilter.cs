using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD5 RID: 3541
	[DataContract]
	internal class IsGreaterOrEqualValueComparisonCdpaPropertyFilter : ValueComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x06005FCF RID: 24527 RVA: 0x00148C3A File Offset: 0x00146E3A
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isGreaterOrEqual";
			}
		}
	}
}
