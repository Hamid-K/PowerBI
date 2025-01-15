using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000075 RID: 117
	internal sealed class ODataMetadataFormat : ODataFormat
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000CD5F File Offset: 0x0000AF5F
		public override string ToString()
		{
			return "Metadata";
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000CD66 File Offset: 0x0000AF66
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			if (!messageInfo.IsResponse)
			{
				return Enumerable.Empty<ODataPayloadKind>();
			}
			return ODataMetadataFormat.DetectPayloadKindImplementation(messageInfo, settings);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000CD89 File Offset: 0x0000AF89
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataMetadataInputContext(messageInfo, messageReaderSettings);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000CDAA File Offset: 0x0000AFAA
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataMetadataOutputContext(messageInfo, messageWriterSettings);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ODataPayloadKindDetectionInfo odataPayloadKindDetectionInfo = new ODataPayloadKindDetectionInfo(messageInfo, settings);
			try
			{
				using (XmlReader xmlReader = ODataMetadataReaderUtils.CreateXmlReader(messageInfo.MessageStream, odataPayloadKindDetectionInfo.GetEncoding(), odataPayloadKindDetectionInfo.MessageReaderSettings))
				{
					if (xmlReader.TryReadToNextElement() && string.CompareOrdinal("Edmx", xmlReader.LocalName) == 0 && xmlReader.NamespaceURI == "http://docs.oasis-open.org/odata/ns/edmx")
					{
						return new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument };
					}
				}
			}
			catch (XmlException)
			{
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}
	}
}
