using System;
using System.Collections.Generic;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.Artifacts
{
	// Token: 0x02000031 RID: 49
	public class DataModelArtifacts
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000055B2 File Offset: 0x000037B2
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000055BA File Offset: 0x000037BA
		public string ModelVersion { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000055C3 File Offset: 0x000037C3
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000055CB File Offset: 0x000037CB
		public IReadOnlyList<PbixModelRole> DataModelRoles { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000055D4 File Offset: 0x000037D4
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000055DC File Offset: 0x000037DC
		public IReadOnlyList<PbixDataSource> EmbeddedDataSources { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000055E5 File Offset: 0x000037E5
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000055ED File Offset: 0x000037ED
		public IReadOnlyList<PbixModelParameter> DataModelParameters { get; set; }
	}
}
