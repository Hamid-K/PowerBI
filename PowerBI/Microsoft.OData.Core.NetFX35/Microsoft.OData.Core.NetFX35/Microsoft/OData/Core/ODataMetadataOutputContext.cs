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
	// Token: 0x02000188 RID: 392
	internal sealed class ODataMetadataOutputContext : ODataOutputContext
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003444C File Offset: 0x0003264C
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal ODataMetadataOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				this.xmlWriter = ODataAtomWriterUtils.CreateXmlWriter(messageStream, messageWriterSettings, encoding);
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

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000344A8 File Offset: 0x000326A8
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000344B5 File Offset: 0x000326B5
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			ODataAtomWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.Flush();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000344DC File Offset: 0x000326DC
		internal override void WriteMetadataDocument()
		{
			IEnumerable<EdmError> enumerable;
			if (!EdmxWriter.TryWriteEdmx(base.Model, this.xmlWriter, EdmxTarget.OData, out enumerable))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (EdmError edmError in enumerable)
				{
					stringBuilder.AppendLine(edmError.ToString());
				}
				throw new ODataException(Strings.ODataMetadataOutputContext_ErrorWritingMetadata(stringBuilder.ToString()));
			}
			this.Flush();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00034560 File Offset: 0x00032760
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.xmlWriter != null)
				{
					this.xmlWriter.Flush();
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.xmlWriter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000660 RID: 1632
		private Stream messageOutputStream;

		// Token: 0x04000661 RID: 1633
		private XmlWriter xmlWriter;
	}
}
