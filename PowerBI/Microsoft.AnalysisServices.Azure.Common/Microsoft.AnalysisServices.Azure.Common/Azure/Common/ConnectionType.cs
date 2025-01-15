using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000054 RID: 84
	[CLSCompliant(true)]
	[DataContract]
	public enum ConnectionType
	{
		// Token: 0x04000146 RID: 326
		[EnumMember]
		Http,
		// Token: 0x04000147 RID: 327
		[EnumMember]
		Tcp
	}
}
