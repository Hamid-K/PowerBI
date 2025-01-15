using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000997 RID: 2455
	internal class TokenData
	{
		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06004BF7 RID: 19447 RVA: 0x0013045C File Offset: 0x0012E65C
		// (set) Token: 0x06004BF8 RID: 19448 RVA: 0x00130464 File Offset: 0x0012E664
		public TokenType TokenType { get; set; }

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06004BF9 RID: 19449 RVA: 0x0013046D File Offset: 0x0012E66D
		// (set) Token: 0x06004BFA RID: 19450 RVA: 0x00130475 File Offset: 0x0012E675
		public int StartIndex { get; set; }

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06004BFB RID: 19451 RVA: 0x0013047E File Offset: 0x0012E67E
		// (set) Token: 0x06004BFC RID: 19452 RVA: 0x00130486 File Offset: 0x0012E686
		public int EndIndex { get; set; }

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06004BFD RID: 19453 RVA: 0x0013048F File Offset: 0x0012E68F
		// (set) Token: 0x06004BFE RID: 19454 RVA: 0x00130497 File Offset: 0x0012E697
		public int ScopeWeight { get; set; }

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06004BFF RID: 19455 RVA: 0x001304A0 File Offset: 0x0012E6A0
		public int Length
		{
			get
			{
				return this.EndIndex - this.StartIndex + 1;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06004C00 RID: 19456 RVA: 0x001304B1 File Offset: 0x0012E6B1
		// (set) Token: 0x06004C01 RID: 19457 RVA: 0x001304B9 File Offset: 0x0012E6B9
		public Action<LinkedListNode<TokenData>>[] TokenActions { get; private set; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06004C02 RID: 19458 RVA: 0x001304C2 File Offset: 0x0012E6C2
		// (set) Token: 0x06004C03 RID: 19459 RVA: 0x001304CA File Offset: 0x0012E6CA
		public Parser Parser { get; private set; }

		// Token: 0x06004C04 RID: 19460 RVA: 0x001304D3 File Offset: 0x0012E6D3
		public TokenData(TokenType tokenType, Action<LinkedListNode<TokenData>>[] tokenActions, Parser parser, int start, int end, int weight)
		{
			this.TokenType = tokenType;
			this.TokenActions = tokenActions;
			this.Parser = parser;
			this.StartIndex = start;
			this.EndIndex = end;
			this.ScopeWeight = weight;
		}
	}
}
