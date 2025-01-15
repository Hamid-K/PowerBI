using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200000D RID: 13
	internal sealed class TypeMapper
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000040CB File Offset: 0x000022CB
		public TypeMapper(XmlAttributeOverrides overrides)
			: this(overrides, DesignXmlSerializerOptions.Default, null)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000040D6 File Offset: 0x000022D6
		public TypeMapper(XmlAttributeOverrides overrides, DesignXmlSerializerOptions options, object userSerializationOptions)
		{
			this.attributesOverrides = overrides;
			this.options = options;
			this.userSerializationOptions = userSerializationOptions;
			this.includedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
			this.IncludeStandardTypes();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000410C File Offset: 0x0000230C
		private void IncludeStandardTypes()
		{
			this.includedTypes.Add("xsd:string", typeof(string));
			this.includedTypes.Add("xsd:boolean", typeof(bool));
			this.includedTypes.Add("xsd:decimal", typeof(decimal));
			this.includedTypes.Add("xsd:float", typeof(float));
			this.includedTypes.Add("xsd:double", typeof(double));
			this.includedTypes.Add("xsd:byte", typeof(sbyte));
			this.includedTypes.Add("xsd:unsignedByte", typeof(byte));
			this.includedTypes.Add("xsd:short", typeof(short));
			this.includedTypes.Add("xsd:unsignedShort", typeof(ushort));
			this.includedTypes.Add("xsd:int", typeof(int));
			this.includedTypes.Add("xsd:unsignedInt", typeof(uint));
			this.includedTypes.Add("xsd:long", typeof(long));
			this.includedTypes.Add("xsd:unsignedLong", typeof(ulong));
			this.includedTypes.Add("xsd:dateTime", typeof(DateTime));
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004285 File Offset: 0x00002485
		private bool IncludeDesignTimeProperties
		{
			get
			{
				return (this.options & DesignXmlSerializerOptions.IgnoreDesignTimeProperties) == DesignXmlSerializerOptions.Default;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004292 File Offset: 0x00002492
		private bool IncludeRuntimeProperties
		{
			get
			{
				return (this.options & DesignXmlSerializerOptions.IgnoreRuntimeProperties) == DesignXmlSerializerOptions.Default;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000042A0 File Offset: 0x000024A0
		private bool UseNamespaces
		{
			get
			{
				return (this.options & DesignXmlSerializerOptions.DoNotUseNamespaces) == DesignXmlSerializerOptions.Default;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000042AE File Offset: 0x000024AE
		private void AssignStringIfNotEmpty(ref string dest, string src)
		{
			if (src != null && src != string.Empty)
			{
				dest = src;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000042C4 File Offset: 0x000024C4
		public Type GetIncludedType(string name, XmlReader reader)
		{
			Type type;
			if (name.IndexOf(":", StringComparison.Ordinal) >= 0)
			{
				if (name.Count((char x) => x == ':') == 1)
				{
					string[] array = name.Split(new char[] { ':' });
					string text = reader.LookupNamespace(array[0]);
					if (string.Equals(text, "http://www.w3.org/2001/XMLSchema") && this.includedTypes.TryGetValue("xsd:" + array[1], out type))
					{
						return type;
					}
					if (string.Equals(text, "http://schemas.microsoft.com/analysisservices/2010/engine/200/200") && this.includedTypes.TryGetValue(array[1], out type))
					{
						return type;
					}
					return null;
				}
			}
			if (this.includedTypes.TryGetValue(name, out type))
			{
				return type;
			}
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004388 File Offset: 0x00002588
		private bool IsSpecialMapping(Type type)
		{
			return typeof(IXmlSerializable).IsAssignableFrom(type) || typeof(XmlNode).IsAssignableFrom(type);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043AE File Offset: 0x000025AE
		public static bool IsPrimitiveType(Type type)
		{
			return Type.GetTypeCode(type) != TypeCode.Object || type.IsEnum || type == typeof(TimeSpan);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000043D4 File Offset: 0x000025D4
		public TypeMapping GetTypeModel(MemberMapping member, object obj)
		{
			Type type = obj.GetType();
			if (this.IsSpecialMapping(type))
			{
				return this.ImportSpecialMapping(member, obj);
			}
			if (TypeMapper.IsPrimitiveType(type))
			{
				return this.ImportPrimitiveMapping(member, obj);
			}
			if (type.IsArray || obj is ICollection)
			{
				return this.ImportArrayMapping(member, obj);
			}
			return this.ImportStructMapping(member.MemberType, obj, false);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004432 File Offset: 0x00002632
		public TypeMapping GetTypeModel(Type baseType, object obj)
		{
			return this.GetTypeModel(baseType, obj, false);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004440 File Offset: 0x00002640
		public TypeMapping GetTypeModel(Type baseType, object obj, bool isRoot)
		{
			Type type = obj.GetType();
			if (this.IsSpecialMapping(type))
			{
				return this.ImportSpecialMapping(null, obj);
			}
			if (type.IsValueType || type == typeof(string))
			{
				return this.ImportPrimitiveMapping(null, obj);
			}
			if (type.IsArray || obj is ICollection)
			{
				return this.ImportArrayMapping(null, obj);
			}
			if (type.IsClass || type.IsInterface)
			{
				return this.ImportStructMapping(baseType, obj, isRoot);
			}
			return null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000044BD File Offset: 0x000026BD
		public TimeSpan ConvertToTimeSpan(string value)
		{
			if ((this.options & DesignXmlSerializerOptions.BinaryXml) != DesignXmlSerializerOptions.Default)
			{
				return new TimeSpan(long.Parse(value));
			}
			return XmlConvert.ToTimeSpan(value);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000044DF File Offset: 0x000026DF
		private SpecialMapping ImportSpecialMapping(MemberMapping mm, object obj)
		{
			return new SpecialMapping();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000044E8 File Offset: 0x000026E8
		private PrimitiveMapping ImportPrimitiveMapping(MemberMapping member, object obj)
		{
			PrimitiveMapping primitiveMapping = new PrimitiveMapping();
			if (member != null && member.XmlAttributes != null && member.XmlAttributes.XmlElements != null && member.XmlAttributes.XmlElements.Count > 0)
			{
				primitiveMapping.DataType = member.XmlAttributes.XmlElements[0].DataType;
			}
			if (member != null && null != member.MemberType && member.MemberType.IsEnum)
			{
				this.IncludeType(member.MemberType);
			}
			return primitiveMapping;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004570 File Offset: 0x00002770
		private ArrayMapping ImportArrayMapping(MemberMapping mm, object obj)
		{
			ArrayMapping arrayMapping = new ArrayMapping();
			Type type = obj.GetType();
			Type type2 = null;
			string text = null;
			if (mm != null && mm.XmlAttributes != null && mm.XmlAttributes.XmlArrayItems.Count > 0)
			{
				XmlArrayItemAttribute xmlArrayItemAttribute = mm.XmlAttributes.XmlArrayItems[0];
				TypeMapper.TestAndAssignString(ref text, xmlArrayItemAttribute.ElementName);
				type2 = xmlArrayItemAttribute.Type;
				arrayMapping.IsMemberNullable = xmlArrayItemAttribute.IsNullable;
			}
			if (type2 == null)
			{
				if (type.IsArray)
				{
					type2 = type.GetElementType();
				}
				else
				{
					type2 = TypeMapper.GetCollectionElementType(type);
				}
			}
			if (text == null)
			{
				if (type2 == typeof(object))
				{
					text = "anyType";
				}
				else
				{
					text = type2.Name;
				}
			}
			arrayMapping.MemberType = type2;
			arrayMapping.MemberName = text;
			if (arrayMapping.ElementName == null)
			{
				if (type2 == typeof(object))
				{
					arrayMapping.ElementName = "ArrayOfAnyType";
				}
				else
				{
					arrayMapping.ElementName = obj.GetType().Name;
				}
			}
			return arrayMapping;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004670 File Offset: 0x00002870
		private static Type GetCollectionElementType(Type type)
		{
			if (typeof(IDictionary).IsAssignableFrom(type))
			{
				throw new XmlSerializationException(SR.Serialization_IDictionaryNotSupported);
			}
			PropertyInfo propertyInfo = null;
			MemberInfo[] defaultMembers = type.GetDefaultMembers();
			if (defaultMembers != null)
			{
				Type type2 = null;
				for (int i = 0; i < defaultMembers.Length; i++)
				{
					if (defaultMembers[i] is PropertyInfo)
					{
						PropertyInfo propertyInfo2 = (PropertyInfo)defaultMembers[i];
						if (propertyInfo2.CanRead)
						{
							ParameterInfo[] parameters = propertyInfo2.GetGetMethod().GetParameters();
							if (parameters.Length == 1 && !(parameters[0].ParameterType != typeof(int)) && (!(type2 != null) || !propertyInfo2.DeclaringType.IsAssignableFrom(type2)))
							{
								propertyInfo = propertyInfo2;
								type2 = propertyInfo2.DeclaringType;
							}
						}
					}
				}
			}
			if (propertyInfo == null)
			{
				throw new XmlSerializationException(SR.Serialization_NoDefaultAccessors);
			}
			if (type.GetMethod("Add", new Type[] { propertyInfo.PropertyType }) == null)
			{
				throw new XmlSerializationException(SR.Serialization_NoAddMethod);
			}
			return propertyInfo.PropertyType;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000476C File Offset: 0x0000296C
		private void GetMemberName(XmlAttributes attributes, ref string tagName, ref string ns)
		{
			if (attributes.XmlArray != null)
			{
				if (attributes.XmlArray.ElementName != null && attributes.XmlArray.ElementName != string.Empty)
				{
					tagName = attributes.XmlArray.ElementName;
				}
				if (this.UseNamespaces && attributes.XmlArray.Namespace != null && attributes.XmlArray.Namespace != string.Empty)
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
				if (this.UseNamespaces && attributes.XmlElements[0].Namespace != null && attributes.XmlElements[0].Namespace != string.Empty)
				{
					ns = attributes.XmlElements[0].Namespace;
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000488A File Offset: 0x00002A8A
		public bool IsKnownType(Type type)
		{
			return this.includedTypes.ContainsKey(type.Name);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000048A0 File Offset: 0x00002AA0
		public void IncludeType(Type type)
		{
			if (this.includedTypes.ContainsKey(type.Name))
			{
				return;
			}
			this.includedTypes.Add(type.Name, type);
			object[] array = new object[0];
			Dictionary<Type, object[]> dictionary = TypeMapper.typeXmlIncludeAttributeCache;
			lock (dictionary)
			{
				if (!TypeMapper.typeXmlIncludeAttributeCache.ContainsKey(type))
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(XmlIncludeAttribute), true);
					TypeMapper.typeXmlIncludeAttributeCache.Add(type, customAttributes);
				}
				TypeMapper.typeXmlIncludeAttributeCache.TryGetValue(type, out array);
			}
			foreach (XmlIncludeAttribute xmlIncludeAttribute in array)
			{
				this.IncludeType(xmlIncludeAttribute.Type);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004970 File Offset: 0x00002B70
		private MemberMapping ImportMemberMapping(MemberInfo field, ref string tagName, ref string ns)
		{
			XmlAttributes xmlAttributes = this.attributesOverrides[field.DeclaringType, field.Name];
			if (xmlAttributes == null)
			{
				xmlAttributes = new XmlAttributes(field);
			}
			if (xmlAttributes.XmlIgnore)
			{
				return null;
			}
			this.GetMemberName(xmlAttributes, ref tagName, ref ns);
			MemberMapping memberMapping;
			if (field is FieldInfo)
			{
				memberMapping = new ReflectionFieldMapping(tagName, ns, (FieldInfo)field);
			}
			else
			{
				memberMapping = new ReflectionFieldMapping(tagName, ns, (PropertyInfo)field);
			}
			memberMapping.XmlAttributes = xmlAttributes;
			return memberMapping;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000049E4 File Offset: 0x00002BE4
		private void ImportMemberField(StructMapping mapping, MemberInfo field)
		{
			string name = field.Name;
			string empty = string.Empty;
			MemberMapping memberMapping = this.ImportMemberMapping(field, ref name, ref empty);
			if (memberMapping != null)
			{
				mapping.AddMember(name, empty, memberMapping);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004A18 File Offset: 0x00002C18
		private void ImportTypeMembers(StructMapping mapping, Type type)
		{
			if (type.BaseType != null)
			{
				this.ImportTypeMembers(mapping, type.BaseType);
			}
			foreach (FieldInfo fieldInfo in this.GetFieldInfos(type, TypeMapper.typeFieldInfoCache))
			{
				this.ImportMemberField(mapping, fieldInfo);
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
			PropertyInfo[] propertyInfos = this.GetPropertyInfos(type, TypeMapper.typePublicPropInfoCache, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] propertyInfos2 = this.GetPropertyInfos(type, TypeMapper.typePrivatePropInfoCache, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
			List<PropertyInfo> list = new List<PropertyInfo>();
			SortedDictionary<int, PropertyInfo> sortedDictionary = null;
			Dictionary<Type, Dictionary<string, int>> dictionary = TypeMapper.typePropOrderCache;
			lock (dictionary)
			{
				if (!TypeMapper.typePropOrderCache.ContainsKey(type))
				{
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>(StringComparer.Ordinal);
					foreach (PropertyInfo propertyInfo in propertyInfos)
					{
						object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(PropertyOrderAttribute), false);
						PropertyOrderAttribute propertyOrderAttribute = ((customAttributes.Length != 0) ? ((PropertyOrderAttribute)customAttributes[0]) : null);
						if (propertyOrderAttribute != null)
						{
							dictionary2.Add(propertyInfo.Name, propertyOrderAttribute.Order);
						}
					}
					TypeMapper.typePropOrderCache.Add(type, dictionary2);
				}
				foreach (PropertyInfo propertyInfo2 in propertyInfos)
				{
					int num = 0;
					if (TypeMapper.typePropOrderCache[type].TryGetValue(propertyInfo2.Name, out num))
					{
						if (sortedDictionary == null)
						{
							sortedDictionary = new SortedDictionary<int, PropertyInfo>();
						}
						sortedDictionary.Add(num, propertyInfo2);
					}
					else
					{
						list.Add(propertyInfo2);
					}
				}
			}
			if (sortedDictionary != null)
			{
				list.InsertRange(0, sortedDictionary.Values);
			}
			foreach (PropertyInfo propertyInfo3 in propertyInfos2)
			{
				if (this.GetPropertyInfoCustomAttributes(propertyInfo3, TypeMapper.propertyInfoXmlIgnoreOnReadAttributeCache).Length != 0)
				{
					list.Add(propertyInfo3);
				}
			}
			foreach (PropertyInfo propertyInfo4 in list)
			{
				if (propertyInfo4.CanRead)
				{
					PropertyDescriptor propertyDescriptor = properties[propertyInfo4.Name];
					if (propertyDescriptor == null || (propertyInfo4.CanWrite && propertyDescriptor.IsReadOnly))
					{
						this.ImportMemberField(mapping, propertyInfo4);
					}
					else
					{
						this.ImportPropertyDescriptor(mapping, propertyDescriptor);
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004C70 File Offset: 0x00002E70
		private void ImportPropertyDescriptor(StructMapping mapping, PropertyDescriptor prop)
		{
			this.ImportPropertyDescriptor(mapping, prop, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004C7C File Offset: 0x00002E7C
		private void ImportPropertyDescriptor(StructMapping mapping, PropertyDescriptor prop, FieldInfo realField)
		{
			Type componentType = prop.ComponentType;
			XmlAttributes xmlAttributes = this.attributesOverrides[componentType, prop.Name];
			if (xmlAttributes == null)
			{
				xmlAttributes = new XmlAttributes();
				foreach (object obj in prop.Attributes)
				{
					if (obj.GetType() == typeof(XmlIgnoreAttribute))
					{
						xmlAttributes.XmlIgnore = true;
					}
					if (obj.GetType() == typeof(DefaultValueAttribute))
					{
						xmlAttributes.XmlDefaultValue = ((DefaultValueAttribute)obj).Value;
					}
					if (obj.GetType() == typeof(XmlElementAttribute))
					{
						xmlAttributes.XmlElements.Add((XmlElementAttribute)obj);
					}
					if (obj.GetType() == typeof(XmlArrayAttribute))
					{
						xmlAttributes.XmlArray = (XmlArrayAttribute)obj;
					}
					if (obj.GetType() == typeof(XmlArrayItemAttribute))
					{
						XmlArrayItemAttribute xmlArrayItemAttribute = (XmlArrayItemAttribute)obj;
						if (xmlArrayItemAttribute.Type != null)
						{
							this.IncludeType(xmlArrayItemAttribute.Type);
						}
						xmlAttributes.XmlArrayItems.Add(xmlArrayItemAttribute);
					}
				}
				if (prop.SerializationVisibility == DesignerSerializationVisibility.Hidden)
				{
					bool xmlIgnore = xmlAttributes.XmlIgnore;
				}
			}
			if (!xmlAttributes.XmlIgnore)
			{
				string name = prop.Name;
				string text = string.Empty;
				if (this.UseNamespaces && prop.DesignTimeOnly)
				{
					text = "http://schemas.microsoft.com/DataWarehouse/Designer/1.0";
				}
				this.GetMemberName(xmlAttributes, ref name, ref text);
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
				mapping.AddMember(name, text, memberMapping);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004E58 File Offset: 0x00003058
		private void ImportDesignMembers(StructMapping mapping, object obj, bool includeRuntimeProperties, bool includeDesignTimeProperties)
		{
			foreach (object obj2 in TypeDescriptor.GetProperties(obj))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (propertyDescriptor.DesignTimeOnly)
				{
					this.ImportPropertyDescriptor(mapping, propertyDescriptor);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004EBC File Offset: 0x000030BC
		private StructMapping ImportStructMapping(Type objBaseType, object obj, bool isRoot)
		{
			StructMapping structMapping = new StructMapping();
			Type type = obj.GetType();
			if (objBaseType == null)
			{
				objBaseType = type;
			}
			this.IncludeType(type);
			XmlElementAttribute[] customAttributes = this.GetCustomAttributes<XmlElementAttribute>(type, TypeMapper.typeElementAttributeCache);
			if (customAttributes.Length != 0)
			{
				this.AssignStringIfNotEmpty(ref structMapping.Namespace, customAttributes[0].Namespace);
				this.AssignStringIfNotEmpty(ref structMapping.ElementName, customAttributes[0].ElementName);
			}
			XmlRootAttribute[] customAttributes2 = this.GetCustomAttributes<XmlRootAttribute>(type, TypeMapper.typeRootAttributeCache);
			if (customAttributes2.Length != 0)
			{
				this.AssignStringIfNotEmpty(ref structMapping.Namespace, customAttributes2[0].Namespace);
				this.AssignStringIfNotEmpty(ref structMapping.ElementName, customAttributes2[0].ElementName);
			}
			if (structMapping.Namespace == null)
			{
				structMapping.Namespace = string.Empty;
			}
			if (structMapping.ElementName == null)
			{
				structMapping.ElementName = objBaseType.Name;
			}
			bool flag = this.IncludeRuntimeProperties || !isRoot;
			bool includeDesignTimeProperties = this.IncludeDesignTimeProperties;
			if (flag)
			{
				this.ImportTypeMembers(structMapping, type);
			}
			if (includeDesignTimeProperties)
			{
				this.ImportDesignMembers(structMapping, obj, flag, includeDesignTimeProperties);
			}
			return structMapping;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004FB8 File Offset: 0x000031B8
		private T[] GetCustomAttributes<T>(Type objectType, Dictionary<Type, T[]> attributesCache)
		{
			T[] array2;
			lock (attributesCache)
			{
				T[] array;
				if (!attributesCache.TryGetValue(objectType, out array))
				{
					array = Array.ConvertAll<object, T>(objectType.GetCustomAttributes(typeof(T), true), (object element) => (T)((object)element));
					attributesCache.Add(objectType, array);
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000503C File Offset: 0x0000323C
		private object[] GetPropertyInfoCustomAttributes(PropertyInfo propertyInfo, Dictionary<PropertyInfo, object[]> attributesCache)
		{
			object[] array;
			lock (attributesCache)
			{
				object[] customAttributes;
				if (!attributesCache.TryGetValue(propertyInfo, out customAttributes))
				{
					customAttributes = propertyInfo.GetCustomAttributes(typeof(XmlIgnoreOnReadAttribute), false);
					attributesCache.Add(propertyInfo, customAttributes);
				}
				array = customAttributes;
			}
			return array;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000509C File Offset: 0x0000329C
		private PropertyInfo[] GetPropertyInfos(Type objectType, Dictionary<Type, PropertyInfo[]> propertyInfoCache, BindingFlags bindingFlag)
		{
			PropertyInfo[] array;
			lock (propertyInfoCache)
			{
				PropertyInfo[] properties;
				if (!propertyInfoCache.TryGetValue(objectType, out properties))
				{
					properties = objectType.GetProperties(bindingFlag);
					propertyInfoCache.Add(objectType, properties);
				}
				array = properties;
			}
			return array;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000050F0 File Offset: 0x000032F0
		private FieldInfo[] GetFieldInfos(Type objectType, Dictionary<Type, FieldInfo[]> fieldInfoCache)
		{
			FieldInfo[] fields;
			lock (fieldInfoCache)
			{
				if (!fieldInfoCache.TryGetValue(objectType, out fields))
				{
					fields = objectType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
					fieldInfoCache.Add(objectType, fields);
				}
			}
			return fields;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005144 File Offset: 0x00003344
		private static void TestAndAssignString(ref string dest, string src)
		{
			if (src != null && src != string.Empty)
			{
				dest = src;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000515C File Offset: 0x0000335C
		internal static object ParseDeserializedEnumValue(Type type, string xmlValue)
		{
			IList<KeyValuePair<string, string>> enumCustomMapping = TypeMapper.GetEnumCustomMapping(type);
			if (enumCustomMapping != null)
			{
				for (int i = 0; i < enumCustomMapping.Count; i++)
				{
					if (string.Compare(enumCustomMapping[i].Value, xmlValue, StringComparison.InvariantCulture) == 0)
					{
						xmlValue = enumCustomMapping[i].Key;
						break;
					}
				}
			}
			return Enum.Parse(type, xmlValue, true);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000051B8 File Offset: 0x000033B8
		internal static void AdjustEnumValueForSerialization(Type type, ref string value)
		{
			IList<KeyValuePair<string, string>> enumCustomMapping = TypeMapper.GetEnumCustomMapping(type);
			if (enumCustomMapping != null)
			{
				for (int i = 0; i < enumCustomMapping.Count; i++)
				{
					if (string.Compare(enumCustomMapping[i].Key, value, StringComparison.InvariantCulture) == 0)
					{
						value = enumCustomMapping[i].Value;
						return;
					}
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000520C File Offset: 0x0000340C
		private static IList<KeyValuePair<string, string>> GetEnumCustomMapping(Type type)
		{
			Dictionary<Type, IList<KeyValuePair<string, string>>> dictionary = TypeMapper.enumTypeCustomMappingCache;
			IList<KeyValuePair<string, string>> list;
			lock (dictionary)
			{
				if (!TypeMapper.enumTypeCustomMappingCache.TryGetValue(type, out list))
				{
					list = null;
					foreach (FieldInfo fieldInfo in type.GetFields())
					{
						object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(XmlEnumAttribute), false);
						if (customAttributes.Length != 0)
						{
							XmlEnumAttribute xmlEnumAttribute = (XmlEnumAttribute)customAttributes[0];
							if (list == null)
							{
								list = new List<KeyValuePair<string, string>>();
							}
							list.Add(new KeyValuePair<string, string>(fieldInfo.Name, xmlEnumAttribute.Name));
						}
					}
					TypeMapper.enumTypeCustomMappingCache.Add(type, list);
				}
			}
			return list;
		}

		// Token: 0x04000056 RID: 86
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x04000057 RID: 87
		private Dictionary<string, Type> includedTypes;

		// Token: 0x04000058 RID: 88
		private DesignXmlSerializerOptions options;

		// Token: 0x04000059 RID: 89
		private object userSerializationOptions;

		// Token: 0x0400005A RID: 90
		private static Dictionary<Type, Dictionary<string, int>> typePropOrderCache = new Dictionary<Type, Dictionary<string, int>>();

		// Token: 0x0400005B RID: 91
		private static Dictionary<Type, XmlElementAttribute[]> typeElementAttributeCache = new Dictionary<Type, XmlElementAttribute[]>();

		// Token: 0x0400005C RID: 92
		private static Dictionary<Type, XmlRootAttribute[]> typeRootAttributeCache = new Dictionary<Type, XmlRootAttribute[]>();

		// Token: 0x0400005D RID: 93
		private static Dictionary<Type, object[]> typeXmlIncludeAttributeCache = new Dictionary<Type, object[]>();

		// Token: 0x0400005E RID: 94
		private static Dictionary<PropertyInfo, object[]> propertyInfoXmlIgnoreOnReadAttributeCache = new Dictionary<PropertyInfo, object[]>();

		// Token: 0x0400005F RID: 95
		private static Dictionary<Type, PropertyInfo[]> typePublicPropInfoCache = new Dictionary<Type, PropertyInfo[]>();

		// Token: 0x04000060 RID: 96
		private static Dictionary<Type, PropertyInfo[]> typePrivatePropInfoCache = new Dictionary<Type, PropertyInfo[]>();

		// Token: 0x04000061 RID: 97
		private static Dictionary<Type, FieldInfo[]> typeFieldInfoCache = new Dictionary<Type, FieldInfo[]>();

		// Token: 0x04000062 RID: 98
		private static Dictionary<Type, IList<KeyValuePair<string, string>>> enumTypeCustomMappingCache = new Dictionary<Type, IList<KeyValuePair<string, string>>>();
	}
}
