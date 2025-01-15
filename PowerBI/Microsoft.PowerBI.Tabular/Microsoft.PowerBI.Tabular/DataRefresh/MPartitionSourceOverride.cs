using System;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000218 RID: 536
	[CompatibilityRequirement("1400")]
	public class MPartitionSourceOverride : PartitionSourceOverride
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000CA72F File Offset: 0x000C892F
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x000CA73C File Offset: 0x000C893C
		public string Expression
		{
			get
			{
				return this.expression.Value;
			}
			set
			{
				this.expression.Value = value;
				if (base.Owner != null)
				{
					base.Owner.ReplacementProperties.QueryDefinition = value;
				}
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000CA763 File Offset: 0x000C8963
		protected override void OnOwnerChanging(PartitionOverride owner)
		{
			if (owner != null)
			{
				owner.ResetReplacementProperties();
				owner.ReplacementProperties.Type = PartitionSourceType.M;
				if (this.expression.IsSet)
				{
					owner.ReplacementProperties.QueryDefinition = this.expression.Value;
				}
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000CA7A0 File Offset: 0x000C89A0
		internal static void WriteSchema(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("MPartitionSourceOverride object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("query");
			writer.WriteValue("m");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000CA874 File Offset: 0x000C8A74
		internal override bool HandleJsonProperty(JProperty property)
		{
			string name = property.Name;
			if (name == "type")
			{
				return true;
			}
			if (!(name == "expression"))
			{
				return false;
			}
			this.Expression = JsonPropertyHelper.ConvertJsonValueToString(property.Value);
			return true;
		}

		// Token: 0x040006EB RID: 1771
		private ReplacementPropertiesCollection.OverridenProperty<string> expression;
	}
}
