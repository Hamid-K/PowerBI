using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000BE RID: 190
	public sealed class Table : NamedMetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x000605E1 File Offset: 0x0005E7E1
		public Table()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000605F4 File Offset: 0x0005E7F4
		internal Table(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00060604 File Offset: 0x0005E804
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Table.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.DataCategory = string.Empty;
			this.body.Description = string.Empty;
			this.body.ShowAsVariationsOnly = false;
			this.body.IsPrivate = false;
			this.body.AlternateSourcePrecedence = 0;
			this.body.ExcludeFromModelRefresh = false;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this.body.SystemManaged = false;
			this.body.ExcludeFromAutomaticAggregations = false;
			this._Columns = new ColumnCollection(this, comparer);
			this._Partitions = new PartitionCollection(this, comparer);
			this._Measures = new MeasureCollection(this, comparer);
			this._Hierarchies = new HierarchyCollection(this, comparer);
			this._Sets = new SetCollection(this, comparer);
			this._Annotations = new TableAnnotationCollection(this, comparer);
			this._ExtendedProperties = new TableExtendedPropertyCollection(this, comparer);
			this._ExcludedArtifacts = new TableExcludedArtifactCollection(this);
			this._ChangedProperties = new TableChangedPropertyCollection(this);
			this._Calendars = new CalendarCollection(this, comparer);
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00060735 File Offset: 0x0005E935
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Table;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00060738 File Offset: 0x0005E938
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0006074A File Offset: 0x0005E94A
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ModelID.Object;
			}
			internal set
			{
				if (this.body.ModelID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Table, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00060777 File Offset: 0x0005E977
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0006078C File Offset: 0x0005E98C
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Table, null, "Table object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isHidden", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("showAsVariationsOnly", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("showAsVariationsOnly", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Table_IsPrivate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("isPrivate", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isPrivate", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("alternateSourcePrecedence", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("alternateSourcePrecedence", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("excludeFromModelRefresh", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("excludeFromModelRefresh", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Table_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Table_SystemManaged.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("systemManaged", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("systemManaged", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("excludeFromAutomaticAggregations", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("excludeFromAutomaticAggregations", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("dataCategory", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("dataCategory", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (context.SerializationMode == MetadataSerializationMode.Json && context.PartitionsMergedWithTable)
				{
					Partition.WritePartitionPropertiesMetadataSchemaInMergedMode(context, writer);
				}
				if (CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("defaultDetailRowsDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "defaultDetailRowsDefinition", MetadataPropertyNature.ChildProperty, ObjectType.DetailRowsDefinition);
				}
				if (CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("refreshPolicy", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "refreshPolicy", MetadataPropertyNature.ChildProperty, ObjectType.RefreshPolicy);
				}
				if (CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("calculationGroup", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "calculationGroup", MetadataPropertyNature.ChildProperty, ObjectType.CalculationGroup);
				}
				if (writer.ShouldIncludeProperty("columns", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Inferred | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "columns", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Inferred | MetadataPropertyNature.Translatable, ObjectType.Column);
				}
				if ((context.SerializationMode != MetadataSerializationMode.Json || !context.PartitionsMergedWithTable) && writer.ShouldIncludeProperty("partitions", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "partitions", MetadataPropertyNature.ChildCollection, ObjectType.Partition);
				}
				if (writer.ShouldIncludeProperty("measures", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "measures", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Measure);
				}
				if (writer.ShouldIncludeProperty("hierarchies", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "hierarchies", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Hierarchy);
				}
				if (CompatibilityRestrictions.Set.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sets", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "sets", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Set);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (CompatibilityRestrictions.ChangedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("changedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "changedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ChangedProperty);
				}
				if (CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("excludedArtifacts", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "excludedArtifacts", MetadataPropertyNature.ChildCollection, ObjectType.ExcludedArtifact);
				}
				if (CompatibilityRestrictions.Calendar.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("calendars", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "calendars", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Calendar);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00060D34 File Offset: 0x0005EF34
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.ShowAsVariationsOnly)
			{
				int num = CompatibilityRestrictions.Table_ShowAsVariationsOnly[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ShowAsVariationsOnly");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.IsPrivate)
			{
				int num2 = CompatibilityRestrictions.Table_IsPrivate[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "IsPrivate");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.AlternateSourcePrecedence != 0)
			{
				int num3 = CompatibilityRestrictions.Table_AlternateSourcePrecedence[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "AlternateSourcePrecedence");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ExcludeFromModelRefresh)
			{
				int num4 = CompatibilityRestrictions.Table_ExcludeFromModelRefresh[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExcludeFromModelRefresh");
					requiredLevel = num4;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				int num5 = CompatibilityRestrictions.Table_LineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num5, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag");
					requiredLevel = num5;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				int num6 = CompatibilityRestrictions.Table_SourceLineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num6, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag");
					requiredLevel = num6;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.SystemManaged)
			{
				int num7 = CompatibilityRestrictions.Table_SystemManaged[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num7, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SystemManaged");
					requiredLevel = num7;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ExcludeFromAutomaticAggregations)
			{
				int num8 = CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num8, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExcludeFromAutomaticAggregations");
					requiredLevel = num8;
					int num9 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00060F8F File Offset: 0x0005F18F
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00060F97 File Offset: 0x0005F197
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Table.ObjectBody)value;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00060FA5 File Offset: 0x0005F1A5
		internal override ITxObjectBody CreateBody()
		{
			return new Table.ObjectBody(this);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00060FAD File Offset: 0x0005F1AD
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Table();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00060FB4 File Offset: 0x0005F1B4
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Tables;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00060FC4 File Offset: 0x0005F1C4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<Table, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			this.body.DefaultDetailRowsDefinitionID.ResolveById(objectMap, throwIfCantResolve);
			this.body.RefreshPolicyID.ResolveById(objectMap, throwIfCantResolve);
			this.body.CalculationGroupID.ResolveById(objectMap, throwIfCantResolve);
			if (model != null)
			{
				model.Tables.Add(this);
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0006102E File Offset: 0x0005F22E
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00061030 File Offset: 0x0005F230
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.DefaultDetailRowsDefinitionID.Object != null)
			{
				yield return this.body.DefaultDetailRowsDefinitionID.Object;
			}
			if (this.body.RefreshPolicyID.Object != null)
			{
				yield return this.body.RefreshPolicyID.Object;
			}
			if (this.body.CalculationGroupID.Object != null)
			{
				yield return this.body.CalculationGroupID.Object;
			}
			yield break;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00061040 File Offset: 0x0005F240
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Columns;
			yield return this._Partitions;
			yield return this._Measures;
			yield return this._Hierarchies;
			yield return this._Sets;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ExcludedArtifacts;
			yield return this._ChangedProperties;
			yield return this._Calendars;
			yield break;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00061050 File Offset: 0x0005F250
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			ObjectType objectType = child.ObjectType;
			if (objectType == ObjectType.DetailRowsDefinition)
			{
				base.ValidateCompatibilityRequirement(child, "DefaultDetailRowsDefinition", CompatibilityRestrictions.Table_DefaultDetailRowsDefinition);
				ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultDetailRowsDefinition", typeof(DetailRowsDefinition), this.body.DefaultDetailRowsDefinitionID.Object, child);
				DetailRowsDefinition @object = this.body.DefaultDetailRowsDefinitionID.Object;
				this.body.DefaultDetailRowsDefinitionID.Object = (DetailRowsDefinition)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultDetailRowsDefinition", typeof(DetailRowsDefinition), @object, child);
				return;
			}
			if (objectType == ObjectType.CalculationGroup)
			{
				base.ValidateCompatibilityRequirement(child, "CalculationGroup", CompatibilityRestrictions.Table_CalculationGroup);
				ObjectChangeTracker.RegisterPropertyChanging(this, "CalculationGroup", typeof(CalculationGroup), this.body.CalculationGroupID.Object, child);
				CalculationGroup object2 = this.body.CalculationGroupID.Object;
				this.body.CalculationGroupID.Object = (CalculationGroup)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "CalculationGroup", typeof(CalculationGroup), object2, child);
				return;
			}
			if (objectType != ObjectType.RefreshPolicy)
			{
				base.SetDirectChildImpl(child);
				return;
			}
			base.ValidateCompatibilityRequirement(child, "RefreshPolicy", CompatibilityRestrictions.Table_RefreshPolicy);
			ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshPolicy", typeof(RefreshPolicy), this.body.RefreshPolicyID.Object, child);
			RefreshPolicy object3 = this.body.RefreshPolicyID.Object;
			this.body.RefreshPolicyID.Object = (RefreshPolicy)child;
			ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshPolicy", typeof(RefreshPolicy), object3, child);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000611E8 File Offset: 0x0005F3E8
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			ObjectType objectType = child.ObjectType;
			if (objectType != ObjectType.DetailRowsDefinition)
			{
				if (objectType != ObjectType.CalculationGroup)
				{
					if (objectType != ObjectType.RefreshPolicy)
					{
						base.RemoveDirectChildImpl(child);
					}
					else if (this.body.RefreshPolicyID.ObjectID == child.Id)
					{
						ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshPolicy", typeof(RefreshPolicy), this.body.RefreshPolicyID.Object, null);
						base.ResetCompatibilityRequirement();
						RefreshPolicy @object = this.body.RefreshPolicyID.Object;
						this.body.RefreshPolicyID.Object = null;
						ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshPolicy", typeof(RefreshPolicy), @object, null);
						return;
					}
				}
				else if (this.body.CalculationGroupID.ObjectID == child.Id)
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "CalculationGroup", typeof(CalculationGroup), this.body.CalculationGroupID.Object, null);
					base.ResetCompatibilityRequirement();
					CalculationGroup object2 = this.body.CalculationGroupID.Object;
					this.body.CalculationGroupID.Object = null;
					ObjectChangeTracker.RegisterPropertyChanged(this, "CalculationGroup", typeof(CalculationGroup), object2, null);
					return;
				}
			}
			else if (this.body.DefaultDetailRowsDefinitionID.ObjectID == child.Id)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultDetailRowsDefinition", typeof(DetailRowsDefinition), this.body.DefaultDetailRowsDefinitionID.Object, null);
				base.ResetCompatibilityRequirement();
				DetailRowsDefinition object3 = this.body.DefaultDetailRowsDefinitionID.Object;
				this.body.DefaultDetailRowsDefinitionID.Object = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultDetailRowsDefinition", typeof(DetailRowsDefinition), object3, null);
				return;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000613AC File Offset: 0x0005F5AC
		public ColumnCollection Columns
		{
			get
			{
				return this._Columns;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000613B4 File Offset: 0x0005F5B4
		public PartitionCollection Partitions
		{
			get
			{
				return this._Partitions;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x000613BC File Offset: 0x0005F5BC
		public MeasureCollection Measures
		{
			get
			{
				return this._Measures;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x000613C4 File Offset: 0x0005F5C4
		public HierarchyCollection Hierarchies
		{
			get
			{
				return this._Hierarchies;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000613CC File Offset: 0x0005F5CC
		[CompatibilityRequirement(Pbi = "1400")]
		public SetCollection Sets
		{
			get
			{
				return this._Sets;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000613D4 File Offset: 0x0005F5D4
		public TableAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000613DC File Offset: 0x0005F5DC
		[CompatibilityRequirement("1400")]
		public TableExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000613E4 File Offset: 0x0005F5E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public TableExcludedArtifactCollection ExcludedArtifacts
		{
			get
			{
				return this._ExcludedArtifacts;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x000613EC File Offset: 0x0005F5EC
		[CompatibilityRequirement("1567")]
		public TableChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000613F4 File Offset: 0x0005F5F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public CalendarCollection Calendars
		{
			get
			{
				return this._Calendars;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000613FC File Offset: 0x0005F5FC
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0006140C File Offset: 0x0005F60C
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Table, out text))
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0006148E File Offset: 0x0005F68E
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0006149C File Offset: 0x0005F69C
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

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0006150C File Offset: 0x0005F70C
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0006151C File Offset: 0x0005F71C
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

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0006158C File Offset: 0x0005F78C
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0006159C File Offset: 0x0005F79C
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00061620 File Offset: 0x0005F820
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00061630 File Offset: 0x0005F830
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

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000616B4 File Offset: 0x0005F8B4
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000616C4 File Offset: 0x0005F8C4
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

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00061748 File Offset: 0x0005F948
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00061758 File Offset: 0x0005F958
		[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
		public bool ShowAsVariationsOnly
		{
			get
			{
				return this.body.ShowAsVariationsOnly;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ShowAsVariationsOnly, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_ShowAsVariationsOnly, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ShowAsVariationsOnly"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ShowAsVariationsOnly", typeof(bool), this.body.ShowAsVariationsOnly, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_ShowAsVariationsOnly, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool showAsVariationsOnly = this.body.ShowAsVariationsOnly;
					this.body.ShowAsVariationsOnly = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ShowAsVariationsOnly", typeof(bool), showAsVariationsOnly, value);
				}
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0006181C File Offset: 0x0005FA1C
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0006182C File Offset: 0x0005FA2C
		[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
		public bool IsPrivate
		{
			get
			{
				return this.body.IsPrivate;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsPrivate, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_IsPrivate, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "IsPrivate"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsPrivate", typeof(bool), this.body.IsPrivate, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_IsPrivate, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool isPrivate = this.body.IsPrivate;
					this.body.IsPrivate = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsPrivate", typeof(bool), isPrivate, value);
				}
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000618F0 File Offset: 0x0005FAF0
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00061900 File Offset: 0x0005FB00
		[CompatibilityRequirement("1460")]
		public int AlternateSourcePrecedence
		{
			get
			{
				return this.body.AlternateSourcePrecedence;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.AlternateSourcePrecedence, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != 0)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_AlternateSourcePrecedence, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "AlternateSourcePrecedence"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "AlternateSourcePrecedence", typeof(int), this.body.AlternateSourcePrecedence, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_AlternateSourcePrecedence, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					int alternateSourcePrecedence = this.body.AlternateSourcePrecedence;
					this.body.AlternateSourcePrecedence = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "AlternateSourcePrecedence", typeof(int), alternateSourcePrecedence, value);
				}
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000619C4 File Offset: 0x0005FBC4
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x000619D4 File Offset: 0x0005FBD4
		[CompatibilityRequirement("1480")]
		public bool ExcludeFromModelRefresh
		{
			get
			{
				return this.body.ExcludeFromModelRefresh;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ExcludeFromModelRefresh, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_ExcludeFromModelRefresh, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExcludeFromModelRefresh"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ExcludeFromModelRefresh", typeof(bool), this.body.ExcludeFromModelRefresh, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_ExcludeFromModelRefresh, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool excludeFromModelRefresh = this.body.ExcludeFromModelRefresh;
					this.body.ExcludeFromModelRefresh = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ExcludeFromModelRefresh", typeof(bool), excludeFromModelRefresh, value);
				}
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00061A98 File Offset: 0x0005FC98
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00061AA8 File Offset: 0x0005FCA8
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_LineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_LineageTag, array);
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00061B5D File Offset: 0x0005FD5D
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00061B6C File Offset: 0x0005FD6C
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_SourceLineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_SourceLineageTag, array);
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00061C21 File Offset: 0x0005FE21
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00061C30 File Offset: 0x0005FE30
		[CompatibilityRequirement("1562")]
		public bool SystemManaged
		{
			get
			{
				return this.body.SystemManaged;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SystemManaged, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_SystemManaged, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SystemManaged"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SystemManaged", typeof(bool), this.body.SystemManaged, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_SystemManaged, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool systemManaged = this.body.SystemManaged;
					this.body.SystemManaged = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SystemManaged", typeof(bool), systemManaged, value);
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00061CF4 File Offset: 0x0005FEF4
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00061D04 File Offset: 0x0005FF04
		[CompatibilityRequirement("1572")]
		public bool ExcludeFromAutomaticAggregations
		{
			get
			{
				return this.body.ExcludeFromAutomaticAggregations;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ExcludeFromAutomaticAggregations, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExcludeFromAutomaticAggregations"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ExcludeFromAutomaticAggregations", typeof(bool), this.body.ExcludeFromAutomaticAggregations, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool excludeFromAutomaticAggregations = this.body.ExcludeFromAutomaticAggregations;
					this.body.ExcludeFromAutomaticAggregations = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ExcludeFromAutomaticAggregations", typeof(bool), excludeFromAutomaticAggregations, value);
				}
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00061DC8 File Offset: 0x0005FFC8
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00061DDA File Offset: 0x0005FFDA
		internal ObjectId _ModelID
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
			set
			{
				this.body.ModelID.ObjectID = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00061DED File Offset: 0x0005FFED
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00061E00 File Offset: 0x00060000
		[CompatibilityRequirement("1400")]
		public DetailRowsDefinition DefaultDetailRowsDefinition
		{
			get
			{
				return this.body.DefaultDetailRowsDefinitionID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultDetailRowsDefinitionID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "DefaultDetailRowsDefinition", CompatibilityRestrictions.Table_DefaultDetailRowsDefinition);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultDetailRowsDefinition", typeof(DetailRowsDefinition), this.body.DefaultDetailRowsDefinitionID.Object, value);
					DetailRowsDefinition @object = this.body.DefaultDetailRowsDefinitionID.Object;
					this.body.DefaultDetailRowsDefinitionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultDetailRowsDefinition", typeof(DetailRowsDefinition), @object, value);
				}
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00061E99 File Offset: 0x00060099
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00061EAB File Offset: 0x000600AB
		internal ObjectId _DefaultDetailRowsDefinitionID
		{
			get
			{
				return this.body.DefaultDetailRowsDefinitionID.ObjectID;
			}
			set
			{
				this.body.DefaultDetailRowsDefinitionID.ObjectID = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00061EBE File Offset: 0x000600BE
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00061ED0 File Offset: 0x000600D0
		[CompatibilityRequirement("1450")]
		public RefreshPolicy RefreshPolicy
		{
			get
			{
				return this.body.RefreshPolicyID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RefreshPolicyID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "RefreshPolicy", CompatibilityRestrictions.Table_RefreshPolicy);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshPolicy", typeof(RefreshPolicy), this.body.RefreshPolicyID.Object, value);
					RefreshPolicy @object = this.body.RefreshPolicyID.Object;
					this.body.RefreshPolicyID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshPolicy", typeof(RefreshPolicy), @object, value);
				}
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00061F69 File Offset: 0x00060169
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00061F7B File Offset: 0x0006017B
		internal ObjectId _RefreshPolicyID
		{
			get
			{
				return this.body.RefreshPolicyID.ObjectID;
			}
			set
			{
				this.body.RefreshPolicyID.ObjectID = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00061F8E File Offset: 0x0006018E
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00061FA0 File Offset: 0x000601A0
		[CompatibilityRequirement("1470")]
		public CalculationGroup CalculationGroup
		{
			get
			{
				return this.body.CalculationGroupID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.CalculationGroupID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "CalculationGroup", CompatibilityRestrictions.Table_CalculationGroup);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "CalculationGroup", typeof(CalculationGroup), this.body.CalculationGroupID.Object, value);
					CalculationGroup @object = this.body.CalculationGroupID.Object;
					this.body.CalculationGroupID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "CalculationGroup", typeof(CalculationGroup), @object, value);
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00062039 File Offset: 0x00060239
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0006204B File Offset: 0x0006024B
		internal ObjectId _CalculationGroupID
		{
			get
			{
				return this.body.CalculationGroupID.ObjectID;
			}
			set
			{
				this.body.CalculationGroupID.ObjectID = value;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00062060 File Offset: 0x00060260
		internal void CopyFrom(Table other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			else if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				if (this.body.DefaultDetailRowsDefinitionID.Object != null && other.body.DefaultDetailRowsDefinitionID.Object != null)
				{
					this.body.DefaultDetailRowsDefinitionID.Object.CopyFrom(other.body.DefaultDetailRowsDefinitionID.Object, context);
				}
				if (this.body.RefreshPolicyID.Object != null && other.body.RefreshPolicyID.Object != null)
				{
					this.body.RefreshPolicyID.Object.CopyFrom(other.body.RefreshPolicyID.Object, context);
				}
				if (this.body.CalculationGroupID.Object != null && other.body.CalculationGroupID.Object != null)
				{
					this.body.CalculationGroupID.Object.CopyFrom(other.body.CalculationGroupID.Object, context);
				}
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Columns.CopyFrom(other.Columns, context);
				this.Partitions.CopyFrom(other.Partitions, context);
				this.Measures.CopyFrom(other.Measures, context);
				this.Hierarchies.CopyFrom(other.Hierarchies, context);
				this.Sets.CopyFrom(other.Sets, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ExcludedArtifacts.CopyFrom(other.ExcludedArtifacts, context);
				this.ChangedProperties.CopyFrom(other.ChangedProperties, context);
				this.Calendars.CopyFrom(other.Calendars, context);
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0006226B File Offset: 0x0006046B
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Table)other, context);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0006227A File Offset: 0x0006047A
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Table other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00062296 File Offset: 0x00060496
		public void CopyTo(Table other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000622B2 File Offset: 0x000604B2
		public Table Clone()
		{
			return base.CloneInternal<Table>();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000622BC File Offset: 0x000604BC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
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
			if (this.body.ShowAsVariationsOnly)
			{
				if (!CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ShowAsVariationsOnly is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "ShowAsVariationsOnly", this.body.ShowAsVariationsOnly);
			}
			if (this.body.IsPrivate)
			{
				if (!CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member IsPrivate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "IsPrivate", this.body.IsPrivate);
			}
			if (this.body.AlternateSourcePrecedence != 0)
			{
				if (!CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AlternateSourcePrecedence is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<int>(options, "AlternateSourcePrecedence", this.body.AlternateSourcePrecedence);
			}
			if (this.body.ExcludeFromModelRefresh)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromModelRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "ExcludeFromModelRefresh", this.body.ExcludeFromModelRefresh);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
			if (this.body.SystemManaged)
			{
				if (!CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SystemManaged is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "SystemManaged", this.body.SystemManaged);
			}
			if (this.body.ExcludeFromAutomaticAggregations)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromAutomaticAggregations is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "ExcludeFromAutomaticAggregations", this.body.ExcludeFromAutomaticAggregations);
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00062638 File Offset: 0x00060838
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("DefaultDetailRowsDefinitionID", out objectId2))
			{
				this.body.DefaultDetailRowsDefinitionID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("RefreshPolicyID", out objectId3))
			{
				this.body.RefreshPolicyID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("CalculationGroupID", out objectId4))
			{
				this.body.CalculationGroupID.ObjectID = objectId4;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("DataCategory", out text2))
			{
				this.body.DataCategory = text2;
			}
			string text3;
			if (reader.TryReadProperty<string>("Description", out text3))
			{
				this.body.Description = text3;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsHidden", out flag))
			{
				this.body.IsHidden = flag;
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
			bool flag2;
			if (CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("ShowAsVariationsOnly", out flag2))
			{
				this.body.ShowAsVariationsOnly = flag2;
			}
			bool flag3;
			if (CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("IsPrivate", out flag3))
			{
				this.body.IsPrivate = flag3;
			}
			int num;
			if (CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<int>("AlternateSourcePrecedence", out num))
			{
				this.body.AlternateSourcePrecedence = num;
			}
			bool flag4;
			if (CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("ExcludeFromModelRefresh", out flag4))
			{
				this.body.ExcludeFromModelRefresh = flag4;
			}
			string text4;
			if (CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("LineageTag", out text4))
			{
				this.body.LineageTag = text4;
			}
			string text5;
			if (CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceLineageTag", out text5))
			{
				this.body.SourceLineageTag = text5;
			}
			bool flag5;
			if (CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("SystemManaged", out flag5))
			{
				this.body.SystemManaged = flag5;
			}
			bool flag6;
			if (CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("ExcludeFromAutomaticAggregations", out flag6))
			{
				this.body.ExcludeFromAutomaticAggregations = flag6;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000628F0 File Offset: 0x00060AF0
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
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
			if (this.body.ShowAsVariationsOnly)
			{
				if (!CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ShowAsVariationsOnly is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ShowAsVariationsOnly", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("ShowAsVariationsOnly", MetadataPropertyNature.RegularProperty, this.body.ShowAsVariationsOnly);
				}
			}
			if (this.body.IsPrivate)
			{
				if (!CompatibilityRestrictions.Table_IsPrivate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member IsPrivate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("IsPrivate", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("IsPrivate", MetadataPropertyNature.RegularProperty, this.body.IsPrivate);
				}
			}
			if (this.body.AlternateSourcePrecedence != 0)
			{
				if (!CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AlternateSourcePrecedence is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("AlternateSourcePrecedence", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("AlternateSourcePrecedence", MetadataPropertyNature.RegularProperty, this.body.AlternateSourcePrecedence);
				}
			}
			if (this.body.ExcludeFromModelRefresh)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromModelRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ExcludeFromModelRefresh", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("ExcludeFromModelRefresh", MetadataPropertyNature.RegularProperty, this.body.ExcludeFromModelRefresh);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Table_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
				if (!CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (this.body.SystemManaged)
			{
				if (!CompatibilityRestrictions.Table_SystemManaged.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SystemManaged is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SystemManaged", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("SystemManaged", MetadataPropertyNature.RegularProperty, this.body.SystemManaged);
				}
			}
			if (this.body.ExcludeFromAutomaticAggregations)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromAutomaticAggregations is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ExcludeFromAutomaticAggregations", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("ExcludeFromAutomaticAggregations", MetadataPropertyNature.RegularProperty, this.body.ExcludeFromAutomaticAggregations);
				}
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00062DB0 File Offset: 0x00060FB0
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("isHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("structureModifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.StructureModifiedTime);
			}
			if (this.body.ShowAsVariationsOnly)
			{
				if (!CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ShowAsVariationsOnly is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("showAsVariationsOnly", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("showAsVariationsOnly", MetadataPropertyNature.RegularProperty, this.body.ShowAsVariationsOnly);
				}
			}
			if (this.body.IsPrivate)
			{
				if (!CompatibilityRestrictions.Table_IsPrivate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member IsPrivate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("isPrivate", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("isPrivate", MetadataPropertyNature.RegularProperty, this.body.IsPrivate);
				}
			}
			if (this.body.AlternateSourcePrecedence != 0)
			{
				if (!CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AlternateSourcePrecedence is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("alternateSourcePrecedence", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteInt32Property("alternateSourcePrecedence", MetadataPropertyNature.RegularProperty, this.body.AlternateSourcePrecedence);
				}
			}
			if (this.body.ExcludeFromModelRefresh)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromModelRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("excludeFromModelRefresh", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("excludeFromModelRefresh", MetadataPropertyNature.RegularProperty, this.body.ExcludeFromModelRefresh);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Table_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
				if (!CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (this.body.SystemManaged)
			{
				if (!CompatibilityRestrictions.Table_SystemManaged.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SystemManaged is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("systemManaged", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("systemManaged", MetadataPropertyNature.RegularProperty, this.body.SystemManaged);
				}
			}
			if (this.body.ExcludeFromAutomaticAggregations)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromAutomaticAggregations is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("excludeFromAutomaticAggregations", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("excludeFromAutomaticAggregations", MetadataPropertyNature.RegularProperty, this.body.ExcludeFromAutomaticAggregations);
				}
			}
			if (!string.IsNullOrEmpty(this.body.DataCategory) && writer.ShouldIncludeProperty("dataCategory", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("dataCategory", MetadataPropertyNature.RegularProperty, this.body.DataCategory);
			}
			if (context.SerializationMode == MetadataSerializationMode.Json && context.PartitionsMergedWithTable)
			{
				this.WriteSinglePartitionPropertiesInMergedModeToMetadataStream(context, writer);
			}
			if (this.body.DefaultDetailRowsDefinitionID.Object != null)
			{
				if (!CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DefaultDetailRowsDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("defaultDetailRowsDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "defaultDetailRowsDefinition", MetadataPropertyNature.ChildProperty, this.body.DefaultDetailRowsDefinitionID.Object);
				}
			}
			if (this.body.RefreshPolicyID.Object != null)
			{
				if (!CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RefreshPolicyID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("refreshPolicy", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "refreshPolicy", MetadataPropertyNature.ChildProperty, this.body.RefreshPolicyID.Object);
				}
			}
			if (this.body.CalculationGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member CalculationGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("calculationGroup", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "calculationGroup", MetadataPropertyNature.ChildProperty, this.body.CalculationGroupID.Object);
				}
			}
			if (this.Columns.Count > 0 && writer.ShouldIncludeProperty("columns", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Inferred | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "columns", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Inferred | MetadataPropertyNature.Translatable, this.Columns);
			}
			if ((context.SerializationMode != MetadataSerializationMode.Json || !context.PartitionsMergedWithTable) && this.Partitions.Count > 0 && writer.ShouldIncludeProperty("partitions", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "partitions", MetadataPropertyNature.ChildCollection, this.Partitions);
			}
			if (this.Measures.Count > 0 && writer.ShouldIncludeProperty("measures", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "measures", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Measures);
			}
			if (this.Hierarchies.Count > 0 && writer.ShouldIncludeProperty("hierarchies", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "hierarchies", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Hierarchies);
			}
			if (this.Sets.Count > 0)
			{
				if (!CompatibilityRestrictions.Set.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child Set is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sets", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "sets", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Sets);
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
			if (this.Calendars.Count > 0)
			{
				if (!CompatibilityRestrictions.Calendar.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child Calendar is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("calendars", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "calendars", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Calendars);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00063800 File Offset: 0x00061A00
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
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "sets"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Set.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Set set in reader.ReadChildCollectionProperty<Set>(context))
								{
									try
									{
										this.Sets.Add(set);
									}
									catch (Exception ex)
									{
										throw reader.CreateInvalidChildException(context, set, TomSR.Exception_FailedAddDeserializedNamedObject("Set", (set != null) ? set.Name : null, ex.Message), ex);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "name"))
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
				case 7:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c == 'c')
						{
							if (propertyName == "columns")
							{
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (Column column in reader.ReadChildCollectionProperty<Column>(context))
									{
										try
										{
											this.Columns.Add(column);
										}
										catch (Exception ex2)
										{
											throw reader.CreateInvalidChildException(context, column, TomSR.Exception_FailedAddDeserializedNamedObject("Column", (column != null) ? column.Name : null, ex2.Message), ex2);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "ModelID")
					{
						this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 8:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'i')
						{
							if (c != 'm')
							{
								break;
							}
							if (!(propertyName == "measures"))
							{
								break;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Measure measure in reader.ReadChildCollectionProperty<Measure>(context))
								{
									try
									{
										this.Measures.Add(measure);
									}
									catch (Exception ex3)
									{
										throw reader.CreateInvalidChildException(context, measure, TomSR.Exception_FailedAddDeserializedNamedObject("Measure", (measure != null) ? measure.Name : null, ex3.Message), ex3);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "isHidden"))
						{
							break;
						}
					}
					else if (!(propertyName == "IsHidden"))
					{
						break;
					}
					this.body.IsHidden = reader.ReadBooleanProperty();
					return true;
				}
				case 9:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'c')
						{
							if (c != 'i')
							{
								break;
							}
							if (!(propertyName == "isPrivate"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "calendars"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Calendar.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Calendar calendar in reader.ReadChildCollectionProperty<Calendar>(context))
								{
									try
									{
										this.Calendars.Add(calendar);
									}
									catch (Exception ex4)
									{
										throw reader.CreateInvalidChildException(context, calendar, TomSR.Exception_FailedAddDeserializedNamedObject("Calendar", (calendar != null) ? calendar.Name : null, ex4.Message), ex4);
									}
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "IsPrivate"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_IsPrivate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.IsPrivate = reader.ReadBooleanProperty();
					return true;
				}
				case 10:
				{
					char c = propertyName[0];
					if (c != 'L')
					{
						if (c != 'l')
						{
							if (c != 'p')
							{
								break;
							}
							if (!(propertyName == "partitions"))
							{
								break;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (Partition partition in reader.ReadChildCollectionProperty<Partition>(context))
								{
									try
									{
										this.Partitions.Add(partition);
									}
									catch (Exception ex5)
									{
										throw reader.CreateInvalidChildException(context, partition, TomSR.Exception_FailedAddDeserializedNamedObject("Partition", (partition != null) ? partition.Name : null, ex5.Message), ex5);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "lineageTag"))
						{
							break;
						}
					}
					else if (!(propertyName == "LineageTag"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
					if (c <= 'a')
					{
						if (c != 'D')
						{
							if (c != 'a')
							{
								break;
							}
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
									catch (Exception ex6)
									{
										throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex6.Message), ex6);
									}
								}
							}
							return true;
						}
						else if (!(propertyName == "Description"))
						{
							break;
						}
					}
					else if (c != 'd')
					{
						if (c != 'h')
						{
							break;
						}
						if (!(propertyName == "hierarchies"))
						{
							break;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Hierarchy hierarchy in reader.ReadChildCollectionProperty<Hierarchy>(context))
							{
								try
								{
									this.Hierarchies.Add(hierarchy);
								}
								catch (Exception ex7)
								{
									throw reader.CreateInvalidChildException(context, hierarchy, TomSR.Exception_FailedAddDeserializedNamedObject("Hierarchy", (hierarchy != null) ? hierarchy.Name : null, ex7.Message), ex7);
								}
							}
						}
						return true;
					}
					else if (!(propertyName == "description"))
					{
						break;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
				}
				case 12:
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
							if (!(propertyName == "ModifiedTime"))
							{
								break;
							}
							goto IL_0781;
						}
						else if (!(propertyName == "DataCategory"))
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
						if (!(propertyName == "modifiedTime"))
						{
							break;
						}
						goto IL_0781;
					}
					else if (!(propertyName == "dataCategory"))
					{
						break;
					}
					this.body.DataCategory = reader.ReadStringProperty();
					return true;
					IL_0781:
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 'r')
						{
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "systemManaged"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "refreshPolicy"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								RefreshPolicy refreshPolicy = reader.ReadSingleChildProperty<RefreshPolicy>(context);
								try
								{
									this.body.RefreshPolicyID.Object = refreshPolicy;
								}
								catch (Exception ex8)
								{
									throw reader.CreateInvalidChildException(context, refreshPolicy, TomSR.Exception_FailedAddDeserializedObject("RefreshPolicy", ex8.Message), ex8);
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "SystemManaged"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_SystemManaged.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.SystemManaged = reader.ReadBooleanProperty();
					return true;
				}
				case 15:
					if (propertyName == "RefreshPolicyID")
					{
						if (!CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.RefreshPolicyID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 16:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 'c')
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
							if (!(propertyName == "calculationGroup"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								CalculationGroup calculationGroup = reader.ReadSingleChildProperty<CalculationGroup>(context);
								try
								{
									this.body.CalculationGroupID.Object = calculationGroup;
								}
								catch (Exception ex9)
								{
									throw reader.CreateInvalidChildException(context, calculationGroup, TomSR.Exception_FailedAddDeserializedObject("CalculationGroup", ex9.Message), ex9);
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "SourceLineageTag"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.SourceLineageTag = reader.ReadStringProperty();
					return true;
				}
				case 17:
				{
					char c = propertyName[0];
					if (c != 'c')
					{
						if (c == 'e')
						{
							if (propertyName == "excludedArtifacts")
							{
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
										catch (Exception ex10)
										{
											throw reader.CreateInvalidChildException(context, excludedArtifact, TomSR.Exception_FailedAddDeserializedObject("ExcludedArtifact", ex10.Message), ex10);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "changedProperties")
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
								catch (Exception ex11)
								{
									throw reader.CreateInvalidChildException(context, changedProperty, TomSR.Exception_FailedAddDeserializedObject("ChangedProperty", ex11.Message), ex11);
								}
							}
						}
						return true;
					}
					break;
				}
				case 18:
				{
					char c = propertyName[0];
					if (c != 'C')
					{
						if (c == 'e')
						{
							if (propertyName == "extendedProperties")
							{
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
					}
					else if (propertyName == "CalculationGroupID")
					{
						if (!CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.CalculationGroupID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 20:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "showAsVariationsOnly"))
						{
							break;
						}
					}
					else if (!(propertyName == "ShowAsVariationsOnly"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.ShowAsVariationsOnly = reader.ReadBooleanProperty();
					return true;
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
				case 23:
				{
					char c = propertyName[0];
					if (c != 'E')
					{
						if (c != 'e')
						{
							break;
						}
						if (!(propertyName == "excludeFromModelRefresh"))
						{
							break;
						}
					}
					else if (!(propertyName == "ExcludeFromModelRefresh"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.ExcludeFromModelRefresh = reader.ReadBooleanProperty();
					return true;
				}
				case 25:
				{
					char c = propertyName[0];
					if (c != 'A')
					{
						if (c != 'a')
						{
							break;
						}
						if (!(propertyName == "alternateSourcePrecedence"))
						{
							break;
						}
					}
					else if (!(propertyName == "AlternateSourcePrecedence"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.AlternateSourcePrecedence = reader.ReadInt32Property();
					return true;
				}
				case 27:
					if (propertyName == "defaultDetailRowsDefinition")
					{
						if (!CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							DetailRowsDefinition detailRowsDefinition = reader.ReadSingleChildProperty<DetailRowsDefinition>(context);
							try
							{
								this.body.DefaultDetailRowsDefinitionID.Object = detailRowsDefinition;
							}
							catch (Exception ex13)
							{
								throw reader.CreateInvalidChildException(context, detailRowsDefinition, TomSR.Exception_FailedAddDeserializedObject("DetailRowsDefinition", ex13.Message), ex13);
							}
						}
						return true;
					}
					break;
				case 29:
					if (propertyName == "DefaultDetailRowsDefinitionID")
					{
						if (!CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.DefaultDetailRowsDefinitionID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 32:
				{
					char c = propertyName[0];
					if (c != 'E')
					{
						if (c != 'e')
						{
							break;
						}
						if (!(propertyName == "excludeFromAutomaticAggregations"))
						{
							break;
						}
					}
					else if (!(propertyName == "ExcludeFromAutomaticAggregations"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.ExcludeFromAutomaticAggregations = reader.ReadBooleanProperty();
					return true;
				}
				}
			}
			if (context.SerializationMode == MetadataSerializationMode.Json && context.PartitionsMergedWithTable)
			{
				if (this.TryReadSinglePartitionPropertyInMergedModeFromMetadataStream(context, reader, ref classification))
				{
					return true;
				}
				if (classification != UnexpectedPropertyClassification.Unclassified)
				{
					return false;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00064BB0 File Offset: 0x00062DB0
		[Obsolete("Deprecated. Use RequestRefresh method instead.", false)]
		public void Refresh(RefreshType type)
		{
			this.RequestRefresh(type);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00064BB9 File Offset: 0x00062DB9
		[Obsolete("Deprecated. Use RequestRefresh method instead.", false)]
		public void Refresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			this.RequestRefresh(type, overrides);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00064BC4 File Offset: 0x00062DC4
		public void RequestRefresh(RefreshType type)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null, this.body.RefreshPolicyID.Object != null && Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00064C0C File Offset: 0x00062E0C
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides, this.body.RefreshPolicyID.Object != null && Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00064C58 File Offset: 0x00062E58
		public void RequestRefresh(RefreshType type, RefreshPolicyBehavior behavior)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null, behavior != RefreshPolicyBehavior.Ignore && this.body.RefreshPolicyID.Object != null && Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00064CA4 File Offset: 0x00062EA4
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides, RefreshPolicyBehavior behavior)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides, behavior != RefreshPolicyBehavior.Ignore && this.body.RefreshPolicyID.Object != null && Utils.CanApplyRefreshPolicies(type), null, true);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00064CF4 File Offset: 0x00062EF4
		public void RequestRefresh(RefreshType type, DateTime effectiveDate)
		{
			if (this.body.RefreshPolicyID.Object == null)
			{
				throw new InvalidOperationException(TomSR.Exception_TableRefreshPolicyIsMissing(this.Name));
			}
			if (!Utils.CanApplyRefreshPolicies(type))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null, true, new DateTime?(effectiveDate), true);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00064D54 File Offset: 0x00062F54
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides, DateTime effectiveDate)
		{
			if (this.body.RefreshPolicyID.Object == null)
			{
				throw new InvalidOperationException(TomSR.Exception_TableRefreshPolicyIsMissing(this.Name));
			}
			if (!Utils.CanApplyRefreshPolicies(type))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides, true, new DateTime?(effectiveDate), true);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00064DB8 File Offset: 0x00062FB8
		internal void MarkForRefresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			if (this.body.Savepoint != base.Model.TxManager.CurrentSavepoint)
			{
				this.CloneBody(base.Model.TxManager.CurrentSavepoint);
			}
			this.body.RefreshRequested = true;
			this.body.RequestedRefreshMask = (this.body.RequestedRefreshMask |= Utils.ConvertRefreshTypeToMask(type));
			this.body.Overrides = overrides;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00064E37 File Offset: 0x00063037
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00064E40 File Offset: 0x00063040
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00064E64 File Offset: 0x00063064
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.DataCategory))
			{
				result["dataCategory", TomPropCategory.Regular, 103, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DataCategory, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsHidden)
			{
				result["isHidden", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsHidden);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ShowAsVariationsOnly)
			{
				if (!CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ShowAsVariationsOnly is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["showAsVariationsOnly", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.ShowAsVariationsOnly);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsPrivate)
			{
				if (!CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member IsPrivate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["isPrivate", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsPrivate);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.AlternateSourcePrecedence != 0)
			{
				if (!CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member AlternateSourcePrecedence is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["alternateSourcePrecedence", TomPropCategory.Regular, 13, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.AlternateSourcePrecedence);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ExcludeFromModelRefresh)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromModelRefresh is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["excludeFromModelRefresh", TomPropCategory.Regular, 16, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.ExcludeFromModelRefresh);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["lineageTag", TomPropCategory.Regular, 17, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceLineageTag", TomPropCategory.Regular, 18, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.SystemManaged)
			{
				if (!CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SystemManaged is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["systemManaged", TomPropCategory.Regular, 19, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.SystemManaged);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ExcludeFromAutomaticAggregations)
			{
				if (!CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExcludeFromAutomaticAggregations is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["excludeFromAutomaticAggregations", TomPropCategory.Regular, 20, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.ExcludeFromAutomaticAggregations);
			}
			this.SerializeAdditionalDataToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (this.body.DefaultDetailRowsDefinitionID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.DefaultDetailRowsDefinitionID.Object)))
				{
					if (!CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member DefaultDetailRowsDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["defaultDetailRowsDefinition", TomPropCategory.ChildLink, 12, false] = this.body.DefaultDetailRowsDefinitionID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.body.RefreshPolicyID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.RefreshPolicyID.Object)))
				{
					if (!CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member RefreshPolicyID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["refreshPolicy", TomPropCategory.ChildLink, 14, false] = this.body.RefreshPolicyID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.body.CalculationGroupID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.CalculationGroupID.Object)))
				{
					if (!CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member CalculationGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					result["calculationGroup", TomPropCategory.ChildLink, 15, false] = this.body.CalculationGroupID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				IEnumerable<Column> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Column> columns = this.Columns;
					enumerable = columns;
				}
				else
				{
					enumerable = this.Columns.Where((Column o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Column> enumerable2 = enumerable;
				if (enumerable2.Any<Column>())
				{
					object[] array = enumerable2.Select((Column obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["columns", TomPropCategory.ChildCollection, 3, false] = array2;
				}
				IEnumerable<Measure> enumerable3;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Measure> measures = this.Measures;
					enumerable3 = measures;
				}
				else
				{
					enumerable3 = this.Measures.Where((Measure o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Measure> enumerable4 = enumerable3;
				if (enumerable4.Any<Measure>())
				{
					object[] array = enumerable4.Select((Measure obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array3 = array;
					result["measures", TomPropCategory.ChildCollection, 7, false] = array3;
				}
				IEnumerable<Hierarchy> enumerable5;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Hierarchy> hierarchies = this.Hierarchies;
					enumerable5 = hierarchies;
				}
				else
				{
					enumerable5 = this.Hierarchies.Where((Hierarchy o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Hierarchy> enumerable6 = enumerable5;
				if (enumerable6.Any<Hierarchy>())
				{
					object[] array = enumerable6.Select((Hierarchy obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array4 = array;
					result["hierarchies", TomPropCategory.ChildCollection, 8, false] = array4;
				}
				IEnumerable<Set> enumerable7;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Set> sets = this.Sets;
					enumerable7 = sets;
				}
				else
				{
					enumerable7 = this.Sets.Where((Set o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Set> enumerable8 = enumerable7;
				if (enumerable8.Any<Set>())
				{
					if (!CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child Set is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable8.Select((Set obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array5 = array;
					result["sets", TomPropCategory.ChildCollection, 37, false] = array5;
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable9;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable9 = extendedProperties;
					}
					else
					{
						enumerable9 = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable10 = enumerable9;
					if (enumerable10.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable10.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array6 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array6;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExcludedArtifact> enumerable11;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExcludedArtifact> excludedArtifacts = this.ExcludedArtifacts;
						enumerable11 = excludedArtifacts;
					}
					else
					{
						enumerable11 = this.ExcludedArtifacts.Where((ExcludedArtifact o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExcludedArtifact> enumerable12 = enumerable11;
					if (enumerable12.Any<ExcludedArtifact>())
					{
						if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable12.Select((ExcludedArtifact obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array7 = array;
						result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array7;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ChangedProperty> enumerable13;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ChangedProperty> changedProperties = this.ChangedProperties;
						enumerable13 = changedProperties;
					}
					else
					{
						enumerable13 = this.ChangedProperties.Where((ChangedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ChangedProperty> enumerable14 = enumerable13;
					if (enumerable14.Any<ChangedProperty>())
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable14.Select((ChangedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array8 = array;
						result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array8;
					}
				}
				IEnumerable<Calendar> enumerable15;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Calendar> calendars = this.Calendars;
					enumerable15 = calendars;
				}
				else
				{
					enumerable15 = this.Calendars.Where((Calendar o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Calendar> enumerable16 = enumerable15;
				if (enumerable16.Any<Calendar>())
				{
					if (!CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child Calendar is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable16.Select((Calendar obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array9 = array;
					result["calendars", TomPropCategory.ChildCollection, 59, false] = array9;
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array10 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array10;
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00065CAC File Offset: 0x00063EAC
		private void SerializeAdditionalDataToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			Utils.Verify(!ObjectTreeHelper.HasTranslatableDescendants(ObjectType.Partition), "Serialization code for Partitions assumes they don't have translatable properties");
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
			{
				if (options.PartitionsMergedWithTable)
				{
					this.SerializeSinglePartitionInMergedMode(jsonObj, options, mode, dbCompatibilityLevel);
					return;
				}
				this.SerializePartitionsInNormalMode(jsonObj, options, mode, dbCompatibilityLevel);
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00065D04 File Offset: 0x00063F04
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
						if (c == 's')
						{
							if (name == "sets")
							{
								if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								JsonPropertyHelper.ReadObjectCollection(this.Sets, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
						}
					}
					else if (name == "name")
					{
						this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 7:
					if (name == "columns")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Columns, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 8:
				{
					char c = name[0];
					if (c != 'i')
					{
						if (c == 'm')
						{
							if (name == "measures")
							{
								JsonPropertyHelper.ReadObjectCollection(this.Measures, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
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
				{
					char c = name[0];
					if (c != 'c')
					{
						if (c == 'i')
						{
							if (name == "isPrivate")
							{
								if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.IsPrivate = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "calendars")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.Calendars, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 10:
					if (name == "lineageTag")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.LineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 11:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c != 'd')
						{
							if (c == 'h')
							{
								if (name == "hierarchies")
								{
									JsonPropertyHelper.ReadObjectCollection(this.Hierarchies, jsonProp.Value, options, mode, dbCompatibilityLevel);
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
					if (c != 'd')
					{
						if (c == 'm')
						{
							if (name == "modifiedTime")
							{
								this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "dataCategory")
					{
						this.body.DataCategory = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 13:
					if (name == "systemManaged")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.SystemManaged = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 16:
				{
					char c = name[0];
					if (c != 'c')
					{
						if (c == 's')
						{
							if (name == "sourceLineageTag")
							{
								if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "calculationGroup")
					{
						if (jsonProp.Value.Type != 10)
						{
							if (!CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							CalculationGroup calculationGroup = new CalculationGroup();
							calculationGroup.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.CalculationGroupID.Object = calculationGroup;
						}
						return true;
					}
					break;
				}
				case 17:
				{
					char c = name[0];
					if (c != 'c')
					{
						if (c == 'e')
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
					else if (name == "changedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ChangedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 18:
					if (name == "extendedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 20:
					if (name == "showAsVariationsOnly")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.ShowAsVariationsOnly = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
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
				case 23:
					if (name == "excludeFromModelRefresh")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.ExcludeFromModelRefresh = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 25:
					if (name == "alternateSourcePrecedence")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.AlternateSourcePrecedence = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 27:
					if (name == "defaultDetailRowsDefinition")
					{
						if (jsonProp.Value.Type != 10)
						{
							if (!CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							DetailRowsDefinition detailRowsDefinition = new DetailRowsDefinition();
							detailRowsDefinition.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
							this.body.DefaultDetailRowsDefinitionID.Object = detailRowsDefinition;
						}
						return true;
					}
					break;
				case 32:
					if (name == "excludeFromAutomaticAggregations")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.ExcludeFromAutomaticAggregations = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				}
			}
			bool flag = false;
			this.ReadAdditionalPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel, ref flag);
			return flag;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000664E4 File Offset: 0x000646E4
		private void ReadAdditionalPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ref bool wasRead)
		{
			if (jsonProp.Name == "refreshPolicy")
			{
				if (jsonProp.Value.Type != 10)
				{
					jsonProp.Value.VerifyTokenType(1);
					JObject jobject = jsonProp.Value as JObject;
					Utils.Verify(jobject != null);
					RefreshPolicy refreshPolicy = ObjectFactory.CreateRefreshPolicyFromJsonObject(jobject);
					this.RefreshPolicy = refreshPolicy;
					foreach (JProperty jproperty in jobject.Properties())
					{
						if (!refreshPolicy.ReadPropertyFromJson(jproperty, options, mode, dbCompatibilityLevel))
						{
							throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(jsonProp.Name), jsonProp, null);
						}
					}
				}
				wasRead = true;
				return;
			}
			if (options.PartitionsMergedWithTable)
			{
				Utils.Verify(this.Partitions.Count == 1, "When deserializing with PartitionsMergedWithTable flag, Table must have exactly one partition.");
				Partition partition = this.Partitions[0];
				wasRead = partition.ReadPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel);
				return;
			}
			wasRead = this.ReadPartitionPropertiesInNormalMode(jsonProp, options, mode, dbCompatibilityLevel);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000665EC File Offset: 0x000647EC
		internal override IEnumerable<MetadataObject> GetNameLinkedObjects(string objectName = null)
		{
			if (objectName == null)
			{
				objectName = this.Name;
			}
			if (base.Model == null)
			{
				yield break;
			}
			foreach (Perspective perspective in base.Model.Perspectives)
			{
				PerspectiveTable perspectiveTable = perspective.PerspectiveTables.Find(objectName);
				if (perspectiveTable != null)
				{
					yield return perspectiveTable;
				}
			}
			IEnumerator<Perspective> enumerator = null;
			foreach (ModelRole modelRole in base.Model.Roles)
			{
				TablePermission tablePermission = modelRole.TablePermissions.Find(objectName);
				if (tablePermission != null)
				{
					yield return tablePermission;
				}
			}
			IEnumerator<ModelRole> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00066603 File Offset: 0x00064803
		public IReadOnlyList<ModelOperationResult> ApplyRefreshPolicy(bool refresh = true, int maxParallelism = 0)
		{
			return this.ApplyRefreshPolicy(DateTime.Now, refresh, maxParallelism);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00066614 File Offset: 0x00064814
		public IReadOnlyList<ModelOperationResult> ApplyRefreshPolicy(DateTime effectiveDate, bool refresh = true, int maxParallelism = 0)
		{
			if (base.Model == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotApplyRefreshPolicyDisconnectedTable);
			}
			if (base.Model.Server == null)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotApplyRefreshPolicyDisconnectedModel);
			}
			if (base.Model.HasLocalChanges)
			{
				throw new InvalidOperationException(TomSR.Exception_CannotApplyRefreshPolicyModifiedModel);
			}
			if (this.RefreshPolicy == null)
			{
				throw new InvalidOperationException(TomSR.Exception_TableRefreshPolicyIsMissing(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)));
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
				ModelOperationResult modelOperationResult = base.Model.SaveChangesImpl(SaveFlags.Default, maxParallelism);
				readOnlyList = new List<ModelOperationResult>(1) { modelOperationResult };
			}
			catch (InvalidOperationException)
			{
				if (base.Model.HasLocalChanges)
				{
					base.Model.UndoLocalChangesImpl();
				}
				throw;
			}
			return readOnlyList;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00066704 File Offset: 0x00064904
		internal void ValidateBeforeApplyRefreshPolicy()
		{
			if (this.RefreshPolicy.SourceExpression == null)
			{
				throw new InvalidOperationException(TomSR.Exception_RefreshPolicyInvalidSourceExpression(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)));
			}
			if (!this.RefreshPolicy.SourceExpression.Contains("RangeStart") || !this.RefreshPolicy.SourceExpression.Contains("RangeEnd"))
			{
				throw new InvalidOperationException(TomSR.Exception_RefreshPolicyInvalidSourceExpression(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)));
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0006677C File Offset: 0x0006497C
		internal void ApplyRefreshPolicyImpl(SaveContext context)
		{
			DateTime dateTime = ((this.body.ApplyRefreshPolicyEffectiveDate != null) ? this.body.ApplyRefreshPolicyEffectiveDate.Value : DateTime.Now);
			this.ApplyRefreshPolicyImpl(context, dateTime, this.body.RefreshAfterApplyRefreshPolicyRequested);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000667CC File Offset: 0x000649CC
		internal void ApplyRefreshPolicyImpl(SaveContext context, DateTime effectiveDate, bool refreshPartitions)
		{
			PartitionPolicyRangeMap partitionPolicyRangeMap = context.GeneratePartitionPolicyRangesMap(this, effectiveDate, refreshPartitions);
			foreach (Partition partition in partitionPolicyRangeMap.PartitionsToDelete)
			{
				this.Partitions.Remove(partition);
			}
			foreach (PolicyRangePartitionSource policyRangePartitionSource in partitionPolicyRangeMap.PartitionPolicyRangesToCreate)
			{
				Partition partition2 = new Partition();
				partition2.Name = policyRangePartitionSource.GeneratePartitionName(false);
				partition2.Source = policyRangePartitionSource;
				partition2.Mode = ModeType.Import;
				this.Partitions.Add(policyRangePartitionSource.Partition);
			}
			if (partitionPolicyRangeMap.DirectQueryPartitionToCreate != null)
			{
				Partition partition3 = new Partition();
				partition3.Name = partitionPolicyRangeMap.DirectQueryPartitionToCreate.GeneratePartitionName(true);
				partition3.Source = partitionPolicyRangeMap.DirectQueryPartitionToCreate;
				partition3.Mode = ModeType.DirectQuery;
				this.Partitions.Add(partitionPolicyRangeMap.DirectQueryPartitionToCreate.Partition);
			}
			if (refreshPartitions && partitionPolicyRangeMap.HasPollingExpression && partitionPolicyRangeMap.PartitionPolicyRangesToRefresh.Count > 0)
			{
				foreach (PolicyRangePartitionSource policyRangePartitionSource2 in partitionPolicyRangeMap.PartitionPolicyRangesToRefresh)
				{
					policyRangePartitionSource2.Partition.RequestRefreshPolicyImpact();
				}
			}
			this.body.RefreshAfterApplyRefreshPolicyRequested = refreshPartitions;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00066940 File Offset: 0x00064B40
		internal void ApplyRefreshPolicyRefreshManagement(SaveContext context)
		{
			if (this.body.RefreshAfterApplyRefreshPolicyRequested)
			{
				this.ApplyRefreshPolicyRefreshManagementImpl(context, Utils.ConvertRefreshMaskToType(this.body.RequestedRefreshMask).FirstOrDefault<RefreshType>(), this.body.Overrides);
			}
			this.body.RefreshRequested = false;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00066990 File Offset: 0x00064B90
		internal void ApplyRefreshPolicyRefreshManagementImpl(SaveContext context, RefreshType type, ICollection<OverrideCollection> overrides)
		{
			PartitionPolicyRangeMap partitionPolicyRangesMap = context.GetPartitionPolicyRangesMap(this);
			foreach (PolicyRangePartitionSource policyRangePartitionSource in partitionPolicyRangesMap.PartitionPolicyRangesToRefresh)
			{
				string text;
				if (context.UnblockingDatabase || !partitionPolicyRangesMap.HasPollingExpression)
				{
					policyRangePartitionSource.Partition.MarkForRefresh(type, overrides);
				}
				else if (!partitionPolicyRangesMap.OriginalRefreshBookmarks.TryGetValue(policyRangePartitionSource.Partition.Name, out text) || string.IsNullOrEmpty(policyRangePartitionSource.Partition.RefreshBookmark) || !string.Equals(policyRangePartitionSource.Partition.RefreshBookmark, text, StringComparison.InvariantCulture))
				{
					policyRangePartitionSource.Partition.MarkForRefresh(type, overrides);
				}
			}
			foreach (IList<PolicyRangePartitionSource> list in partitionPolicyRangesMap.PartitionPolicyRangesToMerge)
			{
				List<PolicyRangePartitionSource> list2 = (List<PolicyRangePartitionSource>)list;
				PolicyRangePartitionSource policyRangePartitionSource2 = null;
				foreach (PolicyRangePartitionSource policyRangePartitionSource3 in partitionPolicyRangesMap.PartitionPolicyRangesToCreate)
				{
					if (DateTime.Compare(policyRangePartitionSource3.Start, list2[0].Start) == 0)
					{
						policyRangePartitionSource2 = policyRangePartitionSource3;
					}
				}
				context.QueueRefreshPolicyRelatedPartitionsMerge(list2.Select((PolicyRangePartitionSource r) => r.Partition), policyRangePartitionSource2.Partition);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00066B1C File Offset: 0x00064D1C
		internal override void OnBeforeDeserialize(DeserializeOptions options)
		{
			base.OnBeforeDeserialize(options);
			if (options.PartitionsMergedWithTable)
			{
				Utils.Verify(this.Partitions.Count == 0);
				this.Partitions.Add(new Partition());
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00066B50 File Offset: 0x00064D50
		internal override void OnAfterDeserialize(DeserializeOptions options)
		{
			base.OnAfterDeserialize(options);
			if (options.PartitionsMergedWithTable)
			{
				Utils.Verify(this.Partitions.Count == 1);
				this.Partitions[0].Name = this.Name;
			}
			foreach (MetadataObject metadataObject in base.GetAllDescendants())
			{
				metadataObject.BuildIndirectNameCrossLinkPathIfNeeded();
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00066BD4 File Offset: 0x00064DD4
		private void SerializePartitionsInNormalMode(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IgnoreChildren)
			{
				IEnumerable<Partition> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Partition> partitions = this.Partitions;
					enumerable = partitions;
				}
				else
				{
					enumerable = this.Partitions.Where((Partition o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Partition> enumerable2 = enumerable;
				if (enumerable2.Any<Partition>())
				{
					object[] array = enumerable2.Select((Partition obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					jsonObj["partitions", TomPropCategory.ChildCollection, 5, false] = array2;
				}
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00066C80 File Offset: 0x00064E80
		private void SerializeSinglePartitionInMergedMode(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (this.Partitions.Count != 1)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_TableMustHaveSinglePartitionForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
			}
			Partition partition = this.Partitions[0];
			if (partition.Name != this.Name)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_PartitionMustHaveSameNameAsParentTableForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
			}
			if (!string.IsNullOrEmpty(partition.Description))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_PartitionMustHaveEmptyDescriptionForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
			}
			if (partition.Annotations.Count != 0)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_PartitionMustHaveNoAnnotationsForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
			}
			partition.SerializeToJsonObject(jsonObj, options, mode, dbCompatibilityLevel);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00066D3E File Offset: 0x00064F3E
		private bool ReadPartitionPropertiesInNormalMode(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (jsonProp.Name == "partitions")
			{
				JsonPropertyHelper.ReadObjectCollection(this.Partitions, jsonProp.Value, options, mode, dbCompatibilityLevel);
				return true;
			}
			return false;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00066D6A File Offset: 0x00064F6A
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Table_1Arg(this.Name);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00066D78 File Offset: 0x00064F78
		private protected override void OnSerializeStart(SerializationActivityContext context)
		{
			base.OnSerializeStart(context);
			if (context.SerializationMode == MetadataSerializationMode.Json && context.PartitionsMergedWithTable)
			{
				if (this._Partitions.Count != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_TableMustHaveSinglePartitionForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
				}
				if (string.Compare(this._Partitions[0].Name, this.Name, StringComparison.Ordinal) != 0)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_PartitionMustHaveSameNameAsParentTableForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
				}
				if (!string.IsNullOrEmpty(this._Partitions[0].Description))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_PartitionMustHaveEmptyDescriptionForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
				}
				if (this._Partitions[0].Annotations.Count != 0 || this._Partitions[0].ExtendedProperties.Count != 0)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_PartitionMustHaveNoAnnotationsForMergedMode(ClientHostingManager.MarkAsRestrictedInformation(this.Name, InfoRestrictionType.CCON)), null);
				}
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00066E78 File Offset: 0x00065078
		private protected override void OnDeserializeStart(SerializationActivityContext context)
		{
			base.OnDeserializeStart(context);
			if (context.SerializationMode == MetadataSerializationMode.Json && context.PartitionsMergedWithTable)
			{
				Utils.Verify(this._Partitions.Count == 0);
				context.ActivityInfo["SerializationActivity::PartitionMergedIntoTable"] = new Partition();
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00066EC8 File Offset: 0x000650C8
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			if (context.SerializationMode == MetadataSerializationMode.Json && context.PartitionsMergedWithTable)
			{
				Utils.Verify(this._Partitions.Count == 0);
				Partition partition;
				Utils.Verify(context.TryExtractActivityInfo<Partition>("SerializationActivity::PartitionMergedIntoTable", out partition));
				partition.Name = this.Name;
				this._Partitions.Add(partition);
			}
			base.OnDeserializeEnd(context);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00066F2A File Offset: 0x0006512A
		private void WriteSinglePartitionPropertiesInMergedModeToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			this._Partitions[0].WritePartitionPropertiesInMergedModeToMetadataStream(context, writer);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00066F3F File Offset: 0x0006513F
		private bool TryReadSinglePartitionPropertyInMergedModeFromMetadataStream(SerializationActivityContext context, IMetadataReader reader, ref UnexpectedPropertyClassification classification)
		{
			return context.GetActivityInfo<Partition>("SerializationActivity::PartitionMergedIntoTable").TryReadPartitionPropertyInMergedModeFromMetadataStream(context, reader, ref classification);
		}

		// Token: 0x04000177 RID: 375
		internal Table.ObjectBody body;

		// Token: 0x04000178 RID: 376
		private ColumnCollection _Columns;

		// Token: 0x04000179 RID: 377
		private PartitionCollection _Partitions;

		// Token: 0x0400017A RID: 378
		private MeasureCollection _Measures;

		// Token: 0x0400017B RID: 379
		private HierarchyCollection _Hierarchies;

		// Token: 0x0400017C RID: 380
		private SetCollection _Sets;

		// Token: 0x0400017D RID: 381
		private TableAnnotationCollection _Annotations;

		// Token: 0x0400017E RID: 382
		private TableExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0400017F RID: 383
		private TableExcludedArtifactCollection _ExcludedArtifacts;

		// Token: 0x04000180 RID: 384
		private TableChangedPropertyCollection _ChangedProperties;

		// Token: 0x04000181 RID: 385
		private CalendarCollection _Calendars;

		// Token: 0x020002D0 RID: 720
		internal class ObjectBody : IncrementalRefreshMetadataObjectBody<Table>
		{
			// Token: 0x0600230F RID: 8975 RVA: 0x000E0150 File Offset: 0x000DE350
			public ObjectBody(Table owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<Table, Model>(owner, "Model");
				this.DefaultDetailRowsDefinitionID = new ChildLink<Table, DetailRowsDefinition>(owner, "DefaultDetailRowsDefinition");
				this.RefreshPolicyID = new ChildLink<Table, RefreshPolicy>(owner, "RefreshPolicy");
				this.CalculationGroupID = new ChildLink<Table, CalculationGroup>(owner, "CalculationGroup");
			}

			// Token: 0x06002310 RID: 8976 RVA: 0x000E01BE File Offset: 0x000DE3BE
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06002311 RID: 8977 RVA: 0x000E01C8 File Offset: 0x000DE3C8
			internal bool IsEqualTo(Table.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && PropertyHelper.AreValuesIdentical(this.ShowAsVariationsOnly, other.ShowAsVariationsOnly) && PropertyHelper.AreValuesIdentical(this.IsPrivate, other.IsPrivate) && PropertyHelper.AreValuesIdentical(this.AlternateSourcePrecedence, other.AlternateSourcePrecedence) && PropertyHelper.AreValuesIdentical(this.ExcludeFromModelRefresh, other.ExcludeFromModelRefresh) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.SystemManaged, other.SystemManaged) && PropertyHelper.AreValuesIdentical(this.ExcludeFromAutomaticAggregations, other.ExcludeFromAutomaticAggregations) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context)) && this.DefaultDetailRowsDefinitionID.IsEqualTo(other.DefaultDetailRowsDefinitionID, context) && this.RefreshPolicyID.IsEqualTo(other.RefreshPolicyID, context) && this.CalculationGroupID.IsEqualTo(other.CalculationGroupID, context);
			}

			// Token: 0x06002312 RID: 8978 RVA: 0x000E0390 File Offset: 0x000DE590
			internal void CopyFromImpl(Table.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.DataCategory = other.DataCategory;
				this.Description = other.Description;
				this.IsHidden = other.IsHidden;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.StructureModifiedTime = other.StructureModifiedTime;
				}
				this.ShowAsVariationsOnly = other.ShowAsVariationsOnly;
				this.IsPrivate = other.IsPrivate;
				this.AlternateSourcePrecedence = other.AlternateSourcePrecedence;
				this.ExcludeFromModelRefresh = other.ExcludeFromModelRefresh;
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.SystemManaged = other.SystemManaged;
				this.ExcludeFromAutomaticAggregations = other.ExcludeFromAutomaticAggregations;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
				this.DefaultDetailRowsDefinitionID.CopyFrom(other.DefaultDetailRowsDefinitionID, context);
				this.RefreshPolicyID.CopyFrom(other.RefreshPolicyID, context);
				this.CalculationGroupID.CopyFrom(other.CalculationGroupID, context);
			}

			// Token: 0x06002313 RID: 8979 RVA: 0x000E04E4 File Offset: 0x000DE6E4
			internal void CopyFromImpl(Table.ObjectBody other)
			{
				this.Name = other.Name;
				this.DataCategory = other.DataCategory;
				this.Description = other.Description;
				this.IsHidden = other.IsHidden;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.ShowAsVariationsOnly = other.ShowAsVariationsOnly;
				this.IsPrivate = other.IsPrivate;
				this.AlternateSourcePrecedence = other.AlternateSourcePrecedence;
				this.ExcludeFromModelRefresh = other.ExcludeFromModelRefresh;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.SystemManaged = other.SystemManaged;
				this.ExcludeFromAutomaticAggregations = other.ExcludeFromAutomaticAggregations;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
				this.DefaultDetailRowsDefinitionID.CopyFrom(other.DefaultDetailRowsDefinitionID, ObjectChangeTracker.BodyCloneContext);
				this.RefreshPolicyID.CopyFrom(other.RefreshPolicyID, ObjectChangeTracker.BodyCloneContext);
				this.CalculationGroupID.CopyFrom(other.CalculationGroupID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002314 RID: 8980 RVA: 0x000E05F1 File Offset: 0x000DE7F1
			public override void CopyFrom(MetadataObjectBody<Table> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Table.ObjectBody)other, context);
			}

			// Token: 0x06002315 RID: 8981 RVA: 0x000E0608 File Offset: 0x000DE808
			internal bool IsEqualTo(Table.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.DataCategory, other.DataCategory) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.ShowAsVariationsOnly, other.ShowAsVariationsOnly) && PropertyHelper.AreValuesIdentical(this.IsPrivate, other.IsPrivate) && PropertyHelper.AreValuesIdentical(this.AlternateSourcePrecedence, other.AlternateSourcePrecedence) && PropertyHelper.AreValuesIdentical(this.ExcludeFromModelRefresh, other.ExcludeFromModelRefresh) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.SystemManaged, other.SystemManaged) && PropertyHelper.AreValuesIdentical(this.ExcludeFromAutomaticAggregations, other.ExcludeFromAutomaticAggregations) && this.ModelID.IsEqualTo(other.ModelID) && this.DefaultDetailRowsDefinitionID.IsEqualTo(other.DefaultDetailRowsDefinitionID) && this.RefreshPolicyID.IsEqualTo(other.RefreshPolicyID) && this.CalculationGroupID.IsEqualTo(other.CalculationGroupID);
			}

			// Token: 0x06002316 RID: 8982 RVA: 0x000E0790 File Offset: 0x000DE990
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Table.ObjectBody)other);
			}

			// Token: 0x06002317 RID: 8983 RVA: 0x000E07AC File Offset: 0x000DE9AC
			internal void CompareWith(Table.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
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
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "StructureModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.StructureModifiedTime, this.StructureModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ShowAsVariationsOnly, other.ShowAsVariationsOnly))
				{
					context.RegisterPropertyChange(base.Owner, "ShowAsVariationsOnly", typeof(bool), PropertyFlags.DdlAndUser, other.ShowAsVariationsOnly, this.ShowAsVariationsOnly);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsPrivate, other.IsPrivate))
				{
					context.RegisterPropertyChange(base.Owner, "IsPrivate", typeof(bool), PropertyFlags.DdlAndUser, other.IsPrivate, this.IsPrivate);
				}
				if (!PropertyHelper.AreValuesIdentical(this.AlternateSourcePrecedence, other.AlternateSourcePrecedence))
				{
					context.RegisterPropertyChange(base.Owner, "AlternateSourcePrecedence", typeof(int), PropertyFlags.DdlAndUser, other.AlternateSourcePrecedence, this.AlternateSourcePrecedence);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ExcludeFromModelRefresh, other.ExcludeFromModelRefresh))
				{
					context.RegisterPropertyChange(base.Owner, "ExcludeFromModelRefresh", typeof(bool), PropertyFlags.DdlAndUser, other.ExcludeFromModelRefresh, this.ExcludeFromModelRefresh);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SystemManaged, other.SystemManaged))
				{
					context.RegisterPropertyChange(base.Owner, "SystemManaged", typeof(bool), PropertyFlags.DdlAndUser, other.SystemManaged, this.SystemManaged);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ExcludeFromAutomaticAggregations, other.ExcludeFromAutomaticAggregations))
				{
					context.RegisterPropertyChange(base.Owner, "ExcludeFromAutomaticAggregations", typeof(bool), PropertyFlags.DdlAndUser, other.ExcludeFromAutomaticAggregations, this.ExcludeFromAutomaticAggregations);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
				this.DefaultDetailRowsDefinitionID.CompareWith(other.DefaultDetailRowsDefinitionID, "DefaultDetailRowsDefinitionID", "DefaultDetailRowsDefinition", PropertyFlags.None, context);
				this.RefreshPolicyID.CompareWith(other.RefreshPolicyID, "RefreshPolicyID", "RefreshPolicy", PropertyFlags.None, context);
				this.CalculationGroupID.CompareWith(other.CalculationGroupID, "CalculationGroupID", "CalculationGroup", PropertyFlags.None, context);
			}

			// Token: 0x06002318 RID: 8984 RVA: 0x000E0BCC File Offset: 0x000DEDCC
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Table.ObjectBody)other, context);
			}

			// Token: 0x04000A47 RID: 2631
			internal string Name;

			// Token: 0x04000A48 RID: 2632
			internal string DataCategory;

			// Token: 0x04000A49 RID: 2633
			internal string Description;

			// Token: 0x04000A4A RID: 2634
			internal bool IsHidden;

			// Token: 0x04000A4B RID: 2635
			internal DateTime ModifiedTime;

			// Token: 0x04000A4C RID: 2636
			internal DateTime StructureModifiedTime;

			// Token: 0x04000A4D RID: 2637
			internal bool ShowAsVariationsOnly;

			// Token: 0x04000A4E RID: 2638
			internal bool IsPrivate;

			// Token: 0x04000A4F RID: 2639
			internal int AlternateSourcePrecedence;

			// Token: 0x04000A50 RID: 2640
			internal bool ExcludeFromModelRefresh;

			// Token: 0x04000A51 RID: 2641
			internal string LineageTag;

			// Token: 0x04000A52 RID: 2642
			internal string SourceLineageTag;

			// Token: 0x04000A53 RID: 2643
			internal bool SystemManaged;

			// Token: 0x04000A54 RID: 2644
			internal bool ExcludeFromAutomaticAggregations;

			// Token: 0x04000A55 RID: 2645
			internal ParentLink<Table, Model> ModelID;

			// Token: 0x04000A56 RID: 2646
			internal ChildLink<Table, DetailRowsDefinition> DefaultDetailRowsDefinitionID;

			// Token: 0x04000A57 RID: 2647
			internal ChildLink<Table, RefreshPolicy> RefreshPolicyID;

			// Token: 0x04000A58 RID: 2648
			internal ChildLink<Table, CalculationGroup> CalculationGroupID;
		}

		// Token: 0x020002D1 RID: 721
		private static class SerializationActivityInfoKey
		{
			// Token: 0x04000A59 RID: 2649
			public const string MergedPartition = "SerializationActivity::PartitionMergedIntoTable";
		}
	}
}
