using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000012 RID: 18
	internal class GeoJsonObjectReader : SpatialReader<IDictionary<string, object>>
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00002EDA File Offset: 0x000010DA
		internal GeoJsonObjectReader(SpatialPipeline destination)
			: base(destination)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002EE4 File Offset: 0x000010E4
		protected override void ReadGeographyImplementation(IDictionary<string, object> input)
		{
			TypeWashedPipeline typeWashedPipeline = new TypeWashedToGeographyLongLatPipeline(base.Destination);
			new GeoJsonObjectReader.SendToTypeWashedPipeline(typeWashedPipeline).SendToPipeline(input, true);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002F0C File Offset: 0x0000110C
		protected override void ReadGeometryImplementation(IDictionary<string, object> input)
		{
			TypeWashedPipeline typeWashedPipeline = new TypeWashedToGeometryPipeline(base.Destination);
			new GeoJsonObjectReader.SendToTypeWashedPipeline(typeWashedPipeline).SendToPipeline(input, true);
		}

		// Token: 0x02000013 RID: 19
		private class SendToTypeWashedPipeline
		{
			// Token: 0x060000C5 RID: 197 RVA: 0x00002F32 File Offset: 0x00001132
			internal SendToTypeWashedPipeline(TypeWashedPipeline pipeline)
			{
				this.pipeline = pipeline;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00002F44 File Offset: 0x00001144
			internal void SendToPipeline(IDictionary<string, object> members, bool requireSetCoordinates)
			{
				SpatialType spatialType = GeoJsonObjectReader.SendToTypeWashedPipeline.GetSpatialType(members);
				int? num;
				if (!GeoJsonObjectReader.SendToTypeWashedPipeline.TryGetCoordinateSystemId(members, out num))
				{
					num = default(int?);
				}
				if (requireSetCoordinates || num != null)
				{
					this.pipeline.SetCoordinateSystem(num);
				}
				string text;
				if (spatialType == SpatialType.Collection)
				{
					text = "geometries";
				}
				else
				{
					text = "coordinates";
				}
				IEnumerable memberValueAsJsonArray = GeoJsonObjectReader.SendToTypeWashedPipeline.GetMemberValueAsJsonArray(members, text);
				this.SendShape(spatialType, memberValueAsJsonArray);
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00002FA4 File Offset: 0x000011A4
			private static void SendArrayOfArray(IEnumerable array, Action<IEnumerable> send)
			{
				foreach (object obj in array)
				{
					IEnumerable enumerable = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonArray(obj);
					send.Invoke(enumerable);
				}
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00002FFC File Offset: 0x000011FC
			private static double? ValueAsNullableDouble(object value)
			{
				if (value != null)
				{
					return new double?(GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsDouble(value));
				}
				return default(double?);
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00003024 File Offset: 0x00001224
			private static double ValueAsDouble(object value)
			{
				if (value == null)
				{
					throw new FormatException(Strings.GeoJsonReader_InvalidNullElement);
				}
				if (value is string || value is IDictionary<string, object> || value is IEnumerable || value is bool)
				{
					throw new FormatException(Strings.GeoJsonReader_ExpectedNumeric);
				}
				return Convert.ToDouble(value, CultureInfo.InvariantCulture);
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00003078 File Offset: 0x00001278
			private static IEnumerable ValueAsJsonArray(object value)
			{
				if (value == null)
				{
					return null;
				}
				if (value is string)
				{
					throw new FormatException(Strings.GeoJsonReader_ExpectedArray);
				}
				if (value is IDictionary || value is IDictionary<string, object>)
				{
					throw new FormatException(Strings.GeoJsonReader_ExpectedArray);
				}
				IEnumerable enumerable = value as IEnumerable;
				if (enumerable != null)
				{
					return enumerable;
				}
				throw new FormatException(Strings.GeoJsonReader_ExpectedArray);
			}

			// Token: 0x060000CB RID: 203 RVA: 0x000030D0 File Offset: 0x000012D0
			private static IDictionary<string, object> ValueAsJsonObject(object value)
			{
				if (value == null)
				{
					return null;
				}
				IDictionary<string, object> dictionary = value as IDictionary<string, object>;
				if (dictionary != null)
				{
					return dictionary;
				}
				throw new FormatException(Strings.JsonReaderExtensions_CannotReadValueAsJsonObject(value));
			}

			// Token: 0x060000CC RID: 204 RVA: 0x000030FC File Offset: 0x000012FC
			private static string ValueAsString(string propertyName, object value)
			{
				if (value == null)
				{
					return null;
				}
				string text = value as string;
				if (text != null)
				{
					return text;
				}
				throw new FormatException(Strings.JsonReaderExtensions_CannotReadPropertyValueAsString(value, propertyName));
			}

			// Token: 0x060000CD RID: 205 RVA: 0x00003128 File Offset: 0x00001328
			private static SpatialType GetSpatialType(IDictionary<string, object> geoJsonObject)
			{
				object obj;
				if (geoJsonObject.TryGetValue("type", ref obj))
				{
					return GeoJsonObjectReader.SendToTypeWashedPipeline.ReadTypeName(GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("type", obj));
				}
				throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("type"));
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00003164 File Offset: 0x00001364
			private static bool TryGetCoordinateSystemId(IDictionary<string, object> geoJsonObject, out int? epsgId)
			{
				object obj;
				if (!geoJsonObject.TryGetValue("crs", ref obj))
				{
					epsgId = default(int?);
					return false;
				}
				IDictionary<string, object> dictionary = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonObject(obj);
				epsgId = new int?(GeoJsonObjectReader.SendToTypeWashedPipeline.GetCoordinateSystemIdFromCrs(dictionary));
				return true;
			}

			// Token: 0x060000CF RID: 207 RVA: 0x000031A4 File Offset: 0x000013A4
			private static int GetCoordinateSystemIdFromCrs(IDictionary<string, object> crsJsonObject)
			{
				object obj;
				if (!crsJsonObject.TryGetValue("type", ref obj))
				{
					throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("type"));
				}
				string text = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("type", obj);
				if (!string.Equals(text, "name", 4))
				{
					throw new FormatException(Strings.GeoJsonReader_InvalidCrsType(text));
				}
				object obj2;
				if (!crsJsonObject.TryGetValue("properties", ref obj2))
				{
					throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("properties"));
				}
				IDictionary<string, object> dictionary = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonObject(obj2);
				object obj3;
				if (!dictionary.TryGetValue("name", ref obj3))
				{
					throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("name"));
				}
				string text2 = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("name", obj3);
				int length = "EPSG".Length;
				int num;
				if (text2 == null || !text2.StartsWith("EPSG", 4) || text2.Length == length || text2.get_Chars(length) != ':' || !int.TryParse(text2.Substring(length + 1), ref num))
				{
					throw new FormatException(Strings.GeoJsonReader_InvalidCrsName(text2));
				}
				return num;
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x000032A4 File Offset: 0x000014A4
			private static IEnumerable GetMemberValueAsJsonArray(IDictionary<string, object> geoJsonObject, string memberName)
			{
				object obj;
				if (geoJsonObject.TryGetValue(memberName, ref obj))
				{
					return GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonArray(obj);
				}
				throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember(memberName));
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x000032D0 File Offset: 0x000014D0
			private static bool EnumerableAny(IEnumerable enumerable)
			{
				IEnumerator enumerator = enumerable.GetEnumerator();
				return enumerator.MoveNext();
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x000032EC File Offset: 0x000014EC
			private static SpatialType ReadTypeName(string typeName)
			{
				if (typeName != null)
				{
					if (<PrivateImplementationDetails>{224B6CBA-CF75-43C3-B91B-1B39CE6F7CAF}.$$method0x60000d1-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
						dictionary.Add("Point", 0);
						dictionary.Add("LineString", 1);
						dictionary.Add("Polygon", 2);
						dictionary.Add("MultiPoint", 3);
						dictionary.Add("MultiLineString", 4);
						dictionary.Add("MultiPolygon", 5);
						dictionary.Add("GeometryCollection", 6);
						<PrivateImplementationDetails>{224B6CBA-CF75-43C3-B91B-1B39CE6F7CAF}.$$method0x60000d1-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{224B6CBA-CF75-43C3-B91B-1B39CE6F7CAF}.$$method0x60000d1-1.TryGetValue(typeName, ref num))
					{
						switch (num)
						{
						case 0:
							return SpatialType.Point;
						case 1:
							return SpatialType.LineString;
						case 2:
							return SpatialType.Polygon;
						case 3:
							return SpatialType.MultiPoint;
						case 4:
							return SpatialType.MultiLineString;
						case 5:
							return SpatialType.MultiPolygon;
						case 6:
							return SpatialType.Collection;
						}
					}
				}
				throw new FormatException(Strings.GeoJsonReader_InvalidTypeName(typeName));
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x000033B9 File Offset: 0x000015B9
			private void SendShape(SpatialType spatialType, IEnumerable contentMembers)
			{
				this.pipeline.BeginGeo(spatialType);
				this.SendCoordinates(spatialType, contentMembers);
				this.pipeline.EndGeo();
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x000033DC File Offset: 0x000015DC
			private void SendCoordinates(SpatialType spatialType, IEnumerable contentMembers)
			{
				if (GeoJsonObjectReader.SendToTypeWashedPipeline.EnumerableAny(contentMembers))
				{
					switch (spatialType)
					{
					case SpatialType.Point:
						this.SendPoint(contentMembers);
						return;
					case SpatialType.LineString:
						this.SendLineString(contentMembers);
						return;
					case SpatialType.Polygon:
						this.SendPolygon(contentMembers);
						return;
					case SpatialType.MultiPoint:
						this.SendMultiShape(SpatialType.Point, contentMembers);
						return;
					case SpatialType.MultiLineString:
						this.SendMultiShape(SpatialType.LineString, contentMembers);
						return;
					case SpatialType.MultiPolygon:
						this.SendMultiShape(SpatialType.Polygon, contentMembers);
						return;
					case SpatialType.Collection:
						foreach (object obj in contentMembers)
						{
							IDictionary<string, object> dictionary = (IDictionary<string, object>)obj;
							this.SendToPipeline(dictionary, false);
						}
						break;
					default:
						return;
					}
				}
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00003498 File Offset: 0x00001698
			private void SendPoint(IEnumerable coordinates)
			{
				this.SendPosition(coordinates, true);
				this.pipeline.EndFigure();
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x000034AD File Offset: 0x000016AD
			private void SendLineString(IEnumerable coordinates)
			{
				this.SendPositionArray(coordinates);
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x000034BF File Offset: 0x000016BF
			private void SendPolygon(IEnumerable coordinates)
			{
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(coordinates, delegate(IEnumerable positionArray)
				{
					this.SendPositionArray(positionArray);
				});
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x000034F0 File Offset: 0x000016F0
			private void SendMultiShape(SpatialType containedSpatialType, IEnumerable coordinates)
			{
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(coordinates, delegate(IEnumerable containedShapeCoordinates)
				{
					this.SendShape(containedSpatialType, containedShapeCoordinates);
				});
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x00003550 File Offset: 0x00001750
			private void SendPositionArray(IEnumerable positionArray)
			{
				bool first = true;
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(positionArray, delegate(IEnumerable array)
				{
					this.SendPosition(array, first);
					if (first)
					{
						first = false;
					}
				});
				this.pipeline.EndFigure();
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00003590 File Offset: 0x00001790
			private void SendPosition(IEnumerable positionElements, bool first)
			{
				int num = 0;
				double num2 = 0.0;
				double num3 = 0.0;
				double? num4 = default(double?);
				double? num5 = default(double?);
				foreach (object obj in positionElements)
				{
					num++;
					switch (num)
					{
					case 1:
						num2 = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsDouble(obj);
						break;
					case 2:
						num3 = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsDouble(obj);
						break;
					case 3:
						num4 = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsNullableDouble(obj);
						break;
					case 4:
						num5 = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsNullableDouble(obj);
						break;
					}
				}
				if (num < 2 || num > 4)
				{
					throw new FormatException(Strings.GeoJsonReader_InvalidPosition);
				}
				if (first)
				{
					this.pipeline.BeginFigure(num2, num3, num4, num5);
					return;
				}
				this.pipeline.LineTo(num2, num3, num4, num5);
			}

			// Token: 0x04000016 RID: 22
			private TypeWashedPipeline pipeline;
		}
	}
}
