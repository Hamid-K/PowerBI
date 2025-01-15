using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002DF RID: 735
	public static class TypeMapper
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0003518C File Offset: 0x0003338C
		public static TypeMapping GetTypeMapping(Type type)
		{
			TypeMapper.m_lock.AcquireReaderLock(-1);
			try
			{
				if (TypeMapper.m_mappings.ContainsKey(type))
				{
					return TypeMapper.m_mappings[type];
				}
			}
			finally
			{
				TypeMapper.m_lock.ReleaseReaderLock();
			}
			TypeMapping typeMapping;
			if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				typeMapping = TypeMapper.ImportSpecialMapping(type);
			}
			else if (TypeMapper.IsPrimitiveType(type))
			{
				typeMapping = TypeMapper.ImportPrimitiveMapping(type);
			}
			else if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
			{
				typeMapping = TypeMapper.ImportArrayMapping(type);
			}
			else
			{
				typeMapping = TypeMapper.ImportStructMapping(type);
			}
			TypeMapper.m_lock.AcquireWriterLock(-1);
			try
			{
				TypeMapper.m_mappings[type] = typeMapping;
			}
			finally
			{
				TypeMapper.m_lock.ReleaseWriterLock();
			}
			return typeMapping;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00035268 File Offset: 0x00033468
		public static bool IsPrimitiveType(Type type)
		{
			return type.IsEnum || type.IsPrimitive || type == typeof(string) || type == typeof(Guid) || type == typeof(DateTime);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000352BC File Offset: 0x000334BC
		private static SpecialMapping ImportSpecialMapping(Type type)
		{
			SpecialMapping specialMapping = new SpecialMapping(type);
			foreach (object obj in ((IEnumerable)type.GetCustomAttributes(typeof(XmlElementClassAttribute), true)))
			{
				XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj;
				if (xmlElementAttribute.Type == null || type == xmlElementAttribute.Type)
				{
					if (xmlElementAttribute.Namespace != null)
					{
						specialMapping.Namespace = xmlElementAttribute.Namespace;
					}
					if (xmlElementAttribute.ElementName != null)
					{
						specialMapping.Name = xmlElementAttribute.ElementName;
						break;
					}
					break;
				}
			}
			return specialMapping;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00035368 File Offset: 0x00033568
		private static PrimitiveMapping ImportPrimitiveMapping(Type type)
		{
			return new PrimitiveMapping(type);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00035370 File Offset: 0x00033570
		private static ArrayMapping ImportArrayMapping(Type type)
		{
			ArrayMapping arrayMapping = new ArrayMapping(type);
			arrayMapping.ElementTypes = new Dictionary<string, Type>();
			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				arrayMapping.ItemType = elementType;
				arrayMapping.ElementTypes.Add(elementType.Name, elementType);
			}
			else
			{
				TypeMapper.GetCollectionElementTypes(type, arrayMapping);
			}
			return arrayMapping;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000353C4 File Offset: 0x000335C4
		private static void GetCollectionElementTypes(Type type, ArrayMapping mapping)
		{
			PropertyInfo propertyInfo = null;
			Type type2 = type;
			while (type2 != null)
			{
				MemberInfo[] defaultMembers = type.GetDefaultMembers();
				if (defaultMembers != null)
				{
					for (int i = 0; i < defaultMembers.Length; i++)
					{
						if (defaultMembers[i] is PropertyInfo)
						{
							PropertyInfo propertyInfo2 = (PropertyInfo)defaultMembers[i];
							if (propertyInfo2.CanRead && propertyInfo2.GetGetMethod().GetParameters().Length == 1 && (propertyInfo == null || propertyInfo.PropertyType == typeof(object)))
							{
								propertyInfo = propertyInfo2;
							}
						}
					}
				}
				if (propertyInfo != null)
				{
					break;
				}
				type2 = type2.BaseType;
			}
			if (propertyInfo == null)
			{
				throw new Exception("NoDefaultAccessors");
			}
			if (type.GetMethod("Add", new Type[] { propertyInfo.PropertyType }) == null)
			{
				throw new Exception("NoAddMethod");
			}
			mapping.ItemType = propertyInfo.PropertyType;
			IList customAttributes = propertyInfo.PropertyType.GetCustomAttributes(typeof(XmlElementClassAttribute), true);
			if (customAttributes != null && customAttributes.Count > 0)
			{
				using (IEnumerator enumerator = customAttributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlElementClassAttribute xmlElementClassAttribute = (XmlElementClassAttribute)obj;
						mapping.ElementTypes.Add((xmlElementClassAttribute.ElementName != string.Empty) ? xmlElementClassAttribute.ElementName : xmlElementClassAttribute.Type.Name, xmlElementClassAttribute.Type);
					}
					return;
				}
			}
			string name = propertyInfo.PropertyType.Name;
			mapping.ElementTypes.Add(name, propertyInfo.PropertyType);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00035574 File Offset: 0x00033774
		private static void GetMemberName(XmlAttributes attributes, ref string tagName, ref string ns)
		{
			if (attributes.XmlElements.Count > 0)
			{
				if (attributes.XmlElements[0].ElementName != null && attributes.XmlElements[0].ElementName != string.Empty)
				{
					tagName = attributes.XmlElements[0].ElementName;
				}
				if (attributes.XmlElements[0].Namespace != null && attributes.XmlElements[0].Namespace != string.Empty)
				{
					ns = attributes.XmlElements[0].Namespace;
				}
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00035618 File Offset: 0x00033818
		private static void ImportTypeMembers(StructMapping mapping, Type type)
		{
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				TypeMapper.ImportPropertyInfo(mapping, propertyInfo);
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00035648 File Offset: 0x00033848
		private static void ImportPropertyInfo(StructMapping mapping, PropertyInfo prop)
		{
			Type type = prop.PropertyType;
			bool flag = false;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				flag = true;
				type = type.GetGenericArguments()[0];
			}
			bool flag2 = false;
			XmlAttributes xmlAttributes = new XmlAttributes();
			object[] customAttributes = type.GetCustomAttributes(true);
			object[] customAttributes2 = prop.GetCustomAttributes(true);
			bool flag3 = false;
			int num = customAttributes.Length;
			Array.Resize<object>(ref customAttributes, num + customAttributes2.Length);
			customAttributes2.CopyTo(customAttributes, num);
			foreach (object obj in customAttributes)
			{
				Type type2 = obj.GetType();
				if (type2 == typeof(XmlIgnoreAttribute))
				{
					return;
				}
				if (typeof(DefaultValueAttribute).IsAssignableFrom(type2))
				{
					xmlAttributes.XmlDefaultValue = ((DefaultValueAttribute)obj).Value;
				}
				else if (typeof(XmlElementAttribute).IsAssignableFrom(type2))
				{
					XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj;
					xmlAttributes.XmlElements.Add(xmlElementAttribute);
					if (xmlElementAttribute.Type != null)
					{
						if (string.IsNullOrEmpty(xmlElementAttribute.ElementName))
						{
							type = xmlElementAttribute.Type;
						}
						else
						{
							flag2 = true;
						}
					}
				}
				else if (type2 == typeof(XmlArrayItemAttribute))
				{
					XmlArrayItemAttribute xmlArrayItemAttribute = (XmlArrayItemAttribute)obj;
					int num2 = 0;
					while (num2 < xmlAttributes.XmlArrayItems.Count && xmlAttributes.XmlArrayItems[num2].NestingLevel <= xmlArrayItemAttribute.NestingLevel)
					{
						num2++;
					}
					xmlAttributes.XmlArrayItems.Insert(num2, xmlArrayItemAttribute);
				}
				else if (typeof(XmlAttributeAttribute).IsAssignableFrom(type2))
				{
					xmlAttributes.XmlAttribute = (XmlAttributeAttribute)obj;
				}
				else if (type2 == typeof(ValidValuesAttribute) || type2 == typeof(ValidEnumValuesAttribute))
				{
					flag3 = true;
				}
			}
			string name = prop.Name;
			string empty = string.Empty;
			if (!flag2)
			{
				TypeMapper.GetMemberName(xmlAttributes, ref name, ref empty);
			}
			if (mapping.GetElement(name, empty) != null || mapping.GetAttribute(name, empty) != null)
			{
				return;
			}
			PropertyMapping propertyMapping = new PropertyMapping(type, name, empty, prop);
			propertyMapping.XmlAttributes = xmlAttributes;
			mapping.Members.Add(propertyMapping);
			if (xmlAttributes.XmlAttribute != null)
			{
				if (xmlAttributes.XmlAttribute is XmlChildAttributeAttribute)
				{
					mapping.AddChildAttribute(propertyMapping);
				}
				else
				{
					mapping.Attributes[name, empty] = propertyMapping;
				}
			}
			else
			{
				mapping.Elements[name, empty] = propertyMapping;
				if (flag2)
				{
					mapping.AddUseTypeInfo(name, empty);
				}
			}
			Type declaringType = prop.DeclaringType;
			if (declaringType.IsSubclassOf(typeof(ReportObject)))
			{
				Type type3 = declaringType.Assembly.GetType(declaringType.FullName + "+Definition+Properties", false);
				FieldInfo field;
				if (type3 != null && type3.IsEnum && (field = type3.GetField(prop.Name)) != null)
				{
					propertyMapping.Index = (int)field.GetRawConstantValue();
					propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Object;
					if (flag)
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Object;
					}
					else if (type.IsSubclassOf(typeof(IContainedObject)))
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.ContainedObject;
					}
					else if (type == typeof(bool))
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Boolean;
					}
					else if (type == typeof(int))
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Integer;
					}
					else if (type == typeof(ReportSize))
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Size;
					}
					else if (type.IsEnum)
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Enum;
					}
					else if (type.IsValueType)
					{
						propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.ValueType;
					}
					if (flag3)
					{
						type3 = declaringType.Assembly.GetType(declaringType.FullName + "+Definition", false);
						propertyMapping.Definition = (IPropertyDefinition)type3.InvokeMember("GetProperty", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod, null, null, new object[] { propertyMapping.Index }, CultureInfo.InvariantCulture);
					}
				}
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00035A70 File Offset: 0x00033C70
		private static StructMapping ImportStructMapping(Type type)
		{
			StructMapping structMapping = new StructMapping(type);
			foreach (object obj in ((IEnumerable)type.GetCustomAttributes(typeof(XmlElementClassAttribute), true)))
			{
				XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj;
				if (xmlElementAttribute.Type == null || type == xmlElementAttribute.Type)
				{
					if (xmlElementAttribute.Namespace != null)
					{
						structMapping.Namespace = xmlElementAttribute.Namespace;
					}
					if (xmlElementAttribute.ElementName != null)
					{
						structMapping.Name = xmlElementAttribute.ElementName;
						break;
					}
					break;
				}
			}
			TypeMapper.ImportTypeMembers(structMapping, type);
			structMapping.ResolveChildAttributes();
			return structMapping;
		}

		// Token: 0x040006FE RID: 1790
		private static readonly Dictionary<Type, TypeMapping> m_mappings = new Dictionary<Type, TypeMapping>();

		// Token: 0x040006FF RID: 1791
		private static readonly ReaderWriterLock m_lock = new ReaderWriterLock();
	}
}
