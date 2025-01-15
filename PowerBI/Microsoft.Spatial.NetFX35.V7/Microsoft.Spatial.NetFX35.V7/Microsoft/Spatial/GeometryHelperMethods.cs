using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200005B RID: 91
	internal static class GeometryHelperMethods
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00005974 File Offset: 0x00003B74
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
