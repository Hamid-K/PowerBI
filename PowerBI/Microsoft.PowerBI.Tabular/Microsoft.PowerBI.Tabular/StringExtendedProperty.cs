using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000BC RID: 188
	[CompatibilityRequirement("1400")]
	public class StringExtendedProperty : ExtendedProperty
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0005EF98 File Offset: 0x0005D198
		public StringExtendedProperty()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0005EFA6 File Offset: 0x0005D1A6
		internal StringExtendedProperty(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0005EFB5 File Offset: 0x0005D1B5
		private void OnAfterConstructor()
		{
			this.body.Type = ExtendedPropertyType.String;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0005EFC3 File Offset: 0x0005D1C3
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new StringExtendedProperty();
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0005EFCA File Offset: 0x0005D1CA
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0005EFD2 File Offset: 0x0005D1D2
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

		// Token: 0x06000B9A RID: 2970 RVA: 0x0005EFDC File Offset: 0x0005D1DC
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.Value) && writer.ShouldIncludeProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("value", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Value);
			}
			base.WriteRegularPropertiesToMetadataStream(context, writer);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0005F030 File Offset: 0x0005D230
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

		// Token: 0x06000B9C RID: 2972 RVA: 0x0005F070 File Offset: 0x0005D270
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.Value))
			{
				result["value", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Value, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0005F0CD File Offset: 0x0005D2CD
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			if (jsonProp.Name == "value")
			{
				this.Value = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			return false;
		}
	}
}
