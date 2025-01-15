using System;
using System.Runtime.Serialization;

namespace MsolapWrapper
{
	// Token: 0x02000095 RID: 149
	[DataContract]
	internal sealed class PowerBIApiErrorObject
	{
		// Token: 0x04000220 RID: 544
		[DataMember(IsRequired = false, Name = "code")]
		public string Code;

		// Token: 0x04000221 RID: 545
		[DataMember(IsRequired = false, Name = "pbi.error")]
		public PowerBIErrorDetails PowerBIErrorDetails;
	}
}
