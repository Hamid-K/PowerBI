using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000036 RID: 54
	public class CalculatedTableColumn : Column
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00007FDF File Offset: 0x000061DF
		public CalculatedTableColumn()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007FED File Offset: 0x000061ED
		internal CalculatedTableColumn(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007FFC File Offset: 0x000061FC
		private void OnAfterConstructor()
		{
			this.body.Type = ColumnType.CalculatedTableColumn;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000800A File Offset: 0x0000620A
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new CalculatedTableColumn();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008011 File Offset: 0x00006211
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("InferredName", "IsNameInferred");
			yield return new CustomizedPropertyName("InferredDataType", "IsDataTypeInferred");
			yield break;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000801A File Offset: 0x0000621A
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00008022 File Offset: 0x00006222
		public new bool IsNameInferred
		{
			get
			{
				return base.IsNameInferred;
			}
			set
			{
				base.IsNameInferred = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000802B File Offset: 0x0000622B
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00008033 File Offset: 0x00006233
		public new string SourceColumn
		{
			get
			{
				return base.SourceColumn;
			}
			set
			{
				base.SourceColumn = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000803C File Offset: 0x0000623C
		public new Column ColumnOrigin
		{
			get
			{
				return base.ColumnOrigin;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008044 File Offset: 0x00006244
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			if ((this.IsNameInferred || context.SerializationMode == MetadataSerializationMode.Json) && writer.ShouldIncludeProperty("isNameInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
			{
				writer.WriteBooleanProperty("isNameInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, this.IsNameInferred);
			}
			DataType dataType;
			bool flag;
			if (base.ShouldSerializeDataType(out dataType, out flag) && flag && writer.ShouldIncludeProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
			{
				writer.WriteBooleanProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, flag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceColumn) && writer.ShouldIncludeProperty("sourceColumn", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("sourceColumn", MetadataPropertyNature.RegularProperty, this.body.SourceColumn);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000080FC File Offset: 0x000062FC
		private protected override void WriteCrossLinksToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteCrossLinksToMetadataStream(context, writer);
			if (this.body.ColumnOriginID.Object != null && writer.ShouldIncludeProperty("columnOrigin", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				this.body.ColumnOriginID.WriteToMetadataStream(ObjectType.Table, context.SerializationMode != MetadataSerializationMode.Json, "columnOrigin", true, writer);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000815C File Offset: 0x0000635C
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
			string propertyName = reader.PropertyName;
			if (propertyName == "isNameInferred")
			{
				context.ActivityInfo["SerializationActivity::ColumnIsNameInferred"] = reader.ReadBooleanProperty();
				return true;
			}
			if (propertyName == "isDataTypeInferred")
			{
				context.ActivityInfo["SerializationActivity::ColumnIsDataTypeInferred"] = reader.ReadBooleanProperty();
				return true;
			}
			if (propertyName == "sourceColumn")
			{
				this.body.SourceColumn = reader.ReadStringProperty();
				return true;
			}
			if (propertyName == "columnOriginTable")
			{
				if (this.body.ColumnOriginID.Path == null)
				{
					this.body.ColumnOriginID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.ColumnOriginID.Path.Push(ObjectType.Table, reader.ReadStringProperty());
				return true;
			}
			if (propertyName == "columnOriginColumn")
			{
				if (this.body.ColumnOriginID.Path == null)
				{
					this.body.ColumnOriginID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.ColumnOriginID.Path.Push(ObjectType.Column, reader.ReadStringProperty());
				return true;
			}
			if (!(propertyName == "columnOrigin"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.ColumnOriginID.Path = reader.ReadCrossLinkProperty();
			return true;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000082DC File Offset: 0x000064DC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
			result["isNameInferred", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.IsNameInferred);
			DataType dataType;
			bool flag;
			if (base.ShouldSerializeDataType(out dataType, out flag) && flag)
			{
				result["isDataTypeInferred", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(true);
			}
			if (!string.IsNullOrEmpty(this.body.SourceColumn))
			{
				result["sourceColumn", TomPropCategory.Regular, 22, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceColumn, SplitMultilineOptions.None);
			}
			if (!options.IgnoreInferredProperties && this.body.ColumnOriginID.Object != null)
			{
				this.body.ColumnOriginID.SerializeToJsonObject(false, "columnOrigin", ObjectType.Table, result, 23, true);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000083A0 File Offset: 0x000065A0
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			string name = jsonProp.Name;
			if (name == "isNameInferred")
			{
				this.IsNameInferred = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
				return true;
			}
			if (name == "isDataTypeInferred")
			{
				base.IsDataTypeInferred = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
				return true;
			}
			if (name == "sourceColumn")
			{
				this.SourceColumn = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "columnOriginTable")
			{
				if (this.body.ColumnOriginID.Path == null)
				{
					this.body.ColumnOriginID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				}
				this.body.ColumnOriginID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
				return true;
			}
			if (!(name == "columnOriginColumn"))
			{
				return false;
			}
			if (this.body.ColumnOriginID.Path == null)
			{
				this.body.ColumnOriginID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
			}
			this.body.ColumnOriginID.Path.Push(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
			return true;
		}
	}
}
