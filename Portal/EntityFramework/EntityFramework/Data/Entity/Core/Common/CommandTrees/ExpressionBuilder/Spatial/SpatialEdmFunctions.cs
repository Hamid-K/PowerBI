using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial
{
	// Token: 0x020006F9 RID: 1785
	public static class SpatialEdmFunctions
	{
		// Token: 0x060053C6 RID: 21446 RVA: 0x0012D42C File Offset: 0x0012B62C
		public static DbFunctionExpression GeometryFromText(DbExpression wellKnownText)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromText", new DbExpression[] { wellKnownText });
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x0012D44E File Offset: 0x0012B64E
		public static DbFunctionExpression GeometryFromText(DbExpression wellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromText", new DbExpression[] { wellKnownText, coordinateSystemId });
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0012D480 File Offset: 0x0012B680
		public static DbFunctionExpression GeometryPointFromText(DbExpression pointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownText, "pointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPointFromText", new DbExpression[] { pointWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0012D4B2 File Offset: 0x0012B6B2
		public static DbFunctionExpression GeometryLineFromText(DbExpression lineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownText, "lineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryLineFromText", new DbExpression[] { lineWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0012D4E4 File Offset: 0x0012B6E4
		public static DbFunctionExpression GeometryPolygonFromText(DbExpression polygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownText, "polygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPolygonFromText", new DbExpression[] { polygonWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0012D516 File Offset: 0x0012B716
		public static DbFunctionExpression GeometryMultiPointFromText(DbExpression multiPointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownText, "multiPointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPointFromText", new DbExpression[] { multiPointWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x0012D548 File Offset: 0x0012B748
		public static DbFunctionExpression GeometryMultiLineFromText(DbExpression multiLineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownText, "multiLineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiLineFromText", new DbExpression[] { multiLineWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0012D57A File Offset: 0x0012B77A
		public static DbFunctionExpression GeometryMultiPolygonFromText(DbExpression multiPolygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPolygonFromText", new DbExpression[] { multiPolygonWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x0012D5AC File Offset: 0x0012B7AC
		public static DbFunctionExpression GeometryCollectionFromText(DbExpression geometryCollectionWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryCollectionFromText", new DbExpression[] { geometryCollectionWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0012D5DE File Offset: 0x0012B7DE
		public static DbFunctionExpression GeometryFromBinary(DbExpression wellKnownBinaryValue)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromBinary", new DbExpression[] { wellKnownBinaryValue });
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0012D600 File Offset: 0x0012B800
		public static DbFunctionExpression GeometryFromBinary(DbExpression wellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromBinary", new DbExpression[] { wellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x0012D632 File Offset: 0x0012B832
		public static DbFunctionExpression GeometryPointFromBinary(DbExpression pointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownBinaryValue, "pointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPointFromBinary", new DbExpression[] { pointWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x0012D664 File Offset: 0x0012B864
		public static DbFunctionExpression GeometryLineFromBinary(DbExpression lineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownBinaryValue, "lineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryLineFromBinary", new DbExpression[] { lineWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x0012D696 File Offset: 0x0012B896
		public static DbFunctionExpression GeometryPolygonFromBinary(DbExpression polygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownBinaryValue, "polygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPolygonFromBinary", new DbExpression[] { polygonWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0012D6C8 File Offset: 0x0012B8C8
		public static DbFunctionExpression GeometryMultiPointFromBinary(DbExpression multiPointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownBinaryValue, "multiPointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPointFromBinary", new DbExpression[] { multiPointWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x0012D6FA File Offset: 0x0012B8FA
		public static DbFunctionExpression GeometryMultiLineFromBinary(DbExpression multiLineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownBinaryValue, "multiLineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiLineFromBinary", new DbExpression[] { multiLineWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x0012D72C File Offset: 0x0012B92C
		public static DbFunctionExpression GeometryMultiPolygonFromBinary(DbExpression multiPolygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownBinaryValue, "multiPolygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPolygonFromBinary", new DbExpression[] { multiPolygonWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x0012D75E File Offset: 0x0012B95E
		public static DbFunctionExpression GeometryCollectionFromBinary(DbExpression geometryCollectionWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geometryCollectionWellKnownBinaryValue, "geometryCollectionWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryCollectionFromBinary", new DbExpression[] { geometryCollectionWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x0012D790 File Offset: 0x0012B990
		public static DbFunctionExpression GeometryFromGml(DbExpression geometryMarkup)
		{
			Check.NotNull<DbExpression>(geometryMarkup, "geometryMarkup");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromGml", new DbExpression[] { geometryMarkup });
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x0012D7B2 File Offset: 0x0012B9B2
		public static DbFunctionExpression GeometryFromGml(DbExpression geometryMarkup, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geometryMarkup, "geometryMarkup");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromGml", new DbExpression[] { geometryMarkup, coordinateSystemId });
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x0012D7E4 File Offset: 0x0012B9E4
		public static DbFunctionExpression GeographyFromText(DbExpression wellKnownText)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromText", new DbExpression[] { wellKnownText });
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0012D806 File Offset: 0x0012BA06
		public static DbFunctionExpression GeographyFromText(DbExpression wellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromText", new DbExpression[] { wellKnownText, coordinateSystemId });
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0012D838 File Offset: 0x0012BA38
		public static DbFunctionExpression GeographyPointFromText(DbExpression pointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownText, "pointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPointFromText", new DbExpression[] { pointWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0012D86A File Offset: 0x0012BA6A
		public static DbFunctionExpression GeographyLineFromText(DbExpression lineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownText, "lineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyLineFromText", new DbExpression[] { lineWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0012D89C File Offset: 0x0012BA9C
		public static DbFunctionExpression GeographyPolygonFromText(DbExpression polygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownText, "polygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPolygonFromText", new DbExpression[] { polygonWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0012D8CE File Offset: 0x0012BACE
		public static DbFunctionExpression GeographyMultiPointFromText(DbExpression multiPointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownText, "multiPointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPointFromText", new DbExpression[] { multiPointWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0012D900 File Offset: 0x0012BB00
		public static DbFunctionExpression GeographyMultiLineFromText(DbExpression multiLineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownText, "multiLineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiLineFromText", new DbExpression[] { multiLineWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x0012D932 File Offset: 0x0012BB32
		public static DbFunctionExpression GeographyMultiPolygonFromText(DbExpression multiPolygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPolygonFromText", new DbExpression[] { multiPolygonWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0012D964 File Offset: 0x0012BB64
		public static DbFunctionExpression GeographyCollectionFromText(DbExpression geographyCollectionWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyCollectionFromText", new DbExpression[] { geographyCollectionWellKnownText, coordinateSystemId });
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x0012D996 File Offset: 0x0012BB96
		public static DbFunctionExpression GeographyFromBinary(DbExpression wellKnownBinaryValue)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromBinary", new DbExpression[] { wellKnownBinaryValue });
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x0012D9B8 File Offset: 0x0012BBB8
		public static DbFunctionExpression GeographyFromBinary(DbExpression wellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromBinary", new DbExpression[] { wellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x0012D9EA File Offset: 0x0012BBEA
		public static DbFunctionExpression GeographyPointFromBinary(DbExpression pointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownBinaryValue, "pointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPointFromBinary", new DbExpression[] { pointWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0012DA1C File Offset: 0x0012BC1C
		public static DbFunctionExpression GeographyLineFromBinary(DbExpression lineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownBinaryValue, "lineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyLineFromBinary", new DbExpression[] { lineWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0012DA4E File Offset: 0x0012BC4E
		public static DbFunctionExpression GeographyPolygonFromBinary(DbExpression polygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownBinaryValue, "polygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPolygonFromBinary", new DbExpression[] { polygonWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x0012DA80 File Offset: 0x0012BC80
		public static DbFunctionExpression GeographyMultiPointFromBinary(DbExpression multiPointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownBinaryValue, "multiPointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPointFromBinary", new DbExpression[] { multiPointWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x0012DAB2 File Offset: 0x0012BCB2
		public static DbFunctionExpression GeographyMultiLineFromBinary(DbExpression multiLineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownBinaryValue, "multiLineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiLineFromBinary", new DbExpression[] { multiLineWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x0012DAE4 File Offset: 0x0012BCE4
		public static DbFunctionExpression GeographyMultiPolygonFromBinary(DbExpression multiPolygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownBinaryValue, "multiPolygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPolygonFromBinary", new DbExpression[] { multiPolygonWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x0012DB16 File Offset: 0x0012BD16
		public static DbFunctionExpression GeographyCollectionFromBinary(DbExpression geographyCollectionWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geographyCollectionWellKnownBinaryValue, "geographyCollectionWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyCollectionFromBinary", new DbExpression[] { geographyCollectionWellKnownBinaryValue, coordinateSystemId });
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0012DB48 File Offset: 0x0012BD48
		public static DbFunctionExpression GeographyFromGml(DbExpression geographyMarkup)
		{
			Check.NotNull<DbExpression>(geographyMarkup, "geographyMarkup");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromGml", new DbExpression[] { geographyMarkup });
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x0012DB6A File Offset: 0x0012BD6A
		public static DbFunctionExpression GeographyFromGml(DbExpression geographyMarkup, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geographyMarkup, "geographyMarkup");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromGml", new DbExpression[] { geographyMarkup, coordinateSystemId });
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0012DB9C File Offset: 0x0012BD9C
		public static DbFunctionExpression CoordinateSystemId(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("CoordinateSystemId", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x0012DBBE File Offset: 0x0012BDBE
		public static DbFunctionExpression SpatialTypeName(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialTypeName", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0012DBE0 File Offset: 0x0012BDE0
		public static DbFunctionExpression SpatialDimension(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDimension", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0012DC02 File Offset: 0x0012BE02
		public static DbFunctionExpression SpatialEnvelope(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialEnvelope", new DbExpression[] { geometryValue });
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0012DC24 File Offset: 0x0012BE24
		public static DbFunctionExpression AsBinary(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsBinary", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x0012DC46 File Offset: 0x0012BE46
		public static DbFunctionExpression AsGml(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsGml", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x0012DC68 File Offset: 0x0012BE68
		public static DbFunctionExpression AsText(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsText", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x0012DC8A File Offset: 0x0012BE8A
		public static DbFunctionExpression IsEmptySpatial(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("IsEmptySpatial", new DbExpression[] { spatialValue });
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0012DCAC File Offset: 0x0012BEAC
		public static DbFunctionExpression IsSimpleGeometry(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsSimpleGeometry", new DbExpression[] { geometryValue });
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x0012DCCE File Offset: 0x0012BECE
		public static DbFunctionExpression SpatialBoundary(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialBoundary", new DbExpression[] { geometryValue });
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0012DCF0 File Offset: 0x0012BEF0
		public static DbFunctionExpression IsValidGeometry(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsValidGeometry", new DbExpression[] { geometryValue });
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x0012DD12 File Offset: 0x0012BF12
		public static DbFunctionExpression SpatialEquals(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialEquals", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x0012DD44 File Offset: 0x0012BF44
		public static DbFunctionExpression SpatialDisjoint(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDisjoint", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0012DD76 File Offset: 0x0012BF76
		public static DbFunctionExpression SpatialIntersects(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialIntersects", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0012DDA8 File Offset: 0x0012BFA8
		public static DbFunctionExpression SpatialTouches(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialTouches", new DbExpression[] { geometryValue1, geometryValue2 });
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0012DDDA File Offset: 0x0012BFDA
		public static DbFunctionExpression SpatialCrosses(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialCrosses", new DbExpression[] { geometryValue1, geometryValue2 });
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0012DE0C File Offset: 0x0012C00C
		public static DbFunctionExpression SpatialWithin(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialWithin", new DbExpression[] { geometryValue1, geometryValue2 });
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0012DE3E File Offset: 0x0012C03E
		public static DbFunctionExpression SpatialContains(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialContains", new DbExpression[] { geometryValue1, geometryValue2 });
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0012DE70 File Offset: 0x0012C070
		public static DbFunctionExpression SpatialOverlaps(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialOverlaps", new DbExpression[] { geometryValue1, geometryValue2 });
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0012DEA4 File Offset: 0x0012C0A4
		public static DbFunctionExpression SpatialRelate(this DbExpression geometryValue1, DbExpression geometryValue2, DbExpression intersectionPatternMatrix)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			Check.NotNull<DbExpression>(intersectionPatternMatrix, "intersectionPatternMatrix");
			return EdmFunctions.InvokeCanonicalFunction("SpatialRelate", new DbExpression[] { geometryValue1, geometryValue2, intersectionPatternMatrix });
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0012DEF1 File Offset: 0x0012C0F1
		public static DbFunctionExpression SpatialBuffer(this DbExpression spatialValue, DbExpression distance)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			Check.NotNull<DbExpression>(distance, "distance");
			return EdmFunctions.InvokeCanonicalFunction("SpatialBuffer", new DbExpression[] { spatialValue, distance });
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0012DF23 File Offset: 0x0012C123
		public static DbFunctionExpression Distance(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("Distance", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0012DF55 File Offset: 0x0012C155
		public static DbFunctionExpression SpatialConvexHull(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialConvexHull", new DbExpression[] { geometryValue });
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0012DF77 File Offset: 0x0012C177
		public static DbFunctionExpression SpatialIntersection(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialIntersection", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0012DFA9 File Offset: 0x0012C1A9
		public static DbFunctionExpression SpatialUnion(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialUnion", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0012DFDB File Offset: 0x0012C1DB
		public static DbFunctionExpression SpatialDifference(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDifference", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x0012E00D File Offset: 0x0012C20D
		public static DbFunctionExpression SpatialSymmetricDifference(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialSymmetricDifference", new DbExpression[] { spatialValue1, spatialValue2 });
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0012E03F File Offset: 0x0012C23F
		public static DbFunctionExpression SpatialElementCount(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialElementCount", new DbExpression[] { spatialValue });
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x0012E061 File Offset: 0x0012C261
		public static DbFunctionExpression SpatialElementAt(this DbExpression spatialValue, DbExpression indexValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			Check.NotNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialElementAt", new DbExpression[] { spatialValue, indexValue });
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x0012E093 File Offset: 0x0012C293
		public static DbFunctionExpression XCoordinate(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("XCoordinate", new DbExpression[] { geometryValue });
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x0012E0B5 File Offset: 0x0012C2B5
		public static DbFunctionExpression YCoordinate(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("YCoordinate", new DbExpression[] { geometryValue });
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0012E0D7 File Offset: 0x0012C2D7
		public static DbFunctionExpression Elevation(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Elevation", new DbExpression[] { spatialValue });
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0012E0F9 File Offset: 0x0012C2F9
		public static DbFunctionExpression Measure(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Measure", new DbExpression[] { spatialValue });
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0012E11B File Offset: 0x0012C31B
		public static DbFunctionExpression Latitude(this DbExpression geographyValue)
		{
			Check.NotNull<DbExpression>(geographyValue, "geographyValue");
			return EdmFunctions.InvokeCanonicalFunction("Latitude", new DbExpression[] { geographyValue });
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0012E13D File Offset: 0x0012C33D
		public static DbFunctionExpression Longitude(this DbExpression geographyValue)
		{
			Check.NotNull<DbExpression>(geographyValue, "geographyValue");
			return EdmFunctions.InvokeCanonicalFunction("Longitude", new DbExpression[] { geographyValue });
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0012E15F File Offset: 0x0012C35F
		public static DbFunctionExpression SpatialLength(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialLength", new DbExpression[] { spatialValue });
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0012E181 File Offset: 0x0012C381
		public static DbFunctionExpression StartPoint(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("StartPoint", new DbExpression[] { spatialValue });
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0012E1A3 File Offset: 0x0012C3A3
		public static DbFunctionExpression EndPoint(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("EndPoint", new DbExpression[] { spatialValue });
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0012E1C5 File Offset: 0x0012C3C5
		public static DbFunctionExpression IsClosedSpatial(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("IsClosedSpatial", new DbExpression[] { spatialValue });
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0012E1E7 File Offset: 0x0012C3E7
		public static DbFunctionExpression IsRing(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsRing", new DbExpression[] { geometryValue });
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0012E209 File Offset: 0x0012C409
		public static DbFunctionExpression PointCount(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("PointCount", new DbExpression[] { spatialValue });
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0012E22B File Offset: 0x0012C42B
		public static DbFunctionExpression PointAt(this DbExpression spatialValue, DbExpression indexValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			Check.NotNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("PointAt", new DbExpression[] { spatialValue, indexValue });
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0012E25D File Offset: 0x0012C45D
		public static DbFunctionExpression Area(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Area", new DbExpression[] { spatialValue });
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0012E27F File Offset: 0x0012C47F
		public static DbFunctionExpression Centroid(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("Centroid", new DbExpression[] { geometryValue });
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0012E2A1 File Offset: 0x0012C4A1
		public static DbFunctionExpression PointOnSurface(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("PointOnSurface", new DbExpression[] { geometryValue });
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x0012E2C3 File Offset: 0x0012C4C3
		public static DbFunctionExpression ExteriorRing(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("ExteriorRing", new DbExpression[] { geometryValue });
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0012E2E5 File Offset: 0x0012C4E5
		public static DbFunctionExpression InteriorRingCount(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("InteriorRingCount", new DbExpression[] { geometryValue });
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0012E307 File Offset: 0x0012C507
		public static DbFunctionExpression InteriorRingAt(this DbExpression geometryValue, DbExpression indexValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			Check.NotNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("InteriorRingAt", new DbExpression[] { geometryValue, indexValue });
		}
	}
}
