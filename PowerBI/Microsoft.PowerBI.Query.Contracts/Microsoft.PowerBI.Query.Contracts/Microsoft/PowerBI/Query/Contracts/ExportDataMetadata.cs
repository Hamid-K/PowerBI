using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Query.Contracts
{
	// Token: 0x02000006 RID: 6
	[DataContract]
	public class ExportDataMetadata
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		[DataMember(Name = "primarySelectsMap", Order = 10, EmitDefaultValue = false)]
		public IList<Tuple<string, string>> PrimarySelectsMap { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		[DataMember(Name = "columnsFormatting", Order = 20, EmitDefaultValue = false)]
		public IList<string> ColumnsFormatting { get; set; }
	}
}
