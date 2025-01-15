using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x0200021C RID: 540
	public class QueryPartitionSourceOverride : PartitionSourceOverride
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000CBA78 File Offset: 0x000C9C78
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x000CBA85 File Offset: 0x000C9C85
		public string Query
		{
			get
			{
				return this.query.Value;
			}
			set
			{
				this.query.Value = value;
				if (base.Owner != null)
				{
					base.Owner.ReplacementProperties.QueryDefinition = value;
				}
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000CBAAC File Offset: 0x000C9CAC
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x000CBAB9 File Offset: 0x000C9CB9
		public DataSource DataSource
		{
			get
			{
				return this.dataSource.Value;
			}
			set
			{
				this.dataSource.Value = value;
				if (base.Owner != null)
				{
					base.Owner.ReplacementProperties.DataSourceID = value;
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x000CBAE0 File Offset: 0x000C9CE0
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x000CBAE8 File Offset: 0x000C9CE8
		internal ObjectPath DataSourcePath { get; set; }

		// Token: 0x06001E75 RID: 7797 RVA: 0x000CBAF4 File Offset: 0x000C9CF4
		protected override void OnOwnerChanging(PartitionOverride owner)
		{
			if (owner != null)
			{
				owner.ResetReplacementProperties();
				owner.ReplacementProperties.Type = PartitionSourceType.Query;
				if (this.query.IsSet)
				{
					owner.ReplacementProperties.QueryDefinition = this.query.Value;
				}
				if (this.dataSource.IsSet)
				{
					owner.ReplacementProperties.DataSourceID = this.dataSource.Value;
				}
			}
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000CBB5C File Offset: 0x000C9D5C
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (dbCompatibilityLevel >= 1400)
			{
				writer.WriteStartObject();
			}
			writer.WritePropertyName("description");
			writer.WriteValue("QueryPartitionSourceOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (dbCompatibilityLevel >= 1400)
			{
				writer.WritePropertyName("type");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("query");
				writer.WriteValue("m");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("query");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("dataSource");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			if (dbCompatibilityLevel >= 1400)
			{
				writer.WriteEndObject();
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000CBC58 File Offset: 0x000C9E58
		internal override bool HandleJsonProperty(JProperty property)
		{
			string name = property.Name;
			if (name == "type")
			{
				return true;
			}
			if (name == "query")
			{
				property.Value.VerifyTokenType(8);
				this.Query = (string)property.Value;
				return true;
			}
			if (!(name == "dataSource"))
			{
				return false;
			}
			property.Value.VerifyTokenType(8);
			this.DataSourcePath = new ObjectPath(ObjectType.DataSource, (string)property.Value);
			return true;
		}

		// Token: 0x040006FC RID: 1788
		private ReplacementPropertiesCollection.OverridenProperty<string> query;

		// Token: 0x040006FD RID: 1789
		private ReplacementPropertiesCollection.OverridenProperty<DataSource> dataSource;
	}
}
