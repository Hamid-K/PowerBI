using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000014 RID: 20
	internal sealed class ModelInfo
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002F3F File Offset: 0x0000113F
		public ModelInfo(string id, IASConnectionInfo asConnectionInfo, string modelMetadata)
		{
			this.Id = id;
			this.ModelMetaData = modelMetadata;
			this._asConnectionInfo = asConnectionInfo;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002F5C File Offset: 0x0000115C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002F64 File Offset: 0x00001164
		public string Id { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002F6D File Offset: 0x0000116D
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002F75 File Offset: 0x00001175
		public string ModelMetaData { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002F7E File Offset: 0x0000117E
		public ModelLocation ModelLocation
		{
			get
			{
				return this._asConnectionInfo.ModelLocation;
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly IASConnectionInfo _asConnectionInfo;
	}
}
