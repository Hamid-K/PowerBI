using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000052 RID: 82
	[DataContract]
	public sealed class DatabaseParameter
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000FE13 File Offset: 0x0000E013
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000FE1B File Offset: 0x0000E01B
		[DataMember]
		public string Key { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000FE24 File Offset: 0x0000E024
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000FE2C File Offset: 0x0000E02C
		[DataMember]
		public string Value { get; set; }
	}
}
