using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000363 RID: 867
	internal class RootTypeInfo : TypeInfo
	{
		// Token: 0x06002A21 RID: 10785 RVA: 0x00089410 File Offset: 0x00087610
		internal RootTypeInfo(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap)
			: base(type, null)
		{
			PlanCompiler.Assert(type.EdmType.BaseType == null, "only root types allowed here");
			this.m_propertyMap = new Dictionary<PropertyRef, EdmProperty>();
			this.m_propertyRefList = new List<PropertyRef>();
			this.m_discriminatorMap = discriminatorMap;
			this.TypeIdKind = TypeIdKind.Generated;
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x00089461 File Offset: 0x00087661
		// (set) Token: 0x06002A23 RID: 10787 RVA: 0x00089469 File Offset: 0x00087669
		internal TypeIdKind TypeIdKind { get; set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x00089472 File Offset: 0x00087672
		// (set) Token: 0x06002A25 RID: 10789 RVA: 0x0008947A File Offset: 0x0008767A
		internal TypeUsage TypeIdType { get; set; }

		// Token: 0x06002A26 RID: 10790 RVA: 0x00089483 File Offset: 0x00087683
		internal void AddPropertyMapping(PropertyRef propertyRef, EdmProperty newProperty)
		{
			this.m_propertyMap[propertyRef] = newProperty;
			if (propertyRef is TypeIdPropertyRef)
			{
				this.m_typeIdProperty = newProperty;
				return;
			}
			if (propertyRef is EntitySetIdPropertyRef)
			{
				this.m_entitySetIdProperty = newProperty;
				return;
			}
			if (propertyRef is NullSentinelPropertyRef)
			{
				this.m_nullSentinelProperty = newProperty;
			}
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000894C1 File Offset: 0x000876C1
		internal void AddPropertyRef(PropertyRef propertyRef)
		{
			this.m_propertyRefList.Add(propertyRef);
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000894CF File Offset: 0x000876CF
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x000894D7 File Offset: 0x000876D7
		internal new RowType FlattenedType
		{
			get
			{
				return this.m_flattenedType;
			}
			set
			{
				this.m_flattenedType = value;
				this.m_flattenedTypeUsage = TypeUsage.Create(value);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000894EC File Offset: 0x000876EC
		internal new TypeUsage FlattenedTypeUsage
		{
			get
			{
				return this.m_flattenedTypeUsage;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000894F4 File Offset: 0x000876F4
		internal ExplicitDiscriminatorMap DiscriminatorMap
		{
			get
			{
				return this.m_discriminatorMap;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000894FC File Offset: 0x000876FC
		internal new EdmProperty EntitySetIdProperty
		{
			get
			{
				return this.m_entitySetIdProperty;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00089504 File Offset: 0x00087704
		internal new EdmProperty NullSentinelProperty
		{
			get
			{
				return this.m_nullSentinelProperty;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x0008950C File Offset: 0x0008770C
		internal new IEnumerable<PropertyRef> PropertyRefList
		{
			get
			{
				return this.m_propertyRefList;
			}
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00089514 File Offset: 0x00087714
		internal int GetNestedStructureOffset(PropertyRef property)
		{
			for (int i = 0; i < this.m_propertyRefList.Count; i++)
			{
				NestedPropertyRef nestedPropertyRef = this.m_propertyRefList[i] as NestedPropertyRef;
				if (nestedPropertyRef != null && nestedPropertyRef.InnerProperty.Equals(property))
				{
					return i;
				}
			}
			PlanCompiler.Assert(false, "no complex structure " + ((property != null) ? property.ToString() : null) + " found in TypeInfo");
			return 0;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00089580 File Offset: 0x00087780
		internal new bool TryGetNewProperty(PropertyRef propertyRef, bool throwIfMissing, out EdmProperty property)
		{
			bool flag = this.m_propertyMap.TryGetValue(propertyRef, out property);
			if (throwIfMissing && !flag)
			{
				PlanCompiler.Assert(false, "Unable to find property " + ((propertyRef != null) ? propertyRef.ToString() : null) + " in type " + base.Type.EdmType.Identity);
			}
			return flag;
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000895D4 File Offset: 0x000877D4
		internal new EdmProperty TypeIdProperty
		{
			get
			{
				return this.m_typeIdProperty;
			}
		}

		// Token: 0x04000E71 RID: 3697
		private readonly List<PropertyRef> m_propertyRefList;

		// Token: 0x04000E72 RID: 3698
		private readonly Dictionary<PropertyRef, EdmProperty> m_propertyMap;

		// Token: 0x04000E73 RID: 3699
		private EdmProperty m_nullSentinelProperty;

		// Token: 0x04000E74 RID: 3700
		private EdmProperty m_typeIdProperty;

		// Token: 0x04000E75 RID: 3701
		private readonly ExplicitDiscriminatorMap m_discriminatorMap;

		// Token: 0x04000E76 RID: 3702
		private EdmProperty m_entitySetIdProperty;

		// Token: 0x04000E77 RID: 3703
		private RowType m_flattenedType;

		// Token: 0x04000E78 RID: 3704
		private TypeUsage m_flattenedTypeUsage;
	}
}
