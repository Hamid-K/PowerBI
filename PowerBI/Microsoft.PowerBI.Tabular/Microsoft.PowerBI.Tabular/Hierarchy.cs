using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000063 RID: 99
	public sealed class Hierarchy : NamedMetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x000277DD File Offset: 0x000259DD
		public Hierarchy()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000277F0 File Offset: 0x000259F0
		internal Hierarchy(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00027800 File Offset: 0x00025A00
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Hierarchy.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.State = ObjectState.CalculationNeeded;
			this.body.DisplayFolder = string.Empty;
			this.body.HideMembers = HierarchyHideMembersType.Default;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this._Levels = new LevelCollection(this, comparer);
			this._Annotations = new HierarchyAnnotationCollection(this, comparer);
			this._ExtendedProperties = new HierarchyExtendedPropertyCollection(this, comparer);
			this._ExcludedArtifacts = new HierarchyExcludedArtifactCollection(this);
			this._ChangedProperties = new HierarchyChangedPropertyCollection(this);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000278C0 File Offset: 0x00025AC0
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Hierarchy;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000278C4 File Offset: 0x00025AC4
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000278D6 File Offset: 0x00025AD6
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
					MetadataObject.UpdateMetadataObjectParent<Hierarchy, Table>(this.body.TableID, (Table)value, null, null);
				}
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00027903 File Offset: 0x00025B03
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00027918 File Offset: 0x00025B18
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Hierarchy, null, "Hierarchy object of Tabular Object Model (TOM)", new bool?(false)))
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
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
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
				if (writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("hideMembers", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<HierarchyHideMembersType>("hideMembers", MetadataPropertyNature.RegularProperty, null);
				}
				if (CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("levels", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
				{
					writer.WriteChildCollection(context, "levels", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, ObjectType.Level);
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
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00027C50 File Offset: 0x00025E50
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.HideMembers != HierarchyHideMembersType.Default)
			{
				int num;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Hierarchy_HideMembers[mode], PropertyHelper.GetHierarchyHideMembersTypeCompatibilityRestrictions(this.body.HideMembers)[mode], out num);
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "HideMembers");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				int num2 = CompatibilityRestrictions.Hierarchy_LineageTag[mode];
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
				int num3 = CompatibilityRestrictions.Hierarchy_SourceLineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag");
					requiredLevel = num3;
					int num4 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00027D63 File Offset: 0x00025F63
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00027D6B File Offset: 0x00025F6B
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Hierarchy.ObjectBody)value;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00027D79 File Offset: 0x00025F79
		internal override ITxObjectBody CreateBody()
		{
			return new Hierarchy.ObjectBody(this);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00027D81 File Offset: 0x00025F81
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Hierarchy();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00027D88 File Offset: 0x00025F88
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Table)parent).Hierarchies;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00027D98 File Offset: 0x00025F98
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<Hierarchy, Table>(this.body.TableID, objectMap, throwIfCantResolve, null, null);
			if (table != null)
			{
				table.Hierarchies.Add(this);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00027DC9 File Offset: 0x00025FC9
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00027DCB File Offset: 0x00025FCB
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Levels;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ExcludedArtifacts;
			yield return this._ChangedProperties;
			yield break;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00027DDB File Offset: 0x00025FDB
		public LevelCollection Levels
		{
			get
			{
				return this._Levels;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00027DE3 File Offset: 0x00025FE3
		public HierarchyAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00027DEB File Offset: 0x00025FEB
		[CompatibilityRequirement("1400")]
		public HierarchyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00027DF3 File Offset: 0x00025FF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public HierarchyExcludedArtifactCollection ExcludedArtifacts
		{
			get
			{
				return this._ExcludedArtifacts;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00027DFB File Offset: 0x00025FFB
		[CompatibilityRequirement("1567")]
		public HierarchyChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00027E03 File Offset: 0x00026003
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00027E10 File Offset: 0x00026010
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Hierarchy, out text))
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00027E93 File Offset: 0x00026093
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00027EA0 File Offset: 0x000260A0
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00027F10 File Offset: 0x00026110
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00027F20 File Offset: 0x00026120
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00027FA4 File Offset: 0x000261A4
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00027FB4 File Offset: 0x000261B4
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
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.CalculationNeeded))
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000280D6 File Offset: 0x000262D6
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000280E4 File Offset: 0x000262E4
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00028168 File Offset: 0x00026368
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00028178 File Offset: 0x00026378
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000281FC File Offset: 0x000263FC
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0002820C File Offset: 0x0002640C
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

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00028290 File Offset: 0x00026490
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x000282A0 File Offset: 0x000264A0
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

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00028310 File Offset: 0x00026510
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00028320 File Offset: 0x00026520
		[CompatibilityRequirement("1400")]
		public HierarchyHideMembersType HideMembers
		{
			get
			{
				return this.body.HideMembers;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.HideMembers, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Hierarchy_HideMembers.Merge(PropertyHelper.GetHierarchyHideMembersTypeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Hierarchy_HideMembers.Merge(PropertyHelper.GetHierarchyHideMembersTypeCompatibilityRestrictions(this.body.HideMembers));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != HierarchyHideMembersType.Default))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "HideMembers", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "HideMembers", typeof(HierarchyHideMembersType), this.body.HideMembers, value);
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
					HierarchyHideMembersType hideMembers = this.body.HideMembers;
					this.body.HideMembers = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "HideMembers", typeof(HierarchyHideMembersType), hideMembers, value);
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00028455 File Offset: 0x00026655
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00028464 File Offset: 0x00026664
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Hierarchy_LineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Hierarchy_LineageTag, array);
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00028519 File Offset: 0x00026719
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00028528 File Offset: 0x00026728
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Hierarchy_SourceLineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Hierarchy_SourceLineageTag, array);
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000285DD File Offset: 0x000267DD
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x000285F0 File Offset: 0x000267F0
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00028674 File Offset: 0x00026874
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00028686 File Offset: 0x00026886
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

		// Token: 0x06000571 RID: 1393 RVA: 0x0002869C File Offset: 0x0002689C
		internal void CopyFrom(Hierarchy other, CopyContext context)
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
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Levels.CopyFrom(other.Levels, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ExcludedArtifacts.CopyFrom(other.ExcludedArtifacts, context);
				this.ChangedProperties.CopyFrom(other.ChangedProperties, context);
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000287D4 File Offset: 0x000269D4
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Hierarchy)other, context);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000287E3 File Offset: 0x000269E3
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Hierarchy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000287FF File Offset: 0x000269FF
		public void CopyTo(Hierarchy other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002881B File Offset: 0x00026A1B
		public Hierarchy Clone()
		{
			return base.CloneInternal<Hierarchy>();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00028824 File Offset: 0x00026A24
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (this.body.IsHidden)
			{
				writer.WriteProperty<bool>(options, "IsHidden", this.body.IsHidden);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				writer.WriteProperty<string>(options, "DisplayFolder", this.body.DisplayFolder);
			}
			if (this.body.HideMembers != HierarchyHideMembersType.Default)
			{
				if (!CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsHierarchyHideMembersTypeValueCompatible(this.body.HideMembers, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member HideMembers is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<HierarchyHideMembersType>(options, "HideMembers", this.body.HideMembers);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00028A30 File Offset: 0x00026C30
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
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
			string text3;
			if (reader.TryReadProperty<string>("DisplayFolder", out text3))
			{
				this.body.DisplayFolder = text3;
			}
			HierarchyHideMembersType hierarchyHideMembersType;
			if (CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<HierarchyHideMembersType>("HideMembers", out hierarchyHideMembersType))
			{
				this.body.HideMembers = hierarchyHideMembersType;
			}
			string text4;
			if (CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("LineageTag", out text4))
			{
				this.body.LineageTag = text4;
			}
			string text5;
			if (CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceLineageTag", out text5))
			{
				this.body.SourceLineageTag = text5;
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00028BC4 File Offset: 0x00026DC4
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.IsHidden && writer.ShouldIncludeProperty("IsHidden", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsHidden", MetadataPropertyNature.RegularProperty, this.body.IsHidden);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("DisplayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
			if (this.body.HideMembers != HierarchyHideMembersType.Default)
			{
				if (!CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsHierarchyHideMembersTypeValueCompatible(this.body.HideMembers, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member HideMembers is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("HideMembers", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<HierarchyHideMembersType>("HideMembers", MetadataPropertyNature.RegularProperty, this.body.HideMembers);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
				if (!CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00028E9C File Offset: 0x0002709C
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
			if (this.body.State != ObjectState.CalculationNeeded)
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
			if (!string.IsNullOrEmpty(this.body.DisplayFolder) && writer.ShouldIncludeProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("displayFolder", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.DisplayFolder);
			}
			if (this.body.HideMembers != HierarchyHideMembersType.Default)
			{
				if (!CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsHierarchyHideMembersTypeValueCompatible(this.body.HideMembers, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member HideMembers is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("hideMembers", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<HierarchyHideMembersType>("hideMembers", MetadataPropertyNature.RegularProperty, this.body.HideMembers);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
				if (!CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (this.Levels.Count > 0 && writer.ShouldIncludeProperty("levels", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable))
			{
				writer.WriteChildCollection(context, "levels", MetadataPropertyNature.ChildCollection | MetadataPropertyNature.Translatable, this.Levels);
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
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00029444 File Offset: 0x00027644
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
				{
					char c = propertyName[0];
					if (c != 'S')
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
					else if (!(propertyName == "State"))
					{
						break;
					}
					ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
					if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.State = objectState;
					return true;
				}
				case 6:
					if (propertyName == "levels")
					{
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Level level in reader.ReadChildCollectionProperty<Level>(context))
							{
								try
								{
									this.Levels.Add(level);
								}
								catch (Exception ex)
								{
									throw reader.CreateInvalidChildException(context, level, TomSR.Exception_FailedAddDeserializedNamedObject("Level", (level != null) ? level.Name : null, ex.Message), ex);
								}
							}
						}
						return true;
					}
					break;
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
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "isHidden"))
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
				case 10:
				{
					char c = propertyName[0];
					if (c != 'L')
					{
						if (c != 'l')
						{
							break;
						}
						if (!(propertyName == "lineageTag"))
						{
							break;
						}
					}
					else if (!(propertyName == "LineageTag"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
					if (c <= 'H')
					{
						if (c != 'D')
						{
							if (c != 'H')
							{
								break;
							}
							if (!(propertyName == "HideMembers"))
							{
								break;
							}
							goto IL_04EB;
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
							if (c != 'h')
							{
								break;
							}
							if (!(propertyName == "hideMembers"))
							{
								break;
							}
							goto IL_04EB;
						}
						else if (!(propertyName == "description"))
						{
							break;
						}
					}
					else
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
					this.body.Description = reader.ReadStringProperty();
					return true;
					IL_04EB:
					if (!CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.HierarchyHideMembersType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.HideMembers = reader.ReadEnumProperty<HierarchyHideMembersType>();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "modifiedTime"))
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
				{
					char c = propertyName[0];
					if (c <= 'R')
					{
						if (c != 'D')
						{
							if (c != 'R')
							{
								break;
							}
							if (!(propertyName == "RefreshedTime"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "DisplayFolder"))
							{
								break;
							}
							goto IL_04D8;
						}
					}
					else if (c != 'd')
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
						goto IL_04D8;
					}
					this.body.RefreshedTime = reader.ReadDateTimeProperty();
					return true;
					IL_04D8:
					this.body.DisplayFolder = reader.ReadStringProperty();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c != 'S')
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
					else if (!(propertyName == "SourceLineageTag"))
					{
						break;
					}
					if (!CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
										catch (Exception ex3)
										{
											throw reader.CreateInvalidChildException(context, excludedArtifact, TomSR.Exception_FailedAddDeserializedObject("ExcludedArtifact", ex3.Message), ex3);
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
								catch (Exception ex4)
								{
									throw reader.CreateInvalidChildException(context, changedProperty, TomSR.Exception_FailedAddDeserializedObject("ChangedProperty", ex4.Message), ex4);
								}
							}
						}
						return true;
					}
					break;
				}
				case 18:
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
								catch (Exception ex5)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex5.Message), ex5);
								}
							}
						}
						return true;
					}
					break;
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
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00029D80 File Offset: 0x00027F80
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00029D89 File Offset: 0x00027F89
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00029DAC File Offset: 0x00027FAC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsHidden)
			{
				result["isHidden", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsHidden);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.CalculationNeeded)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.StructureModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["structureModifiedTime", TomPropCategory.Regular, 8, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.StructureModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["refreshedTime", TomPropCategory.Regular, 9, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.RefreshedTime);
			}
			if (!string.IsNullOrEmpty(this.body.DisplayFolder))
			{
				result["displayFolder", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.DisplayFolder, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.HideMembers != HierarchyHideMembersType.Default)
			{
				if (!CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsHierarchyHideMembersTypeValueCompatible(this.body.HideMembers, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member HideMembers is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["hideMembers", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertEnumToJsonValue<HierarchyHideMembersType>(this.body.HideMembers);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["lineageTag", TomPropCategory.Regular, 12, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceLineageTag", TomPropCategory.Regular, 13, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				IEnumerable<Level> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<Level> levels = this.Levels;
					enumerable = levels;
				}
				else
				{
					enumerable = this.Levels.Where((Level o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<Level> enumerable2 = enumerable;
				if (enumerable2.Any<Level>())
				{
					object[] array = enumerable2.Select((Level obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["levels", TomPropCategory.ChildCollection, 9, false] = array2;
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
					IEnumerable<ExcludedArtifact> enumerable5;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExcludedArtifact> excludedArtifacts = this.ExcludedArtifacts;
						enumerable5 = excludedArtifacts;
					}
					else
					{
						enumerable5 = this.ExcludedArtifacts.Where((ExcludedArtifact o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExcludedArtifact> enumerable6 = enumerable5;
					if (enumerable6.Any<ExcludedArtifact>())
					{
						if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable6.Select((ExcludedArtifact obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array4 = array;
						result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array4;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ChangedProperty> enumerable7;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ChangedProperty> changedProperties = this.ChangedProperties;
						enumerable7 = changedProperties;
					}
					else
					{
						enumerable7 = this.ChangedProperties.Where((ChangedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ChangedProperty> enumerable8 = enumerable7;
					if (enumerable8.Any<ChangedProperty>())
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable8.Select((ChangedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array5 = array;
						result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array5;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array6 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array6;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002A570 File Offset: 0x00028770
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
					break;
				case 6:
					if (name == "levels")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Levels, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 8:
					if (name == "isHidden")
					{
						this.body.IsHidden = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 10:
					if (name == "lineageTag")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
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
								if (name == "hideMembers")
								{
									HierarchyHideMembersType hierarchyHideMembersType = JsonPropertyHelper.ConvertJsonValueToEnum<HierarchyHideMembersType>(jsonProp.Value);
									if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsHierarchyHideMembersTypeValueCompatible(hierarchyHideMembersType, mode, dbCompatibilityLevel)))
									{
										return false;
									}
									this.body.HideMembers = hierarchyHideMembersType;
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
					if (name == "modifiedTime")
					{
						this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
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
				case 16:
					if (name == "sourceLineageTag")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
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

		// Token: 0x0600057F RID: 1407 RVA: 0x0002A9FA File Offset: 0x00028BFA
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
					PerspectiveHierarchy perspectiveHierarchy = perspectiveTable.PerspectiveHierarchies.Find(objectName);
					if (perspectiveHierarchy != null)
					{
						yield return perspectiveHierarchy;
					}
				}
			}
			IEnumerator<Perspective> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002AA11 File Offset: 0x00028C11
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_Hierarchy_2Args(this.Name, this.Table.Name);
			}
			return TomSR.ObjectPath_Hierarchy_1Arg(this.Name);
		}

		// Token: 0x04000101 RID: 257
		internal Hierarchy.ObjectBody body;

		// Token: 0x04000102 RID: 258
		private LevelCollection _Levels;

		// Token: 0x04000103 RID: 259
		private HierarchyAnnotationCollection _Annotations;

		// Token: 0x04000104 RID: 260
		private HierarchyExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x04000105 RID: 261
		private HierarchyExcludedArtifactCollection _ExcludedArtifacts;

		// Token: 0x04000106 RID: 262
		private HierarchyChangedPropertyCollection _ChangedProperties;

		// Token: 0x0200026C RID: 620
		internal class ObjectBody : NamedMetadataObjectBody<Hierarchy>
		{
			// Token: 0x06002060 RID: 8288 RVA: 0x000D4864 File Offset: 0x000D2A64
			public ObjectBody(Hierarchy owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.StructureModifiedTime = DateTime.MinValue;
				this.RefreshedTime = DateTime.MinValue;
				this.TableID = new ParentLink<Hierarchy, Table>(owner, "Table");
			}

			// Token: 0x06002061 RID: 8289 RVA: 0x000D489F File Offset: 0x000D2A9F
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06002062 RID: 8290 RVA: 0x000D48A8 File Offset: 0x000D2AA8
			internal bool IsEqualTo(Hierarchy.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime)) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && PropertyHelper.AreValuesIdentical(this.HideMembers, other.HideMembers) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context));
			}

			// Token: 0x06002063 RID: 8291 RVA: 0x000D4A14 File Offset: 0x000D2C14
			internal void CopyFromImpl(Hierarchy.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.IsHidden = other.IsHidden;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
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
				this.DisplayFolder = other.DisplayFolder;
				this.HideMembers = other.HideMembers;
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
			}

			// Token: 0x06002064 RID: 8292 RVA: 0x000D4B34 File Offset: 0x000D2D34
			internal void CopyFromImpl(Hierarchy.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.IsHidden = other.IsHidden;
				this.State = other.State;
				this.ModifiedTime = other.ModifiedTime;
				this.StructureModifiedTime = other.StructureModifiedTime;
				this.RefreshedTime = other.RefreshedTime;
				this.DisplayFolder = other.DisplayFolder;
				this.HideMembers = other.HideMembers;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002065 RID: 8293 RVA: 0x000D4BDB File Offset: 0x000D2DDB
			public override void CopyFrom(MetadataObjectBody<Hierarchy> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Hierarchy.ObjectBody)other, context);
			}

			// Token: 0x06002066 RID: 8294 RVA: 0x000D4BF4 File Offset: 0x000D2DF4
			internal bool IsEqualTo(Hierarchy.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsHidden, other.IsHidden) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.StructureModifiedTime, other.StructureModifiedTime) && PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime) && PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder) && PropertyHelper.AreValuesIdentical(this.HideMembers, other.HideMembers) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x06002067 RID: 8295 RVA: 0x000D4CFE File Offset: 0x000D2EFE
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Hierarchy.ObjectBody)other);
			}

			// Token: 0x06002068 RID: 8296 RVA: 0x000D4D18 File Offset: 0x000D2F18
			internal void CompareWith(Hierarchy.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
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
				if (!PropertyHelper.AreValuesIdentical(this.DisplayFolder, other.DisplayFolder))
				{
					context.RegisterPropertyChange(base.Owner, "DisplayFolder", typeof(string), PropertyFlags.DdlAndUser, other.DisplayFolder, this.DisplayFolder);
				}
				if (!PropertyHelper.AreValuesIdentical(this.HideMembers, other.HideMembers))
				{
					context.RegisterPropertyChange(base.Owner, "HideMembers", typeof(HierarchyHideMembersType), PropertyFlags.DdlAndUser, other.HideMembers, this.HideMembers);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002069 RID: 8297 RVA: 0x000D5012 File Offset: 0x000D3212
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Hierarchy.ObjectBody)other, context);
			}

			// Token: 0x0400084D RID: 2125
			internal string Name;

			// Token: 0x0400084E RID: 2126
			internal string Description;

			// Token: 0x0400084F RID: 2127
			internal bool IsHidden;

			// Token: 0x04000850 RID: 2128
			internal ObjectState State;

			// Token: 0x04000851 RID: 2129
			internal DateTime ModifiedTime;

			// Token: 0x04000852 RID: 2130
			internal DateTime StructureModifiedTime;

			// Token: 0x04000853 RID: 2131
			internal DateTime RefreshedTime;

			// Token: 0x04000854 RID: 2132
			internal string DisplayFolder;

			// Token: 0x04000855 RID: 2133
			internal HierarchyHideMembersType HideMembers;

			// Token: 0x04000856 RID: 2134
			internal string LineageTag;

			// Token: 0x04000857 RID: 2135
			internal string SourceLineageTag;

			// Token: 0x04000858 RID: 2136
			internal ParentLink<Hierarchy, Table> TableID;
		}
	}
}
