using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000200 RID: 512
	internal class ObjectPath : List<KeyValuePair<ObjectType, string>>, IEquatable<ObjectPath>
	{
		// Token: 0x06001D4A RID: 7498 RVA: 0x000C7CFC File Offset: 0x000C5EFC
		public ObjectPath(ObjectType objectType, string name)
		{
			base.Add(new KeyValuePair<ObjectType, string>(objectType, name));
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000C7D14 File Offset: 0x000C5F14
		public ObjectPath(params KeyValuePair<ObjectType, string>[] path)
		{
			if (path != null)
			{
				for (int i = 0; i < path.Length; i++)
				{
					base.Add(path[i]);
				}
			}
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000C7D45 File Offset: 0x000C5F45
		internal ObjectPath(IEnumerable<KeyValuePair<ObjectType, string>> path, bool isPathReversed = false)
			: base(path)
		{
			if (isPathReversed)
			{
				base.Reverse();
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x000C7D57 File Offset: 0x000C5F57
		public bool IsEmpty
		{
			get
			{
				return base.Count == 0;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x000C7D62 File Offset: 0x000C5F62
		internal static IEqualityComparer<ObjectPath> DefaultComparer
		{
			get
			{
				return ObjectPath.comparer;
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000C7D6C File Offset: 0x000C5F6C
		public static ObjectPath Parse(JsonTextReader reader)
		{
			ObjectPath objectPath;
			try
			{
				reader.VerifyToken(1);
				reader.Read();
				objectPath = new ObjectPath(ObjectPath.ParseImpl(reader), false);
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectPathMalformedInput, ex);
			}
			catch (JsonException)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectPath, reader, null);
			}
			return objectPath;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000C7DD0 File Offset: 0x000C5FD0
		public static ObjectPath Parse(string json)
		{
			ObjectPath objectPath;
			using (JsonTextReader jsonTextReader = new CommentIgnoringJsonTextReader(new StringReader(json)))
			{
				jsonTextReader.Read();
				objectPath = ObjectPath.Parse(jsonTextReader);
			}
			return objectPath;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000C7E14 File Offset: 0x000C6014
		public static ObjectPath Parse(JObject obj)
		{
			return new ObjectPath(ObjectPath.ParseImpl(obj), false);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000C7E22 File Offset: 0x000C6022
		public static ObjectPath CreateDbQualifiedPath(ObjectPath path, string dbName)
		{
			ObjectPath objectPath = new ObjectPath(path, false);
			objectPath.Insert(0, new KeyValuePair<ObjectType, string>(ObjectType.Database, dbName));
			return objectPath;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000C7E40 File Offset: 0x000C6040
		public void Write(JsonWriter writer)
		{
			writer.WriteStartObject();
			foreach (KeyValuePair<ObjectType, string> keyValuePair in this)
			{
				writer.WritePropertyName(JsonPropertyName.ObjectPath.GetObjectPathPropertyName(keyValuePair.Key));
				writer.WriteValue(keyValuePair.Value);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000C7EB4 File Offset: 0x000C60B4
		public ObjectPath Clone()
		{
			return new ObjectPath(this, false);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000C7EC0 File Offset: 0x000C60C0
		public bool Equals(ObjectPath other)
		{
			if (base.Count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Key != other[i].Key || string.Compare(base[i].Value, other[i].Value, StringComparison.Ordinal) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000C7F38 File Offset: 0x000C6138
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("{ ");
			for (int i = 0; i < base.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append(string.Format("\"{0}\" : \"{1}\"", Utils.GetUserFriendlyNameOfObjectType(base[i].Key), base[i].Value));
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000C7FB7 File Offset: 0x000C61B7
		public void Push(ObjectType objectType, string objectName)
		{
			base.Add(new KeyValuePair<ObjectType, string>(objectType, objectName));
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000C7FC6 File Offset: 0x000C61C6
		public void Pop()
		{
			Utils.Verify(base.Count > 0);
			base.RemoveAt(base.Count - 1);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000C7FE4 File Offset: 0x000C61E4
		internal void Normalize()
		{
			base.Sort(new Comparison<KeyValuePair<ObjectType, string>>(ObjectPath.CompareObjectPathElements));
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000C7FF8 File Offset: 0x000C61F8
		private static int CompareObjectPathElements(KeyValuePair<ObjectType, string> a, KeyValuePair<ObjectType, string> b)
		{
			return ObjectTreeHelper.GetObjectTypeTopologicalOrder(a.Key) - ObjectTreeHelper.GetObjectTypeTopologicalOrder(b.Key);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000C8013 File Offset: 0x000C6213
		private static IEnumerable<KeyValuePair<ObjectType, string>> ParseImpl(JsonTextReader reader)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				reader.Read();
				reader.VerifyToken(9);
				string text2 = (string)reader.Value;
				reader.Read();
				ObjectType objectType = ObjectPath.JsonPathElementToObjectType(text);
				if (objectType == ObjectType.Null)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
				}
				yield return new KeyValuePair<ObjectType, string>(objectType, text2);
			}
			yield break;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000C8023 File Offset: 0x000C6223
		private static IEnumerable<KeyValuePair<ObjectType, string>> ParseImpl(JObject obj)
		{
			foreach (JProperty jproperty in obj.Properties())
			{
				ObjectType objectType = ObjectPath.JsonPathElementToObjectType(jproperty.Name);
				if (objectType == ObjectType.Null)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(jproperty.Name), obj, null);
				}
				yield return new KeyValuePair<ObjectType, string>(objectType, JsonPropertyHelper.ConvertJsonValueToString(jproperty.Value));
			}
			IEnumerator<JProperty> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000C8034 File Offset: 0x000C6234
		private static ObjectType JsonPathElementToObjectType(string element)
		{
			if (element != null)
			{
				switch (element.Length)
				{
				case 3:
				{
					char c = element[0];
					if (c != 'k')
					{
						if (c == 's')
						{
							if (element == "set")
							{
								return ObjectType.Set;
							}
						}
					}
					else if (element == "kpi")
					{
						return ObjectType.KPI;
					}
					break;
				}
				case 4:
					if (element == "role")
					{
						return ObjectType.Role;
					}
					break;
				case 5:
				{
					char c = element[0];
					if (c != 'l')
					{
						if (c != 'm')
						{
							if (c == 't')
							{
								if (element == "table")
								{
									return ObjectType.Table;
								}
							}
						}
						else if (element == "model")
						{
							return ObjectType.Model;
						}
					}
					else if (element == "level")
					{
						return ObjectType.Level;
					}
					break;
				}
				case 6:
					if (element == "column")
					{
						return ObjectType.Column;
					}
					break;
				case 7:
				{
					char c = element[0];
					if (c != 'c')
					{
						if (c == 'm')
						{
							if (element == "measure")
							{
								return ObjectType.Measure;
							}
						}
					}
					else if (element == "culture")
					{
						return ObjectType.Culture;
					}
					break;
				}
				case 8:
					switch (element[0])
					{
					case 'c':
						if (element == "calendar")
						{
							return ObjectType.Calendar;
						}
						break;
					case 'd':
						if (element == "database")
						{
							return ObjectType.Database;
						}
						break;
					case 'f':
						if (element == "function")
						{
							return ObjectType.Function;
						}
						break;
					}
					break;
				case 9:
				{
					char c = element[0];
					if (c != 'h')
					{
						if (c != 'p')
						{
							if (c == 'v')
							{
								if (element == "variation")
								{
									return ObjectType.Variation;
								}
							}
						}
						else if (element == "partition")
						{
							return ObjectType.Partition;
						}
					}
					else if (element == "hierarchy")
					{
						return ObjectType.Hierarchy;
					}
					break;
				}
				case 10:
				{
					char c = element[0];
					switch (c)
					{
					case 'a':
						if (element == "annotation")
						{
							return ObjectType.Annotation;
						}
						break;
					case 'b':
					case 'c':
						break;
					case 'd':
						if (element == "dataSource")
						{
							return ObjectType.DataSource;
						}
						break;
					case 'e':
						if (element == "expression")
						{
							return ObjectType.Expression;
						}
						break;
					default:
						if (c == 'q')
						{
							if (element == "queryGroup")
							{
								return ObjectType.QueryGroup;
							}
						}
						break;
					}
					break;
				}
				case 11:
				{
					char c = element[0];
					if (c != 'a')
					{
						if (c != 'b')
						{
							if (c == 'p')
							{
								if (element == "perspective")
								{
									return ObjectType.Perspective;
								}
							}
						}
						else if (element == "bindingInfo")
						{
							return ObjectType.BindingInfo;
						}
					}
					else if (element == "alternateOf")
					{
						return ObjectType.AlternateOf;
					}
					break;
				}
				case 12:
					if (element == "relationship")
					{
						return ObjectType.Relationship;
					}
					break;
				case 13:
				{
					char c = element[0];
					if (c != 'g')
					{
						if (c == 'r')
						{
							if (element == "refreshPolicy")
							{
								return ObjectType.RefreshPolicy;
							}
						}
					}
					else if (element == "groupByColumn")
					{
						return ObjectType.GroupByColumn;
					}
					break;
				}
				case 14:
				{
					char c = element[0];
					if (c != 'p')
					{
						if (c == 'r')
						{
							if (element == "roleMembership")
							{
								return ObjectType.RoleMembership;
							}
						}
					}
					else if (element == "perspectiveSet")
					{
						return ObjectType.PerspectiveSet;
					}
					break;
				}
				case 15:
				{
					char c = element[2];
					if (c != 'a')
					{
						if (c != 'b')
						{
							if (c == 'l')
							{
								if (element == "calculationItem")
								{
									return ObjectType.CalculationItem;
								}
							}
						}
						else if (element == "tablePermission")
						{
							return ObjectType.TablePermission;
						}
					}
					else if (element == "changedProperty")
					{
						return ObjectType.ChangedProperty;
					}
					break;
				}
				case 16:
				{
					char c = element[3];
					if (c <= 'e')
					{
						if (c != 'c')
						{
							if (c == 'e')
							{
								if (element == "extendedProperty")
								{
									return ObjectType.ExtendedProperty;
								}
							}
						}
						else if (element == "calculationGroup")
						{
							return ObjectType.CalculationGroup;
						}
					}
					else if (c != 'l')
					{
						if (c != 's')
						{
							if (c == 'u')
							{
								if (element == "columnPermission")
								{
									return ObjectType.ColumnPermission;
								}
							}
						}
						else if (element == "perspectiveTable")
						{
							return ObjectType.PerspectiveTable;
						}
					}
					else if (element == "excludedArtifact")
					{
						return ObjectType.ExcludedArtifact;
					}
					break;
				}
				case 17:
				{
					char c = element[0];
					if (c != 'o')
					{
						if (c == 'p')
						{
							if (element == "perspectiveColumn")
							{
								return ObjectType.PerspectiveColumn;
							}
						}
					}
					else if (element == "objectTranslation")
					{
						return ObjectType.ObjectTranslation;
					}
					break;
				}
				case 18:
				{
					char c = element[0];
					if (c != 'a')
					{
						if (c != 'l')
						{
							if (c == 'p')
							{
								if (element == "perspectiveMeasure")
								{
									return ObjectType.PerspectiveMeasure;
								}
							}
						}
						else if (element == "linguisticMetadata")
						{
							return ObjectType.LinguisticMetadata;
						}
					}
					else if (element == "attributeHierarchy")
					{
						return ObjectType.AttributeHierarchy;
					}
					break;
				}
				case 19:
					if (element == "analyticsAIMetadata")
					{
						return ObjectType.AnalyticsAIMetadata;
					}
					break;
				case 20:
				{
					char c = element[0];
					if (c != 'd')
					{
						if (c != 'p')
						{
							if (c == 'r')
							{
								if (element == "relatedColumnDetails")
								{
									return ObjectType.RelatedColumnDetails;
								}
							}
						}
						else if (element == "perspectiveHierarchy")
						{
							return ObjectType.PerspectiveHierarchy;
						}
					}
					else if (element == "detailRowsDefinition")
					{
						return ObjectType.DetailRowsDefinition;
					}
					break;
				}
				case 21:
					if (element == "calculationExpression")
					{
						return ObjectType.CalculationExpression;
					}
					break;
				case 22:
				{
					char c = element[0];
					if (c != 'd')
					{
						if (c == 'f')
						{
							if (element == "formatStringDefinition")
							{
								return ObjectType.FormatStringDefinition;
							}
						}
					}
					else if (element == "dataCoverageDefinition")
					{
						return ObjectType.DataCoverageDefinition;
					}
					break;
				}
				case 23:
					if (element == "calendarColumnReference")
					{
						return ObjectType.CalendarColumnReference;
					}
					break;
				case 25:
					if (element == "timeUnitColumnAssociation")
					{
						return ObjectType.TimeUnitColumnAssociation;
					}
					break;
				}
			}
			return ObjectType.Null;
		}

		// Token: 0x040006A7 RID: 1703
		private static readonly IEqualityComparer<ObjectPath> comparer = new ObjectPath.Comparer();

		// Token: 0x02000435 RID: 1077
		private sealed class Comparer : IEqualityComparer<ObjectPath>
		{
			// Token: 0x060028B2 RID: 10418 RVA: 0x000EFDDB File Offset: 0x000EDFDB
			public bool Equals(ObjectPath x, ObjectPath y)
			{
				if (x == null)
				{
					return y == null;
				}
				return y != null && x.Equals(y);
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x000EFDF4 File Offset: 0x000EDFF4
			public int GetHashCode(ObjectPath obj)
			{
				int num = 0;
				for (int i = 0; i < obj.Count; i++)
				{
					num ^= (int)obj[i].Key;
					num ^= obj[i].Value.GetHashCode();
				}
				return num;
			}
		}
	}
}
