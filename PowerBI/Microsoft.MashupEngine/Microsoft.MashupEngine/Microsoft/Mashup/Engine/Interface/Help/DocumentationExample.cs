using System;

namespace Microsoft.Mashup.Engine.Interface.Help
{
	// Token: 0x0200013F RID: 319
	public struct DocumentationExample
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0000909A File Offset: 0x0000729A
		public DocumentationExample(string description, string code, string result)
		{
			this.description = description;
			this.code = code;
			this.result = result;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000090B1 File Offset: 0x000072B1
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000090B9 File Offset: 0x000072B9
		public string Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000090C1 File Offset: 0x000072C1
		public string Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04000367 RID: 871
		private readonly string description;

		// Token: 0x04000368 RID: 872
		private readonly string code;

		// Token: 0x04000369 RID: 873
		private readonly string result;
	}
}
