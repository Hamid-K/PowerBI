using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000035 RID: 53
	public class CalculatedColumn : Column
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00007B75 File Offset: 0x00005D75
		public CalculatedColumn()
		{
			this.OnAfterConstructor();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007B83 File Offset: 0x00005D83
		internal CalculatedColumn(IEqualityComparer<string> comparer)
			: base(comparer)
		{
			this.OnAfterConstructor();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007B92 File Offset: 0x00005D92
		private void OnAfterConstructor()
		{
			this.body.Type = ColumnType.Calculated;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007BA0 File Offset: 0x00005DA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.EvaluationBehavior != EvaluationBehavior.Automatic)
			{
				int num;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior[mode], PropertyHelper.GetEvaluationBehaviorCompatibilityRestrictions(this.body.EvaluationBehavior)[mode], out num);
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "EvaluationBehavior");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007C1B File Offset: 0x00005E1B
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new CalculatedColumn();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007C22 File Offset: 0x00005E22
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("InferredDataType", "IsDataTypeInferred");
			yield break;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00007C2B File Offset: 0x00005E2B
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00007C33 File Offset: 0x00005E33
		public new string Expression
		{
			get
			{
				return base.Expression;
			}
			set
			{
				base.Expression = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00007C3C File Offset: 0x00005E3C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00007C44 File Offset: 0x00005E44
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public new EvaluationBehavior EvaluationBehavior
		{
			get
			{
				return base.EvaluationBehavior;
			}
			set
			{
				base.EvaluationBehavior = value;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007C50 File Offset: 0x00005E50
		private protected override void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			base.WriteRegularPropertiesToMetadataStream(context, writer);
			DataType dataType;
			bool flag;
			if (base.ShouldSerializeDataType(out dataType, out flag) && flag && writer.ShouldIncludeProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
			{
				writer.WriteBooleanProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, flag);
			}
			if (this.body.EvaluationBehavior != EvaluationBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsEvaluationBehaviorValueCompatible(this.body.EvaluationBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property EvaluationBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("evaluationBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<EvaluationBehavior>("evaluationBehavior", MetadataPropertyNature.RegularProperty, this.body.EvaluationBehavior);
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007D70 File Offset: 0x00005F70
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
			if (propertyName == "expression")
			{
				this.body.Expression = reader.ReadStringProperty();
				return true;
			}
			if (!(propertyName == "evaluationBehavior"))
			{
				classification = UnexpectedPropertyClassification.UnknownProperty;
				return false;
			}
			if (!CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				classification = UnexpectedPropertyClassification.IncompatibleProperty;
				return false;
			}
			this.body.EvaluationBehavior = reader.ReadEnumProperty<EvaluationBehavior>();
			return true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007E3C File Offset: 0x0000603C
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
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 24, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (this.body.EvaluationBehavior != EvaluationBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsEvaluationBehaviorValueCompatible(this.body.EvaluationBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Property EvaluationBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["evaluationBehavior", TomPropCategory.Regular, 43, false] = JsonPropertyHelper.ConvertEnumToJsonValue<EvaluationBehavior>(this.EvaluationBehavior);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007F2C File Offset: 0x0000612C
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
			if (name == "expression")
			{
				this.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (!(name == "evaluationBehavior"))
			{
				return false;
			}
			EvaluationBehavior evaluationBehavior = JsonPropertyHelper.ConvertJsonValueToEnum<EvaluationBehavior>(jsonProp.Value);
			if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsEvaluationBehaviorValueCompatible(evaluationBehavior, mode, dbCompatibilityLevel)))
			{
				return false;
			}
			this.EvaluationBehavior = evaluationBehavior;
			return true;
		}
	}
}
