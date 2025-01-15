using System;
using System.Runtime.Serialization;
using Microsoft.Lucia.Core;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200006E RID: 110
	[DataContract]
	public sealed class ReportMetadata
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00009FFC File Offset: 0x000081FC
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000A004 File Offset: 0x00008204
		[DataMember(IsRequired = false, Order = 10, Name = "Pods")]
		public Pod[] Pods { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000A00D File Offset: 0x0000820D
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000A015 File Offset: 0x00008215
		[DataMember(IsRequired = false, Order = 20, Name = "LinguisticSchema")]
		public ModelLinguisticSchema LinguisticSchema { get; set; }
	}
}
