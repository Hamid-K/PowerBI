using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000069 RID: 105
	[CompatibilityRequirement("1400")]
	public class JsonExtendedProperty : ExtendedProperty
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0002ACB7 File Offset: 0x00028EB7
		public JsonExtendedProperty()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002ACC5 File Offset: 0x00028EC5
		internal JsonExtendedProperty(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		private void OnAfterConstructor()
		{
			this.body.Type = ExtendedPropertyType.Json;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002ACE2 File Offset: 0x00028EE2
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new JsonExtendedProperty();
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0002ACE9 File Offset: 0x00028EE9
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0002ACF1 File Offset: 0x00028EF1
		public new string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002ACFC File Offset: 0x00028EFC
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.Value) && writer.ShouldIncludeProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString | MetadataPropertyNature.DefaultProperty, this.body.Value);
			}
			base.WriteRegularPropertiesToMetadataStream(context, writer);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002AD50 File Offset: 0x00028F50
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			if (classification != UnexpectedPropertyClassification.Unclassified)
			{
				return false;
			}
			if (reader.PropertyName == "value")
			{
				this.body.Value = reader.ReadStringProperty();
				return true;
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002AD90 File Offset: 0x00028F90
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.Value))
			{
				result["value", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.Value, "Value");
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002ADE6 File Offset: 0x00028FE6
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			if (jsonProp.Name == "value")
			{
				this.Value = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
				return true;
			}
			return false;
		}
	}
}
