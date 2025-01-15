using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000103 RID: 259
	[CompatibilityRequirement("1400")]
	public class MPartitionSource : PartitionSource
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x0007B383 File Offset: 0x00079583
		public MPartitionSource()
		{
			this.standaloneExpression = string.Empty;
			this.standaloneAttributes = string.Empty;
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0007B3A1 File Offset: 0x000795A1
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.M;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0007B3A4 File Offset: 0x000795A4
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0007B3C8 File Offset: 0x000795C8
		public string Expression
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.QueryDefinition;
				}
				return this.standaloneExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Expression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.Expression", typeof(string), this.Expression, value);
					string expression = this.Expression;
					if (base.Partition != null)
					{
						base.Partition.body.QueryDefinition = value;
					}
					else
					{
						this.standaloneExpression = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.Expression", typeof(string), expression, value);
				}
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0007B449 File Offset: 0x00079649
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x0007B46C File Offset: 0x0007966C
		public string Attributes
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.MAttributes;
				}
				return this.standaloneAttributes;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.standaloneAttributes, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.Attributes", typeof(string), this.standaloneAttributes, value);
					string text = this.standaloneAttributes;
					if (base.Partition != null)
					{
						base.Partition.body.MAttributes = value;
					}
					else
					{
						this.standaloneAttributes = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.Attributes", typeof(string), text, value);
				}
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0007B4ED File Offset: 0x000796ED
		internal override void MoveDataToPartition(Partition partition)
		{
			partition.body.QueryDefinition = this.standaloneExpression;
			partition.body.MAttributes = this.standaloneAttributes;
			this.standaloneExpression = null;
			this.standaloneAttributes = null;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0007B51F File Offset: 0x0007971F
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			this.standaloneExpression = partition.body.QueryDefinition;
			this.standaloneAttributes = partition.body.MAttributes;
			if (resetPartitionBodyProperties)
			{
				partition.body.QueryDefinition = string.Empty;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0007B556 File Offset: 0x00079756
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("QueryDefinition", "Source.Expression");
			yield return new CustomizedPropertyName("MAttributes", "Source.Attributes");
			yield break;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0007B560 File Offset: 0x00079760
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.Expression))
			{
				jsonObj["expression", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.Attributes))
			{
				if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("An Attributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				jsonObj["attributes", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Attributes, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0007B610 File Offset: 0x00079810
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "type")
			{
				return true;
			}
			if (name == "expression")
			{
				this.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "attributes"))
			{
				return false;
			}
			if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
			{
				return false;
			}
			this.Attributes = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
			return true;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0007B688 File Offset: 0x00079888
		private protected override void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			base.SaveMetadataProperties(context, properties);
			if (!string.IsNullOrEmpty(this.Expression))
			{
				properties.Add(new MetadataProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.Expression));
			}
			if (!string.IsNullOrEmpty(this.Attributes))
			{
				if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("An Attributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				properties.Add(new MetadataProperty("attributes", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.Attributes));
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0007B748 File Offset: 0x00079948
		private protected override bool TryReadMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName == "expression")
			{
				this.Expression = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "attributes"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				classification = UnexpectedPropertyClassification.IncompatibleProperty;
				return false;
			}
			this.Attributes = reader.ReadStringProperty();
			return true;
		}

		// Token: 0x04000258 RID: 600
		private string standaloneExpression;

		// Token: 0x04000259 RID: 601
		private string standaloneAttributes;
	}
}
