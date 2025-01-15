using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200005C RID: 92
	internal sealed class ODataAtomServiceDocumentDeserializer : ODataAtomDeserializer
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000DB78 File Offset: 0x0000BD78
		internal ODataAtomServiceDocumentDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.AtomPublishingServiceElementName = nameTable.Add("service");
			this.AtomPublishingWorkspaceElementName = nameTable.Add("workspace");
			this.AtomPublishingCollectionElementName = nameTable.Add("collection");
			this.AtomPublishingAcceptElementName = nameTable.Add("accept");
			this.AtomPublishingCategoriesElementName = nameTable.Add("categories");
			this.AtomHRefAttributeName = nameTable.Add("href");
			this.AtomPublishingNamespace = nameTable.Add("http://www.w3.org/2007/app");
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
			this.AtomTitleElementName = nameTable.Add("title");
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.ODataMetadataNamespace = nameTable.Add("http://docs.oasis-open.org/odata/ns/metadata");
			this.ODataFunctionImportElementName = nameTable.Add("function-import");
			this.ODataSingletonElementName = nameTable.Add("singleton");
			this.ODataNameAttribute = nameTable.Add("name");
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000DC86 File Offset: 0x0000BE86
		private ODataAtomServiceDocumentMetadataDeserializer ServiceDocumentMetadataDeserializer
		{
			get
			{
				if (this.serviceDocumentMetadataDeserializer == null)
				{
					this.serviceDocumentMetadataDeserializer = new ODataAtomServiceDocumentMetadataDeserializer(base.AtomInputContext);
				}
				return this.serviceDocumentMetadataDeserializer;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		internal ODataServiceDocument ReadServiceDocument()
		{
			base.ReadPayloadStart();
			if (!base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace) || !base.XmlReader.LocalNameEquals(this.AtomPublishingServiceElementName))
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_ServiceDocumentRootElementWrongNameOrNamespace(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataServiceDocument odataServiceDocument = null;
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				odataServiceDocument = this.ReadWorkspace();
			}
			if (odataServiceDocument == null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_MissingWorkspaceElement);
			}
			this.SkipToElementInAtomPublishingNamespace();
			if (base.XmlReader.NodeType != 1)
			{
				base.XmlReader.Read();
				base.ReadPayloadEnd();
				return odataServiceDocument;
			}
			if (base.XmlReader.LocalNameEquals(this.AtomPublishingWorkspaceElementName))
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_MultipleWorkspaceElementsFound);
			}
			throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument(base.XmlReader.LocalName));
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000DD90 File Offset: 0x0000BF90
		private ODataServiceDocument ReadWorkspace()
		{
			bool enableAtomMetadataReading = base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			this.SkipToElementInAtomPublishingNamespace();
			if (base.XmlReader.NodeType == 15)
			{
				return null;
			}
			if (!base.XmlReader.LocalNameEquals(this.AtomPublishingWorkspaceElementName))
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument(base.XmlReader.LocalName));
			}
			List<ODataEntitySetInfo> list = new List<ODataEntitySetInfo>();
			List<ODataFunctionImportInfo> list2 = new List<ODataFunctionImportInfo>();
			List<ODataSingletonInfo> list3 = new List<ODataSingletonInfo>();
			AtomWorkspaceMetadata atomWorkspaceMetadata = null;
			if (enableAtomMetadataReading)
			{
				atomWorkspaceMetadata = new AtomWorkspaceMetadata();
			}
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				for (;;)
				{
					base.XmlReader.SkipInsignificantNodes();
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace))
					{
						if (!base.XmlReader.LocalNameEquals(this.AtomPublishingCollectionElementName))
						{
							break;
						}
						ODataEntitySetInfo odataEntitySetInfo = this.ReadEntitySet();
						list.Add(odataEntitySetInfo);
					}
					else if (base.XmlReader.NamespaceEquals(this.ODataMetadataNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.ODataFunctionImportElementName))
						{
							ODataFunctionImportInfo odataFunctionImportInfo = this.ReadFunctionImportInfo();
							list2.Add(odataFunctionImportInfo);
						}
						else
						{
							if (!base.XmlReader.LocalNameEquals(this.ODataSingletonElementName))
							{
								goto IL_0167;
							}
							ODataSingletonInfo odataSingletonInfo = this.ReadSingletonInfo();
							list3.Add(odataSingletonInfo);
						}
					}
					else if (enableAtomMetadataReading && base.XmlReader.NamespaceEquals(this.AtomNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.AtomTitleElementName))
						{
							this.ServiceDocumentMetadataDeserializer.ReadTitleElementInWorkspace(atomWorkspaceMetadata);
						}
						else
						{
							base.XmlReader.Skip();
						}
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == 15)
					{
						goto IL_01EC;
					}
				}
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInWorkspace(base.XmlReader.LocalName));
				IL_0167:
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedODataElementInWorkspace(base.XmlReader.LocalName));
			}
			IL_01EC:
			base.XmlReader.Read();
			ODataServiceDocument odataServiceDocument = new ODataServiceDocument
			{
				EntitySets = new ReadOnlyEnumerable<ODataEntitySetInfo>(list),
				FunctionImports = new ReadOnlyCollection<ODataFunctionImportInfo>(list2),
				Singletons = new ReadOnlyCollection<ODataSingletonInfo>(list3)
			};
			if (enableAtomMetadataReading)
			{
				odataServiceDocument.SetAnnotation<AtomWorkspaceMetadata>(atomWorkspaceMetadata);
			}
			return odataServiceDocument;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000DFD5 File Offset: 0x0000C1D5
		private ODataEntitySetInfo ReadEntitySet()
		{
			return this.ReadServiceDocumentElement<ODataEntitySetInfo>();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000DFDD File Offset: 0x0000C1DD
		private ODataFunctionImportInfo ReadFunctionImportInfo()
		{
			return this.ReadServiceDocumentElement<ODataFunctionImportInfo>();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		private ODataSingletonInfo ReadSingletonInfo()
		{
			return this.ReadServiceDocumentElement<ODataSingletonInfo>();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		private T ReadServiceDocumentElement<T>() where T : ODataServiceDocumentElement, new()
		{
			T t = new T();
			string attribute = base.XmlReader.GetAttribute(this.AtomHRefAttributeName, this.EmptyNamespace);
			ValidationUtils.ValidateServiceDocumentElementUrl(attribute);
			t.Url = base.ProcessUriFromPayload(attribute, base.XmlReader.XmlBaseUri);
			bool enableAtomMetadataReading = base.MessageReaderSettings.EnableAtomMetadataReading;
			string attribute2 = base.XmlReader.GetAttribute(this.ODataNameAttribute, this.ODataMetadataNamespace);
			t.Name = attribute2;
			AtomResourceCollectionMetadata atomResourceCollectionMetadata = null;
			if (enableAtomMetadataReading)
			{
				atomResourceCollectionMetadata = new AtomResourceCollectionMetadata();
			}
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				bool flag = false;
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.AtomPublishingCategoriesElementName))
						{
							if (enableAtomMetadataReading)
							{
								this.ServiceDocumentMetadataDeserializer.ReadCategoriesElementInCollection(atomResourceCollectionMetadata);
							}
							else
							{
								base.XmlReader.Skip();
							}
						}
						else
						{
							if (!base.XmlReader.LocalNameEquals(this.AtomPublishingAcceptElementName))
							{
								break;
							}
							if (enableAtomMetadataReading)
							{
								this.ServiceDocumentMetadataDeserializer.ReadAcceptElementInCollection(atomResourceCollectionMetadata);
							}
							else
							{
								base.XmlReader.Skip();
							}
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.AtomTitleElementName))
						{
							if (flag)
							{
								goto Block_12;
							}
							this.ServiceDocumentMetadataDeserializer.ReadTitleElementInCollection(atomResourceCollectionMetadata, t);
							flag = true;
						}
						else
						{
							base.XmlReader.Skip();
						}
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == 15)
					{
						goto IL_01E5;
					}
				}
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInResourceCollection(base.XmlReader.LocalName));
				Block_12:
				throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound("collection"));
			}
			IL_01E5:
			base.XmlReader.Read();
			if (enableAtomMetadataReading)
			{
				t.SetAnnotation<AtomResourceCollectionMetadata>(atomResourceCollectionMetadata);
			}
			return t;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E204 File Offset: 0x0000C404
		private void SkipToElementInAtomPublishingNamespace()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != 1)
				{
					if (nodeType == 15)
					{
						return;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace))
					{
						break;
					}
					base.XmlReader.Skip();
				}
			}
		}

		// Token: 0x040001C2 RID: 450
		private readonly string AtomPublishingServiceElementName;

		// Token: 0x040001C3 RID: 451
		private readonly string AtomPublishingWorkspaceElementName;

		// Token: 0x040001C4 RID: 452
		private readonly string AtomHRefAttributeName;

		// Token: 0x040001C5 RID: 453
		private readonly string AtomPublishingCollectionElementName;

		// Token: 0x040001C6 RID: 454
		private readonly string AtomPublishingCategoriesElementName;

		// Token: 0x040001C7 RID: 455
		private readonly string AtomPublishingAcceptElementName;

		// Token: 0x040001C8 RID: 456
		private readonly string AtomPublishingNamespace;

		// Token: 0x040001C9 RID: 457
		private readonly string AtomNamespace;

		// Token: 0x040001CA RID: 458
		private readonly string AtomTitleElementName;

		// Token: 0x040001CB RID: 459
		private readonly string EmptyNamespace;

		// Token: 0x040001CC RID: 460
		private readonly string ODataMetadataNamespace;

		// Token: 0x040001CD RID: 461
		private readonly string ODataFunctionImportElementName;

		// Token: 0x040001CE RID: 462
		private readonly string ODataSingletonElementName;

		// Token: 0x040001CF RID: 463
		private readonly string ODataNameAttribute;

		// Token: 0x040001D0 RID: 464
		private ODataAtomServiceDocumentMetadataDeserializer serviceDocumentMetadataDeserializer;
	}
}
