using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000A9 RID: 169
	internal sealed class ODataPayloadKindDetectionInfo
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x00011B2C File Offset: 0x0000FD2C
		internal ODataPayloadKindDetectionInfo(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMediaType>(messageInfo.MediaType, "messageInfo.MediaType");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "readerSettings");
			this.contentType = messageInfo.MediaType;
			this.encoding = messageInfo.Encoding;
			this.messageReaderSettings = messageReaderSettings;
			this.model = messageInfo.Model;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00011B87 File Offset: 0x0000FD87
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00011B8F File Offset: 0x0000FD8F
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00011B97 File Offset: 0x0000FD97
		internal ODataMediaType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00011B9F File Offset: 0x0000FD9F
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "There is computation needed to get the encoding from the content type; thus a method.")]
		public Encoding GetEncoding()
		{
			return this.encoding ?? this.contentType.SelectEncoding();
		}

		// Token: 0x040002D9 RID: 729
		private readonly ODataMediaType contentType;

		// Token: 0x040002DA RID: 730
		private readonly Encoding encoding;

		// Token: 0x040002DB RID: 731
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x040002DC RID: 732
		private readonly IEdmModel model;
	}
}
