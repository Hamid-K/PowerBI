using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200003D RID: 61
	[DataContract]
	public class DataSourceFunctionDocumentation
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00003744 File Offset: 0x00001944
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000374C File Offset: 0x0000194C
		[DataMember(Name = "Documentation.Name", Order = 0)]
		public string Name { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003755 File Offset: 0x00001955
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000375D File Offset: 0x0000195D
		[DataMember(Name = "Documentation.Caption", Order = 10)]
		public string Caption { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00003766 File Offset: 0x00001966
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000376E File Offset: 0x0000196E
		[DataMember(Name = "Documentation.Description", Order = 20)]
		public string Description { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003777 File Offset: 0x00001977
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000377F File Offset: 0x0000197F
		[DataMember(Name = "Documentation.LongDescription", Order = 30)]
		public string LongDescription { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003788 File Offset: 0x00001988
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00003790 File Offset: 0x00001990
		[DataMember(Name = "Documentation.Examples", Order = 40)]
		public FunctionDocumentationExample Examples { get; set; }
	}
}
