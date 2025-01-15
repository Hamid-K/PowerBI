using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001E4 RID: 484
	internal sealed class ODataAtomServiceDocumentMetadataDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00032DAC File Offset: 0x00030FAC
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

		// Token: 0x06000E2F RID: 3631 RVA: 0x00032E54 File Offset: 0x00031054
		internal void ReadTitleElementInWorkspace(AtomWorkspaceMetadata workspaceMetadata)
		{
			if (workspaceMetadata.Title != null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound("workspace"));
			}
			workspaceMetadata.Title = base.ReadTitleElement();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00032E7C File Offset: 0x0003107C
		internal void ReadTitleElementInCollection(AtomResourceCollectionMetadata collectionMetadata, ODataResourceCollectionInfo collectionInfo)
		{
			if (collectionInfo.Name != null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_MultipleTitleElementsFound("collection"));
			}
			AtomTextConstruct atomTextConstruct = base.ReadTitleElement();
			collectionInfo.Name = atomTextConstruct.Text;
			if (base.ReadAtomMetadata)
			{
				collectionMetadata.Title = atomTextConstruct;
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00032EC4 File Offset: 0x000310C4
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

		// Token: 0x06000E32 RID: 3634 RVA: 0x00033058 File Offset: 0x00031258
		internal void ReadAcceptElementInCollection(AtomResourceCollectionMetadata collectionMetadata)
		{
			if (collectionMetadata.Accept != null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentMetadataDeserializer_MultipleAcceptElementsFoundInCollection);
			}
			collectionMetadata.Accept = base.XmlReader.ReadElementValue();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00033080 File Offset: 0x00031280
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

		// Token: 0x04000526 RID: 1318
		private readonly string AtomNamespace;

		// Token: 0x04000527 RID: 1319
		private readonly string AtomCategoryElementName;

		// Token: 0x04000528 RID: 1320
		private readonly string AtomHRefAttributeName;

		// Token: 0x04000529 RID: 1321
		private readonly string AtomPublishingFixedAttributeName;

		// Token: 0x0400052A RID: 1322
		private readonly string AtomCategorySchemeAttributeName;

		// Token: 0x0400052B RID: 1323
		private readonly string AtomCategoryTermAttributeName;

		// Token: 0x0400052C RID: 1324
		private readonly string AtomCategoryLabelAttributeName;

		// Token: 0x0400052D RID: 1325
		private readonly string EmptyNamespace;
	}
}
