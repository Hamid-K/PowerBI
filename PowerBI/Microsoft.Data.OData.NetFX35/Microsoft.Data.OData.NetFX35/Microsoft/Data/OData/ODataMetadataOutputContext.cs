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
	// Token: 0x020001CB RID: 459
	internal sealed class ODataMetadataOutputContext : ODataOutputContext
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00030250 File Offset: 0x0002E450
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

		// Token: 0x06000D89 RID: 3465 RVA: 0x000302AC File Offset: 0x0002E4AC
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000302B9 File Offset: 0x0002E4B9
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			ODataAtomWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.Flush();
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000302E0 File Offset: 0x0002E4E0
		internal override void WriteMetadataDocument()
		{
			base.Model.SaveODataAnnotations();
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

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003036C File Offset: 0x0002E56C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
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
		}

		// Token: 0x040004B1 RID: 1201
		private Stream messageOutputStream;

		// Token: 0x040004B2 RID: 1202
		private XmlWriter xmlWriter;
	}
}
