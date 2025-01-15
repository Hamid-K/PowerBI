using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200005D RID: 93
	internal class ModelingXmlWriter
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000C578 File Offset: 0x0000A778
		public ModelingXmlWriter(XmlWriter xw, ModelingXmlSchema schema, ModelingSerializationOptions options)
		{
			if (xw == null || schema == null)
			{
				throw new InternalModelingException("xw or schema is null");
			}
			if (xw.Settings != null && xw.Settings.CheckCharacters)
			{
				throw new InvalidOperationException(DevExceptionMessages.XmlReaderCheckCharsTrue);
			}
			this.m_xw = xw;
			this.m_defaultNamespace = schema.TargetNamespace;
			this.m_options = options;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
		public XmlWriter Writer
		{
			get
			{
				return this.m_xw;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000C5DE File Offset: 0x0000A7DE
		public bool ShouldWriteBindings
		{
			get
			{
				return (this.m_options & ModelingSerializationOptions.OmitBindings) == ModelingSerializationOptions.None;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C5EB File Offset: 0x0000A7EB
		public void WriteAttribute(string name, object value)
		{
			this.m_xw.WriteStartAttribute(name);
			this.WriteValue(value);
			this.m_xw.WriteEndAttribute();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000C60B File Offset: 0x0000A80B
		public void WriteAttributeIfNonDefault<T>(string name, T value)
		{
			if (ModelingXmlWriter.IsNonDefaultValue<T>(value))
			{
				this.WriteAttribute(name, value);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000C622 File Offset: 0x0000A822
		public void WriteDefaultNamespace()
		{
			this.WriteNamespacePrefix(string.Empty, this.m_defaultNamespace);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000C635 File Offset: 0x0000A835
		public void WriteNamespacePrefix(string prefix, string namespaceUri)
		{
			this.m_xw.WriteAttributeString("xmlns", prefix, "http://www.w3.org/2000/xmlns/", namespaceUri);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000C650 File Offset: 0x0000A850
		public void WriteNamespacePrefixes(IEnumerable<QName> namespacePrefixes)
		{
			foreach (QName qname in namespacePrefixes)
			{
				this.WriteNamespacePrefix(qname.Name, qname.Namespace);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public void WriteStartElement(string name)
		{
			this.m_xw.WriteStartElement(name, this.m_defaultNamespace);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public void WriteEndElement()
		{
			this.m_xw.WriteEndElement();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000C6C9 File Offset: 0x0000A8C9
		public void WriteElement(string name, object value)
		{
			this.WriteStartElement(name);
			this.WriteValue(value);
			this.WriteEndElement();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		public void WriteElementIfNonDefault<T>(string name, T value)
		{
			if (ModelingXmlWriter.IsNonDefaultValue<T>(value))
			{
				this.WriteElement(name, value);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000C6F6 File Offset: 0x0000A8F6
		public void WriteTypedElement(string name, object value)
		{
			this.WriteStartElement(name);
			if (value == null)
			{
				XmlUtil.WriteXsiNilAttribute(this.m_xw);
			}
			else
			{
				XmlUtil.WriteXsiTypeAttribute(this.m_xw, XmlUtil.GetXmlTypeCode(value.GetType()));
				this.WriteValue(value);
			}
			this.WriteEndElement();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C734 File Offset: 0x0000A934
		public void WriteReferenceElement(string name, object item)
		{
			ModelItem modelItem = item as ModelItem;
			Expression expression = item as Expression;
			Grouping grouping = item as Grouping;
			Parameter parameter = item as Parameter;
			if (item == null)
			{
				return;
			}
			if (modelItem != null)
			{
				if ((this.m_options & ModelingSerializationOptions.NameComments) != ModelingSerializationOptions.None)
				{
					this.m_xw.WriteComment(modelItem.Name);
				}
				this.WriteElement(name, modelItem.ID);
				return;
			}
			if (expression != null)
			{
				if (expression.Name.Length == 0)
				{
					throw new InvalidOperationException(DevExceptionMessages.Xml_ReferenceToUnnamedObject(SRObjectDescriptor.FromScope(expression)));
				}
				this.WriteElement(name, expression.Name);
				return;
			}
			else if (grouping != null)
			{
				if (grouping.Name.Length == 0)
				{
					throw new InvalidOperationException(DevExceptionMessages.Xml_ReferenceToUnnamedObject(SRObjectDescriptor.FromScope(grouping)));
				}
				this.WriteElement(name, grouping.Name);
				return;
			}
			else
			{
				if (parameter == null)
				{
					throw new InternalModelingException("Unknown item '" + ((item != null) ? item.ToString() : null) + "'");
				}
				if (parameter.Name.Length == 0)
				{
					throw new InvalidOperationException(DevExceptionMessages.Xml_ReferenceToUnnamedObject(SRObjectDescriptor.FromScope(parameter)));
				}
				this.WriteElement(name, parameter.Name);
				return;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000C840 File Offset: 0x0000AA40
		public void WriteCollectionElement<T>(string elementName, ICollection<T> items) where T : IXmlWriteable
		{
			IList<T> list = items as IList<T>;
			if (items.Count > 0)
			{
				this.WriteStartElement(elementName);
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						T t = list[i];
						t.WriteTo(this);
					}
				}
				else
				{
					foreach (T t2 in items)
					{
						t2.WriteTo(this);
					}
				}
				this.WriteEndElement();
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public void WriteCollectionElement<T>(string elementName, ICollection<T> items, Action<T> writeItem)
		{
			IList<T> list = items as IList<T>;
			if (items.Count > 0)
			{
				this.WriteStartElement(elementName);
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						writeItem(list[i]);
					}
				}
				else
				{
					foreach (T t in items)
					{
						writeItem(t);
					}
				}
				this.WriteEndElement();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public void WriteValue(object value)
		{
			EntityKey entityKey = value as EntityKey;
			if (value is QName)
			{
				QName qname = (QName)value;
				if (this.m_xw.WriteState == WriteState.Element && qname.Namespace.Length > 0 && this.m_xw.LookupPrefix(qname.Namespace) == null)
				{
					this.WriteNamespacePrefix("np", qname.Namespace);
				}
				this.m_xw.WriteQualifiedName(qname.Name, qname.Namespace);
				return;
			}
			if (value is Guid)
			{
				this.m_xw.WriteString(XmlConvert.ToString((Guid)value));
				return;
			}
			if (value is CultureInfo)
			{
				this.m_xw.WriteString(XmlConvertEx.ToString((CultureInfo)value));
				return;
			}
			if (value is Enum)
			{
				this.m_xw.WriteString(XmlConvertEx.ToString((Enum)value));
				return;
			}
			if (entityKey != null)
			{
				this.m_xw.WriteString(entityKey.ToBase64String());
				return;
			}
			this.m_xw.WriteValue(value);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		public void WriteNilAttribute()
		{
			XmlUtil.WriteXsiNilAttribute(this.m_xw);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000CA68 File Offset: 0x0000AC68
		private static bool IsNonDefaultValue<T>(T value)
		{
			if (value != null)
			{
				ref T ptr = ref value;
				if (default(T) == null)
				{
					T t = value;
					ptr = ref t;
				}
				if (!ptr.Equals(default(T)))
				{
					return value as string != string.Empty;
				}
			}
			return false;
		}

		// Token: 0x04000222 RID: 546
		private readonly XmlWriter m_xw;

		// Token: 0x04000223 RID: 547
		private readonly string m_defaultNamespace;

		// Token: 0x04000224 RID: 548
		private readonly ModelingSerializationOptions m_options;
	}
}
