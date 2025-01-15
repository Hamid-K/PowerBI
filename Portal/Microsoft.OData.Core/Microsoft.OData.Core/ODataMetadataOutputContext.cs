using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200009D RID: 157
	internal sealed class ODataMetadataOutputContext : ODataOutputContext
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00010360 File Offset: 0x0000E560
		internal ODataMetadataOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
			: base(ODataFormat.Metadata, messageInfo, messageWriterSettings)
		{
			try
			{
				this.messageOutputStream = messageInfo.MessageStream;
				this.xmlWriter = ODataMetadataWriterUtils.CreateXmlWriter(this.messageOutputStream, messageWriterSettings, messageInfo.Encoding);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.messageOutputStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000103C8 File Offset: 0x0000E5C8
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000103D5 File Offset: 0x0000E5D5
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			ODataMetadataWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.Flush();
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000103FC File Offset: 0x0000E5FC
		internal override void WriteMetadataDocument()
		{
			IEnumerable<EdmError> enumerable;
			if (!CsdlWriter.TryWriteCsdl(base.Model, this.xmlWriter, CsdlTarget.OData, out enumerable))
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

		// Token: 0x06000690 RID: 1680 RVA: 0x00010480 File Offset: 0x0000E680
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

		// Token: 0x04000293 RID: 659
		private Stream messageOutputStream;

		// Token: 0x04000294 RID: 660
		private XmlWriter xmlWriter;
	}
}
