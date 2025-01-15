using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001C2 RID: 450
	public class ODataEntityReferenceLinkDeserializer : ODataDeserializer
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x0003CD98 File Offset: 0x0003AF98
		public ODataEntityReferenceLinkDeserializer()
			: base(ODataPayloadKind.EntityReferenceLink)
		{
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			ODataEntityReferenceLink odataEntityReferenceLink = messageReader.ReadEntityReferenceLink();
			if (odataEntityReferenceLink != null)
			{
				return ODataEntityReferenceLinkDeserializer.ResolveContentId(odataEntityReferenceLink.Url, readContext);
			}
			return null;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		private static Uri ResolveContentId(Uri uri, ODataDeserializerContext readContext)
		{
			if (uri != null)
			{
				IDictionary<string, string> odataContentIdMapping = readContext.InternalRequest.ODataContentIdMapping;
				if (odataContentIdMapping != null)
				{
					Uri uri2 = new Uri(readContext.InternalUrlHelper.CreateODataLink(new ODataPathSegment[0]));
					Uri uri3 = new Uri(ContentIdHelpers.ResolveContentId(uri.IsAbsoluteUri ? uri2.MakeRelativeUri(uri).OriginalString : uri.OriginalString, odataContentIdMapping), UriKind.RelativeOrAbsolute);
					if (!uri3.IsAbsoluteUri)
					{
						uri3 = new Uri(uri2, uri);
					}
					return uri3;
				}
			}
			return uri;
		}
	}
}
