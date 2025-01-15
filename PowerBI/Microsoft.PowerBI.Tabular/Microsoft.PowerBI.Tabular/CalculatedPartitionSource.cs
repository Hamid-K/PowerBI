using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000EA RID: 234
	public class CalculatedPartitionSource : PartitionSource
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x00076D2E File Offset: 0x00074F2E
		public CalculatedPartitionSource()
		{
			this.standaloneExpression = string.Empty;
			this.standaloneRetainDataTillForceCalculate = false;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00076D48 File Offset: 0x00074F48
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.Calculated;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00076D4B File Offset: 0x00074F4B
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00076D6C File Offset: 0x00074F6C
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00076DED File Offset: 0x00074FED
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00076E10 File Offset: 0x00075010
		[CompatibilityRequirement("1400")]
		public bool RetainDataTillForceCalculate
		{
			get
			{
				if (base.Partition != null)
				{
					return base.Partition.body.RetainDataTillForceCalculate;
				}
				return this.standaloneRetainDataTillForceCalculate;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.RetainDataTillForceCalculate, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(base.Partition, "Source.RetainDataTillForceCalculate", typeof(bool), this.RetainDataTillForceCalculate, value);
					bool retainDataTillForceCalculate = this.RetainDataTillForceCalculate;
					if (base.Partition != null)
					{
						base.Partition.body.RetainDataTillForceCalculate = value;
					}
					else
					{
						this.standaloneRetainDataTillForceCalculate = value;
					}
					ObjectChangeTracker.RegisterPropertyChanged(base.Partition, "Source.RetainDataTillForceCalculate", typeof(bool), retainDataTillForceCalculate, value);
				}
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00076EA5 File Offset: 0x000750A5
		internal override void MoveDataToPartition(Partition partition)
		{
			partition.body.QueryDefinition = this.standaloneExpression;
			partition.body.RetainDataTillForceCalculate = this.standaloneRetainDataTillForceCalculate;
			this.standaloneExpression = null;
			this.standaloneRetainDataTillForceCalculate = false;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00076ED8 File Offset: 0x000750D8
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
			this.standaloneExpression = partition.body.QueryDefinition;
			this.standaloneRetainDataTillForceCalculate = partition.body.RetainDataTillForceCalculate;
			if (resetPartitionBodyProperties)
			{
				partition.body.QueryDefinition = string.Empty;
				partition.body.RetainDataTillForceCalculate = false;
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00076F26 File Offset: 0x00075126
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("QueryDefinition", "Source.Expression");
			yield return new CustomizedPropertyName("RetainDataTillForceCalculate", "Source.RetainDataTillForceCalculate");
			yield break;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00076F30 File Offset: 0x00075130
		internal override void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.Expression))
			{
				jsonObj["expression", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (this.RetainDataTillForceCalculate)
			{
				if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("A RetainDataTillForceCalculate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				jsonObj["retainDataTillForceCalculate", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(true);
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00076FCC File Offset: 0x000751CC
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
			if (!(name == "retainDataTillForceCalculate"))
			{
				return false;
			}
			if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(mode, dbCompatibilityLevel))
			{
				return false;
			}
			this.RetainDataTillForceCalculate = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
			return true;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00077044 File Offset: 0x00075244
		private protected override void SaveMetadataProperties(SerializationActivityContext context, IList<MetadataProperty> properties)
		{
			base.SaveMetadataProperties(context, properties);
			if (!string.IsNullOrEmpty(this.Expression))
			{
				properties.Add(new MetadataProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string), this.Expression));
			}
			if (this.RetainDataTillForceCalculate)
			{
				if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A RetainDataTillForceCalculate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				properties.Add(new MetadataProperty("retainDataTillForceCalculate", MetadataPropertyNature.RegularProperty, typeof(bool), true));
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000770FC File Offset: 0x000752FC
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
			if (!(propertyName == "retainDataTillForceCalculate"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				classification = UnexpectedPropertyClassification.IncompatibleProperty;
				return false;
			}
			this.RetainDataTillForceCalculate = reader.ReadBooleanProperty();
			return true;
		}

		// Token: 0x040001DB RID: 475
		private string standaloneExpression;

		// Token: 0x040001DC RID: 476
		private bool standaloneRetainDataTillForceCalculate;
	}
}
