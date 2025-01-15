using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200000F RID: 15
	public static class GeographyFactory
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000031CC File Offset: 0x000013CC
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeographyFactory<GeographyPoint> Point(CoordinateSystem coordinateSystem, double latitude, double longitude, double? z, double? m)
		{
			return new GeographyFactory<GeographyPoint>(coordinateSystem).Point(latitude, longitude, z, m);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000031DE File Offset: 0x000013DE
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeographyFactory<GeographyPoint> Point(double latitude, double longitude, double? z, double? m)
		{
			return GeographyFactory.Point(CoordinateSystem.DefaultGeography, latitude, longitude, z, m);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000031F0 File Offset: 0x000013F0
		public static GeographyFactory<GeographyPoint> Point(CoordinateSystem coordinateSystem, double latitude, double longitude)
		{
			return GeographyFactory.Point(coordinateSystem, latitude, longitude, null, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003218 File Offset: 0x00001418
		public static GeographyFactory<GeographyPoint> Point(double latitude, double longitude)
		{
			return GeographyFactory.Point(CoordinateSystem.DefaultGeography, latitude, longitude, null, null);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003243 File Offset: 0x00001443
		public static GeographyFactory<GeographyPoint> Point(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyPoint>(coordinateSystem).Point();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003250 File Offset: 0x00001450
		public static GeographyFactory<GeographyPoint> Point()
		{
			return GeographyFactory.Point(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000325C File Offset: 0x0000145C
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeographyFactory<GeographyMultiPoint> MultiPoint(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyMultiPoint>(coordinateSystem).MultiPoint();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003269 File Offset: 0x00001469
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeographyFactory<GeographyMultiPoint> MultiPoint()
		{
			return GeographyFactory.MultiPoint(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003275 File Offset: 0x00001475
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeographyFactory<GeographyLineString> LineString(CoordinateSystem coordinateSystem, double latitude, double longitude, double? z, double? m)
		{
			return new GeographyFactory<GeographyLineString>(coordinateSystem).LineString(latitude, longitude, z, m);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003287 File Offset: 0x00001487
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeographyFactory<GeographyLineString> LineString(double latitude, double longitude, double? z, double? m)
		{
			return GeographyFactory.LineString(CoordinateSystem.DefaultGeography, latitude, longitude, z, m);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003298 File Offset: 0x00001498
		public static GeographyFactory<GeographyLineString> LineString(CoordinateSystem coordinateSystem, double latitude, double longitude)
		{
			return GeographyFactory.LineString(coordinateSystem, latitude, longitude, null, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000032C0 File Offset: 0x000014C0
		public static GeographyFactory<GeographyLineString> LineString(double latitude, double longitude)
		{
			return GeographyFactory.LineString(CoordinateSystem.DefaultGeography, latitude, longitude, null, null);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000032EB File Offset: 0x000014EB
		public static GeographyFactory<GeographyLineString> LineString(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyLineString>(coordinateSystem).LineString();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000032F8 File Offset: 0x000014F8
		public static GeographyFactory<GeographyLineString> LineString()
		{
			return GeographyFactory.LineString(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003304 File Offset: 0x00001504
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeographyFactory<GeographyMultiLineString> MultiLineString(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyMultiLineString>(coordinateSystem).MultiLineString();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003311 File Offset: 0x00001511
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeographyFactory<GeographyMultiLineString> MultiLineString()
		{
			return GeographyFactory.MultiLineString(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000331D File Offset: 0x0000151D
		public static GeographyFactory<GeographyPolygon> Polygon(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyPolygon>(coordinateSystem).Polygon();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000332A File Offset: 0x0000152A
		public static GeographyFactory<GeographyPolygon> Polygon()
		{
			return GeographyFactory.Polygon(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003336 File Offset: 0x00001536
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeographyFactory<GeographyMultiPolygon> MultiPolygon(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyMultiPolygon>(coordinateSystem).MultiPolygon();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003343 File Offset: 0x00001543
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public static GeographyFactory<GeographyMultiPolygon> MultiPolygon()
		{
			return GeographyFactory.MultiPolygon(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000334F File Offset: 0x0000154F
		public static GeographyFactory<GeographyCollection> Collection(CoordinateSystem coordinateSystem)
		{
			return new GeographyFactory<GeographyCollection>(coordinateSystem).Collection();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000335C File Offset: 0x0000155C
		public static GeographyFactory<GeographyCollection> Collection()
		{
			return GeographyFactory.Collection(CoordinateSystem.DefaultGeography);
		}
	}
}
