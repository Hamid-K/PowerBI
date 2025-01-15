using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	internal sealed class DefaultSpatialServices : DbSpatialServices
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00012614 File Offset: 0x00010814
		private DefaultSpatialServices()
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001261C File Offset: 0x0001081C
		private static Exception SpatialServicesUnavailable()
		{
			return new NotImplementedException(Strings.SpatialProviderNotUsable);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00012628 File Offset: 0x00010828
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckProviderValue(object providerValue)
		{
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = providerValue as DefaultSpatialServices.ReadOnlySpatialValues;
			if (readOnlySpatialValues == null)
			{
				throw new ArgumentException(Strings.Spatial_ProviderValueNotCompatibleWithSpatialServices, "providerValue");
			}
			return readOnlySpatialValues;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00012644 File Offset: 0x00010844
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckCompatible(DbGeography geographyValue)
		{
			if (geographyValue != null)
			{
				DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = geographyValue.ProviderValue as DefaultSpatialServices.ReadOnlySpatialValues;
				if (readOnlySpatialValues != null)
				{
					return readOnlySpatialValues;
				}
			}
			throw new ArgumentException(Strings.Spatial_GeographyValueNotCompatibleWithSpatialServices, "geographyValue");
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00012674 File Offset: 0x00010874
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckCompatible(DbGeometry geometryValue)
		{
			if (geometryValue != null)
			{
				DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = geometryValue.ProviderValue as DefaultSpatialServices.ReadOnlySpatialValues;
				if (readOnlySpatialValues != null)
				{
					return readOnlySpatialValues;
				}
			}
			throw new ArgumentException(Strings.Spatial_GeometryValueNotCompatibleWithSpatialServices, "geometryValue");
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000126A4 File Offset: 0x000108A4
		public override DbGeography GeographyFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckProviderValue(providerValue);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000126CB File Offset: 0x000108CB
		public override object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeographyWellKnownValue>(wellKnownValue, "wellKnownValue");
			return new DefaultSpatialServices.ReadOnlySpatialValues(wellKnownValue.CoordinateSystemId, wellKnownValue.WellKnownText, wellKnownValue.WellKnownBinary, null);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000126F4 File Offset: 0x000108F4
		public override DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return new DbGeographyWellKnownValue
			{
				CoordinateSystemId = readOnlySpatialValues.CoordinateSystemId,
				WellKnownBinary = readOnlySpatialValues.CloneBinary(),
				WellKnownText = readOnlySpatialValues.Text
			};
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00012740 File Offset: 0x00010940
		public override DbGeography GeographyFromBinary(byte[] geographyBinary)
		{
			Check.NotNull<byte[]>(geographyBinary, "geographyBinary");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, null, geographyBinary, null);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00012770 File Offset: 0x00010970
		public override DbGeography GeographyFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			Check.NotNull<byte[]>(geographyBinary, "geographyBinary");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, geographyBinary, null);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001279A File Offset: 0x0001099A
		public override DbGeography GeographyLineFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000127A1 File Offset: 0x000109A1
		public override DbGeography GeographyPointFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000127A8 File Offset: 0x000109A8
		public override DbGeography GeographyPolygonFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000127AF File Offset: 0x000109AF
		public override DbGeography GeographyMultiLineFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000127B6 File Offset: 0x000109B6
		public override DbGeography GeographyMultiPointFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000127BD File Offset: 0x000109BD
		public override DbGeography GeographyMultiPolygonFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000127C4 File Offset: 0x000109C4
		public override DbGeography GeographyCollectionFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000127CC File Offset: 0x000109CC
		public override DbGeography GeographyFromText(string geographyText)
		{
			Check.NotNull<string>(geographyText, "geographyText");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, geographyText, null, null);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000127FC File Offset: 0x000109FC
		public override DbGeography GeographyFromText(string geographyText, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geographyText, "geographyText");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, geographyText, null, null);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00012826 File Offset: 0x00010A26
		public override DbGeography GeographyLineFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001282D File Offset: 0x00010A2D
		public override DbGeography GeographyPointFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00012834 File Offset: 0x00010A34
		public override DbGeography GeographyPolygonFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001283B File Offset: 0x00010A3B
		public override DbGeography GeographyMultiLineFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00012842 File Offset: 0x00010A42
		public override DbGeography GeographyMultiPointFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00012849 File Offset: 0x00010A49
		public override DbGeography GeographyMultiPolygonFromText(string multiPolygonKnownText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00012850 File Offset: 0x00010A50
		public override DbGeography GeographyCollectionFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00012858 File Offset: 0x00010A58
		public override DbGeography GeographyFromGml(string geographyMarkup)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, null, null, geographyMarkup);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00012888 File Offset: 0x00010A88
		public override DbGeography GeographyFromGml(string geographyMarkup, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, null, geographyMarkup);
			return DbSpatialServices.CreateGeography(this, readOnlySpatialValues);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000128B2 File Offset: 0x00010AB2
		public override int GetCoordinateSystemId(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return DefaultSpatialServices.CheckCompatible(geographyValue).CoordinateSystemId;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000128CB File Offset: 0x00010ACB
		public override int GetDimension(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000128D2 File Offset: 0x00010AD2
		public override string GetSpatialTypeName(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000128D9 File Offset: 0x00010AD9
		public override bool GetIsEmpty(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000128E0 File Offset: 0x00010AE0
		public override string AsText(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return DefaultSpatialServices.CheckCompatible(geographyValue).Text;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000128F9 File Offset: 0x00010AF9
		public override byte[] AsBinary(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return DefaultSpatialServices.CheckCompatible(geographyValue).CloneBinary();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00012912 File Offset: 0x00010B12
		public override string AsGml(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return DefaultSpatialServices.CheckCompatible(geographyValue).GML;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001292B File Offset: 0x00010B2B
		public override bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00012932 File Offset: 0x00010B32
		public override bool Disjoint(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00012939 File Offset: 0x00010B39
		public override bool Intersects(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00012940 File Offset: 0x00010B40
		public override DbGeography Buffer(DbGeography geographyValue, double distance)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00012947 File Offset: 0x00010B47
		public override double Distance(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001294E File Offset: 0x00010B4E
		public override DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00012955 File Offset: 0x00010B55
		public override DbGeography Union(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001295C File Offset: 0x00010B5C
		public override DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00012963 File Offset: 0x00010B63
		public override DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001296A File Offset: 0x00010B6A
		public override int? GetElementCount(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00012971 File Offset: 0x00010B71
		public override DbGeography ElementAt(DbGeography geographyValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00012978 File Offset: 0x00010B78
		public override double? GetLatitude(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001297F File Offset: 0x00010B7F
		public override double? GetLongitude(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00012986 File Offset: 0x00010B86
		public override double? GetElevation(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001298D File Offset: 0x00010B8D
		public override double? GetMeasure(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00012994 File Offset: 0x00010B94
		public override double? GetLength(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001299B File Offset: 0x00010B9B
		public override DbGeography GetEndPoint(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000129A2 File Offset: 0x00010BA2
		public override DbGeography GetStartPoint(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000129A9 File Offset: 0x00010BA9
		public override bool? GetIsClosed(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000129B0 File Offset: 0x00010BB0
		public override int? GetPointCount(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000129B7 File Offset: 0x00010BB7
		public override DbGeography PointAt(DbGeography geographyValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000129BE File Offset: 0x00010BBE
		public override double? GetArea(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000129C5 File Offset: 0x00010BC5
		public override object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeometryWellKnownValue>(wellKnownValue, "wellKnownValue");
			return new DefaultSpatialServices.ReadOnlySpatialValues(wellKnownValue.CoordinateSystemId, wellKnownValue.WellKnownText, wellKnownValue.WellKnownBinary, null);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000129EC File Offset: 0x00010BEC
		public override DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return new DbGeometryWellKnownValue
			{
				CoordinateSystemId = readOnlySpatialValues.CoordinateSystemId,
				WellKnownBinary = readOnlySpatialValues.CloneBinary(),
				WellKnownText = readOnlySpatialValues.Text
			};
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00012A38 File Offset: 0x00010C38
		public override DbGeometry GeometryFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckProviderValue(providerValue);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00012A60 File Offset: 0x00010C60
		public override DbGeometry GeometryFromBinary(byte[] geometryBinary)
		{
			Check.NotNull<byte[]>(geometryBinary, "geometryBinary");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, null, geometryBinary, null);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00012A90 File Offset: 0x00010C90
		public override DbGeometry GeometryFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			Check.NotNull<byte[]>(geometryBinary, "geometryBinary");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, geometryBinary, null);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00012ABA File Offset: 0x00010CBA
		public override DbGeometry GeometryLineFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00012AC1 File Offset: 0x00010CC1
		public override DbGeometry GeometryPointFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00012AC8 File Offset: 0x00010CC8
		public override DbGeometry GeometryPolygonFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00012ACF File Offset: 0x00010CCF
		public override DbGeometry GeometryMultiLineFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00012AD6 File Offset: 0x00010CD6
		public override DbGeometry GeometryMultiPointFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00012ADD File Offset: 0x00010CDD
		public override DbGeometry GeometryMultiPolygonFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00012AE4 File Offset: 0x00010CE4
		public override DbGeometry GeometryCollectionFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00012AEC File Offset: 0x00010CEC
		public override DbGeometry GeometryFromText(string geometryText)
		{
			Check.NotNull<string>(geometryText, "geometryText");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, geometryText, null, null);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00012B1C File Offset: 0x00010D1C
		public override DbGeometry GeometryFromText(string geometryText, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geometryText, "geometryText");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, geometryText, null, null);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00012B46 File Offset: 0x00010D46
		public override DbGeometry GeometryLineFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00012B4D File Offset: 0x00010D4D
		public override DbGeometry GeometryPointFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00012B54 File Offset: 0x00010D54
		public override DbGeometry GeometryPolygonFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00012B5B File Offset: 0x00010D5B
		public override DbGeometry GeometryMultiLineFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00012B62 File Offset: 0x00010D62
		public override DbGeometry GeometryMultiPointFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00012B69 File Offset: 0x00010D69
		public override DbGeometry GeometryMultiPolygonFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00012B70 File Offset: 0x00010D70
		public override DbGeometry GeometryCollectionFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00012B78 File Offset: 0x00010D78
		public override DbGeometry GeometryFromGml(string geometryMarkup)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, null, null, geometryMarkup);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00012BA8 File Offset: 0x00010DA8
		public override DbGeometry GeometryFromGml(string geometryMarkup, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, null, geometryMarkup);
			return DbSpatialServices.CreateGeometry(this, readOnlySpatialValues);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00012BD2 File Offset: 0x00010DD2
		public override int GetCoordinateSystemId(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return DefaultSpatialServices.CheckCompatible(geometryValue).CoordinateSystemId;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00012BEB File Offset: 0x00010DEB
		public override DbGeometry GetBoundary(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00012BF2 File Offset: 0x00010DF2
		public override int GetDimension(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00012BF9 File Offset: 0x00010DF9
		public override DbGeometry GetEnvelope(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00012C00 File Offset: 0x00010E00
		public override string GetSpatialTypeName(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00012C07 File Offset: 0x00010E07
		public override bool GetIsEmpty(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00012C0E File Offset: 0x00010E0E
		public override bool GetIsSimple(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00012C15 File Offset: 0x00010E15
		public override bool GetIsValid(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00012C1C File Offset: 0x00010E1C
		public override string AsText(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return DefaultSpatialServices.CheckCompatible(geometryValue).Text;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00012C35 File Offset: 0x00010E35
		public override byte[] AsBinary(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return DefaultSpatialServices.CheckCompatible(geometryValue).CloneBinary();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00012C4E File Offset: 0x00010E4E
		public override string AsGml(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return DefaultSpatialServices.CheckCompatible(geometryValue).GML;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00012C67 File Offset: 0x00010E67
		public override bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00012C6E File Offset: 0x00010E6E
		public override bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00012C75 File Offset: 0x00010E75
		public override bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00012C7C File Offset: 0x00010E7C
		public override bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00012C83 File Offset: 0x00010E83
		public override bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00012C8A File Offset: 0x00010E8A
		public override bool Within(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00012C91 File Offset: 0x00010E91
		public override bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00012C98 File Offset: 0x00010E98
		public override bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00012C9F File Offset: 0x00010E9F
		public override bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00012CA6 File Offset: 0x00010EA6
		public override DbGeometry Buffer(DbGeometry geometryValue, double distance)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00012CAD File Offset: 0x00010EAD
		public override double Distance(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00012CB4 File Offset: 0x00010EB4
		public override DbGeometry GetConvexHull(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00012CBB File Offset: 0x00010EBB
		public override DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00012CC2 File Offset: 0x00010EC2
		public override DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00012CC9 File Offset: 0x00010EC9
		public override DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public override DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00012CD7 File Offset: 0x00010ED7
		public override int? GetElementCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00012CDE File Offset: 0x00010EDE
		public override DbGeometry ElementAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00012CE5 File Offset: 0x00010EE5
		public override double? GetXCoordinate(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00012CEC File Offset: 0x00010EEC
		public override double? GetYCoordinate(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00012CF3 File Offset: 0x00010EF3
		public override double? GetElevation(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00012CFA File Offset: 0x00010EFA
		public override double? GetMeasure(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00012D01 File Offset: 0x00010F01
		public override double? GetLength(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00012D08 File Offset: 0x00010F08
		public override DbGeometry GetEndPoint(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00012D0F File Offset: 0x00010F0F
		public override DbGeometry GetStartPoint(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00012D16 File Offset: 0x00010F16
		public override bool? GetIsClosed(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00012D1D File Offset: 0x00010F1D
		public override bool? GetIsRing(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00012D24 File Offset: 0x00010F24
		public override int? GetPointCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00012D2B File Offset: 0x00010F2B
		public override DbGeometry PointAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00012D32 File Offset: 0x00010F32
		public override double? GetArea(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00012D39 File Offset: 0x00010F39
		public override DbGeometry GetCentroid(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00012D40 File Offset: 0x00010F40
		public override DbGeometry GetPointOnSurface(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00012D47 File Offset: 0x00010F47
		public override DbGeometry GetExteriorRing(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00012D4E File Offset: 0x00010F4E
		public override int? GetInteriorRingCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00012D55 File Offset: 0x00010F55
		public override DbGeometry InteriorRingAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x0400012A RID: 298
		internal static readonly DefaultSpatialServices Instance = new DefaultSpatialServices();

		// Token: 0x0200072F RID: 1839
		[Serializable]
		private sealed class ReadOnlySpatialValues
		{
			// Token: 0x06005552 RID: 21842 RVA: 0x00131A20 File Offset: 0x0012FC20
			internal ReadOnlySpatialValues(int spatialRefSysId, string textValue, byte[] binaryValue, string gmlValue)
			{
				this.srid = spatialRefSysId;
				this.wkb = ((binaryValue == null) ? null : ((byte[])binaryValue.Clone()));
				this.wkt = textValue;
				this.gml = gmlValue;
			}

			// Token: 0x17001012 RID: 4114
			// (get) Token: 0x06005553 RID: 21843 RVA: 0x00131A55 File Offset: 0x0012FC55
			internal int CoordinateSystemId
			{
				get
				{
					return this.srid;
				}
			}

			// Token: 0x06005554 RID: 21844 RVA: 0x00131A5D File Offset: 0x0012FC5D
			internal byte[] CloneBinary()
			{
				if (this.wkb != null)
				{
					return (byte[])this.wkb.Clone();
				}
				return null;
			}

			// Token: 0x17001013 RID: 4115
			// (get) Token: 0x06005555 RID: 21845 RVA: 0x00131A79 File Offset: 0x0012FC79
			internal string Text
			{
				get
				{
					return this.wkt;
				}
			}

			// Token: 0x17001014 RID: 4116
			// (get) Token: 0x06005556 RID: 21846 RVA: 0x00131A81 File Offset: 0x0012FC81
			internal string GML
			{
				get
				{
					return this.gml;
				}
			}

			// Token: 0x04001E9C RID: 7836
			private readonly int srid;

			// Token: 0x04001E9D RID: 7837
			private readonly byte[] wkb;

			// Token: 0x04001E9E RID: 7838
			private readonly string wkt;

			// Token: 0x04001E9F RID: 7839
			private readonly string gml;
		}
	}
}
