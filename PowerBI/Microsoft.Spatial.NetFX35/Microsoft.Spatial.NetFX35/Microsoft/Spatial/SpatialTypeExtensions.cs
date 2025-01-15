using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000038 RID: 56
	public static class SpatialTypeExtensions
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000045A0 File Offset: 0x000027A0
		public static void SendTo(this ISpatial shape, SpatialPipeline destination)
		{
			if (shape == null)
			{
				return;
			}
			if (shape.GetType().IsSubclassOf(typeof(Geometry)))
			{
				((Geometry)shape).SendTo(destination);
				return;
			}
			((Geography)shape).SendTo(destination);
		}
	}
}
