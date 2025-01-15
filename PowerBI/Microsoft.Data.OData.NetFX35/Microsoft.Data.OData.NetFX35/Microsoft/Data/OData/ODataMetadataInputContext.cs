using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.OData.Atom;

namespace Microsoft.Data.OData
{
	// Token: 0x02000201 RID: 513
	internal sealed class ODataMetadataInputContext : ODataInputContext
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x000352A0 File Offset: 0x000334A0
		internal ODataMetadataInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			try
			{
				this.baseXmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, false, messageReaderSettings.MessageQuotas.MaxNestingDepth, messageReaderSettings.ReaderBehavior.ODataNamespace);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00035340 File Offset: 0x00033540
		internal override IEdmModel ReadMetadataDocument()
		{
			return this.ReadMetadataDocumentImplementation();
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00035348 File Offset: 0x00033548
		protected override void DisposeImplementation()
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

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003538C File Offset: 0x0003358C
		private IEdmModel ReadMetadataDocumentImplementation()
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(this.xmlReader, out edmModel, out enumerable))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (EdmError edmError in enumerable)
				{
					stringBuilder.AppendLine(edmError.ToString());
				}
				throw new ODataException(Strings.ODataMetadataInputContext_ErrorReadingMetadata(stringBuilder.ToString()));
			}
			edmModel.LoadODataAnnotations(base.MessageReaderSettings.MessageQuotas.MaxEntityPropertyMappingsPerType);
			return edmModel;
		}

		// Token: 0x04000586 RID: 1414
		private XmlReader baseXmlReader;

		// Token: 0x04000587 RID: 1415
		private BufferingXmlReader xmlReader;
	}
}
