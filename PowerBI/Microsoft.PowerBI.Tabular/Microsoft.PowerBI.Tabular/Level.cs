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
	// Token: 0x0200006D RID: 109
	public sealed class Level : NamedMetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0002D1D1 File Offset: 0x0002B3D1
		public Level()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		internal Level(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Level.ObjectBody(this);
			this.body.Ordinal = -1;
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this._Annotations = new LevelAnnotationCollection(this, comparer);
			this._ExtendedProperties = new LevelExtendedPropertyCollection(this, comparer);
			this._ChangedProperties = new LevelChangedPropertyCollection(this);
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0002D27F File Offset: 0x0002B47F
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Level;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0002D283 File Offset: 0x0002B483
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0002D295 File Offset: 0x0002B495
		public override MetadataObject Parent
		{
			get
			{
				return this.body.HierarchyID.Object;
			}
			internal set
			{
				if (this.body.HierarchyID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Level, Hierarchy>(this.body.HierarchyID, (Hierarchy)value, null, null);
				}
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0002D2C2 File Offset: 0x0002B4C2
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.HierarchyID.ObjectID;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002D2D4 File Offset: 0x0002B4D4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Level, null, "Level object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("ordinal", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("ordinal", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.Level_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("column", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<Level, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Column, context.SerializationMode != MetadataSerializationMode.Json, "column", false, writer);
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
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				int num = CompatibilityRestrictions.Level_LineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				int num2 = CompatibilityRestrictions.Level_SourceLineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag");
					requiredLevel = num2;
					int num3 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0002D5A1 File Offset: 0x0002B7A1
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0002D5A9 File Offset: 0x0002B7A9
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Level.ObjectBody)value;
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002D5B7 File Offset: 0x0002B7B7
		internal override ITxObjectBody CreateBody()
		{
			return new Level.ObjectBody(this);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002D5BF File Offset: 0x0002B7BF
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Level();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002D5C6 File Offset: 0x0002B7C6
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Hierarchy)parent).Levels;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0002D5D4 File Offset: 0x0002B7D4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Hierarchy hierarchy = MetadataObject.ResolveMetadataObjectParentById<Level, Hierarchy>(this.body.HierarchyID, objectMap, throwIfCantResolve, null, null);
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (hierarchy != null)
			{
				hierarchy.Levels.Add(this);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0002D618 File Offset: 0x0002B818
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0002D630 File Offset: 0x0002B830
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.ColumnID.IsResolved && !this.body.ColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Column"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0002D684 File Offset: 0x0002B884
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.ColumnID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0002D698 File Offset: 0x0002B898
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.ColumnID.Validate(result, throwOnError);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002D6AC File Offset: 0x0002B8AC
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.ColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002D6C8 File Offset: 0x0002B8C8
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ChangedProperties;
			yield break;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0002D6D8 File Offset: 0x0002B8D8
		public LevelAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		[CompatibilityRequirement("1400")]
		public LevelExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0002D6E8 File Offset: 0x0002B8E8
		[CompatibilityRequirement("1567")]
		public LevelChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0002D6F0 File Offset: 0x0002B8F0
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0002D700 File Offset: 0x0002B900
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Level, out text))
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0002D783 File Offset: 0x0002B983
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0002D790 File Offset: 0x0002B990
		public int Ordinal
		{
			get
			{
				return this.body.Ordinal;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Ordinal, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Ordinal", typeof(int), this.body.Ordinal, value);
					int ordinal = this.body.Ordinal;
					this.body.Ordinal = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Ordinal", typeof(int), ordinal, value);
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0002D814 File Offset: 0x0002BA14
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0002D824 File Offset: 0x0002BA24
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0002D894 File Offset: 0x0002BA94
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0002D8A4 File Offset: 0x0002BAA4
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0002D928 File Offset: 0x0002BB28
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0002D938 File Offset: 0x0002BB38
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Level_LineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Level_LineageTag, array);
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0002D9ED File Offset: 0x0002BBED
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0002D9FC File Offset: 0x0002BBFC
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Level_SourceLineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Level_SourceLineageTag, array);
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

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0002DAB1 File Offset: 0x0002BCB1
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		public Hierarchy Hierarchy
		{
			get
			{
				return this.body.HierarchyID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.HierarchyID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Hierarchy", typeof(Hierarchy), this.body.HierarchyID.Object, value);
					Hierarchy @object = this.body.HierarchyID.Object;
					this.body.HierarchyID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Hierarchy", typeof(Hierarchy), @object, value);
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0002DB48 File Offset: 0x0002BD48
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0002DB5A File Offset: 0x0002BD5A
		internal ObjectId _HierarchyID
		{
			get
			{
				return this.body.HierarchyID.ObjectID;
			}
			set
			{
				this.body.HierarchyID.ObjectID = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0002DB6D File Offset: 0x0002BD6D
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0002DB80 File Offset: 0x0002BD80
		public Column Column
		{
			get
			{
				return this.body.ColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Column", typeof(Column), this.body.ColumnID.Object, value);
					Column @object = this.body.ColumnID.Object;
					this.body.ColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Column", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0002DC04 File Offset: 0x0002BE04
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0002DC16 File Offset: 0x0002BE16
		internal ObjectId _ColumnID
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
			set
			{
				this.body.ColumnID.ObjectID = value;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002DC2C File Offset: 0x0002BE2C
		internal void CopyFrom(Level other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0;
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
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ChangedProperties.CopyFrom(other.ChangedProperties, context);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002DD00 File Offset: 0x0002BF00
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Level)other, context);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002DD0F File Offset: 0x0002BF0F
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Level other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002DD2B File Offset: 0x0002BF2B
		public void CopyTo(Level other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002DD47 File Offset: 0x0002BF47
		public Level Clone()
		{
			return base.CloneInternal<Level>();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002DD50 File Offset: 0x0002BF50
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.HierarchyID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "HierarchyID", this.body.HierarchyID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
			if (this.body.Ordinal != -1)
			{
				writer.WriteProperty<int>(options, "Ordinal", this.body.Ordinal);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002DF08 File Offset: 0x0002C108
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("HierarchyID", out objectId))
			{
				this.body.HierarchyID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId2))
			{
				this.body.ColumnID.ObjectID = objectId2;
			}
			int num;
			if (reader.TryReadProperty<int>("Ordinal", out num))
			{
				this.body.Ordinal = num;
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
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			string text3;
			if (CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("LineageTag", out text3))
			{
				this.body.LineageTag = text3;
			}
			string text4;
			if (CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceLineageTag", out text4))
			{
				this.body.SourceLineageTag = text4;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002E020 File Offset: 0x0002C220
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.HierarchyID.Object != null && writer.ShouldIncludeProperty("HierarchyID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("HierarchyID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.HierarchyID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.ColumnID.Object);
			}
			if (this.body.Ordinal != -1 && writer.ShouldIncludeProperty("Ordinal", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("Ordinal", MetadataPropertyNature.RegularProperty, this.body.Ordinal);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Level_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
				if (!CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002E270 File Offset: 0x0002C470
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (this.body.Ordinal != -1 && writer.ShouldIncludeProperty("ordinal", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("ordinal", MetadataPropertyNature.RegularProperty, this.body.Ordinal);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Level_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
				if (!CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("column", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.ColumnID.WriteToMetadataStream(ObjectType.Column, context.SerializationMode != MetadataSerializationMode.Json, "column", false, writer);
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

		// Token: 0x0600060F RID: 1551 RVA: 0x0002E5D0 File Offset: 0x0002C7D0
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
				case 6:
					if (propertyName == "column")
					{
						this.body.ColumnID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.Column, p));
						return true;
					}
					break;
				case 7:
				{
					char c = propertyName[0];
					if (c != 'O')
					{
						if (c != 'o')
						{
							break;
						}
						if (!(propertyName == "ordinal"))
						{
							break;
						}
					}
					else if (!(propertyName == "Ordinal"))
					{
						break;
					}
					this.body.Ordinal = reader.ReadInt32Property();
					return true;
				}
				case 8:
					if (propertyName == "ColumnID")
					{
						this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
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
					if (!CompatibilityRestrictions.Level_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
							if (!(propertyName == "HierarchyID"))
							{
								break;
							}
							this.body.HierarchyID.ObjectID = reader.ReadObjectIdProperty();
							return true;
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
							break;
						}
						if (!(propertyName == "description"))
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
								catch (Exception ex)
								{
									throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex.Message), ex);
								}
							}
						}
						return true;
					}
					this.body.Description = reader.ReadStringProperty();
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
					if (!CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.SourceLineageTag = reader.ReadStringProperty();
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
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, changedProperty, TomSR.Exception_FailedAddDeserializedObject("ChangedProperty", ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					break;
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
								catch (Exception ex3)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex3.Message), ex3);
								}
							}
						}
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002EBC8 File Offset: 0x0002CDC8
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002EBD1 File Offset: 0x0002CDD1
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002EBF4 File Offset: 0x0002CDF4
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Ordinal != -1)
			{
				result["ordinal", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.Ordinal);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["lineageTag", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceLineageTag", TomPropCategory.Regular, 8, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ColumnID.Object != null)
			{
				this.body.ColumnID.SerializeToJsonObject(false, "column", ObjectType.Column, result, 5, false);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable = extendedProperties;
					}
					else
					{
						enumerable = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable2 = enumerable;
					if (enumerable2.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable2.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array2 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array2;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ChangedProperty> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ChangedProperty> changedProperties = this.ChangedProperties;
						enumerable3 = changedProperties;
					}
					else
					{
						enumerable3 = this.ChangedProperties.Where((ChangedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ChangedProperty> enumerable4 = enumerable3;
					if (enumerable4.Any<ChangedProperty>())
					{
						if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ChangedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ChangedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array3;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array4;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002F080 File Offset: 0x0002D280
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
				case 6:
					if (name == "column")
					{
						this.body.ColumnID.Path = new ObjectPath(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
						return true;
					}
					break;
				case 7:
					if (name == "ordinal")
					{
						this.body.Ordinal = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
						return true;
					}
					break;
				case 10:
					if (name == "lineageTag")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
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
						if (c == 'd')
						{
							if (name == "description")
							{
								this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
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
					if (name == "modifiedTime")
					{
						this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				case 16:
					if (name == "sourceLineageTag")
					{
						if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
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
				}
			}
			return false;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002F348 File Offset: 0x0002D548
		internal override string GetFormattedObjectPath()
		{
			if (this.Hierarchy != null && this.Hierarchy.Table != null)
			{
				return TomSR.ObjectPath_Level_3Args(this.Name, this.Hierarchy.Name, this.Hierarchy.Table.Name);
			}
			if (this.Hierarchy != null)
			{
				return TomSR.ObjectPath_Level_2Args(this.Name, this.Hierarchy.Name);
			}
			return TomSR.ObjectPath_Level_1Arg(this.Name);
		}

		// Token: 0x0400010C RID: 268
		internal Level.ObjectBody body;

		// Token: 0x0400010D RID: 269
		private LevelAnnotationCollection _Annotations;

		// Token: 0x0400010E RID: 270
		private LevelExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0400010F RID: 271
		private LevelChangedPropertyCollection _ChangedProperties;

		// Token: 0x02000276 RID: 630
		internal class ObjectBody : NamedMetadataObjectBody<Level>
		{
			// Token: 0x060020A2 RID: 8354 RVA: 0x000D5D67 File Offset: 0x000D3F67
			public ObjectBody(Level owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.HierarchyID = new ParentLink<Level, Hierarchy>(owner, "Hierarchy");
				this.ColumnID = new CrossLink<Level, Column>(owner, "Column");
			}

			// Token: 0x060020A3 RID: 8355 RVA: 0x000D5D9D File Offset: 0x000D3F9D
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x060020A4 RID: 8356 RVA: 0x000D5DA8 File Offset: 0x000D3FA8
			internal bool IsEqualTo(Level.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Ordinal, other.Ordinal) && PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.HierarchyID.IsEqualTo(other.HierarchyID, context)) && this.ColumnID.IsEqualTo(other.ColumnID, context);
			}

			// Token: 0x060020A5 RID: 8357 RVA: 0x000D5E88 File Offset: 0x000D4088
			internal void CopyFromImpl(Level.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Ordinal = other.Ordinal;
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.HierarchyID.CopyFrom(other.HierarchyID, context);
				}
				this.ColumnID.CopyFrom(other.ColumnID, context);
			}

			// Token: 0x060020A6 RID: 8358 RVA: 0x000D5F44 File Offset: 0x000D4144
			internal void CopyFromImpl(Level.ObjectBody other)
			{
				this.Ordinal = other.Ordinal;
				this.Name = other.Name;
				this.Description = other.Description;
				this.ModifiedTime = other.ModifiedTime;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.HierarchyID.CopyFrom(other.HierarchyID, ObjectChangeTracker.BodyCloneContext);
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060020A7 RID: 8359 RVA: 0x000D5FC5 File Offset: 0x000D41C5
			public override void CopyFrom(MetadataObjectBody<Level> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Level.ObjectBody)other, context);
			}

			// Token: 0x060020A8 RID: 8360 RVA: 0x000D5FDC File Offset: 0x000D41DC
			internal bool IsEqualTo(Level.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Ordinal, other.Ordinal) && PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && this.HierarchyID.IsEqualTo(other.HierarchyID) && this.ColumnID.IsEqualTo(other.ColumnID);
			}

			// Token: 0x060020A9 RID: 8361 RVA: 0x000D6092 File Offset: 0x000D4292
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Level.ObjectBody)other);
			}

			// Token: 0x060020AA RID: 8362 RVA: 0x000D60AC File Offset: 0x000D42AC
			internal void CompareWith(Level.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Ordinal, other.Ordinal))
				{
					context.RegisterPropertyChange(base.Owner, "Ordinal", typeof(int), PropertyFlags.DdlAndUser, other.Ordinal, this.Ordinal);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				this.HierarchyID.CompareWith(other.HierarchyID, "HierarchyID", "Hierarchy", PropertyFlags.ReadOnly, context);
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.None, context);
			}

			// Token: 0x060020AB RID: 8363 RVA: 0x000D6274 File Offset: 0x000D4474
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Level.ObjectBody)other, context);
			}

			// Token: 0x04000882 RID: 2178
			internal int Ordinal;

			// Token: 0x04000883 RID: 2179
			internal string Name;

			// Token: 0x04000884 RID: 2180
			internal string Description;

			// Token: 0x04000885 RID: 2181
			internal DateTime ModifiedTime;

			// Token: 0x04000886 RID: 2182
			internal string LineageTag;

			// Token: 0x04000887 RID: 2183
			internal string SourceLineageTag;

			// Token: 0x04000888 RID: 2184
			internal ParentLink<Level, Hierarchy> HierarchyID;

			// Token: 0x04000889 RID: 2185
			internal CrossLink<Level, Column> ColumnID;
		}
	}
}
