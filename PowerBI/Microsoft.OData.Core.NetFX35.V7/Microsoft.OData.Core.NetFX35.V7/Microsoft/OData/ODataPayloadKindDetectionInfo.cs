using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000084 RID: 132
	internal sealed class ODataPayloadKindDetectionInfo
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0000E414 File Offset: 0x0000C614
		internal ODataPayloadKindDetectionInfo(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMediaType>(messageInfo.MediaType, "messageInfo.MediaType");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "readerSettings");
			this.contentType = messageInfo.MediaType;
			this.encoding = messageInfo.Encoding;
			this.messageReaderSettings = messageReaderSettings;
			this.model = messageInfo.Model;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000E46F File Offset: 0x0000C66F
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000E477 File Offset: 0x0000C677
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000E47F File Offset: 0x0000C67F
		internal ODataMediaType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000E487 File Offset: 0x0000C687
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "There is computation needed to get the encoding from the content type; thus a method.")]
		public Encoding GetEncoding()
		{
			return this.encoding ?? this.contentType.SelectEncoding();
		}

		// Token: 0x04000273 RID: 627
		private readonly ODataMediaType contentType;

		// Token: 0x04000274 RID: 628
		private readonly Encoding encoding;

		// Token: 0x04000275 RID: 629
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x04000276 RID: 630
		private readonly IEdmModel model;
	}
}
