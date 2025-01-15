using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000032 RID: 50
	internal abstract class ODataSerializer
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000616B File Offset: 0x0000436B
		protected ODataSerializer(ODataOutputContext outputContext)
		{
			this.outputContext = outputContext;
			this.WriterValidator = outputContext.WriterValidator;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006186 File Offset: 0x00004386
		internal bool UseServerFormatBehavior
		{
			get
			{
				return this.outputContext.UseServerFormatBehavior;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006193 File Offset: 0x00004393
		internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.outputContext.UseDefaultFormatBehavior;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000061A0 File Offset: 0x000043A0
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.outputContext.MessageWriterSettings;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000061AD File Offset: 0x000043AD
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.outputContext.UrlResolver;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000061BA File Offset: 0x000043BA
		internal bool WritingResponse
		{
			get
			{
				return this.outputContext.WritingResponse;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000061C7 File Offset: 0x000043C7
		internal IEdmModel Model
		{
			get
			{
				return this.outputContext.Model;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000061D4 File Offset: 0x000043D4
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return new DuplicatePropertyNamesChecker(this.MessageWriterSettings.WriterBehavior.AllowDuplicatePropertyNames, this.WritingResponse, !this.MessageWriterSettings.EnableFullValidation);
		}

		// Token: 0x04000121 RID: 289
		protected readonly IWriterValidator WriterValidator;

		// Token: 0x04000122 RID: 290
		private readonly ODataOutputContext outputContext;
	}
}
