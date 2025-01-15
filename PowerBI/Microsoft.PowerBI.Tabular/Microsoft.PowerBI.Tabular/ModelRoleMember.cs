using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000082 RID: 130
	public abstract class ModelRoleMember : NamedMetadataObject
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x00041AEA File Offset: 0x0003FCEA
		private protected ModelRoleMember()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00041AFD File Offset: 0x0003FCFD
		private protected ModelRoleMember(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00041B0C File Offset: 0x0003FD0C
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new ModelRoleMember.ObjectBody(this);
			this.body.MemberName = string.Empty;
			this.body.MemberID = string.Empty;
			this.body.IdentityProvider = string.Empty;
			this.body.MemberType = RoleMemberType.Auto;
			this._Annotations = new ModelRoleMemberAnnotationCollection(this, comparer);
			this._ExtendedProperties = new ModelRoleMemberExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00041B7B File Offset: 0x0003FD7B
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.RoleMembership;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00041B7F File Offset: 0x0003FD7F
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00041B91 File Offset: 0x0003FD91
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
					MetadataObject.UpdateMetadataObjectParent<ModelRoleMember, ModelRole>(this.body.RoleID, (ModelRole)value, null, null);
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00041BBE File Offset: 0x0003FDBE
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.RoleID.ObjectID;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00041BD0 File Offset: 0x0003FDD0
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateChoiceScope())
			{
				ModelRoleMember.WriteMetadataSchemaForWindowsModelRoleMember(context, writer);
				ModelRoleMember.WriteMetadataSchemaForExternalModelRoleMember(context, writer);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00041C10 File Offset: 0x0003FE10
		private static void WriteMetadataSchemaForWindowsModelRoleMember(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.RoleMembership, "WindowsModelRoleMember", "WindowsModelRoleMember object of Tabular Object Model (TOM)", new bool?(false)))
			{
				ModelRoleMember.WriteMetadataSchemaForCommonModelRoleMemberRegularProperties(context, writer);
				ModelRoleMember.WriteMetadataSchemaForCommonModelRoleMemberChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00041C60 File Offset: 0x0003FE60
		private static void WriteMetadataSchemaForExternalModelRoleMember(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.RoleMembership, "ExternalModelRoleMember", "ExternalModelRoleMember object of Tabular Object Model (TOM)", new bool?(false)))
			{
				ModelRoleMember.WriteMetadataSchemaForCommonModelRoleMemberRegularProperties(context, writer);
				if (writer.ShouldIncludeProperty("identityProvider", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("identityProvider", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("memberType", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RoleMemberType>("memberType", MetadataPropertyNature.RegularProperty, null);
				}
				ModelRoleMember.WriteMetadataSchemaForCommonModelRoleMemberChildCollectionsProperties(context, writer);
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00041CF0 File Offset: 0x0003FEF0
		private static void WriteMetadataSchemaForCommonModelRoleMemberRegularProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("memberName", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("memberName", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("memberId", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteProperty("memberId", MetadataPropertyNature.RegularProperty, typeof(string));
			}
			if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00041D74 File Offset: 0x0003FF74
		private static void WriteMetadataSchemaForCommonModelRoleMemberChildCollectionsProperties(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
			}
			if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00041DD3 File Offset: 0x0003FFD3
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00041DDB File Offset: 0x0003FFDB
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ModelRoleMember.ObjectBody)value;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00041DE9 File Offset: 0x0003FFE9
		internal override ITxObjectBody CreateBody()
		{
			return new ModelRoleMember.ObjectBody(this);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00041DF1 File Offset: 0x0003FFF1
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((ModelRole)parent).Members;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00041E00 File Offset: 0x00040000
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			ModelRole modelRole = MetadataObject.ResolveMetadataObjectParentById<ModelRoleMember, ModelRole>(this.body.RoleID, objectMap, throwIfCantResolve, null, null);
			if (modelRole != null)
			{
				modelRole.Members.Add(this);
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00041E31 File Offset: 0x00040031
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00041E33 File Offset: 0x00040033
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00041E43 File Offset: 0x00040043
		public ModelRoleMemberAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00041E4B File Offset: 0x0004004B
		[CompatibilityRequirement("1400")]
		public ModelRoleMemberExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00041E53 File Offset: 0x00040053
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x00041E60 File Offset: 0x00040060
		public string MemberName
		{
			get
			{
				return this.body.MemberName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MemberName, value))
				{
					if (this.Parent != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("MemberName", "ModelRoleMember"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MemberName", typeof(string), this.body.MemberName, value);
					string memberName = this.body.MemberName;
					this.body.MemberName = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MemberName", typeof(string), memberName, value);
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00041EED File Offset: 0x000400ED
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00041EFC File Offset: 0x000400FC
		public string MemberID
		{
			get
			{
				return this.body.MemberID;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MemberID, value))
				{
					if (this.Parent != null)
					{
						throw new InvalidOperationException(TomSR.Exception_CantChangeImmutableProperty("MemberID", "ModelRoleMember"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MemberID", typeof(string), this.body.MemberID, value);
					string memberID = this.body.MemberID;
					this.body.MemberID = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MemberID", typeof(string), memberID, value);
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00041F89 File Offset: 0x00040189
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00041F98 File Offset: 0x00040198
		internal string IdentityProvider
		{
			get
			{
				return this.body.IdentityProvider;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.IdentityProvider, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "IdentityProvider", typeof(string), this.body.IdentityProvider, value);
					string identityProvider = this.body.IdentityProvider;
					this.body.IdentityProvider = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "IdentityProvider", typeof(string), identityProvider, value);
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00042008 File Offset: 0x00040208
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00042018 File Offset: 0x00040218
		internal RoleMemberType MemberType
		{
			get
			{
				return this.body.MemberType;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MemberType, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "MemberType", typeof(RoleMemberType), this.body.MemberType, value);
					RoleMemberType memberType = this.body.MemberType;
					this.body.MemberType = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MemberType", typeof(RoleMemberType), memberType, value);
				}
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0004209C File Offset: 0x0004029C
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x000420AC File Offset: 0x000402AC
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00042130 File Offset: 0x00040330
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00042144 File Offset: 0x00040344
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000421C8 File Offset: 0x000403C8
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x000421DA File Offset: 0x000403DA
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

		// Token: 0x060007CD RID: 1997 RVA: 0x000421F0 File Offset: 0x000403F0
		internal void CopyFrom(ModelRoleMember other, CopyContext context)
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

		// Token: 0x060007CE RID: 1998 RVA: 0x000422B2 File Offset: 0x000404B2
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ModelRoleMember)other, context);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000422C1 File Offset: 0x000404C1
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ModelRoleMember other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000422DD File Offset: 0x000404DD
		public void CopyTo(ModelRoleMember other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000422F9 File Offset: 0x000404F9
		public ModelRoleMember Clone()
		{
			return base.CloneInternal<ModelRoleMember>();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00042304 File Offset: 0x00040504
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.RoleID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "RoleID", this.body.RoleID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.MemberName))
			{
				writer.WriteProperty<string>(options, "MemberName", this.body.MemberName);
			}
			if (!string.IsNullOrEmpty(this.body.MemberID))
			{
				writer.WriteProperty<string>(options, "MemberID", this.body.MemberID);
			}
			if (!string.IsNullOrEmpty(this.body.IdentityProvider))
			{
				writer.WriteProperty<string>(options, "IdentityProvider", this.body.IdentityProvider);
			}
			if (this.body.MemberType != RoleMemberType.Auto)
			{
				writer.WriteProperty<RoleMemberType>(options, "MemberType", this.body.MemberType);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000423EC File Offset: 0x000405EC
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("RoleID", out objectId))
			{
				this.body.RoleID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("MemberName", out text))
			{
				this.body.MemberName = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("MemberID", out text2))
			{
				this.body.MemberID = text2;
			}
			string text3;
			if (reader.TryReadProperty<string>("IdentityProvider", out text3))
			{
				this.body.IdentityProvider = text3;
			}
			RoleMemberType roleMemberType;
			if (reader.TryReadProperty<RoleMemberType>("MemberType", out roleMemberType))
			{
				this.body.MemberType = roleMemberType;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000424AC File Offset: 0x000406AC
		private protected sealed override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.RoleID.Object != null && writer.ShouldIncludeProperty("RoleID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("RoleID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.RoleID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.MemberName) && writer.ShouldIncludeProperty("MemberName", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("MemberName", MetadataPropertyNature.RegularProperty, this.body.MemberName);
			}
			if (!string.IsNullOrEmpty(this.body.MemberID) && writer.ShouldIncludeProperty("MemberID", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("MemberID", MetadataPropertyNature.RegularProperty, this.body.MemberID);
			}
			if (!string.IsNullOrEmpty(this.body.IdentityProvider) && writer.ShouldIncludeProperty("IdentityProvider", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("IdentityProvider", MetadataPropertyNature.RegularProperty, this.body.IdentityProvider);
			}
			if (this.body.MemberType != RoleMemberType.Auto && writer.ShouldIncludeProperty("MemberType", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<RoleMemberType>("MemberType", MetadataPropertyNature.RegularProperty, this.body.MemberType);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000425E0 File Offset: 0x000407E0
		private protected virtual void WriteRegularPropertiesToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!string.IsNullOrEmpty(this.body.MemberName) && writer.ShouldIncludeProperty("memberName", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("memberName", MetadataPropertyNature.RegularProperty, this.body.MemberName);
			}
			if (!string.IsNullOrEmpty(this.body.MemberID) && writer.ShouldIncludeProperty("memberId", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("memberId", MetadataPropertyNature.RegularProperty, this.body.MemberID);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000426A0 File Offset: 0x000408A0
		private protected sealed override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			this.WriteRegularPropertiesToMetadataStream(context, writer);
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

		// Token: 0x060007D7 RID: 2007 RVA: 0x00042760 File Offset: 0x00040960
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
				case 6:
					if (propertyName == "RoleID")
					{
						this.body.RoleID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 8:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "memberId"))
						{
							break;
						}
					}
					else if (!(propertyName == "MemberID"))
					{
						break;
					}
					this.body.MemberID = reader.ReadStringProperty();
					return true;
				}
				case 10:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'm')
						{
							break;
						}
						if (!(propertyName == "memberName"))
						{
							break;
						}
					}
					else if (!(propertyName == "MemberName"))
					{
						if (!(propertyName == "MemberType"))
						{
							break;
						}
						this.body.MemberType = reader.ReadEnumProperty<RoleMemberType>();
						return true;
					}
					this.body.MemberName = reader.ReadStringProperty();
					return true;
				}
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
				case 16:
					if (propertyName == "IdentityProvider")
					{
						this.body.IdentityProvider = reader.ReadStringProperty();
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
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
								}
							}
						}
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00042AE8 File Offset: 0x00040CE8
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.MemberName))
			{
				result["memberName", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.MemberName, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.MemberID))
			{
				result["memberId", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.MemberID, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 7, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
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

		// Token: 0x060007D9 RID: 2009 RVA: 0x00042D34 File Offset: 0x00040F34
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "memberName")
			{
				this.body.MemberName = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "memberId")
			{
				this.body.MemberID = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
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

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00042E26 File Offset: 0x00041026
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00042E33 File Offset: 0x00041033
		public override string Name
		{
			get
			{
				return ModelRoleMember.ComputeName(this.body);
			}
			set
			{
				throw new InvalidOperationException(TomSR.Exception_NameCannotBeSetForReadOnlyNamedObjects);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00042E40 File Offset: 0x00041040
		internal static ModelRoleMember CreateFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			string text;
			if (reader.TryMoveToProperty("identityProvider"))
			{
				text = reader.ReadStringProperty();
			}
			else
			{
				text = null;
			}
			reader.Reset();
			if (string.IsNullOrEmpty(text))
			{
				return new WindowsModelRoleMember();
			}
			return new ExternalModelRoleMember();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00042E7E File Offset: 0x0004107E
		internal bool IsWindowsIdentityProvider()
		{
			return string.IsNullOrEmpty(this.body.IdentityProvider);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00042E90 File Offset: 0x00041090
		private static string ComputeName(ModelRoleMember.ObjectBody body)
		{
			string text = (string.IsNullOrEmpty(body.MemberName) ? body.MemberID : body.MemberName);
			if (string.IsNullOrEmpty(body.IdentityProvider))
			{
				return text;
			}
			if (body.IdentityProvider.Contains('#'))
			{
				return string.Format("{0}#'{1}'", text, body.IdentityProvider.Replace("'", "''"));
			}
			return string.Format("{0}#{1}", text, body.IdentityProvider);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00042F09 File Offset: 0x00041109
		internal override string GetFormattedObjectPath()
		{
			if (this.Role != null)
			{
				return TomSR.ObjectPath_RoleMembership_2Args(this.Name, this.Role.Name);
			}
			return TomSR.ObjectPath_RoleMembership_1Arg(this.Name);
		}

		// Token: 0x0400013C RID: 316
		internal ModelRoleMember.ObjectBody body;

		// Token: 0x0400013D RID: 317
		private ModelRoleMemberAnnotationCollection _Annotations;

		// Token: 0x0400013E RID: 318
		private ModelRoleMemberExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0400013F RID: 319
		internal static Func<ModelRoleMember, ModelRoleMember, bool> CompareRoleMembershipType = (ModelRoleMember member1, ModelRoleMember member2) => member1.IsWindowsIdentityProvider() == member2.IsWindowsIdentityProvider();

		// Token: 0x02000290 RID: 656
		internal class ObjectBody : NamedMetadataObjectBody<ModelRoleMember>
		{
			// Token: 0x06002173 RID: 8563 RVA: 0x000D965B File Offset: 0x000D785B
			public ObjectBody(ModelRoleMember owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.RoleID = new ParentLink<ModelRoleMember, ModelRole>(owner, "Role");
			}

			// Token: 0x06002174 RID: 8564 RVA: 0x000D9680 File Offset: 0x000D7880
			public override string GetObjectName()
			{
				return ModelRoleMember.ComputeName(this);
			}

			// Token: 0x06002175 RID: 8565 RVA: 0x000D9688 File Offset: 0x000D7888
			internal bool IsEqualTo(ModelRoleMember.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.MemberName, other.MemberName) && PropertyHelper.AreValuesIdentical(this.MemberID, other.MemberID) && PropertyHelper.AreValuesIdentical(this.IdentityProvider, other.IdentityProvider) && PropertyHelper.AreValuesIdentical(this.MemberType, other.MemberType) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.RoleID.IsEqualTo(other.RoleID, context));
			}

			// Token: 0x06002176 RID: 8566 RVA: 0x000D973C File Offset: 0x000D793C
			internal void CopyFromImpl(ModelRoleMember.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.MemberName = other.MemberName;
				this.MemberID = other.MemberID;
				this.IdentityProvider = other.IdentityProvider;
				this.MemberType = other.MemberType;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.RoleID.CopyFrom(other.RoleID, context);
				}
			}

			// Token: 0x06002177 RID: 8567 RVA: 0x000D97D0 File Offset: 0x000D79D0
			internal void CopyFromImpl(ModelRoleMember.ObjectBody other)
			{
				this.MemberName = other.MemberName;
				this.MemberID = other.MemberID;
				this.IdentityProvider = other.IdentityProvider;
				this.MemberType = other.MemberType;
				this.ModifiedTime = other.ModifiedTime;
				this.RoleID.CopyFrom(other.RoleID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002178 RID: 8568 RVA: 0x000D982F File Offset: 0x000D7A2F
			public override void CopyFrom(MetadataObjectBody<ModelRoleMember> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ModelRoleMember.ObjectBody)other, context);
			}

			// Token: 0x06002179 RID: 8569 RVA: 0x000D9848 File Offset: 0x000D7A48
			internal bool IsEqualTo(ModelRoleMember.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.MemberName, other.MemberName) && PropertyHelper.AreValuesIdentical(this.MemberID, other.MemberID) && PropertyHelper.AreValuesIdentical(this.IdentityProvider, other.IdentityProvider) && PropertyHelper.AreValuesIdentical(this.MemberType, other.MemberType) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.RoleID.IsEqualTo(other.RoleID);
			}

			// Token: 0x0600217A RID: 8570 RVA: 0x000D98D4 File Offset: 0x000D7AD4
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ModelRoleMember.ObjectBody)other);
			}

			// Token: 0x0600217B RID: 8571 RVA: 0x000D98F0 File Offset: 0x000D7AF0
			internal void CompareWith(ModelRoleMember.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.MemberName, other.MemberName))
				{
					context.RegisterPropertyChange(base.Owner, "MemberName", typeof(string), PropertyFlags.DdlAndUser, other.MemberName, this.MemberName);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MemberID, other.MemberID))
				{
					context.RegisterPropertyChange(base.Owner, "MemberID", typeof(string), PropertyFlags.DdlAndUser, other.MemberID, this.MemberID);
				}
				if (!PropertyHelper.AreValuesIdentical(this.IdentityProvider, other.IdentityProvider))
				{
					context.RegisterPropertyChange(base.Owner, "IdentityProvider", typeof(string), PropertyFlags.DdlAndUser, other.IdentityProvider, this.IdentityProvider);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MemberType, other.MemberType))
				{
					context.RegisterPropertyChange(base.Owner, "MemberType", typeof(RoleMemberType), PropertyFlags.DdlAndUser, other.MemberType, this.MemberType);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.RoleID.CompareWith(other.RoleID, "RoleID", "Role", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x0600217C RID: 8572 RVA: 0x000D9A55 File Offset: 0x000D7C55
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ModelRoleMember.ObjectBody)other, context);
			}

			// Token: 0x04000931 RID: 2353
			internal string MemberName;

			// Token: 0x04000932 RID: 2354
			internal string MemberID;

			// Token: 0x04000933 RID: 2355
			internal string IdentityProvider;

			// Token: 0x04000934 RID: 2356
			internal RoleMemberType MemberType;

			// Token: 0x04000935 RID: 2357
			internal DateTime ModifiedTime;

			// Token: 0x04000936 RID: 2358
			internal ParentLink<ModelRoleMember, ModelRole> RoleID;
		}
	}
}
