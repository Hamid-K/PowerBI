using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200005E RID: 94
	internal sealed class ODataAtomServiceDocumentMetadataSerializer : ODataAtomMetadataSerializer
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		internal ODataAtomServiceDocumentMetadataSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.OData.Core.Atom.AtomTextConstruct.set_Text(System.String)", Justification = "The value 'Default' is a spec defined constant which is not to be localized.")]
		internal void WriteServiceDocumentMetadata(ODataServiceDocument serviceDocument)
		{
			AtomWorkspaceMetadata annotation = serviceDocument.GetAnnotation<AtomWorkspaceMetadata>();
			AtomTextConstruct atomTextConstruct = null;
			if (annotation != null)
			{
				atomTextConstruct = annotation.Title;
			}
			if (atomTextConstruct == null)
			{
				atomTextConstruct = new AtomTextConstruct
				{
					Text = "Default"
				};
			}
			if (base.UseServerFormatBehavior && atomTextConstruct.Kind == AtomTextConstructKind.Text)
			{
				base.WriteElementWithTextContent("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct.Text);
				return;
			}
			base.WriteTextConstruct("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E634 File Offset: 0x0000C834
		internal void WriteEntitySetInfoMetadata(ODataEntitySetInfo entitySetInfo)
		{
			AtomResourceCollectionMetadata annotation = entitySetInfo.GetAnnotation<AtomResourceCollectionMetadata>();
			AtomTextConstruct atomTextConstruct = null;
			if (annotation != null)
			{
				atomTextConstruct = annotation.Title;
			}
			if (entitySetInfo.Name != null)
			{
				if (atomTextConstruct == null)
				{
					atomTextConstruct = new AtomTextConstruct
					{
						Text = entitySetInfo.Name
					};
				}
				else if (string.CompareOrdinal(atomTextConstruct.Text, entitySetInfo.Name) != 0)
				{
					throw new ODataException(Strings.ODataAtomServiceDocumentMetadataSerializer_ResourceCollectionNameAndTitleMismatch(entitySetInfo.Name, atomTextConstruct.Text));
				}
			}
			if (base.UseServerFormatBehavior && atomTextConstruct.Kind == AtomTextConstructKind.Text)
			{
				base.WriteElementWithTextContent("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct.Text);
			}
			else
			{
				base.WriteTextConstruct("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct);
			}
			if (annotation != null)
			{
				string accept = annotation.Accept;
				if (accept != null)
				{
					base.WriteElementWithTextContent(string.Empty, "accept", "http://www.w3.org/2007/app", accept);
				}
				AtomCategoriesMetadata categories = annotation.Categories;
				if (categories != null)
				{
					base.XmlWriter.WriteStartElement(string.Empty, "categories", "http://www.w3.org/2007/app");
					Uri href = categories.Href;
					bool? @fixed = categories.Fixed;
					string scheme = categories.Scheme;
					IEnumerable<AtomCategoryMetadata> categories2 = categories.Categories;
					if (href != null)
					{
						if (@fixed != null || scheme != null || (categories2 != null && Enumerable.Any<AtomCategoryMetadata>(categories2)))
						{
							throw new ODataException(Strings.ODataAtomWriterMetadataUtils_CategoriesHrefWithOtherValues);
						}
						base.XmlWriter.WriteAttributeString("href", base.UriToUrlAttributeValue(href));
					}
					else
					{
						if (@fixed != null)
						{
							base.XmlWriter.WriteAttributeString("fixed", @fixed.Value ? "yes" : "no");
						}
						if (scheme != null)
						{
							base.XmlWriter.WriteAttributeString("scheme", scheme);
						}
						if (categories2 != null)
						{
							foreach (AtomCategoryMetadata atomCategoryMetadata in categories2)
							{
								base.WriteCategory("atom", atomCategoryMetadata.Term, atomCategoryMetadata.Scheme, atomCategoryMetadata.Label);
							}
						}
					}
					base.XmlWriter.WriteEndElement();
				}
			}
		}
	}
}
