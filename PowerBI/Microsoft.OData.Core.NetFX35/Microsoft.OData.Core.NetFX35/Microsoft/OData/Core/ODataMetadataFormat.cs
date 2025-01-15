using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.OData.Core.Atom;

namespace Microsoft.OData.Core
{
	// Token: 0x02000186 RID: 390
	internal sealed class ODataMetadataFormat : ODataFormat
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x00034153 File Offset: 0x00032353
		public override string ToString()
		{
			return "Metadata";
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003415C File Offset: 0x0003235C
		public override IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataMessageInfo messageInfo, ODataMessageReaderSettings settings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			if (!messageInfo.IsResponse)
			{
				return Enumerable.Empty<ODataPayloadKind>();
			}
			return ODataMetadataFormat.DetectPayloadKindImplementation(messageInfo.GetMessageStream.Invoke(), new ODataPayloadKindDetectionInfo(messageInfo.MediaType, messageInfo.Encoding, settings, messageInfo.Model));
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000341AC File Offset: 0x000323AC
		public override ODataInputContext CreateInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			return new ODataMetadataInputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageReaderSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000341FC File Offset: 0x000323FC
		public override ODataOutputContext CreateOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			return new ODataMetadataOutputContext(this, messageInfo.GetMessageStream.Invoke(), messageInfo.Encoding, messageWriterSettings, messageInfo.IsResponse, true, messageInfo.Model, messageInfo.UrlResolver);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003424C File Offset: 0x0003244C
		private static IEnumerable<ODataPayloadKind> DetectPayloadKindImplementation(Stream messageStream, ODataPayloadKindDetectionInfo detectionInfo)
		{
			try
			{
				using (XmlReader xmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, detectionInfo.GetEncoding(), detectionInfo.MessageReaderSettings))
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
