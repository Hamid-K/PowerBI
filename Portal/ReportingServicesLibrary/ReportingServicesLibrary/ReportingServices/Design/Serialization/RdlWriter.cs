using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.RdlModel;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000395 RID: 917
	internal class RdlWriter
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x0007BC4B File Offset: 0x00079E4B
		public RdlWriter(XmlAttributeOverrides attributesOverrides, RdlSerializerOptions options)
		{
			this.attributesOverrides = attributesOverrides;
			this.mapper = new TypeMapper(true, attributesOverrides, options);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0007BC68 File Offset: 0x00079E68
		public void SerializeComponent(XmlWriter writer, Type rootType, object root)
		{
			this.writer = writer;
			this.isRootWritten = false;
			writer.WriteStartDocument();
			this.WriteObject(rootType, root, null, null);
			writer.WriteEndDocument();
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0007BC90 File Offset: 0x00079E90
		private void WriteObject(Type objType, object obj, string name, string ns)
		{
			if (obj == null)
			{
				return;
			}
			if (obj is IVoluntarySerializable && !((IVoluntarySerializable)obj).ShouldBeSerialized())
			{
				return;
			}
			TypeMapping typeModel = this.mapper.GetTypeModel(objType, obj);
			if (name == null)
			{
				name = typeModel.ElementName;
				ns = typeModel.Namespace;
			}
			if (typeModel is PrimitiveMapping)
			{
				this.WritePrimitive(typeModel, obj, name, ns);
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
				this.WriteStructure(obj, name, ns, (StructMapping)typeModel);
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0007BD30 File Offset: 0x00079F30
		private void WriteSpecialContent(object obj)
		{
			IXmlSerializable xmlSerializable = (IXmlSerializable)obj;
			if (xmlSerializable != null)
			{
				xmlSerializable.WriteXml(this.writer);
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0007BD53 File Offset: 0x00079F53
		private void WriteSpecialMapping(object obj, string name, string ns)
		{
			this.writer.WriteStartElement(name, ns);
			this.WriteSpecialContent(obj);
			this.writer.WriteEndElement();
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0007BD74 File Offset: 0x00079F74
		private void WritePrimitiveContent(TypeMapping mapping, object obj)
		{
			Type type = obj.GetType();
			string text;
			if (type == typeof(bool))
			{
				text = (((bool)obj) ? "true" : "false");
			}
			else if (type == typeof(DateTime))
			{
				text = XmlCustomFormatter.FromDateTime((DateTime)obj);
			}
			else
			{
				text = obj.ToString();
			}
			this.writer.WriteString(text);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0007BDE6 File Offset: 0x00079FE6
		private void WritePrimitive(TypeMapping mapping, object obj, string name, string ns)
		{
			this.writer.WriteStartElement(name, ns);
			this.WritePrimitiveContent(mapping, obj);
			this.writer.WriteEndElement();
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0007BE0C File Offset: 0x0007A00C
		private void WriteArrayContent(object array, ArrayMapping mapping, string ns)
		{
			if (array is IDictionary)
			{
				array = ((IDictionary)array).Values;
			}
			foreach (object obj in ((IEnumerable)array))
			{
				Type type = obj.GetType();
				string text = (string)mapping.ElementTypes[type];
				this.WriteObject(type, obj, text, ns);
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0007BE94 File Offset: 0x0007A094
		private int SerializableArrayCount(object array)
		{
			if (array is ICollection && ((ICollection)array).Count == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in ((IEnumerable)array))
			{
				if (obj is IVoluntarySerializable)
				{
					if (((IVoluntarySerializable)obj).ShouldBeSerialized())
					{
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0007BF1C File Offset: 0x0007A11C
		private void WriteArray(object array, string name, string ns, ArrayMapping mapping)
		{
			if (this.SerializableArrayCount(array) == 0)
			{
				return;
			}
			this.writer.WriteStartElement(name, ns);
			this.WriteArrayContent(array, mapping, ns);
			this.writer.WriteEndElement();
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0007BF4C File Offset: 0x0007A14C
		private void WriteMember(object component, object obj, MemberMapping memberMapping, string name, string ns)
		{
			if (obj == null)
			{
				return;
			}
			bool flag = false;
			if (memberMapping is PropertyMapping)
			{
				if (!((PropertyMapping)memberMapping).Property.ShouldSerializeValue(component))
				{
					return;
				}
				if (((PropertyMapping)memberMapping).Property.GetType().FullName == "Microsoft.Designer.Design.DesignPropertyDescriptor")
				{
					flag = true;
				}
			}
			else
			{
				object xmlDefaultValue = memberMapping.XmlAttributes.XmlDefaultValue;
				if (xmlDefaultValue != null && xmlDefaultValue.Equals(obj))
				{
					return;
				}
			}
			if (!flag && obj is IVoluntarySerializable && !((IVoluntarySerializable)obj).ShouldBeSerialized())
			{
				return;
			}
			XmlElementAttributes xmlElements = memberMapping.XmlAttributes.XmlElements;
			Type type = obj.GetType();
			if (xmlElements.Count > 0)
			{
				name = null;
				foreach (object obj2 in xmlElements)
				{
					XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj2;
					if (xmlElementAttribute.Type == null || type == xmlElementAttribute.Type)
					{
						name = xmlElementAttribute.ElementName;
						break;
					}
				}
			}
			TypeMapping typeModel = this.mapper.GetTypeModel(memberMapping, obj);
			if (name == null)
			{
				name = typeModel.ElementName;
				ns = typeModel.Namespace;
			}
			if (typeModel is PrimitiveMapping)
			{
				this.WritePrimitive(typeModel, obj, name, ns);
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
				this.WriteStructure(obj, name, ns, (StructMapping)typeModel);
			}
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0007C0E4 File Offset: 0x0007A2E4
		private void WriteStructureContent(object obj, StructMapping mapping, string ns)
		{
			if (obj is IComponent && ((IComponent)obj).Site == null)
			{
				return;
			}
			bool flag = false;
			if (!(obj is CustomProperty))
			{
				MemberMapping element = mapping.GetElement("Name", "");
				if (element != null)
				{
					object value = element.GetValue(obj);
					if (value is string)
					{
						this.writer.WriteAttributeString("Name", "", (string)value);
						flag = true;
					}
				}
			}
			int count = mapping.members.Count;
			for (int i = 0; i < count; i++)
			{
				MemberMapping memberMapping = (MemberMapping)mapping.members[i];
				if (!flag || !(memberMapping.Name == "Name"))
				{
					if (memberMapping.ParentElementName != null)
					{
						string parentElementName = memberMapping.ParentElementName;
						string parentElementNameSpace = memberMapping.ParentElementNameSpace;
						if (!(bool)mapping.GetParentXmlElement(parentElementName, parentElementNameSpace))
						{
							ArrayList arrayList = new ArrayList();
							for (int j = 0; j < count; j++)
							{
								MemberMapping memberMapping2 = (MemberMapping)mapping.members[j];
								if (memberMapping2.ParentElementName == parentElementName && memberMapping2.ParentElementNameSpace == parentElementNameSpace)
								{
									object value2 = memberMapping2.GetValue(obj);
									if (value2 != null)
									{
										object xmlDefaultValue = memberMapping2.XmlAttributes.XmlDefaultValue;
										if ((xmlDefaultValue == null || !xmlDefaultValue.Equals(value2)) && (!(obj is IVoluntarySerializable) || ((IVoluntarySerializable)obj).ShouldBeSerialized()) && (value2 is string || !(value2 is IEnumerable) || this.SerializableArrayCount(value2) != 0))
										{
											arrayList.Add(memberMapping2);
										}
									}
								}
							}
							if (arrayList.Count != 0)
							{
								this.writer.WriteStartElement(parentElementName, (parentElementNameSpace != "") ? parentElementNameSpace : "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
								foreach (object obj2 in arrayList)
								{
									MemberMapping memberMapping3 = (MemberMapping)obj2;
									this.WriteMember(obj, memberMapping3.GetValue(obj), memberMapping3, memberMapping3.Name, (memberMapping3.Namespace != string.Empty) ? memberMapping3.Namespace : ns);
								}
								this.writer.WriteEndElement();
								mapping.SetParentXmlElement(parentElementName, parentElementNameSpace);
							}
						}
					}
					else
					{
						this.WriteMember(obj, memberMapping.GetValue(obj), memberMapping, memberMapping.Name, (memberMapping.Namespace != string.Empty) ? memberMapping.Namespace : ns);
					}
				}
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0007C384 File Offset: 0x0007A584
		private void WriteStructure(object obj, string name, string ns, StructMapping mapping)
		{
			if (obj != null)
			{
				if (ns == "")
				{
					ns = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
				}
				if (!this.isRootWritten)
				{
					this.writer.WriteStartElement(name, ns);
					this.writer.WriteAttributeString("xmlns", "", null, "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
					this.writer.WriteAttributeString("xmlns", "rd", null, "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
					this.isRootWritten = true;
				}
				else
				{
					this.writer.WriteStartElement(name, ns);
				}
				this.WriteStructureContent(obj, mapping, ns);
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x04000CD2 RID: 3282
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x04000CD3 RID: 3283
		private TypeMapper mapper;

		// Token: 0x04000CD4 RID: 3284
		private XmlWriter writer;

		// Token: 0x04000CD5 RID: 3285
		private bool isRootWritten;
	}
}
