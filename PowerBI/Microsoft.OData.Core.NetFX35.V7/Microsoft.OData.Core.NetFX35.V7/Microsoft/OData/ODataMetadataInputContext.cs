using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000076 RID: 118
	internal sealed class ODataMetadataInputContext : ODataInputContext
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0000CE68 File Offset: 0x0000B068
		public ODataMetadataInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: base(ODataFormat.Metadata, messageInfo, messageReaderSettings)
		{
			try
			{
				this.baseXmlReader = ODataMetadataReaderUtils.CreateXmlReader(messageInfo.MessageStream, messageInfo.Encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, false, messageReaderSettings.MessageQuotas.MaxNestingDepth);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					messageInfo.MessageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		internal override IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			return this.ReadMetadataDocumentImplementation(getReferencedModelReaderFunc);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.baseXmlReader != null)
					{
						this.baseXmlReader.Dispose();
					}
				}
				finally
				{
					this.baseXmlReader = null;
					this.xmlReader = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000CF40 File Offset: 0x0000B140
		private IEdmModel ReadMetadataDocumentImplementation(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!CsdlReader.TryParse(this.xmlReader, getReferencedModelReaderFunc, out edmModel, out enumerable))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (EdmError edmError in enumerable)
				{
					stringBuilder.AppendLine(edmError.ToString());
				}
				throw new ODataException(Strings.ODataMetadataInputContext_ErrorReadingMetadata(stringBuilder.ToString()));
			}
			return edmModel;
		}

		// Token: 0x0400022B RID: 555
		private XmlReader baseXmlReader;

		// Token: 0x0400022C RID: 556
		private BufferingXmlReader xmlReader;
	}
}
