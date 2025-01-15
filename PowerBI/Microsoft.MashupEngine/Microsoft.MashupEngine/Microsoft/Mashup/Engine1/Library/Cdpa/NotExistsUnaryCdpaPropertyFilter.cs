using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DCA RID: 3530
	[DataContract]
	internal class NotExistsUnaryCdpaPropertyFilter : UnaryCdpaPropertyFilter
	{
		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x06005FB4 RID: 24500 RVA: 0x00148AC0 File Offset: 0x00146CC0
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "notExists";
			}
		}
	}
}
