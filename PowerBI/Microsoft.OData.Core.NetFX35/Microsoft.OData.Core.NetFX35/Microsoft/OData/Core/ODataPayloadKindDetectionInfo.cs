using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200018E RID: 398
	internal sealed class ODataPayloadKindDetectionInfo
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x00034791 File Offset: 0x00032991
		internal ODataPayloadKindDetectionInfo(ODataMediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMediaType>(contentType, "contentType");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "readerSettings");
			this.contentType = contentType;
			this.encoding = encoding;
			this.messageReaderSettings = messageReaderSettings;
			this.model = model;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x000347CC File Offset: 0x000329CC
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000347D4 File Offset: 0x000329D4
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x000347DC File Offset: 0x000329DC
		internal ODataMediaType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000347E4 File Offset: 0x000329E4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "There is computation needed to get the encoding from the content type; thus a method.")]
		public Encoding GetEncoding()
		{
			return this.encoding ?? this.contentType.SelectEncoding();
		}

		// Token: 0x04000684 RID: 1668
		private readonly ODataMediaType contentType;

		// Token: 0x04000685 RID: 1669
		private readonly Encoding encoding;

		// Token: 0x04000686 RID: 1670
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x04000687 RID: 1671
		private readonly IEdmModel model;
	}
}
