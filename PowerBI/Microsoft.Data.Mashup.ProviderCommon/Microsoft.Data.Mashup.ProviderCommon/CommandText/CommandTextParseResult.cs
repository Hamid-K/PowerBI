using System;
using System.Collections.Generic;

namespace Microsoft.Data.Mashup.ProviderCommon.CommandText
{
	// Token: 0x0200001C RID: 28
	internal class CommandTextParseResult
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00006732 File Offset: 0x00004932
		public CommandTextParseResult(string expression, IEnumerable<string> resourceNames)
		{
			this.expression = expression;
			this.resourceNames = resourceNames;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006748 File Offset: 0x00004948
		public string Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006750 File Offset: 0x00004950
		public IEnumerable<string> ResourceNames
		{
			get
			{
				return this.resourceNames;
			}
		}

		// Token: 0x0400009C RID: 156
		private readonly string expression;

		// Token: 0x0400009D RID: 157
		private readonly IEnumerable<string> resourceNames;
	}
}
