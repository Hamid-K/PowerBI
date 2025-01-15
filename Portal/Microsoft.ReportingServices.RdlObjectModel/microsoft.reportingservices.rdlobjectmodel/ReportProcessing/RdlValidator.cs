using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200007E RID: 126
	internal sealed class RdlValidator
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000179EC File Offset: 0x00015BEC
		public RdlValidator(XmlReader xmlReader, IEnumerable<string> validationNamespaces)
		{
			this.m_reader = xmlReader;
			this.m_validationNamespaces = new HashSet<string>(validationNamespaces);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00017A08 File Offset: 0x00015C08
		public bool ValidateStartElement(out string message)
		{
			message = null;
			XmlSchemaComplexType xmlSchemaComplexType = null;
			ArrayList arrayList = null;
			if (this.m_rdlElementStack == null)
			{
				this.m_rdlElementStack = new RdlValidator.RdlElementStack();
			}
			if (this.m_reader.SchemaInfo != null && this.m_validationNamespaces.Contains(this.m_reader.NamespaceURI))
			{
				xmlSchemaComplexType = this.m_reader.SchemaInfo.SchemaType as XmlSchemaComplexType;
			}
			if (xmlSchemaComplexType != null)
			{
				arrayList = new ArrayList();
				RdlValidator.TraverseParticle(xmlSchemaComplexType.ContentTypeParticle, arrayList);
			}
			if (xmlSchemaComplexType != null && 1 < arrayList.Count && "MapLayersType" != xmlSchemaComplexType.Name && "ReportItemsType" != xmlSchemaComplexType.Name)
			{
				Hashtable hashtable = new Hashtable(arrayList.Count);
				hashtable.Add("_ParentName", this.m_reader.LocalName);
				hashtable.Add("_Type", xmlSchemaComplexType);
				this.m_rdlElementStack.Add(hashtable);
			}
			else
			{
				this.m_rdlElementStack.Add(null);
			}
			if (0 < this.m_reader.Depth && this.m_rdlElementStack != null)
			{
				Hashtable hashtable2 = this.m_rdlElementStack[this.m_reader.Depth - 1];
				if (hashtable2 != null)
				{
					if (hashtable2.ContainsKey(this.m_reader.LocalName))
					{
						message = this.ValidationMessage("rdlValidationInvalidElement", (string)hashtable2["_ParentName"], this.m_reader.LocalName);
						return false;
					}
					hashtable2.Add(this.m_reader.LocalName, null);
				}
			}
			string text = (this.m_reader.GetAttribute("MustUnderstand") ?? string.Empty).Trim();
			if (!string.IsNullOrEmpty(text))
			{
				foreach (string text2 in text.Split(Array.Empty<char>()))
				{
					string text3 = this.m_reader.LookupNamespace(text2);
					if (!this.m_validationNamespaces.Contains(text3))
					{
						IXmlLineInfo xmlLineInfo = (IXmlLineInfo)this.m_reader;
						int lineNumber = xmlLineInfo.LineNumber;
						int linePosition = xmlLineInfo.LinePosition;
						message = RDLValidatingReaderStringsWrapper.rdlValidationUnknownRequiredNamespaces(text2, text3, "Microsoft SQL Server 2017 Release Candidate 1 (RC1)", lineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), linePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00017C48 File Offset: 0x00015E48
		public bool ValidateEndElement(out string message)
		{
			message = null;
			bool flag = true;
			if (this.m_rdlElementStack != null)
			{
				Hashtable hashtable = this.m_rdlElementStack[this.m_rdlElementStack.Count - 1];
				if (hashtable != null)
				{
					XmlSchemaComplexType xmlSchemaComplexType = hashtable["_Type"] as XmlSchemaComplexType;
					ArrayList arrayList = new ArrayList();
					RdlValidator.TraverseParticle(xmlSchemaComplexType.ContentTypeParticle, arrayList);
					for (int i = 0; i < arrayList.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement = arrayList[i] as XmlSchemaElement;
						if (xmlSchemaElement.MinOccurs > 0m && !hashtable.ContainsKey(xmlSchemaElement.Name))
						{
							flag = false;
							message = this.ValidationMessage("rdlValidationMissingChildElement", hashtable["_ParentName"] as string, xmlSchemaElement.Name);
						}
					}
					this.m_rdlElementStack[this.m_rdlElementStack.Count - 1] = null;
				}
				this.m_rdlElementStack.RemoveAt(this.m_rdlElementStack.Count - 1);
			}
			return flag;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00017D40 File Offset: 0x00015F40
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
					RdlValidator.TraverseParticle((XmlSchemaParticle)xmlSchemaObject, elementDeclsInContentModel);
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00017DBC File Offset: 0x00015FBC
		private string ValidationMessage(string id, string parentType, string childType)
		{
			int num = 0;
			int num2 = 0;
			IXmlLineInfo xmlLineInfo = this.m_reader as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				num = xmlLineInfo.LineNumber;
				num2 = xmlLineInfo.LinePosition;
			}
			return RDLValidatingReaderStringsWrapper.Keys.GetString(id, parentType, childType, num.ToString(CultureInfo.InvariantCulture.NumberFormat), num2.ToString(CultureInfo.InvariantCulture.NumberFormat));
		}

		// Token: 0x04000117 RID: 279
		private const string MustUnderstandAttributeName = "MustUnderstand";

		// Token: 0x04000118 RID: 280
		private readonly XmlReader m_reader;

		// Token: 0x04000119 RID: 281
		private RdlValidator.RdlElementStack m_rdlElementStack;

		// Token: 0x0400011A RID: 282
		private readonly HashSet<string> m_validationNamespaces;

		// Token: 0x02000333 RID: 819
		private sealed class RdlElementStack : ArrayList
		{
			// Token: 0x17000735 RID: 1845
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
	}
}
