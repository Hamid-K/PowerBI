using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD2 RID: 3538
	[DataContract]
	internal class EqualsValueComparisonCdpaPropertyFilter : ValueComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x00148B8A File Offset: 0x00146D8A
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "equals";
			}
		}
	}
}
