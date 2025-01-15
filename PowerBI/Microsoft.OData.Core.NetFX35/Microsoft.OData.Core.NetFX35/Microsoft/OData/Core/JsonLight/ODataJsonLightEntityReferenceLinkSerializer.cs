using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000DC RID: 220
	internal sealed class ODataJsonLightEntityReferenceLinkSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x0001C409 File Offset: 0x0001A609
		internal ODataJsonLightEntityReferenceLinkSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001C430 File Offset: 0x0001A630
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link, true);
			});
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001C480 File Offset: 0x0001A680
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks);
			});
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink, bool isTopLevel)
		{
			this.WriterValidator.ValidateEntityReferenceLink(entityReferenceLink);
			base.JsonWriter.StartObjectScope();
			if (isTopLevel)
			{
				base.WriteContextUriProperty(ODataPayloadKind.EntityReferenceLink, null, null, null);
			}
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.id");
			base.JsonWriter.WriteValue(base.UriToString(entityReferenceLink.Url));
			base.InstanceAnnotationWriter.WriteInstanceAnnotations(entityReferenceLink.InstanceAnnotations, null);
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001C52C File Offset: 0x0001A72C
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks entityReferenceLinks)
		{
			bool flag = false;
			base.JsonWriter.StartObjectScope();
			base.WriteContextUriProperty(ODataPayloadKind.EntityReferenceLinks, null, null, null);
			if (entityReferenceLinks.Count != null)
			{
				this.WriteCountAnnotation(entityReferenceLinks.Count.Value);
			}
			if (entityReferenceLinks.NextPageLink != null)
			{
				flag = true;
				this.WriteNextLinkAnnotation(entityReferenceLinks.NextPageLink);
			}
			base.InstanceAnnotationWriter.WriteInstanceAnnotations(entityReferenceLinks.InstanceAnnotations, null);
			base.JsonWriter.WriteValuePropertyName();
			base.JsonWriter.StartArrayScope();
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					this.WriterValidator.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLinkImplementation(odataEntityReferenceLink, false);
				}
			}
			base.JsonWriter.EndArrayScope();
			if (!flag && entityReferenceLinks.NextPageLink != null)
			{
				this.WriteNextLinkAnnotation(entityReferenceLinks.NextPageLink);
			}
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001C644 File Offset: 0x0001A844
		private void WriteNextLinkAnnotation(Uri nextPageLink)
		{
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
			base.JsonWriter.WriteValue(base.UriToString(nextPageLink));
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001C668 File Offset: 0x0001A868
		private void WriteCountAnnotation(long countValue)
		{
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
			base.JsonWriter.WriteValue(countValue);
		}
	}
}
