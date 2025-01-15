using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x0200001C RID: 28
	internal sealed class PbixPortalWireContract
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003397 File Offset: 0x00001597
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000339F File Offset: 0x0000159F
		public PbixPortalWireContract.PbixProperties Properties { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000033A8 File Offset: 0x000015A8
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000033B0 File Offset: 0x000015B0
		public IList<DataSource> DataSources { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000033B9 File Offset: 0x000015B9
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000033C1 File Offset: 0x000015C1
		public IList<DataModelRole> DataModelRoles { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000033CA File Offset: 0x000015CA
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000033D2 File Offset: 0x000015D2
		public IList<DataModelParameter> DataModelParameters { get; set; }

		// Token: 0x0200004C RID: 76
		internal sealed class PbixProperties
		{
			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000159 RID: 345 RVA: 0x000086CA File Offset: 0x000068CA
			// (set) Token: 0x0600015A RID: 346 RVA: 0x000086D2 File Offset: 0x000068D2
			public bool IsMobileOptimized { get; set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600015B RID: 347 RVA: 0x000086DB File Offset: 0x000068DB
			// (set) Token: 0x0600015C RID: 348 RVA: 0x000086E3 File Offset: 0x000068E3
			public bool HasEmbeddedModels { get; set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600015D RID: 349 RVA: 0x000086EC File Offset: 0x000068EC
			// (set) Token: 0x0600015E RID: 350 RVA: 0x000086F4 File Offset: 0x000068F4
			public double PbixShredderVersion { get; set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600015F RID: 351 RVA: 0x000086FD File Offset: 0x000068FD
			// (set) Token: 0x06000160 RID: 352 RVA: 0x00008705 File Offset: 0x00006905
			public bool ModelRefreshAllowed { get; set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000161 RID: 353 RVA: 0x0000870E File Offset: 0x0000690E
			// (set) Token: 0x06000162 RID: 354 RVA: 0x00008716 File Offset: 0x00006916
			public bool HasDirectQuery { get; set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000163 RID: 355 RVA: 0x0000871F File Offset: 0x0000691F
			// (set) Token: 0x06000164 RID: 356 RVA: 0x00008727 File Offset: 0x00006927
			public byte[] DataModel { get; set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00008730 File Offset: 0x00006930
			// (set) Token: 0x06000166 RID: 358 RVA: 0x00008738 File Offset: 0x00006938
			public string ModelVersion { get; set; }
		}
	}
}
