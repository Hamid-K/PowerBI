using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200024B RID: 587
	internal sealed class ODataJsonLightPayloadKindDetectionDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060019FF RID: 6655 RVA: 0x00048443 File Offset: 0x00046643
		internal ODataJsonLightPayloadKindDetectionDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0004CBF8 File Offset: 0x0004ADF8
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

		// Token: 0x06001A01 RID: 6657 RVA: 0x0004CC5C File Offset: 0x0004AE5C
		internal Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataPayloadKindDetectionInfo detectionInfo)
		{
			base.JsonReader.DisableInStreamErrorDetection = true;
			return base.ReadPayloadStartAsync(ODataPayloadKind.Unsupported, null, false, false).FollowOnSuccessWith((Task t) => this.DetectPayloadKindImplementation(detectionInfo)).FollowOnFaultAndCatchExceptionWith((ODataException t) => Enumerable.Empty<ODataPayloadKind>())
				.FollowAlwaysWith(delegate(Task<IEnumerable<ODataPayloadKind>> t)
				{
					base.JsonReader.DisableInStreamErrorDetection = false;
				});
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0004CCE0 File Offset: 0x0004AEE0
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
					if (text != null && text.StartsWith("@odata.", StringComparison.Ordinal))
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
