using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E2 RID: 226
	public sealed class OlapInfoCube
	{
		// Token: 0x06000C76 RID: 3190 RVA: 0x0002ED1C File Offset: 0x0002CF1C
		internal OlapInfoCube(DataRow cubeInfo)
		{
			this.cubeInfo = cubeInfo;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002ED2B File Offset: 0x0002CF2B
		public string CubeName
		{
			get
			{
				return this.cubeInfo["CubeName"].ToString();
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002ED42 File Offset: 0x0002CF42
		public DateTime LastDataUpdate
		{
			get
			{
				if (this.cubeInfo["LastDataUpdate"] is DBNull)
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				return (DateTime)this.cubeInfo["LastDataUpdate"];
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002ED7B File Offset: 0x0002CF7B
		public DateTime LastSchemaUpdate
		{
			get
			{
				if (this.cubeInfo["LastSchemaUpdate"] is DBNull)
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				return (DateTime)this.cubeInfo["LastSchemaUpdate"];
			}
		}

		// Token: 0x040007FC RID: 2044
		private DataRow cubeInfo;
	}
}
