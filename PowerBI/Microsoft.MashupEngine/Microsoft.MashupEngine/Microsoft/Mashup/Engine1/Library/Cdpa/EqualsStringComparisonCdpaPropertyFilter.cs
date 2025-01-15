using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DCE RID: 3534
	[DataContract]
	internal class EqualsStringComparisonCdpaPropertyFilter : StringComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06005FBF RID: 24511 RVA: 0x00148B8A File Offset: 0x00146D8A
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
