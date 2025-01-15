using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200004F RID: 79
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public class DataBindingHint : BindingInfo
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0001C192 File Offset: 0x0001A392
		public DataBindingHint()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001C1A0 File Offset: 0x0001A3A0
		internal DataBindingHint(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001C1AF File Offset: 0x0001A3AF
		private void OnAfterConstructor()
		{
			this.body.Type = BindingInfoType.DataBindingHint;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001C1BD File Offset: 0x0001A3BD
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new DataBindingHint();
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public new string ConnectionId
		{
			get
			{
				return base.ConnectionId;
			}
			set
			{
				base.ConnectionId = value;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if (!string.IsNullOrEmpty(this.body.ConnectionId) && writer.ShouldIncludeProperty("connectionId", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("connectionId", MetadataPropertyNature.RegularProperty, this.body.ConnectionId);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001C224 File Offset: 0x0001A424
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
			if (reader.PropertyName == "connectionId")
			{
				this.body.ConnectionId = reader.ReadStringProperty();
				return true;
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001C264 File Offset: 0x0001A464
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.ConnectionId))
			{
				result["connectionId", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ConnectionId, SplitMultilineOptions.None);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001C2B6 File Offset: 0x0001A4B6
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			if (jsonProp.Name == "connectionId")
			{
				this.ConnectionId = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			return false;
		}
	}
}
