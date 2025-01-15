using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018C RID: 396
	internal sealed class ODataJsonLightEntityReferenceLinkSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00024A25 File Offset: 0x00022C25
		internal ODataJsonLightEntityReferenceLinkSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.metadataUriBuilder = jsonLightOutputContext.CreateMetadataUriBuilder();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00024A64 File Offset: 0x00022C64
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link, entitySet, navigationProperty, true);
			});
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00024ACC File Offset: 0x00022CCC
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks, entitySet, navigationProperty);
			});
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00024B10 File Offset: 0x00022D10
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, bool isTopLevel)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			base.JsonWriter.StartObjectScope();
			Uri uri;
			if (isTopLevel && this.metadataUriBuilder.TryBuildEntityReferenceLinkMetadataUri(entityReferenceLink.SerializationInfo, entitySet, navigationProperty, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
			base.JsonWriter.WriteName("url");
			base.JsonWriter.WriteValue(base.UriToString(entityReferenceLink.Url));
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00024B84 File Offset: 0x00022D84
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks entityReferenceLinks, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			bool flag = false;
			bool flag2 = false;
			base.JsonWriter.StartObjectScope();
			Uri uri;
			if (this.metadataUriBuilder.TryBuildEntityReferenceLinksMetadataUri(entityReferenceLinks.SerializationInfo, entitySet, navigationProperty, out uri))
			{
				base.WriteMetadataUriProperty(uri);
			}
			if (entityReferenceLinks.Count != null)
			{
				flag2 = true;
				this.WriteCountAnnotation(entityReferenceLinks.Count.Value);
			}
			if (entityReferenceLinks.NextPageLink != null)
			{
				flag = true;
				this.WriteNextLinkAnnotation(entityReferenceLinks.NextPageLink);
			}
			base.JsonWriter.WriteValuePropertyName();
			base.JsonWriter.StartArrayScope();
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLinkImplementation(odataEntityReferenceLink, entitySet, null, false);
				}
			}
			base.JsonWriter.EndArrayScope();
			if (!flag2 && entityReferenceLinks.Count != null)
			{
				this.WriteCountAnnotation(entityReferenceLinks.Count.Value);
			}
			if (!flag && entityReferenceLinks.NextPageLink != null)
			{
				this.WriteNextLinkAnnotation(entityReferenceLinks.NextPageLink);
			}
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00024CCC File Offset: 0x00022ECC
		private void WriteNextLinkAnnotation(Uri nextPageLink)
		{
			base.JsonWriter.WriteName("odata.nextLink");
			base.JsonWriter.WriteValue(base.UriToString(nextPageLink));
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00024CF0 File Offset: 0x00022EF0
		private void WriteCountAnnotation(long countValue)
		{
			base.JsonWriter.WriteName("odata.count");
			base.JsonWriter.WriteValue(countValue);
		}

		// Token: 0x04000418 RID: 1048
		private readonly ODataJsonLightMetadataUriBuilder metadataUriBuilder;
	}
}
