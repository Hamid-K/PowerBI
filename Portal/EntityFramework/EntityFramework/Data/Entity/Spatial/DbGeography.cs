using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000091 RID: 145
	[DataContract]
	[Serializable]
	public class DbGeography
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00011976 File Offset: 0x0000FB76
		internal DbGeography()
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001197E File Offset: 0x0000FB7E
		internal DbGeography(DbSpatialServices spatialServices, object spatialProviderValue)
		{
			this._spatialProvider = spatialServices;
			this._providerValue = spatialProviderValue;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00011994 File Offset: 0x0000FB94
		public static int DefaultCoordinateSystemId
		{
			get
			{
				return 4326;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001199B File Offset: 0x0000FB9B
		public object ProviderValue
		{
			get
			{
				return this._providerValue;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000119A3 File Offset: 0x0000FBA3
		public virtual DbSpatialServices Provider
		{
			get
			{
				return this._spatialProvider;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000119AB File Offset: 0x0000FBAB
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x000119BC File Offset: 0x0000FBBC
		[DataMember(Name = "Geography")]
		public DbGeographyWellKnownValue WellKnownValue
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

		// Token: 0x060004DB RID: 1243 RVA: 0x000119F6 File Offset: 0x0000FBF6
		public static DbGeography FromBinary(byte[] wellKnownBinary)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeographyFromBinary(wellKnownBinary);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00011A0F File Offset: 0x0000FC0F
		public static DbGeography FromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeographyFromBinary(wellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00011A29 File Offset: 0x0000FC29
		public static DbGeography LineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(lineWellKnownBinary, "lineWellKnownBinary");
			return DbSpatialServices.Default.GeographyLineFromBinary(lineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00011A43 File Offset: 0x0000FC43
		public static DbGeography PointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(pointWellKnownBinary, "pointWellKnownBinary");
			return DbSpatialServices.Default.GeographyPointFromBinary(pointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00011A5D File Offset: 0x0000FC5D
		public static DbGeography PolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(polygonWellKnownBinary, "polygonWellKnownBinary");
			return DbSpatialServices.Default.GeographyPolygonFromBinary(polygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00011A77 File Offset: 0x0000FC77
		public static DbGeography MultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiLineFromBinary(multiLineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011A91 File Offset: 0x0000FC91
		public static DbGeography MultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiPointFromBinary(multiPointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00011AAB File Offset: 0x0000FCAB
		public static DbGeography MultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiPolygonFromBinary(multiPolygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011AC5 File Offset: 0x0000FCC5
		public static DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(geographyCollectionWellKnownBinary, "geographyCollectionWellKnownBinary");
			return DbSpatialServices.Default.GeographyCollectionFromBinary(geographyCollectionWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00011ADF File Offset: 0x0000FCDF
		public static DbGeography FromGml(string geographyMarkup)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			return DbSpatialServices.Default.GeographyFromGml(geographyMarkup);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		public static DbGeography FromGml(string geographyMarkup, int coordinateSystemId)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			return DbSpatialServices.Default.GeographyFromGml(geographyMarkup, coordinateSystemId);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00011B12 File Offset: 0x0000FD12
		public static DbGeography FromText(string wellKnownText)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeographyFromText(wellKnownText);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00011B2B File Offset: 0x0000FD2B
		public static DbGeography FromText(string wellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeographyFromText(wellKnownText, coordinateSystemId);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00011B45 File Offset: 0x0000FD45
		public static DbGeography LineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(lineWellKnownText, "lineWellKnownText");
			return DbSpatialServices.Default.GeographyLineFromText(lineWellKnownText, coordinateSystemId);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00011B5F File Offset: 0x0000FD5F
		public static DbGeography PointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(pointWellKnownText, "pointWellKnownText");
			return DbSpatialServices.Default.GeographyPointFromText(pointWellKnownText, coordinateSystemId);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00011B79 File Offset: 0x0000FD79
		public static DbGeography PolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(polygonWellKnownText, "polygonWellKnownText");
			return DbSpatialServices.Default.GeographyPolygonFromText(polygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00011B93 File Offset: 0x0000FD93
		public static DbGeography MultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiLineWellKnownText, "multiLineWellKnownText");
			return DbSpatialServices.Default.GeographyMultiLineFromText(multiLineWellKnownText, coordinateSystemId);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011BAD File Offset: 0x0000FDAD
		public static DbGeography MultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPointWellKnownText, "multiPointWellKnownText");
			return DbSpatialServices.Default.GeographyMultiPointFromText(multiPointWellKnownText, coordinateSystemId);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00011BC7 File Offset: 0x0000FDC7
		public static DbGeography MultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			return DbSpatialServices.Default.GeographyMultiPolygonFromText(multiPolygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00011BE1 File Offset: 0x0000FDE1
		public static DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			return DbSpatialServices.Default.GeographyCollectionFromText(geographyCollectionWellKnownText, coordinateSystemId);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00011BFB File Offset: 0x0000FDFB
		public int CoordinateSystemId
		{
			get
			{
				return this._spatialProvider.GetCoordinateSystemId(this);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00011C09 File Offset: 0x0000FE09
		public int Dimension
		{
			get
			{
				return this._spatialProvider.GetDimension(this);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00011C17 File Offset: 0x0000FE17
		public string SpatialTypeName
		{
			get
			{
				return this._spatialProvider.GetSpatialTypeName(this);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00011C25 File Offset: 0x0000FE25
		public bool IsEmpty
		{
			get
			{
				return this._spatialProvider.GetIsEmpty(this);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011C33 File Offset: 0x0000FE33
		public virtual string AsText()
		{
			return this._spatialProvider.AsText(this);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00011C41 File Offset: 0x0000FE41
		internal string AsTextIncludingElevationAndMeasure()
		{
			return this._spatialProvider.AsTextIncludingElevationAndMeasure(this);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00011C4F File Offset: 0x0000FE4F
		public byte[] AsBinary()
		{
			return this._spatialProvider.AsBinary(this);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011C5D File Offset: 0x0000FE5D
		public string AsGml()
		{
			return this._spatialProvider.AsGml(this);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011C6B File Offset: 0x0000FE6B
		public bool SpatialEquals(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.SpatialEquals(this, other);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00011C86 File Offset: 0x0000FE86
		public bool Disjoint(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Disjoint(this, other);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011CA1 File Offset: 0x0000FEA1
		public bool Intersects(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Intersects(this, other);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public DbGeography Buffer(double? distance)
		{
			Check.NotNull<double>(distance, "distance");
			return this._spatialProvider.Buffer(this, distance.Value);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00011CDD File Offset: 0x0000FEDD
		public double? Distance(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return new double?(this._spatialProvider.Distance(this, other));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00011CFD File Offset: 0x0000FEFD
		public DbGeography Intersection(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Intersection(this, other);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00011D18 File Offset: 0x0000FF18
		public DbGeography Union(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Union(this, other);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00011D33 File Offset: 0x0000FF33
		public DbGeography Difference(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Difference(this, other);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00011D4E File Offset: 0x0000FF4E
		public DbGeography SymmetricDifference(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.SymmetricDifference(this, other);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00011D69 File Offset: 0x0000FF69
		public int? ElementCount
		{
			get
			{
				return this._spatialProvider.GetElementCount(this);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00011D77 File Offset: 0x0000FF77
		public DbGeography ElementAt(int index)
		{
			return this._spatialProvider.ElementAt(this, index);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00011D86 File Offset: 0x0000FF86
		public double? Latitude
		{
			get
			{
				return this._spatialProvider.GetLatitude(this);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00011D94 File Offset: 0x0000FF94
		public double? Longitude
		{
			get
			{
				return this._spatialProvider.GetLongitude(this);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00011DA2 File Offset: 0x0000FFA2
		public double? Elevation
		{
			get
			{
				return this._spatialProvider.GetElevation(this);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public double? Measure
		{
			get
			{
				return this._spatialProvider.GetMeasure(this);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011DBE File Offset: 0x0000FFBE
		public double? Length
		{
			get
			{
				return this._spatialProvider.GetLength(this);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00011DCC File Offset: 0x0000FFCC
		public DbGeography StartPoint
		{
			get
			{
				return this._spatialProvider.GetStartPoint(this);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00011DDA File Offset: 0x0000FFDA
		public DbGeography EndPoint
		{
			get
			{
				return this._spatialProvider.GetEndPoint(this);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		public bool? IsClosed
		{
			get
			{
				return this._spatialProvider.GetIsClosed(this);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00011DF6 File Offset: 0x0000FFF6
		public int? PointCount
		{
			get
			{
				return this._spatialProvider.GetPointCount(this);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00011E04 File Offset: 0x00010004
		public DbGeography PointAt(int index)
		{
			return this._spatialProvider.PointAt(this, index);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00011E13 File Offset: 0x00010013
		public double? Area
		{
			get
			{
				return this._spatialProvider.GetArea(this);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00011E21 File Offset: 0x00010021
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SRID={1};{0}", new object[]
			{
				this.WellKnownValue.WellKnownText ?? base.ToString(),
				this.CoordinateSystemId
			});
		}

		// Token: 0x0400011F RID: 287
		private DbSpatialServices _spatialProvider;

		// Token: 0x04000120 RID: 288
		private object _providerValue;
	}
}
