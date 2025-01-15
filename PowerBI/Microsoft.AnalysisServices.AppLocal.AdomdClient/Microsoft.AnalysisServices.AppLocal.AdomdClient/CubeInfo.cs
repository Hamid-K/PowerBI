using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007F RID: 127
	public sealed class CubeInfo
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x00026488 File Offset: 0x00024688
		internal CubeInfo(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
			this.theCubes = null;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002649E File Offset: 0x0002469E
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

		// Token: 0x0400056D RID: 1389
		private MDDatasetFormatter formatter;

		// Token: 0x0400056E RID: 1390
		private OlapInfoCubeCollection theCubes;
	}
}
