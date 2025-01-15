using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200002D RID: 45
	internal sealed class ODataAtomCollectionDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00005B37 File Offset: 0x00003D37
		internal ODataAtomCollectionDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			this.duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005B4C File Offset: 0x00003D4C
		internal ODataCollectionStart ReadCollectionStart(out bool isCollectionElementEmpty)
		{
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
			{
				throw new ODataException(Strings.ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace(base.XmlReader.NamespaceURI, base.XmlReader.ODataMetadataNamespace));
			}
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName) || base.XmlReader.LocalNameEquals(this.ODataNullAttributeName)))
				{
					throw new ODataException(Strings.ODataAtomCollectionDeserializer_TypeOrNullAttributeNotAllowed);
				}
			}
			base.XmlReader.MoveToElement();
			ODataCollectionStart odataCollectionStart = new ODataCollectionStart();
			isCollectionElementEmpty = base.XmlReader.IsEmptyElement;
			if (!isCollectionElementEmpty)
			{
				base.XmlReader.Read();
			}
			return odataCollectionStart;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005C18 File Offset: 0x00003E18
		internal void ReadCollectionEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005C28 File Offset: 0x00003E28
		internal object ReadCollectionItem(IEdmTypeReference expectedItemType, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			if (!base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName))
			{
				throw new ODataException(Strings.ODataAtomCollectionDeserializer_WrongCollectionItemElementName(base.XmlReader.LocalName, base.XmlReader.ODataNamespace));
			}
			object obj = base.ReadNonEntityValue(expectedItemType, this.duplicatePropertyNamesChecker, collectionValidator, true);
			base.XmlReader.Read();
			return obj;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005C88 File Offset: 0x00003E88
		internal void SkipToElementInODataMetadataNamespace()
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
					if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
					{
						break;
					}
					base.XmlReader.Skip();
				}
				if (base.XmlReader.EOF)
				{
					return;
				}
			}
		}

		// Token: 0x04000116 RID: 278
		private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
