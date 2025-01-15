using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200002A RID: 42
	internal abstract class ODataDeserializer
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000051E9 File Offset: 0x000033E9
		protected ODataDeserializer(ODataInputContext inputContext)
		{
			this.inputContext = inputContext;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000051F8 File Offset: 0x000033F8
		internal bool UseServerFormatBehavior
		{
			get
			{
				return this.inputContext.UseServerFormatBehavior;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005205 File Offset: 0x00003405
		internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.inputContext.UseDefaultFormatBehavior;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005212 File Offset: 0x00003412
		internal ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.inputContext.MessageReaderSettings;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000521F File Offset: 0x0000341F
		internal bool ReadingResponse
		{
			get
			{
				return this.inputContext.ReadingResponse;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000522C File Offset: 0x0000342C
		internal IEdmModel Model
		{
			get
			{
				return this.inputContext.Model;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005239 File Offset: 0x00003439
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return this.inputContext.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x0400010E RID: 270
		private readonly ODataInputContext inputContext;
	}
}
