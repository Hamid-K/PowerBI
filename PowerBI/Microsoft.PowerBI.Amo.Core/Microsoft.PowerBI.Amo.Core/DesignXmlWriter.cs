using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000004 RID: 4
	internal sealed class DesignXmlWriter
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public DesignXmlWriter(XmlAttributeOverrides attributesOverrides, DesignXmlSerializerOptions options, object userSerializationOptions)
		{
			this.attributesOverrides = attributesOverrides;
			this.serializationOptions = options;
			this.mapper = new TypeMapper(attributesOverrides, options, userSerializationOptions);
			this.userSerializationOptions = userSerializationOptions;
			this.noStartDoc = (options & DesignXmlSerializerOptions.DontWriteStartDocument) == DesignXmlSerializerOptions.DontWriteStartDocument;
			this.includeForeignComponents = (options & DesignXmlSerializerOptions.IncludeForeignComponent) == DesignXmlSerializerOptions.IncludeForeignComponent;
			this.includeDesignTimeProperties = (this.serializationOptions & DesignXmlSerializerOptions.IgnoreDesignTimeProperties) == DesignXmlSerializerOptions.Default;
			this.includeSiteName = this.includeDesignTimeProperties && (this.serializationOptions & DesignXmlSerializerOptions.DontWriteSiteName) == DesignXmlSerializerOptions.Default;
			this.useNamespaces = (options & DesignXmlSerializerOptions.DoNotUseNamespaces) == DesignXmlSerializerOptions.Default;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002E49 File Offset: 0x00001049
		public string GetRealNamespace(string ns)
		{
			if (!this.useNamespaces)
			{
				return null;
			}
			return ns;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E58 File Offset: 0x00001058
		public void SerializeComponent(XmlWriter writer, Type rootType, object root)
		{
			this.writer = writer;
			this.isRootWritten = false;
			this.tempNamespacePrefix = 1;
			IComponent component = root as IComponent;
			if (component != null)
			{
				this.ourContainer = ((component.Site != null) ? component.Site.Container : null);
			}
			if (!this.noStartDoc)
			{
				writer.WriteStartDocument();
			}
			this.WriteObject(rootType, root, null, null, true);
			if (!this.noStartDoc)
			{
				writer.WriteEndDocument();
			}
			if (!this.noStartDoc)
			{
				writer.Close();
			}
			else
			{
				writer.Flush();
			}
			this.ourContainer = null;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002EE4 File Offset: 0x000010E4
		private void WriteNilElement(string name, string ns)
		{
			this.WriteStartElement(name, ns);
			this.writer.WriteAttributeString("nil", this.GetRealNamespace("http://www.w3.org/2001/XMLSchema-instance"), "true");
			this.writer.WriteEndElement();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002F1C File Offset: 0x0000111C
		private string GetQualifiedName(string name, string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return name;
			}
			string text = this.writer.LookupPrefix(ns);
			if (text == null)
			{
				string text2 = "q";
				int num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
				text = text2 + num.ToString(CultureInfo.InvariantCulture);
				if (this.useNamespaces)
				{
					this.writer.WriteAttributeString("xmlns", text, null, ns);
				}
			}
			else if (text.Length == 0)
			{
				return name;
			}
			return text + ":" + name;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F9D File Offset: 0x0000119D
		private void WriteXsiType(string name, string ns)
		{
			this.writer.WriteAttributeString("type", this.GetRealNamespace("http://www.w3.org/2001/XMLSchema-instance"), this.GetQualifiedName(name, ns));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002FC4 File Offset: 0x000011C4
		private void WriteXsdType(Type type)
		{
			if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime))
			{
				string text = null;
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					text = "boolean";
					break;
				case TypeCode.Byte:
					text = "byte";
					break;
				case TypeCode.Int16:
					text = "short";
					break;
				case TypeCode.UInt16:
					text = "unsignedShort";
					break;
				case TypeCode.Int32:
					text = "int";
					break;
				case TypeCode.UInt32:
					text = "unsignedInt";
					break;
				case TypeCode.Int64:
					text = "long";
					break;
				case TypeCode.UInt64:
					text = "unsignedLong";
					break;
				case TypeCode.Single:
					text = "float";
					break;
				case TypeCode.Double:
					text = "double";
					break;
				case TypeCode.Decimal:
					text = "decimal";
					break;
				case TypeCode.DateTime:
					text = "dateTime";
					break;
				case TypeCode.String:
					text = "string";
					break;
				}
				if (text != null)
				{
					this.WriteXsiType(text, "http://www.w3.org/2001/XMLSchema");
					return;
				}
			}
			else
			{
				if (type.IsEnum && !this.mapper.IsKnownType(type))
				{
					this.WriteXsiType("int", "http://www.w3.org/2001/XMLSchema");
					return;
				}
				XmlTypeAttribute[] array = (XmlTypeAttribute[])type.GetCustomAttributes(typeof(XmlTypeAttribute), false);
				string text2 = ((array.Length != 0) ? array[0].Namespace : null);
				this.writer.WriteAttributeString("type", this.GetRealNamespace("http://www.w3.org/2001/XMLSchema-instance"), this.GetQualifiedName(type.Name, text2));
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003142 File Offset: 0x00001342
		private bool ShouldBeSerialized(object obj)
		{
			return !this.isRootWritten || obj != null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003154 File Offset: 0x00001354
		private void WriteObject(Type objType, object obj, string name, string ns, bool isRoot)
		{
			if (!this.includeForeignComponents)
			{
				IComponent component = obj as IComponent;
				if (component != null && component.Site != null && component.Site.Container != this.ourContainer)
				{
					return;
				}
			}
			if (obj == null)
			{
				return;
			}
			this.CallBeginSerializationCallback(obj);
			if (!this.ShouldBeSerialized(obj))
			{
				return;
			}
			bool flag = objType != obj.GetType();
			TypeMapping typeModel = this.mapper.GetTypeModel(objType, obj, isRoot);
			if (name == null)
			{
				name = typeModel.ElementName;
			}
			if (ns == null)
			{
				ns = typeModel.Namespace;
			}
			if (typeModel is PrimitiveMapping)
			{
				this.WritePrimitive(typeModel, obj, name, ns, flag);
				return;
			}
			if (typeModel is ArrayMapping)
			{
				this.WriteArray(obj, name, ns, (ArrayMapping)typeModel);
				return;
			}
			if (typeModel is SpecialMapping)
			{
				this.WriteSpecialMapping(obj, name, ns);
				return;
			}
			if (typeModel is StructMapping)
			{
				this.WriteStructure(obj, name, ns, (StructMapping)typeModel, flag);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003234 File Offset: 0x00001434
		private void SetDataSetObjectName(IContainer container, IComponent comp, PropertyCollection props)
		{
			ISite site = comp.Site;
			if (site == null && container != null)
			{
				container.Add(comp);
				site = comp.Site;
			}
			string text = ((site == null) ? null : site.Name);
			if (text != null && text.Length != 0)
			{
				props["design-time-name"] = text;
				return;
			}
			props.Remove("design-time-name");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000328C File Offset: 0x0000148C
		private void WriteSpecialContent(object obj)
		{
			IXmlSerializable xmlSerializable = obj as IXmlSerializable;
			if (xmlSerializable != null)
			{
				DataSet dataSet = obj as DataSet;
				if (dataSet != null)
				{
					this.SetDataSetObjectName(this.ourContainer, dataSet, dataSet.ExtendedProperties);
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable = (DataTable)obj2;
						this.SetDataSetObjectName(this.ourContainer, dataTable, dataTable.ExtendedProperties);
						foreach (object obj3 in dataTable.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj3;
							this.SetDataSetObjectName(this.ourContainer, dataColumn, dataColumn.ExtendedProperties);
						}
					}
					dataSet.WriteXmlSchema(this.writer);
					dataSet.WriteXml(this.writer, XmlWriteMode.IgnoreSchema);
					return;
				}
				xmlSerializable.WriteXml(this.writer);
				return;
			}
			else
			{
				XmlNode xmlNode = obj as XmlNode;
				if (xmlNode != null)
				{
					xmlNode.WriteTo(this.writer);
					return;
				}
				return;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000033C4 File Offset: 0x000015C4
		private void WriteSpecialMapping(object obj, string name, string ns)
		{
			this.WriteStartElement(name, ns);
			this.WriteSpecialContent(obj);
			this.writer.WriteEndElement();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000033E0 File Offset: 0x000015E0
		private void WritePrimitiveContent(TypeMapping mapping, object obj)
		{
			Type type = obj.GetType();
			string text = null;
			if (type.IsEnum)
			{
				if (this.mapper.IsKnownType(type))
				{
					text = obj.ToString();
				}
				else
				{
					text = XmlConvert.ToString((int)obj);
				}
				string namespaceFromEnumValue = this.GetNamespaceFromEnumValue(type, text);
				if (!string.IsNullOrEmpty(namespaceFromEnumValue))
				{
					string text2 = this.writer.LookupPrefix(namespaceFromEnumValue);
					this.writer.WriteAttributeString("valuens", string.IsNullOrEmpty(text2) ? namespaceFromEnumValue : text2);
				}
				TypeMapper.AdjustEnumValueForSerialization(type, ref text);
			}
			else
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Object:
					if (type == typeof(TimeSpan))
					{
						text = XmlConvert.ToString((TimeSpan)obj);
						goto IL_01F2;
					}
					text = obj.ToString();
					goto IL_01F2;
				case TypeCode.Boolean:
					text = XmlConvert.ToString((bool)obj);
					goto IL_01F2;
				case TypeCode.Char:
					text = XmlConvert.ToString((char)obj);
					goto IL_01F2;
				case TypeCode.SByte:
					text = XmlConvert.ToString((sbyte)obj);
					goto IL_01F2;
				case TypeCode.Byte:
					text = XmlConvert.ToString((byte)obj);
					goto IL_01F2;
				case TypeCode.Int16:
					text = XmlConvert.ToString((short)obj);
					goto IL_01F2;
				case TypeCode.UInt16:
					text = XmlConvert.ToString((ushort)obj);
					goto IL_01F2;
				case TypeCode.Int32:
					text = XmlConvert.ToString((int)obj);
					goto IL_01F2;
				case TypeCode.UInt32:
					text = XmlConvert.ToString((uint)obj);
					goto IL_01F2;
				case TypeCode.Int64:
					text = XmlConvert.ToString((long)obj);
					goto IL_01F2;
				case TypeCode.UInt64:
					text = XmlConvert.ToString((ulong)obj);
					goto IL_01F2;
				case TypeCode.Single:
					text = XmlConvert.ToString((float)obj);
					goto IL_01F2;
				case TypeCode.Double:
					text = XmlConvert.ToString((double)obj);
					goto IL_01F2;
				case TypeCode.Decimal:
					text = XmlConvert.ToString((decimal)obj);
					goto IL_01F2;
				case TypeCode.DateTime:
					text = XmlConvert.ToString((DateTime)obj, XmlDateTimeSerializationMode.Utc);
					goto IL_01F2;
				case TypeCode.String:
					text = (string)obj;
					goto IL_01F2;
				}
				text = obj.ToString();
			}
			IL_01F2:
			if (mapping.DataType == "NMTOKEN")
			{
				text = XmlConvert.EncodeNmToken(text);
			}
			else if (mapping.DataType == "NCName")
			{
				text = XmlConvert.EncodeLocalName(text);
			}
			this.writer.WriteString(text);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000361F File Offset: 0x0000181F
		private void WritePrimitive(TypeMapping mapping, object obj, string name, string ns, bool needTypeSpecs)
		{
			this.WriteStartElement(name, ns);
			if (needTypeSpecs)
			{
				this.WriteXsdType(obj.GetType());
			}
			this.WritePrimitiveContent(mapping, obj);
			this.writer.WriteEndElement();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003650 File Offset: 0x00001850
		private void WriteArrayContent(object array, ArrayMapping mapping, string ns)
		{
			foreach (object obj in ((IEnumerable)array))
			{
				if (obj != null)
				{
					this.WriteObject(mapping.MemberType, obj, mapping.MemberName, ns, false);
				}
				else if (mapping.IsMemberNullable)
				{
					this.WriteNilElement(mapping.MemberName, mapping.MemberNamespace);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000036D4 File Offset: 0x000018D4
		private bool HasSerializableArrayContent(ArrayMapping mapping, object array)
		{
			if (array is ICollection && ((ICollection)array).Count == 0)
			{
				return false;
			}
			foreach (object obj in ((IEnumerable)array))
			{
				if ((mapping.IsMemberNullable && obj == null) || this.ShouldBeSerialized(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003754 File Offset: 0x00001954
		private void WriteArray(object array, string name, string ns, ArrayMapping mapping)
		{
			if (this.isRootWritten)
			{
				if (!this.HasSerializableArrayContent(mapping, array))
				{
					return;
				}
				if (!this.ShouldBeSerialized(array))
				{
					return;
				}
			}
			this.WriteStartElement(name, ns);
			this.WriteArrayContent(array, mapping, ns);
			this.writer.WriteEndElement();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003794 File Offset: 0x00001994
		private void WriteMember(object obj, MemberMapping memberMapping, string name, string ns)
		{
			object xmlDefaultValue = memberMapping.XmlAttributes.XmlDefaultValue;
			if ((xmlDefaultValue == null && obj == null) || (xmlDefaultValue != null && xmlDefaultValue.Equals(obj)))
			{
				return;
			}
			if (name == null)
			{
				name = obj.GetType().Name;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			if (obj == null)
			{
				if (memberMapping.XmlAttributes.XmlArray != null)
				{
					if (memberMapping.XmlAttributes.XmlArray.IsNullable)
					{
						this.WriteNilElement(name, ns);
						return;
					}
				}
				else if (memberMapping.XmlAttributes.XmlElements.Count > 0)
				{
					if (memberMapping.XmlAttributes.XmlElements[0].IsNullable)
					{
						this.WriteNilElement(name, ns);
						return;
					}
				}
				else
				{
					this.WriteNilElement(name, ns);
				}
				return;
			}
			this.CallBeginSerializationCallback(obj);
			TypeMapping typeModel = this.mapper.GetTypeModel(memberMapping, obj);
			if (typeModel is ArrayMapping && !this.HasSerializableArrayContent((ArrayMapping)typeModel, obj))
			{
				return;
			}
			if (!this.ShouldBeSerialized(obj))
			{
				return;
			}
			this.WriteStartElement(name, ns);
			Type type = obj.GetType();
			if (type != memberMapping.MemberType && typeof(XmlNode) != memberMapping.MemberType)
			{
				this.WriteXsdType(type);
			}
			if (typeModel is PrimitiveMapping)
			{
				this.WritePrimitiveContent(typeModel, obj);
			}
			else if (typeModel is ArrayMapping)
			{
				this.WriteArrayContent(obj, (ArrayMapping)typeModel, ns);
			}
			else if (typeModel is SpecialMapping)
			{
				this.WriteSpecialContent(obj);
			}
			else if (typeModel is StructMapping)
			{
				this.WriteStructureContent(obj, (StructMapping)typeModel, ns);
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003918 File Offset: 0x00001B18
		private void WriteStructureContent(object obj, StructMapping mapping, string ns)
		{
			IComponent component = obj as IComponent;
			if (component != null && this.includeSiteName && this.ourContainer != null)
			{
				ISite site = component.Site;
				if (component.Site == null)
				{
					this.ourContainer.Add(component);
					site = component.Site;
				}
				string name = site.Name;
				if (name != null && name.Length != 0)
				{
					this.writer.WriteAttributeString("design-time-name", this.GetRealNamespace("http://schemas.microsoft.com/DataWarehouse/Designer/1.0"), name);
				}
			}
			foreach (object obj2 in mapping.Members)
			{
				MemberMapping memberMapping = (MemberMapping)obj2;
				this.WriteMember(memberMapping.GetValue(obj), memberMapping, memberMapping.MemberName, this.GetRealNamespace((memberMapping.MemberNamespace != string.Empty) ? memberMapping.MemberNamespace : ns));
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003A14 File Offset: 0x00001C14
		private void WriteStartElement(string name, string ns)
		{
			if (!this.isRootWritten)
			{
				this.writer.WriteStartElement(name, this.GetRealNamespace(ns));
				if (this.useNamespaces)
				{
					this.writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
					this.writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
					this.writer.WriteAttributeString("xmlns", "ddl2", null, "http://schemas.microsoft.com/analysisservices/2003/engine/2");
					this.writer.WriteAttributeString("xmlns", "ddl2_2", null, "http://schemas.microsoft.com/analysisservices/2003/engine/2/2");
					this.writer.WriteAttributeString("xmlns", "ddl100_100", null, "http://schemas.microsoft.com/analysisservices/2008/engine/100/100");
					this.writer.WriteAttributeString("xmlns", "ddl200", null, "http://schemas.microsoft.com/analysisservices/2010/engine/200");
					this.writer.WriteAttributeString("xmlns", "ddl200_200", null, "http://schemas.microsoft.com/analysisservices/2010/engine/200/200");
					this.writer.WriteAttributeString("xmlns", "ddl300", null, "http://schemas.microsoft.com/analysisservices/2011/engine/300");
					this.writer.WriteAttributeString("xmlns", "ddl300_300", null, "http://schemas.microsoft.com/analysisservices/2011/engine/300/300");
					this.writer.WriteAttributeString("xmlns", "ddl400", null, "http://schemas.microsoft.com/analysisservices/2012/engine/400");
					this.writer.WriteAttributeString("xmlns", "ddl400_400", null, "http://schemas.microsoft.com/analysisservices/2012/engine/400/400");
					this.writer.WriteAttributeString("xmlns", "ddl500", null, "http://schemas.microsoft.com/analysisservices/2013/engine/500");
					this.writer.WriteAttributeString("xmlns", "ddl500_500", null, "http://schemas.microsoft.com/analysisservices/2013/engine/500/500");
					if (this.includeDesignTimeProperties)
					{
						this.writer.WriteAttributeString("xmlns", "dwd", null, "http://schemas.microsoft.com/DataWarehouse/Designer/1.0");
					}
				}
				this.isRootWritten = true;
				return;
			}
			this.writer.WriteStartElement(name, this.GetRealNamespace(ns));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void WriteStructure(object obj, string name, string ns, StructMapping mapping, bool needTypeSpecs)
		{
			if (obj != null)
			{
				if (mapping.Namespace != null && mapping.Namespace.Length > 0)
				{
					ns = mapping.Namespace;
				}
				this.WriteStartElement(name, ns);
				if (needTypeSpecs)
				{
					this.WriteXsdType(obj.GetType());
				}
				this.WriteStructureContent(obj, mapping, ns);
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003C48 File Offset: 0x00001E48
		private string GetNamespaceFromEnumValue(Type t, string value)
		{
			FieldInfo field = t.GetField(value);
			if (field != null)
			{
				XmlElementAttribute[] array = (XmlElementAttribute[])field.GetCustomAttributes(typeof(XmlElementAttribute), false);
				if (array != null && array.Length != 0)
				{
					return array[0].Namespace;
				}
			}
			return null;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003C8E File Offset: 0x00001E8E
		private void CallBeginSerializationCallback(object obj)
		{
		}

		// Token: 0x04000030 RID: 48
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x04000031 RID: 49
		private DesignXmlSerializerOptions serializationOptions;

		// Token: 0x04000032 RID: 50
		private bool includeForeignComponents;

		// Token: 0x04000033 RID: 51
		private bool includeDesignTimeProperties;

		// Token: 0x04000034 RID: 52
		private bool includeSiteName;

		// Token: 0x04000035 RID: 53
		private bool noStartDoc;

		// Token: 0x04000036 RID: 54
		private bool useNamespaces;

		// Token: 0x04000037 RID: 55
		private TypeMapper mapper;

		// Token: 0x04000038 RID: 56
		private XmlWriter writer;

		// Token: 0x04000039 RID: 57
		private IContainer ourContainer;

		// Token: 0x0400003A RID: 58
		private bool isRootWritten;

		// Token: 0x0400003B RID: 59
		private int tempNamespacePrefix;

		// Token: 0x0400003C RID: 60
		private object userSerializationOptions;
	}
}
