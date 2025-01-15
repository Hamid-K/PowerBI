using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000B9 RID: 185
	internal abstract class ODataSerializer
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x000138B5 File Offset: 0x00011AB5
		protected ODataSerializer(ODataOutputContext outputContext)
		{
			this.outputContext = outputContext;
			this.WriterValidator = outputContext.WriterValidator;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000138D0 File Offset: 0x00011AD0
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.outputContext.MessageWriterSettings;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000138DD File Offset: 0x00011ADD
		internal IODataPayloadUriConverter PayloadUriConverter
		{
			get
			{
				return this.outputContext.PayloadUriConverter;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000138EA File Offset: 0x00011AEA
		internal bool WritingResponse
		{
			get
			{
				return this.outputContext.WritingResponse;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000138F7 File Offset: 0x00011AF7
		internal IEdmModel Model
		{
			get
			{
				return this.outputContext.Model;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00013904 File Offset: 0x00011B04
		internal IDuplicatePropertyNameChecker CreateDuplicatePropertyNameChecker()
		{
			return this.MessageWriterSettings.Validator.CreateDuplicatePropertyNameChecker();
		}

		// Token: 0x04000323 RID: 803
		protected readonly IWriterValidator WriterValidator;

		// Token: 0x04000324 RID: 804
		private readonly ODataOutputContext outputContext;
	}
}
