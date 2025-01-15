using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Spatial;
using System.Text;
using Microsoft.Mashup.Engine1.Library.Spatial;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Spatial;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200112D RID: 4397
	internal static class SpatialUtilities
	{
		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x060072DC RID: 29404 RVA: 0x0018A9EA File Offset: 0x00188BEA
		private static global::System.Spatial.WellKnownTextSqlFormatter FormatterV3
		{
			get
			{
				return global::System.Spatial.SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x060072DD RID: 29405 RVA: 0x0018A9F6 File Offset: 0x00188BF6
		private static Microsoft.Spatial.WellKnownTextSqlFormatter FormatterV4
		{
			get
			{
				return Microsoft.Spatial.SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x060072DE RID: 29406 RVA: 0x0018AA02 File Offset: 0x00188C02
		private static Microsoft.Spatial.WellKnownTextSqlFormatter FormatterV4_7
		{
			get
			{
				return Microsoft.Spatial.SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
			}
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x0018AA0E File Offset: 0x00188C0E
		public static string ToWellKnownTextV3(global::System.Spatial.Geography instance)
		{
			return SpatialUtilities.FormatterV3.Write(instance);
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x0018AA0E File Offset: 0x00188C0E
		public static string ToWellKnownTextV3(global::System.Spatial.Geometry instance)
		{
			return SpatialUtilities.FormatterV3.Write(instance);
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x0018AA1B File Offset: 0x00188C1B
		public static string ToWellKnownTextV4(Microsoft.Spatial.Geography instance)
		{
			return SpatialUtilities.FormatterV4.Write(instance);
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x0018AA1B File Offset: 0x00188C1B
		public static string ToWellKnownTextV4(Microsoft.Spatial.Geometry instance)
		{
			return SpatialUtilities.FormatterV4.Write(instance);
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x0018AA28 File Offset: 0x00188C28
		public static string ToWellKnownTextV47(Microsoft.Spatial.Geography instance)
		{
			return SpatialUtilities.FormatterV4_7.Write(instance);
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x0018AA28 File Offset: 0x00188C28
		public static string ToWellKnownTextV47(Microsoft.Spatial.Geometry instance)
		{
			return SpatialUtilities.FormatterV4_7.Write(instance);
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x0018AA38 File Offset: 0x00188C38
		public static RecordValue ToGeographyFromWellKnownText(string text)
		{
			Microsoft.Spatial.Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				try
				{
					geography = SpatialUtilities.FormatterV4_7.Read<Microsoft.Spatial.Geography>(stringReader);
				}
				catch (Microsoft.Spatial.ParseErrorException ex)
				{
					throw SpatialUtilities.SpatialInvalidFormatError(TextValue.New(text), ex.Message, ex);
				}
			}
			return SpatialUtilities.ToRecord(geography);
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x0018AA9C File Offset: 0x00188C9C
		private static RecordValue ToRecord(Microsoft.Spatial.Geography geography)
		{
			Microsoft.Spatial.GeographyPoint geographyPoint = geography as Microsoft.Spatial.GeographyPoint;
			if (geographyPoint != null)
			{
				return SpatialUtilities.ToRecordFromPoint(geographyPoint);
			}
			Microsoft.Spatial.GeographyLineString geographyLineString = geography as Microsoft.Spatial.GeographyLineString;
			if (geographyLineString != null)
			{
				return SpatialUtilities.ToRecordFromLine(geographyLineString);
			}
			Microsoft.Spatial.GeographyFullGlobe geographyFullGlobe = geography as Microsoft.Spatial.GeographyFullGlobe;
			if (geographyFullGlobe != null)
			{
				if (geographyFullGlobe.CoordinateSystem.EpsgId != null)
				{
					int? epsgId = geographyFullGlobe.CoordinateSystem.EpsgId;
					int defaultGeographyCoordinateSystemID = SpatialUtilities.DefaultGeographyCoordinateSystemID;
					if (!((epsgId.GetValueOrDefault() == defaultGeographyCoordinateSystemID) & (epsgId != null)))
					{
						return RecordValue.New(SpatialTypeValue.FullGlobeWithSRIDKeys, new Value[]
						{
							SpatialUtilities.FullGlobeTextValue,
							NumberValue.New(geographyFullGlobe.CoordinateSystem.EpsgId.Value)
						});
					}
				}
				return RecordValue.New(SpatialTypeValue.FullGlobeKeys, new Value[] { SpatialUtilities.FullGlobeTextValue });
			}
			Microsoft.Spatial.GeographyPolygon geographyPolygon = geography as Microsoft.Spatial.GeographyPolygon;
			if (geographyPolygon != null)
			{
				return SpatialUtilities.ToRecordFromPolygon(geographyPolygon);
			}
			Microsoft.Spatial.GeographyMultiPoint geographyMultiPoint = geography as Microsoft.Spatial.GeographyMultiPoint;
			if (geographyMultiPoint != null)
			{
				return SpatialUtilities.ToRecordFromMultiPoint(geographyMultiPoint);
			}
			Microsoft.Spatial.GeographyMultiLineString geographyMultiLineString = geography as Microsoft.Spatial.GeographyMultiLineString;
			if (geographyMultiLineString != null)
			{
				return SpatialUtilities.ToRecordFromMultiLine(geographyMultiLineString);
			}
			Microsoft.Spatial.GeographyMultiPolygon geographyMultiPolygon = geography as Microsoft.Spatial.GeographyMultiPolygon;
			if (geographyMultiPolygon != null)
			{
				return SpatialUtilities.ToRecordFromMultiPolygon(geographyMultiPolygon);
			}
			Microsoft.Spatial.GeographyCollection geographyCollection = geography as Microsoft.Spatial.GeographyCollection;
			if (geographyCollection != null)
			{
				return SpatialUtilities.ToRecordFromCollection(geographyCollection);
			}
			throw ValueException.NotImplemented(geography.GetType().FullName);
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x0018ABD8 File Offset: 0x00188DD8
		private static RecordValue ToRecordFromPoint(Microsoft.Spatial.GeographyPoint point)
		{
			if (point.IsEmpty)
			{
				return SpatialUtilities.CreatePointRecord(double.NaN, double.NaN, null, null, point.CoordinateSystem.EpsgId, true);
			}
			return SpatialUtilities.CreatePointRecord(point.Longitude, point.Latitude, point.Z, point.M, point.CoordinateSystem.EpsgId, true);
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x0018AC4C File Offset: 0x00188E4C
		private static RecordValue ToRecordFromLine(Microsoft.Spatial.GeographyLineString line)
		{
			if (line.CoordinateSystem.EpsgId != null && line.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeographyCoordinateSystemID)
			{
				Keys lineWithSRIDKeys = SpatialTypeValue.LineWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.LineStringTextValue;
				int num = 1;
				Value value;
				if (!line.IsEmpty)
				{
					Value[] array2 = line.Points.Select((Microsoft.Spatial.GeographyPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(line.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(lineWithSRIDKeys, array);
			}
			Keys lineKeys = SpatialTypeValue.LineKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.LineStringTextValue;
			int num2 = 1;
			Value value2;
			if (!line.IsEmpty)
			{
				Value[] array2 = line.Points.Select((Microsoft.Spatial.GeographyPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(lineKeys, array3);
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x0018AD68 File Offset: 0x00188F68
		private static RecordValue ToRecordFromPolygon(Microsoft.Spatial.GeographyPolygon polygon)
		{
			if (polygon.CoordinateSystem.EpsgId != null && polygon.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeographyCoordinateSystemID)
			{
				Keys polygonWithSRIDKeys = SpatialTypeValue.PolygonWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.PolygonTextValue;
				int num = 1;
				Value value;
				if (!polygon.IsEmpty)
				{
					Value[] array2 = polygon.Rings.Select((Microsoft.Spatial.GeographyLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(polygon.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(polygonWithSRIDKeys, array);
			}
			Keys polygonKeys = SpatialTypeValue.PolygonKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.PolygonTextValue;
			int num2 = 1;
			Value value2;
			if (!polygon.IsEmpty)
			{
				Value[] array2 = polygon.Rings.Select((Microsoft.Spatial.GeographyLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(polygonKeys, array3);
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x0018AE84 File Offset: 0x00189084
		private static RecordValue ToRecordFromMultiPoint(Microsoft.Spatial.GeographyMultiPoint multiPoint)
		{
			if (multiPoint.CoordinateSystem.EpsgId != null && multiPoint.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeographyCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.MultiPointTextValue;
				int num = 1;
				Value value;
				if (!multiPoint.IsEmpty)
				{
					Value[] array2 = multiPoint.Points.Select((Microsoft.Spatial.GeographyPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(multiPoint.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.MultiPointTextValue;
			int num2 = 1;
			Value value2;
			if (!multiPoint.IsEmpty)
			{
				Value[] array2 = multiPoint.Points.Select((Microsoft.Spatial.GeographyPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x0018AFA0 File Offset: 0x001891A0
		private static RecordValue ToRecordFromMultiLine(Microsoft.Spatial.GeographyMultiLineString multiLine)
		{
			if (multiLine.CoordinateSystem.EpsgId != null && multiLine.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeographyCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.MultiLineStringTextValue;
				int num = 1;
				Value value;
				if (!multiLine.IsEmpty)
				{
					Value[] array2 = multiLine.LineStrings.Select((Microsoft.Spatial.GeographyLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(multiLine.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.MultiLineStringTextValue;
			int num2 = 1;
			Value value2;
			if (!multiLine.IsEmpty)
			{
				Value[] array2 = multiLine.LineStrings.Select((Microsoft.Spatial.GeographyLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x0018B0BC File Offset: 0x001892BC
		private static RecordValue ToRecordFromMultiPolygon(Microsoft.Spatial.GeographyMultiPolygon multiPolygon)
		{
			if (multiPolygon.CoordinateSystem.EpsgId != null && multiPolygon.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeographyCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.MultiPolygonTextValue;
				int num = 1;
				Value value;
				if (!multiPolygon.IsEmpty)
				{
					Value[] array2 = multiPolygon.Polygons.Select((Microsoft.Spatial.GeographyPolygon p) => SpatialUtilities.ToRecordFromPolygon(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(multiPolygon.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.MultiPolygonTextValue;
			int num2 = 1;
			Value value2;
			if (!multiPolygon.IsEmpty)
			{
				Value[] array2 = multiPolygon.Polygons.Select((Microsoft.Spatial.GeographyPolygon p) => SpatialUtilities.ToRecordFromPolygon(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x0018B1D8 File Offset: 0x001893D8
		private static RecordValue ToRecordFromCollection(Microsoft.Spatial.GeographyCollection collection)
		{
			if (collection.CoordinateSystem.EpsgId != null && collection.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeographyCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.CollectionTextValue;
				int num = 1;
				Value value;
				if (!collection.IsEmpty)
				{
					Value[] array2 = collection.Geographies.Select((Microsoft.Spatial.Geography p) => SpatialUtilities.ToRecord(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(collection.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.CollectionTextValue;
			int num2 = 1;
			Value value2;
			if (!collection.IsEmpty)
			{
				Value[] array2 = collection.Geographies.Select((Microsoft.Spatial.Geography p) => SpatialUtilities.ToRecord(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x0018B2F4 File Offset: 0x001894F4
		public static RecordValue ToGeometryFromWellKnownText(string text)
		{
			Microsoft.Spatial.Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				try
				{
					geometry = SpatialUtilities.FormatterV4_7.Read<Microsoft.Spatial.Geometry>(stringReader);
				}
				catch (Microsoft.Spatial.ParseErrorException ex)
				{
					throw SpatialUtilities.SpatialInvalidFormatError(TextValue.New(text), ex.Message, ex);
				}
			}
			return SpatialUtilities.ToRecord(geometry);
		}

		// Token: 0x060072EF RID: 29423 RVA: 0x0018B358 File Offset: 0x00189558
		private static RecordValue ToRecord(Microsoft.Spatial.Geometry geometry)
		{
			Microsoft.Spatial.GeometryPoint geometryPoint = geometry as Microsoft.Spatial.GeometryPoint;
			if (geometryPoint != null)
			{
				return SpatialUtilities.ToRecordFromPoint(geometryPoint);
			}
			Microsoft.Spatial.GeometryLineString geometryLineString = geometry as Microsoft.Spatial.GeometryLineString;
			if (geometryLineString != null)
			{
				return SpatialUtilities.ToRecordFromLine(geometryLineString);
			}
			Microsoft.Spatial.GeometryPolygon geometryPolygon = geometry as Microsoft.Spatial.GeometryPolygon;
			if (geometryPolygon != null)
			{
				return SpatialUtilities.ToRecordFromPolygon(geometryPolygon);
			}
			Microsoft.Spatial.GeometryMultiPoint geometryMultiPoint = geometry as Microsoft.Spatial.GeometryMultiPoint;
			if (geometryMultiPoint != null)
			{
				return SpatialUtilities.ToRecordFromMultiPoint(geometryMultiPoint);
			}
			Microsoft.Spatial.GeometryMultiLineString geometryMultiLineString = geometry as Microsoft.Spatial.GeometryMultiLineString;
			if (geometryMultiLineString != null)
			{
				return SpatialUtilities.ToRecordFromMultiLine(geometryMultiLineString);
			}
			Microsoft.Spatial.GeometryMultiPolygon geometryMultiPolygon = geometry as Microsoft.Spatial.GeometryMultiPolygon;
			if (geometryMultiPolygon != null)
			{
				return SpatialUtilities.ToRecordFromMultiPolygon(geometryMultiPolygon);
			}
			Microsoft.Spatial.GeometryCollection geometryCollection = geometry as Microsoft.Spatial.GeometryCollection;
			if (geometryCollection != null)
			{
				return SpatialUtilities.ToRecordFromCollection(geometryCollection);
			}
			throw ValueException.NotImplemented(geometry.GetType().FullName);
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x0018B3F8 File Offset: 0x001895F8
		private static RecordValue ToRecordFromPoint(Microsoft.Spatial.GeometryPoint point)
		{
			if (point.IsEmpty)
			{
				return SpatialUtilities.CreatePointRecord(double.NaN, double.NaN, null, null, point.CoordinateSystem.EpsgId, false);
			}
			return SpatialUtilities.CreatePointRecord(point.X, point.Y, point.Z, point.M, point.CoordinateSystem.EpsgId, false);
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x0018B46C File Offset: 0x0018966C
		private static RecordValue ToRecordFromLine(Microsoft.Spatial.GeometryLineString line)
		{
			if (line.CoordinateSystem.EpsgId != null && line.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeometryCoordinateSystemID)
			{
				Keys lineWithSRIDKeys = SpatialTypeValue.LineWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.LineStringTextValue;
				int num = 1;
				Value value;
				if (!line.IsEmpty)
				{
					Value[] array2 = line.Points.Select((Microsoft.Spatial.GeometryPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(line.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(lineWithSRIDKeys, array);
			}
			Keys lineKeys = SpatialTypeValue.LineKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.LineStringTextValue;
			int num2 = 1;
			Value value2;
			if (!line.IsEmpty)
			{
				Value[] array2 = line.Points.Select((Microsoft.Spatial.GeometryPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(lineKeys, array3);
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x0018B588 File Offset: 0x00189788
		private static RecordValue ToRecordFromPolygon(Microsoft.Spatial.GeometryPolygon polygon)
		{
			if (polygon.CoordinateSystem.EpsgId != null && polygon.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeometryCoordinateSystemID)
			{
				Keys polygonWithSRIDKeys = SpatialTypeValue.PolygonWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.PolygonTextValue;
				int num = 1;
				Value value;
				if (!polygon.IsEmpty)
				{
					Value[] array2 = polygon.Rings.Select((Microsoft.Spatial.GeometryLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(polygon.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(polygonWithSRIDKeys, array);
			}
			Keys polygonKeys = SpatialTypeValue.PolygonKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.PolygonTextValue;
			int num2 = 1;
			Value value2;
			if (!polygon.IsEmpty)
			{
				Value[] array2 = polygon.Rings.Select((Microsoft.Spatial.GeometryLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(polygonKeys, array3);
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x0018B6A4 File Offset: 0x001898A4
		private static RecordValue ToRecordFromMultiPoint(Microsoft.Spatial.GeometryMultiPoint multiPoint)
		{
			if (multiPoint.CoordinateSystem.EpsgId != null && multiPoint.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeometryCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.MultiPointTextValue;
				int num = 1;
				Value value;
				if (!multiPoint.IsEmpty)
				{
					Value[] array2 = multiPoint.Points.Select((Microsoft.Spatial.GeometryPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(multiPoint.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.MultiPointTextValue;
			int num2 = 1;
			Value value2;
			if (!multiPoint.IsEmpty)
			{
				Value[] array2 = multiPoint.Points.Select((Microsoft.Spatial.GeometryPoint p) => SpatialUtilities.ToRecordFromPoint(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x0018B7C0 File Offset: 0x001899C0
		private static RecordValue ToRecordFromMultiLine(Microsoft.Spatial.GeometryMultiLineString multiLine)
		{
			if (multiLine.CoordinateSystem.EpsgId != null && multiLine.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeometryCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.MultiLineStringTextValue;
				int num = 1;
				Value value;
				if (!multiLine.IsEmpty)
				{
					Value[] array2 = multiLine.LineStrings.Select((Microsoft.Spatial.GeometryLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(multiLine.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.MultiLineStringTextValue;
			int num2 = 1;
			Value value2;
			if (!multiLine.IsEmpty)
			{
				Value[] array2 = multiLine.LineStrings.Select((Microsoft.Spatial.GeometryLineString p) => SpatialUtilities.ToRecordFromLine(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072F5 RID: 29429 RVA: 0x0018B8DC File Offset: 0x00189ADC
		private static RecordValue ToRecordFromMultiPolygon(Microsoft.Spatial.GeometryMultiPolygon multiPolygon)
		{
			if (multiPolygon.CoordinateSystem.EpsgId != null && multiPolygon.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeometryCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.MultiPolygonTextValue;
				int num = 1;
				Value value;
				if (!multiPolygon.IsEmpty)
				{
					Value[] array2 = multiPolygon.Polygons.Select((Microsoft.Spatial.GeometryPolygon p) => SpatialUtilities.ToRecordFromPolygon(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(multiPolygon.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.MultiPolygonTextValue;
			int num2 = 1;
			Value value2;
			if (!multiPolygon.IsEmpty)
			{
				Value[] array2 = multiPolygon.Polygons.Select((Microsoft.Spatial.GeometryPolygon p) => SpatialUtilities.ToRecordFromPolygon(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x0018B9F8 File Offset: 0x00189BF8
		private static RecordValue ToRecordFromCollection(Microsoft.Spatial.GeometryCollection collection)
		{
			if (collection.CoordinateSystem.EpsgId != null && collection.CoordinateSystem.EpsgId.Value != SpatialUtilities.DefaultGeometryCoordinateSystemID)
			{
				Keys collectionWithSRIDKeys = SpatialTypeValue.CollectionWithSRIDKeys;
				Value[] array = new Value[3];
				array[0] = SpatialUtilities.CollectionTextValue;
				int num = 1;
				Value value;
				if (!collection.IsEmpty)
				{
					Value[] array2 = collection.Geometries.Select((Microsoft.Spatial.Geometry p) => SpatialUtilities.ToRecord(p)).ToArray<RecordValue>();
					value = ListValue.New(array2);
				}
				else
				{
					value = ListValue.Empty;
				}
				array[num] = value;
				array[2] = NumberValue.New(collection.CoordinateSystem.EpsgId.Value);
				return RecordValue.New(collectionWithSRIDKeys, array);
			}
			Keys collectionKeys = SpatialTypeValue.CollectionKeys;
			Value[] array3 = new Value[2];
			array3[0] = SpatialUtilities.CollectionTextValue;
			int num2 = 1;
			Value value2;
			if (!collection.IsEmpty)
			{
				Value[] array2 = collection.Geometries.Select((Microsoft.Spatial.Geometry p) => SpatialUtilities.ToRecord(p)).ToArray<RecordValue>();
				value2 = ListValue.New(array2);
			}
			else
			{
				value2 = ListValue.Empty;
			}
			array3[num2] = value2;
			return RecordValue.New(collectionKeys, array3);
		}

		// Token: 0x060072F7 RID: 29431 RVA: 0x0018BB14 File Offset: 0x00189D14
		public static RecordValue CreateGeographyPoint(double longitude, double latitude, double? z, double? m, int? srid)
		{
			RecordValue recordValue;
			try
			{
				Microsoft.Spatial.GeographyPoint geographyPoint = Microsoft.Spatial.GeographyPoint.Create(Microsoft.Spatial.CoordinateSystem.Geography(srid), latitude, longitude, z, m);
				recordValue = SpatialUtilities.CreatePointRecord(geographyPoint.Longitude, geographyPoint.Latitude, geographyPoint.Z, geographyPoint.M, geographyPoint.CoordinateSystem.EpsgId, true);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.InvalidArguments(SpatialModule.GeographyPointFrom, new Value[] { TextValue.New(ex.Message) });
			}
			return recordValue;
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x0018BB90 File Offset: 0x00189D90
		public static RecordValue CreateGeometryPointRecord(double x, double y, double? z, double? m, int? srid)
		{
			RecordValue recordValue;
			try
			{
				Microsoft.Spatial.GeometryPoint geometryPoint = Microsoft.Spatial.GeometryPoint.Create(Microsoft.Spatial.CoordinateSystem.Geometry(srid), x, y, z, m);
				recordValue = SpatialUtilities.CreatePointRecord(geometryPoint.X, geometryPoint.Y, geometryPoint.Z, geometryPoint.M, geometryPoint.CoordinateSystem.EpsgId, false);
			}
			catch (ArgumentException ex)
			{
				throw ValueException.InvalidArguments(SpatialModule.GeometryPointFrom, new Value[] { TextValue.New(ex.Message) });
			}
			return recordValue;
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x0018BC0C File Offset: 0x00189E0C
		public static string ToWellKnownText(RecordValue record, bool isGeography, bool omitSrid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int? num = SpatialUtilities.ToWellKnownTextImpl(record, stringBuilder, isGeography);
			if (omitSrid || num == null)
			{
				return stringBuilder.ToString();
			}
			return "SRID=" + num.Value.ToString(CultureInfo.InvariantCulture) + ";" + stringBuilder.ToString();
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x0018BC64 File Offset: 0x00189E64
		private static int? ToWellKnownTextImpl(RecordValue record, StringBuilder builder, bool isGeography)
		{
			Value value;
			if (!record.TryGetValue("Kind", out value))
			{
				throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInvalidRecordFormat, record);
			}
			if (!value.IsText)
			{
				throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInvalidRecordFormat, record);
			}
			string text = value.AsString.ToUpperInvariant();
			builder.Append(text);
			if (text != null)
			{
				int num = text.Length;
				bool flag;
				string text2;
				Func<RecordValue, bool, bool> func;
				Func<RecordValue, StringBuilder, bool, int?> func2;
				switch (num)
				{
				case 5:
				{
					if (!(text == "POINT"))
					{
						goto IL_0358;
					}
					if (!SpatialUtilities.IsPoint(record, isGeography))
					{
						throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInvalidRecordFormat, record);
					}
					if (double.IsNaN(record[isGeography ? "Longitude" : "X"].AsNumber.AsDouble) && double.IsNaN(record[isGeography ? "Latitude" : "Y"].AsNumber.AsDouble))
					{
						return SpatialUtilities.WriteEmpty(record, builder);
					}
					builder.Append("(");
					int? num2 = SpatialUtilities.WritePointContents(record, builder, isGeography);
					builder.Append(")");
					return num2;
				}
				case 6:
				case 8:
				case 11:
				case 13:
				case 14:
					goto IL_0358;
				case 7:
					if (!(text == "POLYGON"))
					{
						goto IL_0358;
					}
					flag = false;
					text2 = "Rings";
					func = new Func<RecordValue, bool, bool>(SpatialUtilities.IsPolygon);
					func2 = new Func<RecordValue, StringBuilder, bool, int?>(SpatialUtilities.WritePolygonContents);
					break;
				case 9:
					if (!(text == "FULLGLOBE"))
					{
						goto IL_0358;
					}
					if (isGeography)
					{
						if (record.Keys.Length == 1)
						{
							return null;
						}
						Value value2;
						if (record.Keys.Length == 2 && record.TryGetValue("SRID", out value2) && value2.IsNumber)
						{
							return new int?(value2.AsNumber.AsInteger32);
						}
					}
					throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInvalidRecordFormat, record);
				case 10:
				{
					char c = text[0];
					if (c != 'L')
					{
						if (c != 'M')
						{
							goto IL_0358;
						}
						if (!(text == "MULTIPOINT"))
						{
							goto IL_0358;
						}
						flag = true;
						text2 = "Components";
						func = new Func<RecordValue, bool, bool>(SpatialUtilities.IsPoint);
						func2 = new Func<RecordValue, StringBuilder, bool, int?>(SpatialUtilities.WritePointContents);
					}
					else
					{
						if (!(text == "LINESTRING"))
						{
							goto IL_0358;
						}
						flag = false;
						text2 = "Points";
						func = new Func<RecordValue, bool, bool>(SpatialUtilities.IsLine);
						func2 = new Func<RecordValue, StringBuilder, bool, int?>(SpatialUtilities.WriteLineContents);
					}
					break;
				}
				case 12:
					if (!(text == "MULTIPOLYGON"))
					{
						goto IL_0358;
					}
					flag = true;
					text2 = "Components";
					func = new Func<RecordValue, bool, bool>(SpatialUtilities.IsPolygon);
					func2 = new Func<RecordValue, StringBuilder, bool, int?>(SpatialUtilities.WritePolygonContents);
					break;
				case 15:
					if (!(text == "MULTILINESTRING"))
					{
						goto IL_0358;
					}
					flag = true;
					text2 = "Components";
					func = new Func<RecordValue, bool, bool>(SpatialUtilities.IsLine);
					func2 = new Func<RecordValue, StringBuilder, bool, int?>(SpatialUtilities.WriteLineContents);
					break;
				default:
					if (num != 18)
					{
						goto IL_0358;
					}
					if (!(text == "GEOMETRYCOLLECTION"))
					{
						goto IL_0358;
					}
					flag = true;
					text2 = "Components";
					func = (RecordValue r, bool g) => true;
					func2 = new Func<RecordValue, StringBuilder, bool, int?>(SpatialUtilities.ToWellKnownTextImpl);
					break;
				}
				ListValue asList = record[text2].AsList;
				if (asList.Count == 0)
				{
					return SpatialUtilities.WriteEmpty(record, builder);
				}
				builder.Append("(");
				if (flag && SpatialUtilities.IsCollection(record, func, isGeography))
				{
					bool flag2 = false;
					HashSet<int> hashSet = new HashSet<int>();
					Value value2;
					if (record.TryGetValue("SRID", out value2) && value2.IsNumber)
					{
						hashSet.Add(value2.AsNumber.AsInteger32);
					}
					foreach (IValueReference valueReference in asList)
					{
						if (flag2)
						{
							builder.Append(", ");
						}
						if (text != "GEOMETRYCOLLECTION")
						{
							builder.Append("(");
						}
						int? num3 = func2(valueReference.Value.AsRecord, builder, isGeography);
						if (text != "GEOMETRYCOLLECTION")
						{
							builder.Append(")");
						}
						if (num3 != null)
						{
							hashSet.Add(num3.Value);
						}
						flag2 = true;
					}
					builder.Append(")");
					num = hashSet.Count;
					if (num == 0)
					{
						return null;
					}
					if (num != 1)
					{
						throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInconsistenSRID(string.Join(", ", hashSet.Select((int s) => s.ToString(CultureInfo.InvariantCulture)).ToArray<string>())), record);
					}
					return new int?(hashSet.Single<int>());
				}
				else
				{
					if (!flag && func(record, isGeography))
					{
						int? num4 = func2(record, builder, isGeography);
						builder.Append(")");
						return num4;
					}
					throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInvalidRecordFormat, record);
				}
			}
			IL_0358:
			throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInvalidToken(text), record);
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x0018C1B4 File Offset: 0x0018A3B4
		private static bool IsPoint(RecordValue record, bool isGeography)
		{
			int length = record.Keys.Length;
			if (length < 3 || length > 6)
			{
				return false;
			}
			Value value;
			if (!record.TryGetValue("Kind", out value))
			{
				return false;
			}
			if (!value.IsText || value.AsString.ToUpperInvariant() != "POINT")
			{
				return false;
			}
			if (isGeography)
			{
				if (!record.Keys.Contains("Longitude"))
				{
					return false;
				}
				if (!record.Keys.Contains("Latitude"))
				{
					return false;
				}
				if (record.Keys.Contains("X"))
				{
					return false;
				}
				if (record.Keys.Contains("Y"))
				{
					return false;
				}
			}
			else
			{
				if (!record.Keys.Contains("X"))
				{
					return false;
				}
				if (!record.Keys.Contains("Y"))
				{
					return false;
				}
				if (record.Keys.Contains("Longitude"))
				{
					return false;
				}
				if (record.Keys.Contains("Latitude"))
				{
					return false;
				}
			}
			foreach (NamedValue namedValue in record.GetFields())
			{
				string key = namedValue.Key;
				if (key != null)
				{
					int length2 = key.Length;
					if (length2 <= 4)
					{
						if (length2 != 1)
						{
							if (length2 != 4)
							{
								goto IL_01D6;
							}
							char c = key[0];
							if (c != 'K')
							{
								if (c != 'S')
								{
									goto IL_01D6;
								}
								if (!(key == "SRID"))
								{
									goto IL_01D6;
								}
							}
							else
							{
								if (!(key == "Kind"))
								{
									goto IL_01D6;
								}
								continue;
							}
						}
						else
						{
							char c = key[0];
							if (c != 'M')
							{
								switch (c)
								{
								case 'X':
								case 'Y':
								case 'Z':
									break;
								default:
									goto IL_01D6;
								}
							}
						}
					}
					else if (length2 != 8)
					{
						if (length2 != 9)
						{
							goto IL_01D6;
						}
						if (!(key == "Longitude"))
						{
							goto IL_01D6;
						}
					}
					else if (!(key == "Latitude"))
					{
						goto IL_01D6;
					}
					if (!namedValue.Value.IsNumber)
					{
						return false;
					}
					continue;
				}
				IL_01D6:
				return false;
			}
			return true;
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x0018C3CC File Offset: 0x0018A5CC
		private static bool IsLine(RecordValue record, bool isGeography)
		{
			int length = record.Keys.Length;
			if (length < 2 || length > 3)
			{
				return false;
			}
			Value value;
			if (!record.TryGetValue("Kind", out value))
			{
				return false;
			}
			if (!value.IsText || value.AsString.ToUpperInvariant() != "LINESTRING")
			{
				return false;
			}
			if (length == 3)
			{
				Value value2;
				if (!record.TryGetValue("SRID", out value2))
				{
					return false;
				}
				if (!value2.IsNumber)
				{
					return false;
				}
			}
			Value value3;
			return record.TryGetValue("Points", out value3) && value3.IsList && value3.AsList.All((IValueReference r) => r.Value.IsRecord && SpatialUtilities.IsPoint(r.Value.AsRecord, isGeography));
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x0018C480 File Offset: 0x0018A680
		private static bool IsValidLineStringForPolygon(RecordValue record, bool isGeography)
		{
			if (!SpatialUtilities.IsLine(record, isGeography))
			{
				return false;
			}
			ListValue asList = record["Points"].AsList;
			int count = asList.Count;
			if (count < 4)
			{
				return false;
			}
			RecordValue asRecord = asList[0].AsRecord;
			RecordValue asRecord2 = asList[count - 1].AsRecord;
			if (isGeography)
			{
				return asRecord["Longitude"].Equals(asRecord2["Longitude"]) && asRecord["Latitude"].Equals(asRecord2["Latitude"]);
			}
			return asRecord["X"].Equals(asRecord2["X"]) && asRecord["Y"].Equals(asRecord2["Y"]);
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x0018C54C File Offset: 0x0018A74C
		private static bool IsPolygon(RecordValue record, bool isGeography)
		{
			int length = record.Keys.Length;
			if (length < 2 || length > 3)
			{
				return false;
			}
			Value value;
			if (!record.TryGetValue("Kind", out value))
			{
				return false;
			}
			if (!value.IsText || value.AsString.ToUpperInvariant() != "POLYGON")
			{
				return false;
			}
			if (length == 3)
			{
				Value value2;
				if (!record.TryGetValue("SRID", out value2))
				{
					return false;
				}
				if (!value2.IsNumber)
				{
					return false;
				}
			}
			Value value3;
			return record.TryGetValue("Rings", out value3) && value3.IsList && value3.AsList.All((IValueReference r) => r.Value.IsRecord && SpatialUtilities.IsValidLineStringForPolygon(r.Value.AsRecord, isGeography));
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x0018C600 File Offset: 0x0018A800
		private static bool IsCollection(RecordValue record, Func<RecordValue, bool, bool> validationFunction, bool isGeography)
		{
			int length = record.Keys.Length;
			if (length < 2 || length > 3)
			{
				return false;
			}
			Value value;
			if (!record.TryGetValue("Kind", out value))
			{
				return false;
			}
			if (!value.IsText || !SpatialUtilities.CollectionNameSet.Contains(value.AsString))
			{
				return false;
			}
			if (length == 3)
			{
				Value value2;
				if (!record.TryGetValue("SRID", out value2))
				{
					return false;
				}
				if (!value2.IsNumber)
				{
					return false;
				}
			}
			Value value3;
			return record.TryGetValue("Components", out value3) && value3.IsList && value3.AsList.All((IValueReference r) => r.Value.IsRecord && validationFunction(r.Value.AsRecord, isGeography));
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x0018C6B8 File Offset: 0x0018A8B8
		private static int? WriteEmpty(RecordValue record, StringBuilder builder)
		{
			builder.Append(" ");
			builder.Append("EMPTY");
			Value value;
			if (record.TryGetValue("SRID", out value))
			{
				return new int?(value.AsNumber.AsInteger32);
			}
			return null;
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x0018C708 File Offset: 0x0018A908
		private static int? WritePointContents(RecordValue record, StringBuilder builder, bool isGeography)
		{
			builder.Append(record[isGeography ? "Longitude" : "X"].AsNumber.AsDouble.ToString("G", CultureInfo.InvariantCulture));
			builder.Append(" ");
			builder.Append(record[isGeography ? "Latitude" : "Y"].AsNumber.AsDouble.ToString("G", CultureInfo.InvariantCulture));
			Value value;
			bool flag = record.TryGetValue("Z", out value);
			if (flag)
			{
				builder.Append(" ");
				builder.Append(value.AsNumber.AsDouble.ToString("G", CultureInfo.InvariantCulture));
			}
			Value value2;
			if (record.TryGetValue("M", out value2))
			{
				if (!flag)
				{
					builder.Append(" null");
				}
				builder.Append(" ");
				builder.Append(value2.AsNumber.AsDouble.ToString("G", CultureInfo.InvariantCulture));
			}
			Value value3;
			if (record.TryGetValue("SRID", out value3))
			{
				return new int?(value3.AsNumber.AsInteger32);
			}
			return null;
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x0018C850 File Offset: 0x0018AA50
		private static int? WriteLineContents(RecordValue record, StringBuilder builder, bool isGeography)
		{
			HashSet<int> hashSet = new HashSet<int>();
			Value value;
			if (record.TryGetValue("SRID", out value) && value.IsNumber)
			{
				hashSet.Add(value.AsNumber.AsInteger32);
			}
			bool flag = false;
			foreach (IValueReference valueReference in record["Points"].AsList)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				int? num = SpatialUtilities.WritePointContents(valueReference.Value.AsRecord, builder, isGeography);
				if (num != null)
				{
					hashSet.Add(num.Value);
				}
				flag = true;
			}
			int count = hashSet.Count;
			if (count == 0)
			{
				return null;
			}
			if (count != 1)
			{
				throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInconsistenSRID(string.Join(", ", hashSet.Select((int s) => s.ToString(CultureInfo.InvariantCulture)).ToArray<string>())), record);
			}
			return new int?(hashSet.Single<int>());
		}

		// Token: 0x06007303 RID: 29443 RVA: 0x0018C97C File Offset: 0x0018AB7C
		private static int? WritePolygonContents(RecordValue record, StringBuilder builder, bool isGeography)
		{
			HashSet<int> hashSet = new HashSet<int>();
			Value value;
			if (record.TryGetValue("SRID", out value) && value.IsNumber)
			{
				hashSet.Add(value.AsNumber.AsInteger32);
			}
			bool flag = false;
			foreach (IValueReference valueReference in record["Rings"].AsList)
			{
				if (flag)
				{
					builder.Append(", ");
				}
				builder.Append("(");
				int? num = SpatialUtilities.WriteLineContents(valueReference.Value.AsRecord, builder, isGeography);
				if (num != null)
				{
					hashSet.Add(num.Value);
				}
				builder.Append(")");
				flag = true;
			}
			int count = hashSet.Count;
			if (count == 0)
			{
				return null;
			}
			if (count != 1)
			{
				throw SpatialUtilities.SpatialInvalidRecordError(Strings.SpatialInconsistenSRID(string.Join(", ", hashSet.Select((int s) => s.ToString(CultureInfo.InvariantCulture)).ToArray<string>())), record);
			}
			return new int?(hashSet.Single<int>());
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x0018CAC0 File Offset: 0x0018ACC0
		public static RecordValue CreatePointRecord(double longitude, double latitude, double? z, double? m, int? srid, bool isGeography)
		{
			List<Value> list = new List<Value>(6);
			int num = (isGeography ? SpatialUtilities.DefaultGeographyCoordinateSystemID : SpatialUtilities.DefaultGeometryCoordinateSystemID);
			bool flag = srid != null && srid.Value != num;
			list.Add(SpatialUtilities.PointTextValue);
			list.Add(NumberValue.New(longitude));
			list.Add(NumberValue.New(latitude));
			if (z != null)
			{
				list.Add(NumberValue.New(z.Value));
			}
			if (m != null)
			{
				list.Add(NumberValue.New(m.Value));
			}
			if (flag)
			{
				list.Add(NumberValue.New(srid.Value));
			}
			return RecordValue.New(isGeography ? ((z != null) ? ((m != null) ? (flag ? SpatialTypeValue.GeographyPointZMSridKeys : SpatialTypeValue.GeographyPointZMKeys) : (flag ? SpatialTypeValue.GeographyPointZSridKeys : SpatialTypeValue.GeographyPointZKeys)) : ((m != null) ? (flag ? SpatialTypeValue.GeographyPointMSridKeys : SpatialTypeValue.GeographyPointMKeys) : (flag ? SpatialTypeValue.GeographyPointSridKeys : SpatialTypeValue.GeographyPointKeys))) : ((z != null) ? ((m != null) ? (flag ? SpatialTypeValue.GeometryPointZMSridKeys : SpatialTypeValue.GeometryPointZMKeys) : (flag ? SpatialTypeValue.GeometryPointZSridKeys : SpatialTypeValue.GeometryPointZKeys)) : ((m != null) ? (flag ? SpatialTypeValue.GeometryPointMSridKeys : SpatialTypeValue.GeometryPointMKeys) : (flag ? SpatialTypeValue.GeometryPointSridKeys : SpatialTypeValue.GeometryPointKeys))), list.ToArray());
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x0018CC44 File Offset: 0x0018AE44
		public static ValueException SpatialInvalidFormatError(TextValue detail, string input, Exception exception = null)
		{
			return ValueException.NewDataFormatError(Strings.SpatialArgumentInvalidWKT(input), detail, exception);
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x000F4153 File Offset: 0x000F2353
		public static ValueException SpatialInvalidRecordError(string message, Value detail)
		{
			return ValueException.NewDataFormatError(message, detail, null);
		}

		// Token: 0x04003F4D RID: 16205
		private const string PointLiteral = "POINT";

		// Token: 0x04003F4E RID: 16206
		private const string LineStringLiteral = "LINESTRING";

		// Token: 0x04003F4F RID: 16207
		private const string PolygonLiteral = "POLYGON";

		// Token: 0x04003F50 RID: 16208
		private const string FullGlobeLiteral = "FULLGLOBE";

		// Token: 0x04003F51 RID: 16209
		private const string MultiPointLiteral = "MULTIPOINT";

		// Token: 0x04003F52 RID: 16210
		private const string MultiLineStringLiteral = "MULTILINESTRING";

		// Token: 0x04003F53 RID: 16211
		private const string MultiPolygonLiteral = "MULTIPOLYGON";

		// Token: 0x04003F54 RID: 16212
		private const string CollectionLiteral = "GEOMETRYCOLLECTION";

		// Token: 0x04003F55 RID: 16213
		private const string SRIDLiteral = "SRID";

		// Token: 0x04003F56 RID: 16214
		private const string EmptyLiteral = "EMPTY";

		// Token: 0x04003F57 RID: 16215
		public static readonly int DefaultGeographyCoordinateSystemID = Microsoft.Spatial.CoordinateSystem.DefaultGeography.EpsgId.Value;

		// Token: 0x04003F58 RID: 16216
		public static readonly int DefaultGeometryCoordinateSystemID = Microsoft.Spatial.CoordinateSystem.DefaultGeometry.EpsgId.Value;

		// Token: 0x04003F59 RID: 16217
		private static readonly TextValue PointTextValue = TextValue.New("POINT");

		// Token: 0x04003F5A RID: 16218
		private static readonly TextValue LineStringTextValue = TextValue.New("LINESTRING");

		// Token: 0x04003F5B RID: 16219
		private static readonly TextValue PolygonTextValue = TextValue.New("POLYGON");

		// Token: 0x04003F5C RID: 16220
		private static readonly TextValue FullGlobeTextValue = TextValue.New("FULLGLOBE");

		// Token: 0x04003F5D RID: 16221
		private static readonly TextValue MultiPointTextValue = TextValue.New("MULTIPOINT");

		// Token: 0x04003F5E RID: 16222
		private static readonly TextValue MultiLineStringTextValue = TextValue.New("MULTILINESTRING");

		// Token: 0x04003F5F RID: 16223
		private static readonly TextValue MultiPolygonTextValue = TextValue.New("MULTIPOLYGON");

		// Token: 0x04003F60 RID: 16224
		private static readonly TextValue CollectionTextValue = TextValue.New("GEOMETRYCOLLECTION");

		// Token: 0x04003F61 RID: 16225
		private static readonly HashSet<string> CollectionNameSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "GEOMETRYCOLLECTION", "MULTIPOINT", "MULTILINESTRING", "MULTIPOLYGON" };
	}
}
