using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000064 RID: 100
	internal static class GeometryHelperMethods
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00006CD0 File Offset: 0x00004ED0
		internal static void SendFigure(this GeometryLineString GeometryLineString, GeometryPipeline pipeline)
		{
			Util.CheckArgumentNull(GeometryLineString, "GeometryLineString");
			for (int i = 0; i < GeometryLineString.Points.Count; i++)
			{
				GeometryPoint geometryPoint = GeometryLineString.Points[i];
				GeometryPosition geometryPosition = new GeometryPosition(geometryPoint.X, geometryPoint.Y, geometryPoint.Z, geometryPoint.M);
				if (i == 0)
				{
					pipeline.BeginFigure(geometryPosition);
				}
				else
				{
					pipeline.LineTo(geometryPosition);
				}
			}
			if (GeometryLineString.Points.Count > 0)
			{
				pipeline.EndFigure();
			}
		}
	}
}
