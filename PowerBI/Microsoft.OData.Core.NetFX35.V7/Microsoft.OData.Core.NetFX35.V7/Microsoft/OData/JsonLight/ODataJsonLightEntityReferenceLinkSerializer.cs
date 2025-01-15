using System;
using System.Collections.Generic;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000209 RID: 521
	internal sealed class ODataJsonLightEntityReferenceLinkSerializer : ODataJsonLightSerializer
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x0003C2D2 File Offset: 0x0003A4D2
		internal ODataJsonLightEntityReferenceLinkSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0003C2DC File Offset: 0x0003A4DC
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link, true);
			});
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0003C310 File Offset: 0x0003A510
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks);
			});
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003C344 File Offset: 0x0003A544
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink, bool isTopLevel)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			base.JsonWriter.StartObjectScope();
			if (isTopLevel)
			{
				base.WriteContextUriProperty(ODataPayloadKind.EntityReferenceLink, null, null, null);
			}
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.id");
			base.JsonWriter.WriteValue(base.UriToString(entityReferenceLink.Url));
			base.InstanceAnnotationWriter.WriteInstanceAnnotations(entityReferenceLink.InstanceAnnotations, null, false);
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0003C3B8 File Offset: 0x0003A5B8
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
			base.InstanceAnnotationWriter.WriteInstanceAnnotations(entityReferenceLinks.InstanceAnnotations, null, false);
			base.JsonWriter.WriteValuePropertyName();
			base.JsonWriter.StartArrayScope();
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
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

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
		private void WriteNextLinkAnnotation(Uri nextPageLink)
		{
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
			base.JsonWriter.WriteValue(base.UriToString(nextPageLink));
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		private void WriteCountAnnotation(long countValue)
		{
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
			base.JsonWriter.WriteValue(countValue);
		}
	}
}
