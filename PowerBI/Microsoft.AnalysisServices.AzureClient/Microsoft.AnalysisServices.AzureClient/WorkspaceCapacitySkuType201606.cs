using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000009 RID: 9
	[Guid("072C2F98-E96A-4987-944C-12DEFA942FF3")]
	[ComVisible(true)]
	[DataContract]
	public enum WorkspaceCapacitySkuType201606
	{
		// Token: 0x0400001F RID: 31
		[EnumMember(Value = "Shared")]
		Shared,
		// Token: 0x04000020 RID: 32
		[EnumMember(Value = "Premium")]
		Premium
	}
}
