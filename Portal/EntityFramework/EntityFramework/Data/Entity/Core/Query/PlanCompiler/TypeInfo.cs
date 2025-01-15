using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000373 RID: 883
	internal class TypeInfo
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x0008CA70 File Offset: 0x0008AC70
		internal static TypeInfo Create(TypeUsage type, TypeInfo superTypeInfo, ExplicitDiscriminatorMap discriminatorMap)
		{
			TypeInfo typeInfo;
			if (superTypeInfo == null)
			{
				typeInfo = new RootTypeInfo(type, discriminatorMap);
			}
			else
			{
				typeInfo = new TypeInfo(type, superTypeInfo);
			}
			return typeInfo;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0008CA93 File Offset: 0x0008AC93
		protected TypeInfo(TypeUsage type, TypeInfo superType)
		{
			this.m_type = type;
			this.m_immediateSubTypes = new List<TypeInfo>();
			this.m_superType = superType;
			if (superType != null)
			{
				superType.m_immediateSubTypes.Add(this);
				this.m_rootType = superType.RootType;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x0008CACF File Offset: 0x0008ACCF
		internal bool IsRootType
		{
			get
			{
				return this.m_rootType == null;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x0008CADA File Offset: 0x0008ACDA
		internal List<TypeInfo> ImmediateSubTypes
		{
			get
			{
				return this.m_immediateSubTypes;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x0008CAE2 File Offset: 0x0008ACE2
		internal TypeInfo SuperType
		{
			get
			{
				return this.m_superType;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x0008CAEA File Offset: 0x0008ACEA
		internal RootTypeInfo RootType
		{
			get
			{
				return this.m_rootType ?? ((RootTypeInfo)this);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x0008CAFC File Offset: 0x0008ACFC
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x0008CB04 File Offset: 0x0008AD04
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x0008CB0C File Offset: 0x0008AD0C
		internal object TypeId { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x0008CB15 File Offset: 0x0008AD15
		internal virtual RowType FlattenedType
		{
			get
			{
				return this.RootType.FlattenedType;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x0008CB22 File Offset: 0x0008AD22
		internal virtual TypeUsage FlattenedTypeUsage
		{
			get
			{
				return this.RootType.FlattenedTypeUsage;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x0008CB2F File Offset: 0x0008AD2F
		internal virtual EdmProperty EntitySetIdProperty
		{
			get
			{
				return this.RootType.EntitySetIdProperty;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x0008CB3C File Offset: 0x0008AD3C
		internal bool HasEntitySetIdProperty
		{
			get
			{
				return this.RootType.EntitySetIdProperty != null;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x0008CB4C File Offset: 0x0008AD4C
		internal virtual EdmProperty NullSentinelProperty
		{
			get
			{
				return this.RootType.NullSentinelProperty;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x0008CB59 File Offset: 0x0008AD59
		internal bool HasNullSentinelProperty
		{
			get
			{
				return this.RootType.NullSentinelProperty != null;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x0008CB69 File Offset: 0x0008AD69
		internal virtual EdmProperty TypeIdProperty
		{
			get
			{
				return this.RootType.TypeIdProperty;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x0008CB76 File Offset: 0x0008AD76
		internal bool HasTypeIdProperty
		{
			get
			{
				return this.RootType.TypeIdProperty != null;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x0008CB86 File Offset: 0x0008AD86
		internal virtual IEnumerable<PropertyRef> PropertyRefList
		{
			get
			{
				return this.RootType.PropertyRefList;
			}
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x0008CB94 File Offset: 0x0008AD94
		internal EdmProperty GetNewProperty(PropertyRef propertyRef)
		{
			EdmProperty edmProperty;
			this.TryGetNewProperty(propertyRef, true, out edmProperty);
			return edmProperty;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x0008CBAD File Offset: 0x0008ADAD
		internal bool TryGetNewProperty(PropertyRef propertyRef, bool throwIfMissing, out EdmProperty newProperty)
		{
			return this.RootType.TryGetNewProperty(propertyRef, throwIfMissing, out newProperty);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x0008CBBD File Offset: 0x0008ADBD
		internal IEnumerable<PropertyRef> GetKeyPropertyRefs()
		{
			RefType refType = null;
			EntityTypeBase entityTypeBase;
			if (TypeHelpers.TryGetEdmType<RefType>(this.m_type, out refType))
			{
				entityTypeBase = refType.ElementType;
			}
			else
			{
				entityTypeBase = TypeHelpers.GetEdmType<EntityTypeBase>(this.m_type);
			}
			foreach (EdmMember edmMember in entityTypeBase.KeyMembers)
			{
				PlanCompiler.Assert(edmMember is EdmProperty, "Non-EdmProperty key members are not supported");
				SimplePropertyRef simplePropertyRef = new SimplePropertyRef(edmMember);
				yield return simplePropertyRef;
			}
			ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmMember>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0008CBCD File Offset: 0x0008ADCD
		internal IEnumerable<PropertyRef> GetIdentityPropertyRefs()
		{
			if (this.HasEntitySetIdProperty)
			{
				yield return EntitySetIdPropertyRef.Instance;
			}
			foreach (PropertyRef propertyRef in this.GetKeyPropertyRefs())
			{
				yield return propertyRef;
			}
			IEnumerator<PropertyRef> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0008CBDD File Offset: 0x0008ADDD
		internal IEnumerable<PropertyRef> GetAllPropertyRefs()
		{
			foreach (PropertyRef propertyRef in this.PropertyRefList)
			{
				yield return propertyRef;
			}
			IEnumerator<PropertyRef> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0008CBED File Offset: 0x0008ADED
		internal IEnumerable<EdmProperty> GetAllProperties()
		{
			foreach (EdmProperty edmProperty in this.FlattenedType.Properties)
			{
				yield return edmProperty;
			}
			ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmProperty>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0008CC00 File Offset: 0x0008AE00
		internal List<TypeInfo> GetTypeHierarchy()
		{
			List<TypeInfo> list = new List<TypeInfo>();
			this.GetTypeHierarchy(list);
			return list;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0008CC1C File Offset: 0x0008AE1C
		private void GetTypeHierarchy(List<TypeInfo> result)
		{
			result.Add(this);
			foreach (TypeInfo typeInfo in this.ImmediateSubTypes)
			{
				typeInfo.GetTypeHierarchy(result);
			}
		}

		// Token: 0x04000EC8 RID: 3784
		private readonly TypeUsage m_type;

		// Token: 0x04000EC9 RID: 3785
		private readonly List<TypeInfo> m_immediateSubTypes;

		// Token: 0x04000ECA RID: 3786
		private readonly TypeInfo m_superType;

		// Token: 0x04000ECB RID: 3787
		private readonly RootTypeInfo m_rootType;
	}
}
