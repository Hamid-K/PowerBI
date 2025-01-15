using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000060 RID: 96
	internal static class GeometryHelperMethods
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000663C File Offset: 0x0000483C
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
