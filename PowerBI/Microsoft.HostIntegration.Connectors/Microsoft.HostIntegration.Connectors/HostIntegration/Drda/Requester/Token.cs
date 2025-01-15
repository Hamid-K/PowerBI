using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000995 RID: 2453
	internal class Token
	{
		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06004BEF RID: 19439 RVA: 0x00130401 File Offset: 0x0012E601
		// (set) Token: 0x06004BF0 RID: 19440 RVA: 0x00130409 File Offset: 0x0012E609
		public TokenType Type { get; private set; }

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x00130412 File Offset: 0x0012E612
		// (set) Token: 0x06004BF2 RID: 19442 RVA: 0x0013041A File Offset: 0x0012E61A
		public Action<LinkedListNode<TokenData>>[] TokenActions { get; private set; }

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x00130423 File Offset: 0x0012E623
		// (set) Token: 0x06004BF4 RID: 19444 RVA: 0x0013042B File Offset: 0x0012E62B
		public string Tag
		{
			get
			{
				return this._tokenTag;
			}
			private set
			{
				this._tokenTag = value;
			}
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x00130434 File Offset: 0x0012E634
		public Token(TokenType tokenType, string tag)
			: this(tokenType, tag, null)
		{
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0013043F File Offset: 0x0012E63F
		public Token(TokenType tokenType, string tag, Action<LinkedListNode<TokenData>>[] tokenActions)
		{
			this.Type = tokenType;
			this.Tag = tag;
			this.TokenActions = tokenActions;
		}

		// Token: 0x04003BE0 RID: 15328
		public string _tokenTag;
	}
}
