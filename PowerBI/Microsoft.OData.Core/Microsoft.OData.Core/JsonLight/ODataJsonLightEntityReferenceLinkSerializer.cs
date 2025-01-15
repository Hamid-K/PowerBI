using System;
using System.Collections.Generic;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000242 RID: 578
	internal sealed class ODataJsonLightEntityReferenceLinkSerializer : ODataJsonLightSerializer
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0004878E File Offset: 0x0004698E
		internal ODataJsonLightEntityReferenceLinkSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00048798 File Offset: 0x00046998
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link, true);
			});
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000487CC File Offset: 0x000469CC
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks);
			});
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00048800 File Offset: 0x00046A00
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

		// Token: 0x06001923 RID: 6435 RVA: 0x00048874 File Offset: 0x00046A74
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

		// Token: 0x06001924 RID: 6436 RVA: 0x00048984 File Offset: 0x00046B84
		private void WriteNextLinkAnnotation(Uri nextPageLink)
		{
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
			base.JsonWriter.WriteValue(base.UriToString(nextPageLink));
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000489A8 File Offset: 0x00046BA8
		private void WriteCountAnnotation(long countValue)
		{
			base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
			base.JsonWriter.WriteValue(countValue);
		}
	}
}
