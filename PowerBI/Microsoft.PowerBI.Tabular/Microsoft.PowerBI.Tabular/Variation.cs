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
	// Token: 0x020000CA RID: 202
	[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
	public sealed class Variation : NamedMetadataObject
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0006A422 File Offset: 0x00068622
		public Variation()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0006A435 File Offset: 0x00068635
		internal Variation(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0006A444 File Offset: 0x00068644
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Variation.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.IsDefault = false;
			this._Annotations = new VariationAnnotationCollection(this, comparer);
			this._ExtendedProperties = new VariationExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0006A4A3 File Offset: 0x000686A3
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Variation;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0006A4A7 File Offset: 0x000686A7
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0006A4B9 File Offset: 0x000686B9
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ColumnID.Object;
			}
			internal set
			{
				if (this.body.ColumnID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Variation, Column>(this.body.ColumnID, (Column)value, null, null);
				}
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0006A4E6 File Offset: 0x000686E6
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ColumnID.ObjectID;
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0006A4F8 File Offset: 0x000686F8
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Variation, null, "Variation object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("isDefault", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("isDefault", MetadataPropertyNature.RegularProperty, typeof(bool));
				}
				if (writer.ShouldIncludeProperty("relationship", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<Variation, Relationship>.WriteMetadataSchema(ObjectType.Relationship, ObjectType.Relationship, true, "relationship", false, writer);
				}
				if (writer.ShouldIncludeProperty("defaultHierarchy", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<Variation, Hierarchy>.WriteMetadataSchema(ObjectType.Hierarchy, ObjectType.Table, true, "defaultHierarchy", false, writer);
				}
				if (writer.ShouldIncludeProperty("defaultColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<Variation, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, true, "defaultColumn", false, writer);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0006A668 File Offset: 0x00068868
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.Variation[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0006A69F File Offset: 0x0006889F
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0006A6A7 File Offset: 0x000688A7
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Variation.ObjectBody)value;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0006A6B5 File Offset: 0x000688B5
		internal override ITxObjectBody CreateBody()
		{
			return new Variation.ObjectBody(this);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0006A6BD File Offset: 0x000688BD
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Variation();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0006A6C4 File Offset: 0x000688C4
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Column)parent).Variations;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0006A6D4 File Offset: 0x000688D4
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Column column = MetadataObject.ResolveMetadataObjectParentById<Variation, Column>(this.body.ColumnID, objectMap, throwIfCantResolve, null, null);
			this.body.RelationshipID.ResolveById(objectMap, throwIfCantResolve);
			this.body.DefaultHierarchyID.ResolveById(objectMap, throwIfCantResolve);
			this.body.DefaultColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (column != null)
			{
				column.Variations.Add(this);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0006A73E File Offset: 0x0006893E
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.RelationshipID.ResolveById(objectMap, throwIfCantResolve);
			this.body.DefaultHierarchyID.ResolveById(objectMap, throwIfCantResolve);
			this.body.DefaultColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0006A77C File Offset: 0x0006897C
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.RelationshipID.IsResolved && !this.body.RelationshipID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Relationship"));
				}
				flag = false;
			}
			if (!this.body.DefaultHierarchyID.IsResolved && !this.body.DefaultHierarchyID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultHierarchy"));
				}
				flag = false;
			}
			if (!this.body.DefaultColumnID.IsResolved && !this.body.DefaultColumnID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DefaultColumn"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0006A858 File Offset: 0x00068A58
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.RelationshipID.TryResolveAfterCopy(copyContext);
			this.body.DefaultHierarchyID.TryResolveAfterCopy(copyContext);
			this.body.DefaultColumnID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0006A890 File Offset: 0x00068A90
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.RelationshipID.Validate(result, throwOnError);
			this.body.DefaultHierarchyID.Validate(result, throwOnError);
			this.body.DefaultColumnID.Validate(result, throwOnError);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0006A8C8 File Offset: 0x00068AC8
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.RelationshipID.IsResolved || !this.body.DefaultHierarchyID.IsResolved || !this.body.DefaultColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0006A917 File Offset: 0x00068B17
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0006A927 File Offset: 0x00068B27
		public VariationAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0006A92F File Offset: 0x00068B2F
		public VariationExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0006A937 File Offset: 0x00068B37
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0006A944 File Offset: 0x00068B44
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Variation, out text))
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0006A9C7 File Offset: 0x00068BC7
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0006A9D4 File Offset: 0x00068BD4
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

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0006AA44 File Offset: 0x00068C44
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0006AA54 File Offset: 0x00068C54
		public bool IsDefault
		{
			get
			{
				return this.body.IsDefault;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IsDefault, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IsDefault", typeof(bool), this.body.IsDefault, value);
					bool isDefault = this.body.IsDefault;
					this.body.IsDefault = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IsDefault", typeof(bool), isDefault, value);
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0006AAD8 File Offset: 0x00068CD8
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0006AAEC File Offset: 0x00068CEC
		public Column Column
		{
			get
			{
				return this.body.ColumnID.Object;
			}
			internal set
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

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0006AB70 File Offset: 0x00068D70
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0006AB82 File Offset: 0x00068D82
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

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0006AB95 File Offset: 0x00068D95
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0006ABA8 File Offset: 0x00068DA8
		public Relationship Relationship
		{
			get
			{
				return this.body.RelationshipID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RelationshipID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Relationship", typeof(Relationship), this.body.RelationshipID.Object, value);
					Relationship @object = this.body.RelationshipID.Object;
					this.body.RelationshipID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Relationship", typeof(Relationship), @object, value);
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0006AC2C File Offset: 0x00068E2C
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0006AC3E File Offset: 0x00068E3E
		internal ObjectId _RelationshipID
		{
			get
			{
				return this.body.RelationshipID.ObjectID;
			}
			set
			{
				this.body.RelationshipID.ObjectID = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0006AC51 File Offset: 0x00068E51
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0006AC64 File Offset: 0x00068E64
		public Hierarchy DefaultHierarchy
		{
			get
			{
				return this.body.DefaultHierarchyID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultHierarchyID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultHierarchy", typeof(Hierarchy), this.body.DefaultHierarchyID.Object, value);
					Hierarchy @object = this.body.DefaultHierarchyID.Object;
					this.body.DefaultHierarchyID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultHierarchy", typeof(Hierarchy), @object, value);
				}
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0006ACE8 File Offset: 0x00068EE8
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0006ACFA File Offset: 0x00068EFA
		internal ObjectId _DefaultHierarchyID
		{
			get
			{
				return this.body.DefaultHierarchyID.ObjectID;
			}
			set
			{
				this.body.DefaultHierarchyID.ObjectID = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0006AD0D File Offset: 0x00068F0D
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0006AD20 File Offset: 0x00068F20
		public Column DefaultColumn
		{
			get
			{
				return this.body.DefaultColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DefaultColumnID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DefaultColumn", typeof(Column), this.body.DefaultColumnID.Object, value);
					Column @object = this.body.DefaultColumnID.Object;
					this.body.DefaultColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DefaultColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0006ADA4 File Offset: 0x00068FA4
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x0006ADB6 File Offset: 0x00068FB6
		internal ObjectId _DefaultColumnID
		{
			get
			{
				return this.body.DefaultColumnID.ObjectID;
			}
			set
			{
				this.body.DefaultColumnID.ObjectID = value;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0006ADCC File Offset: 0x00068FCC
		internal void CopyFrom(Variation other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0006AE51 File Offset: 0x00069051
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Variation)other, context);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0006AE60 File Offset: 0x00069060
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Variation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0006AE7C File Offset: 0x0006907C
		public void CopyTo(Variation other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0006AE98 File Offset: 0x00069098
		public Variation Clone()
		{
			return base.CloneInternal<Variation>();
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0006AEA0 File Offset: 0x000690A0
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Variation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
			this.body.RelationshipID.Validate(null, true);
			if (this.body.RelationshipID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "RelationshipID", this.body.RelationshipID.Object);
			}
			this.body.DefaultHierarchyID.Validate(null, true);
			if (this.body.DefaultHierarchyID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "DefaultHierarchyID", this.body.DefaultHierarchyID.Object);
			}
			this.body.DefaultColumnID.Validate(null, true);
			if (this.body.DefaultColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "DefaultColumnID", this.body.DefaultColumnID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (this.body.IsDefault)
			{
				writer.WriteProperty<bool>(options, "IsDefault", this.body.IsDefault);
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0006B050 File Offset: 0x00069250
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId))
			{
				this.body.ColumnID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("RelationshipID", out objectId2))
			{
				this.body.RelationshipID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (reader.TryReadProperty<ObjectId>("DefaultHierarchyID", out objectId3))
			{
				this.body.DefaultHierarchyID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (reader.TryReadProperty<ObjectId>("DefaultColumnID", out objectId4))
			{
				this.body.DefaultColumnID.ObjectID = objectId4;
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
			if (reader.TryReadProperty<bool>("IsDefault", out flag))
			{
				this.body.IsDefault = flag;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0006B13C File Offset: 0x0006933C
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Variation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Variation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.ColumnID.Object);
			}
			this.body.RelationshipID.Validate(null, true);
			if (this.body.RelationshipID.Object != null && writer.ShouldIncludeProperty("RelationshipID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("RelationshipID", MetadataPropertyNature.CrossLinkProperty, this.body.RelationshipID.Object);
			}
			this.body.DefaultHierarchyID.Validate(null, true);
			if (this.body.DefaultHierarchyID.Object != null && writer.ShouldIncludeProperty("DefaultHierarchyID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("DefaultHierarchyID", MetadataPropertyNature.CrossLinkProperty, this.body.DefaultHierarchyID.Object);
			}
			this.body.DefaultColumnID.Validate(null, true);
			if (this.body.DefaultColumnID.Object != null && writer.ShouldIncludeProperty("DefaultColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("DefaultColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.DefaultColumnID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.IsDefault && writer.ShouldIncludeProperty("IsDefault", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("IsDefault", MetadataPropertyNature.RegularProperty, this.body.IsDefault);
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0006B374 File Offset: 0x00069574
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Variation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Variation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.IsDefault && writer.ShouldIncludeProperty("isDefault", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteBooleanProperty("isDefault", MetadataPropertyNature.RegularProperty, this.body.IsDefault);
			}
			if (this.body.RelationshipID.Object != null && writer.ShouldIncludeProperty("relationship", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.RelationshipID.WriteToMetadataStream(ObjectType.Relationship, true, "relationship", false, writer);
			}
			if (this.body.DefaultHierarchyID.Object != null && writer.ShouldIncludeProperty("defaultHierarchy", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.DefaultHierarchyID.WriteToMetadataStream(ObjectType.Table, true, "defaultHierarchy", false, writer);
			}
			if (this.body.DefaultColumnID.Object != null && writer.ShouldIncludeProperty("defaultColumn", MetadataPropertyNature.CrossLinkProperty))
			{
				this.body.DefaultColumnID.WriteToMetadataStream(ObjectType.Table, true, "defaultColumn", false, writer);
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
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0006B5CC File Offset: 0x000697CC
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
				case 8:
					if (propertyName == "ColumnID")
					{
						this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 9:
				{
					char c = propertyName[0];
					if (c != 'I')
					{
						if (c != 'i')
						{
							break;
						}
						if (!(propertyName == "isDefault"))
						{
							break;
						}
					}
					else if (!(propertyName == "IsDefault"))
					{
						break;
					}
					this.body.IsDefault = reader.ReadBooleanProperty();
					return true;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'a')
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
					}
					else if (!(propertyName == "Description"))
					{
						break;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
				}
				case 12:
					if (propertyName == "relationship")
					{
						this.body.RelationshipID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.Relationship, p));
						return true;
					}
					break;
				case 13:
					if (propertyName == "defaultColumn")
					{
						this.body.DefaultColumnID.Path = reader.ReadCrossLinkProperty();
						return true;
					}
					break;
				case 14:
					if (propertyName == "RelationshipID")
					{
						this.body.RelationshipID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 15:
					if (propertyName == "DefaultColumnID")
					{
						this.body.DefaultColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 16:
					if (propertyName == "defaultHierarchy")
					{
						this.body.DefaultHierarchyID.Path = reader.ReadCrossLinkProperty();
						return true;
					}
					break;
				case 18:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'd')
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
											catch (Exception ex2)
											{
												throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
											}
										}
									}
									return true;
								}
							}
						}
						else if (propertyName == "defaultColumnTable")
						{
							if (this.body.DefaultColumnID.Path == null)
							{
								this.body.DefaultColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
							}
							this.body.DefaultColumnID.Path.Push(ObjectType.Table, reader.ReadStringProperty());
							return true;
						}
					}
					else if (propertyName == "DefaultHierarchyID")
					{
						this.body.DefaultHierarchyID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 19:
					if (propertyName == "defaultColumnColumn")
					{
						if (this.body.DefaultColumnID.Path == null)
						{
							this.body.DefaultColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
						}
						this.body.DefaultColumnID.Path.Push(ObjectType.Column, reader.ReadStringProperty());
						return true;
					}
					break;
				case 21:
					if (propertyName == "defaultHierarchyTable")
					{
						if (this.body.DefaultHierarchyID.Path == null)
						{
							this.body.DefaultHierarchyID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
						}
						this.body.DefaultHierarchyID.Path.Push(ObjectType.Table, reader.ReadStringProperty());
						return true;
					}
					break;
				case 25:
					if (propertyName == "defaultHierarchyHierarchy")
					{
						if (this.body.DefaultHierarchyID.Path == null)
						{
							this.body.DefaultHierarchyID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
						}
						this.body.DefaultHierarchyID.Path.Push(ObjectType.Hierarchy, reader.ReadStringProperty());
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0006BC10 File Offset: 0x00069E10
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0006BC19 File Offset: 0x00069E19
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0006BC3C File Offset: 0x00069E3C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Variation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.IsDefault)
			{
				result["isDefault", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<bool>(this.body.IsDefault);
			}
			if (!options.IncludeTranslatablePropertiesOnly)
			{
				if (this.body.RelationshipID.Object != null)
				{
					this.body.RelationshipID.SerializeToJsonObject(true, "relationship", ObjectType.Relationship, result, 4, false);
				}
				if (this.body.DefaultHierarchyID.Object != null)
				{
					this.body.DefaultHierarchyID.SerializeToJsonObject(true, "defaultHierarchy", ObjectType.Table, result, 5, false);
				}
				if (this.body.DefaultColumnID.Object != null)
				{
					this.body.DefaultColumnID.SerializeToJsonObject(true, "defaultColumn", ObjectType.Table, result, 6, false);
				}
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
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
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0006BF30 File Offset: 0x0006A130
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				if (length != 4)
				{
					switch (length)
					{
					case 9:
						if (name == "isDefault")
						{
							this.body.IsDefault = JsonPropertyHelper.ConvertJsonValueToPrimitive<bool>(jsonProp.Value);
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
						if (name == "relationship")
						{
							this.body.RelationshipID.Path = new ObjectPath(ObjectType.Relationship, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
						break;
					case 13:
						if (name == "defaultColumn")
						{
							this.body.DefaultColumnID.Path = ObjectPath.Parse((JObject)jsonProp.Value);
							return true;
						}
						break;
					case 16:
						if (name == "defaultHierarchy")
						{
							this.body.DefaultHierarchyID.Path = ObjectPath.Parse((JObject)jsonProp.Value);
							return true;
						}
						break;
					case 18:
					{
						char c = name[0];
						if (c != 'd')
						{
							if (c == 'e')
							{
								if (name == "extendedProperties")
								{
									if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
									{
										return false;
									}
									JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
									return true;
								}
							}
						}
						else if (name == "defaultColumnTable")
						{
							if (this.body.DefaultColumnID.Path == null)
							{
								this.body.DefaultColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
							}
							this.body.DefaultColumnID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
						break;
					}
					case 19:
						if (name == "defaultColumnColumn")
						{
							if (this.body.DefaultColumnID.Path == null)
							{
								this.body.DefaultColumnID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
							}
							this.body.DefaultColumnID.Path.Push(ObjectType.Column, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
						break;
					case 21:
						if (name == "defaultHierarchyTable")
						{
							if (this.body.DefaultHierarchyID.Path == null)
							{
								this.body.DefaultHierarchyID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
							}
							this.body.DefaultHierarchyID.Path.Push(ObjectType.Table, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
						break;
					case 25:
						if (name == "defaultHierarchyHierarchy")
						{
							if (this.body.DefaultHierarchyID.Path == null)
							{
								this.body.DefaultHierarchyID.Path = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
							}
							this.body.DefaultHierarchyID.Path.Push(ObjectType.Hierarchy, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
						break;
					}
				}
				else if (name == "name")
				{
					this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0006C314 File Offset: 0x0006A514
		internal override string GetFormattedObjectPath()
		{
			if (this.Column != null && this.Column.Table != null)
			{
				return TomSR.ObjectPath_Variation_3Args(this.Name, this.Column.Name, this.Column.Table.Name);
			}
			if (this.Column != null)
			{
				return TomSR.ObjectPath_Variation_2Args(this.Name, this.Column.Name);
			}
			return TomSR.ObjectPath_Variation_1Arg(this.Name);
		}

		// Token: 0x0400018D RID: 397
		internal Variation.ObjectBody body;

		// Token: 0x0400018E RID: 398
		private VariationAnnotationCollection _Annotations;

		// Token: 0x0400018F RID: 399
		private VariationExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x020002E3 RID: 739
		internal class ObjectBody : NamedMetadataObjectBody<Variation>
		{
			// Token: 0x06002395 RID: 9109 RVA: 0x000E2348 File Offset: 0x000E0548
			public ObjectBody(Variation owner)
				: base(owner)
			{
				this.ColumnID = new ParentLink<Variation, Column>(owner, "Column");
				this.RelationshipID = new CrossLink<Variation, Relationship>(owner, "Relationship");
				this.DefaultHierarchyID = new CrossLink<Variation, Hierarchy>(owner, "DefaultHierarchy");
				this.DefaultColumnID = new CrossLink<Variation, Column>(owner, "DefaultColumn");
			}

			// Token: 0x06002396 RID: 9110 RVA: 0x000E23A0 File Offset: 0x000E05A0
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06002397 RID: 9111 RVA: 0x000E23A8 File Offset: 0x000E05A8
			internal bool IsEqualTo(Variation.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsDefault, other.IsDefault) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ColumnID.IsEqualTo(other.ColumnID, context)) && this.RelationshipID.IsEqualTo(other.RelationshipID, context) && this.DefaultHierarchyID.IsEqualTo(other.DefaultHierarchyID, context) && this.DefaultColumnID.IsEqualTo(other.DefaultColumnID, context);
			}

			// Token: 0x06002398 RID: 9112 RVA: 0x000E2460 File Offset: 0x000E0660
			internal void CopyFromImpl(Variation.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.IsDefault = other.IsDefault;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ColumnID.CopyFrom(other.ColumnID, context);
				}
				this.RelationshipID.CopyFrom(other.RelationshipID, context);
				this.DefaultHierarchyID.CopyFrom(other.DefaultHierarchyID, context);
				this.DefaultColumnID.CopyFrom(other.DefaultColumnID, context);
			}

			// Token: 0x06002399 RID: 9113 RVA: 0x000E2504 File Offset: 0x000E0704
			internal void CopyFromImpl(Variation.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.IsDefault = other.IsDefault;
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
				this.RelationshipID.CopyFrom(other.RelationshipID, ObjectChangeTracker.BodyCloneContext);
				this.DefaultHierarchyID.CopyFrom(other.DefaultHierarchyID, ObjectChangeTracker.BodyCloneContext);
				this.DefaultColumnID.CopyFrom(other.DefaultColumnID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600239A RID: 9114 RVA: 0x000E258D File Offset: 0x000E078D
			public override void CopyFrom(MetadataObjectBody<Variation> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Variation.ObjectBody)other, context);
			}

			// Token: 0x0600239B RID: 9115 RVA: 0x000E25A4 File Offset: 0x000E07A4
			internal bool IsEqualTo(Variation.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.IsDefault, other.IsDefault) && this.ColumnID.IsEqualTo(other.ColumnID) && this.RelationshipID.IsEqualTo(other.RelationshipID) && this.DefaultHierarchyID.IsEqualTo(other.DefaultHierarchyID) && this.DefaultColumnID.IsEqualTo(other.DefaultColumnID);
			}

			// Token: 0x0600239C RID: 9116 RVA: 0x000E2645 File Offset: 0x000E0845
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Variation.ObjectBody)other);
			}

			// Token: 0x0600239D RID: 9117 RVA: 0x000E2660 File Offset: 0x000E0860
			internal void CompareWith(Variation.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IsDefault, other.IsDefault))
				{
					context.RegisterPropertyChange(base.Owner, "IsDefault", typeof(bool), PropertyFlags.DdlAndUser, other.IsDefault, this.IsDefault);
				}
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.ReadOnly, context);
				this.RelationshipID.CompareWith(other.RelationshipID, "RelationshipID", "Relationship", PropertyFlags.None, context);
				this.DefaultHierarchyID.CompareWith(other.DefaultHierarchyID, "DefaultHierarchyID", "DefaultHierarchy", PropertyFlags.None, context);
				this.DefaultColumnID.CompareWith(other.DefaultColumnID, "DefaultColumnID", "DefaultColumn", PropertyFlags.None, context);
			}

			// Token: 0x0600239E RID: 9118 RVA: 0x000E27A7 File Offset: 0x000E09A7
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Variation.ObjectBody)other, context);
			}

			// Token: 0x04000AA5 RID: 2725
			internal string Name;

			// Token: 0x04000AA6 RID: 2726
			internal string Description;

			// Token: 0x04000AA7 RID: 2727
			internal bool IsDefault;

			// Token: 0x04000AA8 RID: 2728
			internal ParentLink<Variation, Column> ColumnID;

			// Token: 0x04000AA9 RID: 2729
			internal CrossLink<Variation, Relationship> RelationshipID;

			// Token: 0x04000AAA RID: 2730
			internal CrossLink<Variation, Hierarchy> DefaultHierarchyID;

			// Token: 0x04000AAB RID: 2731
			internal CrossLink<Variation, Column> DefaultColumnID;
		}
	}
}
