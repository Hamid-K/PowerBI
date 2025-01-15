using System;
using System.Collections.Generic;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000042 RID: 66
	public sealed class FunctionCallQueryToken : QueryToken
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00009635 File Offset: 0x00007835
		public FunctionCallQueryToken(string name, IEnumerable<QueryToken> arguments)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.arguments = new ReadOnlyEnumerable<QueryToken>(arguments ?? ((IEnumerable<QueryToken>)QueryToken.EmptyTokens));
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009669 File Offset: 0x00007869
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionCall;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000966C File Offset: 0x0000786C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009674 File Offset: 0x00007874
		public IEnumerable<QueryToken> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x040001B0 RID: 432
		private readonly string name;

		// Token: 0x040001B1 RID: 433
		private readonly IEnumerable<QueryToken> arguments;
	}
}
