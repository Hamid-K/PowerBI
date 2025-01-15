using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000523 RID: 1315
	public class ComplexTypeMapping : StructuralTypeMapping
	{
		// Token: 0x060040C4 RID: 16580 RVA: 0x000DADC8 File Offset: 0x000D8FC8
		public ComplexTypeMapping(ComplexType complexType)
		{
			Check.NotNull<ComplexType>(complexType, "complexType");
			this.AddType(complexType);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x000DAE30 File Offset: 0x000D9030
		internal ComplexTypeMapping(bool isPartial)
		{
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x000DAE83 File Offset: 0x000D9083
		public ComplexType ComplexType
		{
			get
			{
				return this.m_types.Values.SingleOrDefault<ComplexType>();
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x000DAE95 File Offset: 0x000D9095
		internal ReadOnlyCollection<ComplexType> Types
		{
			get
			{
				return new ReadOnlyCollection<ComplexType>(new List<ComplexType>(this.m_types.Values));
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x000DAEAC File Offset: 0x000D90AC
		internal ReadOnlyCollection<ComplexType> IsOfTypes
		{
			get
			{
				return new ReadOnlyCollection<ComplexType>(new List<ComplexType>(this.m_isOfTypes.Values));
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x000DAEC3 File Offset: 0x000D90C3
		public override ReadOnlyCollection<PropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<PropertyMapping>(new List<PropertyMapping>(this.m_properties.Values));
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x000DAEDA File Offset: 0x000D90DA
		public override ReadOnlyCollection<ConditionPropertyMapping> Conditions
		{
			get
			{
				return new ReadOnlyCollection<ConditionPropertyMapping>(new List<ConditionPropertyMapping>(this.m_conditionProperties.Values));
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x000DAEF1 File Offset: 0x000D90F1
		internal ReadOnlyCollection<PropertyMapping> AllProperties
		{
			get
			{
				List<PropertyMapping> list = new List<PropertyMapping>();
				list.AddRange(this.m_properties.Values);
				list.AddRange(this.m_conditionProperties.Values);
				return new ReadOnlyCollection<PropertyMapping>(list);
			}
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000DAF1F File Offset: 0x000D911F
		internal void AddType(ComplexType type)
		{
			this.m_types.Add(type.FullName, type);
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000DAF33 File Offset: 0x000D9133
		internal void AddIsOfType(ComplexType type)
		{
			this.m_isOfTypes.Add(type.FullName, type);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000DAF47 File Offset: 0x000D9147
		public override void AddPropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Add(propertyMapping.Property.Name, propertyMapping);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x000DAF72 File Offset: 0x000D9172
		public override void RemovePropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Remove(propertyMapping.Property.Name);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000DAF9D File Offset: 0x000D919D
		public override void AddCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.AddConditionProperty(condition, delegate(EdmMember _)
			{
			});
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000DAFD7 File Offset: 0x000D91D7
		public override void RemoveCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.m_conditionProperties.Remove(condition.Property ?? condition.Column);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000DB007 File Offset: 0x000D9207
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this.m_properties.Values);
			MappingItem.SetReadOnly(this.m_conditionProperties.Values);
			base.SetReadOnly();
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x000DB030 File Offset: 0x000D9230
		internal void AddConditionProperty(ConditionPropertyMapping conditionPropertyMap, Action<EdmMember> duplicateMemberConditionError)
		{
			EdmProperty edmProperty = ((conditionPropertyMap.Property != null) ? conditionPropertyMap.Property : conditionPropertyMap.Column);
			if (!this.m_conditionProperties.ContainsKey(edmProperty))
			{
				this.m_conditionProperties.Add(edmProperty, conditionPropertyMap);
				return;
			}
			duplicateMemberConditionError(edmProperty);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x000DB078 File Offset: 0x000D9278
		internal ComplexType GetOwnerType(string memberName)
		{
			foreach (ComplexType complexType in this.m_types.Values)
			{
				EdmMember edmMember;
				if (complexType.Members.TryGetValue(memberName, false, out edmMember) && edmMember is EdmProperty)
				{
					return complexType;
				}
			}
			foreach (ComplexType complexType2 in this.m_isOfTypes.Values)
			{
				EdmMember edmMember2;
				if (complexType2.Members.TryGetValue(memberName, false, out edmMember2) && edmMember2 is EdmProperty)
				{
					return complexType2;
				}
			}
			return null;
		}

		// Token: 0x0400167C RID: 5756
		private readonly Dictionary<string, PropertyMapping> m_properties = new Dictionary<string, PropertyMapping>(StringComparer.Ordinal);

		// Token: 0x0400167D RID: 5757
		private readonly Dictionary<EdmProperty, ConditionPropertyMapping> m_conditionProperties = new Dictionary<EdmProperty, ConditionPropertyMapping>(EqualityComparer<EdmProperty>.Default);

		// Token: 0x0400167E RID: 5758
		private readonly Dictionary<string, ComplexType> m_types = new Dictionary<string, ComplexType>(StringComparer.Ordinal);

		// Token: 0x0400167F RID: 5759
		private readonly Dictionary<string, ComplexType> m_isOfTypes = new Dictionary<string, ComplexType>(StringComparer.Ordinal);
	}
}
