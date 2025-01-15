using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200003C RID: 60
	[DataContract]
	public class DataSourceFunction
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000036E7 File Offset: 0x000018E7
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000036EF File Offset: 0x000018EF
		[DataMember(Name = "name", Order = 0)]
		public string Name { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000036F8 File Offset: 0x000018F8
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00003700 File Offset: 0x00001900
		[DataMember(Name = "parameters", Order = 10)]
		public IList<DataSourceFunctionParameter> Parameters { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003709 File Offset: 0x00001909
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00003711 File Offset: 0x00001911
		[DataMember(Name = "publish", Order = 20)]
		public DataSourceFunctionPublish Publish { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000371A File Offset: 0x0000191A
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00003722 File Offset: 0x00001922
		[DataMember(Name = "documentation", Order = 30)]
		public DataSourceFunctionDocumentation Documentation { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000372B File Offset: 0x0000192B
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00003733 File Offset: 0x00001933
		[DataMember(Name = "functionDocumentation", Order = 40)]
		public DataSourceFunctionDocumentationExternal DocumentationExternal { get; set; }
	}
}
