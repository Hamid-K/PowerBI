using System;
using System.Collections.ObjectModel;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005A RID: 90
	internal static class GeographyHelperMethods
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00006770 File Offset: 0x00004970
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
