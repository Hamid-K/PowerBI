using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BC RID: 444
	internal sealed class ODataPayloadKindDetectionInfo
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x0002E800 File Offset: 0x0002CA00
		internal ODataPayloadKindDetectionInfo(MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, IEdmModel model, IEnumerable<ODataPayloadKind> possiblePayloadKinds)
		{
			ExceptionUtils.CheckArgumentNotNull<MediaType>(contentType, "contentType");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "readerSettings");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataPayloadKind>>(possiblePayloadKinds, "possiblePayloadKinds");
			this.contentType = contentType;
			this.encoding = encoding;
			this.messageReaderSettings = messageReaderSettings;
			this.model = model;
			this.possiblePayloadKinds = possiblePayloadKinds;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002E85A File Offset: 0x0002CA5A
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002E862 File Offset: 0x0002CA62
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0002E86A File Offset: 0x0002CA6A
		public IEnumerable<ODataPayloadKind> PossiblePayloadKinds
		{
			get
			{
				return this.possiblePayloadKinds;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0002E872 File Offset: 0x0002CA72
		internal MediaType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0002E87A File Offset: 0x0002CA7A
		internal object PayloadKindDetectionFormatState
		{
			get
			{
				return this.payloadKindDetectionFormatState;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002E882 File Offset: 0x0002CA82
		public Encoding GetEncoding()
		{
			return this.encoding ?? this.contentType.SelectEncoding();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002E899 File Offset: 0x0002CA99
		public void SetPayloadKindDetectionFormatState(object state)
		{
			this.payloadKindDetectionFormatState = state;
		}

		// Token: 0x0400049A RID: 1178
		private readonly MediaType contentType;

		// Token: 0x0400049B RID: 1179
		private readonly Encoding encoding;

		// Token: 0x0400049C RID: 1180
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x0400049D RID: 1181
		private readonly IEdmModel model;

		// Token: 0x0400049E RID: 1182
		private readonly IEnumerable<ODataPayloadKind> possiblePayloadKinds;

		// Token: 0x0400049F RID: 1183
		private object payloadKindDetectionFormatState;
	}
}
