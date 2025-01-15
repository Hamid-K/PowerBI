using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC1 RID: 3521
	[DataContract]
	internal class EndsWithStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06005FA2 RID: 24482 RVA: 0x00148A32 File Offset: 0x00146C32
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "endsWith";
			}
		}
	}
}
