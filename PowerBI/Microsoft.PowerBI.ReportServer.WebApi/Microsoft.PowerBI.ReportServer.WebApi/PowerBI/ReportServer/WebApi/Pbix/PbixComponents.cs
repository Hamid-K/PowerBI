using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Packaging.Storage;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x0200001B RID: 27
	public sealed class PbixComponents
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003276 File Offset: 0x00001476
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000327E File Offset: 0x0000147E
		public string ReportDocument { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003287 File Offset: 0x00001487
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000328F File Offset: 0x0000148F
		public string ReportMobileState { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000032A0 File Offset: 0x000014A0
		public byte[] DataModel { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000032A9 File Offset: 0x000014A9
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000032B1 File Offset: 0x000014B1
		public ConnectionsSettings Connections { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000032BA File Offset: 0x000014BA
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000032C2 File Offset: 0x000014C2
		public StaticResourceCollection StaticResources { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000032CB File Offset: 0x000014CB
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000032D3 File Offset: 0x000014D3
		public bool HasCustomVisuals { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000032E4 File Offset: 0x000014E4
		public IDictionary<string, byte[]> CustomVisuals { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000032F5 File Offset: 0x000014F5
		public bool IsMobileOptimized { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000032FE File Offset: 0x000014FE
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003306 File Offset: 0x00001506
		public bool HasEmbeddedModels { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000330F File Offset: 0x0000150F
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003317 File Offset: 0x00001517
		public bool HasDirectQuery { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003328 File Offset: 0x00001528
		public bool ModelRefreshAllowed { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003331 File Offset: 0x00001531
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003339 File Offset: 0x00001539
		public IList<DataSource> EmbeddedDataSources { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003342 File Offset: 0x00001542
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000334A File Offset: 0x0000154A
		public IList<DataModelRole> DataModelRoles { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003353 File Offset: 0x00001553
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000335B File Offset: 0x0000155B
		public IList<DataModelParameter> DataModelParameters { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003364 File Offset: 0x00001564
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000336C File Offset: 0x0000156C
		public DataModelDataSourceType DatabaseType { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003375 File Offset: 0x00001575
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000337D File Offset: 0x0000157D
		public string CreatedFromVersion { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003386 File Offset: 0x00001586
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000338E File Offset: 0x0000158E
		public string ModelVersion { get; set; }
	}
}
