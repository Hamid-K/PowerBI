using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC0 RID: 3520
	[DataContract]
	internal class NotStartsWithStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06005FA0 RID: 24480 RVA: 0x00148A2B File Offset: 0x00146C2B
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "notStartsWith";
			}
		}
	}
}
