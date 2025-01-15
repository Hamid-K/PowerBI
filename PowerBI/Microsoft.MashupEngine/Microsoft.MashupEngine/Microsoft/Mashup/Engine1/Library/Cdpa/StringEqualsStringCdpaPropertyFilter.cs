using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DBD RID: 3517
	[DataContract]
	internal class StringEqualsStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x06005F9A RID: 24474 RVA: 0x00148A0E File Offset: 0x00146C0E
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "stringEquals";
			}
		}
	}
}
