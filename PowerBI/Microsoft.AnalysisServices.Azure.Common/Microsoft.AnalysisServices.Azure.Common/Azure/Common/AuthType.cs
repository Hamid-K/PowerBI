using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000055 RID: 85
	[CLSCompliant(true)]
	[DataContract]
	public enum AuthType
	{
		// Token: 0x04000149 RID: 329
		[EnumMember]
		Certificate,
		// Token: 0x0400014A RID: 330
		[EnumMember]
		AppOnlyToken
	}
}
