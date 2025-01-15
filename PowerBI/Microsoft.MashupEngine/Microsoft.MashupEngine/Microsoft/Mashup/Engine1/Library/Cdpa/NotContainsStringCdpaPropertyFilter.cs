using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC4 RID: 3524
	[DataContract]
	internal class NotContainsStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06005FA8 RID: 24488 RVA: 0x00148A47 File Offset: 0x00146C47
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "notContains";
			}
		}
	}
}
