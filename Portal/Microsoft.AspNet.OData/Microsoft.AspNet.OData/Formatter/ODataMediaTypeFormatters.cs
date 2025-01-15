using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000182 RID: 386
	public static class ODataMediaTypeFormatters
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x000323FB File Offset: 0x000305FB
		public static IList<ODataMediaTypeFormatter> Create()
		{
			return new List<ODataMediaTypeFormatter>
			{
				ODataMediaTypeFormatters.CreateApplicationJson(),
				ODataMediaTypeFormatters.CreateApplicationXml(),
				ODataMediaTypeFormatters.CreateRawValue()
			};
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00032423 File Offset: 0x00030623
		private static void AddSupportedEncodings(MediaTypeFormatter formatter)
		{
			formatter.SupportedEncodings.Add(new UTF8Encoding(false, true));
			formatter.SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0003244C File Offset: 0x0003064C
		private static ODataMediaTypeFormatter CreateRawValue()
		{
			ODataMediaTypeFormatter odataMediaTypeFormatter = ODataMediaTypeFormatters.CreateFormatterWithoutMediaTypes(new ODataPayloadKind[] { ODataPayloadKind.Value });
			odataMediaTypeFormatter.MediaTypeMappings.Add(new ODataPrimitiveValueMediaTypeMapping());
			odataMediaTypeFormatter.MediaTypeMappings.Add(new ODataEnumValueMediaTypeMapping());
			odataMediaTypeFormatter.MediaTypeMappings.Add(new ODataBinaryValueMediaTypeMapping());
			odataMediaTypeFormatter.MediaTypeMappings.Add(new ODataCountMediaTypeMapping());
			return odataMediaTypeFormatter;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000324A8 File Offset: 0x000306A8
		private static ODataMediaTypeFormatter CreateApplicationJson()
		{
			ODataMediaTypeFormatter odataMediaTypeFormatter = ODataMediaTypeFormatters.CreateFormatterWithoutMediaTypes(new ODataPayloadKind[]
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
				ODataPayloadKind.Delta
			});
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataMinimalMetadataStreamingTrue));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataMinimalMetadataStreamingFalse));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataMinimalMetadata));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataFullMetadataStreamingTrue));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataFullMetadataStreamingFalse));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataFullMetadata));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataNoMetadataStreamingTrue));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataNoMetadataStreamingFalse));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonODataNoMetadata));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonStreamingTrue));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJsonStreamingFalse));
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationJson));
			odataMediaTypeFormatter.AddDollarFormatQueryStringMappings();
			MediaTypeFormatterExtensions.AddQueryStringMapping(odataMediaTypeFormatter, "$format", "json", ODataMediaTypes.ApplicationJson);
			return odataMediaTypeFormatter;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000325E4 File Offset: 0x000307E4
		private static ODataMediaTypeFormatter CreateApplicationXml()
		{
			ODataMediaTypeFormatter odataMediaTypeFormatter = ODataMediaTypeFormatters.CreateFormatterWithoutMediaTypes(new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
			odataMediaTypeFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ODataMediaTypes.ApplicationXml));
			odataMediaTypeFormatter.AddDollarFormatQueryStringMappings();
			MediaTypeFormatterExtensions.AddQueryStringMapping(odataMediaTypeFormatter, "$format", "xml", ODataMediaTypes.ApplicationXml);
			return odataMediaTypeFormatter;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00032631 File Offset: 0x00030831
		private static ODataMediaTypeFormatter CreateFormatterWithoutMediaTypes(params ODataPayloadKind[] payloadKinds)
		{
			ODataMediaTypeFormatter odataMediaTypeFormatter = new ODataMediaTypeFormatter(payloadKinds);
			ODataMediaTypeFormatters.AddSupportedEncodings(odataMediaTypeFormatter);
			return odataMediaTypeFormatter;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00032640 File Offset: 0x00030840
		private static void AddDollarFormatQueryStringMappings(this ODataMediaTypeFormatter formatter)
		{
			foreach (MediaTypeHeaderValue mediaTypeHeaderValue in ((IEnumerable<MediaTypeHeaderValue>)formatter.SupportedMediaTypes))
			{
				QueryStringMediaTypeMapping queryStringMediaTypeMapping = new QueryStringMediaTypeMapping("$format", mediaTypeHeaderValue);
				formatter.MediaTypeMappings.Add(queryStringMediaTypeMapping);
			}
		}

		// Token: 0x040003AB RID: 939
		private const string DollarFormat = "$format";

		// Token: 0x040003AC RID: 940
		private const string JsonFormat = "json";

		// Token: 0x040003AD RID: 941
		private const string XmlFormat = "xml";
	}
}
