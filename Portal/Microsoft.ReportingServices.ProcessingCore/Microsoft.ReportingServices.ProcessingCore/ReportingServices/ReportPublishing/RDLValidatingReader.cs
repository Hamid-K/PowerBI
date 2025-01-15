using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000394 RID: 916
	internal class RDLValidatingReader : XmlReader
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x000B3354 File Offset: 0x000B1554
		internal RDLValidatingReader(Stream stream, List<Pair<string, Stream>> namespaceSchemaStreamMap)
		{
			try
			{
				this.m_validationNamespaceList = new List<string>();
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				foreach (Pair<string, Stream> pair in namespaceSchemaStreamMap)
				{
					this.m_validationNamespaceList.Add(pair.First);
					xmlReaderSettings.Schemas.Add(pair.First, XmlReader.Create(pair.Second));
				}
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationEventHandler += this.ValidationCallBack;
				xmlReaderSettings.ProhibitDtd = true;
				xmlReaderSettings.CloseInput = true;
				xmlReaderSettings.XmlResolver = new Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.XmlNullResolver();
				this.m_reader = XmlReader.Create(stream, xmlReaderSettings);
			}
			catch (SynchronizationLockException ex)
			{
				throw new ReportProcessingException(RPRes.rsProcessingAbortedByError, ErrorCode.rsProcessingError, ex);
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000B343C File Offset: 0x000B163C
		internal int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.m_reader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000B3460 File Offset: 0x000B1660
		internal int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.m_reader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000B3484 File Offset: 0x000B1684
		private static int CompareWithInvariantCulture(string x, string y, bool ignoreCase)
		{
			return string.Compare(x, y, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000B3494 File Offset: 0x000B1694
		public bool Validate(out string message)
		{
			message = null;
			if (!ListUtils.ContainsWithOrdinalComparer(this.m_reader.NamespaceURI, this.m_validationNamespaceList))
			{
				return true;
			}
			bool flag = true;
			ArrayList arrayList = new ArrayList();
			XmlNodeType nodeType = this.m_reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType == XmlNodeType.EndElement)
				{
					if (this.m_rdlElementStack != null)
					{
						Hashtable hashtable = this.m_rdlElementStack[this.m_rdlElementStack.Count - 1];
						if (hashtable != null)
						{
							XmlSchemaComplexType xmlSchemaComplexType = hashtable["_Type"] as XmlSchemaComplexType;
							Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.TraverseParticle(xmlSchemaComplexType.ContentTypeParticle, arrayList);
							for (int i = 0; i < arrayList.Count; i++)
							{
								XmlSchemaElement xmlSchemaElement = arrayList[i] as XmlSchemaElement;
								if (xmlSchemaElement.MinOccurs > 0m && !hashtable.ContainsKey(xmlSchemaElement.Name))
								{
									flag = false;
									message = RDLValidatingReaderStringsWrapper.rdlValidationMissingChildElement(this.m_reader.LocalName, xmlSchemaElement.Name, this.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), this.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
								}
							}
							this.m_rdlElementStack[this.m_rdlElementStack.Count - 1] = null;
						}
						this.m_rdlElementStack.RemoveAt(this.m_rdlElementStack.Count - 1);
					}
				}
			}
			else
			{
				if (this.m_rdlElementStack == null)
				{
					this.m_rdlElementStack = new Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.RdlElementStack();
				}
				XmlSchemaComplexType xmlSchemaComplexType = this.m_reader.SchemaInfo.SchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.TraverseParticle(xmlSchemaComplexType.ContentTypeParticle, arrayList);
				}
				if (!this.m_reader.IsEmptyElement)
				{
					if (xmlSchemaComplexType != null && 1 < arrayList.Count && Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.CompareWithInvariantCulture("ReportItemsType", xmlSchemaComplexType.Name, false) != 0 && Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.CompareWithInvariantCulture("MapLayersType", xmlSchemaComplexType.Name, false) != 0)
					{
						Hashtable hashtable2 = new Hashtable(arrayList.Count);
						hashtable2.Add("_ParentName", this.m_reader.LocalName);
						hashtable2.Add("_Type", xmlSchemaComplexType);
						this.m_rdlElementStack.Add(hashtable2);
					}
					else
					{
						this.m_rdlElementStack.Add(null);
					}
				}
				else if (xmlSchemaComplexType != null)
				{
					for (int j = 0; j < arrayList.Count; j++)
					{
						XmlSchemaElement xmlSchemaElement2 = arrayList[j] as XmlSchemaElement;
						if (xmlSchemaElement2.MinOccurs > 0m)
						{
							flag = false;
							message = RDLValidatingReaderStringsWrapper.rdlValidationMissingChildElement(this.m_reader.LocalName, xmlSchemaElement2.Name, this.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), this.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						}
					}
				}
				if (0 < this.m_reader.Depth && this.m_rdlElementStack != null)
				{
					Hashtable hashtable3 = this.m_rdlElementStack[this.m_reader.Depth - 1];
					if (hashtable3 != null)
					{
						string text = (string)hashtable3[this.m_reader.LocalName];
						if (text == null)
						{
							hashtable3.Add(this.m_reader.LocalName, this.m_reader.NamespaceURI);
						}
						else if (Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.CompareWithInvariantCulture(text, this.m_reader.NamespaceURI, false) == 0)
						{
							flag = false;
							message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidElement(hashtable3["_ParentName"] as string, this.m_reader.LocalName, this.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), this.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						}
						else
						{
							string text2 = this.m_reader.LocalName + "$" + this.m_reader.NamespaceURI;
							if (hashtable3.ContainsKey(text2))
							{
								flag = false;
								message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidElement(hashtable3["_ParentName"] as string, this.m_reader.LocalName, this.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), this.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
							}
							else
							{
								hashtable3.Add(text2, this.m_reader.LocalName);
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000B38E4 File Offset: 0x000B1AE4
		private static void TraverseParticle(XmlSchemaParticle particle, ArrayList elementDeclsInContentModel)
		{
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
				elementDeclsInContentModel.Add(xmlSchemaElement);
				return;
			}
			if (particle is XmlSchemaGroupBase)
			{
				using (XmlSchemaObjectEnumerator enumerator = (particle as XmlSchemaGroupBase).Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						XmlSchemaObject xmlSchemaObject = enumerator.Current;
						Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.TraverseParticle((XmlSchemaParticle)xmlSchemaObject, elementDeclsInContentModel);
					}
					return;
				}
			}
			if (particle is XmlSchemaAny)
			{
				Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.m_processContent = (particle as XmlSchemaAny).ProcessContents;
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000B3978 File Offset: 0x000B1B78
		private void ValidationCallBack(object sender, ValidationEventArgs args)
		{
			if (this.ValidationEventHandler != null)
			{
				this.ValidationEventHandler(sender, args);
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000B398F File Offset: 0x000B1B8F
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.m_reader.Settings;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x000B399C File Offset: 0x000B1B9C
		public override int AttributeCount
		{
			get
			{
				return this.m_reader.AttributeCount;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000B39A9 File Offset: 0x000B1BA9
		public override string BaseURI
		{
			get
			{
				return this.m_reader.BaseURI;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000B39B6 File Offset: 0x000B1BB6
		public override int Depth
		{
			get
			{
				return this.m_reader.Depth;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000B39C3 File Offset: 0x000B1BC3
		public override bool EOF
		{
			get
			{
				return this.m_reader.EOF;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000B39D0 File Offset: 0x000B1BD0
		public override bool HasValue
		{
			get
			{
				return this.m_reader.HasValue;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000B39DD File Offset: 0x000B1BDD
		public override bool IsEmptyElement
		{
			get
			{
				return this.m_reader.IsEmptyElement;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000B39EA File Offset: 0x000B1BEA
		public override string LocalName
		{
			get
			{
				return this.m_reader.LocalName;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000B39F7 File Offset: 0x000B1BF7
		public override XmlNameTable NameTable
		{
			get
			{
				return this.m_reader.NameTable;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000B3A04 File Offset: 0x000B1C04
		public override string NamespaceURI
		{
			get
			{
				return this.m_reader.NamespaceURI;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000B3A11 File Offset: 0x000B1C11
		public override XmlNodeType NodeType
		{
			get
			{
				return this.m_reader.NodeType;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000B3A1E File Offset: 0x000B1C1E
		public override string Prefix
		{
			get
			{
				return this.m_reader.Prefix;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x000B3A2B File Offset: 0x000B1C2B
		public override ReadState ReadState
		{
			get
			{
				return this.m_reader.ReadState;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000B3A38 File Offset: 0x000B1C38
		public override string Value
		{
			get
			{
				return this.m_reader.Value;
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000B3A45 File Offset: 0x000B1C45
		public override void Close()
		{
			this.m_reader.Close();
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000B3A52 File Offset: 0x000B1C52
		public override string GetAttribute(int i)
		{
			return this.m_reader.GetAttribute(i);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000B3A60 File Offset: 0x000B1C60
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.m_reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000B3A6F File Offset: 0x000B1C6F
		public override string GetAttribute(string name)
		{
			return this.m_reader.GetAttribute(name);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000B3A80 File Offset: 0x000B1C80
		internal string GetAttributeLocalName(string name)
		{
			string text = null;
			if (this.m_reader.HasAttributes)
			{
				while (this.m_reader.MoveToNextAttribute())
				{
					if (this.m_reader.LocalName.Equals(name, StringComparison.Ordinal))
					{
						text = this.m_reader.Value;
						break;
					}
				}
				this.m_reader.MoveToElement();
			}
			return text;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000B3ADA File Offset: 0x000B1CDA
		public override string LookupNamespace(string prefix)
		{
			return this.m_reader.LookupNamespace(prefix);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000B3AE8 File Offset: 0x000B1CE8
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.m_reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000B3AF7 File Offset: 0x000B1CF7
		public override bool MoveToAttribute(string name)
		{
			return this.m_reader.MoveToAttribute(name);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000B3B05 File Offset: 0x000B1D05
		public override bool MoveToElement()
		{
			return this.m_reader.MoveToElement();
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000B3B12 File Offset: 0x000B1D12
		public override bool MoveToFirstAttribute()
		{
			return this.m_reader.MoveToFirstAttribute();
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000B3B1F File Offset: 0x000B1D1F
		public override bool MoveToNextAttribute()
		{
			return this.m_reader.MoveToNextAttribute();
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000B3B2C File Offset: 0x000B1D2C
		public override bool Read()
		{
			return this.m_reader.Read();
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000B3B39 File Offset: 0x000B1D39
		public override bool ReadAttributeValue()
		{
			return this.m_reader.ReadAttributeValue();
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000B3B46 File Offset: 0x000B1D46
		public override void ResolveEntity()
		{
			this.m_reader.ResolveEntity();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600256F RID: 9583 RVA: 0x000B3B54 File Offset: 0x000B1D54
		// (remove) Token: 0x06002570 RID: 9584 RVA: 0x000B3B8C File Offset: 0x000B1D8C
		public event ValidationEventHandler ValidationEventHandler;

		// Token: 0x040015D7 RID: 5591
		private Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.RdlElementStack m_rdlElementStack;

		// Token: 0x040015D8 RID: 5592
		protected List<string> m_validationNamespaceList;

		// Token: 0x040015D9 RID: 5593
		protected XmlReader m_reader;

		// Token: 0x040015DA RID: 5594
		protected static XmlSchemaContentProcessing m_processContent;

		// Token: 0x0200095C RID: 2396
		private sealed class RdlElementStack : ArrayList
		{
			// Token: 0x06008007 RID: 32775 RVA: 0x002106E0 File Offset: 0x0020E8E0
			internal RdlElementStack()
			{
			}

			// Token: 0x1700297C RID: 10620
			internal Hashtable this[int index]
			{
				get
				{
					return (Hashtable)base[index];
				}
				set
				{
					base[index] = value;
				}
			}
		}

		// Token: 0x0200095D RID: 2397
		private sealed class XmlNullResolver : XmlUrlResolver
		{
			// Token: 0x0600800A RID: 32778 RVA: 0x00210700 File Offset: 0x0020E900
			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				throw new XmlException("Can't resolve URI reference.", null);
			}
		}
	}
}
