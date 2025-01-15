using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DCF RID: 3535
	[DataContract]
	internal class NotEqualsStringComparisonCdpaPropertyFilter : StringComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06005FC1 RID: 24513 RVA: 0x00148B99 File Offset: 0x00146D99
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
