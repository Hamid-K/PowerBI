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
	// Token: 0x02000077 RID: 119
	internal sealed class ODataMetadataOutputContext : ODataOutputContext
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000CFBC File Offset: 0x0000B1BC
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

		// Token: 0x0600047E RID: 1150 RVA: 0x0000D024 File Offset: 0x0000B224
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000D031 File Offset: 0x0000B231
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			ODataMetadataWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.Flush();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000D058 File Offset: 0x0000B258
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

		// Token: 0x06000481 RID: 1153 RVA: 0x0000D0DC File Offset: 0x0000B2DC
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

		// Token: 0x0400022D RID: 557
		private Stream messageOutputStream;

		// Token: 0x0400022E RID: 558
		private XmlWriter xmlWriter;
	}
}
