using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD3 RID: 3539
	[DataContract]
	internal class NotEqualsValueComparisonCdpaPropertyFilter : ValueComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x00148B99 File Offset: 0x00146D99
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "notEquals";
			}
		}
	}
}
