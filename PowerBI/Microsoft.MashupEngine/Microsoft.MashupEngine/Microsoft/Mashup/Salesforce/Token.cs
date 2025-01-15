using System;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001FE RID: 510
	internal struct Token
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x00016DEE File Offset: 0x00014FEE
		public Token(string value)
		{
			this.value = value;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00016DF7 File Offset: 0x00014FF7
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000627 RID: 1575
		private readonly string value;
	}
}
