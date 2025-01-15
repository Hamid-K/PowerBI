using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200003E RID: 62
	[DataContract]
	public class DataSourceFunctionDocumentationExternal
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000037A1 File Offset: 0x000019A1
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000037A9 File Offset: 0x000019A9
		[DataMember(Name = "Documentation_Name", Order = 0)]
		public string Name { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000037B2 File Offset: 0x000019B2
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000037BA File Offset: 0x000019BA
		[DataMember(Name = "Documentation_Caption", Order = 10)]
		public string Caption { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000037C3 File Offset: 0x000019C3
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000037CB File Offset: 0x000019CB
		[DataMember(Name = "Documentation_Description", Order = 20)]
		public string Description { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000037D4 File Offset: 0x000019D4
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000037DC File Offset: 0x000019DC
		[DataMember(Name = "Documentation_LongDescription", Order = 30)]
		public string LongDescription { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000037E5 File Offset: 0x000019E5
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000037ED File Offset: 0x000019ED
		[DataMember(Name = "Documentation_Examples", Order = 40)]
		public FunctionDocumentationExample Examples { get; set; }
	}
}
