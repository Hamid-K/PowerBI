using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000093 RID: 147
	[DataContract]
	[Serializable]
	public class DbGeometry
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00011E99 File Offset: 0x00010099
		internal DbGeometry()
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00011EA1 File Offset: 0x000100A1
		internal DbGeometry(DbSpatialServices spatialServices, object spatialProviderValue)
		{
			this._spatialProvider = spatialServices;
			this._providerValue = spatialProviderValue;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00011EB7 File Offset: 0x000100B7
		public static int DefaultCoordinateSystemId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011EBA File Offset: 0x000100BA
		public object ProviderValue
		{
			get
			{
				return this._providerValue;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00011EC2 File Offset: 0x000100C2
		public virtual DbSpatialServices Provider
		{
			get
			{
				return this._spatialProvider;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00011ECA File Offset: 0x000100CA
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00011ED8 File Offset: 0x000100D8
		[DataMember(Name = "Geometry")]
		public DbGeometryWellKnownValue WellKnownValue
		{
			get
			{
				return this._spatialProvider.CreateWellKnownValue(this);
			}
			set
			{
				if (this._spatialProvider != null)
				{
					throw new InvalidOperationException(Strings.Spatial_WellKnownValueSerializationPropertyNotDirectlySettable);
				}
				DbSpatialServices @default = DbSpatialServices.Default;
				this._providerValue = @default.CreateProviderValue(value);
				this._spatialProvider = @default;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00011F12 File Offset: 0x00010112
		public static DbGeometry FromBinary(byte[] wellKnownBinary)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeometryFromBinary(wellKnownBinary);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011F2B File Offset: 0x0001012B
		public static DbGeometry FromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeometryFromBinary(wellKnownBinary, coordinateSystemId);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011F45 File Offset: 0x00010145
		public static DbGeometry LineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(lineWellKnownBinary, "lineWellKnownBinary");
			return DbSpatialServices.Default.GeometryLineFromBinary(lineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011F5F File Offset: 0x0001015F
		public static DbGeometry PointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(pointWellKnownBinary, "pointWellKnownBinary");
			return DbSpatialServices.Default.GeometryPointFromBinary(pointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00011F79 File Offset: 0x00010179
		public static DbGeometry PolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(polygonWellKnownBinary, "polygonWellKnownBinary");
			return DbSpatialServices.Default.GeometryPolygonFromBinary(polygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00011F93 File Offset: 0x00010193
		public static DbGeometry MultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiLineFromBinary(multiLineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00011FAD File Offset: 0x000101AD
		public static DbGeometry MultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiPointFromBinary(multiPointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00011FC7 File Offset: 0x000101C7
		public static DbGeometry MultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiPolygonFromBinary(multiPolygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00011FE1 File Offset: 0x000101E1
		public static DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(geometryCollectionWellKnownBinary, "geometryCollectionWellKnownBinary");
			return DbSpatialServices.Default.GeometryCollectionFromBinary(geometryCollectionWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00011FFB File Offset: 0x000101FB
		public static DbGeometry FromGml(string geometryMarkup)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			return DbSpatialServices.Default.GeometryFromGml(geometryMarkup);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00012014 File Offset: 0x00010214
		public static DbGeometry FromGml(string geometryMarkup, int coordinateSystemId)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			return DbSpatialServices.Default.GeometryFromGml(geometryMarkup, coordinateSystemId);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001202E File Offset: 0x0001022E
		public static DbGeometry FromText(string wellKnownText)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeometryFromText(wellKnownText);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00012047 File Offset: 0x00010247
		public static DbGeometry FromText(string wellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeometryFromText(wellKnownText, coordinateSystemId);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012061 File Offset: 0x00010261
		public static DbGeometry LineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(lineWellKnownText, "lineWellKnownText");
			return DbSpatialServices.Default.GeometryLineFromText(lineWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001207B File Offset: 0x0001027B
		public static DbGeometry PointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(pointWellKnownText, "pointWellKnownText");
			return DbSpatialServices.Default.GeometryPointFromText(pointWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00012095 File Offset: 0x00010295
		public static DbGeometry PolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(polygonWellKnownText, "polygonWellKnownText");
			return DbSpatialServices.Default.GeometryPolygonFromText(polygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000120AF File Offset: 0x000102AF
		public static DbGeometry MultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiLineWellKnownText, "multiLineWellKnownText");
			return DbSpatialServices.Default.GeometryMultiLineFromText(multiLineWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000120C9 File Offset: 0x000102C9
		public static DbGeometry MultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPointWellKnownText, "multiPointWellKnownText");
			return DbSpatialServices.Default.GeometryMultiPointFromText(multiPointWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000120E3 File Offset: 0x000102E3
		public static DbGeometry MultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			return DbSpatialServices.Default.GeometryMultiPolygonFromText(multiPolygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000120FD File Offset: 0x000102FD
		public static DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			return DbSpatialServices.Default.GeometryCollectionFromText(geometryCollectionWellKnownText, coordinateSystemId);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00012117 File Offset: 0x00010317
		public int CoordinateSystemId
		{
			get
			{
				return this._spatialProvider.GetCoordinateSystemId(this);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00012125 File Offset: 0x00010325
		public DbGeometry Boundary
		{
			get
			{
				return this._spatialProvider.GetBoundary(this);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00012133 File Offset: 0x00010333
		public int Dimension
		{
			get
			{
				return this._spatialProvider.GetDimension(this);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00012141 File Offset: 0x00010341
		public DbGeometry Envelope
		{
			get
			{
				return this._spatialProvider.GetEnvelope(this);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001214F File Offset: 0x0001034F
		public string SpatialTypeName
		{
			get
			{
				return this._spatialProvider.GetSpatialTypeName(this);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001215D File Offset: 0x0001035D
		public bool IsEmpty
		{
			get
			{
				return this._spatialProvider.GetIsEmpty(this);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001216B File Offset: 0x0001036B
		public bool IsSimple
		{
			get
			{
				return this._spatialProvider.GetIsSimple(this);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00012179 File Offset: 0x00010379
		public bool IsValid
		{
			get
			{
				return this._spatialProvider.GetIsValid(this);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00012187 File Offset: 0x00010387
		public virtual string AsText()
		{
			return this._spatialProvider.AsText(this);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00012195 File Offset: 0x00010395
		internal string AsTextIncludingElevationAndMeasure()
		{
			return this._spatialProvider.AsTextIncludingElevationAndMeasure(this);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000121A3 File Offset: 0x000103A3
		public byte[] AsBinary()
		{
			return this._spatialProvider.AsBinary(this);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000121B1 File Offset: 0x000103B1
		public string AsGml()
		{
			return this._spatialProvider.AsGml(this);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000121BF File Offset: 0x000103BF
		public bool SpatialEquals(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.SpatialEquals(this, other);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000121DA File Offset: 0x000103DA
		public bool Disjoint(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Disjoint(this, other);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000121F5 File Offset: 0x000103F5
		public bool Intersects(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Intersects(this, other);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00012210 File Offset: 0x00010410
		public bool Touches(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Touches(this, other);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001222B File Offset: 0x0001042B
		public bool Crosses(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Crosses(this, other);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00012246 File Offset: 0x00010446
		public bool Within(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Within(this, other);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00012261 File Offset: 0x00010461
		public bool Contains(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Contains(this, other);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001227C File Offset: 0x0001047C
		public bool Overlaps(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Overlaps(this, other);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00012297 File Offset: 0x00010497
		public bool Relate(DbGeometry other, string matrix)
		{
			Check.NotNull<DbGeometry>(other, "other");
			Check.NotNull<string>(matrix, "matrix");
			return this._spatialProvider.Relate(this, other, matrix);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000122BF File Offset: 0x000104BF
		public DbGeometry Buffer(double? distance)
		{
			Check.NotNull<double>(distance, "distance");
			return this._spatialProvider.Buffer(this, distance.Value);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000122E0 File Offset: 0x000104E0
		public double? Distance(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return new double?(this._spatialProvider.Distance(this, other));
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00012300 File Offset: 0x00010500
		public DbGeometry ConvexHull
		{
			get
			{
				return this._spatialProvider.GetConvexHull(this);
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001230E File Offset: 0x0001050E
		public DbGeometry Intersection(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Intersection(this, other);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00012329 File Offset: 0x00010529
		public DbGeometry Union(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Union(this, other);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00012344 File Offset: 0x00010544
		public DbGeometry Difference(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Difference(this, other);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001235F File Offset: 0x0001055F
		public DbGeometry SymmetricDifference(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.SymmetricDifference(this, other);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001237A File Offset: 0x0001057A
		public int? ElementCount
		{
			get
			{
				return this._spatialProvider.GetElementCount(this);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00012388 File Offset: 0x00010588
		public DbGeometry ElementAt(int index)
		{
			return this._spatialProvider.ElementAt(this, index);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00012397 File Offset: 0x00010597
		public double? XCoordinate
		{
			get
			{
				return this._spatialProvider.GetXCoordinate(this);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000123A5 File Offset: 0x000105A5
		public double? YCoordinate
		{
			get
			{
				return this._spatialProvider.GetYCoordinate(this);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000123B3 File Offset: 0x000105B3
		public double? Elevation
		{
			get
			{
				return this._spatialProvider.GetElevation(this);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000123C1 File Offset: 0x000105C1
		public double? Measure
		{
			get
			{
				return this._spatialProvider.GetMeasure(this);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x000123CF File Offset: 0x000105CF
		public double? Length
		{
			get
			{
				return this._spatialProvider.GetLength(this);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000123DD File Offset: 0x000105DD
		public DbGeometry StartPoint
		{
			get
			{
				return this._spatialProvider.GetStartPoint(this);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000123EB File Offset: 0x000105EB
		public DbGeometry EndPoint
		{
			get
			{
				return this._spatialProvider.GetEndPoint(this);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000123F9 File Offset: 0x000105F9
		public bool? IsClosed
		{
			get
			{
				return this._spatialProvider.GetIsClosed(this);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00012407 File Offset: 0x00010607
		public bool? IsRing
		{
			get
			{
				return this._spatialProvider.GetIsRing(this);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00012415 File Offset: 0x00010615
		public int? PointCount
		{
			get
			{
				return this._spatialProvider.GetPointCount(this);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00012423 File Offset: 0x00010623
		public DbGeometry PointAt(int index)
		{
			return this._spatialProvider.PointAt(this, index);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00012432 File Offset: 0x00010632
		public double? Area
		{
			get
			{
				return this._spatialProvider.GetArea(this);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00012440 File Offset: 0x00010640
		public DbGeometry Centroid
		{
			get
			{
				return this._spatialProvider.GetCentroid(this);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001244E File Offset: 0x0001064E
		public DbGeometry PointOnSurface
		{
			get
			{
				return this._spatialProvider.GetPointOnSurface(this);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001245C File Offset: 0x0001065C
		public DbGeometry ExteriorRing
		{
			get
			{
				return this._spatialProvider.GetExteriorRing(this);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001246A File Offset: 0x0001066A
		public int? InteriorRingCount
		{
			get
			{
				return this._spatialProvider.GetInteriorRingCount(this);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012478 File Offset: 0x00010678
		public DbGeometry InteriorRingAt(int index)
		{
			return this._spatialProvider.InteriorRingAt(this, index);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012487 File Offset: 0x00010687
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SRID={1};{0}", new object[]
			{
				this.WellKnownValue.WellKnownText ?? base.ToString(),
				this.CoordinateSystemId
			});
		}

		// Token: 0x04000124 RID: 292
		private DbSpatialServices _spatialProvider;

		// Token: 0x04000125 RID: 293
		private object _providerValue;
	}
}
