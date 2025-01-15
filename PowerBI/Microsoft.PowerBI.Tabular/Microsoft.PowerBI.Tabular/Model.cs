using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Tabular.AdaptiveCaching;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200007A RID: 122
	public sealed class Model : NamedMetadataObject, INotifyObjectIdChange
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x00035164 File Offset: 0x00033364
		public Model()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
			this.OnAfterConstructor();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000351CC File Offset: 0x000333CC
		internal Model(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
			this.OnAfterConstructor();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00035230 File Offset: 0x00033430
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Model.ObjectBody(this);
			this.body.Name = "Model";
			this.body.Description = string.Empty;
			this.body.StorageLocation = string.Empty;
			this.body.DefaultMode = ModeType.Import;
			this.body.DefaultDataView = DataViewType.Full;
			this.body.Culture = string.Empty;
			this.body.Collation = string.Empty;
			this.body.DataAccessOptions = string.Empty;
			this.body.DefaultPowerBIDataSourceVersion = PowerBIDataSourceVersion.PowerBI_V1;
			this.body.ForceUniqueNames = false;
			this.body.DiscourageImplicitMeasures = false;
			this.body.DiscourageReportMeasures = false;
			this.body.DataSourceVariablesOverrideBehavior = DataSourceVariablesOverrideBehaviorType.Disallow;
			this.body.DataSourceDefaultMaxConnections = 10;
			this.body.SourceQueryCulture = string.Empty;
			this.body.MAttributes = string.Empty;
			this.body.DiscourageCompositeModels = false;
			this.body.AutomaticAggregationOptions = string.Empty;
			this.body.DisableAutoExists = -1;
			this.body.MaxParallelismPerRefresh = -1;
			this.body.MaxParallelismPerQuery = 0;
			this.body.DirectLakeBehavior = DirectLakeBehavior.Automatic;
			this.body.ValueFilterBehavior = ValueFilterBehaviorType.Automatic;
			this._Tables = new TableCollection(this, comparer);
			this._Relationships = new RelationshipCollection(this, comparer);
			this._DataSources = new DataSourceCollection(this, comparer);
			this._Perspectives = new PerspectiveCollection(this, comparer);
			this._Cultures = new CultureCollection(this, comparer);
			this._Roles = new ModelRoleCollection(this, comparer);
			this._Expressions = new NamedExpressionCollection(this, comparer);
			this._QueryGroups = new QueryGroupCollection(this, comparer);
			this._AnalyticsAIMetadata = new AnalyticsAIMetadataCollection(this, comparer);
			this._Functions = new FunctionCollection(this, comparer);
			this._BindingInfoCollection = new BindingInfoCollection(this, comparer);
			this._Annotations = new ModelAnnotationCollection(this, comparer);
			this._ExtendedProperties = new ModelExtendedPropertyCollection(this, comparer);
			this._ExcludedArtifacts = new ModelExcludedArtifactCollection(this);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00035437 File Offset: 0x00033637
		private void OnAfterConstructor()
		{
			base.Id = ObjectId.Model;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00035444 File Offset: 0x00033644
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Model;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00035447 File Offset: 0x00033647
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0003544A File Offset: 0x0003364A
		public override MetadataObject Parent
		{
			get
			{
				return null;
			}
			internal set
			{
				if (value != null)
				{
					throw new TomInternalException("Cannot set Model's parent because Model is a top-level metadata object");
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0003545A File Offset: 0x0003365A
		internal override ObjectId ParentId
		{
			get
			{
				return ObjectId.Null;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00035464 File Offset: 0x00033664
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Model, null, "Model object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("storageLocation", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("storageLocation", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("defaultMode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("defaultMode", MetadataPropertyNature.RegularProperty, PropertyHelper.GetModeTypeCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("defaultDataView", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DataViewType>("defaultDataView", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("culture", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("culture", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("collation", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("collation", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("defaultPowerBIDataSourceVersion", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<PowerBIDataSourceVersion>("defaultPowerBIDataSourceVersion", MetadataPropertyNature.RegularProperty, PropertyHelper.GetPowerBIDataSourceVersionCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("forceUniqueNames", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("forceUniqueNames", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("discourageImplicitMeasures", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("discourageImplicitMeasures", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("discourageReportMeasures", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("discourageReportMeasures", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("dataSourceVariablesOverrideBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DataSourceVariablesOverrideBehaviorType>("dataSourceVariablesOverrideBehavior", MetadataPropertyNature.RegularProperty, null);
				}
				if (CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("dataSourceDefaultMaxConnections", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("dataSourceDefaultMaxConnections", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceQueryCulture", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceQueryCulture", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Model_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("mAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("mAttributes", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("discourageCompositeModels", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("discourageCompositeModels", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("disableAutoExists", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("disableAutoExists", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("maxParallelismPerRefresh", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("maxParallelismPerRefresh", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("maxParallelismPerQuery", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("maxParallelismPerQuery", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("directLakeBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DirectLakeBehavior>("directLakeBehavior", MetadataPropertyNature.RegularProperty, null);
				}
				if (CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("valueFilterBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ValueFilterBehaviorType>("valueFilterBehavior", MetadataPropertyNature.RegularProperty, null);
				}
				if (CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("defaultMeasure", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<Model, Measure>.WriteMetadataSchema(ObjectType.Measure, ObjectType.Table, true, "defaultMeasure", false, writer);
				}
				if (CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("dataAccessOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("dataAccessOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, typeof(string));
				}
				if (CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("automaticAggregationOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					writer.WriteProperty("automaticAggregationOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("dataSources", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "dataSources", MetadataPropertyNature.ChildCollection, ObjectType.DataSource);
				}
				if (writer.ShouldIncludeProperty("tables", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "tables", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Table);
				}
				if (writer.ShouldIncludeProperty("relationships", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "relationships", MetadataPropertyNature.ChildCollection, ObjectType.Relationship);
				}
				if (writer.ShouldIncludeProperty("cultures", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "cultures", MetadataPropertyNature.ChildCollection, ObjectType.Culture);
				}
				if (writer.ShouldIncludeProperty("perspectives", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "perspectives", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Perspective);
				}
				if (writer.ShouldIncludeProperty("roles", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "roles", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Role);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (CompatibilityRestrictions.NamedExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("expressions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "expressions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Expression);
				}
				if (CompatibilityRestrictions.QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("queryGroups", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "queryGroups", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.QueryGroup);
				}
				if (CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("analyticsAIMetadata", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "analyticsAIMetadata", MetadataPropertyNature.ChildCollection, ObjectType.AnalyticsAIMetadata);
				}
				if (CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("excludedArtifacts", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "excludedArtifacts", MetadataPropertyNature.ChildCollection, ObjectType.ExcludedArtifact);
				}
				if (CompatibilityRestrictions.Function.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("functions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "functions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Function);
				}
				if (CompatibilityRestrictions.BindingInfo.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("bindingInfoCollection", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "bindingInfoCollection", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.BindingInfo);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00035C74 File Offset: 0x00033E74
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.DefaultMode != ModeType.Import)
			{
				int num = PropertyHelper.GetModeTypeCompatibilityRestrictions(this.body.DefaultMode)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMode");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.DataAccessOptions) || this.dataAccessOptions.IsDirty)
			{
				int num2 = CompatibilityRestrictions.Model_DataAccessOptions[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataAccessOptions");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DefaultMeasureID.Object != null)
			{
				int num3 = CompatibilityRestrictions.Model_DefaultMeasure[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMeasureID");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DefaultPowerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V1)
			{
				int num4;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion[mode], PropertyHelper.GetPowerBIDataSourceVersionCompatibilityRestrictions(this.body.DefaultPowerBIDataSourceVersion)[mode], out num4);
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultPowerBIDataSourceVersion");
					requiredLevel = num4;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ForceUniqueNames)
			{
				int num5 = CompatibilityRestrictions.Model_ForceUniqueNames[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num5, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ForceUniqueNames");
					requiredLevel = num5;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DiscourageImplicitMeasures)
			{
				int num6 = CompatibilityRestrictions.Model_DiscourageImplicitMeasures[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num6, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DiscourageImplicitMeasures");
					requiredLevel = num6;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DiscourageReportMeasures)
			{
				int num7 = CompatibilityRestrictions.Model_DiscourageReportMeasures[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num7, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DiscourageReportMeasures");
					requiredLevel = num7;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DataSourceVariablesOverrideBehavior != DataSourceVariablesOverrideBehaviorType.Disallow)
			{
				int num8;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior[mode], PropertyHelper.GetDataSourceVariablesOverrideBehaviorTypeCompatibilityRestrictions(this.body.DataSourceVariablesOverrideBehavior)[mode], out num8);
				if (CompatibilityRestrictionSet.CompareLevel(num8, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataSourceVariablesOverrideBehavior");
					requiredLevel = num8;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DataSourceDefaultMaxConnections != 10)
			{
				int num9 = CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num9, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataSourceDefaultMaxConnections");
					requiredLevel = num9;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceQueryCulture))
			{
				int num10 = CompatibilityRestrictions.Model_SourceQueryCulture[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num10, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceQueryCulture");
					requiredLevel = num10;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				int num11 = CompatibilityRestrictions.Model_MAttributes[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num11, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MAttributes");
					requiredLevel = num11;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DiscourageCompositeModels)
			{
				int num12 = CompatibilityRestrictions.Model_DiscourageCompositeModels[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num12, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DiscourageCompositeModels");
					requiredLevel = num12;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions) || this.automaticAggregationOptions.IsDirty)
			{
				int num13 = CompatibilityRestrictions.Model_AutomaticAggregationOptions[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num13, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "AutomaticAggregationOptions");
					requiredLevel = num13;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DisableAutoExists != -1)
			{
				int num14 = CompatibilityRestrictions.Model_DisableAutoExists[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num14, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DisableAutoExists");
					requiredLevel = num14;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.MaxParallelismPerRefresh != -1)
			{
				int num15 = CompatibilityRestrictions.Model_MaxParallelismPerRefresh[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num15, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MaxParallelismPerRefresh");
					requiredLevel = num15;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.MaxParallelismPerQuery != 0)
			{
				int num16 = CompatibilityRestrictions.Model_MaxParallelismPerQuery[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num16, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MaxParallelismPerQuery");
					requiredLevel = num16;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.DirectLakeBehavior != DirectLakeBehavior.Automatic)
			{
				int num17;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Model_DirectLakeBehavior[mode], PropertyHelper.GetDirectLakeBehaviorCompatibilityRestrictions(this.body.DirectLakeBehavior)[mode], out num17);
				if (CompatibilityRestrictionSet.CompareLevel(num17, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DirectLakeBehavior");
					requiredLevel = num17;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ValueFilterBehavior != ValueFilterBehaviorType.Automatic)
			{
				int num18;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Model_ValueFilterBehavior[mode], PropertyHelper.GetValueFilterBehaviorTypeCompatibilityRestrictions(this.body.ValueFilterBehavior)[mode], out num18);
				if (CompatibilityRestrictionSet.CompareLevel(num18, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ValueFilterBehavior");
					requiredLevel = num18;
					int num19 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00036248 File Offset: 0x00034448
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00036250 File Offset: 0x00034450
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Model.ObjectBody)value;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0003625E File Offset: 0x0003445E
		internal override ITxObjectBody CreateBody()
		{
			return new Model.ObjectBody(this);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00036266 File Offset: 0x00034466
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Model();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0003626D File Offset: 0x0003446D
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00036270 File Offset: 0x00034470
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = ((!this.body.DefaultMeasureID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMeasure")) : null);
			if (this.body.DefaultMeasureID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, array);
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000362D4 File Offset: 0x000344D4
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = ((!this.body.DefaultMeasureID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMeasure")) : null);
			if (this.body.DefaultMeasureID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, array);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00036338 File Offset: 0x00034538
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.DefaultMeasureID.IsResolved)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMeasure"));
				if (this.body.DefaultMeasureID.TryResolveByPath())
				{
					base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, array);
				}
				else
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMeasure"));
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000363BB File Offset: 0x000345BB
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.DefaultMeasureID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000363CF File Offset: 0x000345CF
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.DefaultMeasureID.Validate(result, throwOnError);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000363E3 File Offset: 0x000345E3
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.DefaultMeasureID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000363FF File Offset: 0x000345FF
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Tables;
			yield return this._Relationships;
			yield return this._DataSources;
			yield return this._Perspectives;
			yield return this._Cultures;
			yield return this._Roles;
			yield return this._Expressions;
			yield return this._QueryGroups;
			yield return this._AnalyticsAIMetadata;
			yield return this._Functions;
			yield return this._BindingInfoCollection;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ExcludedArtifacts;
			yield break;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0003640F File Offset: 0x0003460F
		public TableCollection Tables
		{
			get
			{
				return this._Tables;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00036417 File Offset: 0x00034617
		public RelationshipCollection Relationships
		{
			get
			{
				return this._Relationships;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0003641F File Offset: 0x0003461F
		public DataSourceCollection DataSources
		{
			get
			{
				return this._DataSources;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00036427 File Offset: 0x00034627
		public PerspectiveCollection Perspectives
		{
			get
			{
				return this._Perspectives;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0003642F File Offset: 0x0003462F
		public CultureCollection Cultures
		{
			get
			{
				return this._Cultures;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00036437 File Offset: 0x00034637
		public ModelRoleCollection Roles
		{
			get
			{
				return this._Roles;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0003643F File Offset: 0x0003463F
		[CompatibilityRequirement("1400")]
		public NamedExpressionCollection Expressions
		{
			get
			{
				return this._Expressions;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00036447 File Offset: 0x00034647
		[CompatibilityRequirement("1480")]
		public QueryGroupCollection QueryGroups
		{
			get
			{
				return this._QueryGroups;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0003644F File Offset: 0x0003464F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public AnalyticsAIMetadataCollection AnalyticsAIMetadata
		{
			get
			{
				return this._AnalyticsAIMetadata;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00036457 File Offset: 0x00034657
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Internal")]
		public FunctionCollection Functions
		{
			get
			{
				return this._Functions;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0003645F File Offset: 0x0003465F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public BindingInfoCollection BindingInfoCollection
		{
			get
			{
				return this._BindingInfoCollection;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00036467 File Offset: 0x00034667
		public ModelAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0003646F File Offset: 0x0003466F
		[CompatibilityRequirement("1400")]
		public ModelExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00036477 File Offset: 0x00034677
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public ModelExcludedArtifactCollection ExcludedArtifacts
		{
			get
			{
				return this._ExcludedArtifacts;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0003647F File Offset: 0x0003467F
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0003648C File Offset: 0x0003468C
		public override string Name
		{
			get
			{
				return this.body.Name;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Name, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Model, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Name", typeof(string), this.body.Name, value);
					string name = this.body.Name;
					this.body.Name = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Name", typeof(string), name, value);
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0003650E File Offset: 0x0003470E
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0003651C File Offset: 0x0003471C
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0003658C File Offset: 0x0003478C
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0003659C File Offset: 0x0003479C
		public string StorageLocation
		{
			get
			{
				return this.body.StorageLocation;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StorageLocation, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StorageLocation", typeof(string), this.body.StorageLocation, value);
					string storageLocation = this.body.StorageLocation;
					this.body.StorageLocation = value;
					this.OnPropertySetStorageLocation(value);
					ObjectChangeTracker.RegisterPropertyChanged(this, "StorageLocation", typeof(string), storageLocation, value);
				}
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00036613 File Offset: 0x00034813
		private void OnPropertySetStorageLocation(string value)
		{
			if (this.database != null)
			{
				this.database.DbStorageLocation = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00036629 File Offset: 0x00034829
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00036638 File Offset: 0x00034838
		public ModeType DefaultMode
		{
			get
			{
				return this.body.DefaultMode;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultMode, value))
				{
					CompatibilityRestrictionSet modeTypeCompatibilityRestrictions = PropertyHelper.GetModeTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet modeTypeCompatibilityRestrictions2 = PropertyHelper.GetModeTypeCompatibilityRestrictions(this.body.DefaultMode);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = modeTypeCompatibilityRestrictions.Compare(modeTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ModeType.Import))
					{
						array = base.ValidateCompatibilityRequirement(modeTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "DefaultMode", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultMode", typeof(ModeType), this.body.DefaultMode, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(modeTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(modeTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(modeTypeCompatibilityRestrictions, array);
						break;
					}
					ModeType defaultMode = this.body.DefaultMode;
					this.body.DefaultMode = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultMode", typeof(ModeType), defaultMode, value);
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00036759 File Offset: 0x00034959
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00036768 File Offset: 0x00034968
		public DataViewType DefaultDataView
		{
			get
			{
				return this.body.DefaultDataView;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultDataView, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultDataView", typeof(DataViewType), this.body.DefaultDataView, value);
					DataViewType defaultDataView = this.body.DefaultDataView;
					this.body.DefaultDataView = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultDataView", typeof(DataViewType), defaultDataView, value);
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000367EC File Offset: 0x000349EC
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x000367FC File Offset: 0x000349FC
		public string Culture
		{
			get
			{
				return this.body.Culture;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Culture, value))
				{
					this.OnPropertySettingCulture(value);
					ObjectChangeTracker.RegisterPropertyChanging(this, "Culture", typeof(string), this.body.Culture, value);
					string culture = this.body.Culture;
					this.body.Culture = value;
					this.OnPropertySetCulture(value);
					ObjectChangeTracker.RegisterPropertyChanged(this, "Culture", typeof(string), culture, value);
				}
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0003687A File Offset: 0x00034A7A
		private void OnPropertySettingCulture(string value)
		{
			this.CanUpdateChildCollections(value, this.Collation);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00036889 File Offset: 0x00034A89
		private void OnPropertySetCulture(string value)
		{
			this.UpdateChildCollections(value, this.Collation);
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00036898 File Offset: 0x00034A98
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x000368A8 File Offset: 0x00034AA8
		public string Collation
		{
			get
			{
				return this.body.Collation;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Collation, value))
				{
					this.OnPropertySettingCollation(value);
					ObjectChangeTracker.RegisterPropertyChanging(this, "Collation", typeof(string), this.body.Collation, value);
					string collation = this.body.Collation;
					this.body.Collation = value;
					this.OnPropertySetCollation(value);
					ObjectChangeTracker.RegisterPropertyChanged(this, "Collation", typeof(string), collation, value);
				}
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00036926 File Offset: 0x00034B26
		private void OnPropertySettingCollation(string value)
		{
			this.CanUpdateChildCollections(this.Culture, value);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00036935 File Offset: 0x00034B35
		private void OnPropertySetCollation(string value)
		{
			this.UpdateChildCollections(this.Culture, value);
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00036944 File Offset: 0x00034B44
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00036954 File Offset: 0x00034B54
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000369D8 File Offset: 0x00034BD8
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x000369E8 File Offset: 0x00034BE8
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00036A6C File Offset: 0x00034C6C
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00036A88 File Offset: 0x00034C88
		[CompatibilityRequirement("1400")]
		public DataAccessOptions DataAccessOptions
		{
			get
			{
				return this.dataAccessOptions.GetProperty(this, this.body.DataAccessOptions);
			}
			set
			{
				if (!this.dataAccessOptions.IsSamePropertyReference(value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						if (((ICustomProperty<Model, string>)value).Owner != null)
						{
							throw new ArgumentException(TomSR.Exception_CustomPropertyAssignedToMultipleObjects("DataAccessOptions"), "value");
						}
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DataAccessOptions, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataAccessOptions"));
					}
					string text = this.body.DataAccessOptions;
					this.dataAccessOptions.ExtractMetadataValueIfNeeded(ref text, false);
					string text2 = ((value != null) ? ((ICustomProperty<Model, string>)value).Convert() : string.Empty);
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataAccessOptions", typeof(string), text, text2);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DataAccessOptions, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					this.body.DataAccessOptions = text2;
					this.dataAccessOptions.SetProperty(this, value, false);
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataAccessOptions", typeof(string), text, text2);
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00036B73 File Offset: 0x00034D73
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00036B80 File Offset: 0x00034D80
		[CompatibilityRequirement("1450")]
		public PowerBIDataSourceVersion DefaultPowerBIDataSourceVersion
		{
			get
			{
				return this.body.DefaultPowerBIDataSourceVersion;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultPowerBIDataSourceVersion, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.Merge(PropertyHelper.GetPowerBIDataSourceVersionCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.Merge(PropertyHelper.GetPowerBIDataSourceVersionCompatibilityRestrictions(this.body.DefaultPowerBIDataSourceVersion));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != PowerBIDataSourceVersion.PowerBI_V1))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "DefaultPowerBIDataSourceVersion", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultPowerBIDataSourceVersion", typeof(PowerBIDataSourceVersion), this.body.DefaultPowerBIDataSourceVersion, value);
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
					PowerBIDataSourceVersion defaultPowerBIDataSourceVersion = this.body.DefaultPowerBIDataSourceVersion;
					this.body.DefaultPowerBIDataSourceVersion = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultPowerBIDataSourceVersion", typeof(PowerBIDataSourceVersion), defaultPowerBIDataSourceVersion, value);
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00036CB5 File Offset: 0x00034EB5
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00036CC4 File Offset: 0x00034EC4
		[CompatibilityRequirement("1465")]
		public bool ForceUniqueNames
		{
			get
			{
				return this.body.ForceUniqueNames;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ForceUniqueNames, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_ForceUniqueNames, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ForceUniqueNames"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ForceUniqueNames", typeof(bool), this.body.ForceUniqueNames, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_ForceUniqueNames, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool forceUniqueNames = this.body.ForceUniqueNames;
					this.body.ForceUniqueNames = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ForceUniqueNames", typeof(bool), forceUniqueNames, value);
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00036D88 File Offset: 0x00034F88
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00036D98 File Offset: 0x00034F98
		[CompatibilityRequirement("1470")]
		public bool DiscourageImplicitMeasures
		{
			get
			{
				return this.body.DiscourageImplicitMeasures;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DiscourageImplicitMeasures, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DiscourageImplicitMeasures, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DiscourageImplicitMeasures"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DiscourageImplicitMeasures", typeof(bool), this.body.DiscourageImplicitMeasures, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DiscourageImplicitMeasures, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool discourageImplicitMeasures = this.body.DiscourageImplicitMeasures;
					this.body.DiscourageImplicitMeasures = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DiscourageImplicitMeasures", typeof(bool), discourageImplicitMeasures, value);
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00036E5C File Offset: 0x0003505C
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00036E6C File Offset: 0x0003506C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Internal")]
		public bool DiscourageReportMeasures
		{
			get
			{
				return this.body.DiscourageReportMeasures;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DiscourageReportMeasures, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DiscourageReportMeasures, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DiscourageReportMeasures"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DiscourageReportMeasures", typeof(bool), this.body.DiscourageReportMeasures, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DiscourageReportMeasures, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool discourageReportMeasures = this.body.DiscourageReportMeasures;
					this.body.DiscourageReportMeasures = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DiscourageReportMeasures", typeof(bool), discourageReportMeasures, value);
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00036F30 File Offset: 0x00035130
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00036F40 File Offset: 0x00035140
		[CompatibilityRequirement("1475")]
		public DataSourceVariablesOverrideBehaviorType DataSourceVariablesOverrideBehavior
		{
			get
			{
				return this.body.DataSourceVariablesOverrideBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataSourceVariablesOverrideBehavior, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.Merge(PropertyHelper.GetDataSourceVariablesOverrideBehaviorTypeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.Merge(PropertyHelper.GetDataSourceVariablesOverrideBehaviorTypeCompatibilityRestrictions(this.body.DataSourceVariablesOverrideBehavior));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != DataSourceVariablesOverrideBehaviorType.Disallow))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "DataSourceVariablesOverrideBehavior", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataSourceVariablesOverrideBehavior", typeof(DataSourceVariablesOverrideBehaviorType), this.body.DataSourceVariablesOverrideBehavior, value);
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
					DataSourceVariablesOverrideBehaviorType dataSourceVariablesOverrideBehavior = this.body.DataSourceVariablesOverrideBehavior;
					this.body.DataSourceVariablesOverrideBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataSourceVariablesOverrideBehavior", typeof(DataSourceVariablesOverrideBehaviorType), dataSourceVariablesOverrideBehavior, value);
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00037075 File Offset: 0x00035275
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00037084 File Offset: 0x00035284
		[CompatibilityRequirement("1510")]
		public int DataSourceDefaultMaxConnections
		{
			get
			{
				return this.body.DataSourceDefaultMaxConnections;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataSourceDefaultMaxConnections, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != 10)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataSourceDefaultMaxConnections"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataSourceDefaultMaxConnections", typeof(int), this.body.DataSourceDefaultMaxConnections, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					int dataSourceDefaultMaxConnections = this.body.DataSourceDefaultMaxConnections;
					this.body.DataSourceDefaultMaxConnections = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataSourceDefaultMaxConnections", typeof(int), dataSourceDefaultMaxConnections, value);
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0003714A File Offset: 0x0003534A
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00037158 File Offset: 0x00035358
		[CompatibilityRequirement("1520")]
		public string SourceQueryCulture
		{
			get
			{
				return this.body.SourceQueryCulture;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceQueryCulture, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_SourceQueryCulture, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceQueryCulture"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceQueryCulture", typeof(string), this.body.SourceQueryCulture, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_SourceQueryCulture, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string sourceQueryCulture = this.body.SourceQueryCulture;
					this.body.SourceQueryCulture = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceQueryCulture", typeof(string), sourceQueryCulture, value);
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0003720D File Offset: 0x0003540D
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0003721C File Offset: 0x0003541C
		[CompatibilityRequirement("1535")]
		public string MAttributes
		{
			get
			{
				return this.body.MAttributes;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MAttributes, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_MAttributes, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MAttributes"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MAttributes", typeof(string), this.body.MAttributes, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_MAttributes, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string mattributes = this.body.MAttributes;
					this.body.MAttributes = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MAttributes", typeof(string), mattributes, value);
				}
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000372D1 File Offset: 0x000354D1
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x000372E0 File Offset: 0x000354E0
		[CompatibilityRequirement("1560")]
		public bool DiscourageCompositeModels
		{
			get
			{
				return this.body.DiscourageCompositeModels;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DiscourageCompositeModels, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DiscourageCompositeModels, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DiscourageCompositeModels"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DiscourageCompositeModels", typeof(bool), this.body.DiscourageCompositeModels, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DiscourageCompositeModels, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool discourageCompositeModels = this.body.DiscourageCompositeModels;
					this.body.DiscourageCompositeModels = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DiscourageCompositeModels", typeof(bool), discourageCompositeModels, value);
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000373A4 File Offset: 0x000355A4
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x000373C0 File Offset: 0x000355C0
		[CompatibilityRequirement("1564")]
		public AutomaticAggregationOptions AutomaticAggregationOptions
		{
			get
			{
				return this.automaticAggregationOptions.GetProperty(this, this.body.AutomaticAggregationOptions);
			}
			set
			{
				if (!this.automaticAggregationOptions.IsSamePropertyReference(value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						if (((ICustomProperty<Model, string>)value).Owner != null)
						{
							throw new ArgumentException(TomSR.Exception_CustomPropertyAssignedToMultipleObjects("AutomaticAggregationOptions"), "value");
						}
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_AutomaticAggregationOptions, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "AutomaticAggregationOptions"));
					}
					string text = this.body.AutomaticAggregationOptions;
					this.automaticAggregationOptions.ExtractMetadataValueIfNeeded(ref text, false);
					string text2 = ((value != null) ? ((ICustomProperty<Model, string>)value).Convert() : string.Empty);
					ObjectChangeTracker.RegisterPropertyChanging(this, "AutomaticAggregationOptions", typeof(string), text, text2);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_AutomaticAggregationOptions, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					this.body.AutomaticAggregationOptions = text2;
					this.automaticAggregationOptions.SetProperty(this, value, false);
					ObjectChangeTracker.RegisterPropertyChanged(this, "AutomaticAggregationOptions", typeof(string), text, text2);
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000374AB File Offset: 0x000356AB
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x000374B8 File Offset: 0x000356B8
		[CompatibilityRequirement("1566")]
		[Obsolete("Not used. Replaced by ValueFilterBehavior")]
		public int DisableAutoExists
		{
			get
			{
				return this.body.DisableAutoExists;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DisableAutoExists, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != -1)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DisableAutoExists, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DisableAutoExists"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DisableAutoExists", typeof(int), this.body.DisableAutoExists, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DisableAutoExists, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					int disableAutoExists = this.body.DisableAutoExists;
					this.body.DisableAutoExists = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DisableAutoExists", typeof(int), disableAutoExists, value);
				}
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0003757D File Offset: 0x0003577D
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0003758C File Offset: 0x0003578C
		[CompatibilityRequirement("1568")]
		public int MaxParallelismPerRefresh
		{
			get
			{
				return this.body.MaxParallelismPerRefresh;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MaxParallelismPerRefresh, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != -1)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_MaxParallelismPerRefresh, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MaxParallelismPerRefresh"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MaxParallelismPerRefresh", typeof(int), this.body.MaxParallelismPerRefresh, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_MaxParallelismPerRefresh, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					int maxParallelismPerRefresh = this.body.MaxParallelismPerRefresh;
					this.body.MaxParallelismPerRefresh = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MaxParallelismPerRefresh", typeof(int), maxParallelismPerRefresh, value);
				}
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00037651 File Offset: 0x00035851
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00037660 File Offset: 0x00035860
		[CompatibilityRequirement("1569")]
		public int MaxParallelismPerQuery
		{
			get
			{
				return this.body.MaxParallelismPerQuery;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MaxParallelismPerQuery, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != 0)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_MaxParallelismPerQuery, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MaxParallelismPerQuery"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MaxParallelismPerQuery", typeof(int), this.body.MaxParallelismPerQuery, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_MaxParallelismPerQuery, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					int maxParallelismPerQuery = this.body.MaxParallelismPerQuery;
					this.body.MaxParallelismPerQuery = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MaxParallelismPerQuery", typeof(int), maxParallelismPerQuery, value);
				}
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00037724 File Offset: 0x00035924
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00037734 File Offset: 0x00035934
		[CompatibilityRequirement("1604")]
		public DirectLakeBehavior DirectLakeBehavior
		{
			get
			{
				return this.body.DirectLakeBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DirectLakeBehavior, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Model_DirectLakeBehavior.Merge(PropertyHelper.GetDirectLakeBehaviorCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Model_DirectLakeBehavior.Merge(PropertyHelper.GetDirectLakeBehaviorCompatibilityRestrictions(this.body.DirectLakeBehavior));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != DirectLakeBehavior.Automatic))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "DirectLakeBehavior", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DirectLakeBehavior", typeof(DirectLakeBehavior), this.body.DirectLakeBehavior, value);
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
					DirectLakeBehavior directLakeBehavior = this.body.DirectLakeBehavior;
					this.body.DirectLakeBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DirectLakeBehavior", typeof(DirectLakeBehavior), directLakeBehavior, value);
				}
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00037869 File Offset: 0x00035A69
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00037878 File Offset: 0x00035A78
		[CompatibilityRequirement("1606")]
		public ValueFilterBehaviorType ValueFilterBehavior
		{
			get
			{
				return this.body.ValueFilterBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ValueFilterBehavior, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Model_ValueFilterBehavior.Merge(PropertyHelper.GetValueFilterBehaviorTypeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Model_ValueFilterBehavior.Merge(PropertyHelper.GetValueFilterBehaviorTypeCompatibilityRestrictions(this.body.ValueFilterBehavior));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ValueFilterBehaviorType.Automatic))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "ValueFilterBehavior", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ValueFilterBehavior", typeof(ValueFilterBehaviorType), this.body.ValueFilterBehavior, value);
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
					ValueFilterBehaviorType valueFilterBehavior = this.body.ValueFilterBehavior;
					this.body.ValueFilterBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ValueFilterBehavior", typeof(ValueFilterBehaviorType), valueFilterBehavior, value);
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000379AD File Offset: 0x00035BAD
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x000379C0 File Offset: 0x00035BC0
		[CompatibilityRequirement("1400")]
		public Measure DefaultMeasure
		{
			get
			{
				return this.body.DefaultMeasureID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultMeasureID.Object, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultMeasure"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultMeasure", typeof(Measure), this.body.DefaultMeasureID.Object, value);
					if (value != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Model_DefaultMeasure, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					Measure @object = this.body.DefaultMeasureID.Object;
					this.body.DefaultMeasureID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultMeasure", typeof(Measure), @object, value);
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00037A84 File Offset: 0x00035C84
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00037A96 File Offset: 0x00035C96
		internal ObjectId _DefaultMeasureID
		{
			get
			{
				return this.body.DefaultMeasureID.ObjectID;
			}
			set
			{
				this.body.DefaultMeasureID.ObjectID = value;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00037AAC File Offset: 0x00035CAC
		internal void CopyFrom(Model other, CopyContext context)
		{
			other.dataAccessOptions.ExtractMetadataValueIfNeeded(ref other.body.DataAccessOptions, true);
			other.automaticAggregationOptions.ExtractMetadataValueIfNeeded(ref other.body.AutomaticAggregationOptions, true);
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0 || this.body.StructureModifiedTime.CompareTo(other.body.StructureModifiedTime) != 0;
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
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Tables.CopyFrom(other.Tables, context);
				this.Relationships.CopyFrom(other.Relationships, context);
				this.DataSources.CopyFrom(other.DataSources, context);
				this.Perspectives.CopyFrom(other.Perspectives, context);
				this.Cultures.CopyFrom(other.Cultures, context);
				this.Roles.CopyFrom(other.Roles, context);
				this.Expressions.CopyFrom(other.Expressions, context);
				this.QueryGroups.CopyFrom(other.QueryGroups, context);
				this.AnalyticsAIMetadata.CopyFrom(other.AnalyticsAIMetadata, context);
				this.Functions.CopyFrom(other.Functions, context);
				this.BindingInfoCollection.CopyFrom(other.BindingInfoCollection, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ExcludedArtifacts.CopyFrom(other.ExcludedArtifacts, context);
			}
			this.dataAccessOptions.UpdateProperty(this.body.DataAccessOptions);
			this.automaticAggregationOptions.UpdateProperty(this.body.AutomaticAggregationOptions);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00037CC3 File Offset: 0x00035EC3
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Model)other, context);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00037CD2 File Offset: 0x00035ED2
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Model other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00037CEE File Offset: 0x00035EEE
		public void CopyTo(Model other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00037D0A File Offset: 0x00035F0A
		public Model Clone()
		{
			return base.CloneInternal<Model>();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00037D12 File Offset: 0x00035F12
		internal void BeforeBodyCompareWith()
		{
			this.dataAccessOptions.ExtractMetadataValueIfNeeded(ref this.body.DataAccessOptions, true);
			this.automaticAggregationOptions.ExtractMetadataValueIfNeeded(ref this.body.AutomaticAggregationOptions, true);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00037D42 File Offset: 0x00035F42
		internal override void OnAfterBodyReverted()
		{
			base.OnAfterBodyReverted();
			this.dataAccessOptions.UpdateProperty(this.body.DataAccessOptions);
			this.automaticAggregationOptions.UpdateProperty(this.body.AutomaticAggregationOptions);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00037D78 File Offset: 0x00035F78
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			this.body.DefaultMeasureID.Validate(null, true);
			if (this.body.DefaultMeasureID.Object != null)
			{
				if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMeasureID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "DefaultMeasureID", this.body.DefaultMeasureID.Object);
			}
			if (string.Compare(this.body.Name, "Model", StringComparison.Ordinal) != 0)
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.StorageLocation))
			{
				writer.WriteProperty<string>(options, "StorageLocation", this.body.StorageLocation);
			}
			if (this.body.DefaultMode != ModeType.Import)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.DefaultMode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<ModeType>(options, "DefaultMode", this.body.DefaultMode);
			}
			if (this.body.DefaultDataView != DataViewType.Full)
			{
				writer.WriteProperty<DataViewType>(options, "DefaultDataView", this.body.DefaultDataView);
			}
			if (!string.IsNullOrEmpty(this.body.Culture))
			{
				writer.WriteProperty<string>(options, "Culture", this.body.Culture);
			}
			if (!string.IsNullOrEmpty(this.body.Collation))
			{
				writer.WriteProperty<string>(options, "Collation", this.body.Collation);
			}
			if (!string.IsNullOrEmpty(this.body.DataAccessOptions) || this.dataAccessOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataAccessOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.dataAccessOptions.ExtractMetadataValueIfNeeded(ref this.body.DataAccessOptions, true);
				if (!string.IsNullOrEmpty(this.body.DataAccessOptions))
				{
					writer.WriteProperty<string>(options, "DataAccessOptions", this.body.DataAccessOptions);
				}
			}
			if (this.body.DefaultPowerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V1)
			{
				if (!CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsPowerBIDataSourceVersionValueCompatible(this.body.DefaultPowerBIDataSourceVersion, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultPowerBIDataSourceVersion is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<PowerBIDataSourceVersion>(options, "DefaultPowerBIDataSourceVersion", this.body.DefaultPowerBIDataSourceVersion);
			}
			if (this.body.ForceUniqueNames)
			{
				if (!CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ForceUniqueNames is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "ForceUniqueNames", this.body.ForceUniqueNames);
			}
			if (this.body.DiscourageImplicitMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageImplicitMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "DiscourageImplicitMeasures", this.body.DiscourageImplicitMeasures);
			}
			if (this.body.DiscourageReportMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageReportMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "DiscourageReportMeasures", this.body.DiscourageReportMeasures);
			}
			if (this.body.DataSourceVariablesOverrideBehavior != DataSourceVariablesOverrideBehaviorType.Disallow)
			{
				if (!CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsDataSourceVariablesOverrideBehaviorTypeValueCompatible(this.body.DataSourceVariablesOverrideBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceVariablesOverrideBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<DataSourceVariablesOverrideBehaviorType>(options, "DataSourceVariablesOverrideBehavior", this.body.DataSourceVariablesOverrideBehavior);
			}
			if (this.body.DataSourceDefaultMaxConnections != 10)
			{
				if (!CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceDefaultMaxConnections is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<int>(options, "DataSourceDefaultMaxConnections", this.body.DataSourceDefaultMaxConnections);
			}
			if (!string.IsNullOrEmpty(this.body.SourceQueryCulture))
			{
				if (!CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceQueryCulture is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceQueryCulture", this.body.SourceQueryCulture);
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "MAttributes", this.body.MAttributes);
			}
			if (this.body.DiscourageCompositeModels)
			{
				if (!CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageCompositeModels is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "DiscourageCompositeModels", this.body.DiscourageCompositeModels);
			}
			if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions) || this.automaticAggregationOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AutomaticAggregationOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.automaticAggregationOptions.ExtractMetadataValueIfNeeded(ref this.body.AutomaticAggregationOptions, true);
				if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions))
				{
					writer.WriteProperty<string>(options, "AutomaticAggregationOptions", this.body.AutomaticAggregationOptions);
				}
			}
			if (this.body.DisableAutoExists != -1)
			{
				if (!CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DisableAutoExists is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<int>(options, "DisableAutoExists", this.body.DisableAutoExists);
			}
			if (this.body.MaxParallelismPerRefresh != -1)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<int>(options, "MaxParallelismPerRefresh", this.body.MaxParallelismPerRefresh);
			}
			if (this.body.MaxParallelismPerQuery != 0)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerQuery is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<int>(options, "MaxParallelismPerQuery", this.body.MaxParallelismPerQuery);
			}
			if (this.body.DirectLakeBehavior != DirectLakeBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsDirectLakeBehaviorValueCompatible(this.body.DirectLakeBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DirectLakeBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<DirectLakeBehavior>(options, "DirectLakeBehavior", this.body.DirectLakeBehavior);
			}
			if (this.body.ValueFilterBehavior != ValueFilterBehaviorType.Automatic)
			{
				if (!CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsValueFilterBehaviorTypeValueCompatible(this.body.ValueFilterBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ValueFilterBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<ValueFilterBehaviorType>(options, "ValueFilterBehavior", this.body.ValueFilterBehavior);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000385A0 File Offset: 0x000367A0
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("DefaultMeasureID", out objectId))
			{
				this.body.DefaultMeasureID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Description", out text2))
			{
				this.body.Description = text2;
			}
			string text3;
			if (reader.TryReadProperty<string>("StorageLocation", out text3))
			{
				this.body.StorageLocation = text3;
			}
			ModeType modeType;
			if (reader.TryReadProperty<ModeType>("DefaultMode", out modeType))
			{
				this.body.DefaultMode = modeType;
			}
			DataViewType dataViewType;
			if (reader.TryReadProperty<DataViewType>("DefaultDataView", out dataViewType))
			{
				this.body.DefaultDataView = dataViewType;
			}
			string text4;
			if (reader.TryReadProperty<string>("Culture", out text4))
			{
				this.body.Culture = text4;
			}
			string text5;
			if (reader.TryReadProperty<string>("Collation", out text5))
			{
				this.body.Collation = text5;
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
			string text6;
			if (CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("DataAccessOptions", out text6))
			{
				this.body.DataAccessOptions = text6;
				this.dataAccessOptions.UpdateProperty(this.body.DataAccessOptions);
			}
			PowerBIDataSourceVersion powerBIDataSourceVersion;
			if (CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<PowerBIDataSourceVersion>("DefaultPowerBIDataSourceVersion", out powerBIDataSourceVersion))
			{
				this.body.DefaultPowerBIDataSourceVersion = powerBIDataSourceVersion;
			}
			bool flag;
			if (CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("ForceUniqueNames", out flag))
			{
				this.body.ForceUniqueNames = flag;
			}
			bool flag2;
			if (CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("DiscourageImplicitMeasures", out flag2))
			{
				this.body.DiscourageImplicitMeasures = flag2;
			}
			bool flag3;
			if (CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("DiscourageReportMeasures", out flag3))
			{
				this.body.DiscourageReportMeasures = flag3;
			}
			DataSourceVariablesOverrideBehaviorType dataSourceVariablesOverrideBehaviorType;
			if (CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<DataSourceVariablesOverrideBehaviorType>("DataSourceVariablesOverrideBehavior", out dataSourceVariablesOverrideBehaviorType))
			{
				this.body.DataSourceVariablesOverrideBehavior = dataSourceVariablesOverrideBehaviorType;
			}
			int num;
			if (CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<int>("DataSourceDefaultMaxConnections", out num))
			{
				this.body.DataSourceDefaultMaxConnections = num;
			}
			string text7;
			if (CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceQueryCulture", out text7))
			{
				this.body.SourceQueryCulture = text7;
			}
			string text8;
			if (CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("MAttributes", out text8))
			{
				this.body.MAttributes = text8;
			}
			bool flag4;
			if (CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("DiscourageCompositeModels", out flag4))
			{
				this.body.DiscourageCompositeModels = flag4;
			}
			string text9;
			if (CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("AutomaticAggregationOptions", out text9))
			{
				this.body.AutomaticAggregationOptions = text9;
				this.automaticAggregationOptions.UpdateProperty(this.body.AutomaticAggregationOptions);
			}
			int num2;
			if (CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<int>("DisableAutoExists", out num2))
			{
				this.body.DisableAutoExists = num2;
			}
			int num3;
			if (CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<int>("MaxParallelismPerRefresh", out num3))
			{
				this.body.MaxParallelismPerRefresh = num3;
			}
			int num4;
			if (CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<int>("MaxParallelismPerQuery", out num4))
			{
				this.body.MaxParallelismPerQuery = num4;
			}
			DirectLakeBehavior directLakeBehavior;
			if (CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<DirectLakeBehavior>("DirectLakeBehavior", out directLakeBehavior))
			{
				this.body.DirectLakeBehavior = directLakeBehavior;
			}
			ValueFilterBehaviorType valueFilterBehaviorType;
			if (CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ValueFilterBehaviorType>("ValueFilterBehavior", out valueFilterBehaviorType))
			{
				this.body.ValueFilterBehavior = valueFilterBehaviorType;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000389AC File Offset: 0x00036BAC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			this.body.DefaultMeasureID.Validate(null, true);
			if (this.body.DefaultMeasureID.Object != null)
			{
				if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMeasureID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DefaultMeasureID", MetadataPropertyNature.CrossLinkProperty))
				{
					writer.WriteObjectIdProperty("DefaultMeasureID", MetadataPropertyNature.CrossLinkProperty, this.body.DefaultMeasureID.Object);
				}
			}
			if (string.Compare(this.body.Name, "Model", StringComparison.Ordinal) != 0 && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.StorageLocation) && writer.ShouldIncludeProperty("StorageLocation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("StorageLocation", MetadataPropertyNature.RegularProperty, this.body.StorageLocation);
			}
			if (this.body.DefaultMode != ModeType.Import)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.DefaultMode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DefaultMode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("DefaultMode", MetadataPropertyNature.RegularProperty, this.body.DefaultMode);
				}
			}
			if (this.body.DefaultDataView != DataViewType.Full && writer.ShouldIncludeProperty("DefaultDataView", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataViewType>("DefaultDataView", MetadataPropertyNature.RegularProperty, this.body.DefaultDataView);
			}
			if (!string.IsNullOrEmpty(this.body.Culture) && writer.ShouldIncludeProperty("Culture", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("Culture", MetadataPropertyNature.RegularProperty, this.body.Culture);
			}
			if (!string.IsNullOrEmpty(this.body.Collation) && writer.ShouldIncludeProperty("Collation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("Collation", MetadataPropertyNature.RegularProperty, this.body.Collation);
			}
			if (!string.IsNullOrEmpty(this.body.DataAccessOptions) || this.dataAccessOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataAccessOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DataAccessOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					this.dataAccessOptions.ExtractMetadataValueIfNeeded(ref this.body.DataAccessOptions, true);
					if (!string.IsNullOrEmpty(this.body.DataAccessOptions))
					{
						writer.WriteStringProperty("DataAccessOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, this.body.DataAccessOptions);
					}
				}
			}
			if (this.body.DefaultPowerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V1)
			{
				if (!CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsPowerBIDataSourceVersionValueCompatible(this.body.DefaultPowerBIDataSourceVersion, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultPowerBIDataSourceVersion is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DefaultPowerBIDataSourceVersion", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<PowerBIDataSourceVersion>("DefaultPowerBIDataSourceVersion", MetadataPropertyNature.RegularProperty, this.body.DefaultPowerBIDataSourceVersion);
				}
			}
			if (this.body.ForceUniqueNames)
			{
				if (!CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ForceUniqueNames is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ForceUniqueNames", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("ForceUniqueNames", MetadataPropertyNature.RegularProperty, this.body.ForceUniqueNames);
				}
			}
			if (this.body.DiscourageImplicitMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageImplicitMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DiscourageImplicitMeasures", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("DiscourageImplicitMeasures", MetadataPropertyNature.RegularProperty, this.body.DiscourageImplicitMeasures);
				}
			}
			if (this.body.DiscourageReportMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageReportMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DiscourageReportMeasures", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("DiscourageReportMeasures", MetadataPropertyNature.RegularProperty, this.body.DiscourageReportMeasures);
				}
			}
			if (this.body.DataSourceVariablesOverrideBehavior != DataSourceVariablesOverrideBehaviorType.Disallow)
			{
				if (!CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsDataSourceVariablesOverrideBehaviorTypeValueCompatible(this.body.DataSourceVariablesOverrideBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceVariablesOverrideBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DataSourceVariablesOverrideBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DataSourceVariablesOverrideBehaviorType>("DataSourceVariablesOverrideBehavior", MetadataPropertyNature.RegularProperty, this.body.DataSourceVariablesOverrideBehavior);
				}
			}
			if (this.body.DataSourceDefaultMaxConnections != 10)
			{
				if (!CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceDefaultMaxConnections is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DataSourceDefaultMaxConnections", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("DataSourceDefaultMaxConnections", MetadataPropertyNature.RegularProperty, this.body.DataSourceDefaultMaxConnections);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceQueryCulture))
			{
				if (!CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceQueryCulture is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceQueryCulture", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceQueryCulture", MetadataPropertyNature.RegularProperty, this.body.SourceQueryCulture);
				}
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.Model_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("MAttributes", MetadataPropertyNature.RegularProperty, this.body.MAttributes);
				}
			}
			if (this.body.DiscourageCompositeModels)
			{
				if (!CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageCompositeModels is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DiscourageCompositeModels", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("DiscourageCompositeModels", MetadataPropertyNature.RegularProperty, this.body.DiscourageCompositeModels);
				}
			}
			if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions) || this.automaticAggregationOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AutomaticAggregationOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("AutomaticAggregationOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					this.automaticAggregationOptions.ExtractMetadataValueIfNeeded(ref this.body.AutomaticAggregationOptions, true);
					if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions))
					{
						writer.WriteStringProperty("AutomaticAggregationOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, this.body.AutomaticAggregationOptions);
					}
				}
			}
			if (this.body.DisableAutoExists != -1)
			{
				if (!CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DisableAutoExists is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DisableAutoExists", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("DisableAutoExists", MetadataPropertyNature.RegularProperty, this.body.DisableAutoExists);
				}
			}
			if (this.body.MaxParallelismPerRefresh != -1)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MaxParallelismPerRefresh", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("MaxParallelismPerRefresh", MetadataPropertyNature.RegularProperty, this.body.MaxParallelismPerRefresh);
				}
			}
			if (this.body.MaxParallelismPerQuery != 0)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerQuery is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MaxParallelismPerQuery", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("MaxParallelismPerQuery", MetadataPropertyNature.RegularProperty, this.body.MaxParallelismPerQuery);
				}
			}
			if (this.body.DirectLakeBehavior != DirectLakeBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsDirectLakeBehaviorValueCompatible(this.body.DirectLakeBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DirectLakeBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("DirectLakeBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DirectLakeBehavior>("DirectLakeBehavior", MetadataPropertyNature.RegularProperty, this.body.DirectLakeBehavior);
				}
			}
			if (this.body.ValueFilterBehavior != ValueFilterBehaviorType.Automatic)
			{
				if (!CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsValueFilterBehaviorTypeValueCompatible(this.body.ValueFilterBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ValueFilterBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ValueFilterBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ValueFilterBehaviorType>("ValueFilterBehavior", MetadataPropertyNature.RegularProperty, this.body.ValueFilterBehavior);
				}
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000394BC File Offset: 0x000376BC
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if ((string.Compare(this.body.Name, "Model", StringComparison.Ordinal) != 0 || (context.SerializationMode == MetadataSerializationMode.Tmdl && !string.IsNullOrEmpty(this.body.Name))) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.StorageLocation) && writer.ShouldIncludeProperty("storageLocation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("storageLocation", MetadataPropertyNature.RegularProperty, this.body.StorageLocation);
			}
			if (this.body.DefaultMode != ModeType.Import)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.DefaultMode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("defaultMode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("defaultMode", MetadataPropertyNature.RegularProperty, this.body.DefaultMode);
				}
			}
			if (this.body.DefaultDataView != DataViewType.Full && writer.ShouldIncludeProperty("defaultDataView", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataViewType>("defaultDataView", MetadataPropertyNature.RegularProperty, this.body.DefaultDataView);
			}
			if (!string.IsNullOrEmpty(this.body.Culture) && writer.ShouldIncludeProperty("culture", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("culture", MetadataPropertyNature.RegularProperty, this.body.Culture);
			}
			if (!string.IsNullOrEmpty(this.body.Collation) && writer.ShouldIncludeProperty("collation", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("collation", MetadataPropertyNature.RegularProperty, this.body.Collation);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (this.body.DefaultPowerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V1)
			{
				if (!CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsPowerBIDataSourceVersionValueCompatible(this.body.DefaultPowerBIDataSourceVersion, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultPowerBIDataSourceVersion is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("defaultPowerBIDataSourceVersion", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<PowerBIDataSourceVersion>("defaultPowerBIDataSourceVersion", MetadataPropertyNature.RegularProperty, this.body.DefaultPowerBIDataSourceVersion);
				}
			}
			if (this.body.ForceUniqueNames)
			{
				if (!CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ForceUniqueNames is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("forceUniqueNames", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("forceUniqueNames", MetadataPropertyNature.RegularProperty, this.body.ForceUniqueNames);
				}
			}
			if (this.body.DiscourageImplicitMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageImplicitMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("discourageImplicitMeasures", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("discourageImplicitMeasures", MetadataPropertyNature.RegularProperty, this.body.DiscourageImplicitMeasures);
				}
			}
			if (this.body.DiscourageReportMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageReportMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("discourageReportMeasures", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("discourageReportMeasures", MetadataPropertyNature.RegularProperty, this.body.DiscourageReportMeasures);
				}
			}
			if (this.body.DataSourceVariablesOverrideBehavior != DataSourceVariablesOverrideBehaviorType.Disallow)
			{
				if (!CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsDataSourceVariablesOverrideBehaviorTypeValueCompatible(this.body.DataSourceVariablesOverrideBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceVariablesOverrideBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("dataSourceVariablesOverrideBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DataSourceVariablesOverrideBehaviorType>("dataSourceVariablesOverrideBehavior", MetadataPropertyNature.RegularProperty, this.body.DataSourceVariablesOverrideBehavior);
				}
			}
			if (this.body.DataSourceDefaultMaxConnections != 10)
			{
				if (!CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceDefaultMaxConnections is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("dataSourceDefaultMaxConnections", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("dataSourceDefaultMaxConnections", MetadataPropertyNature.RegularProperty, this.body.DataSourceDefaultMaxConnections);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceQueryCulture))
			{
				if (!CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceQueryCulture is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceQueryCulture", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceQueryCulture", MetadataPropertyNature.RegularProperty, this.body.SourceQueryCulture);
				}
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.Model_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("mAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("mAttributes", MetadataPropertyNature.RegularProperty, this.body.MAttributes);
				}
			}
			if (this.body.DiscourageCompositeModels)
			{
				if (!CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageCompositeModels is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("discourageCompositeModels", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("discourageCompositeModels", MetadataPropertyNature.RegularProperty, this.body.DiscourageCompositeModels);
				}
			}
			if (this.body.DisableAutoExists != -1)
			{
				if (!CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DisableAutoExists is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("disableAutoExists", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("disableAutoExists", MetadataPropertyNature.RegularProperty, this.body.DisableAutoExists);
				}
			}
			if (this.body.MaxParallelismPerRefresh != -1)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("maxParallelismPerRefresh", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("maxParallelismPerRefresh", MetadataPropertyNature.RegularProperty, this.body.MaxParallelismPerRefresh);
				}
			}
			if (this.body.MaxParallelismPerQuery != 0)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerQuery is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("maxParallelismPerQuery", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("maxParallelismPerQuery", MetadataPropertyNature.RegularProperty, this.body.MaxParallelismPerQuery);
				}
			}
			if (this.body.DirectLakeBehavior != DirectLakeBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsDirectLakeBehaviorValueCompatible(this.body.DirectLakeBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DirectLakeBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("directLakeBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DirectLakeBehavior>("directLakeBehavior", MetadataPropertyNature.RegularProperty, this.body.DirectLakeBehavior);
				}
			}
			if (this.body.ValueFilterBehavior != ValueFilterBehaviorType.Automatic)
			{
				if (!CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsValueFilterBehaviorTypeValueCompatible(this.body.ValueFilterBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ValueFilterBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("valueFilterBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ValueFilterBehaviorType>("valueFilterBehavior", MetadataPropertyNature.RegularProperty, this.body.ValueFilterBehavior);
				}
			}
			if (this.body.DefaultMeasureID.Object != null)
			{
				if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMeasureID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("defaultMeasure", MetadataPropertyNature.CrossLinkProperty))
				{
					this.body.DefaultMeasureID.WriteToMetadataStream(ObjectType.Table, true, "defaultMeasure", false, writer);
				}
			}
			if (!string.IsNullOrEmpty(this.body.DataAccessOptions) || this.dataAccessOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataAccessOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("dataAccessOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					JToken json = this.DataAccessOptions.GetJson();
					if (json != null)
					{
						writer.WriteCustomJsonProperty("dataAccessOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, json);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions) || this.automaticAggregationOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AutomaticAggregationOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("automaticAggregationOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString))
				{
					JToken json2 = this.AutomaticAggregationOptions.GetJson();
					if (json2 != null)
					{
						writer.WriteCustomJsonProperty("automaticAggregationOptions", MetadataPropertyNature.NameProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.JsonString, json2);
					}
				}
			}
			if (this.DataSources.Count > 0 && writer.ShouldIncludeProperty("dataSources", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "dataSources", MetadataPropertyNature.ChildCollection, this.DataSources);
			}
			if (this.Tables.Count > 0 && writer.ShouldIncludeProperty("tables", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "tables", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Tables);
			}
			if (this.Relationships.Count > 0 && writer.ShouldIncludeProperty("relationships", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "relationships", MetadataPropertyNature.ChildCollection, this.Relationships);
			}
			if (this.Cultures.Count > 0 && writer.ShouldIncludeProperty("cultures", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "cultures", MetadataPropertyNature.ChildCollection, this.Cultures);
			}
			if (this.Perspectives.Count > 0 && writer.ShouldIncludeProperty("perspectives", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "perspectives", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Perspectives);
			}
			if (this.Roles.Count > 0 && writer.ShouldIncludeProperty("roles", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "roles", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Roles);
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
			if (this.Expressions.Count > 0)
			{
				if (!CompatibilityRestrictions.NamedExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("expressions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "expressions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Expressions);
				}
			}
			if (this.QueryGroups.Count > 0)
			{
				if (!CompatibilityRestrictions.QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child QueryGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("queryGroups", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "queryGroups", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.QueryGroups);
				}
			}
			if (this.AnalyticsAIMetadata.Count > 0)
			{
				if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child AnalyticsAIMetadata is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("analyticsAIMetadata", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "analyticsAIMetadata", MetadataPropertyNature.ChildCollection, this.AnalyticsAIMetadata);
				}
			}
			if (this.ExcludedArtifacts.Count > 0)
			{
				if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("excludedArtifacts", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "excludedArtifacts", MetadataPropertyNature.ChildCollection, this.ExcludedArtifacts);
				}
			}
			if (this.Functions.Count > 0)
			{
				if (!CompatibilityRestrictions.Function.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child Function is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("functions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "functions", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Functions);
				}
			}
			if (this.BindingInfoCollection.Count > 0)
			{
				if (!CompatibilityRestrictions.BindingInfo.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child BindingInfo is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("bindingInfoCollection", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "bindingInfoCollection", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.BindingInfoCollection);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0003A4B8 File Offset: 0x000386B8
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
					if (c != 'N')
					{
						if (c != 'n')
						{
							break;
						}
						if (!(propertyName == "name"))
						{
							break;
						}
					}
					else if (!(propertyName == "Name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
				}
				case 5:
					if (propertyName == "roles")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ModelRole modelRole in reader.ReadChildCollectionProperty<ModelRole>(context))
							{
								try
								{
									this.Roles.Add(modelRole);
								}
								catch (Exception ex)
								{
									throw reader.CreateInvalidChildException(context, modelRole, TomSR.Exception_FailedAddDeserializedNamedObject("ModelRole", (modelRole != null) ? modelRole.Name : null, ex.Message), ex);
								}
							}
						}
						return true;
					}
					break;
				case 6:
					if (propertyName == "tables")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Table table in reader.ReadChildCollectionProperty<Table>(context))
							{
								try
								{
									this.Tables.Add(table);
								}
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, table, TomSR.Exception_FailedAddDeserializedNamedObject("Table", (table != null) ? table.Name : null, ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					break;
				case 7:
				{
					char c = propertyName[0];
					if (c != 'C')
					{
						if (c != 'c')
						{
							break;
						}
						if (!(propertyName == "culture"))
						{
							break;
						}
					}
					else if (!(propertyName == "Culture"))
					{
						break;
					}
					this.body.Culture = reader.ReadStringProperty();
					return true;
				}
				case 8:
					if (propertyName == "cultures")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Culture culture in reader.ReadChildCollectionProperty<Culture>(context))
							{
								try
								{
									this.Cultures.Add(culture);
								}
								catch (Exception ex3)
								{
									throw reader.CreateInvalidChildException(context, culture, TomSR.Exception_FailedAddDeserializedNamedObject("Culture", (culture != null) ? culture.Name : null, ex3.Message), ex3);
								}
							}
						}
						return true;
					}
					break;
				case 9:
				{
					char c = propertyName[0];
					if (c != 'C')
					{
						if (c != 'c')
						{
							if (c != 'f')
							{
								break;
							}
							if (!(propertyName == "functions"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Function.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Function function in reader.ReadChildCollectionProperty<Function>(context))
								{
									try
									{
										this.Functions.Add(function);
									}
									catch (Exception ex4)
									{
										throw reader.CreateInvalidChildException(context, function, TomSR.Exception_FailedAddDeserializedNamedObject("Function", (function != null) ? function.Name : null, ex4.Message), ex4);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "collation"))
						{
							break;
						}
					}
					else if (!(propertyName == "Collation"))
					{
						break;
					}
					this.body.Collation = reader.ReadStringProperty();
					return true;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c <= 'M')
					{
						if (c != 'D')
						{
							if (c != 'M')
							{
								break;
							}
							if (!(propertyName == "MAttributes"))
							{
								break;
							}
							goto IL_0C8D;
						}
						else if (!(propertyName == "Description"))
						{
							if (!(propertyName == "DefaultMode"))
							{
								break;
							}
							goto IL_09CA;
						}
					}
					else
					{
						switch (c)
						{
						case 'a':
							if (!(propertyName == "annotations"))
							{
								goto IL_16FC;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
								{
									try
									{
										this.Annotations.Add(annotation);
									}
									catch (Exception ex5)
									{
										throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex5.Message), ex5);
									}
								}
							}
							return true;
						case 'b':
						case 'c':
							goto IL_16FC;
						case 'd':
							if (!(propertyName == "description"))
							{
								if (propertyName == "defaultMode")
								{
									goto IL_09CA;
								}
								if (!(propertyName == "dataSources"))
								{
									goto IL_16FC;
								}
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (DataSource dataSource in reader.ReadChildCollectionProperty<DataSource>(context))
									{
										try
										{
											this.DataSources.Add(dataSource);
										}
										catch (Exception ex6)
										{
											throw reader.CreateInvalidChildException(context, dataSource, TomSR.Exception_FailedAddDeserializedNamedObject("DataSource", (dataSource != null) ? dataSource.Name : null, ex6.Message), ex6);
										}
									}
								}
								return true;
							}
							break;
						case 'e':
							if (!(propertyName == "expressions"))
							{
								goto IL_16FC;
							}
							if (!CompatibilityRestrictions.NamedExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (NamedExpression namedExpression in reader.ReadChildCollectionProperty<NamedExpression>(context))
								{
									try
									{
										this.Expressions.Add(namedExpression);
									}
									catch (Exception ex7)
									{
										throw reader.CreateInvalidChildException(context, namedExpression, TomSR.Exception_FailedAddDeserializedNamedObject("NamedExpression", (namedExpression != null) ? namedExpression.Name : null, ex7.Message), ex7);
									}
								}
							}
							return true;
						default:
							if (c != 'm')
							{
								if (c != 'q')
								{
									goto IL_16FC;
								}
								if (!(propertyName == "queryGroups"))
								{
									goto IL_16FC;
								}
								if (!CompatibilityRestrictions.QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (QueryGroup queryGroup in reader.ReadChildCollectionProperty<QueryGroup>(context))
									{
										try
										{
											this.QueryGroups.Add(queryGroup);
										}
										catch (Exception ex8)
										{
											throw reader.CreateInvalidChildException(context, queryGroup, TomSR.Exception_FailedAddDeserializedNamedObject("QueryGroup", (queryGroup != null) ? queryGroup.Name : null, ex8.Message), ex8);
										}
									}
								}
								return true;
							}
							else
							{
								if (!(propertyName == "mAttributes"))
								{
									goto IL_16FC;
								}
								goto IL_0C8D;
							}
							break;
						}
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
					IL_09CA:
					ModeType modeType = reader.ReadEnumProperty<ModeType>();
					if (!PropertyHelper.IsModeTypeValueCompatible(modeType, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.DefaultMode = modeType;
					return true;
					IL_0C8D:
					if (!CompatibilityRestrictions.Model_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.MAttributes = reader.ReadStringProperty();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							if (c != 'p')
							{
								break;
							}
							if (!(propertyName == "perspectives"))
							{
								break;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Perspective perspective in reader.ReadChildCollectionProperty<Perspective>(context))
								{
									try
									{
										this.Perspectives.Add(perspective);
									}
									catch (Exception ex9)
									{
										throw reader.CreateInvalidChildException(context, perspective, TomSR.Exception_FailedAddDeserializedNamedObject("Perspective", (perspective != null) ? perspective.Name : null, ex9.Message), ex9);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "modifiedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "ModifiedTime"))
					{
						break;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 13:
					if (propertyName == "relationships")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Relationship relationship in reader.ReadChildCollectionProperty<Relationship>(context))
							{
								try
								{
									this.Relationships.Add(relationship);
								}
								catch (Exception ex10)
								{
									throw reader.CreateInvalidChildException(context, relationship, TomSR.Exception_FailedAddDeserializedNamedObject("Relationship", (relationship != null) ? relationship.Name : null, ex10.Message), ex10);
								}
							}
						}
						return true;
					}
					break;
				case 14:
					if (propertyName == "defaultMeasure")
					{
						if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.DefaultMeasureID.Path = reader.ReadCrossLinkProperty();
						return true;
					}
					break;
				case 15:
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
							if (!(propertyName == "StorageLocation"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "DefaultDataView"))
							{
								break;
							}
							goto IL_09F8;
						}
					}
					else if (c != 'd')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "storageLocation"))
						{
							break;
						}
					}
					else
					{
						if (!(propertyName == "defaultDataView"))
						{
							break;
						}
						goto IL_09F8;
					}
					this.body.StorageLocation = reader.ReadStringProperty();
					return true;
					IL_09F8:
					this.body.DefaultDataView = reader.ReadEnumProperty<DataViewType>();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'F')
						{
							if (c != 'f')
							{
								break;
							}
							if (!(propertyName == "forceUniqueNames"))
							{
								break;
							}
						}
						else if (!(propertyName == "ForceUniqueNames"))
						{
							break;
						}
						if (!CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.ForceUniqueNames = reader.ReadBooleanProperty();
						return true;
					}
					else if (propertyName == "DefaultMeasureID")
					{
						if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.DefaultMeasureID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 17:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							if (c != 'e')
							{
								break;
							}
							if (!(propertyName == "excludedArtifacts"))
							{
								break;
							}
							if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (ExcludedArtifact excludedArtifact in reader.ReadChildCollectionProperty<ExcludedArtifact>(context))
								{
									try
									{
										this.ExcludedArtifacts.Add(excludedArtifact);
									}
									catch (Exception ex11)
									{
										throw reader.CreateInvalidChildException(context, excludedArtifact, TomSR.Exception_FailedAddDeserializedObject("ExcludedArtifact", ex11.Message), ex11);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "dataAccessOptions"))
						{
							if (!(propertyName == "disableAutoExists"))
							{
								break;
							}
							goto IL_0D88;
						}
					}
					else if (!(propertyName == "DataAccessOptions"))
					{
						if (!(propertyName == "DisableAutoExists"))
						{
							break;
						}
						goto IL_0D88;
					}
					if (!CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					JToken jtoken;
					if (context.SerializationMode != MetadataSerializationMode.Xmla && reader.TryReadCustomJsonProperty(out jtoken))
					{
						if (!this.dataAccessOptions.TryUpdatePropertyFromJson(jtoken, out this.body.DataAccessOptions))
						{
							this.body.DataAccessOptions = JsonPropertyHelper.ConvertJsonContentToString(jtoken);
							this.dataAccessOptions.UpdateProperty(this.body.DataAccessOptions);
						}
					}
					else
					{
						this.body.DataAccessOptions = reader.ReadStringProperty();
						this.dataAccessOptions.UpdateProperty(this.body.DataAccessOptions);
					}
					return true;
					IL_0D88:
					if (!CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DisableAutoExists = reader.ReadInt32Property();
					return true;
				}
				case 18:
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
							if (!(propertyName == "SourceQueryCulture"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "DirectLakeBehavior"))
							{
								break;
							}
							goto IL_0E18;
						}
					}
					else if (c != 'd')
					{
						if (c != 'e')
						{
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "sourceQueryCulture"))
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
									catch (Exception ex12)
									{
										throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex12.Message), ex12);
									}
								}
							}
							return true;
						}
					}
					else
					{
						if (!(propertyName == "directLakeBehavior"))
						{
							break;
						}
						goto IL_0E18;
					}
					if (!CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.SourceQueryCulture = reader.ReadStringProperty();
					return true;
					IL_0E18:
					if (!CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.DirectLakeBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DirectLakeBehavior = reader.ReadEnumProperty<DirectLakeBehavior>();
					return true;
				}
				case 19:
				{
					char c = propertyName[0];
					if (c != 'V')
					{
						if (c != 'a')
						{
							if (c != 'v')
							{
								break;
							}
							if (!(propertyName == "valueFilterBehavior"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "analyticsAIMetadata"))
							{
								break;
							}
							if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (AnalyticsAIMetadata analyticsAIMetadata in reader.ReadChildCollectionProperty<AnalyticsAIMetadata>(context))
								{
									try
									{
										this.AnalyticsAIMetadata.Add(analyticsAIMetadata);
									}
									catch (Exception ex13)
									{
										throw reader.CreateInvalidChildException(context, analyticsAIMetadata, TomSR.Exception_FailedAddDeserializedNamedObject("AnalyticsAIMetadata", (analyticsAIMetadata != null) ? analyticsAIMetadata.Name : null, ex13.Message), ex13);
									}
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "ValueFilterBehavior"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.ValueFilterBehaviorType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.ValueFilterBehavior = reader.ReadEnumProperty<ValueFilterBehaviorType>();
					return true;
				}
				case 21:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 'b')
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
						else
						{
							if (!(propertyName == "bindingInfoCollection"))
							{
								break;
							}
							if (!CompatibilityRestrictions.BindingInfo.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (BindingInfo bindingInfo in reader.ReadChildCollectionProperty<BindingInfo>(context))
								{
									try
									{
										this.BindingInfoCollection.Add(bindingInfo);
									}
									catch (Exception ex14)
									{
										throw reader.CreateInvalidChildException(context, bindingInfo, TomSR.Exception_FailedAddDeserializedNamedObject("BindingInfo", (bindingInfo != null) ? bindingInfo.Name : null, ex14.Message), ex14);
									}
								}
							}
							return true;
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
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "maxParallelismPerQuery"))
						{
							break;
						}
					}
					else if (!(propertyName == "MaxParallelismPerQuery"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.MaxParallelismPerQuery = reader.ReadInt32Property();
					return true;
				}
				case 24:
				{
					char c = propertyName[0];
					if (c <= 'M')
					{
						if (c != 'D')
						{
							if (c != 'M')
							{
								break;
							}
							if (!(propertyName == "MaxParallelismPerRefresh"))
							{
								break;
							}
							goto IL_0DB8;
						}
						else if (!(propertyName == "DiscourageReportMeasures"))
						{
							break;
						}
					}
					else if (c != 'd')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "maxParallelismPerRefresh"))
						{
							break;
						}
						goto IL_0DB8;
					}
					else if (!(propertyName == "discourageReportMeasures"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DiscourageReportMeasures = reader.ReadBooleanProperty();
					return true;
					IL_0DB8:
					if (!CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.MaxParallelismPerRefresh = reader.ReadInt32Property();
					return true;
				}
				case 25:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "discourageCompositeModels"))
						{
							break;
						}
					}
					else if (!(propertyName == "DiscourageCompositeModels"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DiscourageCompositeModels = reader.ReadBooleanProperty();
					return true;
				}
				case 26:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "discourageImplicitMeasures"))
						{
							break;
						}
					}
					else if (!(propertyName == "DiscourageImplicitMeasures"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DiscourageImplicitMeasures = reader.ReadBooleanProperty();
					return true;
				}
				case 27:
				{
					char c = propertyName[0];
					if (c != 'A')
					{
						if (c != 'a')
						{
							break;
						}
						if (!(propertyName == "automaticAggregationOptions"))
						{
							break;
						}
					}
					else if (!(propertyName == "AutomaticAggregationOptions"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					JToken jtoken2;
					if (context.SerializationMode != MetadataSerializationMode.Xmla && reader.TryReadCustomJsonProperty(out jtoken2))
					{
						if (!this.automaticAggregationOptions.TryUpdatePropertyFromJson(jtoken2, out this.body.AutomaticAggregationOptions))
						{
							this.body.AutomaticAggregationOptions = JsonPropertyHelper.ConvertJsonContentToString(jtoken2);
							this.automaticAggregationOptions.UpdateProperty(this.body.AutomaticAggregationOptions);
						}
					}
					else
					{
						this.body.AutomaticAggregationOptions = reader.ReadStringProperty();
						this.automaticAggregationOptions.UpdateProperty(this.body.AutomaticAggregationOptions);
					}
					return true;
				}
				case 31:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "defaultPowerBIDataSourceVersion"))
						{
							if (!(propertyName == "dataSourceDefaultMaxConnections"))
							{
								break;
							}
							goto IL_0C2D;
						}
					}
					else if (!(propertyName == "DefaultPowerBIDataSourceVersion"))
					{
						if (!(propertyName == "DataSourceDefaultMaxConnections"))
						{
							break;
						}
						goto IL_0C2D;
					}
					if (!CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.PowerBIDataSourceVersion.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					PowerBIDataSourceVersion powerBIDataSourceVersion = reader.ReadEnumProperty<PowerBIDataSourceVersion>();
					if (!PropertyHelper.IsPowerBIDataSourceVersionValueCompatible(powerBIDataSourceVersion, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.DefaultPowerBIDataSourceVersion = powerBIDataSourceVersion;
					return true;
					IL_0C2D:
					if (!CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DataSourceDefaultMaxConnections = reader.ReadInt32Property();
					return true;
				}
				case 35:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "dataSourceVariablesOverrideBehavior"))
						{
							break;
						}
					}
					else if (!(propertyName == "DataSourceVariablesOverrideBehavior"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.DataSourceVariablesOverrideBehaviorType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.DataSourceVariablesOverrideBehavior = reader.ReadEnumProperty<DataSourceVariablesOverrideBehaviorType>();
					return true;
				}
				}
			}
			IL_16FC:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0003BFBC File Offset: 0x0003A1BC
		[Obsolete("Deprecated. Use RequestRefresh method instead.", false)]
		public void Refresh(RefreshType type)
		{
			this.RequestRefresh(type);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0003BFC5 File Offset: 0x0003A1C5
		[Obsolete("Deprecated. Use RequestRefresh method instead.", false)]
		public void Refresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			this.RequestRefresh(type, overrides);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
		public void RequestRefresh(RefreshType type)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null, Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0003C004 File Offset: 0x0003A204
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides, Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0003C03C File Offset: 0x0003A23C
		public void RequestRefresh(RefreshType type, RefreshPolicyBehavior behavior)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null, behavior != RefreshPolicyBehavior.Ignore && Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0003C078 File Offset: 0x0003A278
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides, RefreshPolicyBehavior behavior)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides, behavior != RefreshPolicyBehavior.Ignore && Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0003C0B4 File Offset: 0x0003A2B4
		public void RequestRefresh(RefreshType type, DateTime effectiveDate)
		{
			if (!Utils.CanApplyRefreshPolicies(type))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null, true, new DateTime?(effectiveDate), true);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0003C0E6 File Offset: 0x0003A2E6
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides, DateTime effectiveDate)
		{
			if (!Utils.CanApplyRefreshPolicies(type))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides, true, new DateTime?(effectiveDate), true);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0003C11B File Offset: 0x0003A31B
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0003C124 File Offset: 0x0003A324
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0003C148 File Offset: 0x0003A348
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (string.Compare(this.body.Name, "Model", StringComparison.Ordinal) != 0)
			{
				result["name", TomPropCategory.Name, 1, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.StorageLocation))
			{
				result["storageLocation", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.StorageLocation, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DefaultMode != ModeType.Import)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.DefaultMode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["defaultMode", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ModeType>(this.body.DefaultMode);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DefaultDataView != DataViewType.Full)
			{
				result["defaultDataView", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DataViewType>(this.body.DefaultDataView);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Culture))
			{
				result["culture", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Culture, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Collation))
			{
				result["collation", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Collation, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 9, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if ((!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.DataAccessOptions)) || this.dataAccessOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataAccessOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.dataAccessOptions.ExtractMetadataValueIfNeeded(ref this.body.DataAccessOptions, true);
				if (!string.IsNullOrEmpty(this.body.DataAccessOptions))
				{
					result["dataAccessOptions", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.DataAccessOptions, "DataAccessOptions");
				}
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DefaultPowerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V1)
			{
				if (!CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsPowerBIDataSourceVersionValueCompatible(this.body.DefaultPowerBIDataSourceVersion, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultPowerBIDataSourceVersion is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["defaultPowerBIDataSourceVersion", TomPropCategory.Regular, 13, false] = JsonPropertyHelper.ConvertEnumToJsonValue<PowerBIDataSourceVersion>(this.body.DefaultPowerBIDataSourceVersion);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ForceUniqueNames)
			{
				if (!CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ForceUniqueNames is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["forceUniqueNames", TomPropCategory.Regular, 14, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.ForceUniqueNames);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DiscourageImplicitMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageImplicitMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["discourageImplicitMeasures", TomPropCategory.Regular, 15, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.DiscourageImplicitMeasures);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DiscourageReportMeasures)
			{
				if (!CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageReportMeasures is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["discourageReportMeasures", TomPropCategory.Regular, 16, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.DiscourageReportMeasures);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DataSourceVariablesOverrideBehavior != DataSourceVariablesOverrideBehaviorType.Disallow)
			{
				if (!CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsDataSourceVariablesOverrideBehaviorTypeValueCompatible(this.body.DataSourceVariablesOverrideBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceVariablesOverrideBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["dataSourceVariablesOverrideBehavior", TomPropCategory.Regular, 17, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DataSourceVariablesOverrideBehaviorType>(this.body.DataSourceVariablesOverrideBehavior);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DataSourceDefaultMaxConnections != 10)
			{
				if (!CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataSourceDefaultMaxConnections is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["dataSourceDefaultMaxConnections", TomPropCategory.Regular, 18, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.DataSourceDefaultMaxConnections);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceQueryCulture))
			{
				if (!CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceQueryCulture is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceQueryCulture", TomPropCategory.Regular, 19, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceQueryCulture, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["mAttributes", TomPropCategory.Regular, 20, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.MAttributes, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DiscourageCompositeModels)
			{
				if (!CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DiscourageCompositeModels is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["discourageCompositeModels", TomPropCategory.Regular, 21, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.DiscourageCompositeModels);
			}
			if ((!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.AutomaticAggregationOptions)) || this.automaticAggregationOptions.IsDirty)
			{
				if (!CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AutomaticAggregationOptions is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.automaticAggregationOptions.ExtractMetadataValueIfNeeded(ref this.body.AutomaticAggregationOptions, true);
				if (!string.IsNullOrEmpty(this.body.AutomaticAggregationOptions))
				{
					result["automaticAggregationOptions", TomPropCategory.Regular, 22, false] = JsonPropertyHelper.ConvertStringToJsonObject(this.body.AutomaticAggregationOptions, "AutomaticAggregationOptions");
				}
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DisableAutoExists != -1)
			{
				if (!CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DisableAutoExists is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["disableAutoExists", TomPropCategory.Regular, 23, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.DisableAutoExists);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.MaxParallelismPerRefresh != -1)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["maxParallelismPerRefresh", TomPropCategory.Regular, 24, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.MaxParallelismPerRefresh);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.MaxParallelismPerQuery != 0)
			{
				if (!CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MaxParallelismPerQuery is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["maxParallelismPerQuery", TomPropCategory.Regular, 25, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.MaxParallelismPerQuery);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DirectLakeBehavior != DirectLakeBehavior.Automatic)
			{
				if (!CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsDirectLakeBehaviorValueCompatible(this.body.DirectLakeBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DirectLakeBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["directLakeBehavior", TomPropCategory.Regular, 27, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DirectLakeBehavior>(this.body.DirectLakeBehavior);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ValueFilterBehavior != ValueFilterBehaviorType.Automatic)
			{
				if (!CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsValueFilterBehaviorTypeValueCompatible(this.body.ValueFilterBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ValueFilterBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["valueFilterBehavior", TomPropCategory.Regular, 28, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ValueFilterBehaviorType>(this.body.ValueFilterBehavior);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DefaultMeasureID.Object != null)
			{
				if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultMeasureID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.body.DefaultMeasureID.SerializeToJsonObject(true, "defaultMeasure", ObjectType.Table, result, 12, false);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				IEnumerable<Table> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Table> tables = this.Tables;
					enumerable = tables;
				}
				else
				{
					enumerable = this.Tables.Where((Table o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Table> enumerable2 = enumerable;
				if (enumerable2.Any<Table>())
				{
					object[] array = enumerable2.Select((Table obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["tables", TomPropCategory.ChildCollection, 2, false] = array2;
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<Relationship> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<Relationship> relationships = this.Relationships;
						enumerable3 = relationships;
					}
					else
					{
						enumerable3 = this.Relationships.Where((Relationship o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<Relationship> enumerable4 = enumerable3;
					if (enumerable4.Any<Relationship>())
					{
						object[] array = enumerable4.Select((Relationship obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["relationships", TomPropCategory.ChildCollection, 6, false] = array3;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<DataSource> enumerable5;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<DataSource> dataSources = this.DataSources;
						enumerable5 = dataSources;
					}
					else
					{
						enumerable5 = this.DataSources.Where((DataSource o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<DataSource> enumerable6 = enumerable5;
					if (enumerable6.Any<DataSource>())
					{
						object[] array = enumerable6.Select((DataSource obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array4 = array;
						result["dataSources", TomPropCategory.ChildCollection, 1, false] = array4;
					}
				}
				IEnumerable<Perspective> enumerable7;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Perspective> perspectives = this.Perspectives;
					enumerable7 = perspectives;
				}
				else
				{
					enumerable7 = this.Perspectives.Where((Perspective o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Perspective> enumerable8 = enumerable7;
				if (enumerable8.Any<Perspective>())
				{
					object[] array = enumerable8.Select((Perspective obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array5 = array;
					result["perspectives", TomPropCategory.ChildCollection, 28, false] = array5;
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<Culture> enumerable9;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<Culture> cultures = this.Cultures;
						enumerable9 = cultures;
					}
					else
					{
						enumerable9 = this.Cultures.Where((Culture o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<Culture> enumerable10 = enumerable9;
					if (enumerable10.Any<Culture>())
					{
						object[] array = enumerable10.Select((Culture obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array6 = array;
						result["cultures", TomPropCategory.ChildCollection, 12, false] = array6;
					}
				}
				IEnumerable<ModelRole> enumerable11;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<ModelRole> roles = this.Roles;
					enumerable11 = roles;
				}
				else
				{
					enumerable11 = this.Roles.Where((ModelRole o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<ModelRole> enumerable12 = enumerable11;
				if (enumerable12.Any<ModelRole>())
				{
					object[] array = enumerable12.Select((ModelRole obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array7 = array;
					result["roles", TomPropCategory.ChildCollection, 33, false] = array7;
				}
				IEnumerable<NamedExpression> enumerable13;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<NamedExpression> expressions = this.Expressions;
					enumerable13 = expressions;
				}
				else
				{
					enumerable13 = this.Expressions.Where((NamedExpression o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<NamedExpression> enumerable14 = enumerable13;
				if (enumerable14.Any<NamedExpression>())
				{
					if (!CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable14.Select((NamedExpression obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array8 = array;
					result["expressions", TomPropCategory.ChildCollection, 40, false] = array8;
				}
				IEnumerable<QueryGroup> enumerable15;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<QueryGroup> queryGroups = this.QueryGroups;
					enumerable15 = queryGroups;
				}
				else
				{
					enumerable15 = this.QueryGroups.Where((QueryGroup o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<QueryGroup> enumerable16 = enumerable15;
				if (enumerable16.Any<QueryGroup>())
				{
					if (!CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child QueryGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable16.Select((QueryGroup obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array9 = array;
					result["queryGroups", TomPropCategory.ChildCollection, 50, false] = array9;
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<AnalyticsAIMetadata> enumerable17;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<AnalyticsAIMetadata> analyticsAIMetadata = this.AnalyticsAIMetadata;
						enumerable17 = analyticsAIMetadata;
					}
					else
					{
						enumerable17 = this.AnalyticsAIMetadata.Where((AnalyticsAIMetadata o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<AnalyticsAIMetadata> enumerable18 = enumerable17;
					if (enumerable18.Any<AnalyticsAIMetadata>())
					{
						if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child AnalyticsAIMetadata is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable18.Select((AnalyticsAIMetadata obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array10 = array;
						result["analyticsAIMetadata", TomPropCategory.ChildCollection, 51, false] = array10;
					}
				}
				IEnumerable<Function> enumerable19;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Function> functions = this.Functions;
					enumerable19 = functions;
				}
				else
				{
					enumerable19 = this.Functions.Where((Function o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Function> enumerable20 = enumerable19;
				if (enumerable20.Any<Function>())
				{
					if (!CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child Function is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable20.Select((Function obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array11 = array;
					result["functions", TomPropCategory.ChildCollection, 62, false] = array11;
				}
				IEnumerable<BindingInfo> enumerable21;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<BindingInfo> bindingInfoCollection = this.BindingInfoCollection;
					enumerable21 = bindingInfoCollection;
				}
				else
				{
					enumerable21 = this.BindingInfoCollection.Where((BindingInfo o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<BindingInfo> enumerable22 = enumerable21;
				if (enumerable22.Any<BindingInfo>())
				{
					if (!CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child BindingInfo is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable22.Select((BindingInfo obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array12 = array;
					result["bindingInfoCollection", TomPropCategory.ChildCollection, 63, false] = array12;
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable23;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable23 = extendedProperties;
					}
					else
					{
						enumerable23 = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable24 = enumerable23;
					if (enumerable24.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable24.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array13 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array13;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExcludedArtifact> enumerable25;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExcludedArtifact> excludedArtifacts = this.ExcludedArtifacts;
						enumerable25 = excludedArtifacts;
					}
					else
					{
						enumerable25 = this.ExcludedArtifacts.Where((ExcludedArtifact o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExcludedArtifact> enumerable26 = enumerable25;
					if (enumerable26.Any<ExcludedArtifact>())
					{
						if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable26.Select((ExcludedArtifact obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array14 = array;
						result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array14;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array15 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array15;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 4:
					if (name == "name")
					{
						this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 5:
					if (name == "roles")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Roles, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 6:
					if (name == "tables")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Tables, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 7:
					if (name == "culture")
					{
						this.body.Culture = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 8:
					if (name == "cultures")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Cultures, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 9:
				{
					char c = name[0];
					if (c != 'c')
					{
						if (c == 'f')
						{
							if (name == "functions")
							{
								if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								JsonPropertyHelper.ReadObjectCollection(this.Functions, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
					}
					else if (name == "collation")
					{
						this.body.Collation = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 11:
				{
					char c = name[1];
					if (c <= 'e')
					{
						if (c != 'A')
						{
							if (c != 'a')
							{
								if (c == 'e')
								{
									if (name == "description")
									{
										this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
										return true;
									}
									if (name == "defaultMode")
									{
										ModeType modeType = JsonPropertyHelper.ConvertJsonValueToEnum<ModeType>(jsonProp.Value);
										if (jsonProp.Value.Type != 10 && !PropertyHelper.IsModeTypeValueCompatible(modeType, mode, dbCompatibilityLevel))
										{
											return false;
										}
										this.body.DefaultMode = modeType;
										return true;
									}
								}
							}
							else if (name == "dataSources")
							{
								JsonPropertyHelper.ReadObjectCollection(this.DataSources, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
						else if (name == "mAttributes")
						{
							if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.MAttributes = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
					}
					else if (c != 'n')
					{
						if (c != 'u')
						{
							if (c == 'x')
							{
								if (name == "expressions")
								{
									if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
									{
										return false;
									}
									JsonPropertyHelper.ReadObjectCollection(this.Expressions, jsonProp.Value, options, mode, dbCompatibilityLevel);
									return true;
								}
							}
						}
						else if (name == "queryGroups")
						{
							if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							JsonPropertyHelper.ReadObjectCollection(this.QueryGroups, jsonProp.Value, options, mode, dbCompatibilityLevel);
							return true;
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
					char c = name[0];
					if (c != 'm')
					{
						if (c == 'p')
						{
							if (name == "perspectives")
							{
								JsonPropertyHelper.ReadObjectCollection(this.Perspectives, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
					}
					else if (name == "modifiedTime")
					{
						this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 13:
					if (name == "relationships")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Relationships, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 14:
					if (name == "defaultMeasure")
					{
						if (!CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.DefaultMeasureID.Path = ObjectPath.Parse((JObject)jsonProp.Value);
						return true;
					}
					break;
				case 15:
				{
					char c = name[0];
					if (c != 'd')
					{
						if (c == 's')
						{
							if (name == "storageLocation")
							{
								this.body.StorageLocation = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "defaultDataView")
					{
						this.body.DefaultDataView = JsonPropertyHelper.ConvertJsonValueToEnum<DataViewType>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 16:
					if (name == "forceUniqueNames")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.ForceUniqueNames = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 17:
				{
					char c = name[1];
					if (c != 'a')
					{
						if (c != 'i')
						{
							if (c == 'x')
							{
								if (name == "excludedArtifacts")
								{
									if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
									{
										return false;
									}
									JsonPropertyHelper.ReadObjectCollection(this.ExcludedArtifacts, jsonProp.Value, options, mode, dbCompatibilityLevel);
									return true;
								}
							}
						}
						else if (name == "disableAutoExists")
						{
							if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.DisableAutoExists = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
							return true;
						}
					}
					else if (name == "dataAccessOptions")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						if (!this.dataAccessOptions.TryUpdatePropertyFromJson(jsonProp.Value, out this.body.DataAccessOptions))
						{
							this.body.DataAccessOptions = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
							this.dataAccessOptions.UpdateProperty(this.body.DataAccessOptions);
						}
						return true;
					}
					break;
				}
				case 18:
				{
					char c = name[0];
					if (c != 'd')
					{
						if (c != 'e')
						{
							if (c == 's')
							{
								if (name == "sourceQueryCulture")
								{
									if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel))
									{
										return false;
									}
									this.body.SourceQueryCulture = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
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
					else if (name == "directLakeBehavior")
					{
						DirectLakeBehavior directLakeBehavior = JsonPropertyHelper.ConvertJsonValueToEnum<DirectLakeBehavior>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsDirectLakeBehaviorValueCompatible(directLakeBehavior, mode, dbCompatibilityLevel)))
						{
							return false;
						}
						this.body.DirectLakeBehavior = directLakeBehavior;
						return true;
					}
					break;
				}
				case 19:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c == 'v')
						{
							if (name == "valueFilterBehavior")
							{
								ValueFilterBehaviorType valueFilterBehaviorType = JsonPropertyHelper.ConvertJsonValueToEnum<ValueFilterBehaviorType>(jsonProp.Value);
								if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsValueFilterBehaviorTypeValueCompatible(valueFilterBehaviorType, mode, dbCompatibilityLevel)))
								{
									return false;
								}
								this.body.ValueFilterBehavior = valueFilterBehaviorType;
								return true;
							}
						}
					}
					else if (name == "analyticsAIMetadata")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.AnalyticsAIMetadata, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 21:
				{
					char c = name[0];
					if (c != 'b')
					{
						if (c == 's')
						{
							if (name == "structureModifiedTime")
							{
								this.body.StructureModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "bindingInfoCollection")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.BindingInfoCollection, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 22:
					if (name == "maxParallelismPerQuery")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.MaxParallelismPerQuery = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 24:
				{
					char c = name[0];
					if (c != 'd')
					{
						if (c == 'm')
						{
							if (name == "maxParallelismPerRefresh")
							{
								if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.MaxParallelismPerRefresh = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "discourageReportMeasures")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.DiscourageReportMeasures = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 25:
					if (name == "discourageCompositeModels")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.DiscourageCompositeModels = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 26:
					if (name == "discourageImplicitMeasures")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.DiscourageImplicitMeasures = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 27:
					if (name == "automaticAggregationOptions")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						if (!this.automaticAggregationOptions.TryUpdatePropertyFromJson(jsonProp.Value, out this.body.AutomaticAggregationOptions))
						{
							this.body.AutomaticAggregationOptions = JsonPropertyHelper.ConvertJsonContentToString(jsonProp.Value);
							this.automaticAggregationOptions.UpdateProperty(this.body.AutomaticAggregationOptions);
						}
						return true;
					}
					break;
				case 31:
				{
					char c = name[1];
					if (c != 'a')
					{
						if (c == 'e')
						{
							if (name == "defaultPowerBIDataSourceVersion")
							{
								PowerBIDataSourceVersion powerBIDataSourceVersion = JsonPropertyHelper.ConvertJsonValueToEnum<PowerBIDataSourceVersion>(jsonProp.Value);
								if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsPowerBIDataSourceVersionValueCompatible(powerBIDataSourceVersion, mode, dbCompatibilityLevel)))
								{
									return false;
								}
								this.body.DefaultPowerBIDataSourceVersion = powerBIDataSourceVersion;
								return true;
							}
						}
					}
					else if (name == "dataSourceDefaultMaxConnections")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.DataSourceDefaultMaxConnections = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 35:
					if (name == "dataSourceVariablesOverrideBehavior")
					{
						DataSourceVariablesOverrideBehaviorType dataSourceVariablesOverrideBehaviorType = JsonPropertyHelper.ConvertJsonValueToEnum<DataSourceVariablesOverrideBehaviorType>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsDataSourceVariablesOverrideBehaviorTypeValueCompatible(dataSourceVariablesOverrideBehaviorType, mode, dbCompatibilityLevel)))
						{
							return false;
						}
						this.body.DataSourceVariablesOverrideBehavior = dataSourceVariablesOverrideBehaviorType;
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0003E3A6 File Offset: 0x0003C5A6
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0003E3B0 File Offset: 0x0003C5B0
		public Database Database
		{
			get
			{
				return this.database;
			}
			internal set
			{
				if (value != null)
				{
					if (value.CompatibilityMode != CompatibilityMode.Unknown)
					{
						int num;
						string text;
						base.GetCompatibilityRequirement(value.CompatibilityMode, out num, out text);
						if (value.GetCompatibilityLevel() < num)
						{
							throw new CompatibilityViolationException(value.CompatibilityMode, value.GetCompatibilityLevel(), num, text);
						}
					}
					else
					{
						bool flag = false;
						for (int i = 0; i < 3; i++)
						{
							if (base.GetCompatibilityRequirementLevel(CompatibilityRestrictionSet.GetModeByRestrictionIndex(i)) <= value.GetCompatibilityLevel())
							{
								flag = true;
							}
						}
						if (!flag)
						{
							throw new CompatibilityViolationException(this.GetFormattedObjectPath());
						}
					}
					this.body.StorageLocation = value.GetDbStorageLocation(false);
				}
				this.database = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0003E441 File Offset: 0x0003C641
		public Server Server
		{
			get
			{
				if (this.database == null)
				{
					return null;
				}
				return this.database.Parent;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0003E458 File Offset: 0x0003C658
		public bool HasLocalChanges
		{
			get
			{
				return this.TxManager != null && !(this.TxManager.CurrentSavepoint.Name != "Modified") && this.TxManager.CurrentSavepoint.HasDeltaFromPrevious(true);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0003E493 File Offset: 0x0003C693
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0003E49B File Offset: 0x0003C69B
		internal TxManager TxManager { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
		internal bool IsNewModel
		{
			get
			{
				return this.body.CreatedFrom != null && ((Model.ObjectBody)this.body.CreatedFrom).IsNewModelBody;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0003E4CA File Offset: 0x0003C6CA
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0003E4D2 File Offset: 0x0003C6D2
		internal long Version { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0003E4DB File Offset: 0x0003C6DB
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0003E4E3 File Offset: 0x0003C6E3
		internal bool IsInSync { get; private set; }

		// Token: 0x06000742 RID: 1858 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		public ModelOperationResult Sync()
		{
			this.ValidateSyncRequest(false);
			ModelOperationResult modelOperationResult;
			try
			{
				this.IsInSync = true;
				modelOperationResult = this.SyncImpl(false, false, false);
			}
			finally
			{
				this.IsInSync = false;
			}
			return modelOperationResult;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0003E52C File Offset: 0x0003C72C
		public ModelOperationResult Sync(SyncOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.ValidateSyncRequest(options.DiscardLocalChanges);
			ModelOperationResult modelOperationResult;
			try
			{
				this.IsInSync = true;
				modelOperationResult = this.SyncImpl(options.DiscardLocalChanges, false, false);
			}
			finally
			{
				this.IsInSync = false;
			}
			return modelOperationResult;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0003E584 File Offset: 0x0003C784
		public ModelOperationResult SaveChanges()
		{
			if (!this.ValidateSaveRequest(SaveFlags.Default))
			{
				return new ModelOperationResult();
			}
			return this.SaveChangesImpl(SaveFlags.Default, 0);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0003E59D File Offset: 0x0003C79D
		public ModelOperationResult SaveChanges(SaveFlags saveFlags)
		{
			if (!this.ValidateSaveRequest(saveFlags))
			{
				return new ModelOperationResult();
			}
			return this.SaveChangesImpl(saveFlags, 0);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0003E5B6 File Offset: 0x0003C7B6
		public ModelOperationResult SaveChanges(SaveOptions saveOptions)
		{
			if (saveOptions == null)
			{
				throw new ArgumentNullException("saveOptions");
			}
			if (!this.ValidateSaveRequest(saveOptions.SaveFlags))
			{
				return new ModelOperationResult();
			}
			return this.SaveChangesImpl(saveOptions.SaveFlags, saveOptions.MaxParallelism);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0003E5EC File Offset: 0x0003C7EC
		public ModelOperationResult ExecuteXmla(string xmlaRequest)
		{
			if (string.IsNullOrEmpty(xmlaRequest))
			{
				throw new ArgumentNullException("xmlaRequest");
			}
			if (this.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotExecuteXmlaDisconnectedModel);
			}
			if (this.Server.IsInTransactionInternal())
			{
				Database modifiedDatabase = this.Server.CurrentTransaction.ModifiedDatabase;
				if (modifiedDatabase != null && modifiedDatabase != this.database)
				{
					throw new InvalidOperationException(TomSR.Exception_CannotExecuteXmlaAnotherModelInTransaction);
				}
			}
			XmlaResultCollection xmlaResultCollection;
			if (!ExecuteUtil.TryExecuteXmla(this, xmlaRequest, out xmlaResultCollection))
			{
				throw new OperationException(TomSR.Exception_ExecuteXmlaFailed(xmlaResultCollection.GetAggregatedMessage()), xmlaResultCollection, xmlaRequest);
			}
			ObjectImpact objectImpact = this.CompleteModelChanges();
			return new ModelOperationResult
			{
				Impact = objectImpact,
				XmlaResults = xmlaResultCollection
			};
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0003E68C File Offset: 0x0003C88C
		public void UndoLocalChanges()
		{
			if (this.TxManager == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotUndoChangesDisconnectedModel);
			}
			if (this.TxManager.IsInSavepoint("Modified"))
			{
				if (this.TxManager.CurrentSavepoint.Prev == null || this.TxManager.CurrentSavepoint.Prev.Name != "Synced")
				{
					throw new TomInternalException("Cannot undo model changes, because a savepoint to revert to cannot be found.");
				}
				this.UndoLocalChangesImpl();
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0003E702 File Offset: 0x0003C902
		public IReadOnlyList<ModelOperationResult> ApplyRefreshPolicies(bool refresh = true, bool refreshNonPolicyTables = true, int maxParallelism = 0)
		{
			return this.ApplyRefreshPolicies(DateTime.Now, refresh, refreshNonPolicyTables, maxParallelism);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0003E714 File Offset: 0x0003C914
		public IReadOnlyList<ModelOperationResult> ApplyRefreshPolicies(DateTime effectiveDate, bool refresh = true, bool refreshNonPolicyTables = true, int maxParallelism = 0)
		{
			if (this.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotApplyRefreshPolicyDisconnectedModel);
			}
			if (this.HasLocalChanges)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotApplyRefreshPolicyModifiedModel);
			}
			if (DateTime.Compare(effectiveDate, default(DateTime)) == 0)
			{
				effectiveDate = DateTime.Now;
			}
			IReadOnlyList<ModelOperationResult> readOnlyList;
			try
			{
				ObjectChangeTracker.RegisterObjectForRefresh(this, RefreshType.Full, false);
				this.body.MarkForRefresh(RefreshType.Full, null, true, new DateTime?(effectiveDate), refresh);
				if (!refreshNonPolicyTables)
				{
					this.body.RefreshRequested = false;
				}
				ModelOperationResult modelOperationResult = this.SaveChangesImpl(SaveFlags.Default, maxParallelism);
				readOnlyList = new List<ModelOperationResult>(1) { modelOperationResult };
			}
			catch (InvalidOperationException)
			{
				if (this.HasLocalChanges)
				{
					this.UndoLocalChangesImpl();
				}
				throw;
			}
			return readOnlyList;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0003E7C8 File Offset: 0x0003C9C8
		internal ModelOperationResult SyncImpl(bool discardLocalChanges, bool cleanupObsoleteTransaction, bool disableDiscoveryOptimization)
		{
			bool captureXml = this.Server.CaptureXml;
			ModelOperationResult modelOperationResult;
			try
			{
				this.Server.CaptureXml = false;
				if (cleanupObsoleteTransaction)
				{
					this.RollbackTransactionImpl(this.TxManager.GetBeginTxSavepoint());
				}
				else if (this.TxManager.CurrentSavepoint.Name != "Synced")
				{
					this.TxManager.RevertToSavepoint(this.TxManager.GetSyncedSavepoint());
					this.RevertObjectMapToSyncedState();
				}
				if (this.Server.IsInTransactionInternal())
				{
					disableDiscoveryOptimization = true;
				}
				bool flag = this.lastSyncTime != DateTime.MinValue && this.lastSyncTime.Add(Model.ThresholdForFullMetadataDiscovery).CompareTo(DateTime.Now) > 0;
				if (flag && (!cleanupObsoleteTransaction && !disableDiscoveryOptimization))
				{
					long num;
					DateTime dateTime;
					DateTime dateTime2;
					DateTime dateTime3;
					DdlUtil.DiscoverModelMetadataStatus(this.database, out num, out dateTime, out dateTime2, out dateTime3);
					if (num == this.Version && dateTime.CompareTo(this.body.ModifiedTime) == 0 && dateTime3.CompareTo(Model.GetModelLastRefreshTime(this)) == 0)
					{
						return new ModelOperationResult
						{
							Impact = ObjectImpact.Empty,
							XmlaResults = null
						};
					}
				}
				Model model = DdlUtil.DiscoverModel(this.database);
				if (!flag && (!cleanupObsoleteTransaction && !disableDiscoveryOptimization) && model.Version == this.Version && model.body.ModifiedTime.CompareTo(this.body.ModifiedTime) == 0 && Model.GetModelLastRefreshTime(model).CompareTo(Model.GetModelLastRefreshTime(this)) == 0)
				{
					this.lastSyncTime = DateTime.Now;
					modelOperationResult = new ModelOperationResult
					{
						Impact = ObjectImpact.Empty,
						XmlaResults = null
					};
				}
				else
				{
					TxSavepoint txSavepoint = this.TxManager.AddSavepoint("SyncedMostRecent");
					this.UpdateModelObjectsTree(model);
					ObjectImpact objectImpact = txSavepoint.GetDeltaFromPrevious(false).ConvertToImpact();
					this.TxManager.GetSyncedSavepoint();
					txSavepoint.MergeWithPreviousSavepoint();
					txSavepoint.Name = "Synced";
					modelOperationResult = new ModelOperationResult
					{
						Impact = objectImpact,
						XmlaResults = null
					};
				}
			}
			finally
			{
				this.Server.CaptureXml = captureXml;
			}
			return modelOperationResult;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0003E9E8 File Offset: 0x0003CBE8
		internal void UpdateModelSyncTime()
		{
			this.lastSyncTime = DateTime.Now;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
		internal ModelOperationResult SaveChangesImpl(SaveFlags flags, int maxParallelism)
		{
			bool refreshRequested = this.body.RefreshRequested;
			ObjectImpact objectImpact = null;
			XmlaResultCollection xmlaResultCollection = null;
			Model.SaveChangesPhase saveChangesPhase = Model.SaveChangesPhase.Initial;
			SaveContext saveContext = new SaveContext(flags, maxParallelism, this.TxManager.CurrentSavepoint);
			ModelOperationResult modelOperationResult2;
			try
			{
				IList<MetadataObject> list = null;
				ObjectChangelist objectChangelist = null;
				while (saveChangesPhase != Model.SaveChangesPhase.Final)
				{
					switch (saveChangesPhase)
					{
					case Model.SaveChangesPhase.Initial:
						list = saveContext.Savepoint.GetApplyRefreshPolicyDeltaFromPrevious();
						if (list.Count == 0)
						{
							saveChangesPhase = Model.SaveChangesPhase.CRUDOperations;
						}
						else
						{
							if (list[0].ObjectType == ObjectType.Model)
							{
								if (list.Count == 1)
								{
									if (!this.Tables.Any((Table t) => t.RefreshPolicy != null))
									{
										saveChangesPhase = Model.SaveChangesPhase.CRUDOperations;
										break;
									}
								}
								this.ValidateBeforeApplyRefreshPolicies();
								if (this.Database.IsBlocked && refreshRequested && Utils.ConvertRefreshMaskToType(((IRefreshableMetadataObjectBody)this.body).RequestedRefreshMask).Contains(RefreshType.Full))
								{
									saveContext.UnblockingDatabase = true;
								}
							}
							else
							{
								for (int i = 0; i < list.Count; i++)
								{
									((Table)list[i]).ValidateBeforeApplyRefreshPolicy();
								}
							}
							if (this.Server.CaptureXml || !this.Server.Connected)
							{
								throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesSaveInOfflineMode);
							}
							saveContext.MultiPhaseSave = true;
							if (!this.Server.IsInTransactionInternal())
							{
								this.TxManager.ForceTransactionForModifiedModel();
								saveContext.TransactionCreated = true;
							}
							saveChangesPhase = (saveContext.UnblockingDatabase ? Model.SaveChangesPhase.UnblockDatabase : Model.SaveChangesPhase.ApplyRefreshPolicyPartitionManagement);
						}
						break;
					case Model.SaveChangesPhase.UnblockDatabase:
					{
						string text = Utils.ConvertXmlaToString(DdlUtil.FormatRefreshClearModel(this.Database, saveContext.MaxParallelism));
						if (!ExecuteUtil.TryExecuteXmla(this, text, out xmlaResultCollection))
						{
							throw new OperationException(TomSR.Exception_SaveModelChangesFailed(xmlaResultCollection.GetAggregatedMessage()), xmlaResultCollection, text);
						}
						saveChangesPhase = Model.SaveChangesPhase.ApplyRefreshPolicyPartitionManagement;
						break;
					}
					case Model.SaveChangesPhase.ApplyRefreshPolicyPartitionManagement:
						this.ExecuteApplyRefreshPolicyAction(saveContext, Model.SaveChangesPhase.ApplyRefreshPolicyPartitionManagement, list);
						saveChangesPhase = Model.SaveChangesPhase.CRUDOperations;
						break;
					case Model.SaveChangesPhase.CRUDOperations:
						objectChangelist = saveContext.Savepoint.GetDeltaFromPrevious(!saveContext.MultiPhaseSave);
						if (!objectChangelist.IsEmpty)
						{
							IEnumerable<XElement> tabularRequests = DdlUtil.GetTabularRequests(this.database.ID, this.database.CompatibilityMode, this.database.CompatibilityLevel, objectChangelist, saveContext.Flags, saveContext.MaxParallelism);
							if (this.Server.CaptureXml)
							{
								using (IEnumerator<string> enumerator = tabularRequests.Select(new Func<XElement, string>(Utils.ConvertXmlaToString)).GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										string text2 = enumerator.Current;
										this.Server.Execute(text2);
									}
									goto IL_02D6;
								}
							}
							XElement batchElement = DdlUtil.GetBatchElement(!saveContext.MultiPhaseSave && !this.Server.IsInTransactionInternal());
							batchElement.Add(tabularRequests);
							string text3 = Utils.ConvertXmlaToString(batchElement);
							if (!ExecuteUtil.TryExecuteXmla(this, text3, out xmlaResultCollection))
							{
								throw new OperationException(TomSR.Exception_SaveModelChangesFailed(xmlaResultCollection.GetAggregatedMessage()), xmlaResultCollection, text3);
							}
						}
						IL_02D6:
						saveContext.CRUDOperationsExecuted = true;
						saveChangesPhase = (saveContext.MultiPhaseSave ? Model.SaveChangesPhase.ApplyRefreshPolicyRefreshManagement : Model.SaveChangesPhase.Final);
						break;
					case Model.SaveChangesPhase.ApplyRefreshPolicyRefreshManagement:
						if (!objectChangelist.IsEmpty)
						{
							saveContext.PrepareForPostCRUDOperations();
						}
						this.ExecuteApplyRefreshPolicyAction(saveContext, Model.SaveChangesPhase.ApplyRefreshPolicyRefreshManagement, list);
						saveChangesPhase = Model.SaveChangesPhase.RefreshMergeOperations;
						break;
					case Model.SaveChangesPhase.RefreshMergeOperations:
						objectChangelist = saveContext.Savepoint.GetDeltaFromPrevious(true);
						if (!objectChangelist.IsEmpty)
						{
							IEnumerable<XElement> tabularRequests2 = DdlUtil.GetTabularRequests(this.database.ID, this.database.CompatibilityMode, this.database.CompatibilityLevel, objectChangelist, saveContext.Flags, saveContext.MaxParallelism);
							XElement batchElement2 = DdlUtil.GetBatchElement(false);
							batchElement2.Add(tabularRequests2);
							string text4 = Utils.ConvertXmlaToString(batchElement2);
							XmlaResultCollection xmlaResultCollection2;
							if (!ExecuteUtil.TryExecuteXmla(this, text4, out xmlaResultCollection2))
							{
								throw new OperationException(TomSR.Exception_SaveModelChangesFailed(xmlaResultCollection2.GetAggregatedMessage()), xmlaResultCollection2, text4);
							}
							Utils.CombineXmlaResults(ref xmlaResultCollection, xmlaResultCollection2);
						}
						saveChangesPhase = (saveContext.HasDeferredMergeRequests() ? Model.SaveChangesPhase.DeferredMergeOperations : Model.SaveChangesPhase.Final);
						break;
					case Model.SaveChangesPhase.DeferredMergeOperations:
						saveContext.RequestNextBatchOfDeferredMergeRequests();
						objectChangelist = saveContext.Savepoint.GetDeltaFromPrevious(true);
						if (!objectChangelist.IsEmpty)
						{
							IEnumerable<XElement> tabularRequests3 = DdlUtil.GetTabularRequests(this.database.ID, this.database.CompatibilityMode, this.database.CompatibilityLevel, objectChangelist, saveContext.Flags, saveContext.MaxParallelism);
							XElement batchElement3 = DdlUtil.GetBatchElement(false);
							batchElement3.Add(tabularRequests3);
							string text5 = Utils.ConvertXmlaToString(batchElement3);
							XmlaResultCollection xmlaResultCollection3;
							if (!ExecuteUtil.TryExecuteXmla(this, text5, out xmlaResultCollection3))
							{
								throw new OperationException(TomSR.Exception_SaveModelChangesFailed(xmlaResultCollection3.GetAggregatedMessage()), xmlaResultCollection3, text5);
							}
							Utils.CombineXmlaResults(ref xmlaResultCollection, xmlaResultCollection3);
						}
						if (!saveContext.HasDeferredMergeRequests())
						{
							saveChangesPhase = Model.SaveChangesPhase.Final;
						}
						break;
					}
				}
				Utils.Verify(saveContext.CRUDOperationsExecuted, "There is no valid path that skip the CRUD-operations phase!");
				if (!this.Server.CaptureXml)
				{
					if (saveContext.TransactionCreated)
					{
						ModelOperationResult modelOperationResult = ModelOperationResult.ConvertFrom(((ITxService)this.Server).CommitTransactionWithResult());
						if (modelOperationResult != null)
						{
							objectImpact = modelOperationResult.Impact;
							Utils.CombineXmlaResults(ref xmlaResultCollection, modelOperationResult.XmlaResults);
						}
						saveContext.TransactionCreated = false;
					}
					if (objectImpact == null)
					{
						objectImpact = this.CompleteModelChanges();
					}
					if (this.HasLocalChanges)
					{
						throw new TomInternalException("After saving the changes is completed, the model must have no local changes!");
					}
					if (refreshRequested && this.database.IsBlocked)
					{
						Server.SendRefresh(this.Database, ObjectExpansion.ObjectProperties);
					}
				}
				else
				{
					Utils.Verify(!saveContext.TransactionCreated);
				}
				modelOperationResult2 = new ModelOperationResult
				{
					Impact = objectImpact,
					XmlaResults = xmlaResultCollection
				};
			}
			finally
			{
				if (saveContext.TransactionCreated)
				{
					((ITxService)this.Server).RollbackTransaction();
				}
			}
			return modelOperationResult2;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0003EF78 File Offset: 0x0003D178
		internal void ApplyImpact(ImpactDataSet impactDataSet)
		{
			if (impactDataSet == null || impactDataSet.IsEmpty)
			{
				TxSavepoint currentSavepoint = this.TxManager.CurrentSavepoint;
				this.UpdateModelObjectsTree(DdlUtil.DiscoverModel(this.database));
				if (this.TxManager.CurrentSavepoint != currentSavepoint && this.TxManager.CurrentSavepoint.GetDeltaFromPrevious(false).IsEmpty)
				{
					this.TxManager.CurrentSavepoint.MergeWithPreviousSavepoint();
					this.TxManager.CurrentSavepoint.Name = currentSavepoint.Name;
					return;
				}
			}
			else
			{
				this.ApplyImpact(impactDataSet.ConvertToServerImpact(this, base.GetNamesComparer()));
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0003F00D File Offset: 0x0003D20D
		internal void UndoLocalChangesImpl()
		{
			this.TxManager.RevertToSavepoint(this.TxManager.CurrentSavepoint.Prev);
			this.RevertObjectMapToSyncedState();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0003F030 File Offset: 0x0003D230
		internal void RollbackTransaction()
		{
			if (this.TxManager == null)
			{
				throw new TomInternalException("Cannot rollback transaction. This Model does not have any save points created.");
			}
			TxSavepoint beginTxSavepoint = this.TxManager.GetBeginTxSavepoint();
			if (beginTxSavepoint == null)
			{
				throw new TomInternalException("Cannot rollback transaction. Can't find 'BeginTransaction' savepoint.");
			}
			this.RollbackTransactionImpl(beginTxSavepoint);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0003F074 File Offset: 0x0003D274
		internal ObjectImpact CompleteTransaction()
		{
			if (this.TxManager == null)
			{
				throw new TomInternalException("Cannot complete transaction. This Model does not have any save points created.");
			}
			TxSavepoint beginTxSavepoint = this.TxManager.GetBeginTxSavepoint();
			if (beginTxSavepoint == null)
			{
				throw new TomInternalException("Cannot complete transaction. Can't find 'BeginTransaction' savepoint.");
			}
			if (this.TxManager.CurrentSavepoint == beginTxSavepoint)
			{
				this.TxManager.CurrentSavepoint.Name = "Synced";
				this.lastSyncTime = DateTime.Now;
				return ObjectImpact.Empty;
			}
			ObjectImpact objectImpact = this.FoldSavepointsAndComputeModelChanges(beginTxSavepoint);
			this.TxManager.CurrentSavepoint.MergeWithPreviousSavepoint();
			this.TxManager.CurrentSavepoint.DropPreviousSavepoints();
			this.TxManager.CurrentSavepoint.Name = "Synced";
			this.mapDeletedObjectsByIdSinceBeginTx.Clear();
			this.mapDeletedObjectsByIdSinceSync.Clear();
			this.mapAddedObjectsByIdSinceBeginTx.Clear();
			this.mapAddedObjectsByIdSinceSync.Clear();
			this.lastSyncTime = DateTime.Now;
			return objectImpact;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0003F158 File Offset: 0x0003D358
		private static DateTime GetModelLastRefreshTime(Model model)
		{
			DateTime dateTime = DateTime.MinValue;
			foreach (Table table in model.Tables)
			{
				foreach (Partition partition in table.Partitions)
				{
					if (partition.body.RefreshedTime.CompareTo(dateTime) > 0)
					{
						dateTime = partition.body.RefreshedTime;
					}
				}
				foreach (Column column in table.Columns)
				{
					if (column.body.RefreshedTime.CompareTo(dateTime) > 0)
					{
						dateTime = column.body.RefreshedTime;
					}
					if (column.AttributeHierarchy.body.RefreshedTime.CompareTo(dateTime) > 0)
					{
						dateTime = column.AttributeHierarchy.body.RefreshedTime;
					}
				}
				foreach (Hierarchy hierarchy in table.Hierarchies)
				{
					if (hierarchy.body.RefreshedTime.CompareTo(dateTime) > 0)
					{
						dateTime = hierarchy.body.RefreshedTime;
					}
				}
			}
			foreach (Relationship relationship in model.Relationships)
			{
				if (relationship.body.RefreshedTime.CompareTo(dateTime) > 0)
				{
					dateTime = relationship.body.RefreshedTime;
				}
			}
			return dateTime;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0003F384 File Offset: 0x0003D584
		private static IEnumerable<Table> GetTablesForIRModelRefresh(RefreshType refreshType, TableCollection tables, bool isUnblockingDatabase)
		{
			if (isUnblockingDatabase)
			{
				return tables;
			}
			return Model.GetTablesForIRModelRefreshImpl(refreshType, tables);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0003F392 File Offset: 0x0003D592
		private static IEnumerable<Table> GetTablesForIRModelRefreshImpl(RefreshType refreshType, TableCollection tables)
		{
			foreach (Table table in tables)
			{
				if (!table.ExcludeFromModelRefresh && !Model.IsCalcTableExcludedFromIRModelRefresh(refreshType, table))
				{
					yield return table;
				}
			}
			IEnumerator<Table> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0003F3AC File Offset: 0x0003D5AC
		private static bool IsCalcTableExcludedFromIRModelRefresh(RefreshType refreshType, Table table)
		{
			if (table.Partitions.Count > 1)
			{
				return false;
			}
			Partition partition = table.Partitions[0];
			if (partition.SourceType != PartitionSourceType.Calculated)
			{
				return false;
			}
			if (refreshType != RefreshType.Full || !((CalculatedPartitionSource)partition.Source).RetainDataTillForceCalculate)
			{
				return false;
			}
			ObjectState state = partition.State;
			return state == ObjectState.Ready || state == ObjectState.ForceCalculationNeeded;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0003F40C File Offset: 0x0003D60C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidateSyncRequest(bool discardLocalChanges)
		{
			if (this.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotSyncModelDisconnected);
			}
			if (this.IsNewModel)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotSyncNewModel);
			}
			if (this.HasLocalChanges && !discardLocalChanges)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotSyncModelModified);
			}
			if (this.database.HasObsoleteTransaction())
			{
				throw new InvalidOperationException(TomSR.Exception_CannotSyncModelOfDirtyDatabase(this.database.Name));
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0003F478 File Offset: 0x0003D678
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool ValidateSaveRequest(SaveFlags flags)
		{
			if (SaveOptions.IsFlagEnabled(flags, SaveFlags.DelayValidation | SaveFlags.ForceValidation))
			{
				throw new InvalidOperationException(TomSR.Exception_ValidationFlagsSimultaneousUse);
			}
			if (this.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotSaveChangesDisconnectedModel);
			}
			if (this.Server.IsInTransactionInternal())
			{
				Database modifiedDatabase = this.Server.CurrentTransaction.ModifiedDatabase;
				if (modifiedDatabase != null && modifiedDatabase != this.database)
				{
					throw new InvalidOperationException(TomSR.Exception_CannotSaveChangeAnotherModelInTransaction);
				}
			}
			else if (!SaveOptions.IsFlagDisabled(flags, SaveFlags.DelayValidation | SaveFlags.ForceValidation))
			{
				throw new InvalidOperationException(TomSR.Exception_ValidationFlagsOutsideTransaction);
			}
			return this.TxManager != null && this.TxManager.CurrentSavepoint != null && !(this.TxManager.CurrentSavepoint.Name == "Synced");
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0003F52C File Offset: 0x0003D72C
		private void UpdateModelObjectsTree(Model model)
		{
			CopyContext copyContext = new CopyContext(CopyFlags.IncludeObjectIds | CopyFlags.DontResolveCrossLinks | CopyFlags.MetadataSync, null);
			this.CopyFrom(model, copyContext);
			base.TryResolveCrossLinksAfterSubtreeCopy(copyContext);
			this.Version = model.Version;
			this.lastSyncTime = DateTime.Now;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0003F56C File Offset: 0x0003D76C
		private void ValidateBeforeApplyRefreshPolicies()
		{
			foreach (Table table in this.Tables)
			{
				if (table.RefreshPolicy != null)
				{
					table.ValidateBeforeApplyRefreshPolicy();
				}
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
		private void ExecuteApplyRefreshPolicyAction(SaveContext context, Model.SaveChangesPhase phase, IList<MetadataObject> arpObjects)
		{
			int num = ((arpObjects[0].ObjectType == ObjectType.Model) ? 1 : 0);
			if (num > 0)
			{
				if (phase != Model.SaveChangesPhase.ApplyRefreshPolicyPartitionManagement)
				{
					if (phase == Model.SaveChangesPhase.ApplyRefreshPolicyRefreshManagement)
					{
						this.ApplyRefreshPoliciesRefreshManagement(context);
					}
				}
				else
				{
					this.ApplyRefreshPoliciesImpl(context);
				}
			}
			for (int i = num; i < arpObjects.Count; i++)
			{
				if (phase != Model.SaveChangesPhase.ApplyRefreshPolicyPartitionManagement)
				{
					if (phase == Model.SaveChangesPhase.ApplyRefreshPolicyRefreshManagement)
					{
						((Table)arpObjects[i]).ApplyRefreshPolicyRefreshManagement(context);
					}
				}
				else
				{
					((Table)arpObjects[i]).ApplyRefreshPolicyImpl(context);
				}
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0003F63C File Offset: 0x0003D83C
		private void ApplyRefreshPoliciesImpl(SaveContext context)
		{
			DateTime dateTime = ((this.body.ApplyRefreshPolicyEffectiveDate != null) ? this.body.ApplyRefreshPolicyEffectiveDate.Value : DateTime.Now);
			foreach (Table table in this.Tables)
			{
				if (table.RefreshPolicy != null && (context.UnblockingDatabase || !table.ExcludeFromModelRefresh) && !table.body.ApplyRefreshPolicyRequested)
				{
					table.ApplyRefreshPolicyImpl(context, dateTime, this.body.RefreshAfterApplyRefreshPolicyRequested);
				}
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		private void ApplyRefreshPoliciesRefreshManagement(SaveContext context)
		{
			if (!this.body.RefreshAfterApplyRefreshPolicyRequested)
			{
				if (this.body.RefreshRequested)
				{
					RefreshType refreshType = Utils.ConvertRefreshMaskToType(this.body.RequestedRefreshMask).FirstOrDefault<RefreshType>();
					foreach (Table table in from t in Model.GetTablesForIRModelRefresh(refreshType, this.Tables, context.UnblockingDatabase)
						where t.RefreshPolicy == null
						select t)
					{
						table.MarkForRefresh(refreshType, this.body.Overrides);
					}
					this.body.RefreshRequested = false;
				}
				return;
			}
			if (this.body.RefreshRequested)
			{
				RefreshType refreshType2 = Utils.ConvertRefreshMaskToType(this.body.RequestedRefreshMask).FirstOrDefault<RefreshType>();
				foreach (Table table2 in Model.GetTablesForIRModelRefresh(refreshType2, this.Tables, context.UnblockingDatabase))
				{
					if (table2.RefreshPolicy != null)
					{
						if (!table2.body.ApplyRefreshPolicyRequested)
						{
							table2.ApplyRefreshPolicyRefreshManagementImpl(context, refreshType2, this.body.Overrides);
						}
					}
					else
					{
						table2.MarkForRefresh(refreshType2, this.body.Overrides);
					}
				}
				this.body.RefreshRequested = false;
				return;
			}
			foreach (Table table3 in this.Tables.Where((Table t) => !t.ExcludeFromModelRefresh && t.RefreshPolicy != null && !t.body.ApplyRefreshPolicyRequested))
			{
				table3.ApplyRefreshPolicyRefreshManagementImpl(context, Utils.ConvertRefreshMaskToType(this.body.RequestedRefreshMask).FirstOrDefault<RefreshType>(), this.body.Overrides);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0003F8E4 File Offset: 0x0003DAE4
		private void ApplyImpact(ServerImpact impact)
		{
			if (this.TxManager.CurrentSavepoint != null)
			{
				this.TxManager.CurrentSavepoint.AnyRenameRequestedThroughAPI = false;
				this.TxManager.CurrentSavepoint.AllMergePartitionsRequestedTables.Clear();
			}
			this.HandleImpactDeletedObjects(impact);
			this.HandleImpactAffectedObjects(impact);
			if (impact.ModelVersion > 0L)
			{
				this.Version = impact.ModelVersion;
				this.lastSyncTime = DateTime.Now;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0003F954 File Offset: 0x0003DB54
		private void HandleImpactDeletedObjects(ServerImpact impact)
		{
			foreach (ObjectId objectId in impact.DeletedObjects)
			{
				MetadataObject metadataObject;
				if ((this.objectMap.ContainsKey(objectId) || this.mapDeletedObjectsByIdSinceBeginTx.ContainsKey(objectId) || this.mapDeletedObjectsByIdSinceSync.ContainsKey(objectId)) && this.objectMap.TryGetValue(objectId, out metadataObject))
				{
					if (metadataObject.ParentCollection != null)
					{
						metadataObject.ParentCollection.Remove(metadataObject);
					}
					else
					{
						if (metadataObject.Parent == null)
						{
							throw new TomInternalException("Model can't be deleted from the Database");
						}
						metadataObject.Parent.RemoveDirectChild(metadataObject);
					}
				}
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0003FA0C File Offset: 0x0003DC0C
		private void HandleImpactAffectedObjects(ServerImpact impact)
		{
			Dictionary<ObjectId, MetadataObject> dictionary = new Dictionary<ObjectId, MetadataObject>();
			foreach (MetadataObject metadataObject in impact.AffectedObjects)
			{
				dictionary[metadataObject.Id] = metadataObject;
			}
			foreach (MetadataObject metadataObject2 in impact.AffectedObjects)
			{
				metadataObject2.ResolveLinks(dictionary, false);
			}
			List<MetadataObject> list = impact.AffectedObjects.Where((MetadataObject obj) => obj.Parent == null).ToList<MetadataObject>();
			list.Sort((MetadataObject o1, MetadataObject o2) => ObjectTreeHelper.GetObjectTypeTopologicalOrder(o1.ObjectType) - ObjectTreeHelper.GetObjectTypeTopologicalOrder(o2.ObjectType));
			CopyContext copyContext = (impact.IsFullModel ? new CopyContext(CopyFlags.IncludeObjectIds, null) : new CopyContext(CopyFlags.IncludeObjectIds | CopyFlags.Incremental, this.objectMap));
			foreach (MetadataObject metadataObject3 in list)
			{
				this.MergeAffectedSubtree(metadataObject3, copyContext);
			}
			foreach (MetadataObject metadataObject4 in copyContext.CopiedObjects)
			{
				metadataObject4.ResolveCrossLinks(this.objectMap, true);
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0003FB94 File Offset: 0x0003DD94
		private void MergeAffectedSubtree(MetadataObject affectedSubtreeRoot, CopyContext mergeCopyContext)
		{
			Utils.Verify(!affectedSubtreeRoot.Id.IsNull, "Every object from AffectedObjects list must have an Id. It's coming from Server and Server must always assign Id.");
			MetadataObject metadataObject;
			if (!this.objectMap.TryGetValue(affectedSubtreeRoot.Id, out metadataObject))
			{
				MetadataObject metadataObject2;
				Utils.Verify(this.objectMap.TryGetValue(affectedSubtreeRoot.ParentId, out metadataObject2), "Can't find parent of affected object in Model tree.");
				IMetadataObjectCollection parentCollection = affectedSubtreeRoot.GetParentCollection(metadataObject2);
				Utils.Verify(parentCollection != null, "There is no way that an affected-root does not have a parent-collection");
				INamedMetadataObjectCollection namedMetadataObjectCollection = parentCollection as INamedMetadataObjectCollection;
				if (namedMetadataObjectCollection != null)
				{
					NamedMetadataObject namedMetadataObject = (NamedMetadataObject)affectedSubtreeRoot;
					metadataObject = namedMetadataObjectCollection.Find(namedMetadataObject.Name);
					if (metadataObject == null)
					{
						metadataObject = affectedSubtreeRoot.CreateObjectOfSameType();
						namedMetadataObjectCollection.Add(metadataObject);
					}
				}
				else
				{
					ObjectType itemType = parentCollection.ItemType;
					ChangedProperty changedProperty;
					if (itemType != ObjectType.ChangedProperty)
					{
						if (itemType != ObjectType.ExcludedArtifact)
						{
							throw TomInternalException.Create("Invalid item-type for the parent-collection of an affected-root - {0} is not supported", new object[] { parentCollection.ItemType });
						}
						ExcludedArtifact excludedArtifact;
						if (Utils.TryFindEquivalentUniqueObject<ExcludedArtifact>((IEnumerable<ExcludedArtifact>)parentCollection, (ExcludedArtifact)affectedSubtreeRoot, Utils.IsEquivalentExcludedArtifact, out excludedArtifact))
						{
							metadataObject = excludedArtifact;
						}
						else
						{
							metadataObject = new ChangedProperty();
							parentCollection.Add(metadataObject);
						}
					}
					else if (Utils.TryFindEquivalentUniqueObject<ChangedProperty>((IEnumerable<ChangedProperty>)parentCollection, (ChangedProperty)affectedSubtreeRoot, Utils.IsEquivalentChangedProperty, out changedProperty))
					{
						metadataObject = changedProperty;
					}
					else
					{
						metadataObject = new ChangedProperty();
						parentCollection.Add(metadataObject);
					}
				}
			}
			metadataObject.CopyFrom(affectedSubtreeRoot, mergeCopyContext);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
		private ObjectImpact CompleteModelChanges()
		{
			TxSavepoint syncedSavepoint = this.TxManager.GetSyncedSavepoint();
			if (this.TxManager.CurrentSavepoint == syncedSavepoint)
			{
				return ObjectImpact.Empty;
			}
			TxSavepoint currentSavepoint = this.TxManager.CurrentSavepoint;
			ObjectImpact objectImpact = this.FoldSavepointsAndComputeModelChanges(syncedSavepoint);
			currentSavepoint.MergeWithPreviousSavepoint();
			currentSavepoint.Name = "Synced";
			this.mapDeletedObjectsByIdSinceSync.Clear();
			this.mapAddedObjectsByIdSinceSync.Clear();
			return objectImpact;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0003FD48 File Offset: 0x0003DF48
		private ObjectImpact FoldSavepointsAndComputeModelChanges(TxSavepoint referenceSavepoint)
		{
			this.TxManager.CurrentSavepoint.ClearPendingOperationFlags();
			while (this.TxManager.CurrentSavepoint.Prev != referenceSavepoint)
			{
				this.TxManager.CurrentSavepoint.MergeWithPreviousSavepoint();
			}
			return this.TxManager.CurrentSavepoint.GetDeltaFromPrevious(false).ConvertToImpact();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0003FDA0 File Offset: 0x0003DFA0
		private void RollbackTransactionImpl(TxSavepoint beginTxSavepoint)
		{
			this.TxManager.RevertToSavepoint(beginTxSavepoint);
			beginTxSavepoint.Name = "Synced";
			this.RevertObjectMapToBeginTxState();
			this.lastSyncTime = DateTime.MinValue;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0003FDCA File Offset: 0x0003DFCA
		void INotifyObjectIdChange.NotifyIdChanging(MetadataObject obj, ObjectId newId)
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0003FDCC File Offset: 0x0003DFCC
		void INotifyObjectIdChange.NotifyIdChanged(MetadataObject obj, ObjectId oldId)
		{
			this.objectMap[obj.Id] = obj;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
		internal void NotifySubtreeRemoved(MetadataObject obj)
		{
			if (obj.Id.IsNull)
			{
				return;
			}
			foreach (MetadataObject metadataObject in from o in obj.GetSelfAndAllDescendants()
				where !o.Id.IsNull
				select o)
			{
				this.objectMap.Remove(metadataObject.Id);
				if (this.TxManager != null)
				{
					this.mapDeletedObjectsByIdSinceSync[metadataObject.Id] = metadataObject;
					if (this.Server != null && this.Server.IsInTransactionInternal() && this.Server.CurrentTransaction.ModifiedDatabase == this.Database)
					{
						this.mapDeletedObjectsByIdSinceBeginTx[metadataObject.Id] = metadataObject;
					}
				}
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0003FEC8 File Offset: 0x0003E0C8
		internal void NotifySubtreeAdded(MetadataObject obj)
		{
			if (obj.Id.IsNull)
			{
				return;
			}
			foreach (MetadataObject metadataObject in obj.GetSelfAndAllDescendants())
			{
				this.objectMap[metadataObject.Id] = metadataObject;
				if (this.TxManager != null)
				{
					this.mapAddedObjectsByIdSinceSync[metadataObject.Id] = metadataObject;
					if (this.Server != null && this.Server.IsInTransactionInternal())
					{
						this.mapAddedObjectsByIdSinceBeginTx[metadataObject.Id] = metadataObject;
					}
				}
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0003FF74 File Offset: 0x0003E174
		private void RevertObjectMapToSyncedState()
		{
			Dictionary<ObjectId, MetadataObject> dictionary = this.mapDeletedObjectsByIdSinceSync.Except(this.mapAddedObjectsByIdSinceSync).ToDictionary((KeyValuePair<ObjectId, MetadataObject> p) => p.Key, (KeyValuePair<ObjectId, MetadataObject> p) => p.Value);
			foreach (ObjectId objectId in this.mapAddedObjectsByIdSinceSync.Except(this.mapDeletedObjectsByIdSinceSync).ToDictionary((KeyValuePair<ObjectId, MetadataObject> p) => p.Key, (KeyValuePair<ObjectId, MetadataObject> p) => p.Value).Keys)
			{
				this.mapAddedObjectsByIdSinceBeginTx.Remove(objectId);
				this.objectMap.Remove(objectId);
			}
			foreach (ObjectId objectId2 in dictionary.Keys)
			{
				this.mapDeletedObjectsByIdSinceBeginTx.Remove(objectId2);
				this.objectMap[objectId2] = dictionary[objectId2];
			}
			this.mapAddedObjectsByIdSinceSync.Clear();
			this.mapDeletedObjectsByIdSinceSync.Clear();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000400F4 File Offset: 0x0003E2F4
		private void RevertObjectMapToBeginTxState()
		{
			Dictionary<ObjectId, MetadataObject> dictionary = this.mapDeletedObjectsByIdSinceBeginTx.Except(this.mapAddedObjectsByIdSinceBeginTx).ToDictionary((KeyValuePair<ObjectId, MetadataObject> p) => p.Key, (KeyValuePair<ObjectId, MetadataObject> p) => p.Value);
			foreach (ObjectId objectId in this.mapAddedObjectsByIdSinceBeginTx.Except(this.mapDeletedObjectsByIdSinceBeginTx).ToDictionary((KeyValuePair<ObjectId, MetadataObject> p) => p.Key, (KeyValuePair<ObjectId, MetadataObject> p) => p.Value).Keys)
			{
				this.objectMap.Remove(objectId);
			}
			foreach (ObjectId objectId2 in dictionary.Keys)
			{
				this.objectMap[objectId2] = dictionary[objectId2];
			}
			this.mapDeletedObjectsByIdSinceBeginTx.Clear();
			this.mapDeletedObjectsByIdSinceSync.Clear();
			this.mapAddedObjectsByIdSinceBeginTx.Clear();
			this.mapAddedObjectsByIdSinceSync.Clear();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00040270 File Offset: 0x0003E470
		private void CanUpdateChildCollections(string culture, string collation)
		{
			IEqualityComparer<string> stringComparer = DdlUtil.GetStringComparer(culture, collation);
			foreach (INamedMetadataObjectCollection namedMetadataObjectCollection in from c in base.GetSelfAndAllDescendants().SelectMany((MetadataObject o) => o.GetChildrenCollections(false))
				where c is INamedMetadataObjectCollection
				select (INamedMetadataObjectCollection)c)
			{
				ObjectChangeTracker.RegisterCollectionChanging(namedMetadataObjectCollection);
				if (!namedMetadataObjectCollection.CanUpdateCultureInfo(stringComparer))
				{
					throw new ArgumentException(TomSR.Exception_CannotUpdateCultureCollation);
				}
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00040344 File Offset: 0x0003E544
		private void UpdateChildCollections(string culture, string collation)
		{
			IEqualityComparer<string> stringComparer = DdlUtil.GetStringComparer(culture, collation);
			foreach (INamedMetadataObjectCollection namedMetadataObjectCollection in from c in base.GetSelfAndAllDescendants().SelectMany((MetadataObject o) => o.GetChildrenCollections(false))
				where c is INamedMetadataObjectCollection
				select (INamedMetadataObjectCollection)c)
			{
				ObjectChangeTracker.RegisterCollectionChanging(namedMetadataObjectCollection);
				namedMetadataObjectCollection.UpdateCultureInfo(stringComparer);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0004040C File Offset: 0x0003E60C
		[CompatibilityRequirement("1564")]
		[Obsolete("This method was deprecated, please use ApplyAutomaticAggregations instead.")]
		public void ApplyPerformanceRecommendations(AutomaticAggregationOptions options)
		{
			this.ApplyAutomaticAggregations(options);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00040415 File Offset: 0x0003E615
		[CompatibilityRequirement("1564")]
		public void ApplyAutomaticAggregations()
		{
			AdaptiveCachingHelper.ApplyAutomaticAggregations(this, null);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0004041E File Offset: 0x0003E61E
		[CompatibilityRequirement("1564")]
		public void ApplyAutomaticAggregations(AutomaticAggregationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			AdaptiveCachingHelper.ApplyAutomaticAggregations(this, options);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00040435 File Offset: 0x0003E635
		internal void DetachFromDatabase()
		{
			this.database = null;
			base.MarkAsRemoved();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00040444 File Offset: 0x0003E644
		internal void CreateTxManager(bool isInTransaction, bool isSynced)
		{
			this.TxManager = new TxManager(this, isInTransaction, isSynced);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00040454 File Offset: 0x0003E654
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Model_1Arg(this.Name);
		}

		// Token: 0x0400011B RID: 283
		internal Model.ObjectBody body;

		// Token: 0x0400011C RID: 284
		internal CustomProperty<Model, string, DataAccessOptions> dataAccessOptions;

		// Token: 0x0400011D RID: 285
		internal CustomProperty<Model, string, AutomaticAggregationOptions> automaticAggregationOptions;

		// Token: 0x0400011E RID: 286
		private TableCollection _Tables;

		// Token: 0x0400011F RID: 287
		private RelationshipCollection _Relationships;

		// Token: 0x04000120 RID: 288
		private DataSourceCollection _DataSources;

		// Token: 0x04000121 RID: 289
		private PerspectiveCollection _Perspectives;

		// Token: 0x04000122 RID: 290
		private CultureCollection _Cultures;

		// Token: 0x04000123 RID: 291
		private ModelRoleCollection _Roles;

		// Token: 0x04000124 RID: 292
		private NamedExpressionCollection _Expressions;

		// Token: 0x04000125 RID: 293
		private QueryGroupCollection _QueryGroups;

		// Token: 0x04000126 RID: 294
		private AnalyticsAIMetadataCollection _AnalyticsAIMetadata;

		// Token: 0x04000127 RID: 295
		private FunctionCollection _Functions;

		// Token: 0x04000128 RID: 296
		private BindingInfoCollection _BindingInfoCollection;

		// Token: 0x04000129 RID: 297
		private ModelAnnotationCollection _Annotations;

		// Token: 0x0400012A RID: 298
		private ModelExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0400012B RID: 299
		private ModelExcludedArtifactCollection _ExcludedArtifacts;

		// Token: 0x0400012C RID: 300
		private static readonly TimeSpan ThresholdForFullMetadataDiscovery = new TimeSpan(0, 5, 0);

		// Token: 0x0400012D RID: 301
		private Database database;

		// Token: 0x0400012E RID: 302
		private DateTime lastSyncTime = DateTime.MinValue;

		// Token: 0x0400012F RID: 303
		private Dictionary<ObjectId, MetadataObject> objectMap = new Dictionary<ObjectId, MetadataObject>();

		// Token: 0x04000130 RID: 304
		private Dictionary<ObjectId, MetadataObject> mapAddedObjectsByIdSinceBeginTx = new Dictionary<ObjectId, MetadataObject>();

		// Token: 0x04000131 RID: 305
		private Dictionary<ObjectId, MetadataObject> mapDeletedObjectsByIdSinceBeginTx = new Dictionary<ObjectId, MetadataObject>();

		// Token: 0x04000132 RID: 306
		private Dictionary<ObjectId, MetadataObject> mapAddedObjectsByIdSinceSync = new Dictionary<ObjectId, MetadataObject>();

		// Token: 0x04000133 RID: 307
		private Dictionary<ObjectId, MetadataObject> mapDeletedObjectsByIdSinceSync = new Dictionary<ObjectId, MetadataObject>();

		// Token: 0x02000286 RID: 646
		internal class ObjectBody : IncrementalRefreshMetadataObjectBody<Model>
		{
			// Token: 0x06002107 RID: 8455 RVA: 0x000D7A90 File Offset: 0x000D5C90
			public ObjectBody(Model owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.DefaultMeasureID = new CrossLink<Model, Measure>(owner, "DefaultMeasure");
			}

			// Token: 0x06002108 RID: 8456 RVA: 0x000D7AC0 File Offset: 0x000D5CC0
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06002109 RID: 8457 RVA: 0x000D7AC8 File Offset: 0x000D5CC8
			internal bool IsEqualTo(Model.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.StorageLocation, other.StorageLocation) && PropertyHelper.AreValuesIdentical(this.DefaultMode, other.DefaultMode) && PropertyHelper.AreValuesIdentical(this.DefaultDataView, other.DefaultDataView) && PropertyHelper.AreValuesIdentical(this.Culture, other.Culture) && PropertyHelper.AreValuesIdentical(this.Collation, other.Collation) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && PropertyHelper.AreValuesIdentical(this.DataAccessOptions, other.DataAccessOptions) && PropertyHelper.AreValuesIdentical(this.DefaultPowerBIDataSourceVersion, other.DefaultPowerBIDataSourceVersion) && PropertyHelper.AreValuesIdentical(this.ForceUniqueNames, other.ForceUniqueNames) && PropertyHelper.AreValuesIdentical(this.DiscourageImplicitMeasures, other.DiscourageImplicitMeasures) && PropertyHelper.AreValuesIdentical(this.DiscourageReportMeasures, other.DiscourageReportMeasures) && PropertyHelper.AreValuesIdentical(this.DataSourceVariablesOverrideBehavior, other.DataSourceVariablesOverrideBehavior) && PropertyHelper.AreValuesIdentical(this.DataSourceDefaultMaxConnections, other.DataSourceDefaultMaxConnections) && PropertyHelper.AreValuesIdentical(this.SourceQueryCulture, other.SourceQueryCulture) && PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes) && PropertyHelper.AreValuesIdentical(this.DiscourageCompositeModels, other.DiscourageCompositeModels) && PropertyHelper.AreValuesIdentical(this.AutomaticAggregationOptions, other.AutomaticAggregationOptions) && PropertyHelper.AreValuesIdentical(this.DisableAutoExists, other.DisableAutoExists) && PropertyHelper.AreValuesIdentical(this.MaxParallelismPerRefresh, other.MaxParallelismPerRefresh) && PropertyHelper.AreValuesIdentical(this.MaxParallelismPerQuery, other.MaxParallelismPerQuery) && PropertyHelper.AreValuesIdentical(this.DirectLakeBehavior, other.DirectLakeBehavior) && PropertyHelper.AreValuesIdentical(this.ValueFilterBehavior, other.ValueFilterBehavior) && this.DefaultMeasureID.IsEqualTo(other.DefaultMeasureID, context);
			}

			// Token: 0x0600210A RID: 8458 RVA: 0x000D7D20 File Offset: 0x000D5F20
			internal void CopyFromImpl(Model.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.StorageLocation = other.StorageLocation;
				this.DefaultMode = other.DefaultMode;
				this.DefaultDataView = other.DefaultDataView;
				if (!string.IsNullOrEmpty(other.Culture))
				{
					this.Culture = other.Culture;
				}
				if (!string.IsNullOrEmpty(other.Collation))
				{
					this.Collation = other.Collation;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.StructureModifiedTime = other.StructureModifiedTime;
				}
				this.DataAccessOptions = other.DataAccessOptions;
				this.DefaultPowerBIDataSourceVersion = other.DefaultPowerBIDataSourceVersion;
				this.ForceUniqueNames = other.ForceUniqueNames;
				this.DiscourageImplicitMeasures = other.DiscourageImplicitMeasures;
				this.DiscourageReportMeasures = other.DiscourageReportMeasures;
				this.DataSourceVariablesOverrideBehavior = other.DataSourceVariablesOverrideBehavior;
				this.DataSourceDefaultMaxConnections = other.DataSourceDefaultMaxConnections;
				this.SourceQueryCulture = other.SourceQueryCulture;
				this.MAttributes = other.MAttributes;
				this.DiscourageCompositeModels = other.DiscourageCompositeModels;
				this.AutomaticAggregationOptions = other.AutomaticAggregationOptions;
				this.DisableAutoExists = other.DisableAutoExists;
				this.MaxParallelismPerRefresh = other.MaxParallelismPerRefresh;
				this.MaxParallelismPerQuery = other.MaxParallelismPerQuery;
				this.DirectLakeBehavior = other.DirectLakeBehavior;
				this.ValueFilterBehavior = other.ValueFilterBehavior;
				this.DefaultMeasureID.CopyFrom(other.DefaultMeasureID, context);
			}

			// Token: 0x0600210B RID: 8459 RVA: 0x000D7EC4 File Offset: 0x000D60C4
			internal void CopyFromImpl(Model.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.StorageLocation = other.StorageLocation;
				this.DefaultMode = other.DefaultMode;
				this.DefaultDataView = other.DefaultDataView;
				this.Culture = other.Culture;
				this.Collation = other.Collation;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.DataAccessOptions = other.DataAccessOptions;
				this.DefaultPowerBIDataSourceVersion = other.DefaultPowerBIDataSourceVersion;
				this.ForceUniqueNames = other.ForceUniqueNames;
				this.DiscourageImplicitMeasures = other.DiscourageImplicitMeasures;
				this.DiscourageReportMeasures = other.DiscourageReportMeasures;
				this.DataSourceVariablesOverrideBehavior = other.DataSourceVariablesOverrideBehavior;
				this.DataSourceDefaultMaxConnections = other.DataSourceDefaultMaxConnections;
				this.SourceQueryCulture = other.SourceQueryCulture;
				this.MAttributes = other.MAttributes;
				this.DiscourageCompositeModels = other.DiscourageCompositeModels;
				this.AutomaticAggregationOptions = other.AutomaticAggregationOptions;
				this.DisableAutoExists = other.DisableAutoExists;
				this.MaxParallelismPerRefresh = other.MaxParallelismPerRefresh;
				this.MaxParallelismPerQuery = other.MaxParallelismPerQuery;
				this.DirectLakeBehavior = other.DirectLakeBehavior;
				this.ValueFilterBehavior = other.ValueFilterBehavior;
				this.DefaultMeasureID.CopyFrom(other.DefaultMeasureID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600210C RID: 8460 RVA: 0x000D8013 File Offset: 0x000D6213
			public override void CopyFrom(MetadataObjectBody<Model> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Model.ObjectBody)other, context);
			}

			// Token: 0x0600210D RID: 8461 RVA: 0x000D802C File Offset: 0x000D622C
			internal bool IsEqualTo(Model.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.StorageLocation, other.StorageLocation) && PropertyHelper.AreValuesIdentical(this.DefaultMode, other.DefaultMode) && PropertyHelper.AreValuesIdentical(this.DefaultDataView, other.DefaultDataView) && PropertyHelper.AreValuesIdentical(this.Culture, other.Culture) && PropertyHelper.AreValuesIdentical(this.Collation, other.Collation) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.DataAccessOptions, other.DataAccessOptions) && PropertyHelper.AreValuesIdentical(this.DefaultPowerBIDataSourceVersion, other.DefaultPowerBIDataSourceVersion) && PropertyHelper.AreValuesIdentical(this.ForceUniqueNames, other.ForceUniqueNames) && PropertyHelper.AreValuesIdentical(this.DiscourageImplicitMeasures, other.DiscourageImplicitMeasures) && PropertyHelper.AreValuesIdentical(this.DiscourageReportMeasures, other.DiscourageReportMeasures) && PropertyHelper.AreValuesIdentical(this.DataSourceVariablesOverrideBehavior, other.DataSourceVariablesOverrideBehavior) && PropertyHelper.AreValuesIdentical(this.DataSourceDefaultMaxConnections, other.DataSourceDefaultMaxConnections) && PropertyHelper.AreValuesIdentical(this.SourceQueryCulture, other.SourceQueryCulture) && PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes) && PropertyHelper.AreValuesIdentical(this.DiscourageCompositeModels, other.DiscourageCompositeModels) && PropertyHelper.AreValuesIdentical(this.AutomaticAggregationOptions, other.AutomaticAggregationOptions) && PropertyHelper.AreValuesIdentical(this.DisableAutoExists, other.DisableAutoExists) && PropertyHelper.AreValuesIdentical(this.MaxParallelismPerRefresh, other.MaxParallelismPerRefresh) && PropertyHelper.AreValuesIdentical(this.MaxParallelismPerQuery, other.MaxParallelismPerQuery) && PropertyHelper.AreValuesIdentical(this.DirectLakeBehavior, other.DirectLakeBehavior) && PropertyHelper.AreValuesIdentical(this.ValueFilterBehavior, other.ValueFilterBehavior) && this.DefaultMeasureID.IsEqualTo(other.DefaultMeasureID);
			}

			// Token: 0x0600210E RID: 8462 RVA: 0x000D825C File Offset: 0x000D645C
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				base.Owner.BeforeBodyCompareWith();
				return base.IsEqualTo(other) && this.IsEqualTo((Model.ObjectBody)other);
			}

			// Token: 0x0600210F RID: 8463 RVA: 0x000D8280 File Offset: 0x000D6480
			internal void CompareWith(Model.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StorageLocation, other.StorageLocation))
				{
					context.RegisterPropertyChange(base.Owner, "StorageLocation", typeof(string), PropertyFlags.DdlAndUser, other.StorageLocation, this.StorageLocation);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DefaultMode, other.DefaultMode))
				{
					context.RegisterPropertyChange(base.Owner, "DefaultMode", typeof(ModeType), PropertyFlags.DdlAndUser, other.DefaultMode, this.DefaultMode);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DefaultDataView, other.DefaultDataView))
				{
					context.RegisterPropertyChange(base.Owner, "DefaultDataView", typeof(DataViewType), PropertyFlags.DdlAndUser, other.DefaultDataView, this.DefaultDataView);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Culture, other.Culture))
				{
					context.RegisterPropertyChange(base.Owner, "Culture", typeof(string), PropertyFlags.DdlAndUser, other.Culture, this.Culture);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Collation, other.Collation))
				{
					context.RegisterPropertyChange(base.Owner, "Collation", typeof(string), PropertyFlags.DdlAndUser, other.Collation, this.Collation);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DataAccessOptions, other.DataAccessOptions))
				{
					context.RegisterPropertyChange(base.Owner, "DataAccessOptions", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.DataAccessOptions, this.DataAccessOptions);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DefaultPowerBIDataSourceVersion, other.DefaultPowerBIDataSourceVersion))
				{
					context.RegisterPropertyChange(base.Owner, "DefaultPowerBIDataSourceVersion", typeof(PowerBIDataSourceVersion), PropertyFlags.DdlAndUser, other.DefaultPowerBIDataSourceVersion, this.DefaultPowerBIDataSourceVersion);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ForceUniqueNames, other.ForceUniqueNames))
				{
					context.RegisterPropertyChange(base.Owner, "ForceUniqueNames", typeof(bool), PropertyFlags.DdlAndUser, other.ForceUniqueNames, this.ForceUniqueNames);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DiscourageImplicitMeasures, other.DiscourageImplicitMeasures))
				{
					context.RegisterPropertyChange(base.Owner, "DiscourageImplicitMeasures", typeof(bool), PropertyFlags.DdlAndUser, other.DiscourageImplicitMeasures, this.DiscourageImplicitMeasures);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DiscourageReportMeasures, other.DiscourageReportMeasures))
				{
					context.RegisterPropertyChange(base.Owner, "DiscourageReportMeasures", typeof(bool), PropertyFlags.DdlAndUser, other.DiscourageReportMeasures, this.DiscourageReportMeasures);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DataSourceVariablesOverrideBehavior, other.DataSourceVariablesOverrideBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "DataSourceVariablesOverrideBehavior", typeof(DataSourceVariablesOverrideBehaviorType), PropertyFlags.DdlAndUser, other.DataSourceVariablesOverrideBehavior, this.DataSourceVariablesOverrideBehavior);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DataSourceDefaultMaxConnections, other.DataSourceDefaultMaxConnections))
				{
					context.RegisterPropertyChange(base.Owner, "DataSourceDefaultMaxConnections", typeof(int), PropertyFlags.DdlAndUser, other.DataSourceDefaultMaxConnections, this.DataSourceDefaultMaxConnections);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceQueryCulture, other.SourceQueryCulture))
				{
					context.RegisterPropertyChange(base.Owner, "SourceQueryCulture", typeof(string), PropertyFlags.DdlAndUser, other.SourceQueryCulture, this.SourceQueryCulture);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes))
				{
					context.RegisterPropertyChange(base.Owner, "MAttributes", typeof(string), PropertyFlags.DdlAndUser, other.MAttributes, this.MAttributes);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DiscourageCompositeModels, other.DiscourageCompositeModels))
				{
					context.RegisterPropertyChange(base.Owner, "DiscourageCompositeModels", typeof(bool), PropertyFlags.DdlAndUser, other.DiscourageCompositeModels, this.DiscourageCompositeModels);
				}
				if (!PropertyHelper.AreValuesIdentical(this.AutomaticAggregationOptions, other.AutomaticAggregationOptions))
				{
					context.RegisterPropertyChange(base.Owner, "AutomaticAggregationOptions", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.Json, other.AutomaticAggregationOptions, this.AutomaticAggregationOptions);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DisableAutoExists, other.DisableAutoExists))
				{
					context.RegisterPropertyChange(base.Owner, "DisableAutoExists", typeof(int), PropertyFlags.DdlAndUser, other.DisableAutoExists, this.DisableAutoExists);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MaxParallelismPerRefresh, other.MaxParallelismPerRefresh))
				{
					context.RegisterPropertyChange(base.Owner, "MaxParallelismPerRefresh", typeof(int), PropertyFlags.DdlAndUser, other.MaxParallelismPerRefresh, this.MaxParallelismPerRefresh);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MaxParallelismPerQuery, other.MaxParallelismPerQuery))
				{
					context.RegisterPropertyChange(base.Owner, "MaxParallelismPerQuery", typeof(int), PropertyFlags.DdlAndUser, other.MaxParallelismPerQuery, this.MaxParallelismPerQuery);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DirectLakeBehavior, other.DirectLakeBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "DirectLakeBehavior", typeof(DirectLakeBehavior), PropertyFlags.DdlAndUser, other.DirectLakeBehavior, this.DirectLakeBehavior);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ValueFilterBehavior, other.ValueFilterBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "ValueFilterBehavior", typeof(ValueFilterBehaviorType), PropertyFlags.DdlAndUser, other.ValueFilterBehavior, this.ValueFilterBehavior);
				}
				this.DefaultMeasureID.CompareWith(other.DefaultMeasureID, "DefaultMeasureID", "DefaultMeasure", PropertyFlags.ModelReference, context);
			}

			// Token: 0x06002110 RID: 8464 RVA: 0x000D891B File Offset: 0x000D6B1B
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.Owner.BeforeBodyCompareWith();
				base.CompareWith(other, context);
				this.CompareWith((Model.ObjectBody)other, context);
			}

			// Token: 0x06002111 RID: 8465 RVA: 0x000D893D File Offset: 0x000D6B3D
			public static Model.ObjectBody GetBodyForNewModel(Model model)
			{
				return new Model.ObjectBody(model)
				{
					IsNewModelBody = true,
					Id = ObjectId.Model
				};
			}

			// Token: 0x17000713 RID: 1811
			// (get) Token: 0x06002112 RID: 8466 RVA: 0x000D8957 File Offset: 0x000D6B57
			// (set) Token: 0x06002113 RID: 8467 RVA: 0x000D895F File Offset: 0x000D6B5F
			public bool IsNewModelBody { get; private set; }

			// Token: 0x040008CC RID: 2252
			internal string Name;

			// Token: 0x040008CD RID: 2253
			internal string Description;

			// Token: 0x040008CE RID: 2254
			internal string StorageLocation;

			// Token: 0x040008CF RID: 2255
			internal ModeType DefaultMode;

			// Token: 0x040008D0 RID: 2256
			internal DataViewType DefaultDataView;

			// Token: 0x040008D1 RID: 2257
			internal string Culture;

			// Token: 0x040008D2 RID: 2258
			internal string Collation;

			// Token: 0x040008D3 RID: 2259
			internal DateTime ModifiedTime;

			// Token: 0x040008D4 RID: 2260
			internal DateTime StructureModifiedTime;

			// Token: 0x040008D5 RID: 2261
			internal string DataAccessOptions;

			// Token: 0x040008D6 RID: 2262
			internal PowerBIDataSourceVersion DefaultPowerBIDataSourceVersion;

			// Token: 0x040008D7 RID: 2263
			internal bool ForceUniqueNames;

			// Token: 0x040008D8 RID: 2264
			internal bool DiscourageImplicitMeasures;

			// Token: 0x040008D9 RID: 2265
			internal bool DiscourageReportMeasures;

			// Token: 0x040008DA RID: 2266
			internal DataSourceVariablesOverrideBehaviorType DataSourceVariablesOverrideBehavior;

			// Token: 0x040008DB RID: 2267
			internal int DataSourceDefaultMaxConnections;

			// Token: 0x040008DC RID: 2268
			internal string SourceQueryCulture;

			// Token: 0x040008DD RID: 2269
			internal string MAttributes;

			// Token: 0x040008DE RID: 2270
			internal bool DiscourageCompositeModels;

			// Token: 0x040008DF RID: 2271
			internal string AutomaticAggregationOptions;

			// Token: 0x040008E0 RID: 2272
			internal int DisableAutoExists;

			// Token: 0x040008E1 RID: 2273
			internal int MaxParallelismPerRefresh;

			// Token: 0x040008E2 RID: 2274
			internal int MaxParallelismPerQuery;

			// Token: 0x040008E3 RID: 2275
			internal DirectLakeBehavior DirectLakeBehavior;

			// Token: 0x040008E4 RID: 2276
			internal ValueFilterBehaviorType ValueFilterBehavior;

			// Token: 0x040008E5 RID: 2277
			internal CrossLink<Model, Measure> DefaultMeasureID;
		}

		// Token: 0x02000287 RID: 647
		private enum SaveChangesPhase
		{
			// Token: 0x040008E8 RID: 2280
			Initial,
			// Token: 0x040008E9 RID: 2281
			UnblockDatabase,
			// Token: 0x040008EA RID: 2282
			ApplyRefreshPolicyPartitionManagement,
			// Token: 0x040008EB RID: 2283
			CRUDOperations,
			// Token: 0x040008EC RID: 2284
			ApplyRefreshPolicyRefreshManagement,
			// Token: 0x040008ED RID: 2285
			RefreshMergeOperations,
			// Token: 0x040008EE RID: 2286
			DeferredMergeOperations,
			// Token: 0x040008EF RID: 2287
			Final
		}
	}
}
