using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x0200004A RID: 74
	public class ODataMediaTypeResolver
	{
		// Token: 0x0600024E RID: 590 RVA: 0x000068AC File Offset: 0x00004AAC
		public virtual IEnumerable<ODataMediaTypeFormat> GetMediaTypeFormats(ODataPayloadKind payloadKind)
		{
			if (ODataMediaTypeResolver.JsonPayloadKindSet.Contains(payloadKind))
			{
				return ODataMediaTypeResolver.JsonMediaTypeFormats;
			}
			if (payloadKind == ODataPayloadKind.Batch)
			{
				return ODataMediaTypeResolver.SpecialMediaTypeFormat[payloadKind].Concat(ODataMediaTypeResolver.JsonMediaTypeFormats);
			}
			return ODataMediaTypeResolver.SpecialMediaTypeFormat[payloadKind];
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000068E7 File Offset: 0x00004AE7
		internal static ODataMediaTypeResolver GetMediaTypeResolver(IServiceProvider container)
		{
			if (container == null)
			{
				return ODataMediaTypeResolver.MediaTypeResolver;
			}
			return container.GetRequiredService<ODataMediaTypeResolver>();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000068F8 File Offset: 0x00004AF8
		private static IEnumerable<ODataMediaTypeFormat> SetJsonLightMediaTypes()
		{
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>("odata.metadata", "minimal");
			KeyValuePair<string, string> keyValuePair2 = new KeyValuePair<string, string>("odata.metadata", "full");
			KeyValuePair<string, string> keyValuePair3 = new KeyValuePair<string, string>("odata.metadata", "none");
			KeyValuePair<string, string> keyValuePair4 = new KeyValuePair<string, string>("odata.streaming", "true");
			KeyValuePair<string, string> keyValuePair5 = new KeyValuePair<string, string>("odata.streaming", "false");
			KeyValuePair<string, string> keyValuePair6 = new KeyValuePair<string, string>("IEEE754Compatible", "false");
			KeyValuePair<string, string> keyValuePair7 = new KeyValuePair<string, string>("IEEE754Compatible", "true");
			return new List<ODataMediaTypeFormat>
			{
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair4, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair4, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair4 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair5, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair5, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair5 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair4, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair4, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair4 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair5, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair5, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair5 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair2 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair4, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair4, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair4 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair5, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair5, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair5 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair3 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair4, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair4, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair4 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair5, keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair5, keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair5 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair6 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json", new KeyValuePair<string, string>[] { keyValuePair7 }), ODataFormat.Json),
				new ODataMediaTypeFormat(new ODataMediaType("application", "json"), ODataFormat.Json)
			};
		}

		// Token: 0x040000F9 RID: 249
		private static readonly HashSet<ODataPayloadKind> JsonPayloadKindSet = new HashSet<ODataPayloadKind>
		{
			ODataPayloadKind.ResourceSet,
			ODataPayloadKind.Resource,
			ODataPayloadKind.Property,
			ODataPayloadKind.EntityReferenceLink,
			ODataPayloadKind.EntityReferenceLinks,
			ODataPayloadKind.Collection,
			ODataPayloadKind.ServiceDocument,
			ODataPayloadKind.Error,
			ODataPayloadKind.Parameter,
			ODataPayloadKind.Delta,
			ODataPayloadKind.IndividualProperty
		};

		// Token: 0x040000FA RID: 250
		private static readonly ODataMediaTypeResolver MediaTypeResolver = new ODataMediaTypeResolver();

		// Token: 0x040000FB RID: 251
		private static IDictionary<ODataPayloadKind, IEnumerable<ODataMediaTypeFormat>> SpecialMediaTypeFormat = new Dictionary<ODataPayloadKind, IEnumerable<ODataMediaTypeFormat>>
		{
			{
				ODataPayloadKind.Batch,
				new ODataMediaTypeFormat[]
				{
					new ODataMediaTypeFormat(new ODataMediaType("multipart", "mixed"), ODataFormat.Batch)
				}
			},
			{
				ODataPayloadKind.Value,
				new ODataMediaTypeFormat[]
				{
					new ODataMediaTypeFormat(new ODataMediaType("text", "plain"), ODataFormat.RawValue)
				}
			},
			{
				ODataPayloadKind.BinaryValue,
				new ODataMediaTypeFormat[]
				{
					new ODataMediaTypeFormat(new ODataMediaType("application", "octet-stream"), ODataFormat.RawValue)
				}
			},
			{
				ODataPayloadKind.MetadataDocument,
				new ODataMediaTypeFormat[]
				{
					new ODataMediaTypeFormat(new ODataMediaType("application", "xml"), ODataFormat.Metadata)
				}
			},
			{
				ODataPayloadKind.Asynchronous,
				new ODataMediaTypeFormat[]
				{
					new ODataMediaTypeFormat(new ODataMediaType("application", "http"), ODataFormat.RawValue)
				}
			}
		};

		// Token: 0x040000FC RID: 252
		private static IEnumerable<ODataMediaTypeFormat> JsonMediaTypeFormats = ODataMediaTypeResolver.SetJsonLightMediaTypes();
	}
}
