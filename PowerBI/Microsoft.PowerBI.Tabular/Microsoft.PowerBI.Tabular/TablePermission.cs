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
	// Token: 0x020000C4 RID: 196
	public sealed class TablePermission : NamedMetadataObject, ILinkedMetadataObject
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x000671CF File Offset: 0x000653CF
		public TablePermission()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000671E2 File Offset: 0x000653E2
		internal TablePermission(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000671F4 File Offset: 0x000653F4
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new TablePermission.ObjectBody(this);
			this.body.FilterExpression = string.Empty;
			this.body.State = ObjectState.Ready;
			this.body.ErrorMessage = string.Empty;
			this.body.MetadataPermission = MetadataPermission.Default;
			this._Annotations = new TablePermissionAnnotationCollection(this, comparer);
			this._ExtendedProperties = new TablePermissionExtendedPropertyCollection(this, comparer);
			this._ColumnPermissions = new ColumnPermissionCollection(this, comparer);
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0006726C File Offset: 0x0006546C
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.TablePermission;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00067270 File Offset: 0x00065470
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00067282 File Offset: 0x00065482
		public override MetadataObject Parent
		{
			get
			{
				return this.body.RoleID.Object;
			}
			internal set
			{
				if (this.body.RoleID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<TablePermission, ModelRole>(this.body.RoleID, (ModelRole)value, null, null);
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000672AF File Offset: 0x000654AF
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.RoleID.ObjectID;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000672C4 File Offset: 0x000654C4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.TablePermission, null, "TablePermission object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("filterExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("filterExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
				if (CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("metadataPermission", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<MetadataPermission>("metadataPermission", MetadataPropertyNature.RegularProperty, null);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (CompatibilityRestrictions.ColumnPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("columnPermissions", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "columnPermissions", MetadataPropertyNature.ChildCollection, ObjectType.ColumnPermission);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000674A4 File Offset: 0x000656A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				int num;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.TablePermission_MetadataPermission[mode], PropertyHelper.GetMetadataPermissionCompatibilityRestrictions(this.body.MetadataPermission)[mode], out num);
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MetadataPermission");
					requiredLevel = num;
					int num2 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00067523 File Offset: 0x00065723
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0006752B File Offset: 0x0006572B
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (TablePermission.ObjectBody)value;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00067539 File Offset: 0x00065739
		internal override ITxObjectBody CreateBody()
		{
			return new TablePermission.ObjectBody(this);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00067541 File Offset: 0x00065741
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new TablePermission();
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00067548 File Offset: 0x00065748
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((ModelRole)parent).TablePermissions;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00067558 File Offset: 0x00065758
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			ModelRole modelRole = MetadataObject.ResolveMetadataObjectParentById<TablePermission, ModelRole>(this.body.RoleID, objectMap, throwIfCantResolve, null, null);
			this.body.TableID.ResolveById(objectMap, throwIfCantResolve);
			if (modelRole != null)
			{
				modelRole.TablePermissions.Add(this);
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0006759C File Offset: 0x0006579C
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.TableID.ResolveById(objectMap, throwIfCantResolve);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000675B4 File Offset: 0x000657B4
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.TableID.IsResolved && !this.body.TableID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Table"));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00067608 File Offset: 0x00065808
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			if (!this.body.TableID.TryResolveAfterCopy(copyContext) && this.body.TableID.Path != null && !this.body.TableID.Path.IsEmpty)
			{
				this.body._name = this.body.TableID.Path[this.body.TableID.Path.Count - 1].Value;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00067690 File Offset: 0x00065890
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.TableID.Validate(result, throwOnError);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000676A4 File Offset: 0x000658A4
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.TableID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000676C0 File Offset: 0x000658C0
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ColumnPermissions;
			yield break;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x000676D0 File Offset: 0x000658D0
		public TablePermissionAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000676D8 File Offset: 0x000658D8
		[CompatibilityRequirement("1400")]
		public TablePermissionExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x000676E0 File Offset: 0x000658E0
		[CompatibilityRequirement("1400")]
		public ColumnPermissionCollection ColumnPermissions
		{
			get
			{
				return this._ColumnPermissions;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000676E8 File Offset: 0x000658E8
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00067709 File Offset: 0x00065909
		public override string Name
		{
			get
			{
				if (this.Table != null)
				{
					return this.Table.Name;
				}
				return this.body._name;
			}
			set
			{
				if (this.Table != null)
				{
					throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReferencedObjects);
				}
				this.body._name = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0006772A File Offset: 0x0006592A
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00067738 File Offset: 0x00065938
		public string FilterExpression
		{
			get
			{
				return this.body.FilterExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.FilterExpression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "FilterExpression", typeof(string), this.body.FilterExpression, value);
					string filterExpression = this.body.FilterExpression;
					this.body.FilterExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "FilterExpression", typeof(string), filterExpression, value);
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000677A8 File Offset: 0x000659A8
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x000677B8 File Offset: 0x000659B8
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0006783C File Offset: 0x00065A3C
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0006784C File Offset: 0x00065A4C
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

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0006796E File Offset: 0x00065B6E
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0006797C File Offset: 0x00065B7C
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

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000679EC File Offset: 0x00065BEC
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x000679FC File Offset: 0x00065BFC
		[CompatibilityRequirement("1400")]
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
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.TablePermission_MetadataPermission.Merge(PropertyHelper.GetMetadataPermissionCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.TablePermission_MetadataPermission.Merge(PropertyHelper.GetMetadataPermissionCompatibilityRestrictions(this.body.MetadataPermission));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != MetadataPermission.Default))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "MetadataPermission", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MetadataPermission", typeof(MetadataPermission), this.body.MetadataPermission, value);
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
					MetadataPermission metadataPermission = this.body.MetadataPermission;
					this.body.MetadataPermission = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MetadataPermission", typeof(MetadataPermission), metadataPermission, value);
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00067B31 File Offset: 0x00065D31
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00067B44 File Offset: 0x00065D44
		public ModelRole Role
		{
			get
			{
				return this.body.RoleID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RoleID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Role", typeof(ModelRole), this.body.RoleID.Object, value);
					ModelRole @object = this.body.RoleID.Object;
					this.body.RoleID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Role", typeof(ModelRole), @object, value);
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00067BC8 File Offset: 0x00065DC8
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00067BDA File Offset: 0x00065DDA
		internal ObjectId _RoleID
		{
			get
			{
				return this.body.RoleID.ObjectID;
			}
			set
			{
				this.body.RoleID.ObjectID = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00067BED File Offset: 0x00065DED
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00067C00 File Offset: 0x00065E00
		public Table Table
		{
			get
			{
				return this.body.TableID.Object;
			}
			set
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00067C84 File Offset: 0x00065E84
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00067C96 File Offset: 0x00065E96
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

		// Token: 0x06000C6A RID: 3178 RVA: 0x00067CAC File Offset: 0x00065EAC
		internal void CopyFrom(TablePermission other, CopyContext context)
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
				this.ColumnPermissions.CopyFrom(other.ColumnPermissions, context);
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00067D80 File Offset: 0x00065F80
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((TablePermission)other, context);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00067D8F File Offset: 0x00065F8F
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(TablePermission other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00067DAB File Offset: 0x00065FAB
		public void CopyTo(TablePermission other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00067DC7 File Offset: 0x00065FC7
		public TablePermission Clone()
		{
			return base.CloneInternal<TablePermission>();
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00067DD0 File Offset: 0x00065FD0
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.RoleID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "RoleID", this.body.RoleID.Object);
			}
			this.body.TableID.Validate(null, true);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.FilterExpression))
			{
				writer.WriteProperty<string>(options, "FilterExpression", this.body.FilterExpression);
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<MetadataPermission>(options, "MetadataPermission", this.body.MetadataPermission);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00067EEC File Offset: 0x000660EC
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("RoleID", out objectId))
			{
				this.body.RoleID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId2))
			{
				this.body.TableID.ObjectID = objectId2;
			}
			string text;
			if (reader.TryReadProperty<string>("FilterExpression", out text))
			{
				this.body.FilterExpression = text;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			string text2;
			if (reader.TryReadProperty<string>("ErrorMessage", out text2))
			{
				this.body.ErrorMessage = text2;
			}
			MetadataPermission metadataPermission;
			if (CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<MetadataPermission>("MetadataPermission", out metadataPermission))
			{
				this.body.MetadataPermission = metadataPermission;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00067FDC File Offset: 0x000661DC
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.RoleID.Object != null && writer.ShouldIncludeProperty("RoleID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("RoleID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.RoleID.Object);
			}
			this.body.TableID.Validate(null, true);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.CrossLinkProperty, this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.FilterExpression) && writer.ShouldIncludeProperty("FilterExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("FilterExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.FilterExpression);
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MetadataPermission", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<MetadataPermission>("MetadataPermission", MetadataPropertyNature.RegularProperty, this.body.MetadataPermission);
				}
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0006815C File Offset: 0x0006635C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty, this.Name);
			}
			if (!string.IsNullOrEmpty(this.body.FilterExpression) && writer.ShouldIncludeProperty("filterExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("filterExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.FilterExpression);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
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
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("metadataPermission", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<MetadataPermission>("metadataPermission", MetadataPropertyNature.RegularProperty, this.body.MetadataPermission);
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
			if (this.ColumnPermissions.Count > 0)
			{
				if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ColumnPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("columnPermissions", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "columnPermissions", MetadataPropertyNature.ChildCollection, this.ColumnPermissions);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00068498 File Offset: 0x00066698
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
					if (propertyName == "name")
					{
						this.Name = reader.ReadStringProperty();
						return true;
					}
					break;
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
					if (propertyName == "RoleID")
					{
						this.body.RoleID.ObjectID = reader.ReadObjectIdProperty();
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
					if (c <= 'M')
					{
						if (c != 'E')
						{
							if (c != 'M')
							{
								break;
							}
							if (!(propertyName == "ModifiedTime"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "ErrorMessage"))
							{
								break;
							}
							goto IL_02E2;
						}
					}
					else if (c != 'e')
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
					else
					{
						if (!(propertyName == "errorMessage"))
						{
							break;
						}
						goto IL_02E2;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
					IL_02E2:
					this.body.ErrorMessage = reader.ReadStringProperty();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c != 'F')
					{
						if (c != 'f')
						{
							break;
						}
						if (!(propertyName == "filterExpression"))
						{
							break;
						}
					}
					else if (!(propertyName == "FilterExpression"))
					{
						break;
					}
					this.body.FilterExpression = reader.ReadStringProperty();
					return true;
				}
				case 17:
					if (propertyName == "columnPermissions")
					{
						if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ColumnPermission columnPermission in reader.ReadChildCollectionProperty<ColumnPermission>(context))
							{
								try
								{
									this.ColumnPermissions.Add(columnPermission);
								}
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, columnPermission, TomSR.Exception_FailedAddDeserializedNamedObject("ColumnPermission", (columnPermission != null) ? columnPermission.Name : null, ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					break;
				case 18:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'e')
						{
							if (c != 'm')
							{
								break;
							}
							if (!(propertyName == "metadataPermission"))
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
					else if (!(propertyName == "MetadataPermission"))
					{
						break;
					}
					if (!CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.MetadataPermission.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.MetadataPermission = reader.ReadEnumProperty<MetadataPermission>();
					return true;
				}
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00068A28 File Offset: 0x00066C28
		private protected override void OnDeserializeEnd(SerializationActivityContext context)
		{
			base.OnDeserializeEnd(context);
			if (context.SerializationMode != MetadataSerializationMode.Xmla)
			{
				context.RegistrerObjectForMasterReferenceCrossLinkReconstruction(this);
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00068A44 File Offset: 0x00066C44
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				result["name", TomPropCategory.Name, 0, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.FilterExpression))
			{
				result["filterExpression", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.FilterExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.Ready)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.MetadataPermission != MetadataPermission.Default)
			{
				if (!CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsMetadataPermissionValueCompatible(this.body.MetadataPermission, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MetadataPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["metadataPermission", TomPropCategory.Regular, 8, false] = JsonPropertyHelper.ConvertEnumToJsonValue<MetadataPermission>(this.body.MetadataPermission);
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
					IEnumerable<ColumnPermission> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ColumnPermission> columnPermissions = this.ColumnPermissions;
						enumerable3 = columnPermissions;
					}
					else
					{
						enumerable3 = this.ColumnPermissions.Where((ColumnPermission o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ColumnPermission> enumerable4 = enumerable3;
					if (enumerable4.Any<ColumnPermission>())
					{
						if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ColumnPermission is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ColumnPermission obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["columnPermissions", TomPropCategory.ChildCollection, 41, false] = array3;
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

		// Token: 0x06000C76 RID: 3190 RVA: 0x00068ED8 File Offset: 0x000670D8
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				if (length != 4)
				{
					if (length != 5)
					{
						switch (length)
						{
						case 11:
							if (name == "annotations")
							{
								JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
							break;
						case 12:
						{
							char c = name[0];
							if (c != 'e')
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
							else if (name == "errorMessage")
							{
								this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
							break;
						}
						case 16:
							if (name == "filterExpression")
							{
								this.body.FilterExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
							break;
						case 17:
							if (name == "columnPermissions")
							{
								if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								JsonPropertyHelper.ReadObjectCollection(this.ColumnPermissions, jsonProp.Value, options, mode, dbCompatibilityLevel);
								return true;
							}
							break;
						case 18:
						{
							char c = name[0];
							if (c != 'e')
							{
								if (c == 'm')
								{
									if (name == "metadataPermission")
									{
										MetadataPermission metadataPermission = JsonPropertyHelper.ConvertJsonValueToEnum<MetadataPermission>(jsonProp.Value);
										if (jsonProp.Value.Type != 10 && (!CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsMetadataPermissionValueCompatible(metadataPermission, mode, dbCompatibilityLevel)))
										{
											return false;
										}
										this.body.MetadataPermission = metadataPermission;
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
						}
					}
					else if (name == "state")
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
				else if (name == "name")
				{
					this.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00069180 File Offset: 0x00067380
		internal override bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			if (this.body.TableID.Object != null)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.body._name))
			{
				return false;
			}
			if (this.body.TableID.Path == null || this.body.TableID.Path.IsEmpty)
			{
				this.body.TableID.Path = new ObjectPath(ObjectType.Table, this.body._name);
			}
			return true;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00069200 File Offset: 0x00067400
		void ILinkedMetadataObject.GetLinkedObjectTarget(out ObjectId objectId, out ObjectPath objectPath, out MetadataObject @object, out string property)
		{
			objectId = this.body.TableID.ObjectID;
			objectPath = this.body.TableID.Path;
			@object = this.body.TableID.Object;
			property = null;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00069240 File Offset: 0x00067440
		internal override string GetFormattedObjectPath()
		{
			if (this.Role != null)
			{
				return TomSR.ObjectPath_TablePermission_2Args(this.Name, this.Role.Name);
			}
			return TomSR.ObjectPath_TablePermission_1Arg(this.Name);
		}

		// Token: 0x04000184 RID: 388
		internal TablePermission.ObjectBody body;

		// Token: 0x04000185 RID: 389
		private TablePermissionAnnotationCollection _Annotations;

		// Token: 0x04000186 RID: 390
		private TablePermissionExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x04000187 RID: 391
		private ColumnPermissionCollection _ColumnPermissions;

		// Token: 0x020002D9 RID: 729
		internal class ObjectBody : NamedMetadataObjectBody<TablePermission>
		{
			// Token: 0x0600234F RID: 9039 RVA: 0x000E1478 File Offset: 0x000DF678
			public ObjectBody(TablePermission owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.RoleID = new ParentLink<TablePermission, ModelRole>(owner, "Role");
				this.TableID = new CrossLink<TablePermission, Table>(owner, "Table");
			}

			// Token: 0x06002350 RID: 9040 RVA: 0x000E14AE File Offset: 0x000DF6AE
			public override string GetObjectName()
			{
				if (this.TableID.Object == null)
				{
					return this._name;
				}
				return this.TableID.Object.Name;
			}

			// Token: 0x06002351 RID: 9041 RVA: 0x000E14D4 File Offset: 0x000DF6D4
			internal bool IsEqualTo(TablePermission.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.FilterExpression, other.FilterExpression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.MetadataPermission, other.MetadataPermission) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.RoleID.IsEqualTo(other.RoleID, context)) && this.TableID.IsEqualTo(other.TableID, context);
			}

			// Token: 0x06002352 RID: 9042 RVA: 0x000E15C4 File Offset: 0x000DF7C4
			internal void CopyFromImpl(TablePermission.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.FilterExpression = other.FilterExpression;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				this.MetadataPermission = other.MetadataPermission;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.RoleID.CopyFrom(other.RoleID, context);
				}
				this.TableID.CopyFrom(other.TableID, context);
				this._name = other._name;
			}

			// Token: 0x06002353 RID: 9043 RVA: 0x000E169C File Offset: 0x000DF89C
			internal void CopyFromImpl(TablePermission.ObjectBody other)
			{
				this.FilterExpression = other.FilterExpression;
				this.ModifiedTime = other.ModifiedTime;
				this.State = other.State;
				this.ErrorMessage = other.ErrorMessage;
				this.MetadataPermission = other.MetadataPermission;
				this.RoleID.CopyFrom(other.RoleID, ObjectChangeTracker.BodyCloneContext);
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
				this._name = other._name;
			}

			// Token: 0x06002354 RID: 9044 RVA: 0x000E171D File Offset: 0x000DF91D
			public override void CopyFrom(MetadataObjectBody<TablePermission> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((TablePermission.ObjectBody)other, context);
			}

			// Token: 0x06002355 RID: 9045 RVA: 0x000E1734 File Offset: 0x000DF934
			internal bool IsEqualTo(TablePermission.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.FilterExpression, other.FilterExpression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.MetadataPermission, other.MetadataPermission) && this.RoleID.IsEqualTo(other.RoleID) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x06002356 RID: 9046 RVA: 0x000E17D5 File Offset: 0x000DF9D5
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((TablePermission.ObjectBody)other);
			}

			// Token: 0x06002357 RID: 9047 RVA: 0x000E17F0 File Offset: 0x000DF9F0
			internal void CompareWith(TablePermission.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.FilterExpression, other.FilterExpression))
				{
					context.RegisterPropertyChange(base.Owner, "FilterExpression", typeof(string), PropertyFlags.DdlAndUser, other.FilterExpression, this.FilterExpression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MetadataPermission, other.MetadataPermission))
				{
					context.RegisterPropertyChange(base.Owner, "MetadataPermission", typeof(MetadataPermission), PropertyFlags.DdlAndUser, other.MetadataPermission, this.MetadataPermission);
				}
				this.RoleID.CompareWith(other.RoleID, "RoleID", "Role", PropertyFlags.ReadOnly, context);
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.None, context);
			}

			// Token: 0x06002358 RID: 9048 RVA: 0x000E197C File Offset: 0x000DFB7C
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((TablePermission.ObjectBody)other, context);
			}

			// Token: 0x04000A7C RID: 2684
			internal string FilterExpression;

			// Token: 0x04000A7D RID: 2685
			internal DateTime ModifiedTime;

			// Token: 0x04000A7E RID: 2686
			internal ObjectState State;

			// Token: 0x04000A7F RID: 2687
			internal string ErrorMessage;

			// Token: 0x04000A80 RID: 2688
			internal MetadataPermission MetadataPermission;

			// Token: 0x04000A81 RID: 2689
			internal ParentLink<TablePermission, ModelRole> RoleID;

			// Token: 0x04000A82 RID: 2690
			internal CrossLink<TablePermission, Table> TableID;

			// Token: 0x04000A83 RID: 2691
			internal string _name;
		}
	}
}
