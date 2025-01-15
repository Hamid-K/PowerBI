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
	// Token: 0x020000B1 RID: 177
	public abstract class Relationship : NamedMetadataObject
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0005913D File Offset: 0x0005733D
		private protected Relationship()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00059150 File Offset: 0x00057350
		private protected Relationship(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00059160 File Offset: 0x00057360
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Relationship.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.IsActive = true;
			this.body.Type = RelationshipType.SingleColumn;
			this.body.CrossFilteringBehavior = CrossFilteringBehavior.OneDirection;
			this.body.JoinOnDateBehavior = DateTimeRelationshipBehavior.DateAndTime;
			this.body.RelyOnReferentialIntegrity = false;
			this.body.FromCardinality = RelationshipEndCardinality.None;
			this.body.ToCardinality = RelationshipEndCardinality.None;
			this.body.State = ObjectState.CalculationNeeded;
			this.body.SecurityFilteringBehavior = SecurityFilteringBehavior.OneDirection;
			this._Annotations = new RelationshipAnnotationCollection(this, comparer);
			this._ExtendedProperties = new RelationshipExtendedPropertyCollection(this, comparer);
			this._ChangedProperties = new RelationshipChangedPropertyCollection(this);
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0005921B File Offset: 0x0005741B
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Relationship;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0005921E File Offset: 0x0005741E
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00059230 File Offset: 0x00057430
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
					MetadataObject.UpdateMetadataObjectParent<Relationship, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0005925D File Offset: 0x0005745D
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00059270 File Offset: 0x00057470
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				Relationship.WriteMetadataSchemaForSingleColumnRelationship(context, writer);
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000592A8 File Offset: 0x000574A8
		private static void WriteMetadataSchemaForSingleColumnRelationship(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Relationship, "SingleColumnRelationship", "SingleColumnRelationship object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
				{
					writer.WriteEnumProperty<RelationshipType>("type", MetadataPropertyNature.TypeProperty, null);
				}
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("isActive", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isActive", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("crossFilteringBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<CrossFilteringBehavior>("crossFilteringBehavior", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("joinOnDateBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DateTimeRelationshipBehavior>("joinOnDateBehavior", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("relyOnReferentialIntegrity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("relyOnReferentialIntegrity", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("securityFilteringBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<SecurityFilteringBehavior>("securityFilteringBehavior", MetadataPropertyNature.RegularProperty, PropertyHelper.GetSecurityFilteringBehaviorCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("fromCardinality", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RelationshipEndCardinality>("fromCardinality", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("toCardinality", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RelationshipEndCardinality>("toCardinality", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("fromColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					if (context.SerializationMode == MetadataSerializationMode.Json)
					{
						CrossLink<Relationship, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, false, "from", false, writer);
					}
					else
					{
						CrossLink<Relationship, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, true, "fromColumn", false, writer);
					}
				}
				if (writer.ShouldIncludeProperty("toColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					if (context.SerializationMode == MetadataSerializationMode.Json)
					{
						CrossLink<Relationship, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, false, "to", false, writer);
					}
					else
					{
						CrossLink<Relationship, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, true, "toColumn", false, writer);
					}
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

		// Token: 0x06000AEC RID: 2796 RVA: 0x0005959C File Offset: 0x0005779C
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.SecurityFilteringBehavior != SecurityFilteringBehavior.OneDirection)
			{
				int num = PropertyHelper.GetSecurityFilteringBehaviorCompatibilityRestrictions(this.body.SecurityFilteringBehavior)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SecurityFilteringBehavior");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0005960A File Offset: 0x0005780A
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00059612 File Offset: 0x00057812
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Relationship.ObjectBody)value;
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00059620 File Offset: 0x00057820
		internal override ITxObjectBody CreateBody()
		{
			return new Relationship.ObjectBody(this);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00059628 File Offset: 0x00057828
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Relationships;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00059638 File Offset: 0x00057838
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<Relationship, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			this.body.FromTableID.ResolveById(objectMap, throwIfCantResolve);
			this.body.FromColumnID.ResolveById(objectMap, throwIfCantResolve);
			this.body.ToTableID.ResolveById(objectMap, throwIfCantResolve);
			this.body.ToColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (model != null)
			{
				model.Relationships.Add(this);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000596B8 File Offset: 0x000578B8
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.FromTableID.ResolveById(objectMap, throwIfCantResolve);
			this.body.FromColumnID.ResolveById(objectMap, throwIfCantResolve);
			this.body.ToTableID.ResolveById(objectMap, throwIfCantResolve);
			this.body.ToColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00059714 File Offset: 0x00057914
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.FromTableID.IsResolved && !this.body.FromTableID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "FromTable"));
				}
				flag = false;
			}
			if (!this.body.FromColumnID.IsResolved && !this.body.FromColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "FromColumn"));
				}
				flag = false;
			}
			if (!this.body.ToTableID.IsResolved && !this.body.ToTableID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ToTable"));
				}
				flag = false;
			}
			if (!this.body.ToColumnID.IsResolved && !this.body.ToColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ToColumn"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00059834 File Offset: 0x00057A34
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.FromTableID.TryResolveAfterCopy(copyContext);
			this.body.FromColumnID.TryResolveAfterCopy(copyContext);
			this.body.ToTableID.TryResolveAfterCopy(copyContext);
			this.body.ToColumnID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0005988C File Offset: 0x00057A8C
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.FromTableID.Validate(result, throwOnError);
			this.body.FromColumnID.Validate(result, throwOnError);
			this.body.ToTableID.Validate(result, throwOnError);
			this.body.ToColumnID.Validate(result, throwOnError);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000598E4 File Offset: 0x00057AE4
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.FromTableID.IsResolved || !this.body.FromColumnID.IsResolved || !this.body.ToTableID.IsResolved || !this.body.ToColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00059947 File Offset: 0x00057B47
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ChangedProperties;
			yield break;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00059957 File Offset: 0x00057B57
		public RelationshipAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0005995F File Offset: 0x00057B5F
		[CompatibilityRequirement("1400")]
		public RelationshipExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00059967 File Offset: 0x00057B67
		[CompatibilityRequirement("1567")]
		public RelationshipChangedPropertyCollection ChangedProperties
		{
			get
			{
				return this._ChangedProperties;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0005996F File Offset: 0x00057B6F
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0005997C File Offset: 0x00057B7C
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Relationship, out text))
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

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000599FE File Offset: 0x00057BFE
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00059A0C File Offset: 0x00057C0C
		public bool IsActive
		{
			get
			{
				return this.body.IsActive;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsActive, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsActive", typeof(bool), this.body.IsActive, value);
					bool isActive = this.body.IsActive;
					this.body.IsActive = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsActive", typeof(bool), isActive, value);
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00059A90 File Offset: 0x00057C90
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00059AA0 File Offset: 0x00057CA0
		public RelationshipType Type
		{
			get
			{
				return this.body.Type;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Type, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Type", typeof(RelationshipType), this.body.Type, value);
					RelationshipType type = this.body.Type;
					this.body.Type = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Type", typeof(RelationshipType), type, value);
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00059B24 File Offset: 0x00057D24
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00059B34 File Offset: 0x00057D34
		public CrossFilteringBehavior CrossFilteringBehavior
		{
			get
			{
				return this.body.CrossFilteringBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.CrossFilteringBehavior, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "CrossFilteringBehavior", typeof(CrossFilteringBehavior), this.body.CrossFilteringBehavior, value);
					CrossFilteringBehavior crossFilteringBehavior = this.body.CrossFilteringBehavior;
					this.body.CrossFilteringBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "CrossFilteringBehavior", typeof(CrossFilteringBehavior), crossFilteringBehavior, value);
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00059BB8 File Offset: 0x00057DB8
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00059BC8 File Offset: 0x00057DC8
		public DateTimeRelationshipBehavior JoinOnDateBehavior
		{
			get
			{
				return this.body.JoinOnDateBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.JoinOnDateBehavior, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "JoinOnDateBehavior", typeof(DateTimeRelationshipBehavior), this.body.JoinOnDateBehavior, value);
					DateTimeRelationshipBehavior joinOnDateBehavior = this.body.JoinOnDateBehavior;
					this.body.JoinOnDateBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "JoinOnDateBehavior", typeof(DateTimeRelationshipBehavior), joinOnDateBehavior, value);
				}
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00059C4C File Offset: 0x00057E4C
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00059C5C File Offset: 0x00057E5C
		public bool RelyOnReferentialIntegrity
		{
			get
			{
				return this.body.RelyOnReferentialIntegrity;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RelyOnReferentialIntegrity, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RelyOnReferentialIntegrity", typeof(bool), this.body.RelyOnReferentialIntegrity, value);
					bool relyOnReferentialIntegrity = this.body.RelyOnReferentialIntegrity;
					this.body.RelyOnReferentialIntegrity = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RelyOnReferentialIntegrity", typeof(bool), relyOnReferentialIntegrity, value);
				}
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00059CE0 File Offset: 0x00057EE0
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00059CF0 File Offset: 0x00057EF0
		internal RelationshipEndCardinality FromCardinality
		{
			get
			{
				return this.body.FromCardinality;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FromCardinality, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FromCardinality", typeof(RelationshipEndCardinality), this.body.FromCardinality, value);
					RelationshipEndCardinality fromCardinality = this.body.FromCardinality;
					this.body.FromCardinality = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FromCardinality", typeof(RelationshipEndCardinality), fromCardinality, value);
				}
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00059D74 File Offset: 0x00057F74
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00059D84 File Offset: 0x00057F84
		internal RelationshipEndCardinality ToCardinality
		{
			get
			{
				return this.body.ToCardinality;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ToCardinality, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ToCardinality", typeof(RelationshipEndCardinality), this.body.ToCardinality, value);
					RelationshipEndCardinality toCardinality = this.body.ToCardinality;
					this.body.ToCardinality = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ToCardinality", typeof(RelationshipEndCardinality), toCardinality, value);
				}
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00059E08 File Offset: 0x00058008
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00059E18 File Offset: 0x00058018
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

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00059F3A File Offset: 0x0005813A
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00059F48 File Offset: 0x00058148
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

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00059FCC File Offset: 0x000581CC
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00059FDC File Offset: 0x000581DC
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0005A060 File Offset: 0x00058260
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0005A070 File Offset: 0x00058270
		public SecurityFilteringBehavior SecurityFilteringBehavior
		{
			get
			{
				return this.body.SecurityFilteringBehavior;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SecurityFilteringBehavior, value))
				{
					CompatibilityRestrictionSet securityFilteringBehaviorCompatibilityRestrictions = PropertyHelper.GetSecurityFilteringBehaviorCompatibilityRestrictions(value);
					CompatibilityRestrictionSet securityFilteringBehaviorCompatibilityRestrictions2 = PropertyHelper.GetSecurityFilteringBehaviorCompatibilityRestrictions(this.body.SecurityFilteringBehavior);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = securityFilteringBehaviorCompatibilityRestrictions.Compare(securityFilteringBehaviorCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != SecurityFilteringBehavior.OneDirection))
					{
						array = base.ValidateCompatibilityRequirement(securityFilteringBehaviorCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "SecurityFilteringBehavior", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SecurityFilteringBehavior", typeof(SecurityFilteringBehavior), this.body.SecurityFilteringBehavior, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(securityFilteringBehaviorCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(securityFilteringBehaviorCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(securityFilteringBehaviorCompatibilityRestrictions, array);
						break;
					}
					SecurityFilteringBehavior securityFilteringBehavior = this.body.SecurityFilteringBehavior;
					this.body.SecurityFilteringBehavior = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SecurityFilteringBehavior", typeof(SecurityFilteringBehavior), securityFilteringBehavior, value);
				}
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0005A192 File Offset: 0x00058392
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0005A1A4 File Offset: 0x000583A4
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0005A1B7 File Offset: 0x000583B7
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0005A1CC File Offset: 0x000583CC
		public Table FromTable
		{
			get
			{
				return this.body.FromTableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FromTableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FromTable", typeof(Table), this.body.FromTableID.Object, value);
					Table @object = this.body.FromTableID.Object;
					this.body.FromTableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FromTable", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0005A250 File Offset: 0x00058450
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0005A262 File Offset: 0x00058462
		internal ObjectId _FromTableID
		{
			get
			{
				return this.body.FromTableID.ObjectID;
			}
			set
			{
				this.body.FromTableID.ObjectID = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0005A275 File Offset: 0x00058475
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0005A288 File Offset: 0x00058488
		internal Column FromColumn
		{
			get
			{
				return this.body.FromColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FromColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FromColumn", typeof(Column), this.body.FromColumnID.Object, value);
					Column @object = this.body.FromColumnID.Object;
					this.body.FromColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FromColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0005A30C File Offset: 0x0005850C
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0005A31E File Offset: 0x0005851E
		internal ObjectId _FromColumnID
		{
			get
			{
				return this.body.FromColumnID.ObjectID;
			}
			set
			{
				this.body.FromColumnID.ObjectID = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0005A331 File Offset: 0x00058531
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0005A344 File Offset: 0x00058544
		public Table ToTable
		{
			get
			{
				return this.body.ToTableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ToTableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ToTable", typeof(Table), this.body.ToTableID.Object, value);
					Table @object = this.body.ToTableID.Object;
					this.body.ToTableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ToTable", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0005A3C8 File Offset: 0x000585C8
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0005A3DA File Offset: 0x000585DA
		internal ObjectId _ToTableID
		{
			get
			{
				return this.body.ToTableID.ObjectID;
			}
			set
			{
				this.body.ToTableID.ObjectID = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0005A3ED File Offset: 0x000585ED
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0005A400 File Offset: 0x00058600
		internal Column ToColumn
		{
			get
			{
				return this.body.ToColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ToColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ToColumn", typeof(Column), this.body.ToColumnID.Object, value);
					Column @object = this.body.ToColumnID.Object;
					this.body.ToColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ToColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0005A484 File Offset: 0x00058684
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0005A496 File Offset: 0x00058696
		internal ObjectId _ToColumnID
		{
			get
			{
				return this.body.ToColumnID.ObjectID;
			}
			set
			{
				this.body.ToColumnID.ObjectID = value;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0005A4AC File Offset: 0x000586AC
		internal void CopyFrom(Relationship other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0 || this.body.RefreshedTime.CompareTo(other.body.RefreshedTime) != 0;
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

		// Token: 0x06000B26 RID: 2854 RVA: 0x0005A5A0 File Offset: 0x000587A0
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Relationship)other, context);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0005A5AF File Offset: 0x000587AF
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Relationship other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0005A5CB File Offset: 0x000587CB
		public void CopyTo(Relationship other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0005A5E7 File Offset: 0x000587E7
		public Relationship Clone()
		{
			return base.CloneInternal<Relationship>();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0005A5F0 File Offset: 0x000587F0
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			this.body.FromTableID.Validate(null, true);
			if (this.body.FromTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "FromTableID", this.body.FromTableID.Object);
			}
			this.body.FromColumnID.Validate(null, true);
			if (this.body.FromColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "FromColumnID", this.body.FromColumnID.Object);
			}
			this.body.ToTableID.Validate(null, true);
			if (this.body.ToTableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ToTableID", this.body.ToTableID.Object);
			}
			this.body.ToColumnID.Validate(null, true);
			if (this.body.ToColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ToColumnID", this.body.ToColumnID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!this.body.IsActive)
			{
				writer.WriteProperty<bool>(options, "IsActive", this.body.IsActive);
			}
			if (this.body.Type != RelationshipType.SingleColumn)
			{
				writer.WriteProperty<RelationshipType>(options, "Type", this.body.Type);
			}
			if (this.body.CrossFilteringBehavior != CrossFilteringBehavior.OneDirection)
			{
				writer.WriteProperty<CrossFilteringBehavior>(options, "CrossFilteringBehavior", this.body.CrossFilteringBehavior);
			}
			if (this.body.JoinOnDateBehavior != DateTimeRelationshipBehavior.DateAndTime)
			{
				writer.WriteProperty<DateTimeRelationshipBehavior>(options, "JoinOnDateBehavior", this.body.JoinOnDateBehavior);
			}
			if (this.body.RelyOnReferentialIntegrity)
			{
				writer.WriteProperty<bool>(options, "RelyOnReferentialIntegrity", this.body.RelyOnReferentialIntegrity);
			}
			if (this.body.FromCardinality != RelationshipEndCardinality.None)
			{
				writer.WriteProperty<RelationshipEndCardinality>(options, "FromCardinality", this.body.FromCardinality);
			}
			if (this.body.ToCardinality != RelationshipEndCardinality.None)
			{
				writer.WriteProperty<RelationshipEndCardinality>(options, "ToCardinality", this.body.ToCardinality);
			}
			if (this.body.SecurityFilteringBehavior != SecurityFilteringBehavior.OneDirection)
			{
				if (!PropertyHelper.IsSecurityFilteringBehaviorValueCompatible(this.body.SecurityFilteringBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SecurityFilteringBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<SecurityFilteringBehavior>(options, "SecurityFilteringBehavior", this.body.SecurityFilteringBehavior);
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0005A890 File Offset: 0x00058A90
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("FromTableID", out objectId2))
			{
				this.body.FromTableID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (reader.TryReadProperty<ObjectId>("FromColumnID", out objectId3))
			{
				this.body.FromColumnID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (reader.TryReadProperty<ObjectId>("ToTableID", out objectId4))
			{
				this.body.ToTableID.ObjectID = objectId4;
			}
			ObjectId objectId5;
			if (reader.TryReadProperty<ObjectId>("ToColumnID", out objectId5))
			{
				this.body.ToColumnID.ObjectID = objectId5;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			bool flag;
			if (reader.TryReadProperty<bool>("IsActive", out flag))
			{
				this.body.IsActive = flag;
			}
			RelationshipType relationshipType;
			if (reader.TryReadProperty<RelationshipType>("Type", out relationshipType))
			{
				this.body.Type = relationshipType;
			}
			CrossFilteringBehavior crossFilteringBehavior;
			if (reader.TryReadProperty<CrossFilteringBehavior>("CrossFilteringBehavior", out crossFilteringBehavior))
			{
				this.body.CrossFilteringBehavior = crossFilteringBehavior;
			}
			DateTimeRelationshipBehavior dateTimeRelationshipBehavior;
			if (reader.TryReadProperty<DateTimeRelationshipBehavior>("JoinOnDateBehavior", out dateTimeRelationshipBehavior))
			{
				this.body.JoinOnDateBehavior = dateTimeRelationshipBehavior;
			}
			bool flag2;
			if (reader.TryReadProperty<bool>("RelyOnReferentialIntegrity", out flag2))
			{
				this.body.RelyOnReferentialIntegrity = flag2;
			}
			RelationshipEndCardinality relationshipEndCardinality;
			if (reader.TryReadProperty<RelationshipEndCardinality>("FromCardinality", out relationshipEndCardinality))
			{
				this.body.FromCardinality = relationshipEndCardinality;
			}
			RelationshipEndCardinality relationshipEndCardinality2;
			if (reader.TryReadProperty<RelationshipEndCardinality>("ToCardinality", out relationshipEndCardinality2))
			{
				this.body.ToCardinality = relationshipEndCardinality2;
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
			if (reader.TryReadProperty<DateTime>("RefreshedTime", out dateTime2))
			{
				this.body.RefreshedTime = dateTime2;
			}
			SecurityFilteringBehavior securityFilteringBehavior;
			if (reader.TryReadProperty<SecurityFilteringBehavior>("SecurityFilteringBehavior", out securityFilteringBehavior))
			{
				this.body.SecurityFilteringBehavior = securityFilteringBehavior;
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0005AA98 File Offset: 0x00058C98
		private protected sealed override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			this.body.FromTableID.Validate(null, true);
			if (this.body.FromTableID.Object != null && writer.ShouldIncludeProperty("FromTableID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("FromTableID", MetadataPropertyNature.CrossLinkProperty, this.body.FromTableID.Object);
			}
			this.body.FromColumnID.Validate(null, true);
			if (this.body.FromColumnID.Object != null && writer.ShouldIncludeProperty("FromColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("FromColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.FromColumnID.Object);
			}
			this.body.ToTableID.Validate(null, true);
			if (this.body.ToTableID.Object != null && writer.ShouldIncludeProperty("ToTableID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ToTableID", MetadataPropertyNature.CrossLinkProperty, this.body.ToTableID.Object);
			}
			this.body.ToColumnID.Validate(null, true);
			if (this.body.ToColumnID.Object != null && writer.ShouldIncludeProperty("ToColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ToColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.ToColumnID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!this.body.IsActive && writer.ShouldIncludeProperty("IsActive", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsActive", MetadataPropertyNature.RegularProperty, this.body.IsActive);
			}
			if (this.body.Type != RelationshipType.SingleColumn && writer.ShouldIncludeProperty("Type", MetadataPropertyNature.TypeProperty))
			{
				writer.WriteEnumProperty<RelationshipType>("Type", MetadataPropertyNature.TypeProperty, this.body.Type);
			}
			if (this.body.CrossFilteringBehavior != CrossFilteringBehavior.OneDirection && writer.ShouldIncludeProperty("CrossFilteringBehavior", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<CrossFilteringBehavior>("CrossFilteringBehavior", MetadataPropertyNature.RegularProperty, this.body.CrossFilteringBehavior);
			}
			if (this.body.JoinOnDateBehavior != DateTimeRelationshipBehavior.DateAndTime && writer.ShouldIncludeProperty("JoinOnDateBehavior", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DateTimeRelationshipBehavior>("JoinOnDateBehavior", MetadataPropertyNature.RegularProperty, this.body.JoinOnDateBehavior);
			}
			if (this.body.RelyOnReferentialIntegrity && writer.ShouldIncludeProperty("RelyOnReferentialIntegrity", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("RelyOnReferentialIntegrity", MetadataPropertyNature.RegularProperty, this.body.RelyOnReferentialIntegrity);
			}
			if (this.body.FromCardinality != RelationshipEndCardinality.None && writer.ShouldIncludeProperty("FromCardinality", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<RelationshipEndCardinality>("FromCardinality", MetadataPropertyNature.RegularProperty, this.body.FromCardinality);
			}
			if (this.body.ToCardinality != RelationshipEndCardinality.None && writer.ShouldIncludeProperty("ToCardinality", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<RelationshipEndCardinality>("ToCardinality", MetadataPropertyNature.RegularProperty, this.body.ToCardinality);
			}
			if (this.body.SecurityFilteringBehavior != SecurityFilteringBehavior.OneDirection)
			{
				if (!PropertyHelper.IsSecurityFilteringBehaviorValueCompatible(this.body.SecurityFilteringBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SecurityFilteringBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SecurityFilteringBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<SecurityFilteringBehavior>("SecurityFilteringBehavior", MetadataPropertyNature.RegularProperty, this.body.SecurityFilteringBehavior);
				}
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0005AE04 File Offset: 0x00059004
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!this.body.IsActive && writer.ShouldIncludeProperty("isActive", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isActive", MetadataPropertyNature.RegularProperty, this.body.IsActive);
			}
			if (this.body.CrossFilteringBehavior != CrossFilteringBehavior.OneDirection && writer.ShouldIncludeProperty("crossFilteringBehavior", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<CrossFilteringBehavior>("crossFilteringBehavior", MetadataPropertyNature.RegularProperty, this.body.CrossFilteringBehavior);
			}
			if (this.body.JoinOnDateBehavior != DateTimeRelationshipBehavior.DateAndTime && writer.ShouldIncludeProperty("joinOnDateBehavior", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DateTimeRelationshipBehavior>("joinOnDateBehavior", MetadataPropertyNature.RegularProperty, this.body.JoinOnDateBehavior);
			}
			if (this.body.RelyOnReferentialIntegrity && writer.ShouldIncludeProperty("relyOnReferentialIntegrity", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("relyOnReferentialIntegrity", MetadataPropertyNature.RegularProperty, this.body.RelyOnReferentialIntegrity);
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
			if (this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.RefreshedTime);
			}
			if (this.body.SecurityFilteringBehavior != SecurityFilteringBehavior.OneDirection)
			{
				if (!PropertyHelper.IsSecurityFilteringBehaviorValueCompatible(this.body.SecurityFilteringBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SecurityFilteringBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("securityFilteringBehavior", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<SecurityFilteringBehavior>("securityFilteringBehavior", MetadataPropertyNature.RegularProperty, this.body.SecurityFilteringBehavior);
				}
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0005B067 File Offset: 0x00059267
		private protected virtual void WriteCrossLinksToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0005B06C File Offset: 0x0005926C
		private protected sealed override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (this.body.Type != RelationshipType.SingleColumn && writer.ShouldIncludeProperty("type", MetadataPropertyNature.TypeProperty))
			{
				writer.WriteEnumProperty<RelationshipType>("type", MetadataPropertyNature.TypeProperty, this.body.Type);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			this.WriteRegularPropertiesToMetadataStream(context, writer);
			this.WriteCrossLinksToMetadataStream(context, writer);
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

		// Token: 0x06000B30 RID: 2864 RVA: 0x0005B21C File Offset: 0x0005941C
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
					if (c <= 'T')
					{
						if (c != 'N')
						{
							if (c != 'T')
							{
								break;
							}
							if (!(propertyName == "Type"))
							{
								break;
							}
							goto IL_0516;
						}
						else if (!(propertyName == "Name"))
						{
							break;
						}
					}
					else if (c != 'n')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "type"))
						{
							break;
						}
						goto IL_0516;
					}
					else if (!(propertyName == "name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
					IL_0516:
					this.body.Type = reader.ReadEnumProperty<RelationshipType>();
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
				case 7:
					if (propertyName == "ModelID")
					{
						this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
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
						if (!(propertyName == "isActive"))
						{
							break;
						}
					}
					else if (!(propertyName == "IsActive"))
					{
						break;
					}
					this.body.IsActive = reader.ReadBooleanProperty();
					return true;
				}
				case 9:
					if (propertyName == "ToTableID")
					{
						this.body.ToTableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 10:
					if (propertyName == "ToColumnID")
					{
						this.body.ToColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 11:
				{
					char c = propertyName[0];
					if (c != 'F')
					{
						if (c == 'a')
						{
							if (propertyName == "annotations")
							{
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
						}
					}
					else if (propertyName == "FromTableID")
					{
						this.body.FromTableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c != 'F')
					{
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
					if (propertyName == "FromColumnID")
					{
						this.body.FromColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c != 'R')
					{
						if (c != 'T')
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
							if (!(propertyName == "ToCardinality"))
							{
								break;
							}
							this.body.ToCardinality = reader.ReadEnumProperty<RelationshipEndCardinality>();
							return true;
						}
					}
					else if (!(propertyName == "RefreshedTime"))
					{
						break;
					}
					this.body.RefreshedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 15:
					if (propertyName == "FromCardinality")
					{
						this.body.FromCardinality = reader.ReadEnumProperty<RelationshipEndCardinality>();
						return true;
					}
					break;
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
				{
					char c = propertyName[0];
					if (c != 'J')
					{
						if (c != 'e')
						{
							if (c != 'j')
							{
								break;
							}
							if (!(propertyName == "joinOnDateBehavior"))
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
									catch (Exception ex3)
									{
										throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex3.Message), ex3);
									}
								}
							}
							return true;
						}
					}
					else if (!(propertyName == "JoinOnDateBehavior"))
					{
						break;
					}
					this.body.JoinOnDateBehavior = reader.ReadEnumProperty<DateTimeRelationshipBehavior>();
					return true;
				}
				case 22:
				{
					char c = propertyName[0];
					if (c != 'C')
					{
						if (c != 'c')
						{
							break;
						}
						if (!(propertyName == "crossFilteringBehavior"))
						{
							break;
						}
					}
					else if (!(propertyName == "CrossFilteringBehavior"))
					{
						break;
					}
					this.body.CrossFilteringBehavior = reader.ReadEnumProperty<CrossFilteringBehavior>();
					return true;
				}
				case 25:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "securityFilteringBehavior"))
						{
							break;
						}
					}
					else if (!(propertyName == "SecurityFilteringBehavior"))
					{
						break;
					}
					SecurityFilteringBehavior securityFilteringBehavior = reader.ReadEnumProperty<SecurityFilteringBehavior>();
					if (!PropertyHelper.IsSecurityFilteringBehaviorValueCompatible(securityFilteringBehavior, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.SecurityFilteringBehavior = securityFilteringBehavior;
					return true;
				}
				case 26:
				{
					char c = propertyName[0];
					if (c != 'R')
					{
						if (c != 'r')
						{
							break;
						}
						if (!(propertyName == "relyOnReferentialIntegrity"))
						{
							break;
						}
					}
					else if (!(propertyName == "RelyOnReferentialIntegrity"))
					{
						break;
					}
					this.body.RelyOnReferentialIntegrity = reader.ReadBooleanProperty();
					return true;
				}
				}
			}
			return false;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0005BA68 File Offset: 0x00059C68
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0005BA71 File Offset: 0x00059C71
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0005BA94 File Offset: 0x00059C94
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !this.body.IsActive)
			{
				result["isActive", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsActive);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Type != RelationshipType.SingleColumn)
			{
				result["type", TomPropCategory.Type, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<RelationshipType>(this.body.Type);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.CrossFilteringBehavior != CrossFilteringBehavior.OneDirection)
			{
				result["crossFilteringBehavior", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertEnumToJsonValue<CrossFilteringBehavior>(this.body.CrossFilteringBehavior);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.JoinOnDateBehavior != DateTimeRelationshipBehavior.DateAndTime)
			{
				result["joinOnDateBehavior", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DateTimeRelationshipBehavior>(this.body.JoinOnDateBehavior);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.RelyOnReferentialIntegrity)
			{
				result["relyOnReferentialIntegrity", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.RelyOnReferentialIntegrity);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.CalculationNeeded)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 14, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 17, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["refreshedTime", TomPropCategory.Regular, 18, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.RefreshedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.SecurityFilteringBehavior != SecurityFilteringBehavior.OneDirection)
			{
				if (!PropertyHelper.IsSecurityFilteringBehaviorValueCompatible(this.body.SecurityFilteringBehavior, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SecurityFilteringBehavior is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["securityFilteringBehavior", TomPropCategory.Regular, 19, false] = JsonPropertyHelper.ConvertEnumToJsonValue<SecurityFilteringBehavior>(this.body.SecurityFilteringBehavior);
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

		// Token: 0x06000B34 RID: 2868 RVA: 0x0005BFF8 File Offset: 0x0005A1F8
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				switch (length)
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
								this.body.Type = JsonPropertyHelper.ConvertJsonValueToEnum<RelationshipType>(jsonProp.Value);
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
				case 7:
				case 9:
				case 10:
				case 14:
				case 15:
				case 16:
					break;
				case 8:
					if (name == "isActive")
					{
						this.body.IsActive = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
						return true;
					}
					break;
				case 11:
					if (name == "annotations")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				case 12:
					if (name == "modifiedTime")
					{
						this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				case 13:
					if (name == "refreshedTime")
					{
						this.body.RefreshedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
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
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 'j')
						{
							if (name == "joinOnDateBehavior")
							{
								this.body.JoinOnDateBehavior = JsonPropertyHelper.ConvertJsonValueToEnum<DateTimeRelationshipBehavior>(jsonProp.Value);
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
					break;
				}
				default:
					switch (length)
					{
					case 22:
						if (name == "crossFilteringBehavior")
						{
							this.body.CrossFilteringBehavior = JsonPropertyHelper.ConvertJsonValueToEnum<CrossFilteringBehavior>(jsonProp.Value);
							return true;
						}
						break;
					case 25:
						if (name == "securityFilteringBehavior")
						{
							SecurityFilteringBehavior securityFilteringBehavior = JsonPropertyHelper.ConvertJsonValueToEnum<SecurityFilteringBehavior>(jsonProp.Value);
							if (jsonProp.Value.Type != 10 && !PropertyHelper.IsSecurityFilteringBehaviorValueCompatible(securityFilteringBehavior, mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.SecurityFilteringBehavior = securityFilteringBehavior;
							return true;
						}
						break;
					case 26:
						if (name == "relyOnReferentialIntegrity")
						{
							this.body.RelyOnReferentialIntegrity = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
							return true;
						}
						break;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0005C374 File Offset: 0x0005A574
		internal static Relationship CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			RelationshipType relationshipType;
			if (reader.TryMoveToProperty("type"))
			{
				relationshipType = reader.ReadEnumProperty<RelationshipType>();
			}
			else
			{
				relationshipType = RelationshipType.SingleColumn;
			}
			reader.Reset();
			if (relationshipType == RelationshipType.SingleColumn)
			{
				return new SingleColumnRelationship();
			}
			throw reader.CreateInvalidDataException(context, TomSR.Exception_UnrecognizedValueOfType("RelationshipType", relationshipType.ToString()), null);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0005C3C8 File Offset: 0x0005A5C8
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Relationship_1Arg(this.Name);
		}

		// Token: 0x0400016A RID: 362
		internal Relationship.ObjectBody body;

		// Token: 0x0400016B RID: 363
		private RelationshipAnnotationCollection _Annotations;

		// Token: 0x0400016C RID: 364
		private RelationshipExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0400016D RID: 365
		private RelationshipChangedPropertyCollection _ChangedProperties;

		// Token: 0x0400016E RID: 366
		internal static Func<Relationship, Relationship, bool> CompareRelationshipType = (Relationship relationship1, Relationship relationship2) => relationship1.Type == relationship2.Type;

		// Token: 0x020002C6 RID: 710
		internal class ObjectBody : NamedMetadataObjectBody<Relationship>
		{
			// Token: 0x060022D3 RID: 8915 RVA: 0x000DEB14 File Offset: 0x000DCD14
			public ObjectBody(Relationship owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.RefreshedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<Relationship, Model>(owner, "Model");
				this.FromTableID = new CrossLink<Relationship, Table>(owner, "FromTable");
				this.FromColumnID = new CrossLink<Relationship, Column>(owner, "FromColumn");
				this.ToTableID = new CrossLink<Relationship, Table>(owner, "ToTable");
				this.ToColumnID = new CrossLink<Relationship, Column>(owner, "ToColumn");
			}

			// Token: 0x060022D4 RID: 8916 RVA: 0x000DEB93 File Offset: 0x000DCD93
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x060022D5 RID: 8917 RVA: 0x000DEB9C File Offset: 0x000DCD9C
			internal bool IsEqualTo(Relationship.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.IsActive, other.IsActive) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.Type, other.Type)) && PropertyHelper.AreValuesIdentical(this.CrossFilteringBehavior, other.CrossFilteringBehavior) && PropertyHelper.AreValuesIdentical(this.JoinOnDateBehavior, other.JoinOnDateBehavior) && PropertyHelper.AreValuesIdentical(this.RelyOnReferentialIntegrity, other.RelyOnReferentialIntegrity) && PropertyHelper.AreValuesIdentical(this.FromCardinality, other.FromCardinality) && PropertyHelper.AreValuesIdentical(this.ToCardinality, other.ToCardinality) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime)) && PropertyHelper.AreValuesIdentical(this.SecurityFilteringBehavior, other.SecurityFilteringBehavior) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context)) && this.FromTableID.IsEqualTo(other.FromTableID, context) && this.FromColumnID.IsEqualTo(other.FromColumnID, context) && this.ToTableID.IsEqualTo(other.ToTableID, context) && this.ToColumnID.IsEqualTo(other.ToColumnID, context);
			}

			// Token: 0x060022D6 RID: 8918 RVA: 0x000DED74 File Offset: 0x000DCF74
			internal void CopyFromImpl(Relationship.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.IsActive = other.IsActive;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.Type = other.Type;
				}
				this.CrossFilteringBehavior = other.CrossFilteringBehavior;
				this.JoinOnDateBehavior = other.JoinOnDateBehavior;
				this.RelyOnReferentialIntegrity = other.RelyOnReferentialIntegrity;
				this.FromCardinality = other.FromCardinality;
				this.ToCardinality = other.ToCardinality;
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
					this.RefreshedTime = other.RefreshedTime;
				}
				this.SecurityFilteringBehavior = other.SecurityFilteringBehavior;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
				this.FromTableID.CopyFrom(other.FromTableID, context);
				this.FromColumnID.CopyFrom(other.FromColumnID, context);
				this.ToTableID.CopyFrom(other.ToTableID, context);
				this.ToColumnID.CopyFrom(other.ToColumnID, context);
			}

			// Token: 0x060022D7 RID: 8919 RVA: 0x000DEEE4 File Offset: 0x000DD0E4
			internal void CopyFromImpl(Relationship.ObjectBody other)
			{
				this.Name = other.Name;
				this.IsActive = other.IsActive;
				this.Type = other.Type;
				this.CrossFilteringBehavior = other.CrossFilteringBehavior;
				this.JoinOnDateBehavior = other.JoinOnDateBehavior;
				this.RelyOnReferentialIntegrity = other.RelyOnReferentialIntegrity;
				this.FromCardinality = other.FromCardinality;
				this.ToCardinality = other.ToCardinality;
				this.State = other.State;
				this.ModifiedTime = other.ModifiedTime;
				this.RefreshedTime = other.RefreshedTime;
				this.SecurityFilteringBehavior = other.SecurityFilteringBehavior;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
				this.FromTableID.CopyFrom(other.FromTableID, ObjectChangeTracker.BodyCloneContext);
				this.FromColumnID.CopyFrom(other.FromColumnID, ObjectChangeTracker.BodyCloneContext);
				this.ToTableID.CopyFrom(other.ToTableID, ObjectChangeTracker.BodyCloneContext);
				this.ToColumnID.CopyFrom(other.ToColumnID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060022D8 RID: 8920 RVA: 0x000DEFEF File Offset: 0x000DD1EF
			public override void CopyFrom(MetadataObjectBody<Relationship> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Relationship.ObjectBody)other, context);
			}

			// Token: 0x060022D9 RID: 8921 RVA: 0x000DF008 File Offset: 0x000DD208
			internal bool IsEqualTo(Relationship.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.IsActive, other.IsActive) && PropertyHelper.AreValuesIdentical(this.CrossFilteringBehavior, other.CrossFilteringBehavior) && PropertyHelper.AreValuesIdentical(this.JoinOnDateBehavior, other.JoinOnDateBehavior) && PropertyHelper.AreValuesIdentical(this.RelyOnReferentialIntegrity, other.RelyOnReferentialIntegrity) && PropertyHelper.AreValuesIdentical(this.FromCardinality, other.FromCardinality) && PropertyHelper.AreValuesIdentical(this.ToCardinality, other.ToCardinality) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime) && PropertyHelper.AreValuesIdentical(this.SecurityFilteringBehavior, other.SecurityFilteringBehavior) && this.ModelID.IsEqualTo(other.ModelID) && this.FromTableID.IsEqualTo(other.FromTableID) && this.FromColumnID.IsEqualTo(other.FromColumnID) && this.ToTableID.IsEqualTo(other.ToTableID) && this.ToColumnID.IsEqualTo(other.ToColumnID);
			}

			// Token: 0x060022DA RID: 8922 RVA: 0x000DF166 File Offset: 0x000DD366
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Relationship.ObjectBody)other);
			}

			// Token: 0x060022DB RID: 8923 RVA: 0x000DF180 File Offset: 0x000DD380
			internal void CompareWith(Relationship.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsActive, other.IsActive))
				{
					context.RegisterPropertyChange(base.Owner, "IsActive", typeof(bool), PropertyFlags.DdlAndUser, other.IsActive, this.IsActive);
				}
				if (!PropertyHelper.AreValuesIdentical(this.CrossFilteringBehavior, other.CrossFilteringBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "CrossFilteringBehavior", typeof(CrossFilteringBehavior), PropertyFlags.DdlAndUser, other.CrossFilteringBehavior, this.CrossFilteringBehavior);
				}
				if (!PropertyHelper.AreValuesIdentical(this.JoinOnDateBehavior, other.JoinOnDateBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "JoinOnDateBehavior", typeof(DateTimeRelationshipBehavior), PropertyFlags.DdlAndUser, other.JoinOnDateBehavior, this.JoinOnDateBehavior);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RelyOnReferentialIntegrity, other.RelyOnReferentialIntegrity))
				{
					context.RegisterPropertyChange(base.Owner, "RelyOnReferentialIntegrity", typeof(bool), PropertyFlags.DdlAndUser, other.RelyOnReferentialIntegrity, this.RelyOnReferentialIntegrity);
				}
				if (!PropertyHelper.AreValuesIdentical(this.FromCardinality, other.FromCardinality))
				{
					context.RegisterPropertyChange(base.Owner, "FromCardinality", typeof(RelationshipEndCardinality), PropertyFlags.DdlAndUser, other.FromCardinality, this.FromCardinality);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ToCardinality, other.ToCardinality))
				{
					context.RegisterPropertyChange(base.Owner, "ToCardinality", typeof(RelationshipEndCardinality), PropertyFlags.DdlAndUser, other.ToCardinality, this.ToCardinality);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime))
				{
					context.RegisterPropertyChange(base.Owner, "RefreshedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.RefreshedTime, this.RefreshedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SecurityFilteringBehavior, other.SecurityFilteringBehavior))
				{
					context.RegisterPropertyChange(base.Owner, "SecurityFilteringBehavior", typeof(SecurityFilteringBehavior), PropertyFlags.DdlAndUser, other.SecurityFilteringBehavior, this.SecurityFilteringBehavior);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
				this.FromTableID.CompareWith(other.FromTableID, "FromTableID", "FromTable", PropertyFlags.None, context);
				this.FromColumnID.CompareWith(other.FromColumnID, "FromColumnID", "FromColumn", PropertyFlags.None, context);
				this.ToTableID.CompareWith(other.ToTableID, "ToTableID", "ToTable", PropertyFlags.None, context);
				this.ToColumnID.CompareWith(other.ToColumnID, "ToColumnID", "ToColumn", PropertyFlags.None, context);
			}

			// Token: 0x060022DC RID: 8924 RVA: 0x000DF516 File Offset: 0x000DD716
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Relationship.ObjectBody)other, context);
			}

			// Token: 0x04000A0D RID: 2573
			internal string Name;

			// Token: 0x04000A0E RID: 2574
			internal bool IsActive;

			// Token: 0x04000A0F RID: 2575
			internal RelationshipType Type;

			// Token: 0x04000A10 RID: 2576
			internal CrossFilteringBehavior CrossFilteringBehavior;

			// Token: 0x04000A11 RID: 2577
			internal DateTimeRelationshipBehavior JoinOnDateBehavior;

			// Token: 0x04000A12 RID: 2578
			internal bool RelyOnReferentialIntegrity;

			// Token: 0x04000A13 RID: 2579
			internal RelationshipEndCardinality FromCardinality;

			// Token: 0x04000A14 RID: 2580
			internal RelationshipEndCardinality ToCardinality;

			// Token: 0x04000A15 RID: 2581
			internal ObjectState State;

			// Token: 0x04000A16 RID: 2582
			internal DateTime ModifiedTime;

			// Token: 0x04000A17 RID: 2583
			internal DateTime RefreshedTime;

			// Token: 0x04000A18 RID: 2584
			internal SecurityFilteringBehavior SecurityFilteringBehavior;

			// Token: 0x04000A19 RID: 2585
			internal ParentLink<Relationship, Model> ModelID;

			// Token: 0x04000A1A RID: 2586
			internal CrossLink<Relationship, Table> FromTableID;

			// Token: 0x04000A1B RID: 2587
			internal CrossLink<Relationship, Column> FromColumnID;

			// Token: 0x04000A1C RID: 2588
			internal CrossLink<Relationship, Table> ToTableID;

			// Token: 0x04000A1D RID: 2589
			internal CrossLink<Relationship, Column> ToColumnID;
		}
	}
}
