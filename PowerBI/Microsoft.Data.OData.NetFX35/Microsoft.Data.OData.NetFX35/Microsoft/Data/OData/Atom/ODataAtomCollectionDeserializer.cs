using System;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021A RID: 538
	internal sealed class ODataAtomCollectionDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003A1BF File Offset: 0x000383BF
		internal ODataAtomCollectionDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			this.duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003A1D4 File Offset: 0x000383D4
		internal ODataCollectionStart ReadCollectionStart(out bool isCollectionElementEmpty)
		{
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
			{
				throw new ODataException(Strings.ODataAtomCollectionDeserializer_TopLevelCollectionElementWrongNamespace(base.XmlReader.NamespaceURI, base.XmlReader.ODataNamespace));
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
			odataCollectionStart.Name = base.XmlReader.LocalName;
			isCollectionElementEmpty = base.XmlReader.IsEmptyElement;
			if (!isCollectionElementEmpty)
			{
				base.XmlReader.Read();
			}
			return odataCollectionStart;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003A2B1 File Offset: 0x000384B1
		internal void ReadCollectionEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003A2C0 File Offset: 0x000384C0
		internal object ReadCollectionItem(IEdmTypeReference expectedItemType, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			if (!base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName))
			{
				throw new ODataException(Strings.ODataAtomCollectionDeserializer_WrongCollectionItemElementName(base.XmlReader.LocalName, base.XmlReader.ODataNamespace));
			}
			object obj = base.ReadNonEntityValue(expectedItemType, this.duplicatePropertyNamesChecker, collectionValidator, true, false);
			base.XmlReader.Read();
			return obj;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003A320 File Offset: 0x00038520
		internal void SkipToElementInODataNamespace()
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
					if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
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

		// Token: 0x04000607 RID: 1543
		private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
