using System;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000131 RID: 305
	[ImmutableObject(true)]
	public sealed class TokenizerRuleContext
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0000AB29 File Offset: 0x00008D29
		public TokenizerRuleContext(IDateTimeProvider dateTimeProvider, TokenizerRuleCategories categories, TokenizerRuleOptions options)
		{
			this.DateTimeProvider = dateTimeProvider;
			this.Categories = categories;
			this.Options = options;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000AB46 File Offset: 0x00008D46
		public IDateTimeProvider DateTimeProvider { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000AB4E File Offset: 0x00008D4E
		public TokenizerRuleCategories Categories { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000AB56 File Offset: 0x00008D56
		public TokenizerRuleOptions Options { get; }
	}
}
