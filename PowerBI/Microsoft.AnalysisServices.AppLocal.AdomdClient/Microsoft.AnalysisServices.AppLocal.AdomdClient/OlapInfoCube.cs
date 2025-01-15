using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E2 RID: 226
	public sealed class OlapInfoCube
	{
		// Token: 0x06000C83 RID: 3203 RVA: 0x0002F04C File Offset: 0x0002D24C
		internal OlapInfoCube(DataRow cubeInfo)
		{
			this.cubeInfo = cubeInfo;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002F05B File Offset: 0x0002D25B
		public string CubeName
		{
			get
			{
				return this.cubeInfo["CubeName"].ToString();
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002F072 File Offset: 0x0002D272
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

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002F0AB File Offset: 0x0002D2AB
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

		// Token: 0x04000809 RID: 2057
		private DataRow cubeInfo;
	}
}
