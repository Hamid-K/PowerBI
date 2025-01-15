using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000097 RID: 151
	internal abstract class ODataSerializer
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0000FEBA File Offset: 0x0000E0BA
		protected ODataSerializer(ODataOutputContext outputContext)
		{
			this.outputContext = outputContext;
			this.WriterValidator = outputContext.WriterValidator;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000FED5 File Offset: 0x0000E0D5
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.outputContext.MessageWriterSettings;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000FEE2 File Offset: 0x0000E0E2
		internal IODataPayloadUriConverter PayloadUriConverter
		{
			get
			{
				return this.outputContext.PayloadUriConverter;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000FEEF File Offset: 0x0000E0EF
		internal bool WritingResponse
		{
			get
			{
				return this.outputContext.WritingResponse;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		internal IEdmModel Model
		{
			get
			{
				return this.outputContext.Model;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000FF09 File Offset: 0x0000E109
		internal IDuplicatePropertyNameChecker CreateDuplicatePropertyNameChecker()
		{
			return this.MessageWriterSettings.Validator.CreateDuplicatePropertyNameChecker();
		}

		// Token: 0x040002BB RID: 699
		protected readonly IWriterValidator WriterValidator;

		// Token: 0x040002BC RID: 700
		private readonly ODataOutputContext outputContext;
	}
}
