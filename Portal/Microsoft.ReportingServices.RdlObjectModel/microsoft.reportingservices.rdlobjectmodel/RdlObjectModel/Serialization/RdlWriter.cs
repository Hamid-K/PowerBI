using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002DD RID: 733
	internal class RdlWriter : RdlReaderWriterBase
	{
		// Token: 0x06001692 RID: 5778 RVA: 0x00034574 File Offset: 0x00032774
		public RdlWriter(RdlSerializerSettings settings)
			: base(settings)
		{
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00034580 File Offset: 0x00032780
		public void Serialize(XmlWriter writer, object root)
		{
			XmlDocument xmlDocument = XmlUtils.CreateXmlDocumentWithNullResolver();
			using (XmlWriter xmlWriter = xmlDocument.CreateNavigator().AppendChild())
			{
				xmlWriter.WriteStartDocument();
				this.WriteObject(xmlWriter, root, null, null, null, 0);
				xmlWriter.WriteEndDocument();
			}
			new NamespaceUpdater().Update(xmlDocument, base.Host);
			xmlDocument.Save(writer);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000345EC File Offset: 0x000327EC
		private void WriteObject(XmlWriter writer, object obj, string name, string ns, MemberMapping member, int nestingLevel)
		{
			if (obj == null)
			{
				return;
			}
			if (obj is IShouldSerialize && !((IShouldSerialize)obj).ShouldSerializeThis())
			{
				return;
			}
			this.WriteObjectContent(writer, null, obj, name, ns, member, nestingLevel);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00034618 File Offset: 0x00032818
		private void WriteObjectContent(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member, int nestingLevel)
		{
			TypeMapping typeMapping = TypeMapper.GetTypeMapping(base.GetSerializationType(obj));
			if (name == null)
			{
				name = typeMapping.Name;
				ns = typeMapping.Namespace;
			}
			if (typeMapping is PrimitiveMapping)
			{
				this.WritePrimitive(writer, component, obj, name, ns, member, typeMapping);
				return;
			}
			if (typeMapping is ArrayMapping)
			{
				this.WriteArray(writer, component, obj, name, ns, member, (ArrayMapping)typeMapping, nestingLevel);
				return;
			}
			if (typeMapping is SpecialMapping)
			{
				this.WriteSpecialMapping(writer, component, obj, name, ns, member);
				return;
			}
			if (typeMapping is StructMapping)
			{
				this.WriteStructure(writer, component, obj, name, ns, member, (StructMapping)typeMapping);
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000346B4 File Offset: 0x000328B4
		private void WriteStartElement(XmlWriter writer, object component, string name, string ns, MemberMapping member)
		{
			writer.WriteStartElement(name, ns);
			if (component != null && member != null && member.ChildAttributes != null)
			{
				foreach (MemberMapping memberMapping in member.ChildAttributes)
				{
					this.WriteChildAttribute(writer, memberMapping.GetValue(component), memberMapping);
				}
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0003472C File Offset: 0x0003292C
		private void WriteSpecialContent(XmlWriter writer, object obj)
		{
			IXmlSerializable xmlSerializable = (IXmlSerializable)obj;
			if (xmlSerializable != null)
			{
				xmlSerializable.WriteXml(writer);
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0003474A File Offset: 0x0003294A
		private void WriteSpecialMapping(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member)
		{
			this.WriteStartElement(writer, component, name, ns, member);
			this.WriteSpecialContent(writer, obj);
			writer.WriteEndElement();
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00034768 File Offset: 0x00032968
		private void WritePrimitiveContent(XmlWriter writer, TypeMapping mapping, object obj)
		{
			if (obj == null)
			{
				return;
			}
			Type type = obj.GetType();
			string text;
			if (type == typeof(string))
			{
				text = (string)obj;
				if (text == "")
				{
					return;
				}
			}
			else if (type == typeof(bool))
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
			writer.WriteString(text);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00034800 File Offset: 0x00032A00
		private void WritePrimitive(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member, TypeMapping mapping)
		{
			this.WriteStartElement(writer, component, name, ns, member);
			this.WritePrimitiveContent(writer, mapping, obj);
			writer.WriteEndElement();
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00034820 File Offset: 0x00032A20
		private void WriteArrayContent(XmlWriter writer, object array, ArrayMapping mapping, MemberMapping member, int nestingLevel, string ns)
		{
			Dictionary<string, Type> elementTypes = mapping.ElementTypes;
			foreach (object obj in ((IEnumerable)array))
			{
				string text = null;
				bool flag = false;
				if (member != null && member.XmlAttributes.XmlArrayItems.Count > nestingLevel)
				{
					XmlArrayItemAttribute xmlArrayItemAttribute = member.XmlAttributes.XmlArrayItems[nestingLevel];
					text = xmlArrayItemAttribute.ElementName;
					flag = xmlArrayItemAttribute.IsNullable;
				}
				else
				{
					Type serializationType = base.GetSerializationType(obj);
					TypeMapping typeMapping = TypeMapper.GetTypeMapping(serializationType);
					if (typeMapping != null)
					{
						text = typeMapping.Name;
						ns = typeMapping.Namespace;
					}
					else
					{
						foreach (KeyValuePair<string, Type> keyValuePair in elementTypes)
						{
							if (keyValuePair.Value == serializationType)
							{
								text = keyValuePair.Key;
								break;
							}
						}
					}
				}
				if (text == null)
				{
					throw new Exception("No array element name.");
				}
				if (obj != null)
				{
					this.WriteObject(writer, obj, text, ns, member, nestingLevel + 1);
				}
				else if (flag)
				{
					this.WriteNilElement(writer, text, ns);
				}
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0003496C File Offset: 0x00032B6C
		private bool ShouldSerializeArray(object array)
		{
			if (array is ICollection && ((ICollection)array).Count == 0)
			{
				return false;
			}
			foreach (object obj in ((IEnumerable)array))
			{
				if (!(obj is IShouldSerialize) || ((IShouldSerialize)obj).ShouldSerializeThis())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000349EC File Offset: 0x00032BEC
		private void WriteArray(XmlWriter writer, object component, object array, string name, string ns, MemberMapping member, ArrayMapping mapping, int nestingLevel)
		{
			if (!this.ShouldSerializeArray(array))
			{
				return;
			}
			this.WriteStartElement(writer, component, name, ns, member);
			this.WriteArrayContent(writer, array, mapping, member, nestingLevel, ns);
			writer.WriteEndElement();
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00034A1C File Offset: 0x00032C1C
		private bool ShouldSerializeValue(object component, object obj, MemberMapping memberMapping)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is IShouldSerialize && !((IShouldSerialize)obj).ShouldSerializeThis())
			{
				return false;
			}
			if (component is IShouldSerialize)
			{
				SerializationMethod serializationMethod = ((IShouldSerialize)component).ShouldSerializeProperty(memberMapping.Name);
				if (serializationMethod == SerializationMethod.Never)
				{
					return false;
				}
				if (serializationMethod == SerializationMethod.Always)
				{
					return true;
				}
			}
			object xmlDefaultValue = memberMapping.XmlAttributes.XmlDefaultValue;
			return xmlDefaultValue == null || !obj.Equals(xmlDefaultValue);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00034A88 File Offset: 0x00032C88
		private void WriteMember(XmlWriter writer, object component, object obj, MemberMapping memberMapping, string name, string ns)
		{
			if (!this.ShouldSerializeValue(component, obj, memberMapping))
			{
				return;
			}
			XmlElementAttributes xmlElements = memberMapping.XmlAttributes.XmlElements;
			if (xmlElements.Count > 0)
			{
				Type serializationType = base.GetSerializationType(obj);
				foreach (object obj2 in xmlElements)
				{
					XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj2;
					if (serializationType == xmlElementAttribute.Type)
					{
						if (!string.IsNullOrEmpty(xmlElementAttribute.ElementName))
						{
							name = xmlElementAttribute.ElementName;
						}
						if (!string.IsNullOrEmpty(xmlElementAttribute.Namespace))
						{
							ns = xmlElementAttribute.Namespace;
							break;
						}
						break;
					}
				}
			}
			this.WriteObjectContent(writer, component, obj, name, ns, memberMapping, 0);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00034B50 File Offset: 0x00032D50
		private void WriteStructContent(XmlWriter writer, object obj, StructMapping mapping, string ns)
		{
			foreach (MemberMapping memberMapping in mapping.Attributes.Values)
			{
				if (memberMapping.Type == typeof(string) && memberMapping.XmlAttributes.XmlElements.Count == 0)
				{
					object value = memberMapping.GetValue(obj);
					if (this.ShouldSerializeValue(obj, value, memberMapping))
					{
						writer.WriteAttributeString(memberMapping.Name, memberMapping.Namespace, (value != null) ? ((string)value) : "");
					}
				}
			}
			foreach (MemberMapping memberMapping2 in mapping.Members)
			{
				if (memberMapping2.XmlAttributes.XmlAttribute == null)
				{
					string text = ((memberMapping2.Namespace != string.Empty) ? memberMapping2.Namespace : ns);
					this.WriteMember(writer, obj, memberMapping2.GetValue(obj), memberMapping2, memberMapping2.Name, text);
				}
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00034C80 File Offset: 0x00032E80
		private void WriteStructure(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member, StructMapping mapping)
		{
			if (obj == null)
			{
				return;
			}
			this.WriteStartElement(writer, component, name, ns, member);
			this.WriteStructContent(writer, obj, mapping, ns);
			writer.WriteEndElement();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00034CA6 File Offset: 0x00032EA6
		private void WriteNilElement(XmlWriter writer, string name, string ns)
		{
			writer.WriteStartElement(name, ns);
			writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			writer.WriteEndElement();
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00034CE4 File Offset: 0x00032EE4
		private void WriteChildAttribute(XmlWriter writer, object obj, MemberMapping mapping)
		{
			if (obj == null)
			{
				return;
			}
			XmlAttributeAttribute xmlAttribute = mapping.XmlAttributes.XmlAttribute;
			string text = obj.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteAttributeString(xmlAttribute.AttributeName, xmlAttribute.Namespace, text);
			}
		}
	}
}
