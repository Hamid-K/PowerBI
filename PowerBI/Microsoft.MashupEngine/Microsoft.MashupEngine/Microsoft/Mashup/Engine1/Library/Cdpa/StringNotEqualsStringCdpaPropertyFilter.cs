using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DBE RID: 3518
	[DataContract]
	internal class StringNotEqualsStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x06005F9C RID: 24476 RVA: 0x00148A1D File Offset: 0x00146C1D
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "stringNotEquals";
			}
		}
	}
}
