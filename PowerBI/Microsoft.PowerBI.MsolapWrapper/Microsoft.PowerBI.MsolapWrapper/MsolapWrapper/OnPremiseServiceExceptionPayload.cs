using System;
using System.Runtime.Serialization;

namespace MsolapWrapper
{
	// Token: 0x02000096 RID: 150
	[DataContract]
	internal sealed class OnPremiseServiceExceptionPayload
	{
		// Token: 0x04000222 RID: 546
		[DataMember(IsRequired = false, Name = "error")]
		public PowerBIApiErrorObject Error;
	}
}
