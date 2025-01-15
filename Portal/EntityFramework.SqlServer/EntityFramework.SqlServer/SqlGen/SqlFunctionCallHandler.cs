using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000036 RID: 54
	internal static class SqlFunctionCallHandler
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x000125B8 File Offset: 0x000107B8
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeStoreFunctionHandlers()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>(19, StringComparer.Ordinal);
			dictionary.Add("CONCAT", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleConcatFunction));
			dictionary.Add("DATEADD", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("DATEDIFF", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("DATENAME", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("DATEPART", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("Parse", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "hierarchyid::Parse"));
			dictionary.Add("GetRoot", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "hierarchyid::GetRoot"));
			dictionary.Add("POINTGEOGRAPHY", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::Point"));
			dictionary.Add("POINTGEOMETRY", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::Point"));
			dictionary.Add("ASTEXTZM", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "AsTextZM", functionExpression, false));
			dictionary.Add("BUFFERWITHTOLERANCE", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "BufferWithTolerance", functionExpression, false));
			dictionary.Add("ENVELOPEANGLE", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "EnvelopeAngle", functionExpression, false));
			dictionary.Add("ENVELOPECENTER", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "EnvelopeCenter", functionExpression, false));
			dictionary.Add("INSTANCEOF", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "InstanceOf", functionExpression, false));
			dictionary.Add("FILTER", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "Filter", functionExpression, false));
			dictionary.Add("MAKEVALID", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "MakeValid", functionExpression, false));
			dictionary.Add("REDUCE", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "Reduce", functionExpression, false));
			dictionary.Add("NUMRINGS", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "NumRings", functionExpression, false));
			dictionary.Add("RINGN", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "RingN", functionExpression, false));
			return dictionary;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012890 File Offset: 0x00010A90
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeCanonicalFunctionHandlers()
		{
			return new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>(16, StringComparer.Ordinal)
			{
				{
					"IndexOf",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionIndexOf)
				},
				{
					"Length",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionLength)
				},
				{
					"NewGuid",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionNewGuid)
				},
				{
					"Round",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionRound)
				},
				{
					"Truncate",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionTruncate)
				},
				{
					"Abs",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionAbs)
				},
				{
					"ToLower",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionToLower)
				},
				{
					"ToUpper",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionToUpper)
				},
				{
					"Trim",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionTrim)
				},
				{
					"Contains",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionContains)
				},
				{
					"StartsWith",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionStartsWith)
				},
				{
					"EndsWith",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionEndsWith)
				},
				{
					"Year",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Month",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Day",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Hour",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Minute",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Second",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Millisecond",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"DayOfYear",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"CurrentDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"CurrentUtcDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCurrentUtcDateTime)
				},
				{
					"CurrentDateTimeOffset",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCurrentDateTimeOffset)
				},
				{
					"GetTotalOffsetMinutes",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionGetTotalOffsetMinutes)
				},
				{
					"LocalDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionLocalDateTime)
				},
				{
					"UtcDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionUtcDateTime)
				},
				{
					"TruncateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionTruncateTime)
				},
				{
					"CreateDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCreateDateTime)
				},
				{
					"CreateDateTimeOffset",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCreateDateTimeOffset)
				},
				{
					"CreateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCreateTime)
				},
				{
					"AddYears",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMonths",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddDays",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddHours",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMinutes",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddSeconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMilliseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMicroseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAddKatmaiOrNewer)
				},
				{
					"AddNanoseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAddKatmaiOrNewer)
				},
				{
					"DiffYears",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMonths",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffDays",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffHours",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMinutes",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffSeconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMilliseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMicroseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiffKatmaiOrNewer)
				},
				{
					"DiffNanoseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiffKatmaiOrNewer)
				},
				{
					"Concat",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleConcatFunction)
				},
				{
					"BitwiseAnd",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseNot",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseOr",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseXor",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				}
			};
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00012D6C File Offset: 0x00010F6C
		private static Dictionary<string, string> InitializeFunctionNameToOperatorDictionary()
		{
			return new Dictionary<string, string>(5, StringComparer.Ordinal)
			{
				{ "Concat", "+" },
				{ "CONCAT", "+" },
				{ "BitwiseAnd", "&" },
				{ "BitwiseNot", "~" },
				{ "BitwiseOr", "|" },
				{ "BitwiseXor", "^" }
			};
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00012DE4 File Offset: 0x00010FE4
		private static Dictionary<string, string> InitializeDateAddFunctionNameToDatepartDictionary()
		{
			return new Dictionary<string, string>(5, StringComparer.Ordinal)
			{
				{ "AddYears", "year" },
				{ "AddMonths", "month" },
				{ "AddDays", "day" },
				{ "AddHours", "hour" },
				{ "AddMinutes", "minute" },
				{ "AddSeconds", "second" },
				{ "AddMilliseconds", "millisecond" },
				{ "AddMicroseconds", "microsecond" },
				{ "AddNanoseconds", "nanosecond" }
			};
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00012E8C File Offset: 0x0001108C
		private static Dictionary<string, string> InitializeDateDiffFunctionNameToDatepartDictionary()
		{
			return new Dictionary<string, string>(5, StringComparer.Ordinal)
			{
				{ "DiffYears", "year" },
				{ "DiffMonths", "month" },
				{ "DiffDays", "day" },
				{ "DiffHours", "hour" },
				{ "DiffMinutes", "minute" },
				{ "DiffSeconds", "second" },
				{ "DiffMilliseconds", "millisecond" },
				{ "DiffMicroseconds", "microsecond" },
				{ "DiffNanoseconds", "nanosecond" }
			};
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00012F34 File Offset: 0x00011134
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeHierarchyIdStaticMethodFunctionsDictionary()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>();
			dictionary.Add("HierarchyIdGetRoot", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "hierarchyid::GetRoot"));
			dictionary.Add("HierarchyIdParse", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "hierarchyid::Parse"));
			return dictionary;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012F9C File Offset: 0x0001119C
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeGeographyStaticMethodFunctionsDictionary()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>();
			dictionary.Add("GeographyFromText", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromTextFunction));
			dictionary.Add("GeographyPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPointFromText"));
			dictionary.Add("GeographyLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STLineFromText"));
			dictionary.Add("GeographyPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPolyFromText"));
			dictionary.Add("GeographyMultiPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPointFromText"));
			dictionary.Add("GeographyMultiLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMLineFromText"));
			dictionary.Add("GeographyMultiPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPolyFromText"));
			dictionary.Add("GeographyCollectionFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STGeomCollFromText"));
			dictionary.Add("GeographyFromBinary", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromBinaryFunction));
			dictionary.Add("GeographyPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPointFromWKB"));
			dictionary.Add("GeographyLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STLineFromWKB"));
			dictionary.Add("GeographyPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPolyFromWKB"));
			dictionary.Add("GeographyMultiPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPointFromWKB"));
			dictionary.Add("GeographyMultiLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMLineFromWKB"));
			dictionary.Add("GeographyMultiPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPolyFromWKB"));
			dictionary.Add("GeographyCollectionFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STGeomCollFromWKB"));
			dictionary.Add("GeographyFromGml", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromGmlFunction));
			return dictionary;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00013240 File Offset: 0x00011440
		private static Dictionary<string, string> InitializeGeographyInstancePropertyFunctionsDictionary()
		{
			return new Dictionary<string, string>
			{
				{ "CoordinateSystemId", "STSrid" },
				{ "Latitude", "Lat" },
				{ "Longitude", "Long" },
				{ "Measure", "M" },
				{ "Elevation", "Z" }
			};
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000132A4 File Offset: 0x000114A4
		private static Dictionary<string, string> InitializeRenamedGeographyInstanceMethodFunctions()
		{
			return new Dictionary<string, string>
			{
				{ "AsText", "STAsText" },
				{ "AsBinary", "STAsBinary" },
				{ "SpatialTypeName", "STGeometryType" },
				{ "SpatialDimension", "STDimension" },
				{ "IsEmptySpatial", "STIsEmpty" },
				{ "SpatialEquals", "STEquals" },
				{ "SpatialDisjoint", "STDisjoint" },
				{ "SpatialIntersects", "STIntersects" },
				{ "SpatialBuffer", "STBuffer" },
				{ "Distance", "STDistance" },
				{ "SpatialUnion", "STUnion" },
				{ "SpatialIntersection", "STIntersection" },
				{ "SpatialDifference", "STDifference" },
				{ "SpatialSymmetricDifference", "STSymDifference" },
				{ "SpatialElementCount", "STNumGeometries" },
				{ "SpatialElementAt", "STGeometryN" },
				{ "SpatialLength", "STLength" },
				{ "StartPoint", "STStartPoint" },
				{ "EndPoint", "STEndPoint" },
				{ "IsClosedSpatial", "STIsClosed" },
				{ "PointCount", "STNumPoints" },
				{ "PointAt", "STPointN" },
				{ "Area", "STArea" }
			};
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013428 File Offset: 0x00011628
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeGeometryStaticMethodFunctionsDictionary()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>();
			dictionary.Add("GeometryFromText", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromTextFunction));
			dictionary.Add("GeometryPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPointFromText"));
			dictionary.Add("GeometryLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STLineFromText"));
			dictionary.Add("GeometryPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPolyFromText"));
			dictionary.Add("GeometryMultiPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPointFromText"));
			dictionary.Add("GeometryMultiLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMLineFromText"));
			dictionary.Add("GeometryMultiPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPolyFromText"));
			dictionary.Add("GeometryCollectionFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STGeomCollFromText"));
			dictionary.Add("GeometryFromBinary", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromBinaryFunction));
			dictionary.Add("GeometryPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPointFromWKB"));
			dictionary.Add("GeometryLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STLineFromWKB"));
			dictionary.Add("GeometryPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPolyFromWKB"));
			dictionary.Add("GeometryMultiPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPointFromWKB"));
			dictionary.Add("GeometryMultiLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMLineFromWKB"));
			dictionary.Add("GeometryMultiPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPolyFromWKB"));
			dictionary.Add("GeometryCollectionFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STGeomCollFromWKB"));
			dictionary.Add("GeometryFromGml", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromGmlFunction));
			return dictionary;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000136CC File Offset: 0x000118CC
		private static Dictionary<string, string> InitializeGeometryInstancePropertyFunctionsDictionary()
		{
			return new Dictionary<string, string>
			{
				{ "CoordinateSystemId", "STSrid" },
				{ "Measure", "M" },
				{ "XCoordinate", "STX" },
				{ "YCoordinate", "STY" },
				{ "Elevation", "Z" }
			};
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00013730 File Offset: 0x00011930
		private static Dictionary<string, string> InitializeRenamedGeometryInstanceMethodFunctions()
		{
			return new Dictionary<string, string>
			{
				{ "AsText", "STAsText" },
				{ "AsBinary", "STAsBinary" },
				{ "SpatialTypeName", "STGeometryType" },
				{ "SpatialDimension", "STDimension" },
				{ "IsEmptySpatial", "STIsEmpty" },
				{ "IsSimpleGeometry", "STIsSimple" },
				{ "IsValidGeometry", "STIsValid" },
				{ "SpatialBoundary", "STBoundary" },
				{ "SpatialEnvelope", "STEnvelope" },
				{ "SpatialEquals", "STEquals" },
				{ "SpatialDisjoint", "STDisjoint" },
				{ "SpatialIntersects", "STIntersects" },
				{ "SpatialTouches", "STTouches" },
				{ "SpatialCrosses", "STCrosses" },
				{ "SpatialWithin", "STWithin" },
				{ "SpatialContains", "STContains" },
				{ "SpatialOverlaps", "STOverlaps" },
				{ "SpatialRelate", "STRelate" },
				{ "SpatialBuffer", "STBuffer" },
				{ "SpatialConvexHull", "STConvexHull" },
				{ "Distance", "STDistance" },
				{ "SpatialUnion", "STUnion" },
				{ "SpatialIntersection", "STIntersection" },
				{ "SpatialDifference", "STDifference" },
				{ "SpatialSymmetricDifference", "STSymDifference" },
				{ "SpatialElementCount", "STNumGeometries" },
				{ "SpatialElementAt", "STGeometryN" },
				{ "SpatialLength", "STLength" },
				{ "StartPoint", "STStartPoint" },
				{ "EndPoint", "STEndPoint" },
				{ "IsClosedSpatial", "STIsClosed" },
				{ "IsRing", "STIsRing" },
				{ "PointCount", "STNumPoints" },
				{ "PointAt", "STPointN" },
				{ "Area", "STArea" },
				{ "Centroid", "STCentroid" },
				{ "PointOnSurface", "STPointOnSurface" },
				{ "ExteriorRing", "STExteriorRing" },
				{ "InteriorRingCount", "STNumInteriorRing" },
				{ "InteriorRingAt", "STInteriorRingN" }
			};
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000139C4 File Offset: 0x00011BC4
		private static ISqlFragment HandleSpatialFromTextFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			string text = (functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::STGeomFromText" : "geography::STGeomFromText");
			string text2 = (functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::Parse" : "geography::Parse");
			if (functionExpression.Arguments.Count == 2)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, text);
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, text2);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00013A28 File Offset: 0x00011C28
		private static ISqlFragment HandleSpatialFromGmlFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			return SqlFunctionCallHandler.HandleSpatialStaticMethodFunctionAppendSrid(sqlgen, functionExpression, functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::GeomFromGml" : "geography::GeomFromGml");
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00013A4C File Offset: 0x00011C4C
		private static ISqlFragment HandleSpatialFromBinaryFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			return SqlFunctionCallHandler.HandleSpatialStaticMethodFunctionAppendSrid(sqlgen, functionExpression, functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::STGeomFromWKB" : "geography::STGeomFromWKB");
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013A70 File Offset: 0x00011C70
		private static ISqlFragment HandleSpatialStaticMethodFunctionAppendSrid(SqlGenerator sqlgen, DbFunctionExpression functionExpression, string functionName)
		{
			if (functionExpression.Arguments.Count == 2)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName);
			}
			DbExpression dbExpression = (functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? SqlFunctionCallHandler._defaultGeometrySridExpression : SqlFunctionCallHandler._defaultGeographySridExpression);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(functionName);
			SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, functionExpression.Arguments.Concat(new DbExpression[] { dbExpression }), sqlBuilder);
			return sqlBuilder;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013ADC File Offset: 0x00011CDC
		internal static ISqlFragment GenerateFunctionCallSql(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			if (SqlFunctionCallHandler.IsSpecialCanonicalFunction(functionExpression))
			{
				return SqlFunctionCallHandler.HandleSpecialCanonicalFunction(sqlgen, functionExpression);
			}
			if (SqlFunctionCallHandler.IsSpecialStoreFunction(functionExpression))
			{
				return SqlFunctionCallHandler.HandleSpecialStoreFunction(sqlgen, functionExpression);
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (SqlFunctionCallHandler.IsSpatialCanonicalFunction(functionExpression, out primitiveTypeKind))
			{
				return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, primitiveTypeKind);
			}
			if (SqlFunctionCallHandler.IsHierarchyCanonicalFunction(functionExpression))
			{
				return SqlFunctionCallHandler.HandleHierarchyIdCanonicalFunction(sqlgen, functionExpression);
			}
			return SqlFunctionCallHandler.HandleFunctionDefault(sqlgen, functionExpression);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013B33 File Offset: 0x00011D33
		private static bool IsSpecialStoreFunction(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.IsStoreFunction(e.Function) && SqlFunctionCallHandler._storeFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013B59 File Offset: 0x00011D59
		private static bool IsSpecialCanonicalFunction(DbFunctionExpression e)
		{
			return e.Function.IsCanonicalFunction() && SqlFunctionCallHandler._canonicalFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013B80 File Offset: 0x00011D80
		private static bool IsHierarchyCanonicalFunction(DbFunctionExpression e)
		{
			if (e.Function.IsCanonicalFunction())
			{
				if (e.ResultType.IsHierarchyIdType())
				{
					return true;
				}
				using (ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = e.Function.Parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TypeUsage.IsHierarchyIdType())
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00013C00 File Offset: 0x00011E00
		private static bool IsSpatialCanonicalFunction(DbFunctionExpression e, out PrimitiveTypeKind spatialTypeKind)
		{
			if (e.Function.IsCanonicalFunction())
			{
				if (e.ResultType.IsSpatialType(out spatialTypeKind))
				{
					return true;
				}
				using (ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = e.Function.Parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TypeUsage.IsSpatialType(out spatialTypeKind))
						{
							return true;
						}
					}
				}
			}
			spatialTypeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00013C84 File Offset: 0x00011E84
		private static ISqlFragment HandleFunctionDefault(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, null);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00013C90 File Offset: 0x00011E90
		private static ISqlFragment HandleFunctionDefaultGivenName(SqlGenerator sqlgen, DbFunctionExpression e, string functionName)
		{
			if (SqlFunctionCallHandler.CastReturnTypeToInt64(e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "bigint");
			}
			if (SqlFunctionCallHandler.CastReturnTypeToInt32(sqlgen, e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "int");
			}
			if (SqlFunctionCallHandler.CastReturnTypeToInt16(e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "smallint");
			}
			if (SqlFunctionCallHandler.CastReturnTypeToSingle(e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "real");
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, null);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013D00 File Offset: 0x00011F00
		private static ISqlFragment HandleFunctionDefaultCastReturnValue(SqlGenerator sqlgen, DbFunctionExpression e, string functionName, string returnType)
		{
			return SqlFunctionCallHandler.WrapWithCast(returnType, delegate(SqlBuilder result)
			{
				if (functionName == null)
				{
					SqlFunctionCallHandler.WriteFunctionName(result, e.Function);
				}
				else
				{
					result.Append(functionName);
				}
				SqlFunctionCallHandler.HandleFunctionArgumentsDefault(sqlgen, e, result);
			});
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013D3C File Offset: 0x00011F3C
		private static ISqlFragment WrapWithCast(string returnType, Action<SqlBuilder> toWrap)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (returnType != null)
			{
				sqlBuilder.Append(" CAST(");
			}
			toWrap(sqlBuilder);
			if (returnType != null)
			{
				sqlBuilder.Append(" AS ");
				sqlBuilder.Append(returnType);
				sqlBuilder.Append(")");
			}
			return sqlBuilder;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013D85 File Offset: 0x00011F85
		private static void HandleFunctionArgumentsDefault(SqlGenerator sqlgen, DbFunctionExpression e, SqlBuilder result)
		{
			bool niladicFunctionAttribute = e.Function.NiladicFunctionAttribute;
			if (niladicFunctionAttribute && e.Arguments.Count > 0)
			{
				throw new MetadataException(Strings.SqlGen_NiladicFunctionsCannotHaveParameters);
			}
			if (!niladicFunctionAttribute)
			{
				SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, e.Arguments, result);
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013DC0 File Offset: 0x00011FC0
		private static void WriteFunctionArguments(SqlGenerator sqlgen, IEnumerable<DbExpression> functionArguments, SqlBuilder result)
		{
			result.Append("(");
			string text = "";
			foreach (DbExpression dbExpression in functionArguments)
			{
				result.Append(text);
				result.Append(dbExpression.Accept<ISqlFragment>(sqlgen));
				text = ", ";
			}
			result.Append(")");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013E38 File Offset: 0x00012038
		private static ISqlFragment HandleFunctionGivenNameBasedOnVersion(SqlGenerator sqlgen, DbFunctionExpression e, string preKatmaiName, string katmaiName)
		{
			if (sqlgen.IsPreKatmai)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, preKatmaiName);
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, katmaiName);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013E53 File Offset: 0x00012053
		private static ISqlFragment HandleSpecialStoreFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunction(SqlFunctionCallHandler._storeFunctionHandlers, sqlgen, e);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013E64 File Offset: 0x00012064
		private static ISqlFragment HandleHierarchyIdCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			SqlFunctionCallHandler.FunctionHandler functionHandler;
			if (SqlFunctionCallHandler._hierarchyIdFunctionNameToStaticMethodHandlerDictionary.TryGetValue(functionExpression.Function.Name, out functionHandler))
			{
				return functionHandler(sqlgen, functionExpression);
			}
			string name = functionExpression.Function.Name;
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, name, functionExpression, false);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013EA8 File Offset: 0x000120A8
		private static ISqlFragment HandleSpecialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunction(SqlFunctionCallHandler._canonicalFunctionHandlers, sqlgen, e);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013EB6 File Offset: 0x000120B6
		private static ISqlFragment HandleSpecialFunction(Dictionary<string, SqlFunctionCallHandler.FunctionHandler> handlers, SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return handlers[e.Function.Name](sqlgen, e);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013ED0 File Offset: 0x000120D0
		private static ISqlFragment HandleSpatialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression, PrimitiveTypeKind spatialTypeKind)
		{
			if (spatialTypeKind == PrimitiveTypeKind.Geography)
			{
				return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, SqlFunctionCallHandler._geographyFunctionNameToStaticMethodHandlerDictionary, SqlFunctionCallHandler._geographyFunctionNameToInstancePropertyNameDictionary, SqlFunctionCallHandler._geographyRenamedInstanceMethodFunctionDictionary);
			}
			return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, SqlFunctionCallHandler._geometryFunctionNameToStaticMethodHandlerDictionary, SqlFunctionCallHandler._geometryFunctionNameToInstancePropertyNameDictionary, SqlFunctionCallHandler._geometryRenamedInstanceMethodFunctionDictionary);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00013F04 File Offset: 0x00012104
		private static ISqlFragment HandleSpatialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression, Dictionary<string, SqlFunctionCallHandler.FunctionHandler> staticMethodsMap, Dictionary<string, string> instancePropertiesMap, Dictionary<string, string> renamedInstanceMethodsMap)
		{
			SqlFunctionCallHandler.FunctionHandler functionHandler;
			if (staticMethodsMap.TryGetValue(functionExpression.Function.Name, out functionHandler))
			{
				return functionHandler(sqlgen, functionExpression);
			}
			string text;
			if (instancePropertiesMap.TryGetValue(functionExpression.Function.Name, out text))
			{
				return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, text, functionExpression, true, null);
			}
			string name;
			if (!renamedInstanceMethodsMap.TryGetValue(functionExpression.Function.Name, out name))
			{
				name = functionExpression.Function.Name;
			}
			string text2 = null;
			if (name == "AsGml")
			{
				text2 = "nvarchar(max)";
			}
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, name, functionExpression, false, text2);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013F90 File Offset: 0x00012190
		private static ISqlFragment WriteInstanceFunctionCall(SqlGenerator sqlgen, string functionName, DbFunctionExpression functionExpression, bool isPropertyAccess)
		{
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, functionName, functionExpression, isPropertyAccess, null);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00013F9C File Offset: 0x0001219C
		private static ISqlFragment WriteInstanceFunctionCall(SqlGenerator sqlgen, string functionName, DbFunctionExpression functionExpression, bool isPropertyAccess, string castReturnTypeTo)
		{
			return SqlFunctionCallHandler.WrapWithCast(castReturnTypeTo, delegate(SqlBuilder result)
			{
				DbExpression dbExpression = functionExpression.Arguments[0];
				if (dbExpression.ExpressionKind != DbExpressionKind.Function)
				{
					sqlgen.ParenthesizeExpressionIfNeeded(dbExpression, result);
				}
				else
				{
					result.Append(dbExpression.Accept<ISqlFragment>(sqlgen));
				}
				result.Append(".");
				result.Append(functionName);
				if (!isPropertyAccess)
				{
					SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, functionExpression.Arguments.Skip(1), result);
				}
			});
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00013FE0 File Offset: 0x000121E0
		private static ISqlFragment HandleSpecialFunctionToOperator(SqlGenerator sqlgen, DbFunctionExpression e, bool parenthesizeArguments)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Arguments.Count > 1)
			{
				if (parenthesizeArguments)
				{
					sqlBuilder.Append("(");
				}
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				if (parenthesizeArguments)
				{
					sqlBuilder.Append(")");
				}
			}
			sqlBuilder.Append(" ");
			sqlBuilder.Append(SqlFunctionCallHandler._functionNameToOperatorDictionary[e.Function.Name]);
			sqlBuilder.Append(" ");
			if (parenthesizeArguments)
			{
				sqlBuilder.Append("(");
			}
			sqlBuilder.Append(e.Arguments[e.Arguments.Count - 1].Accept<ISqlFragment>(sqlgen));
			if (parenthesizeArguments)
			{
				sqlBuilder.Append(")");
			}
			return sqlBuilder;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000140A7 File Offset: 0x000122A7
		private static ISqlFragment HandleConcatFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunctionToOperator(sqlgen, e, false);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000140B1 File Offset: 0x000122B1
		private static ISqlFragment HandleCanonicalFunctionBitwise(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunctionToOperator(sqlgen, e, true);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000140BC File Offset: 0x000122BC
		internal static ISqlFragment HandleDatepartDateFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			DbConstantExpression dbConstantExpression = e.Arguments[0] as DbConstantExpression;
			if (dbConstantExpression == null)
			{
				throw new InvalidOperationException(Strings.SqlGen_InvalidDatePartArgumentExpression(e.Function.NamespaceName, e.Function.Name));
			}
			string text = dbConstantExpression.Value as string;
			if (text == null)
			{
				throw new InvalidOperationException(Strings.SqlGen_InvalidDatePartArgumentExpression(e.Function.NamespaceName, e.Function.Name));
			}
			if (!SqlFunctionCallHandler._datepartKeywords.Contains(text))
			{
				throw new InvalidOperationException(Strings.SqlGen_InvalidDatePartArgumentValue(text, e.Function.NamespaceName, e.Function.Name));
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			SqlFunctionCallHandler.WriteFunctionName(sqlBuilder, e.Function);
			sqlBuilder.Append("(");
			sqlBuilder.Append(text);
			for (int i = 1; i < e.Arguments.Count; i++)
			{
				sqlBuilder.Append(", ");
				sqlBuilder.Append(e.Arguments[i].Accept<ISqlFragment>(sqlgen));
			}
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000141C7 File Offset: 0x000123C7
		private static ISqlFragment HandleCanonicalFunctionDatepart(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionDatepart(sqlgen, e.Function.Name.ToLowerInvariant(), e);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000141E0 File Offset: 0x000123E0
		private static ISqlFragment HandleCanonicalFunctionGetTotalOffsetMinutes(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionDatepart(sqlgen, "tzoffset", e);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000141EE File Offset: 0x000123EE
		private static ISqlFragment HandleCanonicalFunctionLocalDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CAST (");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(" AS DATETIME2)");
			return sqlBuilder;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001422A File Offset: 0x0001242A
		private static ISqlFragment HandleCanonicalFunctionUtcDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CONVERT (DATETIME2, ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", 1)");
			return sqlBuilder;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00014268 File Offset: 0x00012468
		private static ISqlFragment HandleCanonicalFunctionDatepart(SqlGenerator sqlgen, string datepart, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("DATEPART (");
			sqlBuilder.Append(datepart);
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000142BA File Offset: 0x000124BA
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionGivenNameBasedOnVersion(sqlgen, e, "GetDate", "SysDateTime");
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000142CD File Offset: 0x000124CD
		private static ISqlFragment HandleCanonicalFunctionCurrentUtcDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionGivenNameBasedOnVersion(sqlgen, e, "GetUtcDate", "SysUtcDateTime");
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000142E0 File Offset: 0x000124E0
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTimeOffset(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "SysDateTimeOffset");
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000142F8 File Offset: 0x000124F8
		private static ISqlFragment HandleCanonicalFunctionCreateDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			string text = (sqlgen.IsPreKatmai ? "datetime" : "datetime2");
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, text, e.Arguments, true, false);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00014329 File Offset: 0x00012529
		private static ISqlFragment HandleCanonicalFunctionCreateDateTimeOffset(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, "datetimeoffset", e.Arguments, true, true);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014345 File Offset: 0x00012545
		private static ISqlFragment HandleCanonicalFunctionCreateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, "time", e.Arguments, false, false);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00014364 File Offset: 0x00012564
		private static ISqlFragment HandleCanonicalFunctionDateTimeTypeCreation(SqlGenerator sqlgen, string typeName, IList<DbExpression> args, bool hasDatePart, bool hasTimeZonePart)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			int num = 0;
			sqlBuilder.Append("convert (");
			sqlBuilder.Append(typeName);
			sqlBuilder.Append(",");
			if (hasDatePart)
			{
				sqlBuilder.Append("right('000' + ");
				SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[num++]);
				sqlBuilder.Append(", 4)");
				sqlBuilder.Append(" + '-' + ");
				SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[num++]);
				sqlBuilder.Append(" + '-' + ");
				SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[num++]);
				sqlBuilder.Append(" + ' ' + ");
			}
			SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[num++]);
			sqlBuilder.Append(" + ':' + ");
			SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[num++]);
			sqlBuilder.Append(" + ':' + str(");
			sqlBuilder.Append(args[num++].Accept<ISqlFragment>(sqlgen));
			if (sqlgen.IsPreKatmai)
			{
				sqlBuilder.Append(", 6, 3)");
			}
			else
			{
				sqlBuilder.Append(", 10, 7)");
			}
			if (hasTimeZonePart)
			{
				sqlBuilder.Append(" + (CASE WHEN ");
				sqlgen.ParenthesizeExpressionIfNeeded(args[num], sqlBuilder);
				sqlBuilder.Append(" >= 0 THEN '+' ELSE '-' END) + convert(varchar(255), ABS(");
				sqlgen.ParenthesizeExpressionIfNeeded(args[num], sqlBuilder);
				sqlBuilder.Append("/60)) + ':' + convert(varchar(255), ABS(");
				sqlgen.ParenthesizeExpressionIfNeeded(args[num], sqlBuilder);
				sqlBuilder.Append("%60))");
			}
			sqlBuilder.Append(", 121)");
			return sqlBuilder;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000144DD File Offset: 0x000126DD
		private static void AppendConvertToVarchar(SqlGenerator sqlgen, SqlBuilder result, DbExpression e)
		{
			result.Append("convert(varchar(255), ");
			result.Append(e.Accept<ISqlFragment>(sqlgen));
			result.Append(")");
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00014504 File Offset: 0x00012704
		private static ISqlFragment HandleCanonicalFunctionTruncateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			bool flag = e.Arguments[0].ResultType.GetPrimitiveTypeKind() == PrimitiveTypeKind.DateTimeOffset;
			if (sqlgen.IsPreKatmai && flag)
			{
				throw new NotSupportedException(Strings.SqlGen_CanonicalFunctionNotSupportedPriorSql10(e.Function.Name));
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			ISqlFragment sqlFragment = e.Arguments[0].Accept<ISqlFragment>(sqlgen);
			if (sqlgen.IsPreKatmai)
			{
				sqlBuilder.Append("dateadd(d, datediff(d, 0, ");
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append("), 0)");
			}
			else if (!flag)
			{
				sqlBuilder.Append("cast(cast(");
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append(" as date) as datetime2)");
			}
			else
			{
				sqlBuilder.Append("todatetimeoffset(cast(");
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append(" as date), datepart(tz, ");
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append("))");
			}
			return sqlBuilder;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000145DE File Offset: 0x000127DE
		private static ISqlFragment HandleCanonicalFunctionDateAddKatmaiOrNewer(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd(sqlgen, e);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000145F0 File Offset: 0x000127F0
		private static ISqlFragment HandleCanonicalFunctionDateAdd(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("DATEADD (");
			sqlBuilder.Append(SqlFunctionCallHandler._dateAddFunctionNameToDatepartDictionary[e.Function.Name]);
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00014679 File Offset: 0x00012879
		private static ISqlFragment HandleCanonicalFunctionDateDiffKatmaiOrNewer(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff(sqlgen, e);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001468C File Offset: 0x0001288C
		private static ISqlFragment HandleCanonicalFunctionDateDiff(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("DATEDIFF (");
			sqlBuilder.Append(SqlFunctionCallHandler._dateDiffFunctionNameToDatepartDictionary[e.Function.Name]);
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00014715 File Offset: 0x00012915
		private static ISqlFragment HandleCanonicalFunctionIndexOf(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "CHARINDEX");
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014723 File Offset: 0x00012923
		private static ISqlFragment HandleCanonicalFunctionNewGuid(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "NEWID");
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00014731 File Offset: 0x00012931
		private static ISqlFragment HandleCanonicalFunctionLength(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "LEN");
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001473F File Offset: 0x0001293F
		private static ISqlFragment HandleCanonicalFunctionRound(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionRoundOrTruncate(sqlgen, e, true);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00014749 File Offset: 0x00012949
		private static ISqlFragment HandleCanonicalFunctionTruncate(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionRoundOrTruncate(sqlgen, e, false);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00014754 File Offset: 0x00012954
		private static ISqlFragment HandleCanonicalFunctionRoundOrTruncate(SqlGenerator sqlgen, DbFunctionExpression e, bool round)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = false;
			if (e.Arguments.Count == 1)
			{
				flag = SqlFunctionCallHandler.CastReturnTypeToSingle(e);
				if (flag)
				{
					sqlBuilder.Append(" CAST(");
				}
			}
			sqlBuilder.Append("ROUND(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", ");
			if (e.Arguments.Count > 1)
			{
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			}
			else
			{
				sqlBuilder.Append("0");
			}
			if (!round)
			{
				sqlBuilder.Append(", 1");
			}
			sqlBuilder.Append(")");
			if (flag)
			{
				sqlBuilder.Append(" AS real)");
			}
			return sqlBuilder;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00014815 File Offset: 0x00012A15
		private static ISqlFragment HandleCanonicalFunctionAbs(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			if (e.Arguments[0].ResultType.IsPrimitiveType(PrimitiveTypeKind.Byte))
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				return sqlBuilder;
			}
			return SqlFunctionCallHandler.HandleFunctionDefault(sqlgen, e);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00014855 File Offset: 0x00012A55
		private static ISqlFragment HandleCanonicalFunctionTrim(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("LTRIM(RTRIM(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append("))");
			return sqlBuilder;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001488A File Offset: 0x00012A8A
		private static ISqlFragment HandleCanonicalFunctionToLower(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "LOWER");
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00014898 File Offset: 0x00012A98
		private static ISqlFragment HandleCanonicalFunctionToUpper(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "UPPER");
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000148A8 File Offset: 0x00012AA8
		private static void TranslateConstantParameterForLike(SqlGenerator sqlgen, DbExpression targetExpression, DbConstantExpression constSearchParamExpression, SqlBuilder result, bool insertPercentStart, bool insertPercentEnd)
		{
			result.Append(targetExpression.Accept<ISqlFragment>(sqlgen));
			result.Append(" LIKE ");
			StringBuilder stringBuilder = new StringBuilder();
			if (insertPercentStart)
			{
				stringBuilder.Append("%");
			}
			bool flag;
			stringBuilder.Append(SqlProviderManifest.EscapeLikeText(constSearchParamExpression.Value as string, false, out flag));
			if (insertPercentEnd)
			{
				stringBuilder.Append("%");
			}
			DbConstantExpression dbConstantExpression = constSearchParamExpression.ResultType.Constant(stringBuilder.ToString());
			result.Append(dbConstantExpression.Accept<ISqlFragment>(sqlgen));
			if (flag)
			{
				result.Append(" ESCAPE '~'");
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001493A File Offset: 0x00012B3A
		private static ISqlFragment HandleCanonicalFunctionContains(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionContains), sqlgen, e);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014950 File Offset: 0x00012B50
		private static SqlBuilder HandleCanonicalFunctionContains(SqlGenerator sqlgen, IList<DbExpression> args, SqlBuilder result)
		{
			DbConstantExpression dbConstantExpression = args[1] as DbConstantExpression;
			if (dbConstantExpression != null && !string.IsNullOrEmpty(dbConstantExpression.Value as string))
			{
				SqlFunctionCallHandler.TranslateConstantParameterForLike(sqlgen, args[0], dbConstantExpression, result, true, true);
			}
			else
			{
				result.Append("CHARINDEX( ");
				result.Append(args[1].Accept<ISqlFragment>(sqlgen));
				result.Append(", ");
				result.Append(args[0].Accept<ISqlFragment>(sqlgen));
				result.Append(") > 0");
			}
			return result;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000149DA File Offset: 0x00012BDA
		private static ISqlFragment HandleCanonicalFunctionStartsWith(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionStartsWith), sqlgen, e);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000149F0 File Offset: 0x00012BF0
		private static SqlBuilder HandleCanonicalFunctionStartsWith(SqlGenerator sqlgen, IList<DbExpression> args, SqlBuilder result)
		{
			DbConstantExpression dbConstantExpression = args[1] as DbConstantExpression;
			if (dbConstantExpression != null && !string.IsNullOrEmpty(dbConstantExpression.Value as string))
			{
				SqlFunctionCallHandler.TranslateConstantParameterForLike(sqlgen, args[0], dbConstantExpression, result, false, true);
			}
			else
			{
				result.Append("CHARINDEX( ");
				result.Append(args[1].Accept<ISqlFragment>(sqlgen));
				result.Append(", ");
				result.Append(args[0].Accept<ISqlFragment>(sqlgen));
				result.Append(") = 1");
			}
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00014A7A File Offset: 0x00012C7A
		private static ISqlFragment HandleCanonicalFunctionEndsWith(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionEndsWith), sqlgen, e);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00014A90 File Offset: 0x00012C90
		private static SqlBuilder HandleCanonicalFunctionEndsWith(SqlGenerator sqlgen, IList<DbExpression> args, SqlBuilder result)
		{
			DbConstantExpression dbConstantExpression = args[1] as DbConstantExpression;
			DbPropertyExpression dbPropertyExpression = args[0] as DbPropertyExpression;
			if (dbConstantExpression != null && dbPropertyExpression != null && !string.IsNullOrEmpty(dbConstantExpression.Value as string))
			{
				SqlFunctionCallHandler.TranslateConstantParameterForLike(sqlgen, args[0], dbConstantExpression, result, true, false);
			}
			else
			{
				result.Append("CHARINDEX( REVERSE(");
				result.Append(args[1].Accept<ISqlFragment>(sqlgen));
				result.Append("), REVERSE(");
				result.Append(args[0].Accept<ISqlFragment>(sqlgen));
				result.Append(")) = 1");
			}
			return result;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00014B2C File Offset: 0x00012D2C
		private static ISqlFragment WrapPredicate(Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder> predicateTranslator, SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CASE WHEN (");
			predicateTranslator(sqlgen, e.Arguments, sqlBuilder);
			sqlBuilder.Append(") THEN cast(1 as bit) WHEN ( NOT (");
			predicateTranslator(sqlgen, e.Arguments, sqlBuilder);
			sqlBuilder.Append(")) THEN cast(0 as bit) END");
			return sqlBuilder;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00014B80 File Offset: 0x00012D80
		internal static void WriteFunctionName(SqlBuilder result, EdmFunction function)
		{
			string text;
			if (function.StoreFunctionNameAttribute != null)
			{
				text = function.StoreFunctionNameAttribute;
			}
			else
			{
				text = function.Name;
			}
			if (function.IsCanonicalFunction())
			{
				result.Append(text.ToUpperInvariant());
				return;
			}
			if (SqlFunctionCallHandler.IsStoreFunction(function))
			{
				result.Append(text);
				return;
			}
			if (string.IsNullOrEmpty(function.Schema))
			{
				result.Append(SqlGenerator.QuoteIdentifier(function.NamespaceName));
			}
			else
			{
				result.Append(SqlGenerator.QuoteIdentifier(function.Schema));
			}
			result.Append(".");
			result.Append(SqlGenerator.QuoteIdentifier(text));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00014C12 File Offset: 0x00012E12
		internal static bool IsStoreFunction(EdmFunction function)
		{
			return function.BuiltInAttribute && !function.IsCanonicalFunction();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00014C27 File Offset: 0x00012E27
		internal static bool CastReturnTypeToInt64(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt64, PrimitiveTypeKind.Int64);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00014C38 File Offset: 0x00012E38
		internal static bool CastReturnTypeToInt32(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			if (!SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt32.Contains(e.Function.FullName))
			{
				return false;
			}
			return e.Arguments.Select((DbExpression t) => sqlgen.StoreItemCollection.ProviderManifest.GetStoreType(t.ResultType)).Any((TypeUsage storeType) => SqlFunctionCallHandler._maxTypeNames.Contains(storeType.EdmType.Name));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00014CA6 File Offset: 0x00012EA6
		internal static bool CastReturnTypeToInt16(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt16, PrimitiveTypeKind.Int16);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00014CB5 File Offset: 0x00012EB5
		internal static bool CastReturnTypeToSingle(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToSingle, PrimitiveTypeKind.Single);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00014CC4 File Offset: 0x00012EC4
		private static bool CastReturnTypeToGivenType(DbFunctionExpression e, ISet<string> functionsRequiringReturnTypeCast, PrimitiveTypeKind type)
		{
			return functionsRequiringReturnTypeCast.Contains(e.Function.FullName) && e.Arguments.Any((DbExpression t) => t.ResultType.IsPrimitiveType(type));
		}

		// Token: 0x040000E6 RID: 230
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _storeFunctionHandlers = SqlFunctionCallHandler.InitializeStoreFunctionHandlers();

		// Token: 0x040000E7 RID: 231
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _canonicalFunctionHandlers = SqlFunctionCallHandler.InitializeCanonicalFunctionHandlers();

		// Token: 0x040000E8 RID: 232
		private static readonly Dictionary<string, string> _functionNameToOperatorDictionary = SqlFunctionCallHandler.InitializeFunctionNameToOperatorDictionary();

		// Token: 0x040000E9 RID: 233
		private static readonly Dictionary<string, string> _dateAddFunctionNameToDatepartDictionary = SqlFunctionCallHandler.InitializeDateAddFunctionNameToDatepartDictionary();

		// Token: 0x040000EA RID: 234
		private static readonly Dictionary<string, string> _dateDiffFunctionNameToDatepartDictionary = SqlFunctionCallHandler.InitializeDateDiffFunctionNameToDatepartDictionary();

		// Token: 0x040000EB RID: 235
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _hierarchyIdFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeHierarchyIdStaticMethodFunctionsDictionary();

		// Token: 0x040000EC RID: 236
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _geographyFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeGeographyStaticMethodFunctionsDictionary();

		// Token: 0x040000ED RID: 237
		private static readonly Dictionary<string, string> _geographyFunctionNameToInstancePropertyNameDictionary = SqlFunctionCallHandler.InitializeGeographyInstancePropertyFunctionsDictionary();

		// Token: 0x040000EE RID: 238
		private static readonly Dictionary<string, string> _geographyRenamedInstanceMethodFunctionDictionary = SqlFunctionCallHandler.InitializeRenamedGeographyInstanceMethodFunctions();

		// Token: 0x040000EF RID: 239
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _geometryFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeGeometryStaticMethodFunctionsDictionary();

		// Token: 0x040000F0 RID: 240
		private static readonly Dictionary<string, string> _geometryFunctionNameToInstancePropertyNameDictionary = SqlFunctionCallHandler.InitializeGeometryInstancePropertyFunctionsDictionary();

		// Token: 0x040000F1 RID: 241
		private static readonly Dictionary<string, string> _geometryRenamedInstanceMethodFunctionDictionary = SqlFunctionCallHandler.InitializeRenamedGeometryInstanceMethodFunctions();

		// Token: 0x040000F2 RID: 242
		private static readonly ISet<string> _datepartKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"year", "yy", "yyyy", "quarter", "qq", "q", "month", "mm", "m", "dayofyear",
			"dy", "y", "day", "dd", "d", "week", "wk", "ww", "weekday", "dw",
			"w", "hour", "hh", "minute", "mi", "n", "second", "ss", "s", "millisecond",
			"ms", "microsecond", "mcs", "nanosecond", "ns", "tzoffset", "tz", "iso_week", "isoww", "isowk"
		};

		// Token: 0x040000F3 RID: 243
		private static readonly ISet<string> _functionRequiresReturnTypeCastToInt64 = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SqlServer.CHARINDEX" };

		// Token: 0x040000F4 RID: 244
		private static readonly ISet<string> _functionRequiresReturnTypeCastToInt32 = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SqlServer.LEN", "SqlServer.PATINDEX", "SqlServer.DATALENGTH", "SqlServer.CHARINDEX", "Edm.IndexOf", "Edm.Length" };

		// Token: 0x040000F5 RID: 245
		private static readonly ISet<string> _functionRequiresReturnTypeCastToInt16 = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Edm.Abs" };

		// Token: 0x040000F6 RID: 246
		private static readonly ISet<string> _functionRequiresReturnTypeCastToSingle = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Edm.Abs", "Edm.Round", "Edm.Floor", "Edm.Ceiling" };

		// Token: 0x040000F7 RID: 247
		private static readonly ISet<string> _maxTypeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "varchar(max)", "nvarchar(max)", "text", "ntext", "varbinary(max)", "image", "xml" };

		// Token: 0x040000F8 RID: 248
		private static readonly DbExpression _defaultGeographySridExpression = DbExpressionBuilder.Constant(DbGeography.DefaultCoordinateSystemId);

		// Token: 0x040000F9 RID: 249
		private static readonly DbExpression _defaultGeometrySridExpression = DbExpressionBuilder.Constant(DbGeometry.DefaultCoordinateSystemId);

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x06000701 RID: 1793
		private delegate ISqlFragment FunctionHandler(SqlGenerator sqlgen, DbFunctionExpression functionExpr);
	}
}
