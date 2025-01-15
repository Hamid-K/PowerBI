using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public abstract class DbSpatialServices
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00012598 File Offset: 0x00010798
		public static DbSpatialServices Default
		{
			get
			{
				return DbSpatialServices._defaultServices.Value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000125A4 File Offset: 0x000107A4
		public virtual bool NativeTypesAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000125A7 File Offset: 0x000107A7
		protected static DbGeography CreateGeography(DbSpatialServices spatialServices, object providerValue)
		{
			Check.NotNull<DbSpatialServices>(spatialServices, "spatialServices");
			Check.NotNull<object>(providerValue, "providerValue");
			return new DbGeography(spatialServices, providerValue);
		}

		// Token: 0x06000571 RID: 1393
		public abstract DbGeography GeographyFromProviderValue(object providerValue);

		// Token: 0x06000572 RID: 1394
		public abstract object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue);

		// Token: 0x06000573 RID: 1395
		public abstract DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue);

		// Token: 0x06000574 RID: 1396
		public abstract DbGeography GeographyFromBinary(byte[] wellKnownBinary);

		// Token: 0x06000575 RID: 1397
		public abstract DbGeography GeographyFromBinary(byte[] wellKnownBinary, int coordinateSystemId);

		// Token: 0x06000576 RID: 1398
		public abstract DbGeography GeographyLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06000577 RID: 1399
		public abstract DbGeography GeographyPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06000578 RID: 1400
		public abstract DbGeography GeographyPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06000579 RID: 1401
		public abstract DbGeography GeographyMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId);

		// Token: 0x0600057A RID: 1402
		public abstract DbGeography GeographyMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId);

		// Token: 0x0600057B RID: 1403
		public abstract DbGeography GeographyMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x0600057C RID: 1404
		public abstract DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId);

		// Token: 0x0600057D RID: 1405
		public abstract DbGeography GeographyFromText(string wellKnownText);

		// Token: 0x0600057E RID: 1406
		public abstract DbGeography GeographyFromText(string wellKnownText, int coordinateSystemId);

		// Token: 0x0600057F RID: 1407
		public abstract DbGeography GeographyLineFromText(string lineWellKnownText, int coordinateSystemId);

		// Token: 0x06000580 RID: 1408
		public abstract DbGeography GeographyPointFromText(string pointWellKnownText, int coordinateSystemId);

		// Token: 0x06000581 RID: 1409
		public abstract DbGeography GeographyPolygonFromText(string polygonWellKnownText, int coordinateSystemId);

		// Token: 0x06000582 RID: 1410
		public abstract DbGeography GeographyMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId);

		// Token: 0x06000583 RID: 1411
		public abstract DbGeography GeographyMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId);

		// Token: 0x06000584 RID: 1412
		public abstract DbGeography GeographyMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId);

		// Token: 0x06000585 RID: 1413
		public abstract DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId);

		// Token: 0x06000586 RID: 1414
		public abstract DbGeography GeographyFromGml(string geographyMarkup);

		// Token: 0x06000587 RID: 1415
		public abstract DbGeography GeographyFromGml(string geographyMarkup, int coordinateSystemId);

		// Token: 0x06000588 RID: 1416
		public abstract int GetCoordinateSystemId(DbGeography geographyValue);

		// Token: 0x06000589 RID: 1417
		public abstract int GetDimension(DbGeography geographyValue);

		// Token: 0x0600058A RID: 1418
		public abstract string GetSpatialTypeName(DbGeography geographyValue);

		// Token: 0x0600058B RID: 1419
		public abstract bool GetIsEmpty(DbGeography geographyValue);

		// Token: 0x0600058C RID: 1420
		public abstract string AsText(DbGeography geographyValue);

		// Token: 0x0600058D RID: 1421 RVA: 0x000125C8 File Offset: 0x000107C8
		public virtual string AsTextIncludingElevationAndMeasure(DbGeography geographyValue)
		{
			return null;
		}

		// Token: 0x0600058E RID: 1422
		public abstract byte[] AsBinary(DbGeography geographyValue);

		// Token: 0x0600058F RID: 1423
		public abstract string AsGml(DbGeography geographyValue);

		// Token: 0x06000590 RID: 1424
		public abstract bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000591 RID: 1425
		public abstract bool Disjoint(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000592 RID: 1426
		public abstract bool Intersects(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000593 RID: 1427
		public abstract DbGeography Buffer(DbGeography geographyValue, double distance);

		// Token: 0x06000594 RID: 1428
		public abstract double Distance(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000595 RID: 1429
		public abstract DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000596 RID: 1430
		public abstract DbGeography Union(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000597 RID: 1431
		public abstract DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000598 RID: 1432
		public abstract DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06000599 RID: 1433
		public abstract int? GetElementCount(DbGeography geographyValue);

		// Token: 0x0600059A RID: 1434
		public abstract DbGeography ElementAt(DbGeography geographyValue, int index);

		// Token: 0x0600059B RID: 1435
		public abstract double? GetLatitude(DbGeography geographyValue);

		// Token: 0x0600059C RID: 1436
		public abstract double? GetLongitude(DbGeography geographyValue);

		// Token: 0x0600059D RID: 1437
		public abstract double? GetElevation(DbGeography geographyValue);

		// Token: 0x0600059E RID: 1438
		public abstract double? GetMeasure(DbGeography geographyValue);

		// Token: 0x0600059F RID: 1439
		public abstract double? GetLength(DbGeography geographyValue);

		// Token: 0x060005A0 RID: 1440
		public abstract DbGeography GetStartPoint(DbGeography geographyValue);

		// Token: 0x060005A1 RID: 1441
		public abstract DbGeography GetEndPoint(DbGeography geographyValue);

		// Token: 0x060005A2 RID: 1442
		public abstract bool? GetIsClosed(DbGeography geographyValue);

		// Token: 0x060005A3 RID: 1443
		public abstract int? GetPointCount(DbGeography geographyValue);

		// Token: 0x060005A4 RID: 1444
		public abstract DbGeography PointAt(DbGeography geographyValue, int index);

		// Token: 0x060005A5 RID: 1445
		public abstract double? GetArea(DbGeography geographyValue);

		// Token: 0x060005A6 RID: 1446 RVA: 0x000125CB File Offset: 0x000107CB
		protected static DbGeometry CreateGeometry(DbSpatialServices spatialServices, object providerValue)
		{
			Check.NotNull<DbSpatialServices>(spatialServices, "spatialServices");
			Check.NotNull<object>(providerValue, "providerValue");
			return new DbGeometry(spatialServices, providerValue);
		}

		// Token: 0x060005A7 RID: 1447
		public abstract object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue);

		// Token: 0x060005A8 RID: 1448
		public abstract DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue);

		// Token: 0x060005A9 RID: 1449
		public abstract DbGeometry GeometryFromProviderValue(object providerValue);

		// Token: 0x060005AA RID: 1450
		public abstract DbGeometry GeometryFromBinary(byte[] wellKnownBinary);

		// Token: 0x060005AB RID: 1451
		public abstract DbGeometry GeometryFromBinary(byte[] wellKnownBinary, int coordinateSystemId);

		// Token: 0x060005AC RID: 1452
		public abstract DbGeometry GeometryLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005AD RID: 1453
		public abstract DbGeometry GeometryPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005AE RID: 1454
		public abstract DbGeometry GeometryPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005AF RID: 1455
		public abstract DbGeometry GeometryMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005B0 RID: 1456
		public abstract DbGeometry GeometryMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005B1 RID: 1457
		public abstract DbGeometry GeometryMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005B2 RID: 1458
		public abstract DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId);

		// Token: 0x060005B3 RID: 1459
		public abstract DbGeometry GeometryFromText(string wellKnownText);

		// Token: 0x060005B4 RID: 1460
		public abstract DbGeometry GeometryFromText(string wellKnownText, int coordinateSystemId);

		// Token: 0x060005B5 RID: 1461
		public abstract DbGeometry GeometryLineFromText(string lineWellKnownText, int coordinateSystemId);

		// Token: 0x060005B6 RID: 1462
		public abstract DbGeometry GeometryPointFromText(string pointWellKnownText, int coordinateSystemId);

		// Token: 0x060005B7 RID: 1463
		public abstract DbGeometry GeometryPolygonFromText(string polygonWellKnownText, int coordinateSystemId);

		// Token: 0x060005B8 RID: 1464
		public abstract DbGeometry GeometryMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId);

		// Token: 0x060005B9 RID: 1465
		public abstract DbGeometry GeometryMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId);

		// Token: 0x060005BA RID: 1466
		public abstract DbGeometry GeometryMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId);

		// Token: 0x060005BB RID: 1467
		public abstract DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId);

		// Token: 0x060005BC RID: 1468
		public abstract DbGeometry GeometryFromGml(string geometryMarkup);

		// Token: 0x060005BD RID: 1469
		public abstract DbGeometry GeometryFromGml(string geometryMarkup, int coordinateSystemId);

		// Token: 0x060005BE RID: 1470
		public abstract int GetCoordinateSystemId(DbGeometry geometryValue);

		// Token: 0x060005BF RID: 1471
		public abstract DbGeometry GetBoundary(DbGeometry geometryValue);

		// Token: 0x060005C0 RID: 1472
		public abstract int GetDimension(DbGeometry geometryValue);

		// Token: 0x060005C1 RID: 1473
		public abstract DbGeometry GetEnvelope(DbGeometry geometryValue);

		// Token: 0x060005C2 RID: 1474
		public abstract string GetSpatialTypeName(DbGeometry geometryValue);

		// Token: 0x060005C3 RID: 1475
		public abstract bool GetIsEmpty(DbGeometry geometryValue);

		// Token: 0x060005C4 RID: 1476
		public abstract bool GetIsSimple(DbGeometry geometryValue);

		// Token: 0x060005C5 RID: 1477
		public abstract bool GetIsValid(DbGeometry geometryValue);

		// Token: 0x060005C6 RID: 1478
		public abstract string AsText(DbGeometry geometryValue);

		// Token: 0x060005C7 RID: 1479 RVA: 0x000125EC File Offset: 0x000107EC
		public virtual string AsTextIncludingElevationAndMeasure(DbGeometry geometryValue)
		{
			return null;
		}

		// Token: 0x060005C8 RID: 1480
		public abstract byte[] AsBinary(DbGeometry geometryValue);

		// Token: 0x060005C9 RID: 1481
		public abstract string AsGml(DbGeometry geometryValue);

		// Token: 0x060005CA RID: 1482
		public abstract bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005CB RID: 1483
		public abstract bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005CC RID: 1484
		public abstract bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005CD RID: 1485
		public abstract bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005CE RID: 1486
		public abstract bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005CF RID: 1487
		public abstract bool Within(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D0 RID: 1488
		public abstract bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D1 RID: 1489
		public abstract bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D2 RID: 1490
		public abstract bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix);

		// Token: 0x060005D3 RID: 1491
		public abstract DbGeometry Buffer(DbGeometry geometryValue, double distance);

		// Token: 0x060005D4 RID: 1492
		public abstract double Distance(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D5 RID: 1493
		public abstract DbGeometry GetConvexHull(DbGeometry geometryValue);

		// Token: 0x060005D6 RID: 1494
		public abstract DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D7 RID: 1495
		public abstract DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D8 RID: 1496
		public abstract DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005D9 RID: 1497
		public abstract DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x060005DA RID: 1498
		public abstract int? GetElementCount(DbGeometry geometryValue);

		// Token: 0x060005DB RID: 1499
		public abstract DbGeometry ElementAt(DbGeometry geometryValue, int index);

		// Token: 0x060005DC RID: 1500
		public abstract double? GetXCoordinate(DbGeometry geometryValue);

		// Token: 0x060005DD RID: 1501
		public abstract double? GetYCoordinate(DbGeometry geometryValue);

		// Token: 0x060005DE RID: 1502
		public abstract double? GetElevation(DbGeometry geometryValue);

		// Token: 0x060005DF RID: 1503
		public abstract double? GetMeasure(DbGeometry geometryValue);

		// Token: 0x060005E0 RID: 1504
		public abstract double? GetLength(DbGeometry geometryValue);

		// Token: 0x060005E1 RID: 1505
		public abstract DbGeometry GetStartPoint(DbGeometry geometryValue);

		// Token: 0x060005E2 RID: 1506
		public abstract DbGeometry GetEndPoint(DbGeometry geometryValue);

		// Token: 0x060005E3 RID: 1507
		public abstract bool? GetIsClosed(DbGeometry geometryValue);

		// Token: 0x060005E4 RID: 1508
		public abstract bool? GetIsRing(DbGeometry geometryValue);

		// Token: 0x060005E5 RID: 1509
		public abstract int? GetPointCount(DbGeometry geometryValue);

		// Token: 0x060005E6 RID: 1510
		public abstract DbGeometry PointAt(DbGeometry geometryValue, int index);

		// Token: 0x060005E7 RID: 1511
		public abstract double? GetArea(DbGeometry geometryValue);

		// Token: 0x060005E8 RID: 1512
		public abstract DbGeometry GetCentroid(DbGeometry geometryValue);

		// Token: 0x060005E9 RID: 1513
		public abstract DbGeometry GetPointOnSurface(DbGeometry geometryValue);

		// Token: 0x060005EA RID: 1514
		public abstract DbGeometry GetExteriorRing(DbGeometry geometryValue);

		// Token: 0x060005EB RID: 1515
		public abstract int? GetInteriorRingCount(DbGeometry geometryValue);

		// Token: 0x060005EC RID: 1516
		public abstract DbGeometry InteriorRingAt(DbGeometry geometryValue, int index);

		// Token: 0x04000129 RID: 297
		private static readonly Lazy<DbSpatialServices> _defaultServices = new Lazy<DbSpatialServices>(() => new SpatialServicesLoader(DbConfiguration.DependencyResolver).LoadDefaultServices(), true);
	}
}
