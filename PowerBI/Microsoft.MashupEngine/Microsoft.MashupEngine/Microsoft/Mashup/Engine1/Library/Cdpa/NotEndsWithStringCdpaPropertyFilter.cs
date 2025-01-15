using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC2 RID: 3522
	[DataContract]
	internal class NotEndsWithStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06005FA4 RID: 24484 RVA: 0x00148A39 File Offset: 0x00146C39
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "notEndsWith";
			}
		}
	}
}
