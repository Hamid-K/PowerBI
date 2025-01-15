using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000021 RID: 33
	public static class GeometryFactory
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00003B90 File Offset: 0x00001D90
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryPoint> Point(CoordinateSystem coordinateSystem, double x, double y, double? z, double? m)
		{
			return new GeometryFactory<GeometryPoint>(coordinateSystem).Point(x, y, z, m);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003BA2 File Offset: 0x00001DA2
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryPoint> Point(double x, double y, double? z, double? m)
		{
			return GeometryFactory.Point(CoordinateSystem.DefaultGeometry, x, y, z, m);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003BB4 File Offset: 0x00001DB4
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryPoint> Point(CoordinateSystem coordinateSystem, double x, double y)
		{
			return GeometryFactory.Point(coordinateSystem, x, y, null, null);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003BDC File Offset: 0x00001DDC
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryPoint> Point(double x, double y)
		{
			return GeometryFactory.Point(CoordinateSystem.DefaultGeometry, x, y, null, null);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003C07 File Offset: 0x00001E07
		public static GeometryFactory<GeometryPoint> Point(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryPoint>(coordinateSystem).Point();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003C14 File Offset: 0x00001E14
		public static GeometryFactory<GeometryPoint> Point()
		{
			return GeometryFactory.Point(CoordinateSystem.DefaultGeometry);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003C20 File Offset: 0x00001E20
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeometryFactory<GeometryMultiPoint> MultiPoint(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryMultiPoint>(coordinateSystem).MultiPoint();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003C2D File Offset: 0x00001E2D
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeometryFactory<GeometryMultiPoint> MultiPoint()
		{
			return GeometryFactory.MultiPoint(CoordinateSystem.DefaultGeometry);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003C39 File Offset: 0x00001E39
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryLineString> LineString(CoordinateSystem coordinateSystem, double x, double y, double? z, double? m)
		{
			return new GeometryFactory<GeometryLineString>(coordinateSystem).LineString(x, y, z, m);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003C4B File Offset: 0x00001E4B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryLineString> LineString(double x, double y, double? z, double? m)
		{
			return GeometryFactory.LineString(CoordinateSystem.DefaultGeometry, x, y, z, m);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003C5C File Offset: 0x00001E5C
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryLineString> LineString(CoordinateSystem coordinateSystem, double x, double y)
		{
			return GeometryFactory.LineString(coordinateSystem, x, y, null, null);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003C84 File Offset: 0x00001E84
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryFactory<GeometryLineString> LineString(double x, double y)
		{
			return GeometryFactory.LineString(CoordinateSystem.DefaultGeometry, x, y, null, null);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003CAF File Offset: 0x00001EAF
		public static GeometryFactory<GeometryLineString> LineString(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryLineString>(coordinateSystem).LineString();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003CBC File Offset: 0x00001EBC
		public static GeometryFactory<GeometryLineString> LineString()
		{
			return GeometryFactory.LineString(CoordinateSystem.DefaultGeometry);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003CC8 File Offset: 0x00001EC8
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeometryFactory<GeometryMultiLineString> MultiLineString(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryMultiLineString>(coordinateSystem).MultiLineString();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003CD5 File Offset: 0x00001ED5
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeometryFactory<GeometryMultiLineString> MultiLineString()
		{
			return GeometryFactory.MultiLineString(CoordinateSystem.DefaultGeometry);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public static GeometryFactory<GeometryPolygon> Polygon(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryPolygon>(coordinateSystem).Polygon();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003CEE File Offset: 0x00001EEE
		public static GeometryFactory<GeometryPolygon> Polygon()
		{
			return GeometryFactory.Polygon(CoordinateSystem.DefaultGeometry);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003CFA File Offset: 0x00001EFA
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeometryFactory<GeometryMultiPolygon> MultiPolygon(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryMultiPolygon>(coordinateSystem).MultiPolygon();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003D07 File Offset: 0x00001F07
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeometryFactory<GeometryMultiPolygon> MultiPolygon()
		{
			return GeometryFactory.MultiPolygon(CoordinateSystem.DefaultGeometry);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003D13 File Offset: 0x00001F13
		public static GeometryFactory<GeometryCollection> Collection(CoordinateSystem coordinateSystem)
		{
			return new GeometryFactory<GeometryCollection>(coordinateSystem).Collection();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003D20 File Offset: 0x00001F20
		public static GeometryFactory<GeometryCollection> Collection()
		{
			return GeometryFactory.Collection(CoordinateSystem.DefaultGeometry);
		}
	}
}
