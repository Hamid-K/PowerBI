using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000219 RID: 537
	public class OverrideCollection
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000CA8BA File Offset: 0x000C8ABA
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x000CA8C2 File Offset: 0x000C8AC2
		public MetadataObject Scope
		{
			get
			{
				return this.scopeObject;
			}
			set
			{
				if (value != null && !ObjectTreeHelper.SupportsRefresh(value.ObjectType))
				{
					throw new ArgumentException(TomSR.Exception_OverridesScopeObjectDoesntSupportRefresh(Utils.GetUserFriendlyNameOfObjectType(value.ObjectType)), "Scope");
				}
				this.scopeObject = value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000CA8F6 File Offset: 0x000C8AF6
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x000CA8FE File Offset: 0x000C8AFE
		internal ObjectPath ScopePath { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000CA907 File Offset: 0x000C8B07
		public ICollection<DataSourceOverride> DataSources
		{
			get
			{
				return this.dataSourceOverrides;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x000CA90F File Offset: 0x000C8B0F
		public ICollection<PartitionOverride> Partitions
		{
			get
			{
				return this.partitionOverrides;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x000CA917 File Offset: 0x000C8B17
		public ICollection<ColumnOverride> Columns
		{
			get
			{
				return this.columnOverrides;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x000CA91F File Offset: 0x000C8B1F
		public ICollection<NamedExpressionOverride> Expressions
		{
			get
			{
				return this.expressionOverrides;
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000CA928 File Offset: 0x000C8B28
		internal OverrideCollection Clone()
		{
			OverrideCollection overrideCollection = new OverrideCollection();
			overrideCollection.Scope = this.Scope;
			foreach (DataSourceOverride dataSourceOverride in this.dataSourceOverrides)
			{
				overrideCollection.DataSources.Add(dataSourceOverride);
			}
			foreach (ColumnOverride columnOverride in this.columnOverrides)
			{
				overrideCollection.Columns.Add(columnOverride);
			}
			foreach (PartitionOverride partitionOverride in this.partitionOverrides)
			{
				overrideCollection.Partitions.Add(partitionOverride);
			}
			foreach (NamedExpressionOverride namedExpressionOverride in this.expressionOverrides)
			{
				overrideCollection.Expressions.Add(namedExpressionOverride);
			}
			return overrideCollection;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000CAA70 File Offset: 0x000C8C70
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("OverrideCollection of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("scope");
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Database, true);
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Table, true);
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Partition, true);
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataSources");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			ProviderDataSourceOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WritePropertyName("columns");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			DataColumnOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WritePropertyName("partitions");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			PartitionOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			if (CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("expressions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				NamedExpressionOverride.WriteSchema(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000CAC80 File Offset: 0x000C8E80
		internal void ReadFromJson(JsonTextReader jsonReader)
		{
			jsonReader.VerifyToken(1);
			jsonReader.Read();
			Action<JsonTextReader> <>9__0;
			Action<JsonTextReader> <>9__1;
			Action<JsonTextReader> <>9__2;
			Action<JsonTextReader> <>9__3;
			while (jsonReader.TokenType != 13)
			{
				jsonReader.VerifyToken(4);
				string text = (string)jsonReader.Value;
				if (!(text == "scope"))
				{
					if (!(text == "dataSources"))
					{
						if (!(text == "columns"))
						{
							if (!(text == "partitions"))
							{
								if (!(text == "expressions"))
								{
									throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), jsonReader, null);
								}
								jsonReader.Read();
								JsonTextReader jsonReader2 = jsonReader;
								Action<JsonTextReader> action;
								if ((action = <>9__3) == null)
								{
									action = (<>9__3 = delegate(JsonTextReader innerReader)
									{
										NamedExpressionOverride namedExpressionOverride = new NamedExpressionOverride();
										namedExpressionOverride.ReadFromJson(innerReader);
										this.Expressions.Add(namedExpressionOverride);
									});
								}
								JsonPropertyHelper.ParseArrayOfObjects(jsonReader2, action);
							}
							else
							{
								jsonReader.Read();
								JsonTextReader jsonReader3 = jsonReader;
								Action<JsonTextReader> action2;
								if ((action2 = <>9__2) == null)
								{
									action2 = (<>9__2 = delegate(JsonTextReader innerReader)
									{
										PartitionOverride partitionOverride = new PartitionOverride();
										partitionOverride.ReadFromJson(innerReader);
										this.Partitions.Add(partitionOverride);
									});
								}
								JsonPropertyHelper.ParseArrayOfObjects(jsonReader3, action2);
							}
						}
						else
						{
							jsonReader.Read();
							JsonTextReader jsonReader4 = jsonReader;
							Action<JsonTextReader> action3;
							if ((action3 = <>9__1) == null)
							{
								action3 = (<>9__1 = delegate(JsonTextReader innerReader)
								{
									DataColumnOverride dataColumnOverride = new DataColumnOverride();
									dataColumnOverride.ReadFromJson(innerReader);
									this.Columns.Add(dataColumnOverride);
								});
							}
							JsonPropertyHelper.ParseArrayOfObjects(jsonReader4, action3);
						}
					}
					else
					{
						jsonReader.Read();
						JsonTextReader jsonReader5 = jsonReader;
						Action<JsonTextReader> action4;
						if ((action4 = <>9__0) == null)
						{
							action4 = (<>9__0 = delegate(JsonTextReader innerReader)
							{
								DataSourceOverride dataSourceOverride = ObjectFactory.CreateDataSourceOverrideFromJsonReader(innerReader);
								this.DataSources.Add(dataSourceOverride);
								jsonReader.VerifyToken(13);
								jsonReader.Read();
							});
						}
						JsonPropertyHelper.ParseArrayOfObjects(jsonReader5, action4);
					}
				}
				else
				{
					jsonReader.Read();
					jsonReader.VerifyToken(1);
					this.ScopePath = ObjectPath.Parse(jsonReader);
					jsonReader.VerifyToken(13);
					jsonReader.Read();
				}
			}
			jsonReader.VerifyToken(13);
			jsonReader.Read();
		}

		// Token: 0x040006EC RID: 1772
		private List<DataSourceOverride> dataSourceOverrides = new List<DataSourceOverride>();

		// Token: 0x040006ED RID: 1773
		private List<PartitionOverride> partitionOverrides = new List<PartitionOverride>();

		// Token: 0x040006EE RID: 1774
		private List<ColumnOverride> columnOverrides = new List<ColumnOverride>();

		// Token: 0x040006EF RID: 1775
		private List<NamedExpressionOverride> expressionOverrides = new List<NamedExpressionOverride>();

		// Token: 0x040006F0 RID: 1776
		private MetadataObject scopeObject;
	}
}
