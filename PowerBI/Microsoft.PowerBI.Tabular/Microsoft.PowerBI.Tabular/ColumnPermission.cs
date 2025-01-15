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
	// Token: 0x02000047 RID: 71
	[CompatibilityRequirement("1400")]
	public sealed class ColumnPermission : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00018077 File Offset: 0x00016277
		public ColumnPermission()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001808A File Offset: 0x0001628A
		internal ColumnPermission(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00018099 File Offset: 0x00016299
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new ColumnPermission.ObjectBody(this);
			this.body.MetadataPermission = MetadataPermission.Default;
			this._Annotations = new ColumnPermissionAnnotationCollection(this, comparer);
			this._ExtendedProperties = new ColumnPermissionExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000180CD File Offset: 0x000162CD
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ColumnPermission;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000180D1 File Offset: 0x000162D1
		// (set) Token: 0x06000304 RID: 772 RVA: 0x000180E3 File Offset: 0x000162E3
		public override MetadataObject Parent
		{
			get
			{
				return this.body.TablePermissionID.Object;
			}
			internal set
			{
				if (this.body.TablePermissionID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<ColumnPermission, TablePermission>(this.body.TablePermissionID, (TablePermission)value, null, null);
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00018110 File Offset: 0x00016310
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TablePermissionID.ObjectID;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00018124 File Offset: 0x00016324
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.ColumnPermission, null, "ColumnPermission object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("metadataPermission", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<MetadataPermission>("metadataPermission", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, null);
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
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

		// Token: 0x06000307 RID: 775 RVA: 0x00018228 File Offset: 0x00016428
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.ColumnPermission[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				int num = PropertyHelper.GetMetadataPermissionCompatibilityRestrictions(this.body.MetadataPermission)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MetadataPermission");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000182BB File Offset: 0x000164BB
		// (set) Token: 0x06000309 RID: 777 RVA: 0x000182C3 File Offset: 0x000164C3
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ColumnPermission.ObjectBody)value;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000182D1 File Offset: 0x000164D1
		internal override ITxObjectBody CreateBody()
		{
			return new ColumnPermission.ObjectBody(this);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000182D9 File Offset: 0x000164D9
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ColumnPermission();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000182E0 File Offset: 0x000164E0
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((TablePermission)parent).ColumnPermissions;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000182F0 File Offset: 0x000164F0
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			TablePermission tablePermission = MetadataObject.ResolveMetadataObjectParentById<ColumnPermission, TablePermission>(this.body.TablePermissionID, objectMap, throwIfCantResolve, null, null);
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
			if (tablePermission != null)
			{
				tablePermission.ColumnPermissions.Add(this);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00018334 File Offset: 0x00016534
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.ColumnID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001834C File Offset: 0x0001654C
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

		// Token: 0x06000310 RID: 784 RVA: 0x000183A0 File Offset: 0x000165A0
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.ColumnID.TryResolveAfterCopy(copyContext) && this.body.ColumnID.Path != null && !this.body.ColumnID.Path.IsEmpty)
			{
				this.body._name = this.body.ColumnID.Path[this.body.ColumnID.Path.Count - 1].Value;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00018428 File Offset: 0x00016628
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.ColumnID.Validate(result, throwOnError);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001843C File Offset: 0x0001663C
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.ColumnID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00018458 File Offset: 0x00016658
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00018468 File Offset: 0x00016668
		public ColumnPermissionAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00018470 File Offset: 0x00016670
		public ColumnPermissionExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00018478 File Offset: 0x00016678
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00018499 File Offset: 0x00016699
		public override string Name
		{
			get
			{
				if (this.Column != null)
				{
					return this.Column.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Column != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000184BA File Offset: 0x000166BA
		// (set) Token: 0x06000319 RID: 793 RVA: 0x000184C8 File Offset: 0x000166C8
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0001854C File Offset: 0x0001674C
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0001855C File Offset: 0x0001675C
		public MetadataPermission MetadataPermission
		{
			get
			{
				return this.body.MetadataPermission;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MetadataPermission, value))
				{
					CompatibilityRestrictionSet metadataPermissionCompatibilityRestrictions = PropertyHelper.GetMetadataPermissionCompatibilityRestrictions(value);
					CompatibilityRestrictionSet metadataPermissionCompatibilityRestrictions2 = PropertyHelper.GetMetadataPermissionCompatibilityRestrictions(this.body.MetadataPermission);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = metadataPermissionCompatibilityRestrictions.Compare(metadataPermissionCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != MetadataPermission.Default))
					{
						array = base.ValidateCompatibilityRequirement(metadataPermissionCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "MetadataPermission", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MetadataPermission", typeof(MetadataPermission), this.body.MetadataPermission, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(metadataPermissionCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(metadataPermissionCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(metadataPermissionCompatibilityRestrictions, array);
						break;
					}
					MetadataPermission metadataPermission = this.body.MetadataPermission;
					this.body.MetadataPermission = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MetadataPermission", typeof(MetadataPermission), metadataPermission, value);
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0001867D File Offset: 0x0001687D
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00018690 File Offset: 0x00016890
		public TablePermission TablePermission
		{
			get
			{
				return this.body.TablePermissionID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TablePermissionID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TablePermission", typeof(TablePermission), this.body.TablePermissionID.Object, value);
					TablePermission @object = this.body.TablePermissionID.Object;
					this.body.TablePermissionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TablePermission", typeof(TablePermission), @object, value);
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00018714 File Offset: 0x00016914
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00018726 File Offset: 0x00016926
		internal ObjectId _TablePermissionID
		{
			get
			{
				return this.body.TablePermissionID.ObjectID;
			}
			set
			{
				this.body.TablePermissionID.ObjectID = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00018739 File Offset: 0x00016939
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0001874C File Offset: 0x0001694C
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000187D0 File Offset: 0x000169D0
		// (set) Token: 0x06000323 RID: 803 RVA: 0x000187E2 File Offset: 0x000169E2
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

		// Token: 0x06000324 RID: 804 RVA: 0x000187F8 File Offset: 0x000169F8
		internal void CopyFrom(ColumnPermission other, CopyContext context)
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
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000188BA File Offset: 0x00016ABA
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ColumnPermission)other, context);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000188C9 File Offset: 0x00016AC9
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ColumnPermission other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000188E5 File Offset: 0x00016AE5
		public void CopyTo(ColumnPermission other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00018901 File Offset: 0x00016B01
		public ColumnPermission Clone()
		{
			return base.CloneInternal<ColumnPermission>();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001890C File Offset: 0x00016B0C
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ColumnPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TablePermissionID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TablePermissionID", this.body.TablePermissionID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "ColumnID", this.body.ColumnID.Object);
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<MetadataPermission>(options, "MetadataPermission", this.body.MetadataPermission);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00018A24 File Offset: 0x00016C24
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TablePermissionID", out objectId))
			{
				this.body.TablePermissionID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("ColumnID", out objectId2))
			{
				this.body.ColumnID.ObjectID = objectId2;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			MetadataPermission metadataPermission;
			if (reader.TryReadProperty<MetadataPermission>("MetadataPermission", out metadataPermission))
			{
				this.body.MetadataPermission = metadataPermission;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00018AB0 File Offset: 0x00016CB0
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ColumnPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TablePermissionID.Object != null && writer.ShouldIncludeProperty("TablePermissionID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TablePermissionID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TablePermissionID.Object);
			}
			this.body.ColumnID.Validate(null, true);
			if (this.body.ColumnID.Object != null && writer.ShouldIncludeProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("ColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.ColumnID.Object);
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MetadataPermission", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<MetadataPermission>("MetadataPermission", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, this.body.MetadataPermission);
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00018C24 File Offset: 0x00016E24
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ColumnPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty, this.Name);
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("metadataPermission", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<MetadataPermission>("metadataPermission", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.DefaultProperty, this.body.MetadataPermission);
				}
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
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

		// Token: 0x0600032D RID: 813 RVA: 0x00018E18 File Offset: 0x00017018
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				if (length <= 12)
				{
					if (length != 4)
					{
						switch (length)
						{
						case 8:
							if (propertyName == "ColumnID")
							{
								this.body.ColumnID.ObjectID = reader.ReadObjectIdProperty();
								return true;
							}
							break;
						case 11:
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
							break;
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
						}
					}
					else if (propertyName == "name")
					{
						this.Name = reader.ReadStringProperty();
						return true;
					}
				}
				else if (length != 17)
				{
					if (length == 18)
					{
						char c = propertyName[0];
						if (c != 'M')
						{
							if (c != 'e')
							{
								if (c != 'm')
								{
									goto IL_02F9;
								}
								if (!(propertyName == "metadataPermission"))
								{
									goto IL_02F9;
								}
							}
							else
							{
								if (!(propertyName == "extendedProperties"))
								{
									goto IL_02F9;
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
										catch (Exception ex2)
										{
											throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
										}
									}
								}
								return true;
							}
						}
						else if (!(propertyName == "MetadataPermission"))
						{
							goto IL_02F9;
						}
						if (!CompatibilityRestrictions.MetadataPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.MetadataPermission = reader.ReadEnumProperty<MetadataPermission>();
						return true;
					}
				}
				else if (propertyName == "TablePermissionID")
				{
					this.body.TablePermissionID.ObjectID = reader.ReadObjectIdProperty();
					return true;
				}
			}
			IL_02F9:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00019170 File Offset: 0x00017370
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001918C File Offset: 0x0001738C
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object ColumnPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				result["name", TomPropCategory.Name, 0, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 4, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["metadataPermission", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertEnumToJsonValue<MetadataPermission>(this.body.MetadataPermission);
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

		// Token: 0x06000330 RID: 816 RVA: 0x00019448 File Offset: 0x00017648
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "metadataPermission"))
			{
				if (!(name == "extendedProperties"))
				{
					if (!(name == "annotations"))
					{
						return false;
					}
					JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
					return true;
				}
				else
				{
					if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
					{
						return false;
					}
					JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
					return true;
				}
			}
			else
			{
				MetadataPermission metadataPermission = JsonPropertyHelper.ConvertJsonValueToEnum<MetadataPermission>(jsonProp.Value);
				if (jsonProp.Value.Type != 10 && !PropertyHelper.IsMetadataPermissionValueCompatible(metadataPermission, mode, dbCompatibilityLevel))
				{
					return false;
				}
				this.body.MetadataPermission = metadataPermission;
				return true;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00019554 File Offset: 0x00017754
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.ColumnID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name) || this.TablePermission == null || string.IsNullOrEmpty(this.TablePermission.Name))
			{
				return false;
			}
			if (this.body.ColumnID.Path == null || this.body.ColumnID.Path.IsEmpty)
			{
				this.body.ColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
				{
					new KeyValuePair<ObjectType, string>(ObjectType.Table, this.TablePermission.Name),
					new KeyValuePair<ObjectType, string>(ObjectType.Column, this.Name)
				});
			}
			return true;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00019613 File Offset: 0x00017813
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.ColumnID.ObjectID;
			objectPath = this.body.ColumnID.Path;
			@object = this.body.ColumnID.Object;
			property = null;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00019653 File Offset: 0x00017853
		internal override string GetFormattedObjectPath()
		{
			if (this.TablePermission != null)
			{
				return TomSR.ObjectPath_ColumnPermission_2Args(this.Name, this.TablePermission.Name);
			}
			return TomSR.ObjectPath_ColumnPermission_1Arg(this.Name);
		}

		// Token: 0x040000E7 RID: 231
		internal ColumnPermission.ObjectBody body;

		// Token: 0x040000E8 RID: 232
		private ColumnPermissionAnnotationCollection _Annotations;

		// Token: 0x040000E9 RID: 233
		private ColumnPermissionExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0200024C RID: 588
		internal class ObjectBody : NamedMetadataObjectBody<ColumnPermission>
		{
			// Token: 0x06001F99 RID: 8089 RVA: 0x000D1160 File Offset: 0x000CF360
			public ObjectBody(ColumnPermission owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.TablePermissionID = new ParentLink<ColumnPermission, TablePermission>(owner, "TablePermission");
				this.ColumnID = new CrossLink<ColumnPermission, Column>(owner, "Column");
			}

			// Token: 0x06001F9A RID: 8090 RVA: 0x000D1196 File Offset: 0x000CF396
			public override string GetObjectName()
			{
				if (this.ColumnID.Object == null)
				{
					return this._name;
				}
				return this.ColumnID.Object.Name;
			}

			// Token: 0x06001F9B RID: 8091 RVA: 0x000D11BC File Offset: 0x000CF3BC
			internal bool IsEqualTo(ColumnPermission.ObjectBody other, CopyContext context)
			{
				return ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.MetadataPermission, other.MetadataPermission) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TablePermissionID.IsEqualTo(other.TablePermissionID, context)) && this.ColumnID.IsEqualTo(other.ColumnID, context);
			}

			// Token: 0x06001F9C RID: 8092 RVA: 0x000D1248 File Offset: 0x000CF448
			internal void CopyFromImpl(ColumnPermission.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				this.MetadataPermission = other.MetadataPermission;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TablePermissionID.CopyFrom(other.TablePermissionID, context);
				}
				this.ColumnID.CopyFrom(other.ColumnID, context);
				this._name = other._name;
			}

			// Token: 0x06001F9D RID: 8093 RVA: 0x000D12D8 File Offset: 0x000CF4D8
			internal void CopyFromImpl(ColumnPermission.ObjectBody other)
			{
				this.ModifiedTime = other.ModifiedTime;
				this.MetadataPermission = other.MetadataPermission;
				this.TablePermissionID.CopyFrom(other.TablePermissionID, ObjectChangeTracker.BodyCloneContext);
				this.ColumnID.CopyFrom(other.ColumnID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x06001F9E RID: 8094 RVA: 0x000D1335 File Offset: 0x000CF535
			public override void CopyFrom(MetadataObjectBody<ColumnPermission> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ColumnPermission.ObjectBody)other, context);
			}

			// Token: 0x06001F9F RID: 8095 RVA: 0x000D134C File Offset: 0x000CF54C
			internal bool IsEqualTo(ColumnPermission.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.MetadataPermission, other.MetadataPermission) && this.TablePermissionID.IsEqualTo(other.TablePermissionID) && this.ColumnID.IsEqualTo(other.ColumnID);
			}

			// Token: 0x06001FA0 RID: 8096 RVA: 0x000D13AE File Offset: 0x000CF5AE
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ColumnPermission.ObjectBody)other);
			}

			// Token: 0x06001FA1 RID: 8097 RVA: 0x000D13C8 File Offset: 0x000CF5C8
			internal void CompareWith(ColumnPermission.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MetadataPermission, other.MetadataPermission))
				{
					context.RegisterPropertyChange(base.Owner, "MetadataPermission", typeof(MetadataPermission), PropertyFlags.DdlAndUser, other.MetadataPermission, this.MetadataPermission);
				}
				this.TablePermissionID.CompareWith(other.TablePermissionID, "TablePermissionID", "TablePermission", PropertyFlags.ReadOnly, context);
				this.ColumnID.CompareWith(other.ColumnID, "ColumnID", "Column", PropertyFlags.None, context);
			}

			// Token: 0x06001FA2 RID: 8098 RVA: 0x000D1499 File Offset: 0x000CF699
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ColumnPermission.ObjectBody)other, context);
			}

			// Token: 0x040007CE RID: 1998
			internal DateTime ModifiedTime;

			// Token: 0x040007CF RID: 1999
			internal MetadataPermission MetadataPermission;

			// Token: 0x040007D0 RID: 2000
			internal ParentLink<ColumnPermission, TablePermission> TablePermissionID;

			// Token: 0x040007D1 RID: 2001
			internal CrossLink<ColumnPermission, Column> ColumnID;

			// Token: 0x040007D2 RID: 2002
			internal string _name;
		}
	}
}
