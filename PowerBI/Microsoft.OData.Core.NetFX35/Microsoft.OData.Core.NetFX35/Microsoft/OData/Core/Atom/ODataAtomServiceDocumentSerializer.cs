using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200005F RID: 95
	internal sealed class ODataAtomServiceDocumentSerializer : ODataAtomSerializer
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0000E850 File Offset: 0x0000CA50
		internal ODataAtomServiceDocumentSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.atomServiceDocumentMetadataSerializer = new ODataAtomServiceDocumentMetadataSerializer(atomOutputContext);
			this.contextUriBuilder = atomOutputContext.CreateContextUriBuilder();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E874 File Offset: 0x0000CA74
		internal void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			base.WritePayloadStart();
			base.XmlWriter.WriteStartElement(string.Empty, "service", "http://www.w3.org/2007/app");
			if (base.MessageWriterSettings.PayloadBaseUri != null)
			{
				base.XmlWriter.WriteAttributeString("base", "http://www.w3.org/XML/1998/namespace", base.MessageWriterSettings.PayloadBaseUri.AbsoluteUri);
			}
			base.XmlWriter.WriteAttributeString("xmlns", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2007/app");
			base.XmlWriter.WriteAttributeString("atom", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/Atom");
			base.XmlWriter.WriteAttributeString("m", "http://www.w3.org/2000/xmlns/", "http://docs.oasis-open.org/odata/ns/metadata");
			base.WriteContextUriProperty(this.contextUriBuilder.BuildContextUri(ODataPayloadKind.ServiceDocument, null));
			base.XmlWriter.WriteStartElement(string.Empty, "workspace", "http://www.w3.org/2007/app");
			this.atomServiceDocumentMetadataSerializer.WriteServiceDocumentMetadata(serviceDocument);
			if (serviceDocument.EntitySets != null)
			{
				foreach (ODataEntitySetInfo odataEntitySetInfo in serviceDocument.EntitySets)
				{
					this.WriteEntitySetInfo(odataEntitySetInfo);
				}
			}
			if (serviceDocument.Singletons != null)
			{
				foreach (ODataSingletonInfo odataSingletonInfo in serviceDocument.Singletons)
				{
					this.WriteSingletonInfo(odataSingletonInfo);
				}
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			if (serviceDocument.FunctionImports != null)
			{
				foreach (ODataFunctionImportInfo odataFunctionImportInfo in serviceDocument.FunctionImports)
				{
					if (odataFunctionImportInfo == null)
					{
						throw new ODataException(Strings.ValidationUtils_WorkspaceResourceMustNotContainNullItem);
					}
					if (!hashSet.Contains(odataFunctionImportInfo.Name))
					{
						hashSet.Add(odataFunctionImportInfo.Name);
						this.WriteFunctionImportInfo(odataFunctionImportInfo);
					}
				}
			}
			base.XmlWriter.WriteEndElement();
			base.XmlWriter.WriteEndElement();
			base.WritePayloadEnd();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000EA90 File Offset: 0x0000CC90
		private void WriteEntitySetInfo(ODataEntitySetInfo entitySetInfo)
		{
			ValidationUtils.ValidateServiceDocumentElement(entitySetInfo, ODataFormat.Atom);
			base.XmlWriter.WriteStartElement(string.Empty, "collection", "http://www.w3.org/2007/app");
			base.XmlWriter.WriteAttributeString("href", base.UriToUrlAttributeValue(entitySetInfo.Url));
			this.atomServiceDocumentMetadataSerializer.WriteEntitySetInfoMetadata(entitySetInfo);
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000EAF5 File Offset: 0x0000CCF5
		private void WriteSingletonInfo(ODataSingletonInfo singletonInfo)
		{
			this.WriteNonEntitySetInfoElement(singletonInfo, "singleton");
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000EB03 File Offset: 0x0000CD03
		private void WriteFunctionImportInfo(ODataFunctionImportInfo functionInfo)
		{
			this.WriteNonEntitySetInfoElement(functionInfo, "function-import");
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000EB14 File Offset: 0x0000CD14
		private void WriteNonEntitySetInfoElement(ODataServiceDocumentElement serviceDocumentElement, string elementName)
		{
			ValidationUtils.ValidateServiceDocumentElement(serviceDocumentElement, ODataFormat.Atom);
			base.XmlWriter.WriteStartElement("m", elementName, "http://docs.oasis-open.org/odata/ns/metadata");
			base.XmlWriter.WriteAttributeString("href", base.UriToUrlAttributeValue(serviceDocumentElement.Url));
			this.atomServiceDocumentMetadataSerializer.WriteTextConstruct("atom", "title", "http://www.w3.org/2005/Atom", serviceDocumentElement.Name);
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x040001D9 RID: 473
		private readonly ODataContextUriBuilder contextUriBuilder;

		// Token: 0x040001DA RID: 474
		private readonly ODataAtomServiceDocumentMetadataSerializer atomServiceDocumentMetadataSerializer;
	}
}
