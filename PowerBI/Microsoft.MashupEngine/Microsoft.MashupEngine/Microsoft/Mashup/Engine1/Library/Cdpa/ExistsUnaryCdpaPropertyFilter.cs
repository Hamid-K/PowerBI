using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC9 RID: 3529
	[DataContract]
	internal class ExistsUnaryCdpaPropertyFilter : UnaryCdpaPropertyFilter
	{
		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x00148AB9 File Offset: 0x00146CB9
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "exists";
			}
		}
	}
}
