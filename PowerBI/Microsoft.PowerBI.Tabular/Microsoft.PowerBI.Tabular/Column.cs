using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000042 RID: 66
	public abstract class Column : NamedMetadataObject, IMetadataObjectWithOverrides, IMetadataObjectWithLineage
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000FECE File Offset: 0x0000E0CE
		private protected Column()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000FEE1 File Offset: 0x0000E0E1
		private protected Column(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Column.ObjectBody(this);
			this.body.ExplicitName = string.Empty;
			this.body.InferredName = string.Empty;
			this.body.ExplicitDataType = DataType.Automatic;
			this.body.InferredDataType = DataType.Unknown;
			this.body.DataCategory = string.Empty;
			this.body.Description = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.IsNullable = true;
			this.body.Alignment = Alignment.Default;
			this.body.TableDetailPosition = -1;
			this.body.SummarizeBy = AggregateFunction.Default;
			this.body.Type = ColumnType.Data;
			this.body.SourceColumn = string.Empty;
			this.body.Expression = string.Empty;
			this.body.FormatString = string.Empty;
			this.body.IsAvailableInMDX = true;
			this.body.KeepUniqueRows = false;
			this.body.ErrorMessage = string.Empty;
			this.body.SourceProviderType = string.Empty;
			this.body.DisplayFolder = string.Empty;
			this.body.EncodingHint = EncodingHintType.Default;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this.body.EvaluationBehavior = EvaluationBehavior.Automatic;
			this._Variations = new VariationCollection(this, comparer);
			this._Annotations = new ColumnAnnotationCollection(this, comparer);
			this._ExtendedProperties = new ColumnExtendedPropertyCollection(this, comparer);
			this._ChangedProperties = new ColumnChangedPropertyCollection(this);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0001008D File Offset: 0x0000E28D
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Column;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00010090 File Offset: 0x0000E290
		// (set) Token: 0x06000253 RID: 595 RVA: 0x000100A2 File Offset: 0x0000E2A2
		public override MetadataObject Parent
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (this.body.TableID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Column, Table>(this.body.TableID, (Table)value, null, null);
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000100CF File Offset: 0x0000E2CF
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000100E4 File Offset: 0x0000E2E4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				Column.WriteMetadataSchemaForDataColumn(context, writer);
				Column.WriteMetadataSchemaForRowNumberColumn(context, writer);
				Column.WriteMetadataSchemaForCalculatedTableColumn(context, writer);
				Column.WriteMetadataSchemaForCalculatedColumn(context, writer);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00010130 File Offset: 0x0000E330
		private static void WriteMetadataSchemaForDataColumn(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Column, "DataColumn", "DataColumn object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ColumnType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				Column.WriteMetadataSchemaForCommonColumnRegularProperties(context, writer);
				if (writer.ShouldIncludeProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("sourceColumn", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceColumn", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				Column.WriteMetadataSchemaForCommonColumnCrossLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0001021C File Offset: 0x0000E41C
		private static void WriteMetadataSchemaForRowNumberColumn(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Column, "RowNumberColumn", "RowNumberColumn object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ColumnType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				Column.WriteMetadataSchemaForCommonColumnRegularProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnCrossLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private static void WriteMetadataSchemaForCalculatedTableColumn(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Column, "CalculatedTableColumn", "CalculatedTableColumn object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ColumnType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				Column.WriteMetadataSchemaForCommonColumnRegularProperties(context, writer);
				if (writer.ShouldIncludeProperty("isNameInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("isNameInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("sourceColumn", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceColumn", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				Column.WriteMetadataSchemaForCommonColumnCrossLinkProperties(context, writer);
				if (writer.ShouldIncludeProperty("columnOrigin", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
				{
					CrossLink<Column, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, context.SerializationMode != MetadataSerializationMode.Json, "columnOrigin", true, writer);
				}
				Column.WriteMetadataSchemaForCommonColumnChildLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00010408 File Offset: 0x0000E608
		private static void WriteMetadataSchemaForCalculatedColumn(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Column, "CalculatedColumn", "CalculatedColumn object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<ColumnType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				Column.WriteMetadataSchemaForCommonColumnRegularProperties(context, writer);
				if (writer.ShouldIncludeProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("isDataTypeInferred", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Inferred, typeof(bool));
				}
				if (CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("evaluationBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<EvaluationBehavior>("evaluationBehavior", MetadataPropertyNature.RegularProperty, null);
				}
				Column.WriteMetadataSchemaForCommonColumnCrossLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildLinkProperties(context, writer);
				Column.WriteMetadataSchemaForCommonColumnChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00010530 File Offset: 0x0000E730
		private static void WriteMetadataSchemaForCommonColumnRegularProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("dataType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataType>("dataType", MetadataPropertyNature.RegularProperty, null);
			}
			if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
			}
			if (writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isHidden", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
			}
			if (writer.ShouldIncludeProperty("isUnique", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isUnique", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("isKey", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isKey", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("isNullable", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isNullable", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("formatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("formatString", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("isAvailableInMdx", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isAvailableInMdx", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
			if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
			if (writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
			if (writer.ShouldIncludeProperty("keepUniqueRows", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("keepUniqueRows", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("displayOrdinal", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("displayOrdinal", MetadataPropertyNature.RegularProperty, typeof(int));
			}
			if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
			}
			if (writer.ShouldIncludeProperty("sourceProviderType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("sourceProviderType", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
			}
			if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("encodingHint", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<EncodingHintType>("encodingHint", MetadataPropertyNature.RegularProperty, null);
			}
			if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("dataCategory", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("dataCategory", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("alignment", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<Alignment>("alignment", MetadataPropertyNature.RegularProperty, null);
			}
			if (writer.ShouldIncludeProperty("tableDetailPosition", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("tableDetailPosition", MetadataPropertyNature.RegularProperty, typeof(int));
			}
			if (writer.ShouldIncludeProperty("isDefaultLabel", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isDefaultLabel", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("isDefaultImage", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("isDefaultImage", MetadataPropertyNature.RegularProperty, typeof(bool));
			}
			if (writer.ShouldIncludeProperty("summarizeBy", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<AggregateFunction>("summarizeBy", MetadataPropertyNature.RegularProperty, null);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00010948 File Offset: 0x0000EB48
		private static void WriteMetadataSchemaForCommonColumnCrossLinkProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("sortByColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				CrossLink<Column, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Column, context.SerializationMode != MetadataSerializationMode.Json, "sortByColumn", false, writer);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00010974 File Offset: 0x0000EB74
		private static void WriteMetadataSchemaForCommonColumnChildLinkProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("attributeHierarchy", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteSingleChild(context, "attributeHierarchy", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, ObjectType.AttributeHierarchy);
			}
			if (CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("relatedColumnDetails", MetadataPropertyNature.ChildProperty))
			{
				writer.WriteSingleChild(context, "relatedColumnDetails", MetadataPropertyNature.ChildProperty, ObjectType.RelatedColumnDetails);
			}
			if (CompatibilityRestrictions.Column_AlternateOf.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("alternateOf", MetadataPropertyNature.ChildProperty))
			{
				writer.WriteSingleChild(context, "alternateOf", MetadataPropertyNature.ChildProperty, ObjectType.AlternateOf);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00010A10 File Offset: 0x0000EC10
		private static void WriteMetadataSchemaForCommonColumnChildCollectionsProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (CompatibilityRestrictions.Variation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("variations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "variations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Variation);
			}
			if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
			}
			if (CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ChangedProperty);
			}
			if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.EncodingHint != EncodingHintType.Default)
			{
				int num;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Column_EncodingHint[mode], PropertyHelper.GetEncodingHintTypeCompatibilityRestrictions(this.body.EncodingHint)[mode], out num);
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "EncodingHint");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				int num2 = CompatibilityRestrictions.Column_LineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				int num3 = CompatibilityRestrictions.Column_SourceLineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.EvaluationBehavior != EvaluationBehavior.Automatic)
			{
				int num4;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Column_EvaluationBehavior[mode], PropertyHelper.GetEvaluationBehaviorCompatibilityRestrictions(this.body.EvaluationBehavior)[mode], out num4);
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "EvaluationBehavior");
					requiredLevel = num4;
					int num5 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00010C5A File Offset: 0x0000EE5A
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00010C62 File Offset: 0x0000EE62
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Column.ObjectBody)value;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00010C70 File Offset: 0x0000EE70
		internal override ITxObjectBody CreateBody()
		{
			return new Column.ObjectBody(this);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00010C78 File Offset: 0x0000EE78
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Table)parent).Columns;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00010C88 File Offset: 0x0000EE88
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<Column, Table>(this.body.TableID, objectMap, throwIfCantResolve, null, null);
			this.body.ColumnOriginID.ResolveById(objectMap, throwIfCantResolve);
			this.body.SortByColumnID.ResolveById(objectMap, throwIfCantResolve);
			this.body.AttributeHierarchyID.ResolveById(objectMap, throwIfCantResolve);
			this.body.RelatedColumnDetailsID.ResolveById(objectMap, throwIfCantResolve);
			this.body.AlternateOfID.ResolveById(objectMap, throwIfCantResolve);
			if (table != null)
			{
				table.Columns.Add(this);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00010D18 File Offset: 0x0000EF18
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.ColumnOriginID.ResolveById(objectMap, throwIfCantResolve);
			this.body.SortByColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00010D40 File Offset: 0x0000EF40
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.ColumnOriginID.IsResolved && !this.body.ColumnOriginID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ColumnOrigin"));
				}
				flag = false;
			}
			if (!this.body.SortByColumnID.IsResolved && !this.body.SortByColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SortByColumn"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.ColumnOriginID.TryResolveAfterCopy(copyContext);
			this.body.SortByColumnID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00010DFE File Offset: 0x0000EFFE
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.ColumnOriginID.Validate(result, throwOnError);
			this.body.SortByColumnID.Validate(result, throwOnError);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00010E24 File Offset: 0x0000F024
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.ColumnOriginID.IsResolved || !this.body.SortByColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010E54 File Offset: 0x0000F054
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.AttributeHierarchyID.Object != null)
			{
				yield return this.body.AttributeHierarchyID.Object;
			}
			if (this.body.RelatedColumnDetailsID.Object != null)
			{
				yield return this.body.RelatedColumnDetailsID.Object;
			}
			if (this.body.AlternateOfID.Object != null)
			{
				yield return this.body.AlternateOfID.Object;
			}
			yield break;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00010E64 File Offset: 0x0000F064
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Variations;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ChangedProperties;
			yield break;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00010E74 File Offset: 0x0000F074
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			ObjectType objectType = child.ObjectType;
			if (objectType == ObjectType.AttributeHierarchy)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "AttributeHierarchy", typeof(AttributeHierarchy), this.body.AttributeHierarchyID.Object, child);
				AttributeHierarchy @object = this.body.AttributeHierarchyID.Object;
				this.body.AttributeHierarchyID.Object = (AttributeHierarchy)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "AttributeHierarchy", typeof(AttributeHierarchy), @object, child);
				return;
			}
			if (objectType == ObjectType.RelatedColumnDetails)
			{
				base.ValidateCompatibilityRequirement(child, "RelatedColumnDetails", CompatibilityRestrictions.Column_RelatedColumnDetails);
				ObjectChangeTracker.RegisterPropertyChanging(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), this.body.RelatedColumnDetailsID.Object, child);
				RelatedColumnDetails object2 = this.body.RelatedColumnDetailsID.Object;
				this.body.RelatedColumnDetailsID.Object = (RelatedColumnDetails)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), object2, child);
				return;
			}
			if (objectType != ObjectType.AlternateOf)
			{
				base.SetDirectChildImpl(child);
				return;
			}
			base.ValidateCompatibilityRequirement(child, "AlternateOf", CompatibilityRestrictions.Column_AlternateOf);
			ObjectChangeTracker.RegisterPropertyChanging(this, "AlternateOf", typeof(AlternateOf), this.body.AlternateOfID.Object, child);
			AlternateOf object3 = this.body.AlternateOfID.Object;
			this.body.AlternateOfID.Object = (AlternateOf)child;
			ObjectChangeTracker.RegisterPropertyChanged(this, "AlternateOf", typeof(AlternateOf), object3, child);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			ObjectType objectType = child.ObjectType;
			if (objectType != ObjectType.AttributeHierarchy)
			{
				if (objectType != ObjectType.RelatedColumnDetails)
				{
					if (objectType != ObjectType.AlternateOf)
					{
						base.RemoveDirectChildImpl(child);
					}
					else if (this.body.AlternateOfID.ObjectID == child.Id)
					{
						ObjectChangeTracker.RegisterPropertyChanging(this, "AlternateOf", typeof(AlternateOf), this.body.AlternateOfID.Object, null);
						base.ResetCompatibilityRequirement();
						AlternateOf @object = this.body.AlternateOfID.Object;
						this.body.AlternateOfID.Object = null;
						ObjectChangeTracker.RegisterPropertyChanged(this, "AlternateOf", typeof(AlternateOf), @object, null);
						return;
					}
				}
				else if (this.body.RelatedColumnDetailsID.ObjectID == child.Id)
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), this.body.RelatedColumnDetailsID.Object, null);
					base.ResetCompatibilityRequirement();
					RelatedColumnDetails object2 = this.body.RelatedColumnDetailsID.Object;
					this.body.RelatedColumnDetailsID.Object = null;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), object2, null);
					return;
				}
			}
			else if (this.body.AttributeHierarchyID.ObjectID == child.Id)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "AttributeHierarchy", typeof(AttributeHierarchy), this.body.AttributeHierarchyID.Object, null);
				AttributeHierarchy object3 = this.body.AttributeHierarchyID.Object;
				this.body.AttributeHierarchyID.Object = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "AttributeHierarchy", typeof(AttributeHierarchy), object3, null);
				return;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000111B5 File Offset: 0x0000F3B5
		[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
		public VariationCollection Variations
		{
			get
			{
				return this._Variations;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000111BD File Offset: 0x0000F3BD
		public ColumnAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000111C5 File Offset: 0x0000F3C5
		[CompatibilityRequirement("1400")]
		public ColumnExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000111CD File Offset: 0x0000F3CD
		[CompatibilityRequirement("1567")]
		public ColumnChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000111D5 File Offset: 0x0000F3D5
		// (set) Token: 0x06000272 RID: 626 RVA: 0x000111E4 File Offset: 0x0000F3E4
		internal string ExplicitName
		{
			get
			{
				return this.body.ExplicitName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ExplicitName, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Column, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ExplicitName", typeof(string), this.body.ExplicitName, value);
					string explicitName = this.body.ExplicitName;
					this.body.ExplicitName = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ExplicitName", typeof(string), explicitName, value);
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00011266 File Offset: 0x0000F466
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00011274 File Offset: 0x0000F474
		internal string InferredName
		{
			get
			{
				return this.body.InferredName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.InferredName, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Column, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "InferredName", typeof(string), this.body.InferredName, value);
					string inferredName = this.body.InferredName;
					this.body.InferredName = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "InferredName", typeof(string), inferredName, value);
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000112F6 File Offset: 0x0000F4F6
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00011304 File Offset: 0x0000F504
		internal DataType ExplicitDataType
		{
			get
			{
				return this.body.ExplicitDataType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ExplicitDataType, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ExplicitDataType", typeof(DataType), this.body.ExplicitDataType, value);
					DataType explicitDataType = this.body.ExplicitDataType;
					this.body.ExplicitDataType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ExplicitDataType", typeof(DataType), explicitDataType, value);
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00011388 File Offset: 0x0000F588
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00011398 File Offset: 0x0000F598
		internal DataType InferredDataType
		{
			get
			{
				return this.body.InferredDataType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.InferredDataType, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "InferredDataType", typeof(DataType), this.body.InferredDataType, value);
					DataType inferredDataType = this.body.InferredDataType;
					this.body.InferredDataType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "InferredDataType", typeof(DataType), inferredDataType, value);
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0001141C File Offset: 0x0000F61C
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0001142C File Offset: 0x0000F62C
		public string DataCategory
		{
			get
			{
				return this.body.DataCategory;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataCategory, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataCategory", typeof(string), this.body.DataCategory, value);
					string dataCategory = this.body.DataCategory;
					this.body.DataCategory = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataCategory", typeof(string), dataCategory, value);
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0001149C File Offset: 0x0000F69C
		// (set) Token: 0x0600027C RID: 636 RVA: 0x000114AC File Offset: 0x0000F6AC
		public string Description
		{
			get
			{
				return this.body.Description;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Description, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Description", typeof(string), this.body.Description, value);
					string description = this.body.Description;
					this.body.Description = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Description", typeof(string), description, value);
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0001151C File Offset: 0x0000F71C
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0001152C File Offset: 0x0000F72C
		public bool IsHidden
		{
			get
			{
				return this.body.IsHidden;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsHidden, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsHidden", typeof(bool), this.body.IsHidden, value);
					bool isHidden = this.body.IsHidden;
					this.body.IsHidden = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsHidden", typeof(bool), isHidden, value);
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000115B0 File Offset: 0x0000F7B0
		// (set) Token: 0x06000280 RID: 640 RVA: 0x000115C0 File Offset: 0x0000F7C0
		public ObjectState State
		{
			get
			{
				return this.body.State;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.State, value))
				{
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions = PropertyHelper.GetObjectStateCompatibilityRestrictions(value);
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions2 = PropertyHelper.GetObjectStateCompatibilityRestrictions(this.body.State);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = objectStateCompatibilityRestrictions.Compare(objectStateCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.Ready))
					{
						array = base.ValidateCompatibilityRequirement(objectStateCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "State", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "State", typeof(ObjectState), this.body.State, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					}
					ObjectState state = this.body.State;
					this.body.State = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "State", typeof(ObjectState), state, value);
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000116E2 File Offset: 0x0000F8E2
		// (set) Token: 0x06000282 RID: 642 RVA: 0x000116F0 File Offset: 0x0000F8F0
		public bool IsUnique
		{
			get
			{
				return this.body.IsUnique;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsUnique, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsUnique", typeof(bool), this.body.IsUnique, value);
					bool isUnique = this.body.IsUnique;
					this.body.IsUnique = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsUnique", typeof(bool), isUnique, value);
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00011774 File Offset: 0x0000F974
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00011784 File Offset: 0x0000F984
		public bool IsKey
		{
			get
			{
				return this.body.IsKey;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsKey, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsKey", typeof(bool), this.body.IsKey, value);
					bool isKey = this.body.IsKey;
					this.body.IsKey = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsKey", typeof(bool), isKey, value);
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00011808 File Offset: 0x0000FA08
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00011818 File Offset: 0x0000FA18
		public bool IsNullable
		{
			get
			{
				return this.body.IsNullable;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsNullable, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsNullable", typeof(bool), this.body.IsNullable, value);
					bool isNullable = this.body.IsNullable;
					this.body.IsNullable = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsNullable", typeof(bool), isNullable, value);
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0001189C File Offset: 0x0000FA9C
		// (set) Token: 0x06000288 RID: 648 RVA: 0x000118AC File Offset: 0x0000FAAC
		public Alignment Alignment
		{
			get
			{
				return this.body.Alignment;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Alignment, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Alignment", typeof(Alignment), this.body.Alignment, value);
					Alignment alignment = this.body.Alignment;
					this.body.Alignment = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Alignment", typeof(Alignment), alignment, value);
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00011930 File Offset: 0x0000FB30
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00011940 File Offset: 0x0000FB40
		public int TableDetailPosition
		{
			get
			{
				return this.body.TableDetailPosition;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TableDetailPosition, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TableDetailPosition", typeof(int), this.body.TableDetailPosition, value);
					int tableDetailPosition = this.body.TableDetailPosition;
					this.body.TableDetailPosition = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TableDetailPosition", typeof(int), tableDetailPosition, value);
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000119C4 File Offset: 0x0000FBC4
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public bool IsDefaultLabel
		{
			get
			{
				return this.body.IsDefaultLabel;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsDefaultLabel, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsDefaultLabel", typeof(bool), this.body.IsDefaultLabel, value);
					bool isDefaultLabel = this.body.IsDefaultLabel;
					this.body.IsDefaultLabel = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsDefaultLabel", typeof(bool), isDefaultLabel, value);
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00011A58 File Offset: 0x0000FC58
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00011A68 File Offset: 0x0000FC68
		public bool IsDefaultImage
		{
			get
			{
				return this.body.IsDefaultImage;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsDefaultImage, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsDefaultImage", typeof(bool), this.body.IsDefaultImage, value);
					bool isDefaultImage = this.body.IsDefaultImage;
					this.body.IsDefaultImage = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsDefaultImage", typeof(bool), isDefaultImage, value);
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00011AEC File Offset: 0x0000FCEC
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00011AFC File Offset: 0x0000FCFC
		public AggregateFunction SummarizeBy
		{
			get
			{
				return this.body.SummarizeBy;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SummarizeBy, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "SummarizeBy", typeof(AggregateFunction), this.body.SummarizeBy, value);
					AggregateFunction summarizeBy = this.body.SummarizeBy;
					this.body.SummarizeBy = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SummarizeBy", typeof(AggregateFunction), summarizeBy, value);
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00011B80 File Offset: 0x0000FD80
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00011B90 File Offset: 0x0000FD90
		public ColumnType Type
		{
			get
			{
				return this.body.Type;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Type, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Type", typeof(ColumnType), this.body.Type, value);
					ColumnType type = this.body.Type;
					this.body.Type = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Type", typeof(ColumnType), type, value);
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00011C14 File Offset: 0x0000FE14
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00011C24 File Offset: 0x0000FE24
		internal string SourceColumn
		{
			get
			{
				return this.body.SourceColumn;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceColumn, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceColumn", typeof(string), this.body.SourceColumn, value);
					string sourceColumn = this.body.SourceColumn;
					this.body.SourceColumn = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceColumn", typeof(string), sourceColumn, value);
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00011C94 File Offset: 0x0000FE94
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		internal string Expression
		{
			get
			{
				return this.body.Expression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Expression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Expression", typeof(string), this.body.Expression, value);
					string expression = this.body.Expression;
					this.body.Expression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Expression", typeof(string), expression, value);
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00011D14 File Offset: 0x0000FF14
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00011D24 File Offset: 0x0000FF24
		public string FormatString
		{
			get
			{
				return this.body.FormatString;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FormatString, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FormatString", typeof(string), this.body.FormatString, value);
					string formatString = this.body.FormatString;
					this.body.FormatString = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FormatString", typeof(string), formatString, value);
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00011D94 File Offset: 0x0000FF94
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public bool IsAvailableInMDX
		{
			get
			{
				return this.body.IsAvailableInMDX;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsAvailableInMDX, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsAvailableInMDX", typeof(bool), this.body.IsAvailableInMDX, value);
					bool isAvailableInMDX = this.body.IsAvailableInMDX;
					this.body.IsAvailableInMDX = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsAvailableInMDX", typeof(bool), isAvailableInMDX, value);
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00011E28 File Offset: 0x00010028
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00011E38 File Offset: 0x00010038
		public DateTime ModifiedTime
		{
			get
			{
				return this.body.ModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ModifiedTime", typeof(DateTime), this.body.ModifiedTime, value);
					DateTime modifiedTime = this.body.ModifiedTime;
					this.body.ModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ModifiedTime", typeof(DateTime), modifiedTime, value);
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00011EBC File Offset: 0x000100BC
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00011ECC File Offset: 0x000100CC
		public DateTime StructureModifiedTime
		{
			get
			{
				return this.body.StructureModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StructureModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StructureModifiedTime", typeof(DateTime), this.body.StructureModifiedTime, value);
					DateTime structureModifiedTime = this.body.StructureModifiedTime;
					this.body.StructureModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "StructureModifiedTime", typeof(DateTime), structureModifiedTime, value);
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00011F50 File Offset: 0x00010150
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00011F60 File Offset: 0x00010160
		public DateTime RefreshedTime
		{
			get
			{
				return this.body.RefreshedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RefreshedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshedTime", typeof(DateTime), this.body.RefreshedTime, value);
					DateTime refreshedTime = this.body.RefreshedTime;
					this.body.RefreshedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshedTime", typeof(DateTime), refreshedTime, value);
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00011FE4 File Offset: 0x000101E4
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00011FF4 File Offset: 0x000101F4
		public bool KeepUniqueRows
		{
			get
			{
				return this.body.KeepUniqueRows;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.KeepUniqueRows, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "KeepUniqueRows", typeof(bool), this.body.KeepUniqueRows, value);
					bool keepUniqueRows = this.body.KeepUniqueRows;
					this.body.KeepUniqueRows = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "KeepUniqueRows", typeof(bool), keepUniqueRows, value);
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00012078 File Offset: 0x00010278
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00012088 File Offset: 0x00010288
		public int DisplayOrdinal
		{
			get
			{
				return this.body.DisplayOrdinal;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DisplayOrdinal, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DisplayOrdinal", typeof(int), this.body.DisplayOrdinal, value);
					int displayOrdinal = this.body.DisplayOrdinal;
					this.body.DisplayOrdinal = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DisplayOrdinal", typeof(int), displayOrdinal, value);
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0001210C File Offset: 0x0001030C
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0001211C File Offset: 0x0001031C
		public string ErrorMessage
		{
			get
			{
				return this.body.ErrorMessage;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ErrorMessage, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ErrorMessage", typeof(string), this.body.ErrorMessage, value);
					string errorMessage = this.body.ErrorMessage;
					this.body.ErrorMessage = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ErrorMessage", typeof(string), errorMessage, value);
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0001218C File Offset: 0x0001038C
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0001219C File Offset: 0x0001039C
		public string SourceProviderType
		{
			get
			{
				return this.body.SourceProviderType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceProviderType, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceProviderType", typeof(string), this.body.SourceProviderType, value);
					string sourceProviderType = this.body.SourceProviderType;
					this.body.SourceProviderType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceProviderType", typeof(string), sourceProviderType, value);
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001220C File Offset: 0x0001040C
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0001221C File Offset: 0x0001041C
		public string DisplayFolder
		{
			get
			{
				return this.body.DisplayFolder;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DisplayFolder, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DisplayFolder", typeof(string), this.body.DisplayFolder, value);
					string displayFolder = this.body.DisplayFolder;
					this.body.DisplayFolder = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DisplayFolder", typeof(string), displayFolder, value);
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0001228C File Offset: 0x0001048C
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0001229C File Offset: 0x0001049C
		[CompatibilityRequirement("1400")]
		public EncodingHintType EncodingHint
		{
			get
			{
				return this.body.EncodingHint;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.EncodingHint, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Column_EncodingHint.Merge(PropertyHelper.GetEncodingHintTypeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Column_EncodingHint.Merge(PropertyHelper.GetEncodingHintTypeCompatibilityRestrictions(this.body.EncodingHint));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != EncodingHintType.Default))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "EncodingHint", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "EncodingHint", typeof(EncodingHintType), this.body.EncodingHint, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					}
					EncodingHintType encodingHint = this.body.EncodingHint;
					this.body.EncodingHint = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "EncodingHint", typeof(EncodingHintType), encodingHint, value);
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000123D1 File Offset: 0x000105D1
		// (set) Token: 0x060002AE RID: 686 RVA: 0x000123E0 File Offset: 0x000105E0
		[CompatibilityRequirement("1540")]
		public string LineageTag
		{
			get
			{
				return this.body.LineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.LineageTag, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Column_LineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Column_LineageTag, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string lineageTag = this.body.LineageTag;
					this.body.LineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LineageTag", typeof(string), lineageTag, value);
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00012495 File Offset: 0x00010695
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x000124A4 File Offset: 0x000106A4
		[CompatibilityRequirement("1550")]
		public string SourceLineageTag
		{
			get
			{
				return this.body.SourceLineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceLineageTag, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Column_SourceLineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Column_SourceLineageTag, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string sourceLineageTag = this.body.SourceLineageTag;
					this.body.SourceLineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceLineageTag", typeof(string), sourceLineageTag, value);
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00012559 File Offset: 0x00010759
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00012568 File Offset: 0x00010768
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		internal EvaluationBehavior EvaluationBehavior
		{
			get
			{
				return this.body.EvaluationBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.EvaluationBehavior, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Column_EvaluationBehavior.Merge(PropertyHelper.GetEvaluationBehaviorCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Column_EvaluationBehavior.Merge(PropertyHelper.GetEvaluationBehaviorCompatibilityRestrictions(this.body.EvaluationBehavior));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != EvaluationBehavior.Automatic))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "EvaluationBehavior", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "EvaluationBehavior", typeof(EvaluationBehavior), this.body.EvaluationBehavior, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					}
					EvaluationBehavior evaluationBehavior = this.body.EvaluationBehavior;
					this.body.EvaluationBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "EvaluationBehavior", typeof(EvaluationBehavior), evaluationBehavior, value);
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001269E File Offset: 0x0001089E
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000126B0 File Offset: 0x000108B0
		public Table Table
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Table", typeof(Table), this.body.TableID.Object, value);
					Table @object = this.body.TableID.Object;
					this.body.TableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Table", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00012734 File Offset: 0x00010934
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00012746 File Offset: 0x00010946
		internal ObjectId _TableID
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
			set
			{
				this.body.TableID.ObjectID = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00012759 File Offset: 0x00010959
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0001276C File Offset: 0x0001096C
		internal Column ColumnOrigin
		{
			get
			{
				return this.body.ColumnOriginID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ColumnOriginID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ColumnOrigin", typeof(Column), this.body.ColumnOriginID.Object, value);
					Column @object = this.body.ColumnOriginID.Object;
					this.body.ColumnOriginID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ColumnOrigin", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000127F0 File Offset: 0x000109F0
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00012802 File Offset: 0x00010A02
		internal ObjectId _ColumnOriginID
		{
			get
			{
				return this.body.ColumnOriginID.ObjectID;
			}
			set
			{
				this.body.ColumnOriginID.ObjectID = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00012815 File Offset: 0x00010A15
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00012828 File Offset: 0x00010A28
		public Column SortByColumn
		{
			get
			{
				return this.body.SortByColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SortByColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "SortByColumn", typeof(Column), this.body.SortByColumnID.Object, value);
					Column @object = this.body.SortByColumnID.Object;
					this.body.SortByColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SortByColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000128AC File Offset: 0x00010AAC
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000128BE File Offset: 0x00010ABE
		internal ObjectId _SortByColumnID
		{
			get
			{
				return this.body.SortByColumnID.ObjectID;
			}
			set
			{
				this.body.SortByColumnID.ObjectID = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000128D1 File Offset: 0x00010AD1
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000128E4 File Offset: 0x00010AE4
		public AttributeHierarchy AttributeHierarchy
		{
			get
			{
				return this.body.AttributeHierarchyID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.AttributeHierarchyID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "AttributeHierarchy", null);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "AttributeHierarchy", typeof(AttributeHierarchy), this.body.AttributeHierarchyID.Object, value);
					AttributeHierarchy @object = this.body.AttributeHierarchyID.Object;
					this.body.AttributeHierarchyID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "AttributeHierarchy", typeof(AttributeHierarchy), @object, value);
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00012979 File Offset: 0x00010B79
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0001298B File Offset: 0x00010B8B
		internal ObjectId _AttributeHierarchyID
		{
			get
			{
				return this.body.AttributeHierarchyID.ObjectID;
			}
			set
			{
				this.body.AttributeHierarchyID.ObjectID = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0001299E File Offset: 0x00010B9E
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x000129B0 File Offset: 0x00010BB0
		[CompatibilityRequirement(Pbi = "1400")]
		public RelatedColumnDetails RelatedColumnDetails
		{
			get
			{
				return this.body.RelatedColumnDetailsID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RelatedColumnDetailsID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "RelatedColumnDetails", CompatibilityRestrictions.Column_RelatedColumnDetails);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), this.body.RelatedColumnDetailsID.Object, value);
					RelatedColumnDetails @object = this.body.RelatedColumnDetailsID.Object;
					this.body.RelatedColumnDetailsID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RelatedColumnDetails", typeof(RelatedColumnDetails), @object, value);
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00012A49 File Offset: 0x00010C49
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00012A5B File Offset: 0x00010C5B
		internal ObjectId _RelatedColumnDetailsID
		{
			get
			{
				return this.body.RelatedColumnDetailsID.ObjectID;
			}
			set
			{
				this.body.RelatedColumnDetailsID.ObjectID = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00012A6E File Offset: 0x00010C6E
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00012A80 File Offset: 0x00010C80
		[CompatibilityRequirement("1460")]
		public AlternateOf AlternateOf
		{
			get
			{
				return this.body.AlternateOfID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.AlternateOfID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "AlternateOf", CompatibilityRestrictions.Column_AlternateOf);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "AlternateOf", typeof(AlternateOf), this.body.AlternateOfID.Object, value);
					AlternateOf @object = this.body.AlternateOfID.Object;
					this.body.AlternateOfID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "AlternateOf", typeof(AlternateOf), @object, value);
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00012B19 File Offset: 0x00010D19
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00012B2B File Offset: 0x00010D2B
		internal ObjectId _AlternateOfID
		{
			get
			{
				return this.body.AlternateOfID.ObjectID;
			}
			set
			{
				this.body.AlternateOfID.ObjectID = value;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00012B40 File Offset: 0x00010D40
		internal void CopyFrom(Column other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0 || this.body.StructureModifiedTime.CompareTo(other.body.StructureModifiedTime) != 0 || this.body.RefreshedTime.CompareTo(other.body.RefreshedTime) != 0;
			}
			else
			{
				flag = !this.body.IsEqualTo(other.body, context);
			}
			if (flag)
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			else if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				if (this.body.AttributeHierarchyID.Object != null && other.body.AttributeHierarchyID.Object != null)
				{
					this.body.AttributeHierarchyID.Object.CopyFrom(other.body.AttributeHierarchyID.Object, context);
				}
				if (this.body.RelatedColumnDetailsID.Object != null && other.body.RelatedColumnDetailsID.Object != null)
				{
					this.body.RelatedColumnDetailsID.Object.CopyFrom(other.body.RelatedColumnDetailsID.Object, context);
				}
				if (this.body.AlternateOfID.Object != null && other.body.AlternateOfID.Object != null)
				{
					this.body.AlternateOfID.Object.CopyFrom(other.body.AlternateOfID.Object, context);
				}
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Variations.CopyFrom(other.Variations, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ChangedProperties.CopyFrom(other.ChangedProperties, context);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00012D59 File Offset: 0x00010F59
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Column)other, context);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00012D68 File Offset: 0x00010F68
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Column other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00012D84 File Offset: 0x00010F84
		public void CopyTo(Column other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00012DA0 File Offset: 0x00010FA0
		public Column Clone()
		{
			return base.CloneInternal<Column>();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00012DA8 File Offset: 0x00010FA8
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			this.body.SortByColumnID.Validate(null, true);
			if (this.body.SortByColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "SortByColumnID", this.body.SortByColumnID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.ExplicitName))
			{
				writer.WriteProperty<string>(options, "ExplicitName", this.body.ExplicitName);
			}
			if (!string.IsNullOrEmpty(this.body.InferredName))
			{
				writer.WriteProperty<string>(options, "InferredName", this.body.InferredName);
			}
			if (this.body.ExplicitDataType != DataType.Automatic)
			{
				writer.WriteProperty<DataType>(options, "ExplicitDataType", this.body.ExplicitDataType);
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory))
			{
				writer.WriteProperty<string>(options, "DataCategory", this.body.DataCategory);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (this.body.IsHidden)
			{
				writer.WriteProperty<bool>(options, "IsHidden", this.body.IsHidden);
			}
			if (this.body.IsUnique)
			{
				writer.WriteProperty<bool>(options, "IsUnique", this.body.IsUnique);
			}
			if (this.body.IsKey)
			{
				writer.WriteProperty<bool>(options, "IsKey", this.body.IsKey);
			}
			if (!this.body.IsNullable)
			{
				writer.WriteProperty<bool>(options, "IsNullable", this.body.IsNullable);
			}
			if (this.body.Alignment != Alignment.Default)
			{
				writer.WriteProperty<Alignment>(options, "Alignment", this.body.Alignment);
			}
			if (this.body.TableDetailPosition != -1)
			{
				writer.WriteProperty<int>(options, "TableDetailPosition", this.body.TableDetailPosition);
			}
			if (this.body.IsDefaultLabel)
			{
				writer.WriteProperty<bool>(options, "IsDefaultLabel", this.body.IsDefaultLabel);
			}
			if (this.body.IsDefaultImage)
			{
				writer.WriteProperty<bool>(options, "IsDefaultImage", this.body.IsDefaultImage);
			}
			if (this.body.SummarizeBy != AggregateFunction.Default)
			{
				writer.WriteProperty<AggregateFunction>(options, "SummarizeBy", this.body.SummarizeBy);
			}
			if (this.body.Type != ColumnType.Data)
			{
				writer.WriteProperty<ColumnType>(options, "Type", this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.body.SourceColumn))
			{
				writer.WriteProperty<string>(options, "SourceColumn", this.body.SourceColumn);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.FormatString))
			{
				writer.WriteProperty<string>(options, "FormatString", this.body.FormatString);
			}
			if (!this.body.IsAvailableInMDX)
			{
				writer.WriteProperty<bool>(options, "IsAvailableInMDX", this.body.IsAvailableInMDX);
			}
			if (this.body.KeepUniqueRows)
			{
				writer.WriteProperty<bool>(options, "KeepUniqueRows", this.body.KeepUniqueRows);
			}
			if (this.body.DisplayOrdinal != 0)
			{
				writer.WriteProperty<int>(options, "DisplayOrdinal", this.body.DisplayOrdinal);
			}
			if (!string.IsNullOrEmpty(this.body.SourceProviderType))
			{
				writer.WriteProperty<string>(options, "SourceProviderType", this.body.SourceProviderType);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				writer.WriteProperty<string>(options, "DisplayFolder", this.body.DisplayFolder);
			}
			if (this.body.EncodingHint != EncodingHintType.Default)
			{
				if (!CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsEncodingHintTypeValueCompatible(this.body.EncodingHint, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member EncodingHint is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<EncodingHintType>(options, "EncodingHint", this.body.EncodingHint);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
			if (this.body.EvaluationBehavior != EvaluationBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.Column_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsEvaluationBehaviorValueCompatible(this.body.EvaluationBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member EvaluationBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<EvaluationBehavior>(options, "EvaluationBehavior", this.body.EvaluationBehavior);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00013330 File Offset: 0x00011530
		void IMetadataObjectWithOverrides.WriteAllOverridenBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ReplacementPropertiesCollection newProperties)
		{
			string text;
			if (newProperties.IsPropertyOverriden<string>("SourceColumn", out text) && !PropertyHelper.AreValuesIdentical(this.body.SourceColumn, text))
			{
				writer.WriteProperty<string>(options, "SourceColumn", text);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00013370 File Offset: 0x00011570
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("ColumnOriginID", out objectId2))
			{
				this.body.ColumnOriginID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (reader.TryReadProperty<ObjectId>("SortByColumnID", out objectId3))
			{
				this.body.SortByColumnID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (reader.TryReadProperty<ObjectId>("AttributeHierarchyID", out objectId4))
			{
				this.body.AttributeHierarchyID.ObjectID = objectId4;
			}
			ObjectId objectId5;
			if (CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("RelatedColumnDetailsID", out objectId5))
			{
				this.body.RelatedColumnDetailsID.ObjectID = objectId5;
			}
			ObjectId objectId6;
			if (CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("AlternateOfID", out objectId6))
			{
				this.body.AlternateOfID.ObjectID = objectId6;
			}
			string text;
			if (reader.TryReadProperty<string>("ExplicitName", out text))
			{
				this.body.ExplicitName = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("InferredName", out text2))
			{
				this.body.InferredName = text2;
			}
			DataType dataType;
			if (reader.TryReadProperty<DataType>("ExplicitDataType", out dataType))
			{
				this.body.ExplicitDataType = dataType;
			}
			DataType dataType2;
			if (reader.TryReadProperty<DataType>("InferredDataType", out dataType2))
			{
				this.body.InferredDataType = dataType2;
			}
			string text3;
			if (reader.TryReadProperty<string>("DataCategory", out text3))
			{
				this.body.DataCategory = text3;
			}
			string text4;
			if (reader.TryReadProperty<string>("Description", out text4))
			{
				this.body.Description = text4;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsHidden", out flag))
			{
				this.body.IsHidden = flag;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			bool flag2;
			if (reader.TryReadProperty<bool>("IsUnique", out flag2))
			{
				this.body.IsUnique = flag2;
			}
			bool flag3;
			if (reader.TryReadProperty<bool>("IsKey", out flag3))
			{
				this.body.IsKey = flag3;
			}
			bool flag4;
			if (reader.TryReadProperty<bool>("IsNullable", out flag4))
			{
				this.body.IsNullable = flag4;
			}
			Alignment alignment;
			if (reader.TryReadProperty<Alignment>("Alignment", out alignment))
			{
				this.body.Alignment = alignment;
			}
			int num;
			if (reader.TryReadProperty<int>("TableDetailPosition", out num))
			{
				this.body.TableDetailPosition = num;
			}
			bool flag5;
			if (reader.TryReadProperty<bool>("IsDefaultLabel", out flag5))
			{
				this.body.IsDefaultLabel = flag5;
			}
			bool flag6;
			if (reader.TryReadProperty<bool>("IsDefaultImage", out flag6))
			{
				this.body.IsDefaultImage = flag6;
			}
			AggregateFunction aggregateFunction;
			if (reader.TryReadProperty<AggregateFunction>("SummarizeBy", out aggregateFunction))
			{
				this.body.SummarizeBy = aggregateFunction;
			}
			ColumnType columnType;
			if (reader.TryReadProperty<ColumnType>("Type", out columnType))
			{
				this.body.Type = columnType;
			}
			string text5;
			if (reader.TryReadProperty<string>("SourceColumn", out text5))
			{
				this.body.SourceColumn = text5;
			}
			string text6;
			if (reader.TryReadProperty<string>("Expression", out text6))
			{
				this.body.Expression = text6;
			}
			string text7;
			if (reader.TryReadProperty<string>("FormatString", out text7))
			{
				this.body.FormatString = text7;
			}
			bool flag7;
			if (reader.TryReadProperty<bool>("IsAvailableInMDX", out flag7))
			{
				this.body.IsAvailableInMDX = flag7;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			DateTime dateTime2;
			if (reader.TryReadProperty<DateTime>("StructureModifiedTime", out dateTime2))
			{
				this.body.StructureModifiedTime = dateTime2;
			}
			DateTime dateTime3;
			if (reader.TryReadProperty<DateTime>("RefreshedTime", out dateTime3))
			{
				this.body.RefreshedTime = dateTime3;
			}
			bool flag8;
			if (reader.TryReadProperty<bool>("KeepUniqueRows", out flag8))
			{
				this.body.KeepUniqueRows = flag8;
			}
			int num2;
			if (reader.TryReadProperty<int>("DisplayOrdinal", out num2))
			{
				this.body.DisplayOrdinal = num2;
			}
			string text8;
			if (reader.TryReadProperty<string>("ErrorMessage", out text8))
			{
				this.body.ErrorMessage = text8;
			}
			string text9;
			if (reader.TryReadProperty<string>("SourceProviderType", out text9))
			{
				this.body.SourceProviderType = text9;
			}
			string text10;
			if (reader.TryReadProperty<string>("DisplayFolder", out text10))
			{
				this.body.DisplayFolder = text10;
			}
			EncodingHintType encodingHintType;
			if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<EncodingHintType>("EncodingHint", out encodingHintType))
			{
				this.body.EncodingHint = encodingHintType;
			}
			string text11;
			if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("LineageTag", out text11))
			{
				this.body.LineageTag = text11;
			}
			string text12;
			if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceLineageTag", out text12))
			{
				this.body.SourceLineageTag = text12;
			}
			EvaluationBehavior evaluationBehavior;
			if (CompatibilityRestrictions.Column_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<EvaluationBehavior>("EvaluationBehavior", out evaluationBehavior))
			{
				this.body.EvaluationBehavior = evaluationBehavior;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00013838 File Offset: 0x00011A38
		private protected sealed override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			this.body.SortByColumnID.Validate(null, true);
			if (this.body.SortByColumnID.Object != null && writer.ShouldIncludeProperty("SortByColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("SortByColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.SortByColumnID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.ExplicitName) && writer.ShouldIncludeProperty("ExplicitName", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("ExplicitName", MetadataPropertyNature.NameProperty, this.body.ExplicitName);
			}
			if (!string.IsNullOrEmpty(this.body.InferredName) && writer.ShouldIncludeProperty("InferredName", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("InferredName", MetadataPropertyNature.RegularProperty, this.body.InferredName);
			}
			if (this.body.ExplicitDataType != DataType.Automatic && writer.ShouldIncludeProperty("ExplicitDataType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataType>("ExplicitDataType", MetadataPropertyNature.RegularProperty, this.body.ExplicitDataType);
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory) && writer.ShouldIncludeProperty("DataCategory", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("DataCategory", MetadataPropertyNature.RegularProperty, this.body.DataCategory);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("IsHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (this.body.IsUnique && writer.ShouldIncludeProperty("IsUnique", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsUnique", MetadataPropertyNature.RegularProperty, this.body.IsUnique);
			}
			if (this.body.IsKey && writer.ShouldIncludeProperty("IsKey", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsKey", MetadataPropertyNature.RegularProperty, this.body.IsKey);
			}
			if (!this.body.IsNullable && writer.ShouldIncludeProperty("IsNullable", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsNullable", MetadataPropertyNature.RegularProperty, this.body.IsNullable);
			}
			if (this.body.Alignment != Alignment.Default && writer.ShouldIncludeProperty("Alignment", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<Alignment>("Alignment", MetadataPropertyNature.RegularProperty, this.body.Alignment);
			}
			if (this.body.TableDetailPosition != -1 && writer.ShouldIncludeProperty("TableDetailPosition", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("TableDetailPosition", MetadataPropertyNature.RegularProperty, this.body.TableDetailPosition);
			}
			if (this.body.IsDefaultLabel && writer.ShouldIncludeProperty("IsDefaultLabel", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsDefaultLabel", MetadataPropertyNature.RegularProperty, this.body.IsDefaultLabel);
			}
			if (this.body.IsDefaultImage && writer.ShouldIncludeProperty("IsDefaultImage", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsDefaultImage", MetadataPropertyNature.RegularProperty, this.body.IsDefaultImage);
			}
			if (this.body.SummarizeBy != AggregateFunction.Default && writer.ShouldIncludeProperty("SummarizeBy", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<AggregateFunction>("SummarizeBy", MetadataPropertyNature.RegularProperty, this.body.SummarizeBy);
			}
			if (this.body.Type != ColumnType.Data && writer.ShouldIncludeProperty("Type", MetadataPropertyNature.TypeProperty))
			{
				writer.WriteEnumProperty<ColumnType>("Type", MetadataPropertyNature.TypeProperty, this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.body.SourceColumn) && writer.ShouldIncludeProperty("SourceColumn", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("SourceColumn", MetadataPropertyNature.RegularProperty, this.body.SourceColumn);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.FormatString) && writer.ShouldIncludeProperty("FormatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("FormatString", MetadataPropertyNature.RegularProperty, this.body.FormatString);
			}
			if (!this.body.IsAvailableInMDX && writer.ShouldIncludeProperty("IsAvailableInMDX", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsAvailableInMDX", MetadataPropertyNature.RegularProperty, this.body.IsAvailableInMDX);
			}
			if (this.body.KeepUniqueRows && writer.ShouldIncludeProperty("KeepUniqueRows", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("KeepUniqueRows", MetadataPropertyNature.RegularProperty, this.body.KeepUniqueRows);
			}
			if (this.body.DisplayOrdinal != 0 && writer.ShouldIncludeProperty("DisplayOrdinal", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("DisplayOrdinal", MetadataPropertyNature.RegularProperty, this.body.DisplayOrdinal);
			}
			if (!string.IsNullOrEmpty(this.body.SourceProviderType) && writer.ShouldIncludeProperty("SourceProviderType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("SourceProviderType", MetadataPropertyNature.RegularProperty, this.body.SourceProviderType);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
			if (this.body.EncodingHint != EncodingHintType.Default)
			{
				if (!CompatibilityRestrictions.Column_EncodingHint.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsEncodingHintTypeValueCompatible(this.body.EncodingHint, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member EncodingHint is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("EncodingHint", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<EncodingHintType>("EncodingHint", MetadataPropertyNature.RegularProperty, this.body.EncodingHint);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Column_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("LineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("LineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (this.body.EvaluationBehavior != EvaluationBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.Column_EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsEvaluationBehaviorValueCompatible(this.body.EvaluationBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member EvaluationBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("EvaluationBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<EvaluationBehavior>("EvaluationBehavior", MetadataPropertyNature.RegularProperty, this.body.EvaluationBehavior);
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00013FD0 File Offset: 0x000121D0
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.State);
				}
			}
			if (this.body.IsUnique && writer.ShouldIncludeProperty("isUnique", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isUnique", MetadataPropertyNature.RegularProperty, this.body.IsUnique);
			}
			if (this.body.IsKey && writer.ShouldIncludeProperty("isKey", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isKey", MetadataPropertyNature.RegularProperty, this.body.IsKey);
			}
			if (!this.body.IsNullable && writer.ShouldIncludeProperty("isNullable", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isNullable", MetadataPropertyNature.RegularProperty, this.body.IsNullable);
			}
			if (!string.IsNullOrEmpty(this.body.FormatString) && writer.ShouldIncludeProperty("formatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("formatString", MetadataPropertyNature.RegularProperty, this.body.FormatString);
			}
			if (!this.body.IsAvailableInMDX && writer.ShouldIncludeProperty("isAvailableInMdx", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isAvailableInMdx", MetadataPropertyNature.RegularProperty, this.body.IsAvailableInMDX);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.RefreshedTime);
			}
			if (this.body.KeepUniqueRows && writer.ShouldIncludeProperty("keepUniqueRows", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("keepUniqueRows", MetadataPropertyNature.RegularProperty, this.body.KeepUniqueRows);
			}
			if (this.body.DisplayOrdinal != 0 && writer.ShouldIncludeProperty("displayOrdinal", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("displayOrdinal", MetadataPropertyNature.RegularProperty, this.body.DisplayOrdinal);
			}
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (!string.IsNullOrEmpty(this.body.SourceProviderType) && writer.ShouldIncludeProperty("sourceProviderType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("sourceProviderType", MetadataPropertyNature.RegularProperty, this.body.SourceProviderType);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
			if (this.body.EncodingHint != EncodingHintType.Default)
			{
				if (!CompatibilityRestrictions.Column_EncodingHint.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsEncodingHintTypeValueCompatible(this.body.EncodingHint, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member EncodingHint is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("encodingHint", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<EncodingHintType>("encodingHint", MetadataPropertyNature.RegularProperty, this.body.EncodingHint);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Column_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("lineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory) && writer.ShouldIncludeProperty("dataCategory", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("dataCategory", MetadataPropertyNature.RegularProperty, this.body.DataCategory);
			}
			if (this.body.Alignment != Alignment.Default && writer.ShouldIncludeProperty("alignment", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<Alignment>("alignment", MetadataPropertyNature.RegularProperty, this.body.Alignment);
			}
			if (this.body.TableDetailPosition != -1 && writer.ShouldIncludeProperty("tableDetailPosition", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("tableDetailPosition", MetadataPropertyNature.RegularProperty, this.body.TableDetailPosition);
			}
			if (this.body.IsDefaultLabel && writer.ShouldIncludeProperty("isDefaultLabel", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isDefaultLabel", MetadataPropertyNature.RegularProperty, this.body.IsDefaultLabel);
			}
			if (this.body.IsDefaultImage && writer.ShouldIncludeProperty("isDefaultImage", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isDefaultImage", MetadataPropertyNature.RegularProperty, this.body.IsDefaultImage);
			}
			if (this.body.SummarizeBy != AggregateFunction.Default && writer.ShouldIncludeProperty("summarizeBy", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<AggregateFunction>("summarizeBy", MetadataPropertyNature.RegularProperty, this.body.SummarizeBy);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001467C File Offset: 0x0001287C
		private protected virtual void WriteCrossLinksToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (this.body.SortByColumnID.Object != null && writer.ShouldIncludeProperty("sortByColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.SortByColumnID.WriteToMetadataStream(ObjectType.Column, context.SerializationMode != MetadataSerializationMode.Json, "sortByColumn", false, writer);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000146D0 File Offset: 0x000128D0
		private protected sealed override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (this.body.Type != ColumnType.Data && writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
			{
				writer.WriteEnumProperty<ColumnType>("type", MetadataPropertyNature.TypeProperty, this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.Name);
			}
			DataType dataType;
			bool flag;
			if (this.ShouldSerializeDataType(out dataType, out flag) && writer.ShouldIncludeProperty("dataType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataType>("dataType", MetadataPropertyNature.RegularProperty, dataType);
			}
			this.WriteRegularPropertiesToMetadataStream(context, writer);
			this.WriteCrossLinksToMetadataStream(context, writer);
			if (this.body.AttributeHierarchyID.Object != null && writer.ShouldIncludeProperty("attributeHierarchy", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteSingleChild(context, "attributeHierarchy", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.AttributeHierarchyID.Object);
			}
			if (this.body.RelatedColumnDetailsID.Object != null)
			{
				if (!CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RelatedColumnDetailsID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("relatedColumnDetails", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "relatedColumnDetails", MetadataPropertyNature.ChildProperty, this.body.RelatedColumnDetailsID.Object);
				}
			}
			if (this.body.AlternateOfID.Object != null)
			{
				if (!CompatibilityRestrictions.Column_AlternateOf.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AlternateOfID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("alternateOf", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "alternateOf", MetadataPropertyNature.ChildProperty, this.body.AlternateOfID.Object);
				}
			}
			if (this.Variations.Count > 0)
			{
				if (!CompatibilityRestrictions.Variation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child Variation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("variations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "variations", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Variations);
				}
			}
			if (this.ExtendedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, this.ExtendedProperties);
				}
			}
			if (this.ChangedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, this.ChangedProperties);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00014A60 File Offset: 0x00012C60
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				switch (propertyName.Length)
				{
				case 4:
				{
					char c = propertyName[0];
					if (c != 'T')
					{
						if (c != 'n')
						{
							if (c != 't')
							{
								break;
							}
							if (!(propertyName == "type"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "name"))
							{
								break;
							}
							context.ActivityInfo["SerializationActivity::ColumnName"] = reader.ReadStringProperty();
							return true;
						}
					}
					else if (!(propertyName == "Type"))
					{
						break;
					}
					this.body.Type = reader.ReadEnumProperty<ColumnType>();
					return true;
				}
				case 5:
				{
					char c = propertyName[0];
					if (c <= 'S')
					{
						if (c != 'I')
						{
							if (c != 'S')
							{
								break;
							}
							if (!(propertyName == "State"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "IsKey"))
							{
								break;
							}
							goto IL_0B26;
						}
					}
					else if (c != 'i')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "state"))
						{
							break;
						}
					}
					else
					{
						if (!(propertyName == "isKey"))
						{
							break;
						}
						goto IL_0B26;
					}
					ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
					if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.State = objectState;
					return true;
					IL_0B26:
					this.body.IsKey = reader.ReadBooleanProperty();
					return true;
				}
				case 7:
					if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 8:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'd')
						{
							if (c != 'i')
							{
								break;
							}
							if (!(propertyName == "isHidden"))
							{
								if (!(propertyName == "isUnique"))
								{
									break;
								}
								goto IL_0B13;
							}
						}
						else
						{
							if (!(propertyName == "dataType"))
							{
								break;
							}
							context.ActivityInfo["SerializationActivity::ColumnDataType"] = reader.ReadEnumProperty<DataType>();
							return true;
						}
					}
					else if (!(propertyName == "IsHidden"))
					{
						if (!(propertyName == "IsUnique"))
						{
							break;
						}
						goto IL_0B13;
					}
					this.body.IsHidden = reader.ReadBooleanProperty();
					return true;
					IL_0B13:
					this.body.IsUnique = reader.ReadBooleanProperty();
					return true;
				}
				case 9:
				{
					char c = propertyName[0];
					if (c != 'A')
					{
						if (c != 'a')
						{
							break;
						}
						if (!(propertyName == "alignment"))
						{
							break;
						}
					}
					else if (!(propertyName == "Alignment"))
					{
						break;
					}
					this.body.Alignment = reader.ReadEnumProperty<Alignment>();
					return true;
				}
				case 10:
				{
					char c = propertyName[0];
					if (c <= 'L')
					{
						if (c != 'E')
						{
							if (c != 'I')
							{
								if (c != 'L')
								{
									break;
								}
								if (!(propertyName == "LineageTag"))
								{
									break;
								}
								goto IL_0CEA;
							}
							else if (!(propertyName == "IsNullable"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "Expression"))
							{
								break;
							}
							this.body.Expression = reader.ReadStringProperty();
							return true;
						}
					}
					else if (c != 'i')
					{
						if (c != 'l')
						{
							if (c != 'v')
							{
								break;
							}
							if (!(propertyName == "variations"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Variation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Variation variation in reader.ReadChildCollectionProperty<Variation>(context))
								{
									try
									{
										this.Variations.Add(variation);
									}
									catch (Exception ex)
									{
										throw reader.CreateInvalidChildException(context, variation, TomSR.Exception_FailedAddDeserializedNamedObject("Variation", (variation != null) ? variation.Name : null, ex.Message), ex);
									}
								}
							}
							return true;
						}
						else
						{
							if (!(propertyName == "lineageTag"))
							{
								break;
							}
							goto IL_0CEA;
						}
					}
					else if (!(propertyName == "isNullable"))
					{
						break;
					}
					this.body.IsNullable = reader.ReadBooleanProperty();
					return true;
					IL_0CEA:
					if (!CompatibilityRestrictions.Column_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.LineageTag = reader.ReadStringProperty();
					return true;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c <= 'S')
					{
						if (c != 'D')
						{
							if (c != 'S')
							{
								break;
							}
							if (!(propertyName == "SummarizeBy"))
							{
								break;
							}
							goto IL_0B98;
						}
						else if (!(propertyName == "Description"))
						{
							break;
						}
					}
					else if (c != 'a')
					{
						if (c != 'd')
						{
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "summarizeBy"))
							{
								break;
							}
							goto IL_0B98;
						}
						else if (!(propertyName == "description"))
						{
							break;
						}
					}
					else if (!(propertyName == "alternateOf"))
					{
						if (!(propertyName == "annotations"))
						{
							break;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
							{
								try
								{
									this.Annotations.Add(annotation);
								}
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					else
					{
						if (!CompatibilityRestrictions.Column_AlternateOf.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							AlternateOf alternateOf = reader.ReadSingleChildProperty<AlternateOf>(context);
							try
							{
								this.body.AlternateOfID.Object = alternateOf;
							}
							catch (Exception ex3)
							{
								throw reader.CreateInvalidChildException(context, alternateOf, TomSR.Exception_FailedAddDeserializedObject("AlternateOf", ex3.Message), ex3);
							}
						}
						return true;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
					IL_0B98:
					this.body.SummarizeBy = reader.ReadEnumProperty<AggregateFunction>();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c <= 'S')
					{
						switch (c)
						{
						case 'D':
							if (!(propertyName == "DataCategory"))
							{
								return false;
							}
							break;
						case 'E':
							if (propertyName == "ExplicitName")
							{
								this.body.ExplicitName = reader.ReadStringProperty();
								return true;
							}
							if (propertyName == "ErrorMessage")
							{
								goto IL_0C69;
							}
							if (!(propertyName == "EncodingHint"))
							{
								return false;
							}
							goto IL_0CA2;
						case 'F':
							if (!(propertyName == "FormatString"))
							{
								return false;
							}
							goto IL_0BE4;
						case 'G':
						case 'H':
							return false;
						case 'I':
							if (!(propertyName == "InferredName"))
							{
								return false;
							}
							this.body.InferredName = reader.ReadStringProperty();
							return true;
						default:
							if (c != 'M')
							{
								if (c != 'S')
								{
									return false;
								}
								if (!(propertyName == "SourceColumn"))
								{
									return false;
								}
								this.body.SourceColumn = reader.ReadStringProperty();
								return true;
							}
							else
							{
								if (!(propertyName == "ModifiedTime"))
								{
									return false;
								}
								goto IL_0C0A;
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							if (!(propertyName == "dataCategory"))
							{
								return false;
							}
							break;
						case 'e':
							if (propertyName == "errorMessage")
							{
								goto IL_0C69;
							}
							if (!(propertyName == "encodingHint"))
							{
								return false;
							}
							goto IL_0CA2;
						case 'f':
							if (!(propertyName == "formatString"))
							{
								return false;
							}
							goto IL_0BE4;
						default:
							if (c != 'm')
							{
								if (c != 's')
								{
									return false;
								}
								if (!(propertyName == "sortByColumn"))
								{
									return false;
								}
								this.body.SortByColumnID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.Column, p));
								return true;
							}
							else
							{
								if (!(propertyName == "modifiedTime"))
								{
									return false;
								}
								goto IL_0C0A;
							}
							break;
						}
					}
					this.body.DataCategory = reader.ReadStringProperty();
					return true;
					IL_0BE4:
					this.body.FormatString = reader.ReadStringProperty();
					return true;
					IL_0C0A:
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
					IL_0C69:
					this.body.ErrorMessage = reader.ReadStringProperty();
					return true;
					IL_0CA2:
					if (!CompatibilityRestrictions.Column_EncodingHint.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.EncodingHintType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.EncodingHint = reader.ReadEnumProperty<EncodingHintType>();
					return true;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c > 'D')
					{
						if (c != 'R')
						{
							if (c != 'd')
							{
								if (c != 'r')
								{
									break;
								}
								if (!(propertyName == "refreshedTime"))
								{
									break;
								}
							}
							else
							{
								if (!(propertyName == "displayFolder"))
								{
									break;
								}
								goto IL_0C8F;
							}
						}
						else if (!(propertyName == "RefreshedTime"))
						{
							break;
						}
						this.body.RefreshedTime = reader.ReadDateTimeProperty();
						return true;
					}
					if (c != 'A')
					{
						if (c != 'D')
						{
							break;
						}
						if (!(propertyName == "DisplayFolder"))
						{
							break;
						}
					}
					else
					{
						if (!(propertyName == "AlternateOfID"))
						{
							break;
						}
						if (!CompatibilityRestrictions.Column_AlternateOf.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.AlternateOfID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					IL_0C8F:
					this.body.DisplayFolder = reader.ReadStringProperty();
					return true;
				}
				case 14:
				{
					char c = propertyName[0];
					if (c <= 'K')
					{
						if (c <= 'D')
						{
							if (c != 'C')
							{
								if (c != 'D')
								{
									break;
								}
								if (!(propertyName == "DisplayOrdinal"))
								{
									break;
								}
								goto IL_0C56;
							}
							else
							{
								if (!(propertyName == "ColumnOriginID"))
								{
									break;
								}
								this.body.ColumnOriginID.ObjectID = reader.ReadObjectIdProperty();
								return true;
							}
						}
						else if (c != 'I')
						{
							if (c != 'K')
							{
								break;
							}
							if (!(propertyName == "KeepUniqueRows"))
							{
								break;
							}
							goto IL_0C43;
						}
						else if (!(propertyName == "IsDefaultLabel"))
						{
							if (!(propertyName == "IsDefaultImage"))
							{
								break;
							}
							goto IL_0B85;
						}
					}
					else if (c <= 'd')
					{
						if (c != 'S')
						{
							if (c != 'd')
							{
								break;
							}
							if (!(propertyName == "displayOrdinal"))
							{
								break;
							}
							goto IL_0C56;
						}
						else
						{
							if (!(propertyName == "SortByColumnID"))
							{
								break;
							}
							this.body.SortByColumnID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
					}
					else if (c != 'i')
					{
						if (c != 'k')
						{
							break;
						}
						if (!(propertyName == "keepUniqueRows"))
						{
							break;
						}
						goto IL_0C43;
					}
					else if (!(propertyName == "isDefaultLabel"))
					{
						if (!(propertyName == "isDefaultImage"))
						{
							break;
						}
						goto IL_0B85;
					}
					this.body.IsDefaultLabel = reader.ReadBooleanProperty();
					return true;
					IL_0B85:
					this.body.IsDefaultImage = reader.ReadBooleanProperty();
					return true;
					IL_0C43:
					this.body.KeepUniqueRows = reader.ReadBooleanProperty();
					return true;
					IL_0C56:
					this.body.DisplayOrdinal = reader.ReadInt32Property();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c <= 'I')
					{
						if (c != 'E')
						{
							if (c != 'I')
							{
								break;
							}
							if (propertyName == "InferredDataType")
							{
								this.body.InferredDataType = reader.ReadEnumProperty<DataType>();
								return true;
							}
							if (!(propertyName == "IsAvailableInMDX"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "ExplicitDataType"))
							{
								break;
							}
							this.body.ExplicitDataType = reader.ReadEnumProperty<DataType>();
							return true;
						}
					}
					else
					{
						if (c != 'S')
						{
							if (c != 'i')
							{
								if (c != 's')
								{
									break;
								}
								if (!(propertyName == "sourceLineageTag"))
								{
									break;
								}
							}
							else
							{
								if (!(propertyName == "isAvailableInMdx"))
								{
									break;
								}
								goto IL_0BF7;
							}
						}
						else if (!(propertyName == "SourceLineageTag"))
						{
							break;
						}
						if (!CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.SourceLineageTag = reader.ReadStringProperty();
						return true;
					}
					IL_0BF7:
					this.body.IsAvailableInMDX = reader.ReadBooleanProperty();
					return true;
				}
				case 17:
					if (propertyName == "changedProperties")
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ChangedProperty changedProperty in reader.ReadChildCollectionProperty<ChangedProperty>(context))
							{
								try
								{
									this.ChangedProperties.Add(changedProperty);
								}
								catch (Exception ex4)
								{
									throw reader.CreateInvalidChildException(context, changedProperty, TomSR.Exception_FailedAddDeserializedObject("ChangedProperty", ex4.Message), ex4);
								}
							}
						}
						return true;
					}
					break;
				case 18:
				{
					char c = propertyName[0];
					if (c <= 'S')
					{
						if (c != 'E')
						{
							if (c != 'S')
							{
								break;
							}
							if (!(propertyName == "SourceProviderType"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "EvaluationBehavior"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Column_EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.EvaluationBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.EvaluationBehavior = reader.ReadEnumProperty<EvaluationBehavior>();
							return true;
						}
					}
					else if (c != 'a')
					{
						if (c != 'e')
						{
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "sourceProviderType"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "extendedProperties"))
							{
								break;
							}
							if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (ExtendedProperty extendedProperty in reader.ReadChildCollectionProperty<ExtendedProperty>(context))
								{
									try
									{
										this.ExtendedProperties.Add(extendedProperty);
									}
									catch (Exception ex5)
									{
										throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex5.Message), ex5);
									}
								}
							}
							return true;
						}
					}
					else
					{
						if (!(propertyName == "attributeHierarchy"))
						{
							break;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							AttributeHierarchy attributeHierarchy = reader.ReadSingleChildProperty<AttributeHierarchy>(context);
							try
							{
								this.body.AttributeHierarchyID.Object = attributeHierarchy;
							}
							catch (Exception ex6)
							{
								throw reader.CreateInvalidChildException(context, attributeHierarchy, TomSR.Exception_FailedAddDeserializedObject("AttributeHierarchy", ex6.Message), ex6);
							}
						}
						return true;
					}
					this.body.SourceProviderType = reader.ReadStringProperty();
					return true;
				}
				case 19:
				{
					char c = propertyName[0];
					if (c != 'T')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "tableDetailPosition"))
						{
							break;
						}
					}
					else if (!(propertyName == "TableDetailPosition"))
					{
						break;
					}
					this.body.TableDetailPosition = reader.ReadInt32Property();
					return true;
				}
				case 20:
				{
					char c = propertyName[0];
					if (c != 'A')
					{
						if (c == 'r')
						{
							if (propertyName == "relatedColumnDetails")
							{
								if (!CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									RelatedColumnDetails relatedColumnDetails = reader.ReadSingleChildProperty<RelatedColumnDetails>(context);
									try
									{
										this.body.RelatedColumnDetailsID.Object = relatedColumnDetails;
									}
									catch (Exception ex7)
									{
										throw reader.CreateInvalidChildException(context, relatedColumnDetails, TomSR.Exception_FailedAddDeserializedObject("RelatedColumnDetails", ex7.Message), ex7);
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "AttributeHierarchyID")
					{
						this.body.AttributeHierarchyID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 21:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "structureModifiedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "StructureModifiedTime"))
					{
						break;
					}
					this.body.StructureModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 22:
					if (propertyName == "RelatedColumnDetailsID")
					{
						if (!CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.RelatedColumnDetailsID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00015CD4 File Offset: 0x00013ED4
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00015CDD File Offset: 0x00013EDD
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00015D00 File Offset: 0x00013F00
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.DataCategory))
			{
				result["dataCategory", TomPropCategory.Regular, 108, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DataCategory, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsHidden)
			{
				result["isHidden", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsHidden);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 11, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsUnique)
			{
				result["isUnique", TomPropCategory.Regular, 12, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsUnique);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsKey)
			{
				result["isKey", TomPropCategory.Regular, 13, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsKey);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !this.body.IsNullable)
			{
				result["isNullable", TomPropCategory.Regular, 14, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsNullable);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Alignment != Alignment.Default)
			{
				result["alignment", TomPropCategory.Regular, 115, false] = JsonPropertyHelper.ConvertEnumToJsonValue<Alignment>(this.body.Alignment);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.TableDetailPosition != -1)
			{
				result["tableDetailPosition", TomPropCategory.Regular, 116, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.TableDetailPosition);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsDefaultLabel)
			{
				result["isDefaultLabel", TomPropCategory.Regular, 117, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsDefaultLabel);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsDefaultImage)
			{
				result["isDefaultImage", TomPropCategory.Regular, 118, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsDefaultImage);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.SummarizeBy != AggregateFunction.Default)
			{
				result["summarizeBy", TomPropCategory.Regular, 119, false] = JsonPropertyHelper.ConvertEnumToJsonValue<AggregateFunction>(this.body.SummarizeBy);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Type != ColumnType.Data)
			{
				result["type", TomPropCategory.Type, 21, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ColumnType>(this.body.Type);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.FormatString))
			{
				result["formatString", TomPropCategory.Regular, 25, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.FormatString, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !this.body.IsAvailableInMDX)
			{
				result["isAvailableInMdx", TomPropCategory.Regular, 26, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsAvailableInMDX);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 29, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 30, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["refreshedTime", TomPropCategory.Regular, 31, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.RefreshedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.KeepUniqueRows)
			{
				result["keepUniqueRows", TomPropCategory.Regular, 33, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.KeepUniqueRows);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DisplayOrdinal != 0)
			{
				result["displayOrdinal", TomPropCategory.Regular, 34, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.DisplayOrdinal);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 35, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceProviderType))
			{
				result["sourceProviderType", TomPropCategory.Regular, 36, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceProviderType, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				result["displayFolder", TomPropCategory.Regular, 37, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DisplayFolder, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.EncodingHint != EncodingHintType.Default)
			{
				if (!CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsEncodingHintTypeValueCompatible(this.body.EncodingHint, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member EncodingHint is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["encodingHint", TomPropCategory.Regular, 38, false] = JsonPropertyHelper.ConvertEnumToJsonValue<EncodingHintType>(this.body.EncodingHint);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["lineageTag", TomPropCategory.Regular, 41, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceLineageTag", TomPropCategory.Regular, 42, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				result["name", TomPropCategory.Name, 0, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.Name, SplitMultilineOptions.None);
			}
			DataType dataType;
			bool flag;
			if (!options.IncludeTranslatablePropertiesOnly && this.ShouldSerializeDataType(out dataType, out flag))
			{
				result["dataType", TomPropCategory.Regular, 0, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DataType>(dataType);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.SortByColumnID.Object != null)
			{
				this.body.SortByColumnID.SerializeToJsonObject(false, "sortByColumn", ObjectType.Column, result, 27, false);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (this.body.AttributeHierarchyID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.AttributeHierarchyID.Object)))
				{
					result["attributeHierarchy", TomPropCategory.ChildLink, 28, true] = this.body.AttributeHierarchyID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.body.RelatedColumnDetailsID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.RelatedColumnDetailsID.Object)))
				{
					if (!CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member RelatedColumnDetailsID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["relatedColumnDetails", TomPropCategory.ChildLink, 39, false] = this.body.RelatedColumnDetailsID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.body.AlternateOfID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.AlternateOfID.Object)))
				{
					if (!CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member AlternateOfID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["alternateOf", TomPropCategory.ChildLink, 40, false] = this.body.AlternateOfID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				IEnumerable<Variation> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Variation> variations = this.Variations;
					enumerable = variations;
				}
				else
				{
					enumerable = this.Variations.Where((Variation o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Variation> enumerable2 = enumerable;
				if (enumerable2.Any<Variation>())
				{
					if (!CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child Variation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable2.Select((Variation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["variations", TomPropCategory.ChildCollection, 36, false] = array2;
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable3 = extendedProperties;
					}
					else
					{
						enumerable3 = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable4 = enumerable3;
					if (enumerable4.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ChangedProperty> enumerable5;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ChangedProperty> changedProperties = this.ChangedProperties;
						enumerable5 = changedProperties;
					}
					else
					{
						enumerable5 = this.ChangedProperties.Where((ChangedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ChangedProperty> enumerable6 = enumerable5;
					if (enumerable6.Any<ChangedProperty>())
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable6.Select((ChangedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array4 = array;
						result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array4;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array5;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00016A8C File Offset: 0x00014C8C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 4:
				{
					char c = name[0];
					if (c != 'n')
					{
						if (c == 't')
						{
							if (name == "type")
							{
								this.body.Type = JsonPropertyHelper.ConvertJsonValueToEnum<ColumnType>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "name")
					{
						this.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 5:
				{
					char c = name[0];
					if (c != 'i')
					{
						if (c == 's')
						{
							if (name == "state")
							{
								ObjectState objectState = JsonPropertyHelper.ConvertJsonValueToEnum<ObjectState>(jsonProp.Value);
								if (jsonProp.Value.Type != 10 && !PropertyHelper.IsObjectStateValueCompatible(objectState, mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.State = objectState;
								return true;
							}
						}
					}
					else if (name == "isKey")
					{
						this.body.IsKey = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 8:
				{
					char c = name[2];
					if (c != 'H')
					{
						if (c != 'U')
						{
							if (c == 't')
							{
								if (name == "dataType")
								{
									this.DataType = JsonPropertyHelper.ConvertJsonValueToEnum<DataType>(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "isUnique")
						{
							this.body.IsUnique = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
							return true;
						}
					}
					else if (name == "isHidden")
					{
						this.body.IsHidden = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 9:
					if (name == "alignment")
					{
						this.body.Alignment = JsonPropertyHelper.ConvertJsonValueToEnum<Alignment>(jsonProp.Value);
						return true;
					}
					break;
				case 10:
				{
					char c = name[0];
					if (c != 'i')
					{
						if (c != 'l')
						{
							if (c == 'v')
							{
								if (name == "variations")
								{
									if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
									{
										return false;
									}
									JsonPropertyHelper.ReadObjectCollection(this.Variations, jsonProp.Value, options, mode, dbCompatibilityLevel);
									return true;
								}
							}
						}
						else if (name == "lineageTag")
						{
							if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.LineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
					}
					else if (name == "isNullable")
					{
						this.body.IsNullable = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 11:
				{
					char c = name[1];
					if (c <= 'l')
					{
						if (c != 'e')
						{
							if (c == 'l')
							{
								if (name == "alternateOf")
								{
									if (jsonProp.Value.Type != 10)
									{
										if (!CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
										{
											return false;
										}
										AlternateOf alternateOf = new AlternateOf();
										alternateOf.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
										this.body.AlternateOfID.Object = alternateOf;
									}
									return true;
								}
							}
						}
						else if (name == "description")
						{
							this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
					}
					else if (c != 'n')
					{
						if (c == 'u')
						{
							if (name == "summarizeBy")
							{
								this.body.SummarizeBy = JsonPropertyHelper.ConvertJsonValueToEnum<AggregateFunction>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "annotations")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 12:
				{
					char c = name[4];
					if (c <= 'a')
					{
						if (c != 'B')
						{
							if (c != 'C')
							{
								if (c == 'a')
								{
									if (name == "formatString")
									{
										this.body.FormatString = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
										return true;
									}
								}
							}
							else if (name == "dataCategory")
							{
								this.body.DataCategory = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
						else if (name == "sortByColumn")
						{
							this.body.SortByColumnID.Path = new ObjectPath(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
					}
					else if (c != 'd')
					{
						if (c != 'f')
						{
							if (c == 'r')
							{
								if (name == "errorMessage")
								{
									this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "modifiedTime")
						{
							this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
							return true;
						}
					}
					else if (name == "encodingHint")
					{
						EncodingHintType encodingHintType = JsonPropertyHelper.ConvertJsonValueToEnum<EncodingHintType>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsEncodingHintTypeValueCompatible(encodingHintType, mode, dbCompatibilityLevel)))
						{
							return false;
						}
						this.body.EncodingHint = encodingHintType;
						return true;
					}
					break;
				}
				case 13:
				{
					char c = name[0];
					if (c != 'd')
					{
						if (c == 'r')
						{
							if (name == "refreshedTime")
							{
								this.body.RefreshedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "displayFolder")
					{
						this.body.DisplayFolder = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 14:
				{
					char c = name[9];
					if (c <= 'L')
					{
						if (c != 'I')
						{
							if (c == 'L')
							{
								if (name == "isDefaultLabel")
								{
									this.body.IsDefaultLabel = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "isDefaultImage")
						{
							this.body.IsDefaultImage = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
							return true;
						}
					}
					else if (c != 'd')
					{
						if (c == 'e')
						{
							if (name == "keepUniqueRows")
							{
								this.body.KeepUniqueRows = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "displayOrdinal")
					{
						this.body.DisplayOrdinal = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 16:
				{
					char c = name[0];
					if (c != 'i')
					{
						if (c == 's')
						{
							if (name == "sourceLineageTag")
							{
								if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "isAvailableInMdx")
					{
						this.body.IsAvailableInMDX = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 17:
					if (name == "changedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ChangedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 18:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c != 'e')
						{
							if (c == 's')
							{
								if (name == "sourceProviderType")
								{
									this.body.SourceProviderType = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "extendedProperties")
						{
							if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
							return true;
						}
					}
					else if (name == "attributeHierarchy")
					{
						if (jsonProp.Value.Type != 10)
						{
							AttributeHierarchy attributeHierarchy = new AttributeHierarchy();
							attributeHierarchy.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.AttributeHierarchyID.Object = attributeHierarchy;
						}
						return true;
					}
					break;
				}
				case 19:
					if (name == "tableDetailPosition")
					{
						this.body.TableDetailPosition = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 20:
					if (name == "relatedColumnDetails")
					{
						if (jsonProp.Value.Type != 10)
						{
							if (!CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							RelatedColumnDetails relatedColumnDetails = new RelatedColumnDetails();
							relatedColumnDetails.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.RelatedColumnDetailsID.Object = relatedColumnDetails;
						}
						return true;
					}
					break;
				case 21:
					if (name == "structureModifiedTime")
					{
						this.body.StructureModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00017489 File Offset: 0x00015689
		internal override IEnumerable<MetadataObject> GetNameLinkedObjects(string objectName = null)
		{
			if (objectName == null)
			{
				objectName = this.Name;
			}
			if (this.Table == null || base.Model == null)
			{
				yield break;
			}
			foreach (Perspective perspective in base.Model.Perspectives)
			{
				PerspectiveTable perspectiveTable = perspective.PerspectiveTables.Find(this.Table.Name);
				if (perspectiveTable != null)
				{
					PerspectiveColumn perspectiveColumn = perspectiveTable.PerspectiveColumns.Find(objectName);
					if (perspectiveColumn != null)
					{
						yield return perspectiveColumn;
					}
				}
			}
			IEnumerator<Perspective> enumerator = null;
			foreach (ModelRole modelRole in base.Model.Roles)
			{
				TablePermission tablePermission = modelRole.TablePermissions.Find(this.Table.Name);
				if (tablePermission != null)
				{
					ColumnPermission columnPermission = tablePermission.ColumnPermissions.Find(objectName);
					if (columnPermission != null)
					{
						yield return columnPermission;
					}
				}
			}
			IEnumerator<ModelRole> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000174A0 File Offset: 0x000156A0
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000174B0 File Offset: 0x000156B0
		public override string Name
		{
			get
			{
				return Column.ComputeName(this.body);
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Name", typeof(string), this.Name, value);
					string name = this.Name;
					this.body.ExplicitName = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Name", typeof(string), name, value);
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00017511 File Offset: 0x00015711
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string ComputeName(Column.ObjectBody body)
		{
			if (string.IsNullOrEmpty(body.ExplicitName))
			{
				return body.InferredName;
			}
			return body.ExplicitName;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0001752D File Offset: 0x0001572D
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0001753C File Offset: 0x0001573C
		internal bool IsNameInferred
		{
			get
			{
				return Column.ComputeIsNameInferred(this.body);
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.IsNameInferred, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsNameInferred", typeof(string), this.IsNameInferred, value);
					bool isNameInferred = this.IsNameInferred;
					if (value)
					{
						this.body.InferredName = this.body.ExplicitName;
						this.body.ExplicitName = string.Empty;
					}
					else
					{
						this.body.ExplicitName = this.body.InferredName;
						this.body.InferredName = string.Empty;
					}
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsNameInferred", typeof(string), isNameInferred, value);
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000175F9 File Offset: 0x000157F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ComputeIsNameInferred(Column.ObjectBody body)
		{
			return string.IsNullOrEmpty(body.ExplicitName);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00017606 File Offset: 0x00015806
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00017614 File Offset: 0x00015814
		public DataType DataType
		{
			get
			{
				return Column.ComputeDataType(this.body);
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.DataType, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataType", typeof(DataType), this.DataType, value);
					DataType dataType = this.DataType;
					this.body.ExplicitDataType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataType", typeof(DataType), dataType, value);
				}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017689 File Offset: 0x00015889
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static DataType ComputeDataType(Column.ObjectBody body)
		{
			if (body.ExplicitDataType == (DataType)0 || body.ExplicitDataType == DataType.Automatic)
			{
				return body.InferredDataType;
			}
			return body.ExplicitDataType;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000176A9 File Offset: 0x000158A9
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x000176B8 File Offset: 0x000158B8
		public bool IsDataTypeInferred
		{
			get
			{
				return Column.ComputeIsDataTypeInferred(this.body);
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.IsDataTypeInferred, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsDataTypeInferred", typeof(bool), this.IsDataTypeInferred, value);
					bool isDataTypeInferred = this.IsDataTypeInferred;
					if (value)
					{
						this.body.InferredDataType = this.body.ExplicitDataType;
						this.body.ExplicitDataType = DataType.Automatic;
					}
					else
					{
						this.body.ExplicitDataType = this.body.InferredDataType;
						this.body.InferredDataType = DataType.Unknown;
					}
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsDataTypeInferred", typeof(bool), isDataTypeInferred, value);
				}
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001776E File Offset: 0x0001596E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ComputeIsDataTypeInferred(Column.ObjectBody body)
		{
			return body.ExplicitDataType == DataType.Automatic;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001777C File Offset: 0x0001597C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected bool ShouldSerializeDataType(out DataType dataType, out bool isDataTypeInferred)
		{
			dataType = Column.ComputeDataType(this.body);
			isDataTypeInferred = this.body.ExplicitDataType == DataType.Automatic && this.body.InferredDataType != DataType.Unknown;
			return dataType != DataType.Automatic && dataType != DataType.Unknown;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000177CC File Offset: 0x000159CC
		internal static Column CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			ColumnType columnType;
			if (reader.TryMoveToProperty("type"))
			{
				columnType = reader.ReadEnumProperty<ColumnType>();
			}
			else
			{
				columnType = ColumnType.Data;
			}
			reader.Reset();
			switch (columnType)
			{
			case ColumnType.Data:
				return new DataColumn();
			case ColumnType.Calculated:
				return new CalculatedColumn();
			case ColumnType.RowNumber:
				return new RowNumberColumn();
			case ColumnType.CalculatedTableColumn:
				return new CalculatedTableColumn();
			default:
				throw reader.CreateInvalidDataException(context, TomSR.Exception_UnrecognizedValueOfType("ColumnType", columnType.ToString()), null);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00017848 File Offset: 0x00015A48
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_Column_2Args(this.Name, this.Table.Name);
			}
			return TomSR.ObjectPath_Column_1Arg(this.Name);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00017874 File Offset: 0x00015A74
		private protected override void ReadMetadataProperties(SerializationActivityContext context, IMetadataReader reader)
		{
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.ActivityInfo.Remove("SerializationActivity::ColumnName");
				context.ActivityInfo.Remove("SerializationActivity::ColumnIsNameInferred");
				context.ActivityInfo.Remove("SerializationActivity::ColumnDataType");
				context.ActivityInfo.Remove("SerializationActivity::ColumnIsDataTypeInferred");
			}
			base.ReadMetadataProperties(context, reader);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				string text;
				if (context.TryExtractActivityInfo<string>("SerializationActivity::ColumnName", out text))
				{
					bool flag;
					if (!context.TryExtractActivityInfo<bool>("SerializationActivity::ColumnIsNameInferred", out flag))
					{
						flag = false;
					}
					if (flag)
					{
						this.body.InferredName = text;
					}
					else
					{
						this.body.ExplicitName = text;
					}
				}
				DataType dataType;
				if (context.TryExtractActivityInfo<DataType>("SerializationActivity::ColumnDataType", out dataType))
				{
					bool flag2;
					if (!context.TryExtractActivityInfo<bool>("SerializationActivity::ColumnIsDataTypeInferred", out flag2))
					{
						flag2 = false;
					}
					if (flag2)
					{
						this.body.InferredDataType = dataType;
						return;
					}
					this.body.ExplicitDataType = dataType;
				}
			}
		}

		// Token: 0x040000E0 RID: 224
		internal Column.ObjectBody body;

		// Token: 0x040000E1 RID: 225
		private VariationCollection _Variations;

		// Token: 0x040000E2 RID: 226
		private ColumnAnnotationCollection _Annotations;

		// Token: 0x040000E3 RID: 227
		private ColumnExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x040000E4 RID: 228
		private ColumnChangedPropertyCollection _ChangedProperties;

		// Token: 0x02000242 RID: 578
		internal class ObjectBody : NamedMetadataObjectBody<Column>
		{
			// Token: 0x06001F61 RID: 8033 RVA: 0x000CF1F8 File Offset: 0x000CD3F8
			public ObjectBody(Column owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.RefreshedTime = DateTime.MinValue;
				this.TableID = new ParentLink<Column, Table>(owner, "Table");
				this.ColumnOriginID = new CrossLink<Column, Column>(owner, "ColumnOrigin");
				this.SortByColumnID = new CrossLink<Column, Column>(owner, "SortByColumn");
				this.AttributeHierarchyID = new ChildLink<Column, AttributeHierarchy>(owner, "AttributeHierarchy");
				this.RelatedColumnDetailsID = new ChildLink<Column, RelatedColumnDetails>(owner, "RelatedColumnDetails");
				this.AlternateOfID = new ChildLink<Column, AlternateOf>(owner, "AlternateOf");
			}

			// Token: 0x06001F62 RID: 8034 RVA: 0x000CF293 File Offset: 0x000CD493
			public override string GetObjectName()
			{
				return Column.ComputeName(this);
			}

			// Token: 0x06001F63 RID: 8035 RVA: 0x000CF29C File Offset: 0x000CD49C
			internal bool IsEqualTo(Column.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.ExplicitName, other.ExplicitName) && PropertyHelper.AreValuesIdentical(this.InferredName, other.InferredName) && PropertyHelper.AreValuesIdentical(this.ExplicitDataType, other.ExplicitDataType) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.InferredDataType, other.InferredDataType)) && PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && PropertyHelper.AreValuesIdentical(this.IsUnique, other.IsUnique) && PropertyHelper.AreValuesIdentical(this.IsKey, other.IsKey) && PropertyHelper.AreValuesIdentical(this.IsNullable, other.IsNullable) && PropertyHelper.AreValuesIdentical(this.Alignment, other.Alignment) && PropertyHelper.AreValuesIdentical(this.TableDetailPosition, other.TableDetailPosition) && PropertyHelper.AreValuesIdentical(this.IsDefaultLabel, other.IsDefaultLabel) && PropertyHelper.AreValuesIdentical(this.IsDefaultImage, other.IsDefaultImage) && PropertyHelper.AreValuesIdentical(this.SummarizeBy, other.SummarizeBy) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.Type, other.Type)) && PropertyHelper.AreValuesIdentical(this.SourceColumn, other.SourceColumn) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.FormatString, other.FormatString) && PropertyHelper.AreValuesIdentical(this.IsAvailableInMDX, other.IsAvailableInMDX) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime)) && PropertyHelper.AreValuesIdentical(this.KeepUniqueRows, other.KeepUniqueRows) && PropertyHelper.AreValuesIdentical(this.DisplayOrdinal, other.DisplayOrdinal) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.SourceProviderType, other.SourceProviderType) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && PropertyHelper.AreValuesIdentical(this.EncodingHint, other.EncodingHint) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.EvaluationBehavior, other.EvaluationBehavior) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ColumnOriginID.IsEqualTo(other.ColumnOriginID, context)) && this.SortByColumnID.IsEqualTo(other.SortByColumnID, context) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.AttributeHierarchyID.IsEqualTo(other.AttributeHierarchyID, context)) && this.RelatedColumnDetailsID.IsEqualTo(other.RelatedColumnDetailsID, context) && this.AlternateOfID.IsEqualTo(other.AlternateOfID, context);
			}

			// Token: 0x06001F64 RID: 8036 RVA: 0x000CF6A4 File Offset: 0x000CD8A4
			internal void CopyFromImpl(Column.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				if ((context.Flags & CopyFlags.DontTrackObjectChanges) != CopyFlags.DontTrackObjectChanges)
				{
					string text = Column.ComputeName(this);
					string text2 = Column.ComputeName(other);
					if (!string.IsNullOrEmpty(text) && !PropertyHelper.AreValuesIdentical(text, text2))
					{
						ObjectChangeTracker.RegisterPropertyChanging(base.Owner, "Name", typeof(string), text, text2);
						string name = base.Owner.Name;
						this.ExplicitName = text2;
						ObjectChangeTracker.RegisterPropertyChanged(base.Owner, "Name", typeof(string), text, text2);
					}
				}
				this.ExplicitName = other.ExplicitName;
				this.InferredName = other.InferredName;
				this.ExplicitDataType = other.ExplicitDataType;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.InferredDataType = other.InferredDataType;
				}
				this.DataCategory = other.DataCategory;
				this.Description = other.Description;
				this.IsHidden = other.IsHidden;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				this.IsUnique = other.IsUnique;
				this.IsKey = other.IsKey;
				this.IsNullable = other.IsNullable;
				this.Alignment = other.Alignment;
				this.TableDetailPosition = other.TableDetailPosition;
				this.IsDefaultLabel = other.IsDefaultLabel;
				this.IsDefaultImage = other.IsDefaultImage;
				this.SummarizeBy = other.SummarizeBy;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.Type = other.Type;
				}
				this.SourceColumn = other.SourceColumn;
				this.Expression = other.Expression;
				this.FormatString = other.FormatString;
				this.IsAvailableInMDX = other.IsAvailableInMDX;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.StructureModifiedTime = other.StructureModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.RefreshedTime = other.RefreshedTime;
				}
				this.KeepUniqueRows = other.KeepUniqueRows;
				this.DisplayOrdinal = other.DisplayOrdinal;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				this.SourceProviderType = other.SourceProviderType;
				this.DisplayFolder = other.DisplayFolder;
				this.EncodingHint = other.EncodingHint;
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.EvaluationBehavior = other.EvaluationBehavior;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ColumnOriginID.CopyFrom(other.ColumnOriginID, context);
				}
				this.SortByColumnID.CopyFrom(other.SortByColumnID, context);
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.AttributeHierarchyID.CopyFrom(other.AttributeHierarchyID, context);
				}
				this.RelatedColumnDetailsID.CopyFrom(other.RelatedColumnDetailsID, context);
				this.AlternateOfID.CopyFrom(other.AlternateOfID, context);
			}

			// Token: 0x06001F65 RID: 8037 RVA: 0x000CF9F4 File Offset: 0x000CDBF4
			internal void CopyFromImpl(Column.ObjectBody other)
			{
				this.ExplicitName = other.ExplicitName;
				this.InferredName = other.InferredName;
				this.ExplicitDataType = other.ExplicitDataType;
				this.InferredDataType = other.InferredDataType;
				this.DataCategory = other.DataCategory;
				this.Description = other.Description;
				this.IsHidden = other.IsHidden;
				this.State = other.State;
				this.IsUnique = other.IsUnique;
				this.IsKey = other.IsKey;
				this.IsNullable = other.IsNullable;
				this.Alignment = other.Alignment;
				this.TableDetailPosition = other.TableDetailPosition;
				this.IsDefaultLabel = other.IsDefaultLabel;
				this.IsDefaultImage = other.IsDefaultImage;
				this.SummarizeBy = other.SummarizeBy;
				this.Type = other.Type;
				this.SourceColumn = other.SourceColumn;
				this.Expression = other.Expression;
				this.FormatString = other.FormatString;
				this.IsAvailableInMDX = other.IsAvailableInMDX;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.RefreshedTime = other.RefreshedTime;
				this.KeepUniqueRows = other.KeepUniqueRows;
				this.DisplayOrdinal = other.DisplayOrdinal;
				this.ErrorMessage = other.ErrorMessage;
				this.SourceProviderType = other.SourceProviderType;
				this.DisplayFolder = other.DisplayFolder;
				this.EncodingHint = other.EncodingHint;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.EvaluationBehavior = other.EvaluationBehavior;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
				this.ColumnOriginID.CopyFrom(other.ColumnOriginID, ObjectChangeTracker.BodyCloneContext);
				this.SortByColumnID.CopyFrom(other.SortByColumnID, ObjectChangeTracker.BodyCloneContext);
				this.AttributeHierarchyID.CopyFrom(other.AttributeHierarchyID, ObjectChangeTracker.BodyCloneContext);
				this.RelatedColumnDetailsID.CopyFrom(other.RelatedColumnDetailsID, ObjectChangeTracker.BodyCloneContext);
				this.AlternateOfID.CopyFrom(other.AlternateOfID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001F66 RID: 8038 RVA: 0x000CFC11 File Offset: 0x000CDE11
			public override void CopyFrom(MetadataObjectBody<Column> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Column.ObjectBody)other, context);
			}

			// Token: 0x06001F67 RID: 8039 RVA: 0x000CFC28 File Offset: 0x000CDE28
			internal bool IsEqualTo(Column.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.IsUnique, other.IsUnique) && PropertyHelper.AreValuesIdentical(this.IsKey, other.IsKey) && PropertyHelper.AreValuesIdentical(this.IsNullable, other.IsNullable) && PropertyHelper.AreValuesIdentical(this.Alignment, other.Alignment) && PropertyHelper.AreValuesIdentical(this.TableDetailPosition, other.TableDetailPosition) && PropertyHelper.AreValuesIdentical(this.IsDefaultLabel, other.IsDefaultLabel) && PropertyHelper.AreValuesIdentical(this.IsDefaultImage, other.IsDefaultImage) && PropertyHelper.AreValuesIdentical(this.SummarizeBy, other.SummarizeBy) && PropertyHelper.AreValuesIdentical(this.SourceColumn, other.SourceColumn) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.FormatString, other.FormatString) && PropertyHelper.AreValuesIdentical(this.IsAvailableInMDX, other.IsAvailableInMDX) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime) && PropertyHelper.AreValuesIdentical(this.KeepUniqueRows, other.KeepUniqueRows) && PropertyHelper.AreValuesIdentical(this.DisplayOrdinal, other.DisplayOrdinal) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.SourceProviderType, other.SourceProviderType) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && PropertyHelper.AreValuesIdentical(this.EncodingHint, other.EncodingHint) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.EvaluationBehavior, other.EvaluationBehavior) && this.TableID.IsEqualTo(other.TableID) && this.ColumnOriginID.IsEqualTo(other.ColumnOriginID) && this.SortByColumnID.IsEqualTo(other.SortByColumnID) && this.AttributeHierarchyID.IsEqualTo(other.AttributeHierarchyID) && this.RelatedColumnDetailsID.IsEqualTo(other.RelatedColumnDetailsID) && this.AlternateOfID.IsEqualTo(other.AlternateOfID) && (PropertyHelper.AreValuesIdentical(this.ExplicitName, other.ExplicitName) || base.RenameRequestedThroughAPI) && PropertyHelper.AreValuesIdentical(Column.ComputeName(this), Column.ComputeName(other)) && PropertyHelper.AreValuesIdentical(this.ExplicitDataType, other.ExplicitDataType) && PropertyHelper.AreValuesIdentical(Column.ComputeDataType(this), Column.ComputeDataType(other)) && PropertyHelper.AreValuesIdentical(this.InferredName, other.InferredName) && PropertyHelper.AreValuesIdentical(Column.ComputeIsNameInferred(this), Column.ComputeIsNameInferred(other)) && PropertyHelper.AreValuesIdentical(this.InferredDataType, other.InferredDataType) && PropertyHelper.AreValuesIdentical(Column.ComputeIsDataTypeInferred(this), Column.ComputeIsDataTypeInferred(other));
			}

			// Token: 0x06001F68 RID: 8040 RVA: 0x000CFFB0 File Offset: 0x000CE1B0
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Column.ObjectBody)other);
			}

			// Token: 0x06001F69 RID: 8041 RVA: 0x000CFFCC File Offset: 0x000CE1CC
			internal void CompareWith(Column.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory))
				{
					context.RegisterPropertyChange(base.Owner, "DataCategory", typeof(string), PropertyFlags.DdlAndUser, other.DataCategory, this.DataCategory);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden))
				{
					context.RegisterPropertyChange(base.Owner, "IsHidden", typeof(bool), PropertyFlags.DdlAndUser, other.IsHidden, this.IsHidden);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsUnique, other.IsUnique))
				{
					context.RegisterPropertyChange(base.Owner, "IsUnique", typeof(bool), PropertyFlags.DdlAndUser, other.IsUnique, this.IsUnique);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsKey, other.IsKey))
				{
					context.RegisterPropertyChange(base.Owner, "IsKey", typeof(bool), PropertyFlags.DdlAndUser, other.IsKey, this.IsKey);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsNullable, other.IsNullable))
				{
					context.RegisterPropertyChange(base.Owner, "IsNullable", typeof(bool), PropertyFlags.DdlAndUser, other.IsNullable, this.IsNullable);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Alignment, other.Alignment))
				{
					context.RegisterPropertyChange(base.Owner, "Alignment", typeof(Alignment), PropertyFlags.DdlAndUser, other.Alignment, this.Alignment);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TableDetailPosition, other.TableDetailPosition))
				{
					context.RegisterPropertyChange(base.Owner, "TableDetailPosition", typeof(int), PropertyFlags.DdlAndUser, other.TableDetailPosition, this.TableDetailPosition);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsDefaultLabel, other.IsDefaultLabel))
				{
					context.RegisterPropertyChange(base.Owner, "IsDefaultLabel", typeof(bool), PropertyFlags.DdlAndUser, other.IsDefaultLabel, this.IsDefaultLabel);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsDefaultImage, other.IsDefaultImage))
				{
					context.RegisterPropertyChange(base.Owner, "IsDefaultImage", typeof(bool), PropertyFlags.DdlAndUser, other.IsDefaultImage, this.IsDefaultImage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SummarizeBy, other.SummarizeBy))
				{
					context.RegisterPropertyChange(base.Owner, "SummarizeBy", typeof(AggregateFunction), PropertyFlags.DdlAndUser, other.SummarizeBy, this.SummarizeBy);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceColumn, other.SourceColumn))
				{
					context.RegisterPropertyChange(base.Owner, "SourceColumn", typeof(string), PropertyFlags.DdlAndUser, other.SourceColumn, this.SourceColumn);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.FormatString, other.FormatString))
				{
					context.RegisterPropertyChange(base.Owner, "FormatString", typeof(string), PropertyFlags.DdlAndUser, other.FormatString, this.FormatString);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsAvailableInMDX, other.IsAvailableInMDX))
				{
					context.RegisterPropertyChange(base.Owner, "IsAvailableInMDX", typeof(bool), PropertyFlags.DdlAndUser, other.IsAvailableInMDX, this.IsAvailableInMDX);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime))
				{
					context.RegisterPropertyChange(base.Owner, "RefreshedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.RefreshedTime, this.RefreshedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.KeepUniqueRows, other.KeepUniqueRows))
				{
					context.RegisterPropertyChange(base.Owner, "KeepUniqueRows", typeof(bool), PropertyFlags.DdlAndUser, other.KeepUniqueRows, this.KeepUniqueRows);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DisplayOrdinal, other.DisplayOrdinal))
				{
					context.RegisterPropertyChange(base.Owner, "DisplayOrdinal", typeof(int), PropertyFlags.DdlAndUser, other.DisplayOrdinal, this.DisplayOrdinal);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceProviderType, other.SourceProviderType))
				{
					context.RegisterPropertyChange(base.Owner, "SourceProviderType", typeof(string), PropertyFlags.DdlAndUser, other.SourceProviderType, this.SourceProviderType);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder))
				{
					context.RegisterPropertyChange(base.Owner, "DisplayFolder", typeof(string), PropertyFlags.DdlAndUser, other.DisplayFolder, this.DisplayFolder);
				}
				if (!PropertyHelper.AreValuesIdentical(this.EncodingHint, other.EncodingHint))
				{
					context.RegisterPropertyChange(base.Owner, "EncodingHint", typeof(EncodingHintType), PropertyFlags.DdlAndUser, other.EncodingHint, this.EncodingHint);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.EvaluationBehavior, other.EvaluationBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "EvaluationBehavior", typeof(EvaluationBehavior), PropertyFlags.DdlAndUser, other.EvaluationBehavior, this.EvaluationBehavior);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
				this.ColumnOriginID.CompareWith(other.ColumnOriginID, "ColumnOriginID", "ColumnOrigin", PropertyFlags.ReadOnly, context);
				this.SortByColumnID.CompareWith(other.SortByColumnID, "SortByColumnID", "SortByColumn", PropertyFlags.None, context);
				this.AttributeHierarchyID.CompareWith(other.AttributeHierarchyID, "AttributeHierarchyID", "AttributeHierarchy", PropertyFlags.ReadOnly, context);
				this.RelatedColumnDetailsID.CompareWith(other.RelatedColumnDetailsID, "RelatedColumnDetailsID", "RelatedColumnDetails", PropertyFlags.None, context);
				this.AlternateOfID.CompareWith(other.AlternateOfID, "AlternateOfID", "AlternateOf", PropertyFlags.None, context);
				if (!PropertyHelper.AreValuesIdentical(this.ExplicitName, other.ExplicitName) && !base.RenameRequestedThroughAPI)
				{
					context.RegisterPropertyChange(base.Owner, "ExplicitName", typeof(string), PropertyFlags.Ddl, other.ExplicitName, this.ExplicitName);
				}
				if (!PropertyHelper.AreValuesIdentical(Column.ComputeName(this), Column.ComputeName(other)))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), PropertyFlags.User, Column.ComputeName(other), Column.ComputeName(this));
				}
				if (!PropertyHelper.AreValuesIdentical(this.ExplicitDataType, other.ExplicitDataType))
				{
					context.RegisterPropertyChange(base.Owner, "ExplicitDataType", typeof(DataType), PropertyFlags.Ddl, other.ExplicitDataType, this.ExplicitDataType);
				}
				if (!PropertyHelper.AreValuesIdentical(Column.ComputeDataType(this), Column.ComputeDataType(other)))
				{
					context.RegisterPropertyChange(base.Owner, "DataType", typeof(DataType), PropertyFlags.User, Column.ComputeDataType(other), Column.ComputeDataType(this));
				}
				if (!PropertyHelper.AreValuesIdentical(this.InferredName, other.InferredName))
				{
					context.RegisterPropertyChange(base.Owner, "InferredName", typeof(string), PropertyFlags.Ddl | PropertyFlags.ReadOnly, other.InferredName, this.InferredName);
				}
				if (!PropertyHelper.AreValuesIdentical(Column.ComputeIsNameInferred(this), Column.ComputeIsNameInferred(other)))
				{
					context.RegisterPropertyChange(base.Owner, "IsNameInferred", typeof(bool), PropertyFlags.User, Column.ComputeIsNameInferred(other), Column.ComputeIsNameInferred(this));
				}
				if (!PropertyHelper.AreValuesIdentical(this.InferredDataType, other.InferredDataType))
				{
					context.RegisterPropertyChange(base.Owner, "InferredDataType", typeof(DataType), PropertyFlags.Ddl | PropertyFlags.ReadOnly, other.InferredDataType, this.InferredDataType);
				}
				if (!PropertyHelper.AreValuesIdentical(Column.ComputeIsDataTypeInferred(this), Column.ComputeIsDataTypeInferred(other)))
				{
					context.RegisterPropertyChange(base.Owner, "IsDataTypeInferred", typeof(bool), PropertyFlags.User, Column.ComputeIsDataTypeInferred(other), Column.ComputeIsDataTypeInferred(this));
				}
			}

			// Token: 0x06001F6A RID: 8042 RVA: 0x000D09C1 File Offset: 0x000CEBC1
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Column.ObjectBody)other, context);
			}

			// Token: 0x04000785 RID: 1925
			internal string ExplicitName;

			// Token: 0x04000786 RID: 1926
			internal string InferredName;

			// Token: 0x04000787 RID: 1927
			internal DataType ExplicitDataType;

			// Token: 0x04000788 RID: 1928
			internal DataType InferredDataType;

			// Token: 0x04000789 RID: 1929
			internal string DataCategory;

			// Token: 0x0400078A RID: 1930
			internal string Description;

			// Token: 0x0400078B RID: 1931
			internal bool IsHidden;

			// Token: 0x0400078C RID: 1932
			internal ObjectState State;

			// Token: 0x0400078D RID: 1933
			internal bool IsUnique;

			// Token: 0x0400078E RID: 1934
			internal bool IsKey;

			// Token: 0x0400078F RID: 1935
			internal bool IsNullable;

			// Token: 0x04000790 RID: 1936
			internal Alignment Alignment;

			// Token: 0x04000791 RID: 1937
			internal int TableDetailPosition;

			// Token: 0x04000792 RID: 1938
			internal bool IsDefaultLabel;

			// Token: 0x04000793 RID: 1939
			internal bool IsDefaultImage;

			// Token: 0x04000794 RID: 1940
			internal AggregateFunction SummarizeBy;

			// Token: 0x04000795 RID: 1941
			internal ColumnType Type;

			// Token: 0x04000796 RID: 1942
			internal string SourceColumn;

			// Token: 0x04000797 RID: 1943
			internal string Expression;

			// Token: 0x04000798 RID: 1944
			internal string FormatString;

			// Token: 0x04000799 RID: 1945
			internal bool IsAvailableInMDX;

			// Token: 0x0400079A RID: 1946
			internal DateTime ModifiedTime;

			// Token: 0x0400079B RID: 1947
			internal DateTime StructureModifiedTime;

			// Token: 0x0400079C RID: 1948
			internal DateTime RefreshedTime;

			// Token: 0x0400079D RID: 1949
			internal bool KeepUniqueRows;

			// Token: 0x0400079E RID: 1950
			internal int DisplayOrdinal;

			// Token: 0x0400079F RID: 1951
			internal string ErrorMessage;

			// Token: 0x040007A0 RID: 1952
			internal string SourceProviderType;

			// Token: 0x040007A1 RID: 1953
			internal string DisplayFolder;

			// Token: 0x040007A2 RID: 1954
			internal EncodingHintType EncodingHint;

			// Token: 0x040007A3 RID: 1955
			internal string LineageTag;

			// Token: 0x040007A4 RID: 1956
			internal string SourceLineageTag;

			// Token: 0x040007A5 RID: 1957
			internal EvaluationBehavior EvaluationBehavior;

			// Token: 0x040007A6 RID: 1958
			internal ParentLink<Column, Table> TableID;

			// Token: 0x040007A7 RID: 1959
			internal CrossLink<Column, Column> ColumnOriginID;

			// Token: 0x040007A8 RID: 1960
			internal CrossLink<Column, Column> SortByColumnID;

			// Token: 0x040007A9 RID: 1961
			internal ChildLink<Column, AttributeHierarchy> AttributeHierarchyID;

			// Token: 0x040007AA RID: 1962
			internal ChildLink<Column, RelatedColumnDetails> RelatedColumnDetailsID;

			// Token: 0x040007AB RID: 1963
			internal ChildLink<Column, AlternateOf> AlternateOfID;
		}

		// Token: 0x02000243 RID: 579
		internal static class SerializationActivityInfoKey
		{
			// Token: 0x040007AC RID: 1964
			public const string Name = "SerializationActivity::ColumnName";

			// Token: 0x040007AD RID: 1965
			public const string IsNameInferred = "SerializationActivity::ColumnIsNameInferred";

			// Token: 0x040007AE RID: 1966
			public const string DataType = "SerializationActivity::ColumnDataType";

			// Token: 0x040007AF RID: 1967
			public const string IsDataTypeInferred = "SerializationActivity::ColumnIsDataTypeInferred";
		}
	}
}
