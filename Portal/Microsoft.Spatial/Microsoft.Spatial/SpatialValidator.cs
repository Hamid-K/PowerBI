using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200003F RID: 63
	public static class SpatialValidator
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00005056 File Offset: 0x00003256
		public static SpatialPipeline Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateValidator();
		}
	}
}
