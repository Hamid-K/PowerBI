using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC8 RID: 3528
	[DataContract]
	internal class IsFalseUnaryCdpaPropertyFilter : UnaryCdpaPropertyFilter
	{
		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x00148AB2 File Offset: 0x00146CB2
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isFalse";
			}
		}
	}
}
