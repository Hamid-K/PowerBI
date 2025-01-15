using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C6 RID: 454
	internal sealed class ODataVerboseJsonEntityReferenceLinkSerializer : ODataVerboseJsonSerializer
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x0002F793 File Offset: 0x0002D993
		internal ODataVerboseJsonEntityReferenceLinkSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002F7B8 File Offset: 0x0002D9B8
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link);
			});
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002F824 File Offset: 0x0002DA24
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.WriteEntityReferenceLinksImplementation(entityReferenceLinks, this.Version >= ODataVersion.V2 && this.WritingResponse);
			});
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002F858 File Offset: 0x0002DA58
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("uri");
			base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(entityReferenceLink.Url));
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002F8A8 File Offset: 0x0002DAA8
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks entityReferenceLinks, bool includeResultsWrapper)
		{
			if (includeResultsWrapper)
			{
				base.JsonWriter.StartObjectScope();
			}
			if (entityReferenceLinks.Count != null)
			{
				base.JsonWriter.WriteName("__count");
				base.JsonWriter.WriteValue(entityReferenceLinks.Count.Value);
			}
			if (includeResultsWrapper)
			{
				base.JsonWriter.WriteDataArrayName();
			}
			base.JsonWriter.StartArrayScope();
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLinkImplementation(odataEntityReferenceLink);
				}
			}
			base.JsonWriter.EndArrayScope();
			if (entityReferenceLinks.NextPageLink != null)
			{
				base.JsonWriter.WriteName("__next");
				base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(entityReferenceLinks.NextPageLink));
			}
			if (includeResultsWrapper)
			{
				base.JsonWriter.EndObjectScope();
			}
		}
	}
}
