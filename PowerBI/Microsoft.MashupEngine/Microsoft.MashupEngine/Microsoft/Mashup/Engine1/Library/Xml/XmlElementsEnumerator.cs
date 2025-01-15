using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000285 RID: 645
	internal class XmlElementsEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x00035440 File Offset: 0x00033640
		public XmlElementsEnumerator(XmlNodeList nodes)
		{
			this.nodes = nodes;
			this.index = 0;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00035458 File Offset: 0x00033658
		private static Value GetElementValue(XmlElement xmlElement)
		{
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode is XmlText)
				{
					string innerXml = xmlElement.InnerXml;
					if (string.IsNullOrEmpty(innerXml))
					{
						return TextValue.Empty;
					}
					return TextValue.New(innerXml);
				}
				else
				{
					XmlCDataSection xmlCDataSection = xmlNode as XmlCDataSection;
					if (xmlCDataSection != null)
					{
						string data = xmlCDataSection.Data;
						if (string.IsNullOrEmpty(data))
						{
							return TextValue.Empty;
						}
						return TextValue.New(data);
					}
				}
			}
			return new XmlElementsListValue(xmlElement.ChildNodes).ToTable(XmlElementsEnumerator.TableType);
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00035520 File Offset: 0x00033720
		public IValueReference Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00035528 File Offset: 0x00033728
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00035530 File Offset: 0x00033730
		public bool MoveNext()
		{
			while (this.index < this.nodes.Count)
			{
				XmlNodeList xmlNodeList = this.nodes;
				int num = this.index;
				this.index = num + 1;
				XmlElement xmlElement = xmlNodeList[num] as XmlElement;
				if (xmlElement != null && (!(xmlElement.LocalName == "schema") || !(xmlElement.NamespaceURI == "http://www.w3.org/2001/XMLSchema")))
				{
					TableValue tableValue = (xmlElement.HasAttributes ? new XmlAttributesListValue(xmlElement.Attributes) : ListValue.Empty).ToTable(XmlAttributesEnumerator.TableType);
					this.current = RecordValue.New(XmlElementsEnumerator.ElementKeys, new Value[]
					{
						TextValue.New(xmlElement.LocalName),
						TextValue.New(xmlElement.NamespaceURI),
						XmlElementsEnumerator.GetElementValue(xmlElement),
						tableValue
					});
					return true;
				}
			}
			this.current = null;
			return false;
		}

		// Token: 0x040007D8 RID: 2008
		private const string XSDSchemaName = "schema";

		// Token: 0x040007D9 RID: 2009
		private const string XSDSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x040007DA RID: 2010
		private static readonly Keys ElementKeys = Keys.New("Name", "Namespace", "Value", "Attributes");

		// Token: 0x040007DB RID: 2011
		public static readonly TableTypeValue TableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(XmlElementsEnumerator.ElementKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Any,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				XmlAttributesEnumerator.TableType,
				LogicalValue.False
			})
		}), false));

		// Token: 0x040007DC RID: 2012
		private readonly XmlNodeList nodes;

		// Token: 0x040007DD RID: 2013
		private int index;

		// Token: 0x040007DE RID: 2014
		private Value current;
	}
}
