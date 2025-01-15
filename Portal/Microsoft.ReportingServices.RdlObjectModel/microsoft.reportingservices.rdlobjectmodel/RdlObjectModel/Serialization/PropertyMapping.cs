using System;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E3 RID: 739
	public class PropertyMapping : MemberMapping
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00035C12 File Offset: 0x00033E12
		public PropertyInfo Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00035C1A File Offset: 0x00033E1A
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x00035C22 File Offset: 0x00033E22
		public int Index
		{
			get
			{
				return this.m_index;
			}
			set
			{
				this.m_index = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00035C2B File Offset: 0x00033E2B
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00035C33 File Offset: 0x00033E33
		public PropertyMapping.PropertyTypeCode TypeCode
		{
			get
			{
				return this.m_typeCode;
			}
			set
			{
				this.m_typeCode = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00035C3C File Offset: 0x00033E3C
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x00035C44 File Offset: 0x00033E44
		public IPropertyDefinition Definition
		{
			get
			{
				return this.m_definition;
			}
			set
			{
				this.m_definition = value;
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00035C4D File Offset: 0x00033E4D
		public PropertyMapping(Type propertyType, string name, string ns, PropertyInfo property)
			: base(propertyType, name, ns, !property.CanWrite)
		{
			this.m_property = property;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00035C6C File Offset: 0x00033E6C
		public override void SetValue(object obj, object value)
		{
			if (this.m_typeCode == PropertyMapping.PropertyTypeCode.None)
			{
				this.m_property.SetValue(obj, value, null);
				return;
			}
			IPropertyStore propertyStore = ((ReportObject)obj).PropertyStore;
			if (this.m_definition != null)
			{
				this.m_definition.Validate(obj, value);
			}
			switch (this.m_typeCode)
			{
			case PropertyMapping.PropertyTypeCode.ContainedObject:
				propertyStore.SetObject(this.m_index, (IContainedObject)value);
				return;
			case PropertyMapping.PropertyTypeCode.Boolean:
				propertyStore.SetBoolean(this.m_index, (bool)value);
				return;
			case PropertyMapping.PropertyTypeCode.Integer:
			case PropertyMapping.PropertyTypeCode.Enum:
				propertyStore.SetInteger(this.m_index, (int)value);
				return;
			case PropertyMapping.PropertyTypeCode.Size:
				propertyStore.SetSize(this.m_index, (ReportSize)value);
				return;
			default:
				propertyStore.SetObject(this.m_index, value);
				return;
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00035D30 File Offset: 0x00033F30
		public override object GetValue(object obj)
		{
			if (this.m_typeCode == PropertyMapping.PropertyTypeCode.None)
			{
				return this.m_property.GetValue(obj, null);
			}
			IPropertyStore propertyStore = ((ReportObject)obj).PropertyStore;
			switch (this.m_typeCode)
			{
			case PropertyMapping.PropertyTypeCode.Boolean:
				return propertyStore.GetBoolean(this.m_index);
			case PropertyMapping.PropertyTypeCode.Integer:
				return propertyStore.GetInteger(this.m_index);
			case PropertyMapping.PropertyTypeCode.Size:
				return propertyStore.GetSize(this.m_index);
			case PropertyMapping.PropertyTypeCode.Enum:
			{
				int integer = propertyStore.GetInteger(this.m_index);
				return Enum.ToObject(this.Type, integer);
			}
			case PropertyMapping.PropertyTypeCode.ValueType:
			{
				object obj2 = propertyStore.GetObject(this.m_index);
				if (obj2 == null)
				{
					obj2 = Activator.CreateInstance(this.Type);
				}
				return obj2;
			}
			default:
				return propertyStore.GetObject(this.m_index);
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00035E00 File Offset: 0x00034000
		public override bool HasValue(object obj)
		{
			if (this.m_typeCode == PropertyMapping.PropertyTypeCode.None)
			{
				return this.m_property.GetValue(obj, null) != null;
			}
			IPropertyStore propertyStore = ((ReportObject)obj).PropertyStore;
			switch (this.m_typeCode)
			{
			case PropertyMapping.PropertyTypeCode.Boolean:
				return propertyStore.ContainsBoolean(this.m_index);
			case PropertyMapping.PropertyTypeCode.Integer:
			case PropertyMapping.PropertyTypeCode.Enum:
				return propertyStore.ContainsInteger(this.m_index);
			case PropertyMapping.PropertyTypeCode.Size:
				return propertyStore.ContainsSize(this.m_index);
			default:
				return propertyStore.ContainsObject(this.m_index);
			}
		}

		// Token: 0x04000708 RID: 1800
		private readonly PropertyInfo m_property;

		// Token: 0x04000709 RID: 1801
		private int m_index;

		// Token: 0x0400070A RID: 1802
		private PropertyMapping.PropertyTypeCode m_typeCode;

		// Token: 0x0400070B RID: 1803
		private IPropertyDefinition m_definition;

		// Token: 0x02000419 RID: 1049
		public enum PropertyTypeCode
		{
			// Token: 0x040007E7 RID: 2023
			None,
			// Token: 0x040007E8 RID: 2024
			Object,
			// Token: 0x040007E9 RID: 2025
			ContainedObject,
			// Token: 0x040007EA RID: 2026
			Boolean,
			// Token: 0x040007EB RID: 2027
			Integer,
			// Token: 0x040007EC RID: 2028
			Size,
			// Token: 0x040007ED RID: 2029
			Enum,
			// Token: 0x040007EE RID: 2030
			ValueType
		}
	}
}
