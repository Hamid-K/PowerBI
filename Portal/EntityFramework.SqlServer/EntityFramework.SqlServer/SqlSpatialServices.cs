using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class SqlSpatialServices : DbSpatialServices
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000939B File Offset: 0x0000759B
		internal SqlSpatialServices()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000093A3 File Offset: 0x000075A3
		internal SqlSpatialServices(SqlTypesAssemblyLoader loader)
		{
			this._loader = loader;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000093B2 File Offset: 0x000075B2
		public override bool NativeTypesAvailable
		{
			get
			{
				return (this._loader ?? SqlTypesAssemblyLoader.DefaultInstance).TryGetSqlTypesAssembly() != null;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000093CC File Offset: 0x000075CC
		private static bool TryGetSpatialServiceFromAssembly(Assembly assembly, out SqlSpatialServices services)
		{
			if (SqlSpatialServices._otherSpatialServices == null || !SqlSpatialServices._otherSpatialServices.TryGetValue(assembly.FullName, out services))
			{
				SqlSpatialServices instance = SqlSpatialServices.Instance;
				lock (instance)
				{
					if (SqlSpatialServices._otherSpatialServices == null || !SqlSpatialServices._otherSpatialServices.TryGetValue(assembly.FullName, out services))
					{
						SqlTypesAssembly sqlTypesAssembly;
						if (SqlTypesAssemblyLoader.DefaultInstance.TryGetSqlTypesAssembly(assembly, out sqlTypesAssembly))
						{
							if (SqlSpatialServices._otherSpatialServices == null)
							{
								SqlSpatialServices._otherSpatialServices = new Dictionary<string, SqlSpatialServices>(1);
							}
							services = new SqlSpatialServices(new SqlTypesAssemblyLoader(sqlTypesAssembly));
							SqlSpatialServices._otherSpatialServices.Add(assembly.FullName, services);
						}
						else
						{
							services = null;
						}
					}
				}
			}
			return services != null;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009484 File Offset: 0x00007684
		internal SqlTypesAssembly SqlTypes
		{
			get
			{
				return (this._loader ?? SqlTypesAssemblyLoader.DefaultInstance).GetSqlTypesAssembly();
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000949C File Offset: 0x0000769C
		public override object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeographyWellKnownValue>(wellKnownValue, "wellKnownValue");
			object obj;
			if (wellKnownValue.WellKnownText != null)
			{
				obj = this.SqlTypes.SqlTypesGeographyFromText(wellKnownValue.WellKnownText, wellKnownValue.CoordinateSystemId);
			}
			else
			{
				if (wellKnownValue.WellKnownBinary == null)
				{
					throw new ArgumentException(Strings.Spatial_WellKnownGeographyValueNotValid, "wellKnownValue");
				}
				obj = this.SqlTypes.SqlTypesGeographyFromBinary(wellKnownValue.WellKnownBinary, wellKnownValue.CoordinateSystemId);
			}
			return obj;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000950C File Offset: 0x0000770C
		public override DbGeography GeographyFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			object obj = this.NormalizeProviderValue(providerValue, this.SqlTypes.SqlGeographyType);
			if (!this.SqlTypes.IsSqlGeographyNull(obj))
			{
				return DbSpatialServices.CreateGeography(this, obj);
			}
			return null;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009550 File Offset: 0x00007750
		private object NormalizeProviderValue(object providerValue, Type expectedSpatialType)
		{
			Type type = providerValue.GetType();
			if (type != expectedSpatialType)
			{
				SqlSpatialServices sqlSpatialServices;
				if (SqlSpatialServices.TryGetSpatialServiceFromAssembly(providerValue.GetType().Assembly(), out sqlSpatialServices))
				{
					if (expectedSpatialType == this.SqlTypes.SqlGeographyType)
					{
						if (type == sqlSpatialServices.SqlTypes.SqlGeographyType)
						{
							return this.ConvertToSqlValue(sqlSpatialServices.GeographyFromProviderValue(providerValue), "providerValue");
						}
					}
					else if (type == sqlSpatialServices.SqlTypes.SqlGeometryType)
					{
						return this.ConvertToSqlValue(sqlSpatialServices.GeometryFromProviderValue(providerValue), "providerValue");
					}
				}
				throw new ArgumentException(Strings.SqlSpatialServices_ProviderValueNotSqlType(expectedSpatialType.AssemblyQualifiedName), "providerValue");
			}
			return providerValue;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000095FC File Offset: 0x000077FC
		public override DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return SqlSpatialServices.CreateWellKnownValue<DbGeographyWellKnownValue>(geographyValue.AsSpatialValue(), () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid, "geographyValue"), () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt, "geographyValue"), (int coordinateSystemId, byte[] wkb, string wkt) => new DbGeographyWellKnownValue
			{
				CoordinateSystemId = coordinateSystemId,
				WellKnownBinary = wkb,
				WellKnownText = wkt
			});
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009680 File Offset: 0x00007880
		public override object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeometryWellKnownValue>(wellKnownValue, "wellKnownValue");
			object obj;
			if (wellKnownValue.WellKnownText != null)
			{
				obj = this.SqlTypes.SqlTypesGeometryFromText(wellKnownValue.WellKnownText, wellKnownValue.CoordinateSystemId);
			}
			else
			{
				if (wellKnownValue.WellKnownBinary == null)
				{
					throw new ArgumentException(Strings.Spatial_WellKnownGeometryValueNotValid, "wellKnownValue");
				}
				obj = this.SqlTypes.SqlTypesGeometryFromBinary(wellKnownValue.WellKnownBinary, wellKnownValue.CoordinateSystemId);
			}
			return obj;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000096F0 File Offset: 0x000078F0
		public override DbGeometry GeometryFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			object obj = this.NormalizeProviderValue(providerValue, this.SqlTypes.SqlGeometryType);
			if (!this.SqlTypes.IsSqlGeometryNull(obj))
			{
				return DbSpatialServices.CreateGeometry(this, obj);
			}
			return null;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009734 File Offset: 0x00007934
		public override DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return SqlSpatialServices.CreateWellKnownValue<DbGeometryWellKnownValue>(geometryValue.AsSpatialValue(), () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid, "geometryValue"), () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt, "geometryValue"), (int coordinateSystemId, byte[] wkb, string wkt) => new DbGeometryWellKnownValue
			{
				CoordinateSystemId = coordinateSystemId,
				WellKnownBinary = wkb,
				WellKnownText = wkt
			});
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000097B8 File Offset: 0x000079B8
		private static TValue CreateWellKnownValue<TValue>(IDbSpatialValue spatialValue, Func<Exception> onMissingcoordinateSystemId, Func<Exception> onMissingWkbAndWkt, Func<int, byte[], string, TValue> onValidValue)
		{
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId == null)
			{
				throw onMissingcoordinateSystemId();
			}
			string wellKnownText = spatialValue.WellKnownText;
			if (wellKnownText != null)
			{
				return onValidValue(coordinateSystemId.Value, null, wellKnownText);
			}
			byte[] wellKnownBinary = spatialValue.WellKnownBinary;
			if (wellKnownBinary != null)
			{
				return onValidValue(coordinateSystemId.Value, wellKnownBinary, null);
			}
			throw onMissingWkbAndWkt();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009816 File Offset: 0x00007A16
		public override string AsTextIncludingElevationAndMeasure(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return this.SqlTypes.GeographyAsTextZM(geographyValue);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009830 File Offset: 0x00007A30
		public override string AsTextIncludingElevationAndMeasure(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return this.SqlTypes.GeometryAsTextZM(geometryValue);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000984A File Offset: 0x00007A4A
		private object ConvertToSqlValue(DbGeography geographyValue, string argumentName)
		{
			if (geographyValue == null)
			{
				return null;
			}
			return this.SqlTypes.ConvertToSqlTypesGeography(geographyValue);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000985D File Offset: 0x00007A5D
		private object ConvertToSqlValue(DbGeometry geometryValue, string argumentName)
		{
			if (geometryValue == null)
			{
				return null;
			}
			return this.SqlTypes.ConvertToSqlTypesGeometry(geometryValue);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009870 File Offset: 0x00007A70
		private object ConvertToSqlBytes(byte[] binaryValue, string argumentName)
		{
			if (binaryValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlBytesFromByteArray(binaryValue);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009883 File Offset: 0x00007A83
		private object ConvertToSqlChars(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlCharsFromString(stringValue);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009896 File Offset: 0x00007A96
		private object ConvertToSqlString(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlStringFromString(stringValue);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000098A9 File Offset: 0x00007AA9
		private object ConvertToSqlXml(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlXmlFromString(stringValue);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000098BC File Offset: 0x00007ABC
		private bool ConvertSqlBooleanToBoolean(object sqlBoolean)
		{
			return this.SqlTypes.SqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000098CA File Offset: 0x00007ACA
		private bool? ConvertSqlBooleanToNullableBoolean(object sqlBoolean)
		{
			return this.SqlTypes.SqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000098D8 File Offset: 0x00007AD8
		private byte[] ConvertSqlBytesToBinary(object sqlBytes)
		{
			return this.SqlTypes.SqlBytesToByteArray(sqlBytes);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000098E6 File Offset: 0x00007AE6
		private string ConvertSqlCharsToString(object sqlCharsValue)
		{
			return this.SqlTypes.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000098F4 File Offset: 0x00007AF4
		private string ConvertSqlStringToString(object sqlCharsValue)
		{
			return this.SqlTypes.SqlStringToString(sqlCharsValue);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009902 File Offset: 0x00007B02
		private double ConvertSqlDoubleToDouble(object sqlDoubleValue)
		{
			return this.SqlTypes.SqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009910 File Offset: 0x00007B10
		private double? ConvertSqlDoubleToNullableDouble(object sqlDoubleValue)
		{
			return this.SqlTypes.SqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000991E File Offset: 0x00007B1E
		private int ConvertSqlInt32ToInt(object sqlInt32Value)
		{
			return this.SqlTypes.SqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000992C File Offset: 0x00007B2C
		private int? ConvertSqlInt32ToNullableInt(object sqlInt32Value)
		{
			return this.SqlTypes.SqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000993A File Offset: 0x00007B3A
		private string ConvertSqlXmlToString(object sqlXmlValue)
		{
			return this.SqlTypes.SqlXmlToString(sqlXmlValue);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009948 File Offset: 0x00007B48
		public override DbGeography GeographyFromText(string wellKnownText)
		{
			object obj = this.ConvertToSqlString(wellKnownText, "wellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyParse.Value.Invoke(null, new object[] { obj });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000998C File Offset: 0x00007B8C
		public override DbGeography GeographyFromText(string wellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(wellKnownText, "wellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStGeomFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000099D8 File Offset: 0x00007BD8
		public override DbGeography GeographyPointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(pointWellKnownText, "pointWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStPointFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009A24 File Offset: 0x00007C24
		public override DbGeography GeographyLineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(lineWellKnownText, "lineWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStLineFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009A70 File Offset: 0x00007C70
		public override DbGeography GeographyPolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(polygonWellKnownText, "polygonWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStPolyFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009ABC File Offset: 0x00007CBC
		public override DbGeography GeographyMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPointWellKnownText, "multiPointWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStmPointFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009B08 File Offset: 0x00007D08
		public override DbGeography GeographyMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiLineWellKnownText, "multiLineWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStmLineFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009B54 File Offset: 0x00007D54
		public override DbGeography GeographyMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPolygonKnownText, "multiPolygonWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStmPolyFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009BA0 File Offset: 0x00007DA0
		public override DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeographyStGeomCollFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009BEC File Offset: 0x00007DEC
		public override DbGeography GeographyFromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStGeomFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009C38 File Offset: 0x00007E38
		public override DbGeography GeographyFromBinary(byte[] wellKnownBinary)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStGeomFromWkb.Value.Invoke(null, new object[] { obj, 4326 });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009C88 File Offset: 0x00007E88
		public override DbGeography GeographyPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(pointWellKnownBinary, "pointWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStPointFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public override DbGeography GeographyLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(lineWellKnownBinary, "lineWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStLineFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009D20 File Offset: 0x00007F20
		public override DbGeography GeographyPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(polygonWellKnownBinary, "polygonWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStPolyFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009D6C File Offset: 0x00007F6C
		public override DbGeography GeographyMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStmPointFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public override DbGeography GeographyMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStmLineFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009E04 File Offset: 0x00008004
		public override DbGeography GeographyMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStmPolyFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009E50 File Offset: 0x00008050
		public override DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(geographyCollectionWellKnownBinary, "geographyCollectionWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeographyStGeomCollFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009E9C File Offset: 0x0000809C
		public override DbGeography GeographyFromGml(string geographyMarkup)
		{
			object obj = this.ConvertToSqlXml(geographyMarkup, "geographyMarkup");
			object obj2 = this.SqlTypes.SmiSqlGeographyGeomFromGml.Value.Invoke(null, new object[] { obj, 4326 });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009EEC File Offset: 0x000080EC
		public override DbGeography GeographyFromGml(string geographyMarkup, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlXml(geographyMarkup, "geographyMarkup");
			object obj2 = this.SqlTypes.SmiSqlGeographyGeomFromGml.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009F38 File Offset: 0x00008138
		public override int GetCoordinateSystemId(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyStSrid.Value.GetValue(obj, null);
			return this.ConvertSqlInt32ToInt(value);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009F80 File Offset: 0x00008180
		public override string GetSpatialTypeName(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStGeometryType.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlStringToString(obj2);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009FCC File Offset: 0x000081CC
		public override int GetDimension(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStDimension.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToInt(obj2);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A018 File Offset: 0x00008218
		public override byte[] AsBinary(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStAsBinary.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBytesToBinary(obj2);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A064 File Offset: 0x00008264
		public override string AsGml(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyAsGml.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlXmlToString(obj2);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public override string AsText(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStAsText.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlCharsToString(obj2);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A0FC File Offset: 0x000082FC
		public override bool GetIsEmpty(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStIsEmpty.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(obj2);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A148 File Offset: 0x00008348
		public override bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStEquals.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A1A4 File Offset: 0x000083A4
		public override bool Disjoint(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStDisjoint.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A200 File Offset: 0x00008400
		public override bool Intersects(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStIntersects.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A25C File Offset: 0x0000845C
		public override DbGeography Buffer(DbGeography geographyValue, double distance)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStBuffer.Value.Invoke(obj, new object[] { distance });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public override double Distance(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStDistance.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlDoubleToDouble(obj3);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A30C File Offset: 0x0000850C
		public override DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStIntersection.Value.Invoke(obj, new object[] { obj2 });
			return this.GeographyFromProviderValue(obj3);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A368 File Offset: 0x00008568
		public override DbGeography Union(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStUnion.Value.Invoke(obj, new object[] { obj2 });
			return this.GeographyFromProviderValue(obj3);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A3C4 File Offset: 0x000085C4
		public override DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStDifference.Value.Invoke(obj, new object[] { obj2 });
			return this.GeographyFromProviderValue(obj3);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A420 File Offset: 0x00008620
		public override DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object obj3 = this.SqlTypes.ImiSqlGeographyStSymDifference.Value.Invoke(obj, new object[] { obj2 });
			return this.GeographyFromProviderValue(obj3);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A47C File Offset: 0x0000867C
		public override int? GetElementCount(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStNumGeometries.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(obj2);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public override DbGeography ElementAt(DbGeography geographyValue, int index)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStGeometryN.Value.Invoke(obj, new object[] { index });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A51C File Offset: 0x0000871C
		public override double? GetLatitude(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyLat.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A564 File Offset: 0x00008764
		public override double? GetLongitude(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyLong.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A5AC File Offset: 0x000087AC
		public override double? GetElevation(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyZ.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public override double? GetMeasure(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyM.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A63C File Offset: 0x0000883C
		public override double? GetLength(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStLength.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(obj2);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A688 File Offset: 0x00008888
		public override DbGeography GetStartPoint(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStStartPoint.Value.Invoke(obj, new object[0]);
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A6D4 File Offset: 0x000088D4
		public override DbGeography GetEndPoint(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStEndPoint.Value.Invoke(obj, new object[0]);
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A720 File Offset: 0x00008920
		public override bool? GetIsClosed(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStIsClosed.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(obj2);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A76C File Offset: 0x0000896C
		public override int? GetPointCount(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStNumPoints.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(obj2);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A7B8 File Offset: 0x000089B8
		public override DbGeography PointAt(DbGeography geographyValue, int index)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStPointN.Value.Invoke(obj, new object[] { index });
			return this.GeographyFromProviderValue(obj2);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A80C File Offset: 0x00008A0C
		public override double? GetArea(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.SqlTypes.ImiSqlGeographyStArea.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(obj2);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A858 File Offset: 0x00008A58
		public override DbGeometry GeometryFromText(string wellKnownText)
		{
			object obj = this.ConvertToSqlString(wellKnownText, "wellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryParse.Value.Invoke(null, new object[] { obj });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A89C File Offset: 0x00008A9C
		public override DbGeometry GeometryFromText(string wellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(wellKnownText, "wellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStGeomFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public override DbGeometry GeometryPointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(pointWellKnownText, "pointWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStPointFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A934 File Offset: 0x00008B34
		public override DbGeometry GeometryLineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(lineWellKnownText, "lineWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStLineFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A980 File Offset: 0x00008B80
		public override DbGeometry GeometryPolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(polygonWellKnownText, "polygonWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStPolyFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public override DbGeometry GeometryMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPointWellKnownText, "multiPointWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStmPointFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AA18 File Offset: 0x00008C18
		public override DbGeometry GeometryMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiLineWellKnownText, "multiLineWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStmLineFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AA64 File Offset: 0x00008C64
		public override DbGeometry GeometryMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPolygonKnownText, "multiPolygonKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStmPolyFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public override DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			object obj2 = this.SqlTypes.SmiSqlGeometryStGeomCollFromText.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public override DbGeometry GeometryFromBinary(byte[] wellKnownBinary)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStGeomFromWkb.Value.Invoke(null, new object[] { obj, 0 });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AB48 File Offset: 0x00008D48
		public override DbGeometry GeometryFromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStGeomFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000AB94 File Offset: 0x00008D94
		public override DbGeometry GeometryPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(pointWellKnownBinary, "pointWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStPointFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public override DbGeometry GeometryLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(lineWellKnownBinary, "lineWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStLineFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public override DbGeometry GeometryPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(polygonWellKnownBinary, "polygonWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStPolyFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000AC78 File Offset: 0x00008E78
		public override DbGeometry GeometryMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStmPointFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		public override DbGeometry GeometryMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStmLineFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000AD10 File Offset: 0x00008F10
		public override DbGeometry GeometryMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStmPolyFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public override DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(geometryCollectionWellKnownBinary, "geometryCollectionWellKnownBinary");
			object obj2 = this.SqlTypes.SmiSqlGeometryStGeomCollFromWkb.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public override DbGeometry GeometryFromGml(string geometryMarkup)
		{
			object obj = this.ConvertToSqlXml(geometryMarkup, "geometryMarkup");
			object obj2 = this.SqlTypes.SmiSqlGeometryGeomFromGml.Value.Invoke(null, new object[] { obj, 0 });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public override DbGeometry GeometryFromGml(string geometryMarkup, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlXml(geometryMarkup, "geometryMarkup");
			object obj2 = this.SqlTypes.SmiSqlGeometryGeomFromGml.Value.Invoke(null, new object[] { obj, coordinateSystemId });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AE40 File Offset: 0x00009040
		public override int GetCoordinateSystemId(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryStSrid.Value.GetValue(obj, null);
			return this.ConvertSqlInt32ToInt(value);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AE88 File Offset: 0x00009088
		public override string GetSpatialTypeName(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStGeometryType.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlStringToString(obj2);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AED4 File Offset: 0x000090D4
		public override int GetDimension(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStDimension.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToInt(obj2);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AF20 File Offset: 0x00009120
		public override DbGeometry GetEnvelope(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStEnvelope.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AF6C File Offset: 0x0000916C
		public override byte[] AsBinary(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStAsBinary.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBytesToBinary(obj2);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public override string AsGml(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryAsGml.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlXmlToString(obj2);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B004 File Offset: 0x00009204
		public override string AsText(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStAsText.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlCharsToString(obj2);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B050 File Offset: 0x00009250
		public override bool GetIsEmpty(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStIsEmpty.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(obj2);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B09C File Offset: 0x0000929C
		public override bool GetIsSimple(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStIsSimple.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(obj2);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public override DbGeometry GetBoundary(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStBoundary.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B134 File Offset: 0x00009334
		public override bool GetIsValid(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStIsValid.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(obj2);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B180 File Offset: 0x00009380
		public override bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStEquals.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B1DC File Offset: 0x000093DC
		public override bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStDisjoint.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B238 File Offset: 0x00009438
		public override bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStIntersects.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B294 File Offset: 0x00009494
		public override bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStTouches.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public override bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStCrosses.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B34C File Offset: 0x0000954C
		public override bool Within(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStWithin.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public override bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStContains.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B404 File Offset: 0x00009604
		public override bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStOverlaps.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B460 File Offset: 0x00009660
		public override bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStRelate.Value.Invoke(obj, new object[] { obj2, matrix });
			return this.ConvertSqlBooleanToBoolean(obj3);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public override DbGeometry Buffer(DbGeometry geometryValue, double distance)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStBuffer.Value.Invoke(obj, new object[] { distance });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B514 File Offset: 0x00009714
		public override double Distance(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStDistance.Value.Invoke(obj, new object[] { obj2 });
			return this.ConvertSqlDoubleToDouble(obj3);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B570 File Offset: 0x00009770
		public override DbGeometry GetConvexHull(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStConvexHull.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B5BC File Offset: 0x000097BC
		public override DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStIntersection.Value.Invoke(obj, new object[] { obj2 });
			return this.GeometryFromProviderValue(obj3);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B618 File Offset: 0x00009818
		public override DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStUnion.Value.Invoke(obj, new object[] { obj2 });
			return this.GeometryFromProviderValue(obj3);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B674 File Offset: 0x00009874
		public override DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStDifference.Value.Invoke(obj, new object[] { obj2 });
			return this.GeometryFromProviderValue(obj3);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public override DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object obj3 = this.SqlTypes.ImiSqlGeometryStSymDifference.Value.Invoke(obj, new object[] { obj2 });
			return this.GeometryFromProviderValue(obj3);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B72C File Offset: 0x0000992C
		public override int? GetElementCount(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStNumGeometries.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(obj2);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B778 File Offset: 0x00009978
		public override DbGeometry ElementAt(DbGeometry geometryValue, int index)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStGeometryN.Value.Invoke(obj, new object[] { index });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B7CC File Offset: 0x000099CC
		public override double? GetXCoordinate(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryStx.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B814 File Offset: 0x00009A14
		public override double? GetYCoordinate(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometrySty.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B85C File Offset: 0x00009A5C
		public override double? GetElevation(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryZ.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public override double? GetMeasure(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryM.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public override double? GetLength(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStLength.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(obj2);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B938 File Offset: 0x00009B38
		public override DbGeometry GetStartPoint(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStStartPoint.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B984 File Offset: 0x00009B84
		public override DbGeometry GetEndPoint(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStEndPoint.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public override bool? GetIsClosed(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStIsClosed.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(obj2);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BA1C File Offset: 0x00009C1C
		public override bool? GetIsRing(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStIsRing.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(obj2);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BA68 File Offset: 0x00009C68
		public override int? GetPointCount(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStNumPoints.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(obj2);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public override DbGeometry PointAt(DbGeometry geometryValue, int index)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStPointN.Value.Invoke(obj, new object[] { index });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override double? GetArea(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStArea.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(obj2);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000BB54 File Offset: 0x00009D54
		public override DbGeometry GetCentroid(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStCentroid.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public override DbGeometry GetPointOnSurface(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStPointOnSurface.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public override DbGeometry GetExteriorRing(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStExteriorRing.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000BC38 File Offset: 0x00009E38
		public override int? GetInteriorRingCount(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStNumInteriorRing.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(obj2);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BC84 File Offset: 0x00009E84
		public override DbGeometry InteriorRingAt(DbGeometry geometryValue, int index)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.SqlTypes.ImiSqlGeometryStInteriorRingN.Value.Invoke(obj, new object[] { index });
			return this.GeometryFromProviderValue(obj2);
		}

		// Token: 0x0400002E RID: 46
		internal static readonly SqlSpatialServices Instance = new SqlSpatialServices();

		// Token: 0x0400002F RID: 47
		private static Dictionary<string, SqlSpatialServices> _otherSpatialServices;

		// Token: 0x04000030 RID: 48
		[NonSerialized]
		private readonly SqlTypesAssemblyLoader _loader;
	}
}
