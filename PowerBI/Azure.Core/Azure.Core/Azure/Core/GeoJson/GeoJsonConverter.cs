using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000AE RID: 174
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class GeoJsonConverter : JsonConverter<GeoObject>
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x000108B3 File Offset: 0x0000EAB3
		public override bool CanConvert(Type typeToConvert)
		{
			return typeof(GeoObject).IsAssignableFrom(typeToConvert);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000108C5 File Offset: 0x0000EAC5
		public override GeoObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return GeoJsonConverter.Read(JsonDocument.ParseValue(ref reader).RootElement);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000108D7 File Offset: 0x0000EAD7
		public override void Write(Utf8JsonWriter writer, GeoObject value, JsonSerializerOptions options)
		{
			GeoJsonConverter.Write(writer, value);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000108E0 File Offset: 0x0000EAE0
		internal static GeoObject Read(JsonElement element)
		{
			string @string = GeoJsonConverter.GetRequiredProperty(element, "type").GetString();
			GeoBoundingBox geoBoundingBox = GeoJsonConverter.ReadBoundingBox(in element);
			JsonElement.ArrayEnumerator arrayEnumerator;
			if (@string == "GeometryCollection")
			{
				List<GeoObject> list = new List<GeoObject>();
				arrayEnumerator = GeoJsonConverter.GetRequiredProperty(element, "geometries").EnumerateArray();
				foreach (JsonElement jsonElement in arrayEnumerator)
				{
					list.Add(GeoJsonConverter.Read(jsonElement));
				}
				return new GeoCollection(list, geoBoundingBox, GeoJsonConverter.ReadAdditionalProperties(in element, "geometries"));
			}
			IReadOnlyDictionary<string, object> readOnlyDictionary = GeoJsonConverter.ReadAdditionalProperties(in element, "coordinates");
			JsonElement requiredProperty = GeoJsonConverter.GetRequiredProperty(element, "coordinates");
			if (@string == "Point")
			{
				return new GeoPoint(GeoJsonConverter.ReadCoordinate(requiredProperty), geoBoundingBox, readOnlyDictionary);
			}
			if (@string == "LineString")
			{
				return new GeoLineString(GeoJsonConverter.ReadCoordinates(requiredProperty), geoBoundingBox, readOnlyDictionary);
			}
			if (@string == "MultiPoint")
			{
				List<GeoPoint> list2 = new List<GeoPoint>();
				foreach (GeoPosition geoPosition in GeoJsonConverter.ReadCoordinates(requiredProperty))
				{
					list2.Add(new GeoPoint(geoPosition, null, GeoObject.DefaultProperties));
				}
				return new GeoPointCollection(list2, geoBoundingBox, readOnlyDictionary);
			}
			if (@string == "Polygon")
			{
				List<GeoLinearRing> list3 = new List<GeoLinearRing>();
				arrayEnumerator = requiredProperty.EnumerateArray();
				foreach (JsonElement jsonElement2 in arrayEnumerator)
				{
					list3.Add(new GeoLinearRing(GeoJsonConverter.ReadCoordinates(jsonElement2)));
				}
				return new GeoPolygon(list3, geoBoundingBox, readOnlyDictionary);
			}
			if (@string == "MultiLineString")
			{
				List<GeoLineString> list4 = new List<GeoLineString>();
				arrayEnumerator = requiredProperty.EnumerateArray();
				foreach (JsonElement jsonElement3 in arrayEnumerator)
				{
					list4.Add(new GeoLineString(GeoJsonConverter.ReadCoordinates(jsonElement3), null, GeoObject.DefaultProperties));
				}
				return new GeoLineStringCollection(list4, geoBoundingBox, readOnlyDictionary);
			}
			if (!(@string == "MultiPolygon"))
			{
				throw new NotSupportedException("Unsupported geometry type '" + @string + "' ");
			}
			List<GeoPolygon> list5 = new List<GeoPolygon>();
			arrayEnumerator = requiredProperty.EnumerateArray();
			foreach (JsonElement jsonElement4 in arrayEnumerator)
			{
				List<GeoLinearRing> list6 = new List<GeoLinearRing>();
				foreach (JsonElement jsonElement5 in jsonElement4.EnumerateArray())
				{
					list6.Add(new GeoLinearRing(GeoJsonConverter.ReadCoordinates(jsonElement5)));
				}
				list5.Add(new GeoPolygon(list6));
			}
			return new GeoPolygonCollection(list5, geoBoundingBox, readOnlyDictionary);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00010C2C File Offset: 0x0000EE2C
		[NullableContext(2)]
		private static GeoBoundingBox ReadBoundingBox(in JsonElement element)
		{
			GeoBoundingBox geoBoundingBox = null;
			JsonElement jsonElement;
			if (element.TryGetProperty("bbox", ref jsonElement))
			{
				int arrayLength = jsonElement.GetArrayLength();
				if (arrayLength != 4)
				{
					if (arrayLength != 6)
					{
						throw new JsonException("Only 2 or 3 element coordinates supported");
					}
					geoBoundingBox = new GeoBoundingBox(jsonElement[0].GetDouble(), jsonElement[1].GetDouble(), jsonElement[3].GetDouble(), jsonElement[4].GetDouble(), new double?(jsonElement[2].GetDouble()), new double?(jsonElement[5].GetDouble()));
				}
				else
				{
					geoBoundingBox = new GeoBoundingBox(jsonElement[0].GetDouble(), jsonElement[1].GetDouble(), jsonElement[2].GetDouble(), jsonElement[3].GetDouble());
				}
			}
			return geoBoundingBox;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00010D28 File Offset: 0x0000EF28
		[return: Nullable(new byte[] { 1, 1, 2 })]
		private static IReadOnlyDictionary<string, object> ReadAdditionalProperties(in JsonElement element, string knownProperty = "coordinates")
		{
			Dictionary<string, object> dictionary = null;
			foreach (JsonProperty jsonProperty in element.EnumerateObject())
			{
				string name = jsonProperty.Name;
				if (!name.Equals("type", StringComparison.Ordinal) && !name.Equals("bbox", StringComparison.Ordinal) && !name.Equals(knownProperty, StringComparison.Ordinal))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>();
					}
					Dictionary<string, object> dictionary2 = dictionary;
					string text = name;
					JsonElement value = jsonProperty.Value;
					dictionary2.Add(text, GeoJsonConverter.ReadAdditionalPropertyValue(in value));
				}
			}
			IReadOnlyDictionary<string, object> readOnlyDictionary = dictionary;
			return readOnlyDictionary ?? GeoObject.DefaultProperties;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00010DDC File Offset: 0x0000EFDC
		[NullableContext(2)]
		private static object ReadAdditionalPropertyValue(in JsonElement element)
		{
			switch (element.ValueKind)
			{
			case 0:
			case 7:
				return null;
			case 1:
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (JsonProperty jsonProperty in element.EnumerateObject())
				{
					Dictionary<string, object> dictionary2 = dictionary;
					string name = jsonProperty.Name;
					JsonElement value = jsonProperty.Value;
					dictionary2.Add(name, GeoJsonConverter.ReadAdditionalPropertyValue(in value));
				}
				return dictionary;
			}
			case 2:
			{
				List<object> list = new List<object>();
				foreach (JsonElement jsonElement in element.EnumerateArray())
				{
					list.Add(GeoJsonConverter.ReadAdditionalPropertyValue(in jsonElement));
				}
				return list.ToArray();
			}
			case 3:
				return element.GetString();
			case 4:
			{
				int num;
				if (element.TryGetInt32(ref num))
				{
					return num;
				}
				long num2;
				if (element.TryGetInt64(ref num2))
				{
					return num2;
				}
				return element.GetDouble();
			}
			case 5:
				return true;
			case 6:
				return false;
			default:
				throw new NotSupportedException("Not supported value kind " + element.ValueKind.ToString());
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00010F4C File Offset: 0x0000F14C
		private static IReadOnlyList<GeoPosition> ReadCoordinates(JsonElement coordinatesElement)
		{
			GeoPosition[] array = new GeoPosition[coordinatesElement.GetArrayLength()];
			int num = 0;
			foreach (JsonElement jsonElement in coordinatesElement.EnumerateArray())
			{
				array[num] = GeoJsonConverter.ReadCoordinate(jsonElement);
				num++;
			}
			return array;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		private static GeoPosition ReadCoordinate(JsonElement coordinate)
		{
			int arrayLength = coordinate.GetArrayLength();
			if (arrayLength < 2 || arrayLength > 3)
			{
				throw new JsonException("Only 2 or 3 element coordinates supported");
			}
			double @double = coordinate[0].GetDouble();
			double double2 = coordinate[1].GetDouble();
			double? num = null;
			if (arrayLength > 2)
			{
				num = new double?(coordinate[2].GetDouble());
			}
			return new GeoPosition(@double, double2, num);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00011034 File Offset: 0x0000F234
		internal static void Write(Utf8JsonWriter writer, GeoObject value)
		{
			GeoJsonConverter.<>c__DisplayClass20_0 CS$<>8__locals1;
			CS$<>8__locals1.writer = writer;
			CS$<>8__locals1.writer.WriteStartObject();
			GeoPoint geoPoint = value as GeoPoint;
			if (geoPoint == null)
			{
				GeoLineString geoLineString = value as GeoLineString;
				if (geoLineString == null)
				{
					GeoPolygon geoPolygon = value as GeoPolygon;
					if (geoPolygon == null)
					{
						GeoPointCollection geoPointCollection = value as GeoPointCollection;
						if (geoPointCollection == null)
						{
							GeoLineStringCollection geoLineStringCollection = value as GeoLineStringCollection;
							if (geoLineStringCollection == null)
							{
								GeoPolygonCollection geoPolygonCollection = value as GeoPolygonCollection;
								if (geoPolygonCollection == null)
								{
									GeoCollection geoCollection = value as GeoCollection;
									if (geoCollection == null)
									{
										throw new NotSupportedException(string.Format("Geometry type '{0}' not supported", (value != null) ? value.GetType() : null));
									}
									GeoJsonConverter.<Write>g__WriteType|20_1("GeometryCollection", ref CS$<>8__locals1);
									CS$<>8__locals1.writer.WritePropertyName("geometries");
									CS$<>8__locals1.writer.WriteStartArray();
									foreach (GeoObject geoObject in geoCollection.Geometries)
									{
										GeoJsonConverter.Write(CS$<>8__locals1.writer, geoObject);
									}
									CS$<>8__locals1.writer.WriteEndArray();
								}
								else
								{
									GeoJsonConverter.<Write>g__WriteType|20_1("MultiPolygon", ref CS$<>8__locals1);
									CS$<>8__locals1.writer.WritePropertyName("coordinates");
									CS$<>8__locals1.writer.WriteStartArray();
									foreach (GeoPolygon geoPolygon2 in geoPolygonCollection.Polygons)
									{
										CS$<>8__locals1.writer.WriteStartArray();
										foreach (GeoLinearRing geoLinearRing in geoPolygon2.Rings)
										{
											GeoJsonConverter.<Write>g__WritePositions|20_3(geoLinearRing.Coordinates, ref CS$<>8__locals1);
										}
										CS$<>8__locals1.writer.WriteEndArray();
									}
									CS$<>8__locals1.writer.WriteEndArray();
								}
							}
							else
							{
								GeoJsonConverter.<Write>g__WriteType|20_1("MultiLineString", ref CS$<>8__locals1);
								CS$<>8__locals1.writer.WritePropertyName("coordinates");
								CS$<>8__locals1.writer.WriteStartArray();
								foreach (GeoLineString geoLineString2 in geoLineStringCollection.Lines)
								{
									GeoJsonConverter.<Write>g__WritePositions|20_3(geoLineString2.Coordinates, ref CS$<>8__locals1);
								}
								CS$<>8__locals1.writer.WriteEndArray();
							}
						}
						else
						{
							GeoJsonConverter.<Write>g__WriteType|20_1("MultiPoint", ref CS$<>8__locals1);
							CS$<>8__locals1.writer.WritePropertyName("coordinates");
							CS$<>8__locals1.writer.WriteStartArray();
							foreach (GeoPoint geoPoint2 in geoPointCollection.Points)
							{
								GeoJsonConverter.<Write>g__WritePosition|20_2(geoPoint2.Coordinates, ref CS$<>8__locals1);
							}
							CS$<>8__locals1.writer.WriteEndArray();
						}
					}
					else
					{
						GeoJsonConverter.<Write>g__WriteType|20_1("Polygon", ref CS$<>8__locals1);
						CS$<>8__locals1.writer.WritePropertyName("coordinates");
						CS$<>8__locals1.writer.WriteStartArray();
						foreach (GeoLinearRing geoLinearRing2 in geoPolygon.Rings)
						{
							GeoJsonConverter.<Write>g__WritePositions|20_3(geoLinearRing2.Coordinates, ref CS$<>8__locals1);
						}
						CS$<>8__locals1.writer.WriteEndArray();
					}
				}
				else
				{
					GeoJsonConverter.<Write>g__WriteType|20_1("LineString", ref CS$<>8__locals1);
					CS$<>8__locals1.writer.WritePropertyName("coordinates");
					GeoJsonConverter.<Write>g__WritePositions|20_3(geoLineString.Coordinates, ref CS$<>8__locals1);
				}
			}
			else
			{
				GeoJsonConverter.<Write>g__WriteType|20_1("Point", ref CS$<>8__locals1);
				CS$<>8__locals1.writer.WritePropertyName("coordinates");
				GeoJsonConverter.<Write>g__WritePosition|20_2(geoPoint.Coordinates, ref CS$<>8__locals1);
			}
			GeoBoundingBox boundingBox = value.BoundingBox;
			if (boundingBox != null)
			{
				CS$<>8__locals1.writer.WritePropertyName("bbox");
				CS$<>8__locals1.writer.WriteStartArray();
				CS$<>8__locals1.writer.WriteNumberValue(boundingBox.West);
				CS$<>8__locals1.writer.WriteNumberValue(boundingBox.South);
				if (boundingBox.MinAltitude != null)
				{
					CS$<>8__locals1.writer.WriteNumberValue(boundingBox.MinAltitude.Value);
				}
				CS$<>8__locals1.writer.WriteNumberValue(boundingBox.East);
				CS$<>8__locals1.writer.WriteNumberValue(boundingBox.North);
				if (boundingBox.MaxAltitude != null)
				{
					CS$<>8__locals1.writer.WriteNumberValue(boundingBox.MaxAltitude.Value);
				}
				CS$<>8__locals1.writer.WriteEndArray();
			}
			foreach (KeyValuePair<string, object> keyValuePair in value.CustomProperties)
			{
				CS$<>8__locals1.writer.WritePropertyName(keyValuePair.Key);
				GeoJsonConverter.WriteAdditionalPropertyValue(CS$<>8__locals1.writer, keyValuePair.Value);
			}
			CS$<>8__locals1.writer.WriteEndObject();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001152C File Offset: 0x0000F72C
		private static void WriteAdditionalPropertyValue(Utf8JsonWriter writer, [Nullable(2)] object value)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			if (value is int)
			{
				int num = (int)value;
				writer.WriteNumberValue(num);
				return;
			}
			if (value is double)
			{
				double num2 = (double)value;
				writer.WriteNumberValue(num2);
				return;
			}
			if (value is float)
			{
				float num3 = (float)value;
				writer.WriteNumberValue(num3);
				return;
			}
			if (value is long)
			{
				long num4 = (long)value;
				writer.WriteNumberValue(num4);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				writer.WriteStringValue(text);
				return;
			}
			if (value is bool)
			{
				bool flag = (bool)value;
				writer.WriteBooleanValue(flag);
				return;
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = value as IEnumerable<KeyValuePair<string, object>>;
			if (enumerable != null)
			{
				writer.WriteStartObject();
				foreach (KeyValuePair<string, object> keyValuePair in enumerable)
				{
					writer.WritePropertyName(keyValuePair.Key);
					GeoJsonConverter.WriteAdditionalPropertyValue(writer, keyValuePair.Value);
				}
				writer.WriteEndObject();
				return;
			}
			IEnumerable<object> enumerable2 = value as IEnumerable<object>;
			if (enumerable2 == null)
			{
				string text2 = "Not supported type ";
				Type type = value.GetType();
				throw new NotSupportedException(text2 + ((type != null) ? type.ToString() : null));
			}
			writer.WriteStartArray();
			foreach (object obj in enumerable2)
			{
				GeoJsonConverter.WriteAdditionalPropertyValue(writer, obj);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000116CC File Offset: 0x0000F8CC
		private static JsonElement GetRequiredProperty(JsonElement element, string name)
		{
			JsonElement jsonElement;
			if (!element.TryGetProperty(name, ref jsonElement))
			{
				throw new JsonException("GeoJSON object expected to have '" + name + "' property.");
			}
			return jsonElement;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011704 File Offset: 0x0000F904
		[CompilerGenerated]
		internal static void <Write>g__WritePositionValues|20_0(GeoPosition type, ref GeoJsonConverter.<>c__DisplayClass20_0 A_1)
		{
			A_1.writer.WriteNumberValue(type.Longitude);
			A_1.writer.WriteNumberValue(type.Latitude);
			if (type.Altitude != null)
			{
				A_1.writer.WriteNumberValue(type.Altitude.Value);
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00011760 File Offset: 0x0000F960
		[CompilerGenerated]
		internal static void <Write>g__WriteType|20_1(string type, ref GeoJsonConverter.<>c__DisplayClass20_0 A_1)
		{
			A_1.writer.WriteString("type", type);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00011773 File Offset: 0x0000F973
		[CompilerGenerated]
		internal static void <Write>g__WritePosition|20_2(GeoPosition type, ref GeoJsonConverter.<>c__DisplayClass20_0 A_1)
		{
			A_1.writer.WriteStartArray();
			GeoJsonConverter.<Write>g__WritePositionValues|20_0(type, ref A_1);
			A_1.writer.WriteEndArray();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00011794 File Offset: 0x0000F994
		[NullableContext(0)]
		[CompilerGenerated]
		internal static void <Write>g__WritePositions|20_3(GeoArray<GeoPosition> positions, ref GeoJsonConverter.<>c__DisplayClass20_0 A_1)
		{
			A_1.writer.WriteStartArray();
			foreach (GeoPosition geoPosition in positions)
			{
				GeoJsonConverter.<Write>g__WritePosition|20_2(geoPosition, ref A_1);
			}
			A_1.writer.WriteEndArray();
		}

		// Token: 0x04000237 RID: 567
		private const string PointType = "Point";

		// Token: 0x04000238 RID: 568
		private const string LineStringType = "LineString";

		// Token: 0x04000239 RID: 569
		private const string MultiPointType = "MultiPoint";

		// Token: 0x0400023A RID: 570
		private const string PolygonType = "Polygon";

		// Token: 0x0400023B RID: 571
		private const string MultiLineStringType = "MultiLineString";

		// Token: 0x0400023C RID: 572
		private const string MultiPolygonType = "MultiPolygon";

		// Token: 0x0400023D RID: 573
		private const string GeometryCollectionType = "GeometryCollection";

		// Token: 0x0400023E RID: 574
		private const string TypeProperty = "type";

		// Token: 0x0400023F RID: 575
		private const string GeometriesProperty = "geometries";

		// Token: 0x04000240 RID: 576
		private const string CoordinatesProperty = "coordinates";

		// Token: 0x04000241 RID: 577
		private const string BBoxProperty = "bbox";
	}
}
