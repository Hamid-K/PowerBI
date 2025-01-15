using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x0200039B RID: 923
	internal class TypeMapper
	{
		// Token: 0x06001E6C RID: 7788 RVA: 0x0007C9DA File Offset: 0x0007ABDA
		public TypeMapper(bool serializing, XmlAttributeOverrides overrides, RdlSerializerOptions options)
		{
			this.serializing = serializing;
			this.attributesOverrides = overrides;
			this.includedTypes = new Hashtable();
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0007C9FB File Offset: 0x0007ABFB
		private void AssignStringIfNotEmpty(ref string dest, string src)
		{
			if (src != null && src != string.Empty)
			{
				dest = src;
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0007CA10 File Offset: 0x0007AC10
		public Type GetIncludedType(string name)
		{
			return (Type)this.includedTypes[name];
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0007CA24 File Offset: 0x0007AC24
		public TypeMapping GetTypeModel(MemberMapping member, object obj)
		{
			Type type = obj.GetType();
			if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				return this.ImportSpecialMapping(member, obj);
			}
			if (type.IsPrimitive || type.IsEnum || type == typeof(string))
			{
				return this.ImportPrimitiveMapping(member, obj);
			}
			if (type.IsArray || obj is ICollection)
			{
				return this.ImportArrayMapping(member, obj);
			}
			return this.ImportStructMapping(type, obj);
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0007CAA0 File Offset: 0x0007ACA0
		public TypeMapping GetTypeModel(Type baseType, object obj)
		{
			Type type = obj.GetType();
			if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				return this.ImportSpecialMapping(null, obj);
			}
			if (type.IsPrimitive || type.IsEnum || type == typeof(string))
			{
				return this.ImportPrimitiveMapping(null, obj);
			}
			if (type.IsArray || obj is ICollection)
			{
				return this.ImportArrayMapping(null, obj);
			}
			return this.ImportStructMapping(baseType, obj);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0007CB1B File Offset: 0x0007AD1B
		private SpecialMapping ImportSpecialMapping(MemberMapping mm, object obj)
		{
			return new SpecialMapping();
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0007CB24 File Offset: 0x0007AD24
		private PrimitiveMapping ImportPrimitiveMapping(MemberMapping member, object obj)
		{
			PrimitiveMapping primitiveMapping = new PrimitiveMapping();
			if (member != null && member.XmlAttributes != null && member.XmlAttributes.XmlElements != null && member.XmlAttributes.XmlElements.Count > 0)
			{
				primitiveMapping.DataType = member.XmlAttributes.XmlElements[0].DataType;
			}
			return primitiveMapping;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0007CB80 File Offset: 0x0007AD80
		private ArrayMapping ImportArrayMapping(MemberMapping mm, object obj)
		{
			ArrayMapping arrayMapping = new ArrayMapping();
			Type type = obj.GetType();
			arrayMapping.ElementTypes = new Hashtable();
			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				arrayMapping.ItemType = elementType;
				arrayMapping.ElementTypes.Add(elementType.Name, elementType);
			}
			else
			{
				this.GetCollectionElementTypes(type, arrayMapping, mm);
			}
			return arrayMapping;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0007CBDC File Offset: 0x0007ADDC
		private void GetCollectionElementTypes(Type type, ArrayMapping mapping, MemberMapping mm)
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
			ICollection collection = propertyInfo.GetCustomAttributes(typeof(XmlArrayItemAttribute), true);
			if (collection.Count == 0 && mm != null)
			{
				collection = mm.XmlAttributes.XmlArrayItems;
			}
			mapping.ElementTypes = new Hashtable();
			mapping.ElementTypes.Add(propertyInfo.PropertyType.Name, propertyInfo.PropertyType);
			foreach (object obj in collection)
			{
				XmlArrayItemAttribute xmlArrayItemAttribute = (XmlArrayItemAttribute)obj;
				if (!this.serializing)
				{
					mapping.ElementTypes.Add((xmlArrayItemAttribute.ElementName != string.Empty) ? xmlArrayItemAttribute.ElementName : xmlArrayItemAttribute.Type.Name, xmlArrayItemAttribute.Type);
				}
				else
				{
					mapping.ElementTypes.Add(xmlArrayItemAttribute.Type, (xmlArrayItemAttribute.ElementName != string.Empty) ? xmlArrayItemAttribute.ElementName : xmlArrayItemAttribute.Type.Name);
				}
			}
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0007CDE4 File Offset: 0x0007AFE4
		private void GetMemberName(XmlAttributes attributes, ref string tagName, ref string ns)
		{
			if (attributes.XmlArray != null)
			{
				if (attributes.XmlArray.ElementName != null && attributes.XmlArray.ElementName != string.Empty)
				{
					tagName = attributes.XmlArray.ElementName;
				}
				if (attributes.XmlArray.Namespace != null && attributes.XmlArray.Namespace != string.Empty)
				{
					ns = attributes.XmlArray.Namespace;
				}
			}
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

		// Token: 0x06001E76 RID: 7798 RVA: 0x0007CEF4 File Offset: 0x0007B0F4
		private void ImportMemberField(StructMapping mapping, MemberInfo field)
		{
			string name = field.Name;
			string empty = string.Empty;
			XmlAttributes xmlAttributes = this.attributesOverrides[mapping.ObjectType, field.Name];
			if (xmlAttributes == null)
			{
				xmlAttributes = new XmlAttributes(field);
			}
			if (!xmlAttributes.XmlIgnore)
			{
				this.GetMemberName(xmlAttributes, ref name, ref empty);
				MemberMapping memberMapping;
				if (field is FieldInfo)
				{
					memberMapping = new ReflectionFieldMapping(name, empty, (FieldInfo)field);
				}
				else
				{
					memberMapping = new ReflectionFieldMapping(name, empty, (PropertyInfo)field);
				}
				memberMapping.XmlAttributes = xmlAttributes;
				XmlParentElementAttribute xmlParentElement = xmlAttributes.XmlParentElement;
				if (xmlParentElement != null)
				{
					memberMapping.ParentElementName = xmlParentElement.ParentElementName;
					memberMapping.ParentElementNameSpace = xmlParentElement.XmlNameSpace;
					mapping.AddParentXmlElement(memberMapping.ParentElementName, memberMapping.ParentElementNameSpace);
				}
				mapping.elements[name, string.Empty] = memberMapping;
				mapping.members.Add(memberMapping);
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0007CFCC File Offset: 0x0007B1CC
		private void ImportTypeMembers(StructMapping mapping, object obj)
		{
			foreach (FieldInfo fieldInfo in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				this.ImportMemberField(mapping, fieldInfo);
			}
			foreach (object obj2 in TypeDescriptor.GetProperties(obj))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				this.ImportPropertyDescriptor(mapping, propertyDescriptor);
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0007D054 File Offset: 0x0007B254
		private void ImportPropertyDescriptor(StructMapping mapping, PropertyDescriptor prop)
		{
			this.ImportPropertyDescriptor(mapping, prop, null);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0007D060 File Offset: 0x0007B260
		private void ImportPropertyDescriptor(StructMapping mapping, PropertyDescriptor prop, FieldInfo realField)
		{
			bool flag = false;
			Type componentType = prop.ComponentType;
			XmlAttributes xmlAttributes = this.attributesOverrides[mapping.ObjectType, prop.Name];
			if (xmlAttributes == null)
			{
				xmlAttributes = new XmlAttributes();
				foreach (object obj in prop.Attributes)
				{
					Type type = obj.GetType();
					if (type == typeof(XmlIgnoreAttribute))
					{
						xmlAttributes.XmlIgnore = true;
					}
					else if (type == typeof(DefaultValueAttribute))
					{
						xmlAttributes.XmlDefaultValue = ((DefaultValueAttribute)obj).Value;
					}
					else if (type == typeof(XmlParentElementAttribute))
					{
						xmlAttributes.XmlParentElement = (XmlParentElementAttribute)obj;
					}
					else if (type == typeof(XmlElementAttribute) || type == typeof(XmlElementClassAttribute))
					{
						XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj;
						if (xmlElementAttribute.Type == null)
						{
							xmlAttributes.XmlElements.Add(xmlElementAttribute);
						}
						else
						{
							PropertyInfo property = prop.ComponentType.GetProperty(prop.Name);
							Attribute[] array;
							if (type == typeof(XmlElementAttribute))
							{
								array = Attribute.GetCustomAttributes(property, type, false);
							}
							else
							{
								array = Attribute.GetCustomAttributes(property.PropertyType, type, false);
							}
							foreach (XmlElementAttribute xmlElementAttribute2 in array)
							{
								xmlAttributes.XmlElements.Add(xmlElementAttribute2);
							}
							flag = true;
						}
					}
					else if (type == typeof(XmlArrayAttribute))
					{
						xmlAttributes.XmlArray = (XmlArrayAttribute)obj;
					}
					else if (type == typeof(XmlArrayItemAttribute))
					{
						foreach (XmlArrayItemAttribute xmlArrayItemAttribute in Attribute.GetCustomAttributes(prop.ComponentType.GetProperty(prop.Name), type, false))
						{
							xmlAttributes.XmlArrayItems.Add(xmlArrayItemAttribute);
						}
					}
				}
			}
			if (xmlAttributes.XmlIgnore)
			{
				return;
			}
			string name = prop.Name;
			string text = string.Empty;
			if (prop.DesignTimeOnly)
			{
				text = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";
			}
			if (!flag)
			{
				this.GetMemberName(xmlAttributes, ref name, ref text);
			}
			MemberMapping memberMapping;
			if (realField != null)
			{
				memberMapping = new ReflectionFieldMapping(name, text, realField);
			}
			else
			{
				memberMapping = new PropertyMapping(name, text, prop);
			}
			memberMapping.XmlAttributes = xmlAttributes;
			mapping.elements[name, text] = memberMapping;
			mapping.members.Add(memberMapping);
			if (flag)
			{
				mapping.UseTypeName = name;
				mapping.UseTypeNameSpace = text;
			}
			XmlParentElementAttribute xmlParentElement = xmlAttributes.XmlParentElement;
			if (xmlParentElement != null)
			{
				string parentElementName = xmlParentElement.ParentElementName;
				string xmlNameSpace = xmlParentElement.XmlNameSpace;
				memberMapping.ParentElementName = parentElementName;
				memberMapping.ParentElementNameSpace = xmlNameSpace;
				mapping.AddParentXmlElement(parentElementName, xmlNameSpace);
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0007D37C File Offset: 0x0007B57C
		private StructMapping ImportStructMapping(Type objBaseType, object obj)
		{
			StructMapping structMapping = new StructMapping(obj.GetType());
			object[] customAttributes = obj.GetType().GetCustomAttributes(typeof(XmlElementAttribute), true);
			if (customAttributes.Length != 0)
			{
				this.AssignStringIfNotEmpty(ref structMapping.Namespace, ((XmlElementAttribute)customAttributes[0]).Namespace);
				this.AssignStringIfNotEmpty(ref structMapping.ElementName, ((XmlElementAttribute)customAttributes[0]).ElementName);
			}
			object[] customAttributes2 = obj.GetType().GetCustomAttributes(typeof(XmlRootAttribute), true);
			if (customAttributes2.Length != 0)
			{
				this.AssignStringIfNotEmpty(ref structMapping.Namespace, ((XmlRootAttribute)customAttributes2[0]).Namespace);
				this.AssignStringIfNotEmpty(ref structMapping.ElementName, ((XmlRootAttribute)customAttributes2[0]).ElementName);
			}
			if (structMapping.Namespace == null)
			{
				structMapping.Namespace = string.Empty;
			}
			if (structMapping.ElementName == null)
			{
				structMapping.ElementName = objBaseType.Name;
			}
			this.ImportTypeMembers(structMapping, obj);
			return structMapping;
		}

		// Token: 0x04000CDE RID: 3294
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x04000CDF RID: 3295
		private Hashtable includedTypes;

		// Token: 0x04000CE0 RID: 3296
		private bool serializing;
	}
}
