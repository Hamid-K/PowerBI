using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007F RID: 127
	public sealed class CubeInfo
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00026158 File Offset: 0x00024358
		internal CubeInfo(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
			this.theCubes = null;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002616E File Offset: 0x0002436E
		public OlapInfoCubeCollection Cubes
		{
			get
			{
				if (this.theCubes == null)
				{
					this.theCubes = new OlapInfoCubeCollection(this.formatter);
				}
				return this.theCubes;
			}
		}

		// Token: 0x04000560 RID: 1376
		private MDDatasetFormatter formatter;

		// Token: 0x04000561 RID: 1377
		private OlapInfoCubeCollection theCubes;
	}
}
