using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000058 RID: 88
	internal abstract class ODataDeserializer
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00009C43 File Offset: 0x00007E43
		protected ODataDeserializer(ODataInputContext inputContext)
		{
			this.inputContext = inputContext;
			this.ReaderValidator = this.inputContext.MessageReaderSettings.Validator;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00009C68 File Offset: 0x00007E68
		internal ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.inputContext.MessageReaderSettings;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00009C75 File Offset: 0x00007E75
		internal bool ReadingResponse
		{
			get
			{
				return this.inputContext.ReadingResponse;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00009C82 File Offset: 0x00007E82
		internal IEdmModel Model
		{
			get
			{
				return this.inputContext.Model;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009C8F File Offset: 0x00007E8F
		internal PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector()
		{
			return this.inputContext.CreatePropertyAndAnnotationCollector();
		}

		// Token: 0x04000191 RID: 401
		protected IReaderValidator ReaderValidator;

		// Token: 0x04000192 RID: 402
		private readonly ODataInputContext inputContext;
	}
}
