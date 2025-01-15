using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DF RID: 223
	public sealed class OlapInfo
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0002EA88 File Offset: 0x0002CC88
		internal OlapInfo(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0002EA97 File Offset: 0x0002CC97
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

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0002EAB8 File Offset: 0x0002CCB8
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002EAD9 File Offset: 0x0002CCD9
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

		// Token: 0x040007F3 RID: 2035
		private MDDatasetFormatter formatter;

		// Token: 0x040007F4 RID: 2036
		private CubeInfo theCubeInfo;

		// Token: 0x040007F5 RID: 2037
		private AxesInfo theAxesInfo;

		// Token: 0x040007F6 RID: 2038
		private CellInfo theCellInfo;
	}
}
