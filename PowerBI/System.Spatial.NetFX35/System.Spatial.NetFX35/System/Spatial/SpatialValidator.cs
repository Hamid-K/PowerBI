using System;

namespace System.Spatial
{
	// Token: 0x0200003E RID: 62
	public static class SpatialValidator
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00004D25 File Offset: 0x00002F25
		public static SpatialPipeline Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateValidator();
		}
	}
}
