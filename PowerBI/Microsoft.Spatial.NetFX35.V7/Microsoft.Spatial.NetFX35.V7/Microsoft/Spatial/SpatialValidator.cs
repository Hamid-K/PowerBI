using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200003A RID: 58
	public static class SpatialValidator
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00004382 File Offset: 0x00002582
		public static SpatialPipeline Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateValidator();
		}
	}
}
