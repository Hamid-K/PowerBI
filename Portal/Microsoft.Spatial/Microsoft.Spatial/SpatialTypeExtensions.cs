using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000038 RID: 56
	public static class SpatialTypeExtensions
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000480E File Offset: 0x00002A0E
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
