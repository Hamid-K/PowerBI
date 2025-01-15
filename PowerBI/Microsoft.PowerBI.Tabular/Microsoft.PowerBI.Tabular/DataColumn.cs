using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000050 RID: 80
	public class DataColumn : Column
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0001C2ED File Offset: 0x0001A4ED
		public DataColumn()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001C2FB File Offset: 0x0001A4FB
		internal DataColumn(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001C30A File Offset: 0x0001A50A
		private void OnAfterConstructor()
		{
			this.body.Type = ColumnType.Data;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001C318 File Offset: 0x0001A518
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new DataColumn();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001C31F File Offset: 0x0001A51F
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("InferredDataType", "IsDataTypeInferred");
			yield break;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001C328 File Offset: 0x0001A528
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0001C330 File Offset: 0x0001A530
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

		// Token: 0x06000397 RID: 919 RVA: 0x0001C33C File Offset: 0x0001A53C
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteRegularPropertiesToMetadataStream(context, writer);
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

		// Token: 0x06000398 RID: 920 RVA: 0x0001C3BC File Offset: 0x0001A5BC
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
			if (propertyName == "isDataTypeInferred")
			{
				context.ActivityInfo["SerializationActivity::ColumnIsDataTypeInferred"] = reader.ReadBooleanProperty();
				return true;
			}
			if (!(propertyName == "sourceColumn"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			this.body.SourceColumn = reader.ReadStringProperty();
			return true;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001C434 File Offset: 0x0001A634
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (options.IncludeTranslatablePropertiesOnly)
			{
				return;
			}
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
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001C4AC File Offset: 0x0001A6AC
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (base.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel))
			{
				return true;
			}
			string name = jsonProp.Name;
			if (name == "isDataTypeInferred")
			{
				base.IsDataTypeInferred = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
				return true;
			}
			if (!(name == "sourceColumn"))
			{
				return false;
			}
			this.SourceColumn = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
			return true;
		}
	}
}
