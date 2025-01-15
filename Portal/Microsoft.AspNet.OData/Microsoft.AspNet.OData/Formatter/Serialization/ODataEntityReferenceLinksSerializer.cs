using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001A0 RID: 416
	public class ODataEntityReferenceLinksSerializer : ODataSerializer
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x00037191 File Offset: 0x00035391
		public ODataEntityReferenceLinksSerializer()
			: base(ODataPayloadKind.EntityReferenceLinks)
		{
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003719C File Offset: 0x0003539C
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (graph != null)
			{
				ODataEntityReferenceLinks odataEntityReferenceLinks = graph as ODataEntityReferenceLinks;
				if (odataEntityReferenceLinks == null)
				{
					IEnumerable<Uri> enumerable = graph as IEnumerable<Uri>;
					if (enumerable == null)
					{
						throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
						{
							base.GetType().Name,
							graph.GetType().FullName
						}));
					}
					ODataEntityReferenceLinks odataEntityReferenceLinks2 = new ODataEntityReferenceLinks();
					odataEntityReferenceLinks2.Links = enumerable.Select((Uri uri) => new ODataEntityReferenceLink
					{
						Url = uri
					});
					odataEntityReferenceLinks = odataEntityReferenceLinks2;
					if (writeContext.Request != null)
					{
						odataEntityReferenceLinks.Count = writeContext.InternalRequest.Context.TotalCount;
					}
				}
				messageWriter.WriteEntityReferenceLinks(odataEntityReferenceLinks);
			}
		}
	}
}
