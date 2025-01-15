using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200019B RID: 411
	internal sealed class ODataJsonLightPayloadKindDetectionDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00029367 File Offset: 0x00027567
		internal ODataJsonLightPayloadKindDetectionDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00029370 File Offset: 0x00027570
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			IEnumerable<ODataPayloadKind> enumerable;
			try
			{
				base.ReadPayloadStart(ODataPayloadKind.Unsupported, null, false, false);
				enumerable = this.DetectPayloadKindImplementation(detectionInfo);
			}
			catch (ODataException)
			{
				enumerable = Enumerable.Empty<ODataPayloadKind>();
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return enumerable;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000293D4 File Offset: 0x000275D4
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataPayloadKindDetectionInfo detectionInfo)
		{
			if (base.MetadataUriParseResult != null)
			{
				detectionInfo.SetPayloadKindDetectionFormatState(new ODataJsonLightPayloadKindDetectionState(base.MetadataUriParseResult));
				return base.MetadataUriParseResult.DetectedPayloadKinds;
			}
			ODataError odataError = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (!ODataJsonLightReaderUtils.IsAnnotationProperty(text))
				{
					return Enumerable.Empty<ODataPayloadKind>();
				}
				string text2;
				string text3;
				if (ODataJsonLightDeserializer.TryParsePropertyAnnotation(text, out text2, out text3))
				{
					return Enumerable.Empty<ODataPayloadKind>();
				}
				if (ODataJsonLightReaderUtils.IsODataAnnotationName(text))
				{
					if (string.CompareOrdinal("odata.error", text) != 0)
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
					if (odataError != null || !base.JsonReader.StartBufferingAndTryToReadInStreamErrorPropertyValue(out odataError))
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
					base.JsonReader.SkipValue();
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			if (odataError == null)
			{
				return Enumerable.Empty<ODataPayloadKind>();
			}
			return new ODataPayloadKind[] { ODataPayloadKind.Error };
		}
	}
}
