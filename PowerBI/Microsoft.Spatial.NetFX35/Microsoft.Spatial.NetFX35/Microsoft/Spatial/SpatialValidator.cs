using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000040 RID: 64
	public static class SpatialValidator
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00004E3D File Offset: 0x0000303D
		public static SpatialPipeline Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateValidator();
		}
	}
}
