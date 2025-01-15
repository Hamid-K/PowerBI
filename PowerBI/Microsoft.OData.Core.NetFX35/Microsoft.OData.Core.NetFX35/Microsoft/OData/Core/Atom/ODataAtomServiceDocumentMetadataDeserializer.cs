using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200005D RID: 93
	internal sealed class ODataAtomServiceDocumentMetadataDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000E258 File Offset: 0x0000C458
		internal ODataAtomServiceDocumentMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
			this.AtomCategoryElementName = nameTable.Add("category");
			this.AtomHRefAttributeName = nameTable.Add("href");
			this.AtomPublishingFixedAttributeName = nameTable.Add("fixed");
			this.AtomCategorySchemeAttributeName = nameTable.Add("scheme");
			this.AtomCategoryTermAttributeName = nameTable.Add("term");
			this.AtomCategoryLabelAttributeName = nameTable.Add("label");
			this.EmptyNamespace = nameTable.Add(string.Empty);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E300 File Offset: 0x0000C500
		internal void ReadTitleElementInWorkspace(AtomWorkspaceMetadata workspaceMetadata)
		{
			if (workspaceMetadata.Title != null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound("workspace"));
			}
			workspaceMetadata.Title = base.ReadTitleElement();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E328 File Offset: 0x0000C528
		internal void ReadTitleElementInCollection(AtomResourceCollectionMetadata collectionMetadata, ODataServiceDocumentElement odataServiceDocumentElement)
		{
			AtomTextConstruct atomTextConstruct = base.ReadTitleElement();
			if (odataServiceDocumentElement.Name == null)
			{
				odataServiceDocumentElement.Name = atomTextConstruct.Text;
			}
			if (base.ReadAtomMetadata)
			{
				collectionMetadata.Title = atomTextConstruct;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E360 File Offset: 0x0000C560
		internal void ReadCategoriesElementInCollection(AtomResourceCollectionMetadata collectionMetadata)
		{
			AtomCategoriesMetadata atomCategoriesMetadata = new AtomCategoriesMetadata();
			List<AtomCategoryMetadata> list = new List<AtomCategoryMetadata>();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string value = base.XmlReader.Value;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomHRefAttributeName))
					{
						atomCategoriesMetadata.Href = base.ProcessUriFromPayload(value, base.XmlReader.XmlBaseUri);
					}
					else if (base.XmlReader.LocalNameEquals(this.AtomPublishingFixedAttributeName))
					{
						if (string.CompareOrdinal(value, "yes") == 0)
						{
							atomCategoriesMetadata.Fixed = new bool?(true);
						}
						else
						{
							if (string.CompareOrdinal(value, "no") != 0)
							{
								throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_InvalidFixedAttributeValue(value));
							}
							atomCategoriesMetadata.Fixed = new bool?(false);
						}
					}
					else if (base.XmlReader.LocalNameEquals(this.AtomCategorySchemeAttributeName))
					{
						atomCategoriesMetadata.Scheme = value;
					}
				}
			}
			base.XmlReader.MoveToElement();
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				do
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomNamespace) && base.XmlReader.LocalNameEquals(this.AtomCategoryElementName))
					{
						list.Add(this.ReadCategoryElementInCollection());
					}
				}
				while (base.XmlReader.NodeType != 15);
			}
			base.XmlReader.Read();
			atomCategoriesMetadata.Categories = new ReadOnlyEnumerable<AtomCategoryMetadata>(list);
			collectionMetadata.Categories = atomCategoriesMetadata;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		internal void ReadAcceptElementInCollection(AtomResourceCollectionMetadata collectionMetadata)
		{
			if (collectionMetadata.Accept != null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_MultipleAcceptElementsFoundInCollection);
			}
			collectionMetadata.Accept = base.XmlReader.ReadElementValue();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E51C File Offset: 0x0000C71C
		private AtomCategoryMetadata ReadCategoryElementInCollection()
		{
			AtomCategoryMetadata atomCategoryMetadata = new AtomCategoryMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string value = base.XmlReader.Value;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomCategoryTermAttributeName))
					{
						atomCategoryMetadata.Term = value;
					}
					else if (base.XmlReader.LocalNameEquals(this.AtomCategorySchemeAttributeName))
					{
						atomCategoryMetadata.Scheme = value;
					}
					else if (base.XmlReader.LocalNameEquals(this.AtomCategoryLabelAttributeName))
					{
						atomCategoryMetadata.Label = value;
					}
				}
			}
			return atomCategoryMetadata;
		}

		// Token: 0x040001D1 RID: 465
		private readonly string AtomNamespace;

		// Token: 0x040001D2 RID: 466
		private readonly string AtomCategoryElementName;

		// Token: 0x040001D3 RID: 467
		private readonly string AtomHRefAttributeName;

		// Token: 0x040001D4 RID: 468
		private readonly string AtomPublishingFixedAttributeName;

		// Token: 0x040001D5 RID: 469
		private readonly string AtomCategorySchemeAttributeName;

		// Token: 0x040001D6 RID: 470
		private readonly string AtomCategoryTermAttributeName;

		// Token: 0x040001D7 RID: 471
		private readonly string AtomCategoryLabelAttributeName;

		// Token: 0x040001D8 RID: 472
		private readonly string EmptyNamespace;
	}
}
