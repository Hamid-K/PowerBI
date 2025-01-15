using System;
using System.Collections.ObjectModel;

namespace Microsoft.Spatial
{
	// Token: 0x02000051 RID: 81
	internal static class GeographyHelperMethods
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000541C File Offset: 0x0000361C
		internal static void SendFigure(this GeographyLineString lineString, GeographyPipeline pipeline)
		{
			ReadOnlyCollection<GeographyPoint> points = lineString.Points;
			for (int i = 0; i < points.Count; i++)
			{
				if (i == 0)
				{
					pipeline.BeginFigure(new GeographyPosition(points[i].Latitude, points[i].Longitude, points[i].Z, points[i].M));
				}
				else
				{
					pipeline.LineTo(new GeographyPosition(points[i].Latitude, points[i].Longitude, points[i].Z, points[i].M));
				}
			}
			if (points.Count > 0)
			{
				pipeline.EndFigure();
			}
		}
	}
}
