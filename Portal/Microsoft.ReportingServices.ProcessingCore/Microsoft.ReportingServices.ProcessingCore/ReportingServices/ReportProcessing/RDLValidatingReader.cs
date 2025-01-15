using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200078A RID: 1930
	internal class RDLValidatingReader : XmlValidatingReader
	{
		// Token: 0x06006B9F RID: 27551 RVA: 0x001B5A48 File Offset: 0x001B3C48
		public RDLValidatingReader(XmlReader xmlReader, string validationNamespace)
			: base(xmlReader)
		{
			this.m_validationNamespace = validationNamespace;
			base.XmlResolver = new RDLValidatingReader.XmlNullResolver();
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x001B5A63 File Offset: 0x001B3C63
		private static int CompareWithInvariantCulture(string x, string y, bool ignoreCase)
		{
			return string.Compare(x, y, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x001B5A74 File Offset: 0x001B3C74
		public bool Validate(out string message)
		{
			message = null;
			if (RDLValidatingReader.CompareWithInvariantCulture(this.m_validationNamespace, base.NamespaceURI, false) != 0)
			{
				return true;
			}
			bool flag = true;
			ArrayList arrayList = new ArrayList();
			XmlNodeType nodeType = base.NodeType;
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
							RDLValidatingReader.TraverseParticle(xmlSchemaComplexType.ContentTypeParticle, arrayList);
							for (int i = 0; i < arrayList.Count; i++)
							{
								XmlSchemaElement xmlSchemaElement = arrayList[i] as XmlSchemaElement;
								if (xmlSchemaElement.MinOccurs > 0m && !hashtable.ContainsKey(xmlSchemaElement.Name))
								{
									flag = false;
									message = RDLValidatingReaderStringsWrapper.rdlValidationMissingChildElement(base.LocalName, xmlSchemaElement.Name, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
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
					this.m_rdlElementStack = new RDLValidatingReader.RdlElementStack();
				}
				XmlSchemaComplexType xmlSchemaComplexType = base.SchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					RDLValidatingReader.TraverseParticle(xmlSchemaComplexType.ContentTypeParticle, arrayList);
				}
				if (!base.IsEmptyElement)
				{
					if (xmlSchemaComplexType != null && 1 < arrayList.Count && RDLValidatingReader.CompareWithInvariantCulture("ReportItemsType", xmlSchemaComplexType.Name, false) != 0 && RDLValidatingReader.CompareWithInvariantCulture("MapLayersType", xmlSchemaComplexType.Name, false) != 0)
					{
						Hashtable hashtable2 = new Hashtable(arrayList.Count);
						hashtable2.Add("_ParentName", base.LocalName);
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
							message = RDLValidatingReaderStringsWrapper.rdlValidationMissingChildElement(base.LocalName, xmlSchemaElement2.Name, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						}
					}
				}
				if (0 < base.Depth && this.m_rdlElementStack != null)
				{
					Hashtable hashtable3 = this.m_rdlElementStack[base.Depth - 1];
					if (hashtable3 != null)
					{
						if (hashtable3.ContainsKey(base.LocalName))
						{
							flag = false;
							message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidElement(hashtable3["_ParentName"] as string, base.LocalName, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						}
						else
						{
							hashtable3.Add(base.LocalName, null);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x001B5DB8 File Offset: 0x001B3FB8
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
				foreach (XmlSchemaObject xmlSchemaObject in (particle as XmlSchemaGroupBase).Items)
				{
					RDLValidatingReader.TraverseParticle((XmlSchemaParticle)xmlSchemaObject, elementDeclsInContentModel);
				}
			}
		}

		// Token: 0x0400361D RID: 13853
		private RDLValidatingReader.RdlElementStack m_rdlElementStack;

		// Token: 0x0400361E RID: 13854
		private string m_validationNamespace;

		// Token: 0x02000CE5 RID: 3301
		private sealed class RdlElementStack : ArrayList
		{
			// Token: 0x06008D6A RID: 36202 RVA: 0x0023F5D0 File Offset: 0x0023D7D0
			internal RdlElementStack()
			{
			}

			// Token: 0x17002B65 RID: 11109
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

		// Token: 0x02000CE6 RID: 3302
		private sealed class XmlNullResolver : XmlUrlResolver
		{
			// Token: 0x06008D6D RID: 36205 RVA: 0x0023F5F0 File Offset: 0x0023D7F0
			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				throw new XmlException("Can't resolve URI reference ", null);
			}
		}
	}
}
