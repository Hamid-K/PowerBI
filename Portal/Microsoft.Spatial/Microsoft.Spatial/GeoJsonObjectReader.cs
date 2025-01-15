using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x0200000B RID: 11
	internal class GeoJsonObjectReader : SpatialReader<IDictionary<string, object>>
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00002FEF File Offset: 0x000011EF
		internal GeoJsonObjectReader(SpatialPipeline destination)
			: base(destination)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002FF8 File Offset: 0x000011F8
		protected override void ReadGeographyImplementation(IDictionary<string, object> input)
		{
			TypeWashedPipeline typeWashedPipeline = new TypeWashedToGeographyLongLatPipeline(base.Destination);
			new GeoJsonObjectReader.SendToTypeWashedPipeline(typeWashedPipeline).SendToPipeline(input, true);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003020 File Offset: 0x00001220
		protected override void ReadGeometryImplementation(IDictionary<string, object> input)
		{
			TypeWashedPipeline typeWashedPipeline = new TypeWashedToGeometryPipeline(base.Destination);
			new GeoJsonObjectReader.SendToTypeWashedPipeline(typeWashedPipeline).SendToPipeline(input, true);
		}

		// Token: 0x0200007F RID: 127
		private class SendToTypeWashedPipeline
		{
			// Token: 0x06000353 RID: 851 RVA: 0x00007AFC File Offset: 0x00005CFC
			internal SendToTypeWashedPipeline(TypeWashedPipeline pipeline)
			{
				this.pipeline = pipeline;
			}

			// Token: 0x06000354 RID: 852 RVA: 0x00007B0C File Offset: 0x00005D0C
			internal void SendToPipeline(IDictionary<string, object> members, bool requireSetCoordinates)
			{
				SpatialType spatialType = GeoJsonObjectReader.SendToTypeWashedPipeline.GetSpatialType(members);
				int? num;
				if (!GeoJsonObjectReader.SendToTypeWashedPipeline.TryGetCoordinateSystemId(members, out num))
				{
					num = null;
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

			// Token: 0x06000355 RID: 853 RVA: 0x00007B6C File Offset: 0x00005D6C
			private static void SendArrayOfArray(IEnumerable array, Action<IEnumerable> send)
			{
				foreach (object obj in array)
				{
					IEnumerable enumerable = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonArray(obj);
					send(enumerable);
				}
			}

			// Token: 0x06000356 RID: 854 RVA: 0x00007BC4 File Offset: 0x00005DC4
			private static double? ValueAsNullableDouble(object value)
			{
				if (value != null)
				{
					return new double?(GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsDouble(value));
				}
				return null;
			}

			// Token: 0x06000357 RID: 855 RVA: 0x00007BEC File Offset: 0x00005DEC
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

			// Token: 0x06000358 RID: 856 RVA: 0x00007C40 File Offset: 0x00005E40
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

			// Token: 0x06000359 RID: 857 RVA: 0x00007C98 File Offset: 0x00005E98
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

			// Token: 0x0600035A RID: 858 RVA: 0x00007CC4 File Offset: 0x00005EC4
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

			// Token: 0x0600035B RID: 859 RVA: 0x00007CF0 File Offset: 0x00005EF0
			private static SpatialType GetSpatialType(IDictionary<string, object> geoJsonObject)
			{
				object obj;
				if (geoJsonObject.TryGetValue("type", out obj))
				{
					return GeoJsonObjectReader.SendToTypeWashedPipeline.ReadTypeName(GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("type", obj));
				}
				throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("type"));
			}

			// Token: 0x0600035C RID: 860 RVA: 0x00007D2C File Offset: 0x00005F2C
			private static bool TryGetCoordinateSystemId(IDictionary<string, object> geoJsonObject, out int? epsgId)
			{
				object obj;
				if (!geoJsonObject.TryGetValue("crs", out obj))
				{
					epsgId = null;
					return false;
				}
				IDictionary<string, object> dictionary = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonObject(obj);
				epsgId = new int?(GeoJsonObjectReader.SendToTypeWashedPipeline.GetCoordinateSystemIdFromCrs(dictionary));
				return true;
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00007D6C File Offset: 0x00005F6C
			private static int GetCoordinateSystemIdFromCrs(IDictionary<string, object> crsJsonObject)
			{
				object obj;
				if (!crsJsonObject.TryGetValue("type", out obj))
				{
					throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("type"));
				}
				string text = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("type", obj);
				if (!string.Equals(text, "name", StringComparison.Ordinal))
				{
					throw new FormatException(Strings.GeoJsonReader_InvalidCrsType(text));
				}
				object obj2;
				if (!crsJsonObject.TryGetValue("properties", out obj2))
				{
					throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("properties"));
				}
				IDictionary<string, object> dictionary = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonObject(obj2);
				object obj3;
				if (!dictionary.TryGetValue("name", out obj3))
				{
					throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("name"));
				}
				string text2 = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("name", obj3);
				int length = "EPSG".Length;
				int num;
				if (text2 == null || !text2.StartsWith("EPSG", StringComparison.Ordinal) || text2.Length == length || text2[length] != ':' || !int.TryParse(text2.Substring(length + 1), out num))
				{
					throw new FormatException(Strings.GeoJsonReader_InvalidCrsName(text2));
				}
				return num;
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00007E6C File Offset: 0x0000606C
			private static IEnumerable GetMemberValueAsJsonArray(IDictionary<string, object> geoJsonObject, string memberName)
			{
				object obj;
				if (geoJsonObject.TryGetValue(memberName, out obj))
				{
					return GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonArray(obj);
				}
				throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember(memberName));
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00007E98 File Offset: 0x00006098
			private static bool EnumerableAny(IEnumerable enumerable)
			{
				IEnumerator enumerator = enumerable.GetEnumerator();
				return enumerator.MoveNext();
			}

			// Token: 0x06000360 RID: 864 RVA: 0x00007EB4 File Offset: 0x000060B4
			private static SpatialType ReadTypeName(string typeName)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(typeName);
				if (num <= 2386032169U)
				{
					if (num != 1547714260U)
					{
						if (num != 2050635977U)
						{
							if (num == 2386032169U)
							{
								if (typeName == "Polygon")
								{
									return SpatialType.Polygon;
								}
							}
						}
						else if (typeName == "MultiLineString")
						{
							return SpatialType.MultiLineString;
						}
					}
					else if (typeName == "MultiPolygon")
					{
						return SpatialType.MultiPolygon;
					}
				}
				else if (num <= 3786658501U)
				{
					if (num != 3694217412U)
					{
						if (num == 3786658501U)
						{
							if (typeName == "GeometryCollection")
							{
								return SpatialType.Collection;
							}
						}
					}
					else if (typeName == "MultiPoint")
					{
						return SpatialType.MultiPoint;
					}
				}
				else if (num != 3936939825U)
				{
					if (num == 4094167858U)
					{
						if (typeName == "LineString")
						{
							return SpatialType.LineString;
						}
					}
				}
				else if (typeName == "Point")
				{
					return SpatialType.Point;
				}
				throw new FormatException(Strings.GeoJsonReader_InvalidTypeName(typeName));
			}

			// Token: 0x06000361 RID: 865 RVA: 0x00007FA1 File Offset: 0x000061A1
			private void SendShape(SpatialType spatialType, IEnumerable contentMembers)
			{
				this.pipeline.BeginGeo(spatialType);
				this.SendCoordinates(spatialType, contentMembers);
				this.pipeline.EndGeo();
			}

			// Token: 0x06000362 RID: 866 RVA: 0x00007FC4 File Offset: 0x000061C4
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

			// Token: 0x06000363 RID: 867 RVA: 0x0000807C File Offset: 0x0000627C
			private void SendPoint(IEnumerable coordinates)
			{
				this.SendPosition(coordinates, true);
				this.pipeline.EndFigure();
			}

			// Token: 0x06000364 RID: 868 RVA: 0x00008091 File Offset: 0x00006291
			private void SendLineString(IEnumerable coordinates)
			{
				this.SendPositionArray(coordinates);
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0000809A File Offset: 0x0000629A
			private void SendPolygon(IEnumerable coordinates)
			{
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(coordinates, delegate(IEnumerable positionArray)
				{
					this.SendPositionArray(positionArray);
				});
			}

			// Token: 0x06000366 RID: 870 RVA: 0x000080B0 File Offset: 0x000062B0
			private void SendMultiShape(SpatialType containedSpatialType, IEnumerable coordinates)
			{
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(coordinates, delegate(IEnumerable containedShapeCoordinates)
				{
					this.SendShape(containedSpatialType, containedShapeCoordinates);
				});
			}

			// Token: 0x06000367 RID: 871 RVA: 0x000080E4 File Offset: 0x000062E4
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

			// Token: 0x06000368 RID: 872 RVA: 0x00008124 File Offset: 0x00006324
			private void SendPosition(IEnumerable positionElements, bool first)
			{
				int num = 0;
				double num2 = 0.0;
				double num3 = 0.0;
				double? num4 = null;
				double? num5 = null;
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

			// Token: 0x0400011B RID: 283
			private TypeWashedPipeline pipeline;
		}
	}
}
