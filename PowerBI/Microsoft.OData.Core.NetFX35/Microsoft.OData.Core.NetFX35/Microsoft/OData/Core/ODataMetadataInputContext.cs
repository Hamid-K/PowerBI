using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.OData.Core.Atom;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Core
{
	// Token: 0x02000187 RID: 391
	internal sealed class ODataMetadataInputContext : ODataInputContext
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x000342E4 File Offset: 0x000324E4
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal ODataMetadataInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, readingResponse, synchronous, model, urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			try
			{
				this.baseXmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, false, messageReaderSettings.MessageQuotas.MaxNestingDepth);
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

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00034374 File Offset: 0x00032574
		internal override IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			return this.ReadMetadataDocumentImplementation(getReferencedModelReaderFunc);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00034380 File Offset: 0x00032580
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

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000343CC File Offset: 0x000325CC
		private IEdmModel ReadMetadataDocumentImplementation(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!EdmxReader.TryParse(this.xmlReader, getReferencedModelReaderFunc, out edmModel, out enumerable))
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

		// Token: 0x0400065E RID: 1630
		private XmlReader baseXmlReader;

		// Token: 0x0400065F RID: 1631
		private BufferingXmlReader xmlReader;
	}
}
