using System;
using System.Runtime.Serialization;

namespace MsolapWrapper
{
	// Token: 0x02000094 RID: 148
	[DataContract]
	internal sealed class PowerBIErrorDetails
	{
		// Token: 0x0400021F RID: 543
		[DataMember(IsRequired = false, Name = "exceptionCulprit")]
		public int ExceptionCulprit;
	}
}
