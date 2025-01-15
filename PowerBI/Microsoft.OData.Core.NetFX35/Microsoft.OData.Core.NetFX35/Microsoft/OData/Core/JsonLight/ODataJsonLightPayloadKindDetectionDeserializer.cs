using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000F1 RID: 241
	internal sealed class ODataJsonLightPayloadKindDetectionDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x00021811 File Offset: 0x0001FA11
		internal ODataJsonLightPayloadKindDetectionDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002181C File Offset: 0x0001FA1C
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

		// Token: 0x06000940 RID: 2368 RVA: 0x00021880 File Offset: 0x0001FA80
		private IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataPayloadKindDetectionInfo detectionInfo)
		{
			if (base.ContextUriParseResult != null)
			{
				return base.ContextUriParseResult.DetectedPayloadKinds;
			}
			ODataError odataError = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				string text3;
				if (ODataJsonLightDeserializer.TryParsePropertyAnnotation(text, out text2, out text3))
				{
					return Enumerable.Empty<ODataPayloadKind>();
				}
				if (ODataJsonLightReaderUtils.IsAnnotationProperty(text))
				{
					if (text != null && text.StartsWith('@' + "odata.", 4))
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
					base.JsonReader.SkipValue();
				}
				else
				{
					if (string.CompareOrdinal("error", text) != 0)
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
					if (odataError != null || !base.JsonReader.StartBufferingAndTryToReadInStreamErrorPropertyValue(out odataError))
					{
						return Enumerable.Empty<ODataPayloadKind>();
					}
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
