using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DC7 RID: 3527
	[DataContract]
	internal class IsTrueUnaryCdpaPropertyFilter : UnaryCdpaPropertyFilter
	{
		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x00148AA3 File Offset: 0x00146CA3
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isTrue";
			}
		}
	}
}
