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
	// Token: 0x0200009C RID: 156
	internal sealed class ODataMetadataInputContext : ODataInputContext
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001020C File Offset: 0x0000E40C
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

		// Token: 0x06000689 RID: 1673 RVA: 0x0001028C File Offset: 0x0000E48C
		internal override IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			return this.ReadMetadataDocumentImplementation(getReferencedModelReaderFunc);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00010298 File Offset: 0x0000E498
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.baseXmlReader != null)
					{
						((IDisposable)this.baseXmlReader).Dispose();
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

		// Token: 0x0600068B RID: 1675 RVA: 0x000102E4 File Offset: 0x0000E4E4
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

		// Token: 0x04000291 RID: 657
		private XmlReader baseXmlReader;

		// Token: 0x04000292 RID: 658
		private BufferingXmlReader xmlReader;
	}
}
