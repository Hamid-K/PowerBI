using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200007B RID: 123
	internal abstract class ODataDeserializer
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		protected ODataDeserializer(ODataInputContext inputContext)
		{
			this.inputContext = inputContext;
			this.ReaderValidator = this.inputContext.MessageReaderSettings.Validator;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000BAFD File Offset: 0x00009CFD
		internal ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.inputContext.MessageReaderSettings;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000BB0A File Offset: 0x00009D0A
		internal bool ReadingResponse
		{
			get
			{
				return this.inputContext.ReadingResponse;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000BB17 File Offset: 0x00009D17
		internal IEdmModel Model
		{
			get
			{
				return this.inputContext.Model;
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000BB24 File Offset: 0x00009D24
		internal PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector()
		{
			return this.inputContext.CreatePropertyAndAnnotationCollector();
		}

		// Token: 0x040001F3 RID: 499
		protected IReaderValidator ReaderValidator;

		// Token: 0x040001F4 RID: 500
		private readonly ODataInputContext inputContext;
	}
}
