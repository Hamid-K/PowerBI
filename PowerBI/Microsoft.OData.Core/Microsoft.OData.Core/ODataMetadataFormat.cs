using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200009B RID: 155
	internal sealed class ODataMetadataFormat : ODataFormat
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00010077 File Offset: 0x0000E277
		public override string ToString()
		{
			return "Metadata";
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001007E File Offset: 0x0000E27E
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			if (!messageInfo.IsResponse)
			{
				return Enumerable.Empty<ODataPayloadKind>();
			}
			return ODataMetadataFormat.DetectPayloadKindImplementation(messageInfo, settings);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000100A1 File Offset: 0x0000E2A1
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataMetadataInputContext(messageInfo, messageReaderSettings);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000100C2 File Offset: 0x0000E2C2
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataMetadataOutputContext(messageInfo, messageWriterSettings);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000100E3 File Offset: 0x0000E2E3
		public override Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			if (!messageInfo.IsResponse)
			{
				return TaskUtils.GetCompletedTask<IEnumerable<ODataPayloadKind>>(Enumerable.Empty<ODataPayloadKind>());
			}
			return Task.FromResult<IEnumerable<ODataPayloadKind>>(ODataMetadataFormat.DetectPayloadKindImplementation(messageInfo, settings));
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00010110 File Offset: 0x0000E310
		public override Task<ODataInputContext> CreateInputContextAsync(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataMetadataFormat_CreateInputContextAsync));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001013B File Offset: 0x0000E33B
		public override Task<ODataOutputContext> CreateOutputContextAsync(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataMetadataFormat_CreateOutputContextAsync));
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00010168 File Offset: 0x0000E368
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
