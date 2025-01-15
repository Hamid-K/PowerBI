using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000033 RID: 51
	public static class SpatialTypeExtensions
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00003B42 File Offset: 0x00001D42
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
