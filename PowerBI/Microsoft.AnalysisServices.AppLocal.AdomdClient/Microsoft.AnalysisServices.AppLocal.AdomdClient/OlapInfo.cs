using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DF RID: 223
	public sealed class OlapInfo
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
		internal OlapInfo(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002EDC7 File Offset: 0x0002CFC7
		public CubeInfo CubeInfo
		{
			get
			{
				if (this.theCubeInfo == null)
				{
					this.theCubeInfo = new CubeInfo(this.formatter);
				}
				return this.theCubeInfo;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002EDE8 File Offset: 0x0002CFE8
		public AxesInfo AxesInfo
		{
			get
			{
				if (this.theAxesInfo == null)
				{
					this.theAxesInfo = new AxesInfo(this.formatter);
				}
				return this.theAxesInfo;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002EE09 File Offset: 0x0002D009
		public CellInfo CellInfo
		{
			get
			{
				if (this.theCellInfo == null)
				{
					this.theCellInfo = new CellInfo(this.formatter);
				}
				return this.theCellInfo;
			}
		}

		// Token: 0x04000800 RID: 2048
		private MDDatasetFormatter formatter;

		// Token: 0x04000801 RID: 2049
		private CubeInfo theCubeInfo;

		// Token: 0x04000802 RID: 2050
		private AxesInfo theAxesInfo;

		// Token: 0x04000803 RID: 2051
		private CellInfo theCellInfo;
	}
}
