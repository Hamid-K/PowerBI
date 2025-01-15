using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC3 RID: 3523
	[DataContract]
	internal class ContainsStringCdpaPropertyFilter : StringCdpaPropertyFilter
	{
		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06005FA6 RID: 24486 RVA: 0x00148A40 File Offset: 0x00146C40
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "contains";
			}
		}
	}
}
