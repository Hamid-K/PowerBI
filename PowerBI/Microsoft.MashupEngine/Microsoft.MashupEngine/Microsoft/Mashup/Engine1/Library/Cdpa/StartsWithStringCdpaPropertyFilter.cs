using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DBF RID: 3519
	[DataContract]
	internal class StartsWithStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x06005F9E RID: 24478 RVA: 0x00148A24 File Offset: 0x00146C24
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "startsWith";
			}
		}
	}
}
