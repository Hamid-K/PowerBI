using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x0200000B RID: 11
	internal class GeoJsonObjectReader : SpatialReader<IDictionary<string, object>>
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002C07 File Offset: 0x00000E07
		internal GeoJsonObjectReader(SpatialPipeline destination)
			: base(destination)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002C10 File Offset: 0x00000E10
		protected override void ReadGeographyImplementation(IDictionary<string, object> input)
		{
			TypeWashedPipeline typeWashedPipeline = new TypeWashedToGeographyLongLatPipeline(base.Destination);
			new GeoJsonObjectReader.SendToTypeWashedPipeline(typeWashedPipeline).SendToPipeline(input, true);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002C38 File Offset: 0x00000E38
		protected override void ReadGeometryImplementation(IDictionary<string, object> input)
		{
			TypeWashedPipeline typeWashedPipeline = new TypeWashedToGeometryPipeline(base.Destination);
			new GeoJsonObjectReader.SendToTypeWashedPipeline(typeWashedPipeline).SendToPipeline(input, true);
		}

		// Token: 0x02000073 RID: 115
		private class SendToTypeWashedPipeline
		{
			// Token: 0x060002CB RID: 715 RVA: 0x00006D98 File Offset: 0x00004F98
			internal SendToTypeWashedPipeline(TypeWashedPipeline pipeline)
			{
				this.pipeline = pipeline;
			}

			// Token: 0x060002CC RID: 716 RVA: 0x00006DA8 File Offset: 0x00004FA8
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

			// Token: 0x060002CD RID: 717 RVA: 0x00006E08 File Offset: 0x00005008
			private static void SendArrayOfArray(IEnumerable array, Action<IEnumerable> send)
			{
				foreach (object obj in array)
				{
					IEnumerable enumerable = GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonArray(obj);
					send.Invoke(enumerable);
				}
			}

			// Token: 0x060002CE RID: 718 RVA: 0x00006E60 File Offset: 0x00005060
			private static double? ValueAsNullableDouble(object value)
			{
				if (value != null)
				{
					return new double?(GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsDouble(value));
				}
				return default(double?);
			}

			// Token: 0x060002CF RID: 719 RVA: 0x00006E88 File Offset: 0x00005088
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

			// Token: 0x060002D0 RID: 720 RVA: 0x00006EDC File Offset: 0x000050DC
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

			// Token: 0x060002D1 RID: 721 RVA: 0x00006F34 File Offset: 0x00005134
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

			// Token: 0x060002D2 RID: 722 RVA: 0x00006F60 File Offset: 0x00005160
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

			// Token: 0x060002D3 RID: 723 RVA: 0x00006F8C File Offset: 0x0000518C
			private static SpatialType GetSpatialType(IDictionary<string, object> geoJsonObject)
			{
				object obj;
				if (geoJsonObject.TryGetValue("type", ref obj))
				{
					return GeoJsonObjectReader.SendToTypeWashedPipeline.ReadTypeName(GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsString("type", obj));
				}
				throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember("type"));
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x00006FC8 File Offset: 0x000051C8
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

			// Token: 0x060002D5 RID: 725 RVA: 0x00007008 File Offset: 0x00005208
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

			// Token: 0x060002D6 RID: 726 RVA: 0x00007108 File Offset: 0x00005308
			private static IEnumerable GetMemberValueAsJsonArray(IDictionary<string, object> geoJsonObject, string memberName)
			{
				object obj;
				if (geoJsonObject.TryGetValue(memberName, ref obj))
				{
					return GeoJsonObjectReader.SendToTypeWashedPipeline.ValueAsJsonArray(obj);
				}
				throw new FormatException(Strings.GeoJsonReader_MissingRequiredMember(memberName));
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x00007134 File Offset: 0x00005334
			private static bool EnumerableAny(IEnumerable enumerable)
			{
				IEnumerator enumerator = enumerable.GetEnumerator();
				return enumerator.MoveNext();
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x00007150 File Offset: 0x00005350
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

			// Token: 0x060002D9 RID: 729 RVA: 0x0000723D File Offset: 0x0000543D
			private void SendShape(SpatialType spatialType, IEnumerable contentMembers)
			{
				this.pipeline.BeginGeo(spatialType);
				this.SendCoordinates(spatialType, contentMembers);
				this.pipeline.EndGeo();
			}

			// Token: 0x060002DA RID: 730 RVA: 0x00007260 File Offset: 0x00005460
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

			// Token: 0x060002DB RID: 731 RVA: 0x00007318 File Offset: 0x00005518
			private void SendPoint(IEnumerable coordinates)
			{
				this.SendPosition(coordinates, true);
				this.pipeline.EndFigure();
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000732D File Offset: 0x0000552D
			private void SendLineString(IEnumerable coordinates)
			{
				this.SendPositionArray(coordinates);
			}

			// Token: 0x060002DD RID: 733 RVA: 0x00007336 File Offset: 0x00005536
			private void SendPolygon(IEnumerable coordinates)
			{
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(coordinates, delegate(IEnumerable positionArray)
				{
					this.SendPositionArray(positionArray);
				});
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000734C File Offset: 0x0000554C
			private void SendMultiShape(SpatialType containedSpatialType, IEnumerable coordinates)
			{
				GeoJsonObjectReader.SendToTypeWashedPipeline.SendArrayOfArray(coordinates, delegate(IEnumerable containedShapeCoordinates)
				{
					this.SendShape(containedSpatialType, containedShapeCoordinates);
				});
			}

			// Token: 0x060002DF RID: 735 RVA: 0x00007380 File Offset: 0x00005580
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

			// Token: 0x060002E0 RID: 736 RVA: 0x000073C0 File Offset: 0x000055C0
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

			// Token: 0x040000FF RID: 255
			private TypeWashedPipeline pipeline;
		}
	}
}
